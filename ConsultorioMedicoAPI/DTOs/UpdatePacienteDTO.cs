using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.DTOs
{
    public class UpdatePacienteDTO
    {
        public string? Nome { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public string? Telefone { get; set; }
        public Endereco? Endereco { get; set; }
        public string? Email { get; set; }
        public string? Genero { get; set; }
        public string? Alerta { get; set; }
    }
}
