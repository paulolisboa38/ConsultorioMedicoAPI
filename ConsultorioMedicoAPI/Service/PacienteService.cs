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

        public Task<Paciente> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Paciente>> GetPacientesPorIdadeAsync(DateTime dataNascimento)
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
