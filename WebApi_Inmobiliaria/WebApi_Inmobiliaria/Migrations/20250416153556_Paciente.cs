using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Clinica.Migrations
{
    /// <inheritdoc />
    public partial class Paciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Representantes",
                columns: table => new
                {
                    Id_Representante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Telefono_Celular = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono_Fijo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Persona_Contacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefono_Contacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tipo_Identificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Identificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario_Crea = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Equipo_Crea = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fecha_Modifica = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario_Modifica = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Equipo_Modifica = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representantes", x => x.Id_Representante);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id_Paciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Fecha_Nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fecha_Ult_Evaluacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resultados = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fecha_Inic_Tratamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre_Tratamiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cuantrimestre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objectivos_Generales = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Id_Representante = table.Column<int>(type: "int", nullable: false),
                    Tipo_Identificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Identificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Direccion_Postal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nacionalidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Certificado_Discapacidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Grado_Dependencia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Fecha_Certificado = table.Column<DateOnly>(type: "date", nullable: true),
                    Derivado_Por = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario_Crea = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Equipo_Crea = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fecha_Modifica = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario_Modifica = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Equipo_Modifica = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id_Paciente);
                    table.ForeignKey(
                        name: "FK_Paciente_Representante",
                        column: x => x.Id_Representante,
                        principalTable: "Representantes",
                        principalColumn: "Id_Representante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Id_Representante",
                table: "Pacientes",
                column: "Id_Representante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Representantes");
        }
    }
}
