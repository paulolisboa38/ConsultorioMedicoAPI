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
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CriarMedico(MedicoDTO novoMedico)
        {
            var medico = await _medicoService.CriarMedico(novoMedico);

            if (medico.CRM.IsNullOrEmpty()) { return BadRequest("JÁ EXISTE UM REGISTRO COM ESSE CRM!"); }

            return Ok(medico);
        }

        [HttpGet]   
        public async Task<ActionResult<List<Medico>>> ListarTodosMedicos()
        {
            var listaMedicos = await _medicoService.ListarTodosMedicos();

            return Ok(listaMedicos);
        }
    }
}
