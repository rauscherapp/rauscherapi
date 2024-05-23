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
  public class CadastrarAppParametersCommandHandler : CommandHandler,
          IRequestHandler<CadastrarAppParametersCommand, bool>
  {
    private readonly IAppParametersRepository _appparametersRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarAppParametersCommandHandler(IAppParametersRepository appparametersRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _appparametersRepository = appparametersRepository;
      Bus = bus;
    }
    public Task<bool> Handle(CadastrarAppParametersCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var appparameters = new AppParameters(
        message.Id,
        message.StripeApiClientKey,
        message.StripeApiSecret,
        message.StripeApiPriceId,
        message.CommoditiesApiKey,
        message.EmailSender,
        message.EmailPassword
        );

      _appparametersRepository.Add(appparameters);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarAppParametersEvent(
          message.Id,
          message.StripeApiClientKey,
          message.StripeApiSecret,
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
