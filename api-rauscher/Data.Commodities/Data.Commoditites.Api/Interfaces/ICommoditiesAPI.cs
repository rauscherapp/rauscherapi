using Data.Commodities.Api.Model;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Commodities.Api.Interfaces
{
  public interface ICommoditiesAPI
  {
    Task<ApiResponseWrapper> GetExchangeRateAsync(string symbols, string baseCurrency = "USD");
    Task<OpenHighLowCloseResponse> GetOHLCAsync(string symbol);
    Task<Dictionary<string, string>> GetSymbolsAsync();
  }
}
