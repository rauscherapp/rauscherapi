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
  public class ExcluirFolderCommandHandler : CommandHandler,
          IRequestHandler<ExcluirFolderCommand, bool>
  {
    private readonly IFolderRepository _folderRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirFolderCommandHandler(IFolderRepository folderRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _folderRepository = folderRepository;
      Bus = bus;
    }
    public Task<bool> Handle(ExcluirFolderCommand message, CancellationToken cancellationToken)
    {
      var folder = new Folder(message.ID, message.TITLE, message.SLUG, message.ICON);
      _folderRepository.Remove(folder.ID);

      if (Commit())
      {
        Bus.RaiseEvent(new ExcluirFolderEvent(
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
