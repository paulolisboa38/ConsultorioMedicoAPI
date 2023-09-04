using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.Service.Interfaces
{
    public interface IMedicoService
    {
        Task<Medico> CriarMedico(MedicoDTO medico);
        Task<List<Medico>> ListarMedicoPorEspecialidade(string especialidade);
        Task<List<Medico>> ListarTodosMedicos();
        Task<Medico> BuscarMedicoPorCRM(string crm);
        Task<Medico> AtualizarEspecialidadeMedica(int id, string especialidade);
        Task<Medico> AtualizarMedico(int id, MedicoDTO medico);
        Task<List<Consulta>> LIstarConsultasPorMedico(int id);
    }
}