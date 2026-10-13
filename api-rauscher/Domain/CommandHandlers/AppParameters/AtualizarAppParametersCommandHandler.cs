using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class AtualizarAppParametersCommandHandler : CommandHandler,
          IRequestHandler<AtualizarAppParametersCommand, bool>
  {
    private readonly IAppParametersRepository _appparametersRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarAppParametersCommandHandler(IAppParametersRepository appparametersRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _appparametersRepository = appparametersRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarAppParametersCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var appparameters = _appparametersRepository.GetById(message.Id);
      appparameters.Id = message.Id;
      appparameters.StripeApiClientKey = AppParameterSecrets.Resolve(message.StripeApiClientKey, appparameters.StripeApiClientKey);
      appparameters.StripeApiSecret = AppParameterSecrets.Resolve(message.StripeApiSecret, appparameters.StripeApiSecret);
      appparameters.StripeWebhookSecret = AppParameterSecrets.Resolve(message.StripeWebhookSecret, appparameters.StripeWebhookSecret);
      appparameters.StripeApiPriceId = message.StripeApiPriceId;
      appparameters.CommoditiesApiKey = AppParameterSecrets.Resolve(message.CommoditiesApiKey, appparameters.CommoditiesApiKey);
      appparameters.YahooFinanceApiKey = AppParameterSecrets.Resolve(message.YahooFinanceApiKey, appparameters.YahooFinanceApiKey);
      appparameters.EmailSender = message.EmailSender;
      appparameters.EmailPassword = AppParameterSecrets.Resolve(message.EmailPassword, appparameters.EmailPassword);
      appparameters.SmtpServer = message.SmtpServer;
      appparameters.SmtpPort = message.SmtpPort;
      appparameters.EmailReceiver = message.EmailReceiver;
      appparameters.MarketOpeningHour = message.MarketOpeningHour;
      appparameters.MarketClosingHour = message.MarketClosingHour;
      appparameters.MinutesIntervalJob = message.MinutesIntervalJob;
      appparameters.YahooFinanceApiOn = message.YahooFinanceApiOn;
      appparameters.CommoditiesApiOn = message.CommoditiesApiOn;
      appparameters.WhatsappNumber = message.WhatsappNumber;
      appparameters.ContactNumber = message.ContactNumber;
      appparameters.InstagramUrl = message.InstagramUrl;
      appparameters.WebSiteUrl = message.WebSiteUrl;


      _appparametersRepository.Update(appparameters);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarAppParametersEvent(
          message.Id,
          message.StripeApiClientKey,
          message.StripeApiSecret,
          message.StripeWebhookSecret,
          message.StripeApiPriceId,
          message.CommoditiesApiKey,
          message.EmailSender,
          message.EmailPassword,
          message.SmtpServer,
          message.SmtpPort,
          message.EmailReceiver,
          message.MarketOpeningHour,
          message.MarketClosingHour,
          message.MinutesIntervalJob,
          message.YahooFinanceApiOn,
          message.CommoditiesApiOn,
          message.YahooFinanceApiKey
          ));
      }
      return Task.FromResult(true);
    }
  }
}

