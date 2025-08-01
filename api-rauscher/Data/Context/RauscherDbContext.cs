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
    public virtual DbSet<AppParameters> AppParameters { get; set; }
    public virtual DbSet<EventRegistry> EventRegistrys { get; set; }
    public virtual DbSet<CommoditiesRate> CommoditiesRates { get; set; }
    public virtual DbSet<Symbols> Symbolss { get; set; }
    public virtual DbSet<ApiCredentials> ApiCredentialss { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Folder> Folders { get; set; }
    public virtual DbSet<AboutUs> AboutUs { get; set; }
    public virtual DbSet<CommodityOpenHighLowClose> CommodityOpenHighLowCloses { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build();

        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
      }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //ConfigureMap 
      modelBuilder.ApplyConfiguration(new AppParametersMap());
      modelBuilder.ApplyConfiguration(new EventRegistryMap());
      modelBuilder.ApplyConfiguration(new CommoditiesRateMap());
      modelBuilder.ApplyConfiguration(new SymbolsMap());
      modelBuilder.ApplyConfiguration(new ApiCredentialsMap());
      modelBuilder.ApplyConfiguration(new CommodityOpenHighLowCloseMap());
      modelBuilder.ApplyConfiguration(new PostMap());
      modelBuilder.ApplyConfiguration(new FolderMap());
      modelBuilder.ApplyConfiguration(new AboutUsMap());
      modelBuilder.Entity<CommoditiesRate>()
          .HasOne(cr => cr.Symbol)
          .WithMany(s => s.CommoditiesRates)
          .HasForeignKey(cr => cr.SymbolCode)
          .HasPrincipalKey(s => s.Code);
    }
  }
}
