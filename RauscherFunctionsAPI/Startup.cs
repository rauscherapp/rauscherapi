using APIs.Security.JWT;
using Aplication.Provider;
using Application.Interfaces;
using Application.Services;
using CrossCutting.Bus;
using Data.BancoCentral.Api.Options;
using Data.Commoditites.Api.Options;
using Data.YahooFinanceApi.Api.Options;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Options;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RauscherFunctionsAPI;
using RauscherFunctionsAPI.Configurations;
using Serilog;
using Stripe;
using StripeApi.Options;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(MyFunctionApp.Startup))]

namespace MyFunctionApp
{
  public class Startup : FunctionsStartup
  {
    public IConfiguration Configuration { get; set; }
    public override void Configure(IFunctionsHostBuilder builder)
    {

      builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
      Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Information()
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .CreateLogger();

      builder.Services.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddSerilog(dispose: true);
      });

      builder.Services.AddCors(options =>
      {
        options.AddPolicy("AllowSpecificOrigins",
            builder =>
            {
              builder.WithOrigins(
                "http://localhost:4200",
                "https://delightful-mushroom-0d4bdaa0f.5.azurestaticapps.net",
                "http://delightful-mushroom-0d4bdaa0f.5.azurestaticapps.net"
                )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
            });
      });

      // Set up configuration
      var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddEnvironmentVariables();

      Configuration = configuration.Build();

      // ASP.NET Core dependencies
      builder.Services.AddSingleton<ILoggerFactoryWrapper, LoggerFactoryWrapper>();
      builder.Services.AddScoped<IActionContextAccessor, ActionContextAccessor>();

      builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

      builder.Services.AddSingleton<IMediatorHandler, InMemoryBus>();
      builder.Services.AddTransient<IEventBusRabbitMQ, EventBusRabbitMQ>();
      builder.Services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
      builder.Services.AddTransient<IAppParametersOptionsProvider, AppParametersOptionsProvider>();

      builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
      builder.Services.AddDatabaseSetup(Configuration);

      builder.Services.AddAutoMapperSetup();

      builder.Services.AddSingleton<IUrlHelper>(x =>
      {
        var actionContextAccessor = x.GetRequiredService<IActionContextAccessor>();
        var factory = x.GetRequiredService<IUrlHelperFactory>();

        return actionContextAccessor.ActionContext != null
            ? factory.GetUrlHelper(actionContextAccessor.ActionContext)
            : new NoOpUrlHelper();  // Implement a default NoOpUrlHelper
      });

      builder.Services.AddScoped<IUriAppService, UriAppService>();
      builder.Services.AddDependencyInjectionSetup(Configuration);
      builder.Services.Configure<CommoditiesApiOptions>(Configuration.GetSection("CommoditiesApi"));
      builder.Services.Configure<StripeApiOptions>(Configuration.GetSection("StripeApi"));
      builder.Services.Configure<BancoCentralOptions>(Configuration.GetSection("BancoCentralApi"));
      builder.Services.Configure<YahooFinanceOptions>(Configuration.GetSection("YahooFinanceApi"));
      builder.Services.Configure<ParametersOptions>(Configuration.GetSection("ParametersOptions"));

      builder.Services.AddHttpClient<Data.BancoCentral.Api.Infrastructure.BancoCentralAPI>();

      builder.Services.AddHttpClient<Data.YahooFinanceApi.Api.Infrastructure.YahooFinanceAPI>();


      builder.Services.AddHttpClient<Data.Commodities.Api.Infrastructure.CommoditiesAPI>();
      // Authorization
      //services.AddOAuth2Configuration();
      // Creating a mock instance of TokenConfigurations
      TokenConfigurations tokenConfigurations = new TokenConfigurations
      {
        Audience = "rauscher-idei",
        Issuer = "RauscherApp",
        Seconds = 3600, // 1 hour in seconds
      };

      builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

      tokenConfigurations.GenerateSecretJwtKey();

      builder.Services.AddJwtSecurity(tokenConfigurations);

      builder.Services.AddTransient<IAppParametersOptionsProvider, AppParametersOptionsProvider>();

      builder.Services.AddTransient<IConfigureOptions<ParametersOptions>, ConfigureParametersOptions>();

    }
  }
}