using ConsultorioMedicoAPI.DTOs;
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

        //https://localhost:7121/api/Pacientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultasPorPacienteId(int id)
        {
            var consultas = await _pacienteService.GetConsultasPorPacienteIdAsync(id);
            if (consultas is null || !consultas.Any())
            {
                return NotFound(new { message = $"Nenhuma consulta encontrada para o paciente com o id-{id}." });
            }
            return Ok(consultas);
        }

        //https://localhost:7121/api/Pacientes/idade_maior_que/60
        [HttpGet("idade_maior_que/{idade}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesPorIdade(int idade)
        {
            var pacientes = await _pacienteService.GetPacientesPorIdadeAsync(idade);
            if (pacientes is null || !pacientes.Any())
            {
                return NotFound(new { message = $"Nenhum paciente com a idade acima de {idade} anos encontrado." });
            }
            return Ok(pacientes);
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente(CreatePacienteDTO createPacienteDTO)
        {
            if (createPacienteDTO is null)
            {
                return BadRequest(new { message = $"Informe os dados corretos para cadastro." });
            }
            var paciente = await _pacienteService.CreatePacienteAsync(createPacienteDTO);
            return paciente;
        }

    }
}
