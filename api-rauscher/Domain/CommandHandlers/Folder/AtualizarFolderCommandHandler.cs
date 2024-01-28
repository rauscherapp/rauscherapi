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
  public class AtualizarFolderCommandHandler : CommandHandler,
          IRequestHandler<AtualizarFolderCommand, bool>
  {
    private readonly IFolderRepository _folderRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarFolderCommandHandler(IFolderRepository folderRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _folderRepository = folderRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarFolderCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var folder = _folderRepository.GetFolder(message.ID);
      folder.ID = message.ID;
      folder.TITLE = message.TITLE;
      folder.SLUG = message.SLUG;
      folder.ICON = message.ICON;

      _folderRepository.Update(folder);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarFolderEvent(
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
