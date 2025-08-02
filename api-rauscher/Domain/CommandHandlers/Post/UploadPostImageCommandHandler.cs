using Azure.Storage.Blobs;
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
    private readonly BlobServiceClient _blobServiceClient;

    public UploadPostImageCommandHandler(IPostRepository postRepository,
                                         BlobServiceClient blobServiceClient,
                                         IUnitOfWork uow,
                                         IMediatorHandler bus,
                                         INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _postRepository = postRepository;
      _blobServiceClient = blobServiceClient;
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

      var containerClient = _blobServiceClient.GetBlobContainerClient("posts");
      await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

      var fileName = $"{Guid.NewGuid()}{Path.GetExtension(message.File.FileName)}";
      var blobClient = containerClient.GetBlobClient(fileName);
      await blobClient.UploadAsync(message.File.OpenReadStream(), cancellationToken);

      post.SetImageUrl(blobClient.Uri.AbsoluteUri);
      _postRepository.Update(post);

      return Commit();
    }
  }
}
