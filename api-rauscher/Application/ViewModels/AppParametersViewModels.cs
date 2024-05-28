using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class AppParametersViewModel
	{
		[JsonPropertyName("Id")]
		public Guid Id { get; set; }
		[JsonPropertyName("StripeApiClientKey")]
		public string StripeApiClientKey { get; set; }
		[JsonPropertyName("StripeApiSecret")]
		public string StripeApiSecret { get; set; }
		[JsonPropertyName("StripeApiPriceId")]
		public string StripeApiPriceId { get; set; }
		[JsonPropertyName("StripeWebhookSecret")]
		public string StripeWebhookSecret { get; set; }
		[JsonPropertyName("CommoditiesApiKey")]
		public string CommoditiesApiKey { get; set; }
		[JsonPropertyName("EmailSender")]
		public string EmailSender { get; set; }
		[JsonPropertyName("EmailPassword")]
		public string EmailPassword { get; set; }
	}
}
