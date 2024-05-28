using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class AtualizarAppParametersEvent : Event
  {
    public AtualizarAppParametersEvent(
    Guid id,
    string StripeApiClientKey,
    string StripeApiSecret,
    string StripeWebhookSecret,
    string StripeApiPriceId,
    string CommoditiesApiKey,
    string EmailSender,
    string EmailPassword
    )
    {
      Id = id;
      StripeApiClientKey = StripeApiClientKey;
      StripeApiSecret = StripeApiSecret;
      StripeWebhookSecret = StripeWebhookSecret;
      StripeApiPriceId = StripeApiPriceId;
      CommoditiesApiKey = CommoditiesApiKey;
      EmailSender = EmailSender;
      EmailPassword = EmailPassword;
    }
    public Guid Id { get; set; }
    public string StripeApiClientKey { get; set; }
    public string StripeApiSecret { get; set; }
    public string StripeWebhookSecret { get; set; }
    public string CommoditiesApiKey { get; set; }
    public string EmailSender { get; set; }
    public string EmailPassword { get; set; }
  }
}
