using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
  public class AboutUsMap : IEntityTypeConfiguration<AboutUs>
  {
    public void Configure(EntityTypeBuilder<AboutUs> builder)
    {
      builder.ToTable("AboutUs");

      builder.HasKey(e => e.Id);
      builder.Property(e => e.Description)
      .HasColumnName("Description");
    }
  }
}
