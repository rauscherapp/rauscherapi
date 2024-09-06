using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
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
      appparameters.StripeApiClientKey = message.StripeApiClientKey;
      appparameters.StripeApiSecret = message.StripeApiSecret;
      appparameters.StripeWebhookSecret = message.StripeWebhookSecret;
      appparameters.StripeApiPriceId = message.StripeApiPriceId;
      appparameters.CommoditiesApiKey = message.CommoditiesApiKey;
      appparameters.YahooFinanceApiKey = message.YahooFinanceApiKey;
      appparameters.EmailSender = message.EmailSender;
      appparameters.EmailPassword = message.EmailPassword;
      appparameters.SmtpServer = message.SmtpServer;
      appparameters.SmtpPort = message.SmtpPort;
      appparameters.EmailReceiver = message.EmailReceiver;
      appparameters.MarketOpeningHour = message.MarketOpeningHour;
      appparameters.MarketClosingHour = message.MarketClosingHour;
      appparameters.MinutesIntervalJob = message.MinutesIntervalJob;
      appparameters.YahooFinanceApiOn = message.YahooFinanceApiOn;
      appparameters.CommoditiesApiOn = message.CommoditiesApiOn;

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

