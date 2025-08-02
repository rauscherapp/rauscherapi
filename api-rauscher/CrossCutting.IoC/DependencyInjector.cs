using Application.Interfaces;
using Application.Services;
using Data.BancoCentral.Api.Infrastructure;
using Data.BancoCentral.Api.Interfaces;
using Data.Commodities.Api.Infrastructure;
using Data.Commodities.Api.Interfaces;
using Data.Repository;
using Data.Stripe.Api.Service;
using Data.UoW;
using Data.YahooFinanceApi.Api.Infrastructure;
using Data.YahooFinanceApi.Api.Interfaces;
using Domain.Adapters.Providers;
using Domain.Adapters.Vendors;
using Domain.CommandHandlers;
using Domain.CommandHandlers.Apicredentials;
using Domain.Commands;
using Domain.Commands.Apicredentials;
using Domain.Interfaces;
using Domain.Models;
using Domain.Queries;
using Domain.QueryHandlers;
using Domain.QueryParameters;
using Domain.Repositories;
using Infrastructure.BancoCentral;
using Infrastructure.Commodities;
using Infrastructure.RateProvider.Providers;
using Infrastructure.YahooFinance;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Storage.Blobs;
using StripeApi.Service;
using System.Linq;

namespace CrossCutting.IoC
{
  public static class DependencyInjector
  {
    public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
    {

      services.AddSingleton(sp =>
          new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));

      //Services
      services.AddTransient<IAboutUsAppService, AboutUsAppService>();
      services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IEventRegistryAppService, EventRegistryAppService>();
      services.AddTransient<IAppParametersAppService, AppParametersAppService>();
      services.AddTransient<ICommoditiesRateAppService, CommoditiesRateAppService>();
      services.AddTransient<ISymbolsAppService, SymbolsAppService>();
      //services.AddTransient<IApiCredentialsAppService, ApicredentialsAppService>();
      services.AddTransient<IPostAppService, PostAppService>();
      services.AddTransient<IFolderAppService, FolderAppService>();
      services.AddTransient<IEmailService, EmailSenderAppService>();
      services.AddTransient<IStripeCustomerService, StripeCustomerService>();
      services.AddTransient<IStripeSessionService, StripeSessionService>();
      services.AddTransient<IStripeSubscriptionService, StripeSubscriptionService>();


      //Commands
      services.AddTransient<IRequestHandler<SendEmailCommand, bool>, SendEmailCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirEventRegistryCommand, bool>, ExcluirEventRegistryCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarEventRegistryCommand, bool>, CadastrarEventRegistryCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarEventRegistryCommand, bool>, AtualizarEventRegistryCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirAppParametersCommand, bool>, ExcluirAppParametersCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarAppParametersCommand, bool>, CadastrarAppParametersCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarAppParametersCommand, bool>, AtualizarAppParametersCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirCommoditiesRateCommand, bool>, ExcluirCommoditiesRateCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirCommoditiesRateAntigosCommand, bool>, ExcluirCommoditiesRateAntigosCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarCommoditiesRateCommand, bool>, CadastrarCommoditiesRateCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarCommoditiesRateCommand, bool>, AtualizarCommoditiesRateCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirSymbolsCommand, bool>, ExcluirSymbolsCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarSymbolsCommand, bool>, CadastrarSymbolsCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarSymbolsCommand, bool>, AtualizarSymbolsCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarTabelaSymbolsAPICommand, bool>, AtualizarTabelaSymbolsAPICommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirApicredentialsCommand, bool>, ExcluirApicredentialsCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarApicredentialsCommand, bool>, CadastrarApicredentialsCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarApicredentialsCommand, bool>, AtualizarApicredentialsCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirPostCommand, bool>, ExcluirPostCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarPostCommand, bool>, CadastrarPostCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarPostCommand, bool>, AtualizarPostCommandHandler>();
      services.AddTransient<IRequestHandler<ExcluirFolderCommand, bool>, ExcluirFolderCommandHandler>();
      services.AddTransient<IRequestHandler<CadastrarFolderCommand, bool>, CadastrarFolderCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarFolderCommand, bool>, AtualizarFolderCommandHandler>();
      services.AddTransient<IRequestHandler<GerarSecretAndApiKeyCommand, bool>, GerarSecretAndApiKeyCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarAboutUsCommand, bool>, AtualizarAboutUsCommandHandler>();
      services.AddTransient<IRequestHandler<AtualizarOHLCCommoditiesRateCommand, bool>, AtualizarOHLCCommoditiesRateCommandHandler>();
      services.AddTransient<IRequestHandler<UploadPostImageCommand, bool>, UploadPostImageCommandHandler>();

