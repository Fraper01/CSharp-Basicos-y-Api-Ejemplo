using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Clinica.Shared.Dtos
{
    public class PacienteDto
    {
        public int Id_Paciente { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateOnly Fecha_Nacimiento { get; set; }
        public string? Diagnostico { get; set; }
        public DateTime Fecha_Ult_Evaluacion { get; set; }
        public string? Resultados { get; set; }
        public DateTime Fecha_Inic_Tratamiento { get; set; }
        public string? Nombre_Tratamiento { get; set; }
        public string? Cuantrimestre { get; set; }
        public string? Objectivos_Generales { get; set; }
        public string? Observaciones { get; set; }
        public byte[]? Foto { get; set; }
        public int Id_Representante { get; set; }
        public string? Nombre_Representante { get; set; }
        public string? Tipo_Identificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion_Postal { get; set; }
        public string? Localidad { get; set; }
        public string? Nacionalidad { get; set; }
        public string? Domicilio { get; set; }
        public string? Provincia { get; set; }
        public string? Certificado_Discapacidad { get; set; }
        public string? Grado_Dependencia { get; set; }
        public DateOnly? Fecha_Certificado { get; set; }
        public string? Derivado_Por { get; set; }
        public string? Estado { get; set; }
    }
}
