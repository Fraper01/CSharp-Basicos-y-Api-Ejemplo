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

builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi WebAPI de la Clínica", 
        Version = "v1.0" 
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

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

// Registra los servicios
builder.Services.AddScoped<WebApi_Clinica.Services.ClinicaService>(); 
builder.Services.AddScoped<WebApi_Clinica.Services.EspecialidadService>(); 
builder.Services.AddScoped<WebApi_Clinica.Services.PacienteService>(); 
builder.Services.AddScoped<WebApi_Clinica.Services.RepresentanteService>(); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi_Clinica V1"); 
    });
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
