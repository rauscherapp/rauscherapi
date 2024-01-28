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
  public class CadastrarApicredentialsCommandHandler : CommandHandler,
          IRequestHandler<CadastrarApicredentialsCommand, bool>
  {
    private readonly IApiCredentialsRepository _apicredentialsRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarApicredentialsCommandHandler(IApiCredentialsRepository apicredentialsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _apicredentialsRepository = apicredentialsRepository;
      Bus = bus;
    }
    public Task<bool> Handle(CadastrarApicredentialsCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var apicredentials = new ApiCredentials(message.Apikey,
        message.Apisecrethash,
        message.Createdat,
        message.Lastusedat,
        message.Isactive
        );

      _apicredentialsRepository.Add(apicredentials);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarApicredentialsEvent(
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
