using Microsoft.AspNetCore.Mvc;
using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using MVC_TABLADEVALORES_CLINICA.Models.Exceptions;
using MVC_TABLADEVALORES_CLINICA.Services;
using WebApi_Clinica.Shared.Dtos;
using Microsoft.Extensions.Logging;


namespace MVC_TABLADEVALORES_CLINICA.Controllers
{
    public class EspecialidadesController : Controller
    {
        //HTTP: get, post, put, delete (RESTFULL)

        private readonly EspecialidadApiService especialidadApiService;
        private readonly ILogger<MedicoesController> logger; // Añade lista de Errores al log

        public EspecialidadesController(EspecialidadApiService _especialidadApiService, ILogger<MedicoesController> _logger)
        {
            this.especialidadApiService = _especialidadApiService;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(TablaDeContenido));
        }
        [HttpGet]
        public async Task<IActionResult> TablaDeContenido(string? especialidadFiltro = null, string? sortOrder = null)
        {
            try
            {
                const string NombreAscendente = "nombre_asc";
                const string NombreDescendente = "nombre_desc";

                ViewBag.NombreSortParm = string.IsNullOrEmpty(sortOrder) ? NombreAscendente : NombreDescendente;
                ViewBag.especialidadFiltro = especialidadFiltro;
                //ViewBag.NombreSortParm = string.IsNullOrEmpty(sortOrder) ? "nombre_asc" : "nombre_desc";
                //ViewBag.NombreSortParm = string.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
                //ViewBag.NombreSortParm = sortOrder; // Inicializamos ViewBag.NombreSortParm con el valor de sortOrder
                //ViewBag.especialidadFiltro = especialidadFiltro; 
                List<EspecialidadDto> especialidades = await especialidadApiService.ObtenerEspecialidades(especialidadFiltro);
                ViewBag.titulo = "TABLA DE CONTENIDO DE ESPECIALIDADES";

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    switch (sortOrder)
                    {
                        case NombreDescendente:
                            especialidades = especialidades.OrderByDescending(e => e.Nombre).ToList();
                            break;
                        case NombreAscendente: // Aunque la API ya ordena así, lo incluimos por consistencia
                            especialidades = especialidades.OrderBy(e => e.Nombre).ToList();
                            break;
                        default: // Ordenación ascendente por defecto o al hacer clic por primera vez pero esta validado para no ejecutarse
                            // ya que desde la ApiService viene ordenada
                            especialidades = especialidades.OrderBy(e => e.Nombre).ToList();
                            break;
                    }
                }

                return View(especialidades);
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is System.Net.Sockets.SocketException socketEx)
                {
                    if (socketEx.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionRefused)
                    {
                        TempData["MensajeError"] = "No se pudo conectar con la API de especialidades. Compruebe que la API esté en ejecución.";
                        logger.LogError(ex, "Error al obtener especialidades: Conexión rechazada."); // Log
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error de red al obtener la lista de especialidades.";
                        logger.LogError(ex, "Error al obtener especialidades: Error de red.");
                    }
                }
                else
                {
                    TempData["MensajeError"] = "Error de HTTP al obtener la lista de especialidades.";
                    logger.LogError(ex, "Error al obtener especialidades: Error de HTTP.");
                }
                return View(new List<EspecialidadDto>()); // Devuelve una lista vacía para no romper la página.
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de especialidades.";
                logger.LogError(ex, "Error inesperado al obtener especialidades.");
                return View(new List<EspecialidadDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEspecialidadPorId(int id)
        {
            EspecialidadDto? especialidad = await especialidadApiService.ObtenerEspecialidadPorId(id); // Llamada a la API
            // Medico? medico = await persistir_medico.ObtenerMedicoPorId(id);
            if (especialidad == null)
            {
                return BadRequest("La especialidad no existe.");
            }

            return View(especialidad);
        }

        [HttpGet]
        public async Task<IActionResult> AgregarEspecialidad()
        {
            try
            {
                // Obtener la lista de especialidades desde la API solo para que dispare la excepción si la webapi no está corriendo
                List<EspecialidadDto> especialidadesDto = await especialidadApiService.ObtenerEspecialidades();
                ViewBag.titulo = "AGREGAR UNA NUEVA ESPECIALIDAD";
                return View();
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is System.Net.Sockets.SocketException socketEx)
                {
                    if (socketEx.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionRefused)
                    {
                        TempData["MensajeError"] = "No se pudo conectar con la API de especialidades. Compruebe que la API esté en ejecución.";
                        logger.LogError(ex, "Error al obtener especialidades: Conexión rechazada."); // Log
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error de red al obtener la lista de especialidades.";
                        logger.LogError(ex, "Error al obtener especialidades: Error de red.");
                    }
                }
                else
                {
                    TempData["MensajeError"] = "Error de HTTP al obtener la lista de especialidades.";
                    logger.LogError(ex, "Error al obtener especialidades: Error de HTTP.");
                }
                return RedirectToAction(nameof(TablaDeContenido));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de especialidades.";
                logger.LogError(ex, "Error inesperado al obtener especialidades.");
                return RedirectToAction(nameof(TablaDeContenido));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEspecialidad(Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Llamada a la API
                    bool agregado = await especialidadApiService.AgregarEspecialidad(especialidad);
                    if (agregado)
                    {
                        TempData["MensajeExito"] = "Especialidad agregada correctamente.";
                        return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de especialidades
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error al agregar la Especialidad.";
                        ModelState.AddModelError(string.Empty, "Error al agregar la especialidad.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    TempData["MensajeError"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "Error al agregar la especialidad.");
                }
            }
            return View(especialidad); // Mostrar la vista con errores
        }

        [HttpGet]
        public async Task<IActionResult> EditarEspecialidad(int id)
        {
            ViewBag.titulo = "EDITAR ESPECIALIDAD";

            EspecialidadDto? especialidad = await especialidadApiService.ObtenerEspecialidadPorId(id); // Llamada a la API
            if (especialidad == null)
            {
                return BadRequest("La Especialidad no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var especialidadEditar = new Especialidad
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
            };
            return View(especialidadEditar);
        }

        [HttpPost]
        public async Task<IActionResult> EditarEspecialidad(Especialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool actualizado = await especialidadApiService.ActualizarEspecialidad(especialidad); // Llamada a la API
                    if (actualizado)
                    {
                        TempData["MensajeExito"] = "Especialidad modificada correctamente.";
                        return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de especialidades
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error al modificar la Especialidad.";
                        ModelState.AddModelError(string.Empty, "Error al modificar la Especialidad.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    //_logger.LogError(ex, "Error al agregar el especialidad.");
                    TempData["MensajeError"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "Error al agregar la Especialidad.");
                }
            }
            return View(especialidad); // Mostrar la vista con errores
        }

        [HttpGet]
        public async Task<IActionResult> VerEspecialidad(int id)
        {
            ViewBag.titulo = "CONSULTA ESPECIALIDADES";

            EspecialidadDto? especialidad = await especialidadApiService.ObtenerEspecialidadPorId(id); // Llamada a la API
            if (especialidad == null)
            {
                return BadRequest("La Especialidad no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var especialidadEditar = new EspecialidadDto
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
            };
            return View(especialidadEditar);
        }

        [HttpGet]
        public async Task<IActionResult> MostrarEliminarEspecialidad(int id)
        {
            ViewBag.titulo = "ELIMINAR ESPECIALIDAD";

            EspecialidadDto? especialidad = await especialidadApiService.ObtenerEspecialidadPorId(id); // Llamada a la API
            if (especialidad == null)
            {
                return BadRequest("La especialidad no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var especialidadEditar = new EspecialidadDto
            {
                Id = especialidad.Id,
                Nombre = especialidad.Nombre,
            };
            return View("EliminarEspecialidad", especialidadEditar);
        }

        [HttpPost] // Cambia a HttpPost
        public async Task<IActionResult> EliminarEspecialidad(int id)
        {
            bool eliminado = await especialidadApiService.EliminarEspecialidadPorId(id); // Llamada a la API
            if (!eliminado)
            {
                return BadRequest("No se pudo eliminar la especialidad.");
            }
            return RedirectToAction("TablaDeContenido");
        }
        [HttpGet]
        // Acción para mostrar la lista de médicos por especialidad
        public async Task<IActionResult> PorEspecialidad(int especialidadId)
        {
            try
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                List<MedicoDto> medicos = await especialidadApiService.PorEspecialidad(especialidadId);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                if (medicos != null)
                {
                    ViewBag.Especialidad = "Medico de Familia"; ViewBag.titulo = "Médicos por Especialidad";
                    return View(medicos);
                }
                else
                {
                    return BadRequest("No se encontraron médicos para esta especialidad.");
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is System.Net.Sockets.SocketException socketEx)
                {
                    if (socketEx.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionRefused)
                    {
                        TempData["MensajeError"] = "No se pudo conectar con la API de médicos. Compruebe que la API esté en ejecución.";
                        logger.LogError(ex, "Error al obtener médicos: Conexión rechazada."); // Log
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error de red al obtener la lista de médicos.";
                        logger.LogError(ex, "Error al obtener médicos: Error de red.");
                    }
                }
                else
                {
                    TempData["MensajeError"] = "Error de HTTP al obtener la lista de médicos.";
                    logger.LogError(ex, "Error al obtener médicos: Error de HTTP.");
                }
                return View(new List<MedicoDto>()); // Devuelve una lista vacía para no romper la página.
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de médicos.";
                logger.LogError(ex, "Error inesperado al obtener médicos.");
                return View(new List<MedicoDto>());
            }
        }
    }
}
