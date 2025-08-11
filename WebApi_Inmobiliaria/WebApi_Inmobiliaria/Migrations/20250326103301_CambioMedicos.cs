using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Clinica.Migrations
{
    /// <inheritdoc />
    public partial class CambioMedicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos05_Especialidades05_Especialidad",
                table: "Medicos05");

            migrationBuilder.RenameColumn(
                name: "Numcol",
                table: "Medicos05",
                newName: "NumeroColegiado");

            migrationBuilder.RenameColumn(
                name: "Especialidad",
                table: "Medicos05",
                newName: "EspecialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_Medicos05_Especialidad",
                table: "Medicos05",
                newName: "IX_Medicos05_EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos05_Especialidades05",
                table: "Medicos05",
                column: "EspecialidadId",
                principalTable: "Especialidades05",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos05_Especialidades05",
                table: "Medicos05");

            migrationBuilder.RenameColumn(
                name: "NumeroColegiado",
                table: "Medicos05",
                newName: "Numcol");

            migrationBuilder.RenameColumn(
                name: "EspecialidadId",
                table: "Medicos05",
                newName: "Especialidad");

            migrationBuilder.RenameIndex(
                name: "IX_Medicos05_EspecialidadId",
                table: "Medicos05",
                newName: "IX_Medicos05_Especialidad");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos05_Especialidades05_Especialidad",
                table: "Medicos05",
                column: "Especialidad",
                principalTable: "Especialidades05",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
