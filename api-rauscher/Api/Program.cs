using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
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
      host.UseSerilog();
      host.ConfigureLogging(
      loggingBuilder =>
      {
        var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

        var logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithProperty("Name", "RauscherApplication")
              .Enrich.WithProcessName()
              .Enrich.WithThreadName()
              .Enrich.WithClientAgent()
              .Enrich.WithClientIp()
              .Enrich.WithEnvironmentUserName()
              .Enrich.WithExceptionDetails()
              .Enrich.WithDemystifiedStackTraces()
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
