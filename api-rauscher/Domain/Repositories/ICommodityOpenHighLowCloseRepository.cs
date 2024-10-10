using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Repositories
{
  public interface ICommodityOpenHighLowCloseRepository : IRepository<CommodityOpenHighLowClose>
  {
    Task<CommodityOpenHighLowClose> ObterOHCLBySymbolCode(string symbolCode);
  }
}
