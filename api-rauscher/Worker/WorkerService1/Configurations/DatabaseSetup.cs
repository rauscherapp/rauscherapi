using APIs.Security.JWT;
using Data.Context;
using Microsoft.EntityFrameworkCore;

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
