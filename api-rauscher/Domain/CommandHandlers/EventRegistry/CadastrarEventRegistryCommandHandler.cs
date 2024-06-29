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
  public class CadastrarEventRegistryCommandHandler : CommandHandler,
          IRequestHandler<CadastrarEventRegistryCommand, bool>
  {
    private readonly IEventRegistryRepository _eventregistryRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarEventRegistryCommandHandler(IEventRegistryRepository eventregistryRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _eventregistryRepository = eventregistryRepository;
      Bus = bus;
    }
    public Task<bool> Handle(CadastrarEventRegistryCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var eventregistry = new EventRegistry(
        message.EventRegistryId,
        message.Eventname,
        message.Eventdescription,
        message.Eventtype,
        message.Eventdate,
        message.Eventlocation,
        message.Eventlink,
        message.Published
        );

      _eventregistryRepository.Add(eventregistry);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarEventRegistryEvent(
          message.EventRegistryId,
          message.Eventname,
          message.Eventdescription,
          message.Eventtype,
          message.Eventdate,
          message.Eventlocation,
          message.Eventlink,
          message.Published
          ));
      }

      return Task.FromResult(true);
    }

  }
}
