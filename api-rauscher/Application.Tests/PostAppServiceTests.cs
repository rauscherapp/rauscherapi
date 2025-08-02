using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class PostAppServiceTests
    {
        [Fact]
        public async Task UploadPostImage_Returns_PostWithUpdatedImageUrl()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var logger = new Mock<ILogger<PostAppService>>();
            var uriApp = new Mock<IUriAppService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();

            var service = new PostAppService(logger.Object, mediator.Object, mapper, uriApp.Object);

            var postId = Guid.NewGuid();
            var post = new Post(postId, "title", DateTime.UtcNow, "content", "author", true, Guid.NewGuid(), "en");
            post.SetImageUrl("uploads/posts/image.jpg");

            mediator.Setup(m => m.Send(It.IsAny<UploadPostImageCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);
            mediator.Setup(m => m.Send(It.IsAny<ObterPostQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(post);

            var bytes = new byte[] { 1, 2, 3 };
            using var stream = new MemoryStream(bytes);
            var formFile = new FormFile(stream, 0, bytes.Length, "image", "image.jpg");

            // Act
            var result = await service.UploadPostImage(postId, formFile);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.ImgUrl, result.ImgUrl);
        }
    }
}
