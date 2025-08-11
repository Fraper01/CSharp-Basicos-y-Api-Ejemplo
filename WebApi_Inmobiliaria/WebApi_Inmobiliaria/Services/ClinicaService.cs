using WebApi_Clinica.Models;
using WebApi_Clinica.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi_Clinica.Shared.Dtos;

namespace WebApi_Clinica.Services
{
    public class ClinicaService /*: ICRUD<Medico>*/
    {
        private readonly ClinicaContext context;
        public ClinicaService(ClinicaContext _context)
        {
            context = _context;
        }

        public async Task<Medico?> AddAsync(Medico Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Medicos05.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Medico?> CreateAsync(Medico Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Medicos05.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Medico?> DeleteAsync(object id)
        {
            var medicoBuscado = await context.Medicos05.FindAsync(id);

            if (medicoBuscado == null)
            {
                return null;
            }

            context.Medicos05.Remove(medicoBuscado);
            await context.SaveChangesAsync();
            return medicoBuscado;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Medicos05.AnyAsync(m => m.Id == id);
        }

        public async Task<List<MedicoDto>?> GetAllAsync()
        {
            // Manejar el caso donde el contexto podría ser nulo.
            if (context == null)
            {
                return null; // throw new InvalidOperationException("Context is null");
            }
            try
            {
                return await context.Medicos05
                    .Select(m => new MedicoDto
                    {
                        Id = m.Id,
                        NumeroColegiado = m.NumeroColegiado,
                        Nombre = m.Nombre,
                        Apellido = m.Apellido,
                        EspecialidadNombre = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad", //Manejo de nulos
                        Descripcion = m.Descripcion,
                        EspecialidadId = m.EspecialidadId
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MedicoDto?> GetAsync(object id)
        {
            // Manejar el caso donde el contexto podría ser nulo.
            if (context == null)
            {
                return null; // throw new InvalidOperationException("Context is null");
            }
            try
            {
                // 1. se pasa el id a entero para se utilizado luego por se un object
                var idInt = Convert.ToInt32(id);
                // 2. Intenta encontrar el médico usando FindAsync (más eficiente para clave primaria)
                //    Proyecta a MedicoDto (si es necesario)
                var medicoBuscado = await context.Medicos05.Select(m => new MedicoDto
                    {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Apellido = m.Apellido,
                    EspecialidadId = m.EspecialidadId,
                    EspecialidadNombre = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad", //Manejo de nulos
                    Descripcion = m.Descripcion,
                    NumeroColegiado = m.NumeroColegiado
                    }).FirstOrDefaultAsync(m => m.Id == idInt);
                // 3. Si no se encuentra, devuelve null
                if (medicoBuscado == null)
                {
                    return null;
                }
                return medicoBuscado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MedicoDto?> GetByIdAsync(int id)
        {
            // Manejar el caso donde el contexto podría ser nulo.
            if (context == null)
            {
                return null; // throw new InvalidOperationException("Context is null");
            }
            try
            {
                // 1. Intenta encontrar el médico usando FindAsync (más eficiente para clave primaria)
                //    Proyecta a MedicoDto (si es necesario)
                var medicoBuscado = await context.Medicos05.Select(m => new MedicoDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Apellido = m.Apellido,
                    EspecialidadId = m.EspecialidadId,
                    EspecialidadNombre = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad", //Manejo de nulos
                    Descripcion = m.Descripcion,
                    NumeroColegiado = m.NumeroColegiado
                }).FirstOrDefaultAsync(m => m.Id == id);
                // 2. Si no se encuentra, devuelve null
                if (medicoBuscado == null)
                {
                    return null;
                }
                return medicoBuscado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<MedicoDto>?> ReadAllAsync()
        {
            // Manejar el caso donde el contexto podría ser nulo.
            if (context == null)
            {
                return null; // throw new InvalidOperationException("Context is null");
            }
            try
            {
                return await context.Medicos05
                    .Select(m => new MedicoDto
                    {
                        Id = m.Id,
                        NumeroColegiado = m.NumeroColegiado,
                        Nombre = m.Nombre,
                        Apellido = m.Apellido,
                        EspecialidadNombre = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad", //Manejo de nulos
                        Descripcion = m.Descripcion,
                        EspecialidadId = m.EspecialidadId
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Medico?> UpdateAsync(Medico Entity)
        {
            Medico? medicoBuscado = await context.Medicos05.FindAsync(Entity.Id);

            if (medicoBuscado == null)
            {
                return null;
            }

            medicoBuscado.NumeroColegiado = Entity.NumeroColegiado;
            medicoBuscado.Nombre = Entity.Nombre;
            medicoBuscado.Apellido = Entity.Apellido;
            medicoBuscado.EspecialidadId = Entity.EspecialidadId;
            medicoBuscado.Descripcion = Entity.Descripcion;
            await context.SaveChangesAsync();

            return medicoBuscado;
        }
        public async Task<List<MedicoDto>?> GetMedicosByEspecialidadAsync(int especialidadId)
        {
            // Manejar el caso donde el contexto podría ser nulo.
            if (context == null)
            {
                return null; // throw new InvalidOperationException("Context is null");
            }
            try
            {
                var medicos = await context.Medicos05
                    .Where(m => m.EspecialidadId == especialidadId)
                    .Select(m => new MedicoDto
                    {
                        Id = m.Id,
                        NumeroColegiado = m.NumeroColegiado,
                        Nombre = m.Nombre,
                        Apellido = m.Apellido,
                        EspecialidadNombre = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad", //Manejo de nulos
                        Descripcion = m.Descripcion,
                        EspecialidadId = m.EspecialidadId
                    })
                    .ToListAsync(); // Ejecuta la consulta y materializa el resultado en una lista.
                return medicos;
            }
            catch (Exception)
            {
                return null; 
            }
        }
    }
}