      //Queries
      services.AddTransient<IRequestHandler<ListarSymbolsWithRateQuery, PagedList<Symbols>>, ListarSymbolsWithRateQueryHandler>();
      services.AddTransient<IRequestHandler<ObterEventRegistryQuery, EventRegistry>, ObterEventRegistryQueryHandler>();
      services.AddTransient<IRequestHandler<ListarEventRegistryQuery, PagedList<EventRegistry>>, ListarEventRegistryQueryHandler>();
      services.AddTransient<IRequestHandler<ListarEventRegistryAppQuery, PagedList<EventRegistry>>, ListarEventRegistryAppQueryHandler>();
      services.AddTransient<IRequestHandler<ObterAppParametersQuery, AppParameters>, ObterAppParametersQueryHandler>();
      services.AddTransient<IRequestHandler<ListarAppParametersQuery, PagedList<AppParameters>>, ListarAppParametersQueryHandler>();
      services.AddTransient<IRequestHandler<ObterCommoditiesRateQuery, CommoditiesRate>, ObterCommoditiesRateQueryHandler>();
      services.AddTransient<IRequestHandler<ListarCommoditiesRateQuery, PagedList<CommoditiesRate>>, ListarCommoditiesRateQueryHandler>();
      services.AddTransient<IRequestHandler<ObterSymbolsQuery, Symbols>, ObterSymbolsQueryHandler>();
      services.AddTransient<IRequestHandler<ListarSymbolsQuery, IQueryable<Symbols>>, ListarSymbolsQueryHandler>();
      services.AddTransient<IRequestHandler<ObterApiCredentialsQuery, ApiCredentials>, ObterApiCredentialsQueryHandler>();
      services.AddTransient<IRequestHandler<ListarApiCredentialsQuery, PagedList<ApiCredentials>>, ListarApiCredentialsQueryHandler>();
      services.AddTransient<IRequestHandler<ObterPostQuery, Post>, ObterPostQueryHandler>();
      services.AddTransient<IRequestHandler<ListarPostQuery, IQueryable<Post>>, ListarPostQueryHandler>();
      services.AddTransient<IRequestHandler<ListarFolderQuery, PagedList<Folder>>, ListarFolderQueryHandler>();
      services.AddTransient<IRequestHandler<ObterAboutUsQuery, AboutUs>, ObterAboutUsQueryHandler>();

      services.AddTransient<IRateProvider, RateProvider>();
      services.AddTransient<IVendorRateAdapters, RatesAdapter>();
      services.AddTransient<IVendorRateAdapters, YahooRatesAdapter>();
      services.AddTransient<IVendorRateAdapters, CommodititesRatesAdapter>();
      services.AddTransient<ITradeReadRepository, Data.BancoCentral.Api.Service.TradeReadRepository>();
      services.AddTransient<ITradeReadRepository, Data.YahooFinanceApi.Api.Service.TradeReadRepository>();
      services.AddTransient<ITradeReadRepository, Data.Commodities.Api.Service.TradeReadRepository>();

      services.AddTransient<IBancoCentralAPI, BancoCentralAPI>();
      services.AddTransient<IYahooFinanceAPI, YahooFinanceAPI>();
      services.AddTransient<ICommoditiesAPI, CommoditiesAPI>();
      services.AddTransient<ICommodityOpenHighLowCloseRepository, CommodityOpenHighLowCloseRepository>();
      services.AddTransient<IRequestHandler<CadastrarCommoditiesRateCommand, bool>, CadastrarCommoditiesRateCommandHandler>();

      //Repository
      services.AddTransient<IAppParametersRepository, AppParametersRepository>();
      services.AddTransient<IEventRegistryRepository, EventRegistryRepository>();
      services.AddTransient<ICommoditiesRateRepository, CommoditiesRateRepository>();
      services.AddTransient<ISymbolsRepository, SymbolsRepository>();
      services.AddTransient<IApiCredentialsRepository, ApiCredentialsRepository>();
      services.AddTransient<IPostRepository, PostRepository>();
      services.AddTransient<IFolderRepository, FolderRepository>();
      services.AddTransient<IAboutUsRepository, AboutUsRepository>();
      services.AddTransient<IUnitOfWork, UnitOfWork>();

    }
  }
}
