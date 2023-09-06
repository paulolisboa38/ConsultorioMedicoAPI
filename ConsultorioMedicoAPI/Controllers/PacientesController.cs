using ConsultorioMedicoAPI.DTOs;
using ConsultorioMedicoAPI.Models;
using ConsultorioMedicoAPI.Service.Interfaces;
using ConsultorioMedicoAPI.Utils;
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

        [HttpGet("{id}/consultas")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultasPorPacienteId(int id)
        {
            var consultas = await _pacienteService.GetConsultasPorPacienteIdAsync(id);
            if (consultas is null || !consultas.Any())
            {
                return NotFound(new { message = $"Nenhuma consulta encontrada para o paciente com o id-{id}." });
            }
            return Ok(consultas);
        }

        [HttpGet("idade_maior_que={idade}")]
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
            if (Validadores.VerificarZeroNaData(createPacienteDTO.DataNascimento.Dia) ||
                Validadores.VerificarZeroNaData(createPacienteDTO.DataNascimento.Mes) ||
                Validadores.VerificarZeroNaData(createPacienteDTO.DataNascimento.Ano))
            {
                return BadRequest(new { message = "A data não pode iniciar com zero!" });
            }

            var paciente = await _pacienteService.CreatePacienteAsync(createPacienteDTO);
            if (paciente is null)
            {
                return BadRequest(new { message = $"Informe os dados corretos para cadastro." });
            }
            return Ok(paciente);
        }

        [HttpPut("{id}/telefone")]
        public async Task<ActionResult<Paciente>> UpdatePacienteTelefone(int id,UpdatePacienteTelefoneDTO updatePacienteTelefoneDTO)
        {
            var pacienteAtualizado = await _pacienteService.UpdatePacienteTelefoneAsync(id,updatePacienteTelefoneDTO);
            if (pacienteAtualizado is null)
            {
                return NotFound(new { message = $"Paciente com o Id-{id} não encontrado para atualização." });
            }
            return Ok(pacienteAtualizado);
        }

        [HttpPatch("{id}/endereco")]
        public async Task<ActionResult<Paciente?>> UpdatePacienteEndereco(int id,UpdatePacienteEnderecoDTO updatePacienteEnderecoDTO)
        {
            var pacienteEnderecoAtualizado = await _pacienteService.UpdatePacienteEnderecoAsync(id,updatePacienteEnderecoDTO);
            if (pacienteEnderecoAtualizado is null)
            {
                return NotFound(new { message = $"Paciente com o Id-{id} não encontrado para atualização." });
            }
            return Ok(pacienteEnderecoAtualizado);
        }
    }
}
