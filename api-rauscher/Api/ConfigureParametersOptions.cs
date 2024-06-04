using Application.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;

namespace Api
{
  public class ConfigureParametersOptions : IConfigureOptions<ParametersOptions>
  {
    private readonly IAppParametersOptionsProvider _optionsProvider;

    public ConfigureParametersOptions(IAppParametersOptionsProvider optionsProvider)
    {
      _optionsProvider = optionsProvider;
    }

    public void Configure(ParametersOptions options)
    {
      var optionsEntity = _optionsProvider.GetOptions();
      options.StripeApiClientKey = optionsEntity.StripeApiClientKey;
      options.StripeApiSecret = optionsEntity.StripeApiSecret;
      options.StripeWebhookSecret = optionsEntity.StripeWebhookSecret;
      options.StripePriceId = optionsEntity.StripePriceId;
      options.StripeTrialPeriod = optionsEntity.StripeTrialPeriod;
      options.CommoditiesApiKey = optionsEntity.CommoditiesApiKey;
      options.EmailSender = optionsEntity.EmailSender;
      options.EmailPassword = optionsEntity.EmailPassword;

    }
  }
}
