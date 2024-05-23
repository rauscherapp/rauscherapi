using Data.Mappings;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Context
{
  public class RauscherDbContext : DbContext
  {
    public RauscherDbContext()
    {
    }

    public RauscherDbContext(DbContextOptions<RauscherDbContext> options)
        : base(options)
    {
      //this.Database.EnsureCreated();
    }
    //ConfigureDbSet 
    public virtual DbSet<EventRegistry> EventRegistrys { get; set; }
    public virtual DbSet<CommoditiesRate> CommoditiesRates { get; set; }
    public virtual DbSet<Symbols> Symbolss { get; set; }
    public virtual DbSet<ApiCredentials> ApiCredentialss { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Folder> Folders { get; set; }
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
      modelBuilder.ApplyConfiguration(new EventRegistryMap());
      modelBuilder.ApplyConfiguration(new CommoditiesRateMap());
      modelBuilder.ApplyConfiguration(new SymbolsMap());
      modelBuilder.ApplyConfiguration(new ApiCredentialsMap());
      modelBuilder.ApplyConfiguration(new PostMap());
      modelBuilder.ApplyConfiguration(new FolderMap());
    }
  }
}
