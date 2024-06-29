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
    int stripeTrialPeriod,
    string commoditiesApiKey,
    string emailSender,
    string emailPassword,
    string smtpServer,
    int smtpPort,
    string emailReceiver)
    {
      Id = id;
      StripeApiClientKey = stripeApiClientKey;
      StripeApiSecret = stripeApiSecret;
      StripeWebhookSecret = stripeWebhookSecret;
      StripeApiPriceId = stripeApiPriceId;
      StripeTrialPeriod = stripeTrialPeriod;
      CommoditiesApiKey = commoditiesApiKey;
      EmailSender = emailSender;
      EmailPassword = emailPassword;
      SmtpServer = smtpServer;
      SmtpPort = smtpPort;
      EmailReceiver = emailReceiver;
    }
    public Guid Id { get; set; }
    public string StripeApiClientKey { get; set; }
    public string StripeApiSecret { get; set; }
    public string StripeWebhookSecret { get; set; }
    public string StripeApiPriceId { get; set; }
    public int StripeTrialPeriod { get; set; }
    public string CommoditiesApiKey { get; set; }
    public string EmailSender { get; set; }
    public string EmailReceiver { get; set; }
    public string EmailPassword { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
  }
}
