using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class AppParametersCommand : Command
	{
		public Guid Id { get; set; }
		public string StripeApiClientKey { get; set; }
		public string StripeApiSecret { get; set; }
		public string StripeWebhookSecret { get; set; }
		public string StripeApiPriceId { get; set; }
		public int StripeTrialPeriod { get; set; }
		public string CommoditiesApiKey { get; set; }
		public string EmailSender { get; set; }
		public string EmailPassword { get; set; }
	}
}
