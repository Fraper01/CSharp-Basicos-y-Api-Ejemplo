namespace WebApi_Clinica.Shared.Dtos
{
    public class MedicoDto
    {
        public int Id { get; set; }
        public int NumeroColegiado { get; set; }    
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? EspecialidadNombre { get; set; }
        public string? Descripcion { get; set; }
        public int EspecialidadId { get; set; }
    }
}
