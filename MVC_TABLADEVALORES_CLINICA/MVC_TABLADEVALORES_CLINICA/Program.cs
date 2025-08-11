using MVC_TABLADEVALORES_CLINICA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<MedicoApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5186/swagger/WebApi_Clinica/v1/Medicos");
});
builder.Services.AddHttpClient<EspecialidadApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5186/swagger/WebApi_Clinica/v1/Especiald");
});
builder.Services.AddHttpClient<RepresentanteApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5186/swagger/WebApi_Clinica/v1/Represent");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
