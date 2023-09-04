namespace ConsultorioMedicoAPI.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public bool AtivoCRM { get; set; }
        public string Especialidade { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
    }
}
