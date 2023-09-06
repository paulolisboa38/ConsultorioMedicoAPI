using ConsultorioMedicoAPI.Data;
using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedicoAPI.Service
{
    public class MedicoService : IMedicoService
    {
        private readonly DataContext _dataContext;

        public MedicoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Medico> AtualizarEspecialidadeMedico(int id, string especialidade)
        {
            var medico = await _dataContext.Medicos.FindAsync(id);

            if (medico is null) { return new Medico(); }

            medico.Especialidade = especialidade;

            _dataContext.Medicos.Update(medico);
            await _dataContext.SaveChangesAsync();

            return medico;
        }

        public async Task<Medico> AtualizarMedico(int id, MedicoDTO medicoRequest)
        {
            var medico = await _dataContext.Medicos.FindAsync(id);

            if (medico is null) { return new Medico(); }

            var dataNascimento = new DateTime(
                medicoRequest.DataNascimento.Ano,
                medicoRequest.DataNascimento.Mes,
                medicoRequest.DataNascimento.Dia);

            medico.Nome = medicoRequest.Nome;
            medico.Email = medicoRequest.Email;
            medico.Telefone = medicoRequest.Telefone;
            medico.CRM = medicoRequest.CRM;
            medico.AtivoCRM = medicoRequest.AtivoCRM;
            medico.Especialidade = medicoRequest.Especialidade;
            medico.Genero = medicoRequest.Genero;
            medico.DataNascimento = dataNascimento;

            _dataContext.Medicos.Update(medico);
            await _dataContext.SaveChangesAsync();

            return medico;
        }

        public async Task<Medico?> BuscarMedicoPorCRM(string crm)
        {
            var medico = await _dataContext.Medicos.FirstOrDefaultAsync(m => m.CRM == crm);

            return medico;
        }

        public async Task<Medico> CriarMedico(MedicoDTO novoMedico)
        {
            var consultaCRM = await BuscarMedicoPorCRM(novoMedico.CRM);

            if (consultaCRM is not null) { return new Medico(); }

            var endereco = new Endereco()
            {
                CEP = novoMedico.Endereco.CEP,
                Pais = novoMedico.Endereco.Pais,
                Estado = novoMedico.Endereco.Estado,
                Cidade = novoMedico.Endereco.Cidade,
                Bairro = novoMedico.Endereco.Bairro,
                Rua = novoMedico.Endereco.Rua,
                Complemento = novoMedico.Endereco.Complemento,
                Numero = novoMedico.Endereco.Numero
            };

                var medico = new Medico
                {
                    Nome = novoMedico.Nome,
                    AtivoCRM = novoMedico.AtivoCRM,
                    CRM = novoMedico.CRM,
                    DataNascimento = new DateTime(
                        novoMedico.DataNascimento.Ano,
                        novoMedico.DataNascimento.Mes, 
                        novoMedico.DataNascimento.Dia), 
                    Email = novoMedico.Email,
                    Endereco = endereco,
                    Especialidade = novoMedico.Especialidade,
                    Genero = novoMedico.Genero,
                    Telefone = novoMedico.Telefone
                };

            _dataContext.Add(medico);
            await _dataContext.SaveChangesAsync();

            return medico;
        }

        public async Task<List<Consulta>> ListarConsultasPorMedico(int id)
        {
            var listaRetorno = await _dataContext.Consultas.Where(m => m.MedicoId == id).ToListAsync();

            return listaRetorno;
        }

        public async Task<List<Medico>> ListarMedicoPorEspecialidade(string especialidade)
        {
            var listaRetorno = await _dataContext.Medicos.Where(m => m.Especialidade == especialidade).ToListAsync();

            return listaRetorno;
        }

        public async Task<List<Medico>> ListarMedicosDisponiveis(int dia, int mes, int ano, string especialidade)
        {
            var dataConsulta = new DateTime(ano, mes, dia);

            var medicosDisponiveis = await _dataContext.Medicos
                .Where(m => m.Especialidade == especialidade)
                .Include(m => m.Consultas)
                .Where(m => !m.Consultas
                    .Any(c => c.DataConsulta.Date.Year == dataConsulta.Year &&
                              c.DataConsulta.Date.Month == dataConsulta.Month &&
                              c.DataConsulta.Date.Day == dataConsulta.Day))
                .ToListAsync();

            return medicosDisponiveis;
        }
        public async Task<List<Medico>> ListarTodosMedicos()
        {
            var listaRetorno = await _dataContext.Medicos.ToListAsync();

            return listaRetorno;
        }
    }
}
