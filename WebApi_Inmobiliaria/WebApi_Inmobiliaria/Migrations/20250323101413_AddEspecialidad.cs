using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Clinica.Migrations
{
    /// <inheritdoc />
    public partial class AddEspecialidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicos05",
                table: "Medicos05");

            migrationBuilder.RenameTable(
                name: "Medicos05",
                newName: "Medicos05");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicos05",
                table: "Medicos05",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Especialidades05",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades05", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicos05_Especialidad",
                table: "Medicos05",
                column: "Especialidad");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos05_Especialidades05_Especialidad",
                table: "Medicos05",
                column: "Especialidad",
                principalTable: "Especialidades05",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos05_Especialidades05_Especialidad",
                table: "Medicos05");

            migrationBuilder.DropTable(
                name: "Especialidades05");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicos05",
                table: "Medicos05");

            migrationBuilder.DropIndex(
                name: "IX_Medicos05_Especialidad",
                table: "Medicos05");

            migrationBuilder.RenameTable(
                name: "Medicos05",
                newName: "Medicos05");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicos05",
                table: "Medicos05",
                column: "Id");
        }
    }
}
