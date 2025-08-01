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

            var connectionString = "Host=rauscher-db.database.windows.net;Port=5432;Database=db_rauscher;Username=adminsql;Password=Rauscher@2025*;";

            var optionsBuilder = new DbContextOptionsBuilder<RauscherDbContext>();
            optionsBuilder.UseNpgsql(connectionString,
                b => b.MigrationsAssembly("Data.Migrations"));

            return new RauscherDbContext(optionsBuilder.Options);
        }
    }
}
