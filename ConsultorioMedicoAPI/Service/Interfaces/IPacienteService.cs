using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.Service.Interfaces
{
    public interface IPacienteService
    {
        // Obter todas as consultas associadas a um paciente específico
        Task<IEnumerable<Consulta>> GetConsultasPorPacienteIdAsync(int id);

        // Obter todos os pacientes com mais de uma certa idade
        Task<IEnumerable<Paciente>> GetPacientesPorIdadeAsync(int idadeAcima);

        // Criar um novo registro de paciente
        Task<Paciente> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO);

        // Atualizar informações de um paciente, como o número de telefone
        Task<Paciente> UpdatePacienteAsync(int id,UpdatePacienteDTO updatePacienteDTO);

        // Atualizar o endereço de um paciente específico
        Task<Paciente> UpdatePacienteEnderecoAsync(int id,UpdatePacienteEnderecoDTO updatePacienteEnderecoDTO);
    }
}
