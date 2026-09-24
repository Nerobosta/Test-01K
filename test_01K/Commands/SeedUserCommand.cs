using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using test_01K.Commands;
using test_01K.Models;

namespace test_01K.Commands
{
    public class SeedUserCommand : ICommand
    {
        // Este nombre es el que aparecerá en tus comandos disponibles
        public string Name => "seed:user";

        public async Task ExecuteAsync(IServiceProvider services, string[] args)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var email = GetArgValue(args, "--email");
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.Write("Email del usuario estándar: ");
                email = Console.ReadLine();
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                Console.WriteLine("El rol 'User' no existe. Corre 'seed:roles' primero.");
                return;
            }

            var existing = await userManager.FindByEmailAsync(email!);
            if (existing != null)
            {
                Console.WriteLine("Ya existe un usuario con ese email.");
                return;
            }

            Console.Write("Contraseña: ");
            var password = ReadPasswordMasked();

            var user = new User
            {
                UserName = email,
                Email = email,
                FullName = "Usuario Estándar",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                Console.WriteLine("Usuario estándar creado correctamente.");
            }
            else
            {
                foreach (var error in result.Errors)
                    Console.WriteLine($"Error: {error.Description}");
            }
        }

        private static string? GetArgValue(string[] args, string key)
        {
            var index = Array.IndexOf(args, key);
            return (index >= 0 && index + 1 < args.Length) ? args[index + 1] : null;
        }

        private static string ReadPasswordMasked()
        {
            var password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }
    }
}