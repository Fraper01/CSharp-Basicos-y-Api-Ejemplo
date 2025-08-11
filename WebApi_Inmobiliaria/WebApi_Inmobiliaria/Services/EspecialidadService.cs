using WebApi_Clinica.Models;
using WebApi_Clinica.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi_Clinica.Shared.Dtos;

namespace WebApi_Clinica.Services
{
    public class EspecialidadService 
    {
        private readonly ClinicaContext context;
        public EspecialidadService(ClinicaContext _context)
        {
            context = _context;
        }
        public async Task<Especialidad?> AddAsync(Especialidad Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Especialidades05.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Especialidad?> CreateAsync(Especialidad Entity)
        {
            if (Entity == null)
            {
                return null;
            }

            await context.Especialidades05.AddAsync(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task<Especialidad?> DeleteAsync(object id)
        {
            var EspecialiBuscada = await context.Especialidades05.FindAsync(id);

            if (EspecialiBuscada == null)
            {
                return null;
            }

            context.Especialidades05.Remove(EspecialiBuscada);
            await context.SaveChangesAsync();
            return EspecialiBuscada;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Especialidades05.AnyAsync(e => e.Id == id);
        }

        public async Task<List<EspecialidadDto>?> GetAllAsync()
        {
            return await context.Especialidades05
                .Select(m => new EspecialidadDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                })
                .ToListAsync();
        }

        public async Task<EspecialidadDto?> GetAsync(object id)
        {
            var idInt = Convert.ToInt32(id);
            var especialidadBuscada = await context.Especialidades05.Select(m => new EspecialidadDto
            {
                Id = m.Id,
                Nombre = m.Nombre
            }).FirstOrDefaultAsync(m => m.Id == idInt);
            if (especialidadBuscada == null)
            {
                return null;
            }
            return especialidadBuscada;
        }

        public async Task<EspecialidadDto?> GetByIdAsync(int id)
        {
            var especialidadBuscada = await context.Especialidades05.Select(m => new EspecialidadDto
            {
                Id = m.Id,
                Nombre = m.Nombre
            }).FirstOrDefaultAsync(m => m.Id == id);
            if (especialidadBuscada == null)
            {
                return null;
            }
            return especialidadBuscada;
        }

        public async Task<List<EspecialidadDto>?> ReadAllAsync()
        {
            return await context.Especialidades05
                .Select(m => new EspecialidadDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre
                })
                .ToListAsync();
        }

        public async Task<Especialidad?> UpdateAsync(Especialidad Entity)
        {
            Especialidad? EspecialiBuscada = await context.Especialidades05.FindAsync(Entity.Id);

            if (EspecialiBuscada == null)
            {
                return null;
            }

            EspecialiBuscada.Nombre = Entity.Nombre;
            await context.SaveChangesAsync();

            return EspecialiBuscada;
        }
        public async Task<List<MedicoDto>?> GetMedicosByEspecialidadAsync(int especialidadId)
        {
            if (context == null)
            {
                return null; 
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
                        EspecialidadNombre = m.Especialidad != null ? m.Especialidad.Nombre : "Sin Especialidad", 
                        Descripcion = m.Descripcion,
                        EspecialidadId = m.EspecialidadId
                    })
                    .ToListAsync(); 
                return medicos;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
