﻿using APIs.Security.JWT;
using Application.Interfaces;
using Data.Context;
using Domain.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Worker.Configurations
{
  public static class DatabaseSetup
  {
    public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
      if (services == null) throw new ArgumentNullException(nameof(services));

      services.AddDbContext<EventStoreSQLContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

      services.AddDbContext<RauscherDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
      services.AddDbContext<ApiSecurityDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
  }
}
