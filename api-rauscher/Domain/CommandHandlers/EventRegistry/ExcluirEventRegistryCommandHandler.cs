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
  public class ExcluirEventRegistryCommandHandler : CommandHandler,
          IRequestHandler<ExcluirEventRegistryCommand, bool>
  {
    private readonly IEventRegistryRepository _eventregistryRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirEventRegistryCommandHandler(IEventRegistryRepository eventregistryRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _eventregistryRepository = eventregistryRepository;
      Bus = bus;
    }
    public Task<bool> Handle(ExcluirEventRegistryCommand message, CancellationToken cancellationToken)
    {
      _eventregistryRepository.Remove(message.EventRegistryId);

      if (Commit())
      {
        Bus.RaiseEvent(new ExcluirEventRegistryEvent(
message.EventRegistryId,
message.Eventname,
message.Eventdescription,
message.Eventtype,
message.Eventdate,
message.Eventlocation,
message.Eventlink
));
      }
      return Task.FromResult(true);
    }

  }
}
