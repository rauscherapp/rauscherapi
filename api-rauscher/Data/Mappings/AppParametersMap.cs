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

			builder.Property(e => e.StripeTrialPeriod)
			.HasColumnName("StripeTrialPeriod");
			
			builder.Property(e => e.CommoditiesApiKey)
			.HasColumnName("CommoditiesApiKey");			
			builder.Property(e => e.YahooFinanceApiKey)
			.HasColumnName("YahooFinanceApiKey");
			
			builder.Property(e => e.EmailSender)
			.HasColumnName("EmailSender");			
			builder.Property(e => e.EmailReceiver)
			.HasColumnName("EmailReceiver");
			
			builder.Property(e => e.EmailPassword)
			.HasColumnName("EmailPassword");
			
			
			builder.Property(e => e.SmtpServer)
			.HasColumnName("SmtpServer");
			
			builder.Property(e => e.SmtpPort)
			.HasColumnName("SmtpPort");			
			builder.Property(e => e.MarketOpeningHour)
			.HasColumnName("MarketOpeningHour");			
			builder.Property(e => e.MarketClosingHour)
			.HasColumnName("MarketClosingHour");			
			builder.Property(e => e.YahooFinanceApiOn)
			.HasColumnName("YahooFinanceApiOn");			
			builder.Property(e => e.CommoditiesApiOn)
			.HasColumnName("CommoditiesApiOn");			
			builder.Property(e => e.MinutesIntervalJob)
			.HasColumnName("MinutesIntervalJob");			
			builder.Property(e => e.WhatsappNumber)
			.HasColumnName("WhatsappNumber");			
			builder.Property(e => e.ContactNumber)
			.HasColumnName("ContactNumber");			
			builder.Property(e => e.InstagramUrl)
			.HasColumnName("InstagramUrl");
			builder.Property(e => e.WebSiteUrl)
			.HasColumnName("WebSiteUrl");
  }
	}
}
