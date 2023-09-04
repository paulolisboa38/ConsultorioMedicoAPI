using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioMedicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultasPorPacienteId(int id)
        {
            var consultas = await _pacienteService.GetConsultasPorPacienteIdAsync(id);
            if (consultas is null || !consultas.Any())
            {
                return NotFound(new { message = $"Nenhuma consulta encontrada para o paciente com o id-{id}." });
            }
            return Ok(consultas);
        }
    }
}
