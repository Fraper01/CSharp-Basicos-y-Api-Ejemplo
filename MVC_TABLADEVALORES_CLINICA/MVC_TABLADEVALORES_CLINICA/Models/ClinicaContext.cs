using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using MVC_TABLADEVALORES_CLINICA.Migrations;
using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MVC_TABLADEVALORES_CLINICA.Models
{
    public class ClinicaContext:DbContext
    {
        public DbSet<Medico> Medicos05 { get; set; }
        public DbSet<Representante> Representantes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidad> Especialidades05 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>()
                .HasOne<Especialidad>(m => m.Especialidad)
                .WithMany(e => e.Medicos)
                .HasForeignKey(m => m.EspecialidadId)
                .HasConstraintName("FK_Medicos05_Especialidades05"); 

            modelBuilder.Entity<Paciente>()
                .HasOne<Representante>(p => p.Representante)
                .WithMany(e => e.Pacientes)
                .HasForeignKey(m => m.Id_Representante)
                .HasConstraintName("FK_Paciente_Representante");
        }

    }
}
