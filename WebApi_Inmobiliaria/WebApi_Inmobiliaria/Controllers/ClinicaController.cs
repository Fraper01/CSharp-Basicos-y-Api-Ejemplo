using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Clinica.Models;
using WebApi_Clinica.Services;
using WebApi_Clinica.Shared.Dtos;


namespace WebApi_Clinica.Controllers
{
    [Route("api/ClinicaMedico")] // Ruta endpoints de la API Medicos
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        private readonly ClinicaService clinicaService;

        public ClinicaController(ClinicaService _clinicaService)
        {
            clinicaService = _clinicaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Medico medico)
        {
            var medicoCreado = await clinicaService.CreateAsync(medico);
            return Ok(medicoCreado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var medicoEliminado = await clinicaService.DeleteAsync(id);
                if (medicoEliminado == null)
                {
                    return NotFound("Médico no encontrado.");
                }
                return Ok(medicoEliminado);
            }
            catch (Exception)
            {
                //_logger.LogError(ex, $"Error al eliminar médico con ID: {id}");
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente.");
            }
        }

        // [HttpGet("/{id}")]
        [HttpGet("{id}")] // Cambia la ruta a {id}
        public async Task<MedicoDto?> ReadAsync(int id)
        {
            return await clinicaService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<MedicoDto>?> ReadAllAsync()
        {
            return await clinicaService.GetAllAsync();
        }

        [HttpPut("{id}")] // Cambia la ruta a {id}
        public async Task<Medico?> Update(int id, Medico Entity)
        {
            if (Entity.Id != id)
            {
                return Entity;
            }
            Medico? medicoActualizado = await clinicaService.UpdateAsync(Entity);
            return medicoActualizado;
        }

        [HttpGet("Especialidad/{especialidadId}")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicosByEspecialidad(int especialidadId)
        {
            var medicos = await clinicaService.GetMedicosByEspecialidadAsync(especialidadId);

            if (medicos == null || medicos.Count == 0)
            {
                return NotFound();
            }

            return Ok(medicos);
        }
    }
}

