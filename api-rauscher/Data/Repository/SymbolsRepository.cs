using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class SymbolsRepository : Repository<Symbols>, ISymbolsRepository
  {
    public SymbolsRepository(RauscherDbContext context) : base(context)
    {
    }
    public Symbols ObterSymbols(Guid id)
    {
      var Symbols = Db.Symbolss
          .Where(c => c.Id == id);

      return Symbols.FirstOrDefault();
    }
    public Symbols ObterSymbolsByCode(string code)
    {
      var Symbols = Db.Symbolss
          .Where(c => c.Code == code);

      return Symbols.FirstOrDefault();
    }


    public async Task<List<Symbols>> ObterSymbolsAppVisible()
    {
      var Symbols = Db.Symbolss
          .Where(c => c.Appvisible == true);

      return await Symbols.ToListAsync();
    }

    public async Task<PagedList<Symbols>> ListarSymbolss(SymbolsParameters parameters)
    {
      var symbols = Db.Symbolss
      .AsQueryable();

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        symbols = symbols.ApplySort(parameters.OrderBy);

      return PagedList<Symbols>.Create(symbols, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<PagedList<Symbols>> GetSymbolsWithLatestRatesAsync(SymbolsParameters parameters)
    {
      var symbolsQuery = Db.Symbolss
          .Include(s => s.CommoditiesRates)
          .Where(query => query.Appvisible)
          .AsQueryable();

      // Apply sorting
      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
      {
        symbolsQuery = symbolsQuery.ApplySort(parameters.OrderBy);
      }

      // Apply pagination
      return PagedList<Symbols>.Create(symbolsQuery, parameters.PageNumber, parameters.PageSize);
    }
  }
}
