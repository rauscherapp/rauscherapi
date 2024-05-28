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
      appparameters.EmailSender = message.EmailSender;
      appparameters.EmailPassword = message.EmailPassword;

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
          message.EmailPassword
          ));
      }

      return Task.FromResult(true);
    }

  }
}
