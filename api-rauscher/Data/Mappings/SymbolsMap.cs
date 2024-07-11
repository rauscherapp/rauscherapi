using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
  public class SymbolsMap : IEntityTypeConfiguration<Symbols>
  {
    public void Configure(EntityTypeBuilder<Symbols> builder)
    {
      builder.ToTable("Symbols");

      builder.HasKey(e => e.Id);
      builder.Property(e => e.Id)
      .HasColumnName("Id");

      builder.Property(e => e.Code)
      .HasColumnName("Code");

      builder.Property(e => e.Name)
      .HasColumnName("Name");

      builder.Property(e => e.FriendlyName)
      .HasColumnName("FriendlyName");

      builder.Property(e => e.SymbolType)
      .HasColumnName("SYMBOLTYPE");

      builder.Property(e => e.Appvisible)
      .HasColumnName("AppVisible");

    }
  }
}
