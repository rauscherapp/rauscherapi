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
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = "Host=167.71.90.85;Username=rauscheradmin;Password=R4u$Ch3R*2025;Database=<nome>";

            var optionsBuilder = new DbContextOptionsBuilder<RauscherDbContext>();
            optionsBuilder.UseNpgsql(connectionString,
                b => b.MigrationsAssembly("Data.Migrations"));

            return new RauscherDbContext(optionsBuilder.Options);
        }
    }
}
