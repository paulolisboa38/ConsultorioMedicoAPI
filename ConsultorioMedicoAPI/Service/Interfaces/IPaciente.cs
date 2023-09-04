using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.Service.Interfaces
{
    public interface IPacienteService
    {
        // Obter todas as consultas associadas a um paciente específico
        Task<IEnumerable<Consulta>> GetConsultasPorPacienteIdAsync(int id);

        // Atualizar informações de um paciente, como o número de telefone
        Task<Paciente> UpdatePacienteAsync(UpdatePacienteDTO updatePacienteDTO);

        // Obter todos os pacientes com mais de uma certa idade
        Task<IEnumerable<Paciente>> GetPacientesPorIdadeAsync(DateTime dataNascimento);

        // Criar um novo registro de paciente
        Task<Paciente> CreatePacienteAsync(CreatePacienteDTO createPacienteDTO);

        // Atualizar o endereço de um paciente específico
        Task<Paciente> UpdatePacienteEnderecoAsync(int id,UpdatePacienteEnderecoDTO updatePacienteEnderecoDTO);
    }
}
