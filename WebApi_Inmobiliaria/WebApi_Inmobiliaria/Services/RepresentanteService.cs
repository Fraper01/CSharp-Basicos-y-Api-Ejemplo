using WebApi_Clinica.Models;
using WebApi_Clinica.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi_Clinica.Shared.Dtos;

namespace WebApi_Clinica.Services
{
    public class RepresentanteService
    {
        private readonly ClinicaContext context;
        public RepresentanteService(ClinicaContext _context)
        {
            context = _context;
        }
        public async Task<Representante?> AddAsync(Representante Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Representantes.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Representante?> CreateAsync(Representante Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Representantes.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Representante?> DeleteAsync(object id)
        {
            var representanteBuscado = await context.Representantes.FindAsync(id);

            if (representanteBuscado == null)
            {
                return null;
            }

            context.Representantes.Remove(representanteBuscado);
            await context.SaveChangesAsync();
            return representanteBuscado;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Representantes.AnyAsync(e => e.Id_Representante == id);
        }

        public async Task<List<RepresentanteDto>?> GetAllAsync()
        {
            return await context.Representantes
                .Select(m => new RepresentanteDto
                {
                    Id_Representante = m.Id_Representante,
                    Nombres = m.Nombres,
                    Direccion = m.Direccion,
                    Telefono_Celular = m.Telefono_Celular,
                    Telefono_Fijo = m.Telefono_Fijo,
                    Persona_Contacto = m.Persona_Contacto,
                    Telefono_Contacto = m.Telefono_Contacto,
                    Email = m.Email,
                    Tipo_Identificacion = m.Tipo_Identificacion,
                    Identificacion = m.Identificacion,
                    Fecha_Registro = m.Fecha_Registro,
                    Usuario_Crea = m.Usuario_Crea,
                    Equipo_Crea = m.Equipo_Crea,
                    Fecha_Modifica = m.Fecha_Modifica,
                    Usuario_Modifica = m.Usuario_Modifica,
                    Equipo_Modifica = m.Equipo_Modifica,
                    Estado = m.Estado
                })
                .ToListAsync();
        }

        public async Task<RepresentanteDto?> GetAsync(object id)
        {
            // 1. se pasa el id a entero para se utilizado luego por se un object
            var idInt = Convert.ToInt32(id);
            // 2. Intenta encontrar el representante usando FindAsync (más eficiente para clave primaria) pero con Dto no funciona
            //    Proyecta a RepresentanteDto (si es necesario)
            var representanteBuscado = await context.Representantes.Select(m => new RepresentanteDto
            {
                Id_Representante = m.Id_Representante,
                Nombres = m.Nombres,
                Direccion = m.Direccion,
                Telefono_Celular = m.Telefono_Celular,
                Telefono_Fijo = m.Telefono_Fijo,
                Persona_Contacto = m.Persona_Contacto,
                Telefono_Contacto = m.Telefono_Contacto,
                Email = m.Email,
                Tipo_Identificacion = m.Tipo_Identificacion,
                Identificacion = m.Identificacion,
                Fecha_Registro = m.Fecha_Registro,
                Usuario_Crea = m.Usuario_Crea,
                Equipo_Crea = m.Equipo_Crea,
                Fecha_Modifica = m.Fecha_Modifica,
                Usuario_Modifica = m.Usuario_Modifica,
                Equipo_Modifica = m.Equipo_Modifica,
                Estado = m.Estado
            }).FirstOrDefaultAsync(m => m.Id_Representante == idInt);
            // 3. Si no se encuentra, devuelve null
            if (representanteBuscado == null)
            {
                return null;
            }
            return representanteBuscado;
        }

        public async Task<RepresentanteDto?> GetByIdAsync(int id)
        {
            // 2. Intenta encontrar el representante usando FindAsync (más eficiente para clave primaria) pero con Dto no funciona
            //    Proyecta a RepresentanteDto (si es necesario)
            var representanteBuscado = await context.Representantes.Select(m => new RepresentanteDto
            {
                Id_Representante = m.Id_Representante,
                Nombres = m.Nombres,
                Direccion = m.Direccion,
                Telefono_Celular = m.Telefono_Celular,
                Telefono_Fijo = m.Telefono_Fijo,
                Persona_Contacto = m.Persona_Contacto,
                Telefono_Contacto = m.Telefono_Contacto,
                Email = m.Email,
                Tipo_Identificacion = m.Tipo_Identificacion,
                Identificacion = m.Identificacion,
                Fecha_Registro = m.Fecha_Registro,
                Usuario_Crea = m.Usuario_Crea,
                Equipo_Crea = m.Equipo_Crea,
                Fecha_Modifica = m.Fecha_Modifica,
                Usuario_Modifica = m.Usuario_Modifica,
                Equipo_Modifica = m.Equipo_Modifica,
                Estado = m.Estado
            }).FirstOrDefaultAsync(m => m.Id_Representante == id);
            // 3. Si no se encuentra, devuelve null
            if (representanteBuscado == null)
            {
                return null;
            }
            return representanteBuscado;
        }

        public async Task<List<RepresentanteDto>?> ReadAllAsync()
        {
            return await context.Representantes
                .Select(m => new RepresentanteDto
                {
                    Id_Representante = m.Id_Representante,
                    Nombres = m.Nombres,
                    Direccion = m.Direccion,
                    Telefono_Celular = m.Telefono_Celular,
                    Telefono_Fijo = m.Telefono_Fijo,
                    Persona_Contacto = m.Persona_Contacto,
                    Telefono_Contacto = m.Telefono_Contacto,
                    Email = m.Email,
                    Tipo_Identificacion = m.Tipo_Identificacion,
                    Identificacion = m.Identificacion,
                    Fecha_Registro = m.Fecha_Registro,
                    Usuario_Crea = m.Usuario_Crea,
                    Equipo_Crea = m.Equipo_Crea,
                    Fecha_Modifica = m.Fecha_Modifica,
                    Usuario_Modifica = m.Usuario_Modifica,
                    Equipo_Modifica = m.Equipo_Modifica,
                    Estado = m.Estado
                })
                .ToListAsync();
        }

        public async Task<Representante?> UpdateAsync(Representante Entity)
        {
            Representante? representanteBuscado = await context.Representantes.FindAsync(Entity.Id_Representante);

            if (representanteBuscado == null)
            {
                return null;
            }

            representanteBuscado.Nombres = Entity.Nombres;
            representanteBuscado.Direccion = Entity.Direccion;
            representanteBuscado.Telefono_Celular = Entity.Telefono_Celular;
            representanteBuscado.Telefono_Fijo = Entity.Telefono_Fijo;
            representanteBuscado.Persona_Contacto = Entity.Persona_Contacto;
            representanteBuscado.Telefono_Contacto = Entity.Telefono_Contacto;
            representanteBuscado.Email = Entity.Email;
            representanteBuscado.Tipo_Identificacion = Entity.Tipo_Identificacion;
            representanteBuscado.Identificacion = Entity.Identificacion;
            representanteBuscado.Fecha_Registro = Entity.Fecha_Registro;
            representanteBuscado.Usuario_Crea = Entity.Usuario_Crea;
            representanteBuscado.Equipo_Crea = Entity.Equipo_Crea;
            representanteBuscado.Fecha_Modifica = Entity.Fecha_Modifica;
            representanteBuscado.Usuario_Modifica = Entity.Usuario_Modifica;
            representanteBuscado.Equipo_Modifica = Entity.Equipo_Modifica;
            representanteBuscado.Estado = Entity.Estado;

            await context.SaveChangesAsync();

            return representanteBuscado;
        }
        public async Task<List<PacienteDto>?> GetPacientesByRepresentanteAsync(int representanteId)
        {
            // Manejar el caso donde el contexto podría ser nulo.
            if (context == null)
            {
                return null; // throw new InvalidOperationException("Context is null");
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
                        Observaciones = m.Observaciones,
                        Foto = m.Foto,
                        Id_Representante = m.Id_Representante,
                        Nombre_Representante = m.Representante != null ? m.Representante.Nombres : "Sin Representante", //Manejo de nulos
                        Certificado_Discapacidad = m.Certificado_Discapacidad,
                        Grado_Dependencia = m.Grado_Dependencia,
                        Fecha_Certificado = m.Fecha_Certificado,
                        Derivado_Por = m.Derivado_Por
                    })
                    .ToListAsync(); // Ejecuta la consulta y materializa el resultado en una lista.
                return pacientes;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
