namespace ConsultorioMedicoAPI.DTOs
{
    public class ConsultaDTO
    {
        public DataDTO DataConsulta { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public string? Descricao { get; set; }
        public string? PrescricaoMedica { get; set; }
        public string? Diagnostico { get; set; }
    }
}
