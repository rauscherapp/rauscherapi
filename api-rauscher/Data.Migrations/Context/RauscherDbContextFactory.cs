using Data.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data.Migrations.Context
{
    public class RauscherDbContextFactory : IDesignTimeDbContextFactory<RauscherDbContext>
    {
        public RauscherDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Data");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RauscherDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=RauscherApp-Dev;User Id=sa; Password=!@Lexos_Hub-Admin12;TrustServerCertificate=true;",
                b => b.MigrationsAssembly("Data.Migrations")
                );
            return new RauscherDbContext(optionsBuilder.Options);
        }
    }
}
