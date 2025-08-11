using System.ComponentModel.DataAnnotations;

namespace MVC_TABLADEVALORES_CLINICA.Models.ViewModels
{
    public class RepresentanteEdicionViewModel
    {
        public int Id_Representante { get; set; }
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
    }
}
