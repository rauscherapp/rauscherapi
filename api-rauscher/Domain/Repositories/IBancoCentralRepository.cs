using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
  public interface IBancoCentralRepository
  {
    Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync(string date);
  }
}
