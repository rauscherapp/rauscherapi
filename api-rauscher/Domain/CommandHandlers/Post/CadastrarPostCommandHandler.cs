using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class CadastrarPostCommandHandler : CommandHandler,
          IRequestHandler<CadastrarPostCommand, bool>
  {
    private readonly IPostRepository _postRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarPostCommandHandler(IPostRepository postRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _postRepository = postRepository;
      Bus = bus;
    }
    public Task<bool> Handle(CadastrarPostCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var post = new Post(
        message.TITLE,
        message.CREATEDATE,
        message.CONTENT,
        message.AUTHOR,
        message.VISIBLE,
        message.Folderid
        );

      _postRepository.Add(post);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarPostEvent(
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
