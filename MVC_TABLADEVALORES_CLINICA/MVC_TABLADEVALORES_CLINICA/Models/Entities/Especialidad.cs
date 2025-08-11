using System.ComponentModel.DataAnnotations;

namespace MVC_TABLADEVALORES_CLINICA.Models.Entities
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El atributo Nombre de la Especialidad es obligatoria.")]
        [StringLength(100)]
        public string? Nombre { get; set; }

        // Propiedad de navegación para la relación 1:N
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
    }
}