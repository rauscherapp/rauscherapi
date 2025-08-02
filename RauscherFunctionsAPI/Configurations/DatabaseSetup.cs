using APIs.Security.JWT;
using Application.Interfaces;
using Data.Context;
using Domain.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RauscherFunctionsAPI.Configurations
{
  public static class DatabaseSetup
  {
    public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
      if (services == null)
        throw new ArgumentNullException(nameof(services));

      if (configuration == null)
        throw new ArgumentNullException(nameof(configuration));

      // Obtém a string de conexão do arquivo de configuração
      var connectionString = configuration["DefaultConnection"];

      if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException("The database connection string 'DefaultConnection' is not configured.");

      // Registrar os contextos de banco de dados no container de dependências
      services.AddDbContext<EventStoreSQLContext>(options =>
          options.UseNpgsql(connectionString));

      services.AddDbContext<RauscherDbContext>(options =>
          options.UseNpgsql(connectionString));

      services.AddDbContext<ApiSecurityDbContext>(options =>
          options.UseNpgsql(connectionString));
    }
  }
}
