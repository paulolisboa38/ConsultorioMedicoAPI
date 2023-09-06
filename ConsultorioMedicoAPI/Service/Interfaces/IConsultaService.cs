using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.Service.Interfaces
{
    public interface IConsultaService
    {
        Task<List<Consulta>> ListarTodasConsultas();
        Task<Consulta> BuscarConsultaPorId(int idConsulta);
        Task<List<Consulta>> BuscarConsultasPorData(int dia, int mes, int ano);
        Task<Consulta> CriarConsultaAgendada(ConsultaDTO agendamento);
        Task<bool> DeletarConsulta(int idConsulta);
    }
}
