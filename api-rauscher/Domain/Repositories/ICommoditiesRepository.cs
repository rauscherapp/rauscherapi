using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
  public interface ICommoditiesApi  
  {
    Task<IEnumerable<Symbols>> GetSymbolsAsync();
    Task<IEnumerable<CommoditiesRate>> GetLatestCommodityRatesAsync(string baseCurrency, IEnumerable<string> symbols);
    Task<CommodityOpenHighLowClose> GetOHLCAsync(string symbol);
  }
}
