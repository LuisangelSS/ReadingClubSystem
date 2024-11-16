//using ReadingClubSystem.Web.Services.Interfaces;
//using ReadingClubSystem.Web.Services;
using ReadingClubSystem.Infrastructure.Interfaces;
using ReadingClubSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios de la aplicación
builder.Services.AddControllersWithViews();

// Registrar servicios HTTP para comunicar con la API
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
});

// Registrar los otros servicios siguiendo el mismo patrón
builder.Services.AddHttpClient<IClubService, ClubService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
});

builder.Services.AddHttpClient<ILibroService, LibroService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
});

builder.Services.AddHttpClient<IReunionService, ReunionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
});

builder.Services.AddHttpClient<IRecomendacionService, RecomendacionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
});

// Crear la aplicación
var app = builder.Build();

// Configurar el pipeline de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar el enrutamiento
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
