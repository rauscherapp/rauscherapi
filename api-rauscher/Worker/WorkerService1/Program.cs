using APIs.Security.JWT;
using Aplication.Provider;
using Application.Interfaces;
using Data.BancoCentral.Api.Options;
using Data.Commodities.Api.Service;
using Data.Commoditites.Api.Options;
using Data.YahooFinanceApi.Api.Options;
using Domain.Options;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Polly;
using Serilog;
using StripeApi.Options;
using Worker.Configurations;

namespace Worker
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
              var env = context.HostingEnvironment;

              config.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
              var configuration = context.Configuration;

              // Configurar serviços específicos
              services.AddLogging(loggingBuilder =>
                      loggingBuilder.AddSerilog(dispose: true));

              var tokenConfigurations = new TokenConfigurations
              {
                Audience = "rauscher-idei",
                Issuer = "RauscherApp",
                Seconds = 3600,
              };
              tokenConfigurations.GenerateSecretJwtKey();

              services.AddJwtSecurity(tokenConfigurations);
              services.AddDatabaseSetup(configuration); // Certifique-se de ter um método de extensão para isso
              services.AddMediatR(typeof(Program));
              services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
              
              services.AddScoped<IAppParametersOptionsProvider, AppParametersOptionsProvider>();
              services.AddTransient<IConfigureOptions<ParametersOptions>, ConfigureParametersOptions>();
              services.AddAutoMapperSetup();

              services.Configure<CommoditiesApiOptions>(configuration.GetSection("CommoditiesApi"));
              services.Configure<StripeApiOptions>(configuration.GetSection("StripeApi"));
              services.Configure<BancoCentralOptions>(configuration.GetSection("BancoCentralApi"));
              services.Configure<YahooFinanceOptions>(configuration.GetSection("YahooFinanceApi"));
              services.Configure<ParametersOptions>(configuration.GetSection("ParametersOptions"));
              //services.AddHttpClient<CommoditiesRepository>()
              //        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
              services.AddDependencyInjectionSetup(); // Certifique-se de ter um método de extensão para isso
              services.AddHttpClient<Data.BancoCentral.Api.Infrastructure.BancoCentralAPI>();
              
              services.AddHttpClient<Data.YahooFinanceApi.Api.Infrastructure.YahooFinanceAPI>();

              
              services.AddHttpClient<Data.Commodities.Api.Infrastructure.CommoditiesAPI>();
              services.AddSingleton<Microsoft.AspNetCore.Mvc.Routing.IUrlHelperFactory, Microsoft.AspNetCore.Mvc.Routing.UrlHelperFactory>();

              services.AddSignalR();

              services.AddHostedService<Worker>(); // Adiciona a classe Worker como um serviço hospedado
            });
  }
}
