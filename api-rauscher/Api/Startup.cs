using Api.Configurations;
using APIs.Security.JWT;
using Data.Commodities.Api.Service;
using Data.Commoditites.Api.Options;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;
using System;
using System.Configuration;

namespace Api
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", true, true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHealthChecks();

      services.AddApplicationInsightsTelemetry(Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"));

      services.AddCors();

      // WebAPI Config
      services.AddApiConfiguration();

      // AutoMapper Settings
      services.AddAutoMapperSetup();

      // Authorization
      //services.AddOAuth2Configuration();
      // Creating a mock instance of TokenConfigurations
      TokenConfigurations tokenConfigurations = new TokenConfigurations
      {
        Audience = "rauscher-idei",
        Issuer = "RauscherApp",
        Seconds = 3600, // 1 hour in seconds
      };

      services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

      tokenConfigurations.GenerateSecretJwtKey();

      services.AddJwtSecurity(tokenConfigurations);

      // Setting DBContexts
      services.AddDatabaseSetup(Configuration);

      // WebAPI Config
      services.AddControllers();

      // Swagger Config
      services.AddSwaggerConfig();

      // Adding MediatR for Domain Events and Notifications
      services.AddMediatR(typeof(Startup));

      // ASP.NET HttpContext dependency
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      //// .NET Native DI Abstraction
      services.AddDependencyInjectionSetup();

      services.AddMvc(setupAction =>
      {
        setupAction.Filters.Add(
                  new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
        setupAction.Filters.Add(
                  new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
        setupAction.Filters.Add(
                  new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
        setupAction.Filters.Add(
                  new ProducesDefaultResponseTypeAttribute());
        setupAction.Filters.Add(
                  new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
        setupAction.Filters.Add(
                  new AuthorizeFilter());

        setupAction.ReturnHttpNotAcceptable = true;

      });

      services.Configure<CommoditiesApiOptions>(Configuration.GetSection("CommoditiesApi"));
      services.AddHttpClient<CommoditiesRepository>()
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
    }


    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler(appBuilder =>
        {
          appBuilder.Run(async context =>
                  {
              context.Response.StatusCode = 500;
              await context.Response.WriteAsync("Ocorreu um erro inesperado. Tente novamente mais tarde.");
            });
        });

        app.UseHsts();
      }

      app.UseCors(c =>
      {
        c.AllowAnyHeader();
        c.WithExposedHeaders("X-Pagination");
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
        c.WithOrigins("https://tshp.stocklerltda.com.br", "http://localhost:4200");
      });

      app.UseAuthentication();


      app.UseSwagger();

      app.UseHttpsRedirection();

      app.UseSwaggerUI(setupAction =>
      {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
          setupAction.SwaggerEndpoint(
                    $"/swagger/APIOpenApiSpecification{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
        }

        setupAction.RoutePrefix = "";
        setupAction.DefaultModelExpandDepth(2);
        setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        setupAction.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        setupAction.EnableDeepLinking();
        setupAction.DisplayOperationId();
      });

      app.UseRouting();

      app.UseMiddleware<ApiKeyMiddleware>();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseHealthChecks("/health");

      app.UseSerilogRequestLogging();
    }
  }
}
