using Application.Interfaces;
using Application.Services;
using CrossCutting.Bus;
using Data.Context;
using Data.EventSourcing;
using Data.Repository.EventSourcing;
using Data.UoW;
using Domain.Core.Bus;
using Domain.Core.Events;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddScoped<IUriAppService, UriAppService>();

            // Domain - Commands

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Data
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();
            services.AddScoped<IUser, User>();
            //Infra - Fil

        }
    }
}
