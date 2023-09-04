using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsultorioMedicoAPI.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }
        public string? Descricao { get; set; }
        public string? PrescricaoMedica { get; set; }
        public string? Diagnostico { get; set; }
        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
    }
}
