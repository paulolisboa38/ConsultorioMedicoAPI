using ConsultorioMedicoAPI.Models;

namespace ConsultorioMedicoAPI.DTOs
{
    public class MedicoDTO
    {
        public string Nome { get; set; }
        public DataDTO DataNascimento { get; set; }
        public string CRM { get; set; }
        public bool AtivoCRM { get; set; }
        public string Especialidade { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public string Genero { get; set; }
    }
}
