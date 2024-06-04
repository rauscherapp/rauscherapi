using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class CadastrarAppParametersEvent : Event
  {
    public CadastrarAppParametersEvent(
    Guid id,
    string stripeApiClientKey,
    string stripeApiSecret,
    string stripeWebhookSecret,
    string stripeApiPriceId,
    int stripeTrialPeriod,
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
      StripeTrialPeriod = stripeTrialPeriod;
      CommoditiesApiKey = commoditiesApiKey;
      EmailSender = emailSender;
      EmailPassword = emailPassword;
    }
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
