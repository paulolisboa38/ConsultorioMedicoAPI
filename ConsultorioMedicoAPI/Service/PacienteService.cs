using ConsultorioMedicoAPI.Data;
using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
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
                .Where(c => c.Id == id)
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

        public async Task<IEnumerable<Paciente>> GetPacientesPorIdadeAsync(int idadeAcima)
        {
            var pacienteDataNascAprox = CalcularDataNascimentoPorIdade(idadeAcima);
            var pacientes = await _dataContext.Pacientes
               .Where(p => p.DataDeNascimento <= pacienteDataNascAprox)
               .ToListAsync();
            return pacientes;
        }

        public async Task<Paciente?> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO)
        {
            if (DateTime.TryParseExact(createPacienteDTO.DataDeNascimento,"dd/MM/yyyy",
                CultureInfo.InvariantCulture,DateTimeStyles.None,out DateTime parsedDate))
            {
                var paciente = new Paciente
                {
                    Nome = createPacienteDTO.Nome,
                    DataDeNascimento = parsedDate,
                    CPF = createPacienteDTO.CPF,
                    Telefone = createPacienteDTO.Telefone,
                    Endereco = createPacienteDTO.Endereco,
                    Email = createPacienteDTO.Email,
                    Genero = createPacienteDTO.Genero,
                    Alerta = createPacienteDTO.Alerta
                };
                await _dataContext.Pacientes.AddAsync(paciente);
                await _dataContext.SaveChangesAsync();
                return paciente;
            }
            else
            {
                return null;
            }
        }

        public async Task<Paciente?> UpdatePacienteAsync(int id,UpdatePacienteDTO updatePacienteDTO)
        {
            var pacienteDb = await _dataContext.Pacientes.FindAsync(id);
            if (pacienteDb == null)
            {
                return null;
            }
            if (DateTime.TryParseExact(updatePacienteDTO.DataDeNascimento,"dd/MM/yyyy",
                CultureInfo.InvariantCulture,DateTimeStyles.None,out DateTime parsedDate)
                || updatePacienteDTO.DataDeNascimento == string.Empty)
            {
                pacienteDb.Nome = updatePacienteDTO.Nome;
                pacienteDb.DataDeNascimento = parsedDate;
                pacienteDb.Telefone = updatePacienteDTO.Telefone;
                pacienteDb.Endereco = updatePacienteDTO.Endereco;
                pacienteDb.Email = updatePacienteDTO.Email;
                pacienteDb.Genero = updatePacienteDTO.Genero;
                pacienteDb.Alerta = updatePacienteDTO.Alerta;
                await _dataContext.SaveChangesAsync();
                return pacienteDb;
            };
            return null;
        }

        public Task<Paciente> UpdatePacienteEnderecoAsync(int id,UpdatePacienteEnderecoDTO updatePacienteEnderecoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
