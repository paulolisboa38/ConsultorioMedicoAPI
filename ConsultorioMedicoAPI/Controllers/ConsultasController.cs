using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ConsultorioMedicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultasController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Consulta>>> ListarTodasConsultas()
        {
            var consultas = await _consultaService.ListarTodasConsultas();

            if (consultas.IsNullOrEmpty()) { return NotFound(consultas); }

            return Ok(consultas);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta?>> BuscarConsultaPorId(int id)
        {
            var consulta = await _consultaService.BuscarConsultaPorId(id);
            if (consulta is null)
            {
                return NotFound(new { message = $"Consulta com o Id-{id} não encontrada." });
            }
            return Ok(consulta);
        }

        [HttpGet("data={dia}-{mes}-{ano}")]
        public async Task<ActionResult<List<Consulta>>> BuscarConsultasPorData(int dia,int mes,int ano)
        {
            var consultas = await _consultaService.BuscarConsultasPorData(dia,mes,ano);
            if (consultas.IsNullOrEmpty())
            {
                return NotFound(new { message = $"Nenhuma consulta encontrada com a data: {dia}/{mes}/{ano}" });
            }
            return Ok(consultas);
        }

        [HttpPost]
        public async Task<ActionResult<Consulta>> CriarConsultaAgendada(ConsultaDTO agendamento)
        {
            if (Utils.Validadores.VerificarZeroNaData(agendamento.DataConsulta.Dia) ||
                Utils.Validadores.VerificarZeroNaData(agendamento.DataConsulta.Mes) ||
                Utils.Validadores.VerificarZeroNaData(agendamento.DataConsulta.Ano))
            { return BadRequest("A data não pode iniciar com zero!"); }

            var consulta = await _consultaService.CriarConsultaAgendada(agendamento);

            if (consulta.Id is 0) { return BadRequest("Data indisponivel!"); }

            return Ok(consulta);
        }

        [HttpDelete("{idConsulta}")]
        public async Task<ActionResult<string>> DeletarConsulta(int idConsulta)
        {
            var retorno = await _consultaService.DeletarConsulta(idConsulta);

            if (!retorno) { return NotFound($"ID : {idConsulta} não encontrado!"); }

            return NoContent();
        }
    }
}
