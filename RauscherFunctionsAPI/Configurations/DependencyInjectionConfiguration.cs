﻿using CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RauscherFunctionsAPI.Configurations
{
  public static class DependencyInjectionSetup
  {
    public static void AddDependencyInjectionSetup(this IServiceCollection services)
    {
      if (services == null) throw new ArgumentNullException(nameof(services));

      DependencyInjector.RegisterDependencies(services);
    }
  }
}
