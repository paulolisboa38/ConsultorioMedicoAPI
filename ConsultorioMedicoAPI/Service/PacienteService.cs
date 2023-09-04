using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;

namespace ConsultorioMedicoAPI.Service
{
    public class PacienteService : IPacienteService
    {
        public Task<Paciente> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Consulta>> GetConsultasPorPacienteIdAsync(int id)
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
