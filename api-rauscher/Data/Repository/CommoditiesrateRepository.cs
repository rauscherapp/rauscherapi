using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class CommoditiesRateRepository : Repository<CommoditiesRate>, ICommoditiesRateRepository
  {
    public CommoditiesRateRepository(RauscherDbContext context) : base(context)
    {
    }
    public CommoditiesRate ObterCommoditiesRate(Guid id)
    {
      var CommoditiesRate = Db.CommoditiesRates
          .Where(c => c.Id == id);

      return CommoditiesRate.FirstOrDefault();
    }

    public async Task<CommoditiesRate> GetLastPriceBeforeTimestamp(string commodityCode, long? timestamp)
    {
      return await Db.CommoditiesRates
          .Where(cr => cr.SymbolCode == commodityCode && cr.Timestamp < timestamp)
          .OrderByDescending(cr => cr.Timestamp)
          .FirstOrDefaultAsync();      
    }

    public async Task<PagedList<CommoditiesRate>> ListarCommoditiesRates(CommoditiesRateParameters parameters)
    {
      var CommoditiesRate = Db.CommoditiesRates
      .AsQueryable();

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        CommoditiesRate = CommoditiesRate.ApplySort(parameters.OrderBy);

      return PagedList<CommoditiesRate>.Create(CommoditiesRate, parameters.PageNumber, parameters.PageSize);
    }

    public async Task RemoveOlderThanAsync(DateTime date)
    {
      var commoditiesToDelete = await Db.CommoditiesRates
          .Where(cr => cr.Date < date)
          .ToListAsync();

      Db.CommoditiesRates.RemoveRange(commoditiesToDelete);
    }
  }
}
