using Data.YahooFinanceApi.Api.Model;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.YahooFinanceApi.Api.Interfaces
{
  public interface IYahooFinanceAPI
  {
    Task<QuoteResponse> GetExchangeRateAsync();
    Task<QuoteResponse> GetExchangeRateAsync(string code);
  }
}
