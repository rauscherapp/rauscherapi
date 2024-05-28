using System;

namespace Domain.Models
{
	public class AppParameters
	{
		public AppParameters(
		Guid id,
		string stripeApiClientKey,
		string stripeApiSecret,
		string stripeWebhookSecret,
		string stripeApiPriceId,
		string commoditiesApiKey,
		string emailSender,
		string emailPassword
		)
		{
			Id = id;
			StripeApiClientKey = stripeApiClientKey;
			StripeApiSecret = stripeApiSecret;
      StripeWebhookSecret = stripeWebhookSecret;
      StripeApiPriceId = stripeApiPriceId;
			CommoditiesApiKey = commoditiesApiKey;
			EmailSender = emailSender;
			EmailPassword = emailPassword;
		}
		public Guid Id { get; set; }
		public string StripeApiClientKey { get; set; }
		public string StripeApiSecret { get; set; }
		public string StripeWebhookSecret { get; set; }
		public string StripeApiPriceId { get; set; }
		public string CommoditiesApiKey { get; set; }
		public string EmailSender { get; set; }
		public string EmailPassword { get; set; }
	}
}
