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
  public class ExcluirAppParametersCommandHandler : CommandHandler,
          IRequestHandler<ExcluirAppParametersCommand, bool>
  {
    private readonly IAppParametersRepository _appparametersRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirAppParametersCommandHandler(IAppParametersRepository appparametersRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _appparametersRepository = appparametersRepository;
      Bus = bus;
    }
    public Task<bool> Handle(ExcluirAppParametersCommand message, CancellationToken cancellationToken)
    {
      _appparametersRepository.Remove(message.AppParametersId);

      if (Commit())
      {
        Bus.RaiseEvent(new ExcluirAppParametersEvent(
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
