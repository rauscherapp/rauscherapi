using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class DeletePostImageCommandHandler : CommandHandler,
          IRequestHandler<DeletePostImageCommand, bool>
  {
    private readonly IPostRepository _postRepository;
    private readonly IAzureBlobService _blobService;
    private readonly IMediatorHandler Bus;

    public DeletePostImageCommandHandler(IPostRepository postRepository,
                                         IAzureBlobService blobService,
                                         IUnitOfWork uow,
                                         IMediatorHandler bus,
                                         INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _postRepository = postRepository;
      _blobService = blobService;
      Bus = bus;
    }

    public async Task<bool> Handle(DeletePostImageCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return false;
      }

      var post = _postRepository.GetById(message.PostId);
      if (post == null)
      {
        Bus.RaiseEvent(new DomainNotification(nameof(DeletePostImageCommand), "Post not found."));
        return false;
      }

      if (!string.IsNullOrEmpty(post.ImgUrl))
      {
        await _blobService.DeleteBlobAsync(post.ImgUrl);
        post.SetImageUrl(null);
        _postRepository.Update(post);
      }

      return Commit();
    }
  }
}
