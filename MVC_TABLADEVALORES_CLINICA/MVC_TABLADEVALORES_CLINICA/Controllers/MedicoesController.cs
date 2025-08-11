using Microsoft.AspNetCore.Mvc;
using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using MVC_TABLADEVALORES_CLINICA.Services;
using MVC_TABLADEVALORES_CLINICA.Models.Exceptions;
using WebApi_Clinica.Shared.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_TABLADEVALORES_CLINICA.Models;
using System.Net.Http;

namespace MVC_TABLADEVALORES_CLINICA.Controllers
{
    public class MedicoesController : Controller
    {
        //HTTP: get, post, put, delete (RESTFULL)

        private readonly MedicoApiService medicoApiService;
        private readonly EspecialidadApiService especialidadApiService; // Segundo servicio para obtener especialidades
        private readonly ILogger<MedicoesController> logger; // Añade lista de Errores al log

        public MedicoesController(MedicoApiService medicoApiClient, EspecialidadApiService especialidadApiService, ILogger<MedicoesController> _logger)
        {
            this.medicoApiService = medicoApiClient;
            this.especialidadApiService = especialidadApiService; // Inicializa el servicio de especialidades
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(TablaDeContenido));
        }
        [HttpGet]
        public async Task<IActionResult> TablaDeContenido(string nombreFiltro, string apellidoFiltro)
        {
            try
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                List<MedicoDto> medicos = await medicoApiService.ObtenerMedicos(nombreFiltro, apellidoFiltro);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                if (medicos == null)
                {
                    TempData["MensajeError"] = "No se encontraron médicos.";
                    return View(new List<MedicoDto>()); 
                }
                ViewBag.titulo = "TABLA DE CONTENIDO DE MEDICOS";
                return View(medicos);
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
        [HttpGet]
        public async Task<IActionResult> ObtenerMedicoPorId(int id)
        {
            MedicoDto? medico = await medicoApiService.ObtenerMedicoPorId(id); // Llamada a la API
            if (medico == null)
            {
                return BadRequest("El medico no existe.");
            }
            return View(medico);
        }

        [HttpGet]
        public async Task<IActionResult> AgregarMedico()
        {
            try
            {
                // Obtener la lista de especialidades desde la API
                List<EspecialidadDto> especialidadesDto = await especialidadApiService.ObtenerEspecialidades();

                // Mapear los DTOs a objetos SelectListItem para el DropDownList
                List<SelectListItem> especialidadesSelectList = especialidadesDto
                    .OrderBy(e => e.Nombre) // Ordenar por el nombre de la especialidad
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    }).ToList();

                ViewBag.Especialidades = especialidadesSelectList; ViewBag.titulo = "AGREGAR UN NUEVO MEDICO";

                return View();
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
                return RedirectToAction(nameof(TablaDeContenido));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de médicos.";
                logger.LogError(ex, "Error inesperado al obtener médicos.");
                return RedirectToAction(nameof(TablaDeContenido));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMedico(Medico medico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Llamada a la API
                    bool agregado = await medicoApiService.AgregarMedico(medico);
                    if (agregado)
                    {
                        TempData["MensajeExito"] = "Médico agregado correctamente.";
                        return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de médicos
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error al agregar el Mèdico.";
                        ModelState.AddModelError(string.Empty, "Error al agregar el médico.");
                    }
                }
                catch (EspecialidadNoEncontradaException ex)
                {
                    TempData["MensajeError"] = ex.Message; // Almacena el mensaje de la excepción en TempData
                    ModelState.AddModelError("Especialidad", ex.Message); // Agrega el error al ModelState para mostrarlo en la vista
                    logger.LogError(ex, "Error al agregar el médico. Especialidad no Encontrada");
                }
                catch (Exception ex)
                {
                    //Log the error
                    logger.LogError(ex, "Error al agregar el médico. " + ex.Message);
                    TempData["MensajeError"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "Error al agregar el médico. " + ex.Message);
                }
            }
            // Si llegamos aquí, algo salió mal, así que necesitamos repopular el DropDownList
            List<EspecialidadDto> especialidadesDto = await especialidadApiService.ObtenerEspecialidades();
            List<SelectListItem> especialidadesSelectList = especialidadesDto
                .OrderBy(e => e.Nombre) // Ordenar por el nombre de la especialidad
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                }).ToList();
            ViewBag.Especialidades = especialidadesSelectList;
            return View(medico); // Mostrar la vista con errores
        }

        [HttpGet]
        public async Task<IActionResult> EditarMedico(int id)
        {
            try
            {
                // 1. Localiza el Medico
                MedicoDto? medico = await medicoApiService.ObtenerMedicoPorId(id); // Llamada a la API
                if (medico == null)
                {
                    return BadRequest("El medico no existe");
                }
                // 2. Mapear el DTO a la Entidad
                var medicoEditar = new Medico
                {
                    Id = medico.Id,
                    NumeroColegiado = medico.NumeroColegiado,
                    Nombre = medico.Nombre,
                    Apellido = medico.Apellido,
                    EspecialidadId = medico.EspecialidadId,
                    Descripcion = medico.Descripcion,
                };
                // 3. Obtener la lista de especialidades desde la API
                List<EspecialidadDto> especialidadesDto = await especialidadApiService.ObtenerEspecialidades();
                List<SelectListItem> especialidadesSelectList = especialidadesDto
                    .OrderBy(e => e.Nombre) // Ordenar por el nombre de la especialidad
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nombre
                    }).ToList();
                ViewBag.Especialidades = especialidadesSelectList;
                ViewBag.titulo = "EDITAR MEDICO";
                return View(medicoEditar);
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
                return RedirectToAction(nameof(TablaDeContenido));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de médicos.";
                logger.LogError(ex, "Error inesperado al obtener médicos.");
                return RedirectToAction(nameof(TablaDeContenido));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditarMedico(Medico medico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool actualizado = await medicoApiService.ActualizarMedico(medico); // Llamada a la API
                    if (actualizado)
                    {
                        TempData["MensajeExito"] = "Médico modificado correctamente.";
                        return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de médicos
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error al modificar el Médico.";
                        ModelState.AddModelError(string.Empty, "Error al modificar el médico.");
                    }
                }
                catch (EspecialidadNoEncontradaException ex)
                {
                    TempData["MensajeError"] = ex.Message; // Almacena el mensaje de la excepción en TempData
                    ModelState.AddModelError("Especialidad", ex.Message); // Agrega el error al ModelState para mostrarlo en la vista
                }
                catch (Exception ex)
                {
                    // Log the error
                    //_logger.LogError(ex, "Error al agregar el médico.");
                    TempData["MensajeError"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "Error al agregar el médico.");
                }
            }
            // Si llegamos aquí, algo salió mal, así que necesitamos repopular el DropDownList
            List<EspecialidadDto> especialidadesDto = await especialidadApiService.ObtenerEspecialidades();
            List<SelectListItem> especialidadesSelectList = especialidadesDto
                .OrderBy(e => e.Nombre) // Ordenar por el nombre de la especialidad
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                }).ToList();
            ViewBag.Especialidades = especialidadesSelectList; return View(medico); // Mostrar la vista con errores
        }

        [HttpGet]
        public async Task<IActionResult> VerMedico(int id)
        {
            ViewBag.titulo = "CONSULTA MEDICO";

            MedicoDto? medico = await medicoApiService.ObtenerMedicoPorId(id); // Llamada a la API
            if (medico == null)
            {
                return BadRequest("El medico no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var medicoEditar = new MedicoDto
            {
                Id = medico.Id,
                NumeroColegiado = medico.NumeroColegiado,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                EspecialidadId = medico.EspecialidadId,
                Descripcion = medico.Descripcion,
                EspecialidadNombre = medico.EspecialidadNombre,
            };
            return View(medicoEditar);
        }

        [HttpGet]
        public async Task<IActionResult> MostrarEliminarMedico(int id)
        {
            ViewBag.titulo = "ELIMINAR MEDICO";

            MedicoDto? medico = await medicoApiService.ObtenerMedicoPorId(id); // Llamada a la API
            if (medico == null)
            {
                return BadRequest("El medico no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var medicoEditar = new MedicoDto
            {
                Id = medico.Id,
                NumeroColegiado = medico.NumeroColegiado,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                EspecialidadId = medico.EspecialidadId,
                Descripcion = medico.Descripcion,
                EspecialidadNombre = medico.EspecialidadNombre,
            };
            return View("EliminarMedico", medicoEditar);
        }

        [HttpPost] // Cambia a HttpPost
        public async Task<IActionResult> EliminarMedico(int id)
        {
            bool eliminado = await medicoApiService.EliminarMedicoPorId(id); // Llamada a la API
            if (!eliminado)
            {
                TempData["MensajeError"] = "Error al eliminar el Médico.";
                ModelState.AddModelError(string.Empty, "Error al eliminar el médico.");
                return BadRequest("No se pudo eliminar el médico.");
            }
            else
            {
                TempData["MensajeExito"] = "Médico eliminado correctamente.";
                return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de médicos
            }
        }

        [HttpGet]
        // Acción para mostrar la lista de médicos por especialidad
        public async Task<IActionResult> PorEspecialidad(int especialidadId)
        {
            try
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                List<MedicoDto> medicos = await medicoApiService.PorEspecialidad(especialidadId);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                if (medicos == null || medicos.Count == 0)
                {
                    TempData["MensajeError"] = "No se encontraron médicos para la especialidad seleccionada.";
                    return RedirectToAction(nameof(SeleccionarEspecialidad)); // Redirigir a la vista de selección
                }
                // Recuperar el nombre de la especialidad
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                string especialidadNombre = medicos.FirstOrDefault()?.EspecialidadNombre;
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                if (string.IsNullOrEmpty(especialidadNombre))
                {
                    TempData["MensajeError"] = "No se encontró el nombre de la especialidad.";
                    return RedirectToAction(nameof(SeleccionarEspecialidad)); // Redirigir a la vista de selección
                }

                // Pasar el nombre de la especialidad a la vista
                ViewBag.EspecialidadNombre = especialidadNombre;
                ViewBag.titulo = "Médicos por Especialidad";
                return View(medicos);
            }
            catch (MedicosNoEncontradaException ex)
            {
                TempData["MensajeError"] = ex.Message;
                logger.LogWarning(ex, "Medicos por Especialidad no encontrado.");
                return View(new List<MedicoDto>()); // Devuelve una lista vacía o una vista de error.
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
        // Acción para MOSTRAR la página con el dropdown de selección
        [HttpGet]
        public async Task<IActionResult> SeleccionarEspecialidad()
        {
            // 1. Obtener la lista de especialidades (ID y Nombre)
            List<EspecialidadDto> especialidadesDto = await especialidadApiService.ObtenerEspecialidades();
            // 2. Crear una lista de SelectListItem para el dropdown
            var especialidadesSelectList = new List<SelectListItem>();
            especialidadesSelectList.Add(new SelectListItem { Value = "", Text = "-- Seleccione una Especialidad --", Disabled = true, Selected = true }); // Opción por defecto

            foreach (var esp in especialidadesDto)
            {
                especialidadesSelectList.Add(new SelectListItem { Value = esp.Id.ToString(), Text = esp.Nombre });
            }

            // 3. Pasar la lista a la vista (usando ViewBag o un ViewModel)
            ViewBag.Especialidades = especialidadesSelectList;

            return View(); // Devuelve la vista SeleccionarEspecialidad.cshtml
        }
    }
}


