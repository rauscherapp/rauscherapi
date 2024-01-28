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
  public class AtualizarApicredentialsCommandHandler : CommandHandler,
          IRequestHandler<AtualizarApicredentialsCommand, bool>
  {
    private readonly IApiCredentialsRepository _apicredentialsRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarApicredentialsCommandHandler(IApiCredentialsRepository apicredentialsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _apicredentialsRepository = apicredentialsRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarApicredentialsCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var apicredentials = _apicredentialsRepository.ObterApiCredentials(message.Apikey);
      apicredentials.Apikey = message.Apikey;
      apicredentials.Apisecrethash = message.Apisecrethash;
      apicredentials.Createdat = message.Createdat;
      apicredentials.Lastusedat = message.Lastusedat;
      apicredentials.Isactive = message.Isactive;

      _apicredentialsRepository.Update(apicredentials);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarApicredentialsEvent(
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
