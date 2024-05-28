using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
	public class AppParametersMap : IEntityTypeConfiguration<AppParameters>
	{
		public void Configure(EntityTypeBuilder<AppParameters> builder)
		{
			builder.ToTable("AppParameters");
			
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id)
			.HasColumnName("Id");
			
			builder.Property(e => e.StripeApiClientKey)
			.HasColumnName("StripeApiClientKey");
			
			builder.Property(e => e.StripeApiSecret)
			.HasColumnName("StripeApiSecret");			
			
			builder.Property(e => e.StripeWebhookSecret)
			.HasColumnName("StripeWebhookSecret");			

			builder.Property(e => e.StripeApiPriceId)
			.HasColumnName("StripeApiPriceId");
			
			builder.Property(e => e.CommoditiesApiKey)
			.HasColumnName("CommoditiesApiKey");
			
			builder.Property(e => e.EmailSender)
			.HasColumnName("EmailSender");
			
			builder.Property(e => e.EmailPassword)
			.HasColumnName("EmailPassword");
			
		}
	}
}
