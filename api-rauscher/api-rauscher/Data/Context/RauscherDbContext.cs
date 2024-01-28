using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Context
{
  internal class RauscherDbContext : DbContext
  {
    public RauscherDbContext()
    {
    }

    public RauscherDbContext(DbContextOptions<RauscherDbContext> options)
        : base(options)
    {
      this.Database.EnsureCreated();
    }
    //ConfigureDbSet 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
      }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //ConfigureMap
    }
  }
}
