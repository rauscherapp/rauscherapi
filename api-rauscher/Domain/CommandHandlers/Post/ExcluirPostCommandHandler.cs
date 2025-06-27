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
  public class ExcluirPostCommandHandler : CommandHandler,
          IRequestHandler<ExcluirPostCommand, bool>
  {
    private readonly IPostRepository _postRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirPostCommandHandler(IPostRepository postRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _postRepository = postRepository;
      Bus = bus;
    }
    public Task<bool> Handle(ExcluirPostCommand message, CancellationToken cancellationToken)
    {
      _postRepository.Remove(message.ID);

      if (Commit())
      {
        Bus.RaiseEvent(new ExcluirPostEvent(
message.ID,
message.TITLE,
message.CREATEDDATE,
message.CONTENT,
message.AUTHOR,
message.VISIBLE,
message.PUBLISHEDAT,
message.Folderid
));
      }
      return Task.FromResult(true);
    }

  }
}
