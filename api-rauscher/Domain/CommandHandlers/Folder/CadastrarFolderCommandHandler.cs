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
  public class CadastrarFolderCommandHandler : CommandHandler,
          IRequestHandler<CadastrarFolderCommand, bool>
  {
    private readonly IFolderRepository _folderRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarFolderCommandHandler(IFolderRepository folderRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _folderRepository = folderRepository;
      Bus = bus;
    }
    public Task<bool> Handle(CadastrarFolderCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var folder = new Folder(
        message.ID,
        message.TITLE,
        message.SLUG,
        message.ICON
        );

      _folderRepository.Add(folder);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarFolderEvent(
          message.ID,
          message.TITLE,
          message.SLUG,
          message.ICON
          ));
      }

      return Task.FromResult(true);
    }

  }
}
