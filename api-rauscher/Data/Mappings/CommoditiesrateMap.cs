using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
	public class CommoditiesRateMap : IEntityTypeConfiguration<CommoditiesRate>
	{
		public void Configure(EntityTypeBuilder<CommoditiesRate> builder)
		{
			builder.ToTable("CommoditiesRate");
			
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id)
			.HasColumnName("Id");
			
			builder.Property(e => e.Timestamp)
			.HasColumnName("Timestamp");
			
			builder.Property(e => e.BaseCurrency)
			.HasColumnName("Base");
			
			builder.Property(e => e.Date)
			.HasColumnName("Date");
			
			builder.Property(e => e.SymbolCode)
			.HasColumnName("SymbolCode");
			
			builder.Property(e => e.Unit)
			.HasColumnName("Unit");
			
			builder.Property(e => e.Price)
			.HasColumnName("Price")
      .HasColumnType("decimal(18, 4)"); 
			
			builder.Property(e => e.Variationprice)
			.HasColumnName("VariationPrice")
      .HasColumnType("decimal(18, 4)"); 
			
			builder.Property(e => e.Variationpricepercent)
			.HasColumnName("VariationPricePercent")
      .HasColumnType("decimal(18, 4)");

      builder.Property(e => e.Isup)
			.HasColumnName("isUp");

			builder.HasOne<Symbols>()
					.WithMany(s => s.CommoditiesRates)
					.HasForeignKey(c => c.SymbolCode)
					.OnDelete(DeleteBehavior.SetNull);

    }
	}
}
