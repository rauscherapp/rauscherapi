using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class ExcluirAppParametersEvent : Event
  {
    public ExcluirAppParametersEvent(
    Guid id,
    string StripeApiClientKey,
    string StripeApiSecret,
    string StripeWebhookSecret,
    string StripeApiPriceId,
    string CommoditiesApiKey,
    string EmailSender,
    string EmailPassword
,
    string smtpServer,
    int smtpPort,
    string emailReceiver,
    string marketOpeningHour,
    string marketClosingHour,
    int minutesIntervalJob,
    bool yahooFinanceApiOn,
    bool commoditiesApiOn)
    {
      Id = id;
      StripeApiClientKey = StripeApiClientKey;
      StripeApiSecret = StripeApiSecret;
      StripeWebhookSecret = StripeWebhookSecret;
      StripeApiPriceId = StripeApiPriceId;
      CommoditiesApiKey = CommoditiesApiKey;
      EmailSender = EmailSender;
      EmailPassword = EmailPassword;
      SmtpServer = smtpServer;
      SmtpPort = smtpPort;
      EmailReceiver = emailReceiver;
      MarketOpeningHour = marketOpeningHour;
      MarketClosingHour = marketClosingHour;
      MinutesIntervalJob = minutesIntervalJob;
      YahooFinanceApiOn = yahooFinanceApiOn;
      CommoditiesApiOn = commoditiesApiOn;
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
    public bool YahooFinanceApiOn { get; set; }
    public bool CommoditiesApiOn { get; set; }
  }
}
