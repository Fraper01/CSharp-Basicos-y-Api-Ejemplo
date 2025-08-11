using System.ComponentModel.DataAnnotations;

namespace MVC_TABLADEVALORES_CLINICA.Models.Entities
{
    public class Representante
    {
        [Key]
        public int Id_Representante { get; set; }
        [Required(ErrorMessage = "El atributo Nombres es obligatorio.")]
        [StringLength(50)]
        public string? Nombres { get; set; }
        [Required(ErrorMessage = "El atributo Dirección es obligatorio.")]
        [StringLength(500)]
        public string? Direccion { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^\d+$", ErrorMessage = "El Teléfono debe contener solo números enteros.")]
        public string? Telefono_Celular { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^\d+$", ErrorMessage = "El Teléfono debe contener solo números enteros.")]
        public string? Telefono_Fijo { get; set; }
        [StringLength(50)]
        public string? Persona_Contacto { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^\d+$", ErrorMessage = "El Teléfono debe contener solo números enteros.")]
        public string? Telefono_Contacto { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico debe ser válido.")]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Tipo_Identificacion { get; set; }
        [StringLength(50)]
        public string? Identificacion { get; set; }
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
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
