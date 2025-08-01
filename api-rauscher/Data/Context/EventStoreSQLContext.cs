using Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Context
{
  public partial class EventStoreSQLContext : DbContext
  {
    public EventStoreSQLContext()
    {
    }

    public EventStoreSQLContext(DbContextOptions<EventStoreSQLContext> options)
        : base(options)
    {
      //this.Database.EnsureCreated();
    }

    public virtual DbSet<StoredEvent> StoredEvent { get; set; }

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
      modelBuilder.Entity<StoredEvent>(entity =>
      {
        entity.Property(c => c.Id)
                  .HasColumnName("Id")
                  .HasColumnType("uniqueidentifier");

        entity.Property(c => c.AggregateId)
                  .HasColumnName("AggregateId")
                  .HasColumnType("uniqueidentifier");

        entity.Property(c => c.TimeStamp)
              .HasColumnName("CreationDate");

        entity.Property(c => c.MessageType)
                  .HasColumnName("Action")
                  .HasColumnType("varchar(300)");

        entity.Property(c => c.Data)
                  .HasColumnName("Data")
                  .HasColumnType("varchar(max)");

        entity.Property(c => c.ActionUser)
                  .HasColumnName("ActionUser")
                  .HasColumnType("varchar(max)");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
