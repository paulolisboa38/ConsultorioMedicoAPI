using ConsultorioMedicoAPI.Data;
using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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
               .Where(p => p.DataDeNascimento >= pacienteDataNascAprox)
               .ToListAsync();
            return pacientes;
        }

        public Task<Paciente> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Paciente> UpdatePacienteAsync(UpdatePacienteDTO updatePacienteDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Paciente> UpdatePacienteEnderecoAsync(int id,UpdatePacienteEnderecoDTO updatePacienteEnderecoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
