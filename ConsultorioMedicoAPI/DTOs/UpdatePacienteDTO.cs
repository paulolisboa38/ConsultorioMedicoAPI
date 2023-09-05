using ConsultorioMedicoAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ConsultorioMedicoAPI.DTOs
{
    public class UpdatePacienteDTO
    {
        public string? Nome { get; set; }

        [Required]
        public string? DataDeNascimento { get; set; }

        public string? Telefone { get; set; }
        public Endereco? Endereco { get; set; }
        public string? Email { get; set; }
        public string? Genero { get; set; }
        public string? Alerta { get; set; }
    }
}
