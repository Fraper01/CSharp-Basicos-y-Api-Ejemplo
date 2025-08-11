using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApi_Clinica.Shared.Dtos;

namespace MVC_TABLADEVALORES_CLINICA.Services
{
    public class RepresentanteApiService
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl = "http://localhost:5186/api/ClinicaRepresent";

        public RepresentanteApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<RepresentanteDto>> ObtenerRepresentantes(string? nombresFiltro = null)
        {
            try
            {
                var response = await httpClient.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                // Deserializa a la clase que representa la estructura completa
                var respuestaApi = JsonSerializer.Deserialize<RespuestaApiRepresentantes>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                List<RepresentanteDto> representantesEntities = respuestaApi?.Representantes ?? new List<RepresentanteDto>(); 

                if (representantesEntities == null)
                {
                    return new List<RepresentanteDto>(); 
                }

                List<RepresentanteDto> representanteDtos = representantesEntities.Select(representante => new RepresentanteDto
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
                }).ToList();
                representanteDtos = representanteDtos.OrderBy(representante => representante.Nombres).ToList();

                // Filtrar por nombre y apellido si se proporcionan filtros
                if (!string.IsNullOrEmpty(nombresFiltro))
                {
                    representanteDtos = representanteDtos.Where(m =>
                        (string.IsNullOrEmpty(nombresFiltro) || (m.Nombres != null && m.Nombres.Contains(nombresFiltro, StringComparison.OrdinalIgnoreCase)))
                    ).ToList();
                }

                return representanteDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AgregarRepresentante(Representante representante)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(baseUrl, representante);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ActualizarRepresentante(Representante representante)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"{baseUrl}/{representante.Id_Representante}", representante);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<RepresentanteDto?> ObtenerRepresentantePorId(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<RepresentanteDto>($"{baseUrl}/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> EliminarRepresentantePorId(int id)
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
        public async Task<List<PacienteDto>>? PorRepresentante(int respresentanteId)
        {
            try
            {
                string url = $"{baseUrl}/Representante/{respresentanteId}";
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                // Deserializa a la clase que representa la estructura completa
                var respuestaApi = JsonSerializer.Deserialize<RespuestaApiPacientes>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                List<PacienteDto> pacientesEntities = respuestaApi?.Pacientes ?? new List<PacienteDto>(); 
                if (pacientesEntities == null)
                {
                    return new List<PacienteDto>(); 
                }
                List<PacienteDto> pacienteDtos = pacientesEntities.Select(paciente => new PacienteDto
                {
                    Id_Paciente = paciente.Id_Paciente,
                    Nombres = paciente.Nombres,
                    Apellidos = paciente.Apellidos,
                    Fecha_Nacimiento = paciente.Fecha_Nacimiento,
                    Diagnostico = paciente.Diagnostico,
                    Fecha_Ult_Evaluacion = paciente.Fecha_Ult_Evaluacion,
                    Resultados = paciente.Resultados,
                    Fecha_Inic_Tratamiento = paciente.Fecha_Inic_Tratamiento,
                    Nombre_Tratamiento = paciente.Nombre_Tratamiento,
                    Cuantrimestre = paciente.Cuantrimestre,
                    Objectivos_Generales = paciente.Objectivos_Generales,
                    Observaciones = paciente.Observaciones,
                    Foto = paciente.Foto,
                    Id_Representante = paciente.Id_Representante,
                    Nombre_Representante = paciente.Nombre_Representante,
                    Tipo_Identificacion = paciente.Tipo_Identificacion,
                    Identificacion = paciente.Identificacion,
                    Direccion_Postal = paciente.Direccion_Postal,
                    Localidad = paciente.Localidad,
                    Nacionalidad = paciente.Nacionalidad,
                    Domicilio = paciente.Domicilio,
                    Provincia = paciente.Provincia,
                    Certificado_Discapacidad = paciente.Certificado_Discapacidad,
                    Grado_Dependencia = paciente.Grado_Dependencia,
                    Fecha_Certificado = paciente.Fecha_Certificado,
                    Derivado_Por = paciente.Derivado_Por,
                    Estado = paciente.Estado,
                }).ToList();
                return pacientesEntities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class RespuestaApiRepresentantes
    {
        [JsonPropertyName("$values")] 
        public List<RepresentanteDto>? Representantes { get; set; }
    }
    public class RespuestaApiPacientes
    {
        [JsonPropertyName("$values")] 
        public List<PacienteDto>? Pacientes { get; set; }
    }

}
