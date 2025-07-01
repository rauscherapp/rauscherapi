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

            var connectionString = "Server=tcp:rauscher-db.database.windows.net,1433;Initial Catalog=db_rauscher;Persist Security Info=False;User ID=adminsql;Password=Rauscher@2025*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var optionsBuilder = new DbContextOptionsBuilder<RauscherDbContext>();
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Data.Migrations"));

            return new RauscherDbContext(optionsBuilder.Options);
        }
    }
}
