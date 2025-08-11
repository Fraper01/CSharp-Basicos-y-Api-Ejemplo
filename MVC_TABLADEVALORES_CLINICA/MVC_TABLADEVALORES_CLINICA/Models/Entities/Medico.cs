using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_TABLADEVALORES_CLINICA.Models.Entities
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El atributo Numero del Colegio es obligatorio.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El Numero del Colegio debe contener solo números enteros.")]
        public int NumeroColegiado { get; set; }
        [Required(ErrorMessage = "El atributo Nombre es obligatorio.")]
        [StringLength(50)]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El atributo Apellido es obligatorio.")]
        [StringLength(50)]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "El atributo Especialidad es obligatorio.")]
        // Clave foránea para la relación 1:N
        [ForeignKey("Especialidad")]
        public int EspecialidadId { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "El atributo Descripcion es obligatorio.")]
        public string? Descripcion { get; set; }

        // Propiedad de navegación para la relación 1:N
        public Especialidad? Especialidad { get; set; }

    }
}
