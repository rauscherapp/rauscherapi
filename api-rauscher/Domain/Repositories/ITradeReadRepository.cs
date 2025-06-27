using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
  public interface ITradeReadRepository
  {
    Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync(string date);
    Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date);
    string GetRepositoryName();
  }
}
