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
  public class AtualizarPostCommandHandler : CommandHandler,
          IRequestHandler<AtualizarPostCommand, bool>
  {
    private readonly IPostRepository _postRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarPostCommandHandler(IPostRepository postRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _postRepository = postRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarPostCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var post = _postRepository.GetById(message.ID);

      post.SetTitle(message.TITLE);
      post.SetContent(message.CONTENT);

      _postRepository.Update(post);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarPostEvent(
          message.ID,
          message.TITLE,
          message.CREATEDATE,
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
