using Data.BancoCentral.Api.Model;
using Domain.Models;

namespace Data.BancoCentral.Api.Interfaces
{
  public interface IBancoCentralAPI
  {
    Task<Currencies> GetAllCurrenciesAsync();
    Task<ExchangeRate> GetExchangeRateAsync(string date);
    Task<ExchangeRates> GetExchangeRatesByPeriodAsync(string startDate, string endDate);
    Task<ExchangeRate> GetLatestExchangeRateAsync();
    Task<ExchangeRates> GetOpeningRateAsync(string date);
  }
}
