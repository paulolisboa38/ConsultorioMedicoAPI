using ConsultorioMedicoAPI.Data;
using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConsultorioMedicoAPI.Service
{
    public class MedicoService : IMedicoService
    {
        private readonly DataContext _dataContext;

        public MedicoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Medico> AtualizarEspecialidadeMedica(int id, string especialidade)
        {
            throw new NotImplementedException();
        }

        public async Task<Medico> AtualizarMedico(int id, MedicoDTO medico)
        {
            throw new NotImplementedException();
        }

        public async Task<Medico> BuscarMedicoPorCRM(string crm)
        {
            var medico = await _dataContext.Medicos.FirstOrDefaultAsync(m => m.CRM == crm);

            return medico;
        }

        public async Task<Medico> CriarMedico(MedicoDTO novoMedico)
        {
            var consultaCRM = await BuscarMedicoPorCRM(novoMedico.CRM);

            if (consultaCRM is null)
            {
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
            else { return new Medico(); }
        }

        public async Task<List<Consulta>> LIstarConsultasPorMedico(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Medico>> ListarMedicoPorEspecialidade(string especialidade)
        {
            var listaRetorno = await _dataContext.Medicos.Where(m => m.Especialidade == especialidade).ToListAsync();

            return listaRetorno;
        }

        public async Task<List<Medico>> ListarTodosMedicos()
        {
            var listaRetorno = await _dataContext.Medicos.ToListAsync();

            return listaRetorno;
        }
    }
}
