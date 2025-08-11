using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_TABLADEVALORES_CLINICA.Models;
using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using MVC_TABLADEVALORES_CLINICA.Models.Exceptions;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi_Clinica.Shared.Dtos;

namespace MVC_TABLADEVALORES_CLINICA.Services
{
    public class MedicoApiService
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl = "http://localhost:5186/api/ClinicaMedico";
        private readonly string baseUrlEspecialidad = "http://localhost:5186/api/ClinicaEspeciald";
        public MedicoApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<MedicoDto>>? ObtenerMedicos(string nombreFiltro, string  apellidoFiltro)
        {
            try
            {
                var response = await httpClient.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                // Deserializa a la clase que representa la estructura completa
                var respuestaApi = JsonSerializer.Deserialize<RespuestaApiMedicos>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // Accede a la lista de médicos desde la propiedad "Medicos"
                List<MedicoDto> medicosEntities = respuestaApi?.Medicos ?? new List<MedicoDto>(); // Manejo de null
                if (medicosEntities == null)
                {
                    return new List<MedicoDto>(); // Retorna una lista vacía si no hay datos
                }
                List<MedicoDto> medicosDtos = medicosEntities.Select(medico => new MedicoDto
                {
                    Id = medico.Id,
                    NumeroColegiado = medico.NumeroColegiado,
                    Nombre = medico.Nombre,
                    Apellido = medico.Apellido,
                    EspecialidadNombre = medico.EspecialidadNombre,
                    Descripcion = medico.Descripcion,
                }).ToList();

                // Filtrar por nombre y apellido si se proporcionan filtros
                if (!string.IsNullOrEmpty(nombreFiltro) || !string.IsNullOrEmpty(apellidoFiltro))
                {
                    medicosDtos = medicosDtos.Where(m =>
                        (string.IsNullOrEmpty(nombreFiltro) || (m.Nombre != null && m.Nombre.Contains(nombreFiltro, StringComparison.OrdinalIgnoreCase))) &&
                        (string.IsNullOrEmpty(apellidoFiltro) || (m.Apellido != null && m.Apellido.Contains(apellidoFiltro, StringComparison.OrdinalIgnoreCase)))
                    ).ToList();
                }
                return medicosDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AgregarMedico(Medico medico)
        {
            // Verifica si la especialidad existe usando el endpoint Exists
            var responseEspecialidad = await httpClient.GetAsync($"{baseUrlEspecialidad}/Exists/{medico.EspecialidadId}");
            if (!responseEspecialidad.IsSuccessStatusCode)
            {
                // Si la especialidad no existe, lanza una excepción
                var errorContent = await responseEspecialidad.Content.ReadAsStringAsync();
                throw new EspecialidadNoEncontradaException($"La especialidad con ID {medico.EspecialidadId} NO EXISTE. Respuesta WebApi: {errorContent}");
            }
            try
            {
                var response = await httpClient.PostAsJsonAsync(baseUrl, medico);
                response.EnsureSuccessStatusCode(); // Lanza una excepción si hay un error
                return response.IsSuccessStatusCode; // Retorna un booleano
            }
            catch (HttpRequestException)
            {
                throw; // Vuelve a lanzar la excepción para que el controlador la maneje
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> ActualizarMedico(Medico medico)
        {
            // Verifica si la especialidad existe usando el endpoint Exists
            var responseEspecialidad = await httpClient.GetAsync($"{baseUrlEspecialidad}/Exists/{medico.EspecialidadId}");
            if (!responseEspecialidad.IsSuccessStatusCode)
            {
                // Si la especialidad no existe, lanza una excepción
                var errorContent = await responseEspecialidad.Content.ReadAsStringAsync();
                throw new EspecialidadNoEncontradaException($"La especialidad con ID {medico.EspecialidadId} NO EXISTE. Respuesta WebApi: {errorContent}");
            }
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{baseUrl}/{medico.Id}", medico);
                response.EnsureSuccessStatusCode(); // Lanza una excepción si hay un error
                return response.IsSuccessStatusCode; // Retorna un booleano
            }
            catch (HttpRequestException)
            {
                throw; // Vuelve a lanzar la excepción para que el controlador la maneje
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MedicoDto?> ObtenerMedicoPorId(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<MedicoDto>($"{baseUrl}/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> EliminarMedicoPorId(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Acción para mostrar la lista de médicos por especialidad
        public async Task<List<MedicoDto>>? PorEspecialidad(int especialidadId)
        {
            try
            {
                // Construye la URL de la API para obtener los médicos por especialidad.
                string url = $"{baseUrl}/Especialidad/{especialidadId}";
                // Llama a la API para obtener los datos.
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new MedicosNoEncontradaException($"No se encontraron Medicos para la especialidad con ID {especialidadId}.");
                }
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                // Deserializa a la clase que representa la estructura completa
                var respuestaApi = JsonSerializer.Deserialize<RespuestaApiMedicos>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // Accede a la lista de médicos desde la propiedad "Medicos"
                List<MedicoDto> medicosEntities = respuestaApi?.Medicos ?? new List<MedicoDto>(); // Manejo de null
                if (medicosEntities == null)
                {
                    return new List<MedicoDto>(); // Retorna una lista vacía si no hay datos
                }
                List<MedicoDto> medicosDtos = medicosEntities.Select(medico => new MedicoDto
                {
                    Id = medico.Id,
                    NumeroColegiado = medico.NumeroColegiado,
                    Nombre = medico.Nombre,
                    Apellido = medico.Apellido,
                    EspecialidadNombre = medico.EspecialidadNombre,
                    Descripcion = medico.Descripcion,
                }).ToList();
                // Ordena la lista de MedicoDto primero por nombre y luego por apellido
                medicosDtos = medicosDtos.OrderBy(medico => medico.Nombre).ThenBy(medico => medico.Apellido).ToList();

                return medicosDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    // Clase para representar la estructura completa del JSON
    public class RespuestaApiMedicos
    {
        [JsonPropertyName("$values")] // Esto mapea la propiedad "$values" del JSON
        public List<MedicoDto>? Medicos { get; set; }
    }
}







