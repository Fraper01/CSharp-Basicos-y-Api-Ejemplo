using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Clinica.Models;
using WebApi_Clinica.Services;
using WebApi_Clinica.Shared.Dtos;

namespace WebApi_Clinica.Controllers
{
    [Route("api/ClinicaPaciente")] // Ruta endpoints de la API Pacientes
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService pacienteService;

        public PacienteController(PacienteService _pacienteService)
        {
            pacienteService = _pacienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Paciente paciente)
        {
            var medicoCreado = await pacienteService.CreateAsync(paciente);
            return Ok(medicoCreado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var pacienteEliminado = await pacienteService.DeleteAsync(id);
                if (pacienteEliminado == null)
                {
                    return NotFound("Paciente no encontrado.");
                }
                return Ok(pacienteEliminado);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno. Intente nuevamente.");
            }
        }

        [HttpGet("{id}")] // Cambia la ruta a {id}
        public async Task<PacienteDto?> ReadAsync(int id)
        {
            return await pacienteService.GetAsync(id);
        }

        [HttpGet]
        public async Task<List<PacienteDto>?> ReadAllAsync()
        {
            return await pacienteService.GetAllAsync();
        }

        [HttpPut("{id}")] // Cambia la ruta a {id}
        public async Task<Paciente?> Update(int id, Paciente Entity)
        {
            if (Entity.Id_Paciente != id)
            {
                return Entity;
            }
            Paciente? pacienteActualizado = await pacienteService.UpdateAsync(Entity);
            return pacienteActualizado;
        }

        [HttpGet("Representante/{representId}")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesByRepresentante(int representId)
        {
            var pacientes = await pacienteService.GetPacienteByRepresentanteAsync(representId);

            if (pacientes == null || pacientes.Count == 0)
            {
                return NotFound();
            }

            return Ok(pacientes);
        }
    }
}
