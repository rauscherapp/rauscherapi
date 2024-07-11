﻿using Application.AutoMapper;

namespace Worker.Configurations
{
  public static class AutoMapperSetup
  {
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
      if (services == null) throw new ArgumentNullException(nameof(services));

      services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
    }
  }
}
