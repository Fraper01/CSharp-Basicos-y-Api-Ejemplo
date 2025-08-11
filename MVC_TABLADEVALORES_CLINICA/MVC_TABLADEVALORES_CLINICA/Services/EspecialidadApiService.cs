using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi_Clinica.Shared.Dtos;

namespace MVC_TABLADEVALORES_CLINICA.Services
{
    public class EspecialidadApiService
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl = "http://localhost:5186/api/ClinicaEspeciald";

        public EspecialidadApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<EspecialidadDto>> ObtenerEspecialidades(string? especialidadFiltro = null)
        {
            try
            {
                var response = await httpClient.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                // Deserializa a la clase que representa la estructura completa
                var respuestaApi = JsonSerializer.Deserialize<RespuestaApiEspecialidades>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Accede a la lista de especialidades desde la propiedad "Especialidades"
                List<EspecialidadDto> especialidadesEntities = respuestaApi?.Especialidades ?? new List<EspecialidadDto>(); // Manejo de null

                if (especialidadesEntities == null)
                {
                    return new List<EspecialidadDto>(); // Retorna una lista vacía si no hay datos
                }

                List<EspecialidadDto> especialidadesDtos = especialidadesEntities.Select(especialidad => new EspecialidadDto
                {
                    Id = especialidad.Id,
                    Nombre = especialidad.Nombre,
                }).ToList();
                especialidadesDtos = especialidadesDtos.OrderBy(especialidad => especialidad.Nombre).ToList();

                // Filtrar por nombre y apellido si se proporcionan filtros
                if (!string.IsNullOrEmpty(especialidadFiltro))
                {
                    especialidadesDtos = especialidadesDtos.Where(m =>
                        (string.IsNullOrEmpty(especialidadFiltro) || (m.Nombre != null && m.Nombre.Contains(especialidadFiltro, StringComparison.OrdinalIgnoreCase))) 
                    ).ToList();
                }

                return especialidadesDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AgregarEspecialidad(Especialidad especialidad)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(baseUrl, especialidad);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ActualizarEspecialidad(Especialidad especialidad)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{baseUrl}/{especialidad.Id}", especialidad);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<EspecialidadDto?> ObtenerEspecialidadPorId(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<EspecialidadDto>($"{baseUrl}/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> EliminarEspecialidadPorId(int id)
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
                return medicosDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    // Clase para representar la estructura completa del JSON
    public class RespuestaApiEspecialidades
    {
        [JsonPropertyName("$values")] // Esto mapea la propiedad "$values" del JSON
        public List<EspecialidadDto>? Especialidades { get; set; }
    }
}
