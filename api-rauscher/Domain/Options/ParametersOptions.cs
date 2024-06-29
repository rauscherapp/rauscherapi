namespace Domain.Options
{
  public class ParametersOptions
  {
    public string StripeApiClientKey { get; set; }
    public string StripeApiSecret { get; set; }
    public string StripeWebhookSecret { get; set; }
    public string StripePriceId { get; set; }
    public int StripeTrialPeriod { get; set; }
    public string CommoditiesApiKey { get; set; }
    public string EmailSender { get; set; }
    public string EmailReceiver { get; set; }
    public string EmailPassword { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
  }
}
