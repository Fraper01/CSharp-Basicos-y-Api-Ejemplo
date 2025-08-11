using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Clinica.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumeroColegiado { get; set; }
        [Required]
        [StringLength(50)]
        public string? Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string? Apellido { get; set; }
        [Required]
        // Clave foránea para la relación 1:N
        [ForeignKey("Especialidad")] 
        public int EspecialidadId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Descripcion { get; set; }

        // Propiedad de navegación para la relación 1:N
        public Especialidad? Especialidad { get; set; }
    }
}
