using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_TABLADEVALORES_CLINICA.Models.Entities
{
    public class Paciente
    {
        [Key]
        public int Id_Paciente { get; set; }
        [Required(ErrorMessage = "El atributo Nombres es obligatorio.")]
        [StringLength(150)]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "El atributo Apellidos es obligatorio.")]
        [StringLength(150)]
        public string? Apellidos { get; set; }
        [Required(ErrorMessage = "El atributo Fecha de Nacimiento es obligatorio.")]
        public DateOnly Fecha_Nacimiento { get; set; }
        [StringLength(500)]
        public string? Diagnostico { get; set; }
        public DateTime Fecha_Ult_Evaluacion { get; set; }
        [StringLength(500)]
        public string? Resultados { get; set; }
        public DateTime Fecha_Inic_Tratamiento { get; set; }
        [StringLength(50)]
        public string? Nombre_Tratamiento { get; set; }
        public string? Cuantrimestre { get; set; }
        [StringLength(250)]
        public string? Objectivos_Generales { get; set; }
        [StringLength(250)]
        public string? Observaciones { get; set; }   
        public byte[]? Foto { get; set; }
        [Required(ErrorMessage = "El atributo Representante es obligatorio.")]
        // Clave foránea para la relación 1:N
        [ForeignKey("Representante")]
        public int Id_Representante { get; set; }
        [StringLength(20)]
        public string? Tipo_Identificacion { get; set; }
        [StringLength(20)]
        public string? Identificacion { get; set; }
        [StringLength(20)]
        public string? Direccion_Postal  { get; set; }
        [StringLength(20)]
        public string? Localidad { get; set; }
        [StringLength(20)]
        public string? Nacionalidad { get; set; }
        [StringLength(20)]
        public string? Domicilio { get; set; }
        [StringLength(20)]
        public string? Provincia { get; set; }
        [StringLength(20)]
        public string? Certificado_Discapacidad { get; set; }
        [StringLength(20)]
        public string? Grado_Dependencia { get; set; }
        public DateOnly? Fecha_Certificado { get; set; }
        [StringLength(20)]
        public string? Derivado_Por { get; set; }
        [Required]
        public DateTime Fecha_Registro { get; set; }
        [Required]
        [StringLength(20)]
        public string? Usuario_Crea { get; set; }
        [Required]
        [StringLength(20)]
        public string? Equipo_Crea { get; set; }
        public DateTime Fecha_Modifica { get; set; }
        [StringLength(20)]
        public string? Usuario_Modifica { get; set; }
        [StringLength(20)]
        public string? Equipo_Modifica { get; set; }
        [StringLength(10)]
        public string? Estado { get; set; }

        // Propiedad de navegación para la relación 1:N
        public Representante? Representante { get; set; }

    }
}
