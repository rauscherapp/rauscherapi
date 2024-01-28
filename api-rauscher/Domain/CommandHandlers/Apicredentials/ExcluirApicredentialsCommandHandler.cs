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
  public class ExcluirApicredentialsCommandHandler : CommandHandler,
          IRequestHandler<ExcluirApicredentialsCommand, bool>
  {
    private readonly IApiCredentialsRepository _apicredentialsRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirApicredentialsCommandHandler(IApiCredentialsRepository apicredentialsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _apicredentialsRepository = apicredentialsRepository;
      Bus = bus;
    }
    public Task<bool> Handle(ExcluirApicredentialsCommand message, CancellationToken cancellationToken)
    {
      _apicredentialsRepository.RemoveApiCredentials(message.Apikey);

      if (Commit())
      {
        Bus.RaiseEvent(new ExcluirApicredentialsEvent(
          message.Apikey,
          message.Apisecrethash,
          message.Createdat,
          message.Lastusedat,
          message.Isactive
          ));
      }
      return Task.FromResult(true);
    }
  }
}
