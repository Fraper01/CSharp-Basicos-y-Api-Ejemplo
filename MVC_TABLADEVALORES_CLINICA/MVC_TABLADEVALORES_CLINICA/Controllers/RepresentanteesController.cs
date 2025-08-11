using Microsoft.AspNetCore.Mvc;
using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using MVC_TABLADEVALORES_CLINICA.Models.ViewModels;
using MVC_TABLADEVALORES_CLINICA.Services;
using WebApi_Clinica.Shared.Dtos;

namespace MVC_TABLADEVALORES_CLINICA.Controllers
{
    public class RepresentanteesController : Controller
    {
        //HTTP: get, post, put, delete (RESTFULL)

        private readonly RepresentanteApiService representanteApiService;
        private readonly ILogger<MedicoesController> logger; // Añade lista de Errores al log

        public RepresentanteesController(RepresentanteApiService _representanteApiService, ILogger<MedicoesController> _logger)
        {
            this.representanteApiService = _representanteApiService;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(TablaDeContenido));
        }
        [HttpGet]
        public async Task<IActionResult> TablaDeContenido(string nombresFiltro)
        {
            try
            {
                List<RepresentanteDto> representantes = await representanteApiService.ObtenerRepresentantes(nombresFiltro);
                ViewBag.titulo = "TABLA DE CONTENIDO DE REPRESENTANTES";
                return View(representantes);
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is System.Net.Sockets.SocketException socketEx)
                {
                    if (socketEx.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionRefused)
                    {
                        TempData["MensajeError"] = "No se pudo conectar con la API de representantes. Compruebe que la API esté en ejecución.";
                        logger.LogError(ex, "Error al obtener representantes: Conexión rechazada."); // Log
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error de red al obtener la lista de representantes.";
                        logger.LogError(ex, "Error al obtener representantes: Error de red.");
                    }
                }
                else
                {
                    TempData["MensajeError"] = "Error de HTTP al obtener la lista de representantes.";
                    logger.LogError(ex, "Error al obtener especialidades: Error de HTTP.");
                }
                return View(new List<RepresentanteDto>()); // Devuelve una lista vacía para no romper la página.
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de representantes.";
                logger.LogError(ex, "Error inesperado al obtener representantes.");
                return View(new List<RepresentanteDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRepresentantePorId(int id)
        {
            RepresentanteDto? representante = await representanteApiService.ObtenerRepresentantePorId(id); // Llamada a la API
            if (representante == null)
            {
                return BadRequest("El representante no existe.");
            }

            return View(representante);
        }

        [HttpGet]
        public async Task<IActionResult> AgregarRepresentante()
        {
            try
            {
                // Obtener la lista de especialidades desde la API solo para que dispare la excepción si la webapi no está corriendo
                List<RepresentanteDto> representantesDto = await representanteApiService.ObtenerRepresentantes();
                ViewBag.titulo = "AGREGAR UN NUEVO REPRESENTANTE";
                return View();
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is System.Net.Sockets.SocketException socketEx)
                {
                    if (socketEx.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionRefused)
                    {
                        TempData["MensajeError"] = "No se pudo conectar con la API de representantes. Compruebe que la API esté en ejecución.";
                        logger.LogError(ex, "Error al obtener representantes: Conexión rechazada."); // Log
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error de red al obtener la lista de representantes.";
                        logger.LogError(ex, "Error al obtener representantes: Error de red.");
                    }
                }
                else
                {
                    TempData["MensajeError"] = "Error de HTTP al obtener la lista de representantes.";
                    logger.LogError(ex, "Error al obtener representantes: Error de HTTP.");
                }
                return RedirectToAction(nameof(TablaDeContenido));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al obtener la lista de representantes.";
                logger.LogError(ex, "Error inesperado al obtener representantes.");
                return RedirectToAction(nameof(TablaDeContenido));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarRepresentante(RepresentanteCreacionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Crea la instancia de tu entidad 'Representante'
                    var representante = new Representante
                    {
                        Nombres = viewModel.Nombres,
                        Direccion = viewModel.Direccion,
                        Telefono_Celular = viewModel.Telefono_Celular,
                        Telefono_Fijo = viewModel.Telefono_Fijo,
                        Persona_Contacto = viewModel.Persona_Contacto,
                        Telefono_Contacto = viewModel.Telefono_Contacto,
                        Email = viewModel.Email,
                        // Asigna los valores por defecto para los atributos que no están en el ViewModel
                        Fecha_Registro = DateTime.Now,
                        Usuario_Crea = User?.Identity?.Name ?? "System",
                        Equipo_Crea = Environment.MachineName,
                        Estado = "Activo"
                    };                    // Llamada a la API
                    bool agregado = await representanteApiService.AgregarRepresentante(representante);
                    if (agregado)
                    {
                        TempData["MensajeExito"] = "Representante agregada correctamente.";
                        return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de especialidades
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error al agregar el Representante.";
                        ModelState.AddModelError(string.Empty, "Error al agregar el Representante.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    TempData["MensajeError"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "Error al agregar el Representante.");
                }
            }
            return View(viewModel); // Mostrar la vista con errores
        }

        [HttpGet]
        public async Task<IActionResult> EditarRepresentante(int id)
        {
            ViewBag.titulo = "EDITAR REPRESENTANTE";

            RepresentanteDto? representante = await representanteApiService.ObtenerRepresentantePorId(id); // Llamada a la API
            if (representante == null)
            {
                return BadRequest("La Especialidad no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var viewModel = new RepresentanteEdicionViewModel
            {
                Id_Representante = representante.Id_Representante,
                Nombres = representante.Nombres,
                Direccion = representante.Direccion,
                Telefono_Celular = representante.Telefono_Celular,
                Telefono_Fijo = representante.Telefono_Fijo,
                Persona_Contacto = representante.Persona_Contacto,
                Telefono_Contacto = representante.Telefono_Contacto,
                Email = representante.Email,
                Tipo_Identificacion = representante.Tipo_Identificacion,
                Identificacion = representante.Identificacion
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarRepresentante(RepresentanteEdicionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // 1. Obtener la entidad original de la base de datos
                RepresentanteDto? representanteOriginal = await representanteApiService.ObtenerRepresentantePorId(viewModel.Id_Representante);

                if (representanteOriginal == null)
                {
                    return NotFound("Error al recuperar el representante original.");
                }

                // 2. Mapear los valores editables del ViewModel a una nueva entidad
                var representanteActualizado = new Representante
                {
                    Id_Representante = viewModel.Id_Representante,
                    Nombres = viewModel.Nombres,
                    Direccion = viewModel.Direccion,
                    Telefono_Celular = viewModel.Telefono_Celular,
                    Telefono_Fijo = viewModel.Telefono_Fijo,
                    Persona_Contacto = viewModel.Persona_Contacto,
                    Telefono_Contacto = viewModel.Telefono_Contacto,
                    Email = viewModel.Email,
                    Tipo_Identificacion = viewModel.Tipo_Identificacion,
                    Identificacion = viewModel.Identificacion,
                    // 3. Preservar los valores de auditoría de la entidad original
                    Fecha_Registro = representanteOriginal.Fecha_Registro,
                    Usuario_Crea = representanteOriginal.Usuario_Crea,
                    Equipo_Crea = representanteOriginal.Equipo_Crea,
                    //Estado = representanteOriginal.Estado,
                    // 4. Establecer los valores de modificación
                    Fecha_Modifica = DateTime.Now,
                    Usuario_Modifica = User?.Identity?.Name ?? "System",
                    Equipo_Modifica = Environment.MachineName,
                    Estado = "Modificado"
                };
                try
                {
                    bool actualizado = await representanteApiService.ActualizarRepresentante(representanteActualizado); // Llamada a la API
                    if (actualizado)
                    {
                        TempData["MensajeExito"] = "Representante modificado correctamente.";
                        return RedirectToAction(nameof(TablaDeContenido)); // Redirigir a la lista de representantes
                    }
                    else
                    {
                        TempData["MensajeError"] = "Error al modificar el Representante.";
                        ModelState.AddModelError(string.Empty, "Error al modificar la Representante.");
                    }
                }
                catch (Exception ex)
                {
                    TempData["MensajeError"] = ex.Message;
                    ModelState.AddModelError(string.Empty, "Error al agregar el Representante.");
                }
            }
            return View(viewModel); // Mostrar la vista con errores
        }

        [HttpGet]
        public async Task<IActionResult> VerRepresentante(int id)
        {
            ViewBag.titulo = "CONSULTA REPRESENTANTES";

            RepresentanteDto? representante = await representanteApiService.ObtenerRepresentantePorId(id); // Llamada a la API
            if (representante == null)
            {
                return BadRequest("La representante no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var representanteEditar = new RepresentanteDto
            {
                Id_Representante = representante.Id_Representante,
                Nombres = representante.Nombres,
                Direccion = representante.Direccion,
                Telefono_Celular = representante.Telefono_Celular,
                Telefono_Fijo = representante.Telefono_Fijo,
                Persona_Contacto = representante.Persona_Contacto,
                Telefono_Contacto = representante.Telefono_Contacto,
                Email = representante.Email,
                Tipo_Identificacion = representante.Tipo_Identificacion,
                Identificacion = representante.Identificacion,
                Fecha_Registro = representante.Fecha_Registro,
                Usuario_Crea = representante.Usuario_Crea,
                Equipo_Crea = representante.Equipo_Crea,
                Fecha_Modifica = representante.Fecha_Modifica,
                Usuario_Modifica = representante.Usuario_Modifica,
                Equipo_Modifica = representante.Equipo_Modifica,
                Estado = representante.Estado
            };
            return View(representanteEditar);
        }

        [HttpGet]
        public async Task<IActionResult> MostrarEliminarRepresentante(int id)
        {
            ViewBag.titulo = "ELIMINAR REPRESENTANTE";

            RepresentanteDto? representante = await representanteApiService.ObtenerRepresentantePorId(id); // Llamada a la API
            if (representante == null)
            {
                return BadRequest("el representante no existe");
            }
            // 2. Mapear el DTO a la Entidad
            var representanteEditar = new RepresentanteDto
            {
                Id_Representante = representante.Id_Representante,
                Nombres = representante.Nombres,
                Direccion = representante.Direccion,
                Telefono_Celular = representante.Telefono_Celular,
                Telefono_Fijo = representante.Telefono_Fijo,
                Persona_Contacto = representante.Persona_Contacto,
                Telefono_Contacto = representante.Telefono_Contacto,
                Email = representante.Email,
                Tipo_Identificacion = representante.Tipo_Identificacion,
                Identificacion = representante.Identificacion,
                Fecha_Registro = representante.Fecha_Registro,
                Usuario_Crea = representante.Usuario_Crea,
                Equipo_Crea = representante.Equipo_Crea,
                Fecha_Modifica = representante.Fecha_Modifica,
                Usuario_Modifica = representante.Usuario_Modifica,
                Equipo_Modifica = representante.Equipo_Modifica,
                Estado = representante.Estado
            };
            return View("EliminarRepresentante", representanteEditar);
        }

        [HttpPost] // Cambia a HttpPost
        public async Task<IActionResult> EliminarRepresentante(int id)
        {
            bool eliminado = await representanteApiService.EliminarRepresentantePorId(id); // Llamada a la API
            if (!eliminado)
            {
                return BadRequest("No se pudo eliminar el Representante.");
            }
            return RedirectToAction("TablaDeContenido");
        }
        /*[HttpGet]
        // Acción para mostrar la lista de médicos por especialidad
        /*public async Task<IActionResult> PorEspecialidad(int especialidadId)
        {
            try
            {
                List<MedicoDto> medicos = await especialidadApiService.PorEspecialidad(especialidadId);
                ViewBag.Especialidad = "Medico de Familia"; ViewBag.titulo = "Médicos por Especialidad";
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
        }*/
    }
    
}
