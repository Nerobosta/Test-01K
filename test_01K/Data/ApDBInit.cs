using Microsoft.AspNetCore.Identity;
using test_01K.Models;

namespace test_01K.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // 1. Crear los roles si no existen
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Datos del usuario Administrador
            string adminEmail = "admin@test.com";
            string adminPassword = "Admin123Password!"; // Debe cumplir los requisitos de contraseña de Identity

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Administrador Principal",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Crear el usuario
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    // Asignar el rol de Admin
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}