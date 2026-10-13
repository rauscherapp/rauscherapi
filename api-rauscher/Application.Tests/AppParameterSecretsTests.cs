using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.AutoMapper;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.CommandHandlers;
using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class AppParameterSecretsTests
    {
        private static AppParameters StoredParameters(Guid id)
        {
            var parameters = new AppParameters(id, "pk_atual", "sk_atual", "whsec_atual", "price_1", 7,
                "commodities_atual", "app@rauscher.com", "senha_smtp_atual", "smtp.atual.com", 587, "contato@rauscher.com",
                "09:00", "18:00", 5, true, true, "5511999999999", "551133333333", "https://instagram.com/rauscher", "https://rauscher.com");
            parameters.YahooFinanceApiKey = "yahoo_atual";
            return parameters;
        }

        private static AppParametersAppService CreateService(IMediator mediator)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<DomainToViewModelMappingProfile>()).CreateMapper();
            return new AppParametersAppService(NullLogger<AppParametersAppService>.Instance, mediator, mapper, Mock.Of<IUriAppService>());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("sk_live_123", AppParameterSecrets.Mask)]
        public void MaskValue_MasksOnlyFilledValues(string value, string expected)
        {
            Assert.Equal(expected, AppParameterSecrets.MaskValue(value));
        }

        [Theory]
        [InlineData(AppParameterSecrets.Mask, "atual")]
        [InlineData("", "atual")]
        [InlineData(null, "atual")]
        [InlineData("nova", "nova")]
        public void Resolve_KeepsCurrentValueUnlessANewOneIsSent(string incoming, string expected)
        {
            Assert.Equal(expected, AppParameterSecrets.Resolve(incoming, "atual"));
        }

        [Fact]
        public async Task ObterAppParameters_ReturnsSecretsMaskedAndPublicFieldsIntact()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<ObterAppParametersQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(StoredParameters(Guid.NewGuid()));

            var result = await CreateService(mediator.Object).ObterAppParameters();

            Assert.Equal(AppParameterSecrets.Mask, result.StripeApiSecret);
            Assert.Equal(AppParameterSecrets.Mask, result.StripeWebhookSecret);
            Assert.Equal(AppParameterSecrets.Mask, result.StripeApiClientKey);
            Assert.Equal(AppParameterSecrets.Mask, result.CommoditiesApiKey);
            Assert.Equal(AppParameterSecrets.Mask, result.YahooFinanceApiKey);
            Assert.Equal(AppParameterSecrets.Mask, result.EmailPassword);
            Assert.Equal("5511999999999", result.WhatsAppNumber);
            Assert.Equal("551133333333", result.ContactNumber);
            Assert.Equal("https://instagram.com/rauscher", result.InstagramUrl);
            Assert.Equal("https://rauscher.com", result.WebSiteUrl);
            Assert.Equal("smtp.atual.com", result.SmtpServer);
        }

        [Fact]
        public async Task ListarAppParameters_ReturnsSecretsMasked()
        {
            var mediator = new Mock<IMediator>();
            var page = new PagedList<AppParameters>(new List<AppParameters> { StoredParameters(Guid.NewGuid()) }, 1, 0, 10);
            mediator.Setup(m => m.Send(It.IsAny<ListarAppParametersQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(page);

            var result = await CreateService(mediator.Object).ListarAppParameters(new AppParametersParameters { PageNumber = 0, PageSize = 10 });

            var item = Assert.Single(result.Data);
            Assert.Equal(AppParameterSecrets.Mask, item.StripeApiSecret);
            Assert.Equal(AppParameterSecrets.Mask, item.EmailPassword);
            Assert.Equal("5511999999999", item.WhatsAppNumber);
        }

        [Fact]
        public async Task UpdateHandler_MaskedOrEmptySecretsKeepStoredValues()
        {
            var id = Guid.NewGuid();
            var stored = StoredParameters(id);
            var repository = new Mock<IAppParametersRepository>();
            repository.Setup(r => r.GetById(id)).Returns(stored);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(u => u.Commit()).Returns(true);
            var handler = new AtualizarAppParametersCommandHandler(repository.Object, uow.Object, Mock.Of<IMediatorHandler>(), new DomainNotificationHandler());

            // O webadmin devolve no PUT o que recebeu no GET: segredos mascarados, e ainda pode mandar vazio ou nulo
            var command = new AtualizarAppParametersCommand
            {
                Id = id,
                StripeApiClientKey = AppParameterSecrets.Mask,
                StripeApiSecret = AppParameterSecrets.Mask,
                StripeWebhookSecret = "",
                CommoditiesApiKey = null,
                YahooFinanceApiKey = AppParameterSecrets.Mask,
                EmailPassword = "senha_smtp_nova",
                SmtpServer = "smtp.novo.com",
                WhatsappNumber = "5511888888888",
            };

            var ok = await handler.Handle(command, CancellationToken.None);

            Assert.True(ok);
            Assert.Equal("pk_atual", stored.StripeApiClientKey);
            Assert.Equal("sk_atual", stored.StripeApiSecret);
            Assert.Equal("whsec_atual", stored.StripeWebhookSecret);
            Assert.Equal("commodities_atual", stored.CommoditiesApiKey);
            Assert.Equal("yahoo_atual", stored.YahooFinanceApiKey);
            Assert.Equal("senha_smtp_nova", stored.EmailPassword);
            Assert.Equal("smtp.novo.com", stored.SmtpServer);
            Assert.Equal("5511888888888", stored.WhatsappNumber);
            repository.Verify(r => r.Update(stored), Times.Once);
        }
    }
}
