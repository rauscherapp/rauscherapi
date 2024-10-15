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
    string emailReceiver,
    string marketOpeningHour,
    string marketClosingHour,
    int minutesIntervalJob,
    bool yahooFinanceApiOn,
    bool commoditiesApiOn,
    string whatsappNumber,
    string contactNumber,
    string instagramUrl,
    string webSiteUrl)
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
      MarketOpeningHour = marketOpeningHour;
      MarketClosingHour = marketClosingHour;
      MinutesIntervalJob = minutesIntervalJob;
      YahooFinanceApiOn = yahooFinanceApiOn;
      CommoditiesApiOn = commoditiesApiOn;
      WhatsappNumber = whatsappNumber;
      ContactNumber = contactNumber;
      InstagramUrl = instagramUrl;
      WebSiteUrl = webSiteUrl;
    }
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
    public string WhatsappNumber { get; set; }
    public string ContactNumber { get; set; }
    public string InstagramUrl { get; set; }
    public string WebSiteUrl { get; set; }
  }
}
