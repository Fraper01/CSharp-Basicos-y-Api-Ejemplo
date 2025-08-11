using WebApi_Clinica.Models;
using WebApi_Clinica.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi_Clinica.Shared.Dtos;

namespace WebApi_Clinica.Services
{
    public class PacienteService
    {
        private readonly ClinicaContext context;
        public PacienteService(ClinicaContext _context)
        {
            context = _context;
        }

        public async Task<Paciente?> AddAsync(Paciente Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Pacientes.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Paciente?> CreateAsync(Paciente Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Pacientes.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Paciente?> DeleteAsync(object id)
        {
            var pacienteBuscado = await context.Pacientes.FindAsync(id);

            if (pacienteBuscado == null)
            {
                return null;
            }

            context.Pacientes.Remove(pacienteBuscado);
            await context.SaveChangesAsync();
            return pacienteBuscado;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Pacientes.AnyAsync(m => m.Id_Paciente == id);
        }

        public async Task<List<PacienteDto>?> GetAllAsync()
        {
            if (context == null)
            {
                return null; 
            }
            try
            {
                return await context.Pacientes
                    .Select(m => new PacienteDto
                    {
                        Id_Paciente = m.Id_Paciente,
                        Nombres = m.Nombres,
                        Apellidos = m.Apellidos,
                        Fecha_Nacimiento = m.Fecha_Nacimiento,
                        Diagnostico = m.Diagnostico,
                        Fecha_Ult_Evaluacion = m.Fecha_Ult_Evaluacion,
                        Resultados = m.Resultados,
                        Fecha_Inic_Tratamiento = m.Fecha_Inic_Tratamiento,
                        Nombre_Tratamiento = m.Nombre_Tratamiento,
                        Cuantrimestre = m.Cuantrimestre,
                        Objectivos_Generales = m.Objectivos_Generales,
                        Observaciones = m.Observaciones,
                        Foto = m.Foto,
                        Id_Representante = m.Id_Representante,
                        Nombre_Representante = m.Representante != null ? m.Representante.Nombres : "Sin Representante", 
                        Tipo_Identificacion = m.Tipo_Identificacion,
                        Identificacion = m.Identificacion,
                        Direccion_Postal = m.Direccion_Postal,
                        Localidad = m.Localidad,
                        Nacionalidad = m.Nacionalidad,
                        Domicilio = m.Domicilio,
                        Provincia = m.Provincia,
                        Certificado_Discapacidad = m.Certificado_Discapacidad,
                        Grado_Dependencia = m.Grado_Dependencia,
                        Fecha_Certificado = m.Fecha_Certificado,
                        Derivado_Por = m.Derivado_Por,
                        Estado = m.Estado
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PacienteDto?> GetAsync(object id)
        {
            if (context == null)
            {
                return null; 
            }
            try
            {
                var idInt = Convert.ToInt32(id);
                var pacienteBuscado = await context.Pacientes.Select(m => new PacienteDto
                {
                    Id_Paciente = m.Id_Paciente,
                    Nombres = m.Nombres,
                    Apellidos = m.Apellidos,
                    Fecha_Nacimiento = m.Fecha_Nacimiento,
                    Diagnostico = m.Diagnostico,
                    Fecha_Ult_Evaluacion = m.Fecha_Ult_Evaluacion,
                    Resultados = m.Resultados,
                    Fecha_Inic_Tratamiento = m.Fecha_Inic_Tratamiento,
                    Nombre_Tratamiento = m.Nombre_Tratamiento,
                    Cuantrimestre = m.Cuantrimestre,
                    Objectivos_Generales = m.Objectivos_Generales,
                    Observaciones = m.Observaciones,
                    Foto = m.Foto,
                    Id_Representante = m.Id_Representante,
                    Nombre_Representante = m.Representante != null ? m.Representante.Nombres : "Sin Representante", 
                    Tipo_Identificacion = m.Tipo_Identificacion,
                    Identificacion = m.Identificacion,
                    Direccion_Postal = m.Direccion_Postal,
                    Localidad = m.Localidad,
                    Nacionalidad = m.Nacionalidad,
                    Domicilio = m.Domicilio,
                    Provincia = m.Provincia,
                    Certificado_Discapacidad = m.Certificado_Discapacidad,
                    Grado_Dependencia = m.Grado_Dependencia,
                    Fecha_Certificado = m.Fecha_Certificado,
                    Derivado_Por = m.Derivado_Por,
                    Estado = m.Estado
                }).FirstOrDefaultAsync(m => m.Id_Paciente == idInt);
                if (pacienteBuscado == null)
                {
                    return null;
                }
                return pacienteBuscado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PacienteDto?> GetByIdAsync(int id)
        {
            if (context == null)
            {
                return null; 
            }
            try
            {
                var pacienteBuscado = await context.Pacientes.Select(m => new PacienteDto
                {
                    Id_Paciente = m.Id_Paciente,
                    Nombres = m.Nombres,
                    Apellidos = m.Apellidos,
                    Fecha_Nacimiento = m.Fecha_Nacimiento,
                    Diagnostico = m.Diagnostico,
                    Fecha_Ult_Evaluacion = m.Fecha_Ult_Evaluacion,
                    Resultados = m.Resultados,
                    Fecha_Inic_Tratamiento = m.Fecha_Inic_Tratamiento,
                    Nombre_Tratamiento = m.Nombre_Tratamiento,
                    Cuantrimestre = m.Cuantrimestre,
                    Objectivos_Generales = m.Objectivos_Generales,
                    Observaciones = m.Observaciones,
                    Foto = m.Foto,
                    Id_Representante = m.Id_Representante,
                    Nombre_Representante = m.Representante != null ? m.Representante.Nombres : "Sin Representante", 
                    Tipo_Identificacion = m.Tipo_Identificacion,
                    Identificacion = m.Identificacion,
                    Direccion_Postal = m.Direccion_Postal,
                    Localidad = m.Localidad,
                    Nacionalidad = m.Nacionalidad,
                    Domicilio = m.Domicilio,
                    Provincia = m.Provincia,
                    Certificado_Discapacidad = m.Certificado_Discapacidad,
                    Grado_Dependencia = m.Grado_Dependencia,
                    Fecha_Certificado = m.Fecha_Certificado,
                    Derivado_Por = m.Derivado_Por,
                    Estado = m.Estado
                }).FirstOrDefaultAsync(m => m.Id_Paciente == id);
                if (pacienteBuscado == null)
                {
                    return null;
                }
                return pacienteBuscado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<PacienteDto>?> ReadAllAsync()
        {
            if (context == null)
            {
                return null; 
            }
            try
            {
                return await context.Pacientes
                    .Select(m => new PacienteDto
                    {
                        Id_Paciente = m.Id_Paciente,
                        Nombres = m.Nombres,
                        Apellidos = m.Apellidos,
                        Fecha_Nacimiento = m.Fecha_Nacimiento,
                        Diagnostico = m.Diagnostico,
                        Fecha_Ult_Evaluacion = m.Fecha_Ult_Evaluacion,
                        Resultados = m.Resultados,
                        Fecha_Inic_Tratamiento = m.Fecha_Inic_Tratamiento,
                        Nombre_Tratamiento = m.Nombre_Tratamiento,
                        Cuantrimestre = m.Cuantrimestre,
                        Objectivos_Generales = m.Objectivos_Generales,
                        Observaciones = m.Observaciones,
                        Foto = m.Foto,
                        Id_Representante = m.Id_Representante,
                        Nombre_Representante = m.Representante != null ? m.Representante.Nombres : "Sin Representante", 
                        Tipo_Identificacion = m.Tipo_Identificacion,
                        Identificacion = m.Identificacion,
                        Direccion_Postal = m.Direccion_Postal,
                        Localidad = m.Localidad,
                        Nacionalidad = m.Nacionalidad,
                        Domicilio = m.Domicilio,
                        Provincia = m.Provincia,
                        Certificado_Discapacidad = m.Certificado_Discapacidad,
                        Grado_Dependencia = m.Grado_Dependencia,
                        Fecha_Certificado = m.Fecha_Certificado,
                        Derivado_Por = m.Derivado_Por,
                        Estado = m.Estado
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Paciente?> UpdateAsync(Paciente Entity)
        {
            Paciente? pacienteBuscado = await context.Pacientes.FindAsync(Entity.Id_Paciente);

            if (pacienteBuscado == null)
            {
                return null;
            }

            pacienteBuscado.Nombres = Entity.Nombres;
            pacienteBuscado.Apellidos = Entity.Apellidos;
            pacienteBuscado.Fecha_Nacimiento = Entity.Fecha_Nacimiento;
            pacienteBuscado.Diagnostico = Entity.Diagnostico;
            pacienteBuscado.Fecha_Ult_Evaluacion = Entity.Fecha_Ult_Evaluacion;
            pacienteBuscado.Resultados = Entity.Resultados;
            pacienteBuscado.Fecha_Inic_Tratamiento = Entity.Fecha_Inic_Tratamiento;
            pacienteBuscado.Nombre_Tratamiento = Entity.Nombre_Tratamiento;
            pacienteBuscado.Cuantrimestre = Entity.Cuantrimestre;
            pacienteBuscado.Objectivos_Generales = Entity.Objectivos_Generales;
            pacienteBuscado.Observaciones = Entity.Observaciones;
            pacienteBuscado.Foto = Entity.Foto;
            pacienteBuscado.Id_Representante = Entity.Id_Representante;
            pacienteBuscado.Tipo_Identificacion = Entity.Tipo_Identificacion;
            pacienteBuscado.Identificacion = Entity.Identificacion;
            pacienteBuscado.Direccion_Postal = Entity.Direccion_Postal;
            pacienteBuscado.Localidad = Entity.Localidad;
            pacienteBuscado.Nacionalidad = Entity.Nacionalidad;
            pacienteBuscado.Domicilio = Entity.Domicilio;
            pacienteBuscado.Provincia = Entity.Provincia;
            pacienteBuscado.Certificado_Discapacidad = Entity.Certificado_Discapacidad;
            pacienteBuscado.Grado_Dependencia = Entity.Grado_Dependencia;
            pacienteBuscado.Fecha_Certificado = Entity.Fecha_Certificado;
            pacienteBuscado.Derivado_Por = Entity.Derivado_Por;
            pacienteBuscado.Estado = Entity.Estado;

            await context.SaveChangesAsync();

            return pacienteBuscado;
        }
        public async Task<List<PacienteDto>?> GetPacienteByRepresentanteAsync(int representanteId)
        {
            if (context == null)
            {
                return null; 
            }
            try
            {
                var pacientes = await context.Pacientes
                    .Where(m => m.Id_Representante == representanteId)
                    .Select(m => new PacienteDto
                    {
                        Id_Paciente = m.Id_Paciente,
                        Nombres = m.Nombres,
                        Apellidos = m.Apellidos,
                        Fecha_Nacimiento = m.Fecha_Nacimiento,
                        Diagnostico = m.Diagnostico,
                        Fecha_Ult_Evaluacion = m.Fecha_Ult_Evaluacion,
                        Resultados = m.Resultados,
                        Fecha_Inic_Tratamiento = m.Fecha_Inic_Tratamiento,
                        Nombre_Tratamiento = m.Nombre_Tratamiento,
                        Cuantrimestre = m.Cuantrimestre,
                        Objectivos_Generales = m.Objectivos_Generales,
                        Observaciones = m.Observaciones,
                        Foto = m.Foto,
                        Id_Representante = m.Id_Representante,
                        Nombre_Representante = m.Representante != null ? m.Representante.Nombres : "Sin Representante", 
                        Tipo_Identificacion = m.Tipo_Identificacion,
                        Identificacion = m.Identificacion,
                        Direccion_Postal = m.Direccion_Postal,
                        Localidad = m.Localidad,
                        Nacionalidad = m.Nacionalidad,
                        Domicilio = m.Domicilio,
                        Provincia = m.Provincia,
                        Certificado_Discapacidad = m.Certificado_Discapacidad,
                        Grado_Dependencia = m.Grado_Dependencia,
                        Fecha_Certificado = m.Fecha_Certificado,
                        Derivado_Por = m.Derivado_Por,
                        Estado = m.Estado
                    })
                    .ToListAsync(); 
                return pacientes;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
