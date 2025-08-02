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

      builder.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("Id");

      builder.Property(e => e.Description)
      .HasColumnName("Description");
    }
  }
}
