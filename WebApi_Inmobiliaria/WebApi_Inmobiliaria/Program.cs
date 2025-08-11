using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApi_Clinica.Interfaces;
using WebApi_Clinica.Models;
using WebApi_Clinica.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => // Agrega esta configuración
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi WebAPI de la Clínica", // Puedes cambiar el título
        Version = "v1.0" // Asegúrate de que tienes un valor de versión válido
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

//builder.Services.AddDbContext<ClinicaContext>();
builder.Services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddScoped<WebApi_Clinica.Services.ClinicaService>(); // Registra el servicio de medicos
builder.Services.AddScoped<WebApi_Clinica.Services.EspecialidadService>(); // Registra el servicio de especialidades
builder.Services.AddScoped<WebApi_Clinica.Services.PacienteService>(); // Registra el servicio de especialidades
builder.Services.AddScoped<WebApi_Clinica.Services.RepresentanteService>(); // Registra el servicio de especialidades


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi_Clinica V1"); // Asegúrate de que la ruta coincida con la versión
    });
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
