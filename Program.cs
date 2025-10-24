using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SistemaTurnos.Web.Data;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
