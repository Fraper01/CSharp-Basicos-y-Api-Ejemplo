using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Clinica.Models;
using WebApi_Clinica.Services;
using WebApi_Clinica.Shared.Dtos;   

namespace WebApi_Clinica.Controllers
{
    [Route("api/ClinicaEspeciald")] // Ruta endpoints de la API Especialidades
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly EspecialidadService especialidadService;

        public EspecialidadController(EspecialidadService _especialidadService)
        {
            especialidadService = _especialidadService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Especialidad especialidad)
        {
            var especialiCreada = await especialidadService.CreateAsync(especialidad);
            return Ok(especialiCreada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var especialiEliminada = await especialidadService.DeleteAsync(id);
                if (especialiEliminada == null)
                {
                    return NotFound("Especialidad no encontrado.");
                }
                return Ok(especialiEliminada);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente.");
            }
        }

        // [HttpGet("/{id}")]
        [HttpGet("{id}")] // Cambia la ruta a {id}
        public async Task<EspecialidadDto?> ReadAsync(int id)
        {
            return await especialidadService.GetAsync(id);
        }
 
        [HttpGet("Exists/{id}")]
        public async Task<IActionResult> Exists(int id)
        {
            bool exists = await especialidadService.ExistsAsync(id);
            if (exists)
            {
                return Ok(true); // O 204 No Content si prefieres un cuerpo vacío
            }
            else
            {
                return NotFound(false); // Devuelve 404 si no existe
            }
        }

        [HttpGet]
        public async Task<List<EspecialidadDto>?> ReadAllAsync()
        {
            return await especialidadService.GetAllAsync();
        }

        [HttpPut("{id}")] // Cambia la ruta a {id}
        public async Task<Especialidad?> Update(int id, Especialidad Entity)
        {
            if (Entity.Id != id)
            {
                return Entity;
            }
            Especialidad? especialiActualizada = await especialidadService.UpdateAsync(Entity);
            return especialiActualizada;
        }

        [HttpGet("Especialidad/{especialidadId}")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicosByEspecialidad(int especialidadId)
        {
            var medicos = await especialidadService.GetMedicosByEspecialidadAsync(especialidadId);

            if (medicos == null || medicos.Count == 0)
            {
                return NotFound();
            }

            return Ok(medicos);
        }
    }
}

