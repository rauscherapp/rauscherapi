using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using System;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Applications Starting Up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);

            host.ConfigureLogging(
            loggingBuilder =>
            {
                var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

                var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Name", "NomeDaAPI")
                .Enrich.WithProcessName()
                .Enrich.WithThreadName()
                .Enrich.WithClientAgent()
                .Enrich.WithClientIp()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithDemystifiedStackTraces()
                .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
                {
                    clientConfiguration.DeliveryMode = RabbitMQDeliveryMode.Durable;
                    clientConfiguration.Port = 5672;
                    clientConfiguration.Username = configuration["Logging:RabbitMQ:UserName"];
                    clientConfiguration.Password = configuration["Logging:RabbitMQ:Password"];
                    clientConfiguration.Exchange = configuration["Logging:RabbitMQ:Exchange"];
                    clientConfiguration.ExchangeType = configuration["Logging:RabbitMQ:ExchangeType"];
                    clientConfiguration.VHost = configuration["Logging:RabbitMQ:VirtualHost"];
                    clientConfiguration.RouteKey = configuration["Logging:RabbitMQ:RouteKey"];
                    clientConfiguration.Hostnames.Add(configuration["Logging:RabbitMQ:HostName"]);
                    sinkConfiguration.TextFormatter = new JsonFormatter();
                    sinkConfiguration.RestrictedToMinimumLevel = Serilog.Events.LogEventLevel.Information;
                })
                .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

                loggingBuilder.AddSerilog(logger, dispose: true);
            });

            host.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseIISIntegration();
                webBuilder.UseStartup<Startup>();
            });

            return host;
        }
    }
}
