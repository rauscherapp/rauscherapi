using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class CommodityOpenHighLowCloseMap : IEntityTypeConfiguration<CommodityOpenHighLowClose>
    {
        public void Configure(EntityTypeBuilder<CommodityOpenHighLowClose> builder)
        {
            builder.ToTable("CommodityOpenHighLowClose");

            builder.Property(e => e.Id)
                      .HasDefaultValueSql("uuid_generate_v4()")
                      .HasColumnName("Id"); ;

            builder.Property(e => e.Timestamp)
              .HasColumnName("Timestamp")
              .IsRequired();

            builder.Property(e => e.Date)
              .HasColumnName("Date")
              .IsRequired();

            builder.Property(e => e.Base)
              .HasMaxLength(3)
              .HasColumnName("Base")
              .IsRequired();

            builder.Property(e => e.Symbol)
              .HasMaxLength(10)
              .HasColumnName("Symbol")
              .IsRequired();

            builder.Property(e => e.PriceOpen)
              .HasColumnName("PriceOpen")
              .HasColumnType("decimal(18,8)");

            builder.Property(e => e.PriceHigh)
              .HasColumnName("PriceHigh")
              .HasColumnType("decimal(18,8)");

            builder.Property(e => e.PriceLow)
              .HasColumnName("PriceLow")
              .HasColumnType("decimal(18,8)");

            builder.Property(e => e.PriceClose)
              .HasColumnName("PriceClose")
              .HasColumnType("decimal(18,8)");

        }
    }
}
