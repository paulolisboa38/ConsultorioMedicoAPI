using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultorioMedicoAPI.Migrations
{
    /// <inheritdoc />
    public partial class adcionandoListaDeConsultasNasEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Medicos_MedicoId1",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Pacientes_PacienteId1",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_MedicoId1",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_PacienteId1",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "MedicoId1",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "PacienteId1",
                table: "Consultas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicoId1",
                table: "Consultas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId1",
                table: "Consultas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoId1",
                table: "Consultas",
                column: "MedicoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId1",
                table: "Consultas",
                column: "PacienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Medicos_MedicoId1",
                table: "Consultas",
                column: "MedicoId1",
                principalTable: "Medicos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Pacientes_PacienteId1",
                table: "Consultas",
                column: "PacienteId1",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }
    }
}
