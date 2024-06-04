using Application.Interfaces;
using Data.Context;
using Domain.Options;
using System.Linq;

namespace Aplication.Provider
{
  public class AppParametersOptionsProvider : IAppParametersOptionsProvider
  {
    private readonly RauscherDbContext _dbContext;

    public AppParametersOptionsProvider(RauscherDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public ParametersOptions GetOptions()
    {
      // Fetch the options from the database
      var optionsEntity = _dbContext.AppParameters.FirstOrDefault();
      if (optionsEntity == null) return new ParametersOptions();

      return new ParametersOptions
      {
        StripeApiClientKey = optionsEntity.StripeApiClientKey,
        StripeApiSecret = optionsEntity.StripeApiSecret,
        StripeWebhookSecret = optionsEntity.StripeWebhookSecret,
        StripePriceId = optionsEntity.StripeApiPriceId,
        StripeTrialPeriod = optionsEntity.StripeTrialPeriod,
        CommoditiesApiKey = optionsEntity.CommoditiesApiKey,
        EmailSender = optionsEntity.EmailSender,
        EmailPassword = optionsEntity.EmailPassword
      };
    }
  }
}
