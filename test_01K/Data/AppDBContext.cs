using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using test_01K.Models;

namespace test_01K.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        // Constructor necesario para ASP.NET Core
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de valores por defecto en SQL Server
            builder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
