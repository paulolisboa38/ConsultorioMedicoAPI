using ConsultorioMedicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedicoAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.MedicoId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId);
        }
    }
}
