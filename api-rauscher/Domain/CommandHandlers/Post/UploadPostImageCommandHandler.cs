using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class UploadPostImageCommandHandler : CommandHandler,
          IRequestHandler<UploadPostImageCommand, bool>
  {
    private readonly IPostRepository _postRepository;
    private readonly IMediatorHandler Bus;

    public UploadPostImageCommandHandler(IPostRepository postRepository,
                                         IUnitOfWork uow,
                                         IMediatorHandler bus,
                                         INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _postRepository = postRepository;
      Bus = bus;
    }

    public async Task<bool> Handle(UploadPostImageCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return false;
      }

      var post = _postRepository.GetById(message.PostId);
      if (post == null)
      {
        Bus.RaiseEvent(new DomainNotification(nameof(UploadPostImageCommand), "Post not found."));
        return false;
      }

      var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "posts");
      if (!Directory.Exists(uploadsFolder))
      {
        Directory.CreateDirectory(uploadsFolder);
      }

      var fileName = $"{Guid.NewGuid()}{Path.GetExtension(message.File.FileName)}";
      var filePath = Path.Combine(uploadsFolder, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await message.File.CopyToAsync(stream, cancellationToken);
      }

      post.SetImageUrl(Path.Combine("uploads", "posts", fileName));
      _postRepository.Update(post);

      return Commit();
    }
  }
}
