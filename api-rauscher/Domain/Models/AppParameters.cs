using System;

namespace Domain.Models
{
	public class AppParameters
	{
		public AppParameters(
		Guid id,
		string StripeApiClientKey,
		string StripeApiSecret,
		string StripeApiPriceId,
		string CommoditiesApiKey,
		string EmailSender,
		string EmailPassword
		)
		{
			Id = id;
			StripeApiClientKey = StripeApiClientKey;
			StripeApiSecret = StripeApiSecret;
      StripeApiPriceId = StripeApiPriceId;
			CommoditiesApiKey = CommoditiesApiKey;
			EmailSender = EmailSender;
			EmailPassword = EmailPassword;
		}
		public Guid Id { get; set; }
		public string StripeApiClientKey { get; set; }
		public string StripeApiSecret { get; set; }
		public string StripeApiPriceId { get; set; }
		public string CommoditiesApiKey { get; set; }
		public string EmailSender { get; set; }
		public string EmailPassword { get; set; }
	}
}
