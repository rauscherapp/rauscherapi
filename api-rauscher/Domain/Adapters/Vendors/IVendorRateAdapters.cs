using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Adapters.Vendors
{
  public interface IVendorRateAdapters
  {
    Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols);
    Task<IEnumerable<CommoditiesRate>> GetRateAsync(List<Symbols> symbols);
    string GetVendorAdapter();
  }
}
