using System.ComponentModel.DataAnnotations;

namespace WebApi_Clinica.Models
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nombre { get; set; }

        // Propiedad de navegación para la relación 1:N
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
    }
}