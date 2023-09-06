using ConsultorioMedicoAPI.Data;
using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using ConsultorioMedicoAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConsultorioMedicoAPI.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly DataContext _dataContext;

        public PacienteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Consulta>> GetConsultasPorPacienteIdAsync(int id)
        {
            return await _dataContext.Consultas
                .Where(p => p.PacienteId == id)
                .ToListAsync();
        }

        private static DateTime CalcularDataNascimentoPorIdade(int idade)
        {
            DateTime hoje = DateTime.Today;
            DateTime dataNascimento = new DateTime(hoje.Year - idade,hoje.Month,hoje.Day);
            if (hoje.Month < dataNascimento.Month ||
               (hoje.Month == dataNascimento.Month && hoje.Day < dataNascimento.Day))
            {
                dataNascimento = dataNascimento.AddYears(-1);
            }
            return dataNascimento;
        }

        public async Task<IEnumerable<Paciente>?> GetPacientesPorIdadeAsync(int idadeAcima)
        {
            var pacienteDataNascAprox = CalcularDataNascimentoPorIdade(idadeAcima);
            if (!Validadores.VerificarAnoNascimento(pacienteDataNascAprox.Year))
            {
                return null;
            }
            var pacientes = await _dataContext.Pacientes
               .Include(p => p.Endereco)
               .Where(p => p.DataDeNascimento <= pacienteDataNascAprox)
               .ToListAsync();
            return pacientes;
        }

        public async Task<Paciente?> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO)
        {
            var endereco = new Endereco()
            {
                CEP = createPacienteDTO.Endereco.CEP,
                Pais = createPacienteDTO.Endereco.Pais,
                Estado = createPacienteDTO.Endereco.Estado,
                Cidade = createPacienteDTO.Endereco.Cidade,
                Bairro = createPacienteDTO.Endereco.Bairro,
                Rua = createPacienteDTO.Endereco.Rua,
                Complemento = createPacienteDTO.Endereco.Complemento,
                Numero = createPacienteDTO.Endereco.Numero
            };

            var paciente = new Paciente
            {
                Nome = createPacienteDTO.Nome,
                DataDeNascimento = new DateTime(
                    createPacienteDTO.DataNascimento.Ano,
                    createPacienteDTO.DataNascimento.Mes,
                    createPacienteDTO.DataNascimento.Dia),
                CPF = createPacienteDTO.CPF,
                Telefone = createPacienteDTO.Telefone,
                Endereco = endereco,
                Email = createPacienteDTO.Email,
                Genero = createPacienteDTO.Genero,
                Alerta = createPacienteDTO.Alerta
            };
            await _dataContext.Pacientes.AddAsync(paciente);
            await _dataContext.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente?> UpdatePacienteTelefoneAsync(int id,UpdatePacienteTelefoneDTO updatePacienteTelefoneDTO)
        {
            var pacienteDb = await _dataContext.Pacientes.FindAsync(id);
            if (pacienteDb == null)
            {
                return null;
            }
            pacienteDb.Telefone = updatePacienteTelefoneDTO.Telefone;
            return pacienteDb;
        }

        public async Task<Paciente?> UpdatePacienteEnderecoAsync(int id,UpdatePacienteEnderecoDTO updatePacienteEnderecoDTO)
        {
            var pacienteEnderecoDb = await _dataContext.Pacientes
                .Include(e => e.Endereco)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (pacienteEnderecoDb is null)
            {
                return null;
            }
            if (pacienteEnderecoDb.Endereco == null || pacienteEnderecoDb.Endereco.Id == 0)
            {
                pacienteEnderecoDb.Endereco = new Endereco();
            }
            pacienteEnderecoDb.Endereco.CEP = updatePacienteEnderecoDTO.Endereco.CEP;
            pacienteEnderecoDb.Endereco.Pais = updatePacienteEnderecoDTO.Endereco.Pais;
            pacienteEnderecoDb.Endereco.Estado = updatePacienteEnderecoDTO.Endereco.Estado;
            pacienteEnderecoDb.Endereco.Cidade = updatePacienteEnderecoDTO.Endereco.Cidade;
            pacienteEnderecoDb.Endereco.Bairro = updatePacienteEnderecoDTO.Endereco.Bairro;
            pacienteEnderecoDb.Endereco.Rua = updatePacienteEnderecoDTO.Endereco.Rua;
            pacienteEnderecoDb.Endereco.Complemento = updatePacienteEnderecoDTO.Endereco.Complemento;
            pacienteEnderecoDb.Endereco.Numero = updatePacienteEnderecoDTO.Endereco.Numero;
            await _dataContext.SaveChangesAsync();
            return pacienteEnderecoDb;
        }
    }
}
