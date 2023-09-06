using ConsultorioMedicoAPI.Data;
using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedicoAPI.Service
{
    public class ConsultaService : IConsultaService
    {
        private readonly DataContext _dataContext;

        public ConsultaService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Consulta> BuscarConsultaPorId(int idConsulta)
        {
            throw new NotImplementedException();
        }

        public async Task<Consulta> CriarConsultaAgendada(ConsultaDTO agendamento)
        {
            var dataAgendamento = new DateTime(agendamento.DataConsulta.Ano, agendamento.DataConsulta.Mes, agendamento.DataConsulta.Dia);

            var verificarData = await _dataContext.Consultas.FirstOrDefaultAsync(d => d.DataConsulta == dataAgendamento &&
            d.MedicoId == agendamento.MedicoId);

            if (verificarData is not null) { return new Consulta(); }

            var medico = await _dataContext.Medicos.FindAsync(agendamento.MedicoId);

            var paciente = await _dataContext.Pacientes.FindAsync(agendamento.PacienteId);

            var consulta = new Consulta()
            {
                DataConsulta = dataAgendamento,
                MedicoId = agendamento.MedicoId,
                Medico = medico,
                PacienteId = agendamento.PacienteId,
                Paciente = paciente                
            };

            _dataContext.Consultas.Add(consulta);
            await _dataContext.SaveChangesAsync();

            return consulta;
        }

        public async Task<bool> DeletarConsulta(int idConsulta)
        {
            var consulta = await _dataContext.Consultas.FindAsync(idConsulta);

            if (consulta is null) { return false; }

            _dataContext.Remove(consulta);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Consulta>> ListarTodasConsultas()
        {
            return await _dataContext.Consultas.ToListAsync();
        }
    }
}
