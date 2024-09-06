using Data.Context;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class CommodityOpenHighLowCloseRepository : Repository<CommodityOpenHighLowClose>, ICommodityOpenHighLowCloseRepository
  {
    public CommodityOpenHighLowCloseRepository(RauscherDbContext context) : base(context)
    {

    }
    public async Task<CommodityOpenHighLowClose> ObterOHCLBySymbolCode(string symbolCode)
    {
      var ohlc = Db.CommodityOpenHighLowCloses
          .Where(c => c.Symbol.Equals(symbolCode));

      return await ohlc.FirstOrDefaultAsync();
    }
  }
}
