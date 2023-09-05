using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.DTOs
{
    public class CreatePacienteDTO
    {
        public string? Nome { get; set; }
        public DataDTO? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public EnderecoDTO? Endereco { get; set; }
        public string? Email { get; set; }
        public string? Genero { get; set; }
        public string? Alerta { get; set; }
    }
}
