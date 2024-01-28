using Application.Interfaces;
using Application.Services;
using CrossCutting.Bus;
using Data.Commodities.Api.Service;
using Data.Context;
using Data.EventSourcing;
using Data.Repository;
using Data.Repository.EventSourcing;
using Data.UoW;
using Domain.CommandHandlers;
using Domain.CommandHandlers.Apicredentials;
using Domain.Commands;
using Domain.Commands.Apicredentials;
using Domain.Core.Bus;
using Domain.Core.Events;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Models;
using Domain.Queries;
using Domain.QueryHandlers;
using Domain.QueryParameters;
using Domain.Repositories;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.IoC
{
  public static class NativeInjectorBootStrapper
  {
    public static void RegisterServices(IServiceCollection services)
    {
      //ASPNET
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSingleton<ILoggerFactoryWrapper, LoggerFactoryWrapper>();
      services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
      services.AddScoped<IUrlHelper>(x =>
      {
        var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
        var factory = x.GetRequiredService<IUrlHelperFactory>();
        return factory.GetUrlHelper(actionContext);
      });

      // Domain Bus (Mediator)
      services.AddScoped<IMediatorHandler, InMemoryBus>();
      services.AddScoped<IEventBusRabbitMQ, EventBusRabbitMQ>();
      services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();


      //Application 
 services.AddScoped<ICommoditiesRateAppService, CommoditiesRateAppService>(); 
      services.AddScoped<ISymbolsAppService, SymbolsAppService>();
      services.AddScoped<IApiCredentialsAppService, ApicredentialsAppService>();
      services.AddScoped<IPostAppService, PostAppService>();
      services.AddScoped<IFolderAppService, FolderAppService>();
      services.AddScoped<IUriAppService, UriAppService>();

      // Domain - Commands 
 services.AddScoped<IRequestHandler<ExcluirCommoditiesRateCommand, bool>, ExcluirCommoditiesRateCommandHandler>(); 
 services.AddScoped<IRequestHandler<CadastrarCommoditiesRateCommand, bool>, CadastrarCommoditiesRateCommandHandler>(); 
 services.AddScoped<IRequestHandler<AtualizarCommoditiesRateCommand, bool>, AtualizarCommoditiesRateCommandHandler>(); 
      services.AddScoped<IRequestHandler<ExcluirSymbolsCommand, bool>, ExcluirSymbolsCommandHandler>();
      services.AddScoped<IRequestHandler<CadastrarSymbolsCommand, bool>, CadastrarSymbolsCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarSymbolsCommand, bool>, AtualizarSymbolsCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarTabelaSymbolsAPICommand, bool>, AtualizarTabelaSymbolsAPICommandHandler>();
      services.AddScoped<IRequestHandler<ExcluirApicredentialsCommand, bool>, ExcluirApicredentialsCommandHandler>();
      services.AddScoped<IRequestHandler<CadastrarApicredentialsCommand, bool>, CadastrarApicredentialsCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarApicredentialsCommand, bool>, AtualizarApicredentialsCommandHandler>();
      services.AddScoped<IRequestHandler<ExcluirPostCommand, bool>, ExcluirPostCommandHandler>();
      services.AddScoped<IRequestHandler<CadastrarPostCommand, bool>, CadastrarPostCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarPostCommand, bool>, AtualizarPostCommandHandler>();
      services.AddScoped<IRequestHandler<ExcluirFolderCommand, bool>, ExcluirFolderCommandHandler>();
      services.AddScoped<IRequestHandler<CadastrarFolderCommand, bool>, CadastrarFolderCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarFolderCommand, bool>, AtualizarFolderCommandHandler>();
      services.AddScoped<IRequestHandler<GerarSecretAndApiKeyCommand, bool>, GerarSecretAndApiKeyCommandHandler>();

      // Domain - Queries 
 services.AddScoped<IRequestHandler<ObterCommoditiesRateQuery, CommoditiesRate>, ObterCommoditiesRateQueryHandler>(); 
 services.AddScoped<IRequestHandler<ListarCommoditiesRateQuery, PagedList<CommoditiesRate>>, ListarCommoditiesRateQueryHandler>(); 
      services.AddScoped<IRequestHandler<ObterSymbolsQuery, Symbols>, ObterSymbolsQueryHandler>();
      services.AddScoped<IRequestHandler<ListarSymbolsQuery, PagedList<Symbols>>, ListarSymbolsQueryHandler>();
      services.AddScoped<IRequestHandler<ObterApicredentialsQuery, ApiCredentials>, ObterApicredentialsQueryHandler>();
      services.AddScoped<IRequestHandler<ListarApicredentialsQuery, PagedList<ApiCredentials>>, ListarApicredentialsQueryHandler>();
      services.AddScoped<IRequestHandler<ObterPostQuery, Post>, ObterPostQueryHandler>();
      services.AddScoped<IRequestHandler<ListarPostQuery, PagedList<Post>>, ListarPostQueryHandler>();
      services.AddScoped<IRequestHandler<ListarFolderQuery, PagedList<Folder>>, ListarFolderQueryHandler>();

      // Domain - Events
      services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

      // Infra - Data 
 services.AddScoped<ICommoditiesRateRepository, CommoditiesRateRepository>(); 
      services.AddScoped<ISymbolsRepository, SymbolsRepository>();
      services.AddScoped<IApiCredentialsRepository, ApiCredentialsRepository>();
      services.AddScoped<IPostRepository, PostRepository>();
      services.AddScoped<IFolderRepository, FolderRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();


      services.AddScoped<ICommoditiesRepository, CommoditiesRepository>();
      services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
      services.AddScoped<IEventStore, SqlEventStore>();
      services.AddScoped<EventStoreSQLContext>();
      services.AddScoped<RauscherDbContext>();
      services.AddScoped<IUser, User>();
      //Infra - Fil

    }
  }
}
