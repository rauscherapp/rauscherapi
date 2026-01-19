using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
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

            var post = await _postRepository.ObterPost(message.PostId);
            if (post == null)
            {
                Bus.RaiseEvent(new DomainNotification(nameof(UploadPostImageCommand), "Post not found."));
                return false;
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient("imagens");
            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(message.File.FileName)}";
            var blobClient = containerClient.GetBlobClient(fileName);
            var headers = new BlobHttpHeaders
            {
                ContentType = message.File.ContentType // image/jpeg, image/png, etc.
            };
            await blobClient.UploadAsync(message.File.OpenReadStream(), new BlobUploadOptions
            {
                HttpHeaders = headers
            }, cancellationToken);

            post.SetImageUrl(blobClient.Uri.AbsoluteUri);
            _postRepository.Update(post);
            return Commit();
        }
    }
}
