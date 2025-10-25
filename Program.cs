using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;
using SistemaTurnos.Web.Services;
using SistemaTurnos.Web.Services.Interfaces; 
using SistemaTurnos.Web.Utilities;
using SistemaTurnos.Web.Utilities.interfaces;
using SistemaTurnos.Web.Repositories.Interfaces;
using SistemaTurnos.Web.Repositories;


//1. Cargamos las variables de entorno:
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 2. Obtenemos la cadena de conexion (Ya que esta en formato clave: Valor) 
var connectionString = builder.Configuration.GetValue<string>("SUPABASE_CONNECTION_STRING");

// 🚨 TEMPORALMENTE para migraciones:
// Si detectamos que estamos ejecutando la herramienta 'dotnet ef', usamos la cadena directa.
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" && args.Contains("database update")) 
{
    connectionString = builder.Configuration.GetValue<string>("SUPABASE_MIGRATION_CONNECTION");
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("La cadena de conexion no fue encontrada: SUPABASE_CONNECTION_STRING");
}

//3. Configurar el DbContext para usar Npgsql (driver) y el conection pooler:
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
         maxRetryCount: 5,
         maxRetryDelay: TimeSpan.FromSeconds(30),
         errorCodesToAdd: null);
    }));


// ===================================================================================
// [3. REGISTRO DE SERVICIOS Y UTILIDADES (INYECCIÓN DE DEPENDENCIAS)] << AÑADIDO AQUÍ
// ===================================================================================

// ------------------------------------
// 1. REGISTRO DE REPOSITORIOS (Soluciona el error de DI)
// ------------------------------------
builder.Services.AddScoped<IAfiliadoRepository, AfiliadoRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();

// Repositorio CRUD Base (Genérico)
builder.Services.AddScoped(typeof(BaseCrudRepository<>));


// ------------------------------------
// 2. REGISTRO DE SERVICIOS Y UTILIDADES
// ------------------------------------

// **Utilidades** (AddSingleton): El generador de QR es sin estado.
builder.Services.AddSingleton<IQRHelper, QrHelper>();

// **Servicios de Negocio** (AddScoped): Manejan lógica de negocio y contexto de solicitud.
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IAfiliadoService, AfiliadoService>();
builder.Services.AddScoped<IturnoService, TurnoService>();

// ===================================================================================
// FIN REGISTRO DE SERVICIOS
// ===================================================================================

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Manteniendo UseAuthorization aunque las rutas sean públicas, por si decides añadir roles después.
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();