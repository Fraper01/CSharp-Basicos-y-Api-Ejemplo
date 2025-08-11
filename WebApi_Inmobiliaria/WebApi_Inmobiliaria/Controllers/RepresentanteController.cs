using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Clinica.Models;
using WebApi_Clinica.Services;
using WebApi_Clinica.Shared.Dtos;

namespace WebApi_Clinica.Controllers
{
    [Route("api/ClinicaRepresent")] // Ruta endpoints de la API Representantes
    [ApiController]
    public class RepresentanteController : ControllerBase
    {
        private readonly RepresentanteService representanteService;

        public RepresentanteController(RepresentanteService _representanteService)
        {
            representanteService = _representanteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Representante representante)
        {
            var representCreada = await representanteService.CreateAsync(representante);
            return Ok(representCreada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var representEliminada = await representanteService.DeleteAsync(id);
                if (representEliminada == null)
                {
                    return NotFound("Especialidad no encontrado.");
                }
                return Ok(representEliminada);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente.");
            }
        }

        [HttpGet("{id}")] // Cambia la ruta a {id}
        public async Task<RepresentanteDto?> ReadAsync(int id)
        {
            return await representanteService.GetAsync(id);
        }

        [HttpGet("Exists/{id}")]
        public async Task<IActionResult> Exists(int id)
        {
            bool exists = await representanteService.ExistsAsync(id);
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
        public async Task<List<RepresentanteDto>?> ReadAllAsync()
        {
            return await representanteService.GetAllAsync();
        }

        [HttpPut("{id}")] // Cambia la ruta a {id}
        public async Task<Representante?> Update(int id, Representante Entity)
        {
            if (Entity.Id_Representante != id)
            {
                return Entity;
            }
            Representante? representActualizada = await representanteService.UpdateAsync(Entity);
            return representActualizada;
        }

        [HttpGet("Representante/{representId}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesByRepresentante(int representId)
        {
            var pacientes = await representanteService.GetPacientesByRepresentanteAsync(representId);

            if (pacientes == null || pacientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(pacientes);
        }
    }
}
