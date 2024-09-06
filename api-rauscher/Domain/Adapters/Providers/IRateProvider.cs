using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters.Providers
{
  public interface IRateProvider
  {
    Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols);
    Task<IEnumerable<CommoditiesRate>> GetRateAsync(List<Symbols> symbols);
  }
}
