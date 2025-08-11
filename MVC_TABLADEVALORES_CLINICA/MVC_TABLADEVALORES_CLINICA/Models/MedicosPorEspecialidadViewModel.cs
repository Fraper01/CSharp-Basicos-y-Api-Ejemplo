using WebApi_Clinica.Shared.Dtos;

namespace MVC_TABLADEVALORES_CLINICA.Models
{
    public class MedicosPorEspecialidadViewModel
    {
        public int EspecialidadId { get; set; }
        public string? EspecialidadNombre { get; set; }
        public List<MedicoDto>? Medicos { get; set; }
    }
}
