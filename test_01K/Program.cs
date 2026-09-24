using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test_01K.Commands;
using test_01K.Data;
using test_01K.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Obtener la cadena de conexión del appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Registrar DbContext con SQL Server
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Configurar ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 12;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

// 4. Agregar controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Intercepta los comandos (seed:roles, seed:admin, seed:user) antes de arrancar el servidor web
using (var scope = app.Services.CreateScope())
{
    bool wasCommand = await CommandRunner.TryRunAsync(args, scope.ServiceProvider);

    if (wasCommand)
    {
        return; // Termina el programa sin levantar el sitio web
    }
}

// Configurar el pipeline de peticiones HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();