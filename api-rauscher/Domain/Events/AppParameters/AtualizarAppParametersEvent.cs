using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class AtualizarAppParametersEvent : Event
  {
    public AtualizarAppParametersEvent(
    Guid id,
    string stripeApiClientKey,
    string stripeApiSecret,
    string stripeWebhookSecret,
    string stripeApiPriceId,
    string commoditiesApiKey,
    string emailSender,
    string emailPassword,
    string smtpServer,
    int smtpPort,
    string emailReceiver,
    string marketOpeningHour,
    string marketClosingHour,
    int minutesIntervalJob)
    {
      Id = id;
      StripeApiClientKey = stripeApiClientKey;
      StripeApiSecret = stripeApiSecret;
      StripeWebhookSecret = stripeWebhookSecret;
      StripeApiPriceId = stripeApiPriceId;
      CommoditiesApiKey = commoditiesApiKey;
      EmailSender = emailSender;
      EmailPassword = emailPassword;
      SmtpServer = smtpServer;
      SmtpPort = smtpPort;
      EmailReceiver = emailReceiver;
      MarketOpeningHour = marketOpeningHour;
      MarketClosingHour = marketClosingHour;
      MinutesIntervalJob = minutesIntervalJob;
    }
    public Guid Id { get; set; }
    public string StripeApiClientKey { get; set; }
    public string StripeApiSecret { get; set; }
    public string StripeWebhookSecret { get; set; }
    public string StripeApiPriceId { get; set; }
    public string CommoditiesApiKey { get; set; }
    public string EmailSender { get; set; }
    public string EmailPassword { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string EmailReceiver { get; }
    public string MarketOpeningHour { get; set; }
    public string MarketClosingHour { get; set; }
    public int MinutesIntervalJob { get; set; }
  }
}
