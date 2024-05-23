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
  public class AtualizarEventRegistryCommandHandler : CommandHandler,
          IRequestHandler<AtualizarEventRegistryCommand, bool>
  {
    private readonly IEventRegistryRepository _eventregistryRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarEventRegistryCommandHandler(IEventRegistryRepository eventregistryRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _eventregistryRepository = eventregistryRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarEventRegistryCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var eventregistry = _eventregistryRepository.GetById(message.EventRegistryId);
      eventregistry.EventRegistryId = message.EventRegistryId;
      eventregistry.EventName = message.Eventname;
      eventregistry.EventDescription = message.Eventdescription;
      eventregistry.EventType = message.Eventtype;
      eventregistry.EventDate = message.Eventdate;
      eventregistry.EventLocation = message.Eventlocation;
      eventregistry.EventLink = message.Eventlink;

      _eventregistryRepository.Update(eventregistry);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarEventRegistryEvent(
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
