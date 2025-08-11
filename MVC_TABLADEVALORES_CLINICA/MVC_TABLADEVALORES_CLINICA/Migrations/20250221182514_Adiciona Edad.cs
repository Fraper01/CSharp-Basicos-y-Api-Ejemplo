using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_TABLADEVALORES_CLINICA.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaEdad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Medicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Medicos");
        }
    }
}
