using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ConsultorioMedicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CriarMedico(MedicoDTO novoMedico)
        {
            if (Utils.Validadores.VerificarZeroNaData(novoMedico.DataNascimento.Dia) ||
                Utils.Validadores.VerificarZeroNaData(novoMedico.DataNascimento.Mes) ||
                Utils.Validadores.VerificarZeroNaData(novoMedico.DataNascimento.Ano))
            { return BadRequest("A data não pode iniciar com zero!"); }

            var medico = await _medicoService.CriarMedico(novoMedico);

            if (medico.Id is 0) { return BadRequest("JÁ EXISTE UM REGISTRO COM ESSE CRM!"); }

            return Ok(medico);
        }

        [HttpGet]
        public async Task<ActionResult<List<Medico>>> ListarTodosMedicos()
        {
            var listaMedicos = await _medicoService.ListarTodosMedicos();

            if (listaMedicos.IsNullOrEmpty()) { return NotFound(listaMedicos); }

            return Ok(listaMedicos);
        }

        [HttpGet("especialidade={especialidade}")]
        public async Task<ActionResult<List<Medico>>> ListarMedicosPorEspecialidade(string especialidade)
        {
            var listaMedicos = await _medicoService.ListarMedicoPorEspecialidade(especialidade);

            if (listaMedicos.IsNullOrEmpty()) { return NotFound("Nenhum médico cadastrado com essa especialidade!"); }

            return Ok(listaMedicos);
        }

        [HttpGet("disponiveis/data={dia}-{mes}-{ano}&especialidade={especialidade}")]
        public async Task<ActionResult<List<Medico>>> ListarMedicosDisponiveisPorDataEspecialidade(int dia, int mes, int ano, string especialidade)
        {
            if (Utils.Validadores.VerificarZeroNaData(dia) || 
                Utils.Validadores.VerificarZeroNaData(mes) ||
                Utils.Validadores.VerificarZeroNaData(ano)) 
            { return BadRequest("A data não pode conter zero(s) a frente!"); }

            var listaMedicos = await _medicoService.ListarMedicosDisponiveis(dia, mes, ano, especialidade);

            if (listaMedicos.IsNullOrEmpty()) { return NotFound("Nenhum médico cadastrado com essa especialidade!"); }

            return Ok(listaMedicos);
        }

        [HttpGet("{id}/consultas")]
        public async Task<ActionResult<List<Consulta>>> ListarConsultasPorMedico(int id)
        {
            var listaConsultas = await _medicoService.ListarConsultasPorMedico(id);

            if (listaConsultas.IsNullOrEmpty()) { return NotFound("Esse medico não possui consultas cadastradas!"); }

            return Ok(listaConsultas);
        }

        [HttpGet("{crm}")]
        public async Task<ActionResult<List<Consulta>>> BuscarMedicoPorCRM(string crm)
        {
            var medico = await _medicoService.BuscarMedicoPorCRM(crm);

            if (medico.Id is 0) { return NotFound($"CRM : {crm} não encontrado!"); }

            return Ok(medico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medico>> AtualizarMedico(int id, MedicoDTO medicoAtualizado)
        {
            if (Utils.Validadores.VerificarZeroNaData(medicoAtualizado.DataNascimento.Dia) || 
                Utils.Validadores.VerificarZeroNaData(medicoAtualizado.DataNascimento.Mes) || 
                Utils.Validadores.VerificarZeroNaData(medicoAtualizado.DataNascimento.Ano)) 
            { return BadRequest("A data não pode conter zero(s) a frente!"); }

            var atualizarMedico = await _medicoService.AtualizarMedico(id, medicoAtualizado);

            if (atualizarMedico.Id is 0) { return NotFound($"Id : {id} não encontrado!"); }

            return Ok(atualizarMedico);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Medico>> AtualizarEspecialidadeMedico(int id, string especialidade)
        {
            var atualizarMedico = await _medicoService.AtualizarEspecialidadeMedico(id, especialidade);

            if (atualizarMedico.Id is 0) { return NotFound($"Id : {id} não encontrado!"); }

            return Ok(atualizarMedico);
        }        
    }
}
