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
		public string YahooFinanceApiKey { get; set; }
		public string EmailSender { get; set; }
		public string EmailReceiver { get; set; }
		public string EmailPassword { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string MarketOpeningHour { get; set; }
    public string MarketClosingHour { get; set; }
    public bool YahooFinanceApiOn { get; set; }
    public bool CommoditiesApiOn { get; set; }
    public int MinutesIntervalJob { get; set; }
  }
}
