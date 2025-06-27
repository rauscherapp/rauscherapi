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

    public async Task UpdateSymbolCode(Symbols symbols)
    {      
        Db.Symbolss.Update(symbols);
        await Db.SaveChangesAsync();
    }

    public async Task<List<Symbols>> FindAllCommodities()
    {
      var Symbols = Db.Symbolss
          .Where(c => c.SymbolType.ToLower().Equals("commodity"));

      return await Symbols.ToListAsync();
    }

    public async Task<List<Symbols>> FindAllExchanges()
    {
      var Symbols = Db.Symbolss
          .Where(c => c.SymbolType.ToLower().Equals("exchange"));

      return await Symbols.ToListAsync();
    }

    public Symbols ObterSymbols(Guid id)
    {
      return Db.Symbolss.FirstOrDefault(c => c.Id == id);
    }

    public Symbols ObterSymbolsByCode(string code)
    {
      return Db.Symbolss.FirstOrDefault(c => c.Code == code);
    }

    public async Task<List<Symbols>> ObterSymbolsAppVisible()
    {
      var Symbols = Db.Symbolss
          .Where(c => c.Appvisible == true);

      return await Symbols.ToListAsync();
    }

    public async Task<IQueryable<Symbols>> ListarSymbolss(SymbolsParameters parameters)
    {
      if (parameters == null) throw new ArgumentNullException(nameof(parameters));

      var symbols = Db.Symbolss
          .AsNoTracking()
          .AsQueryable();

      if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
      {
        if (!parameters.SearchQuery.Equals("null"))
        {
          var searchQuery = parameters.SearchQuery.ToLower();
          symbols = symbols.Where(s => s.Name.ToLower().Contains(searchQuery)
                                     || s.FriendlyName.ToLower().Contains(searchQuery)
                                     || s.Code.ToLower().Contains(searchQuery));
        }
      }

      if (!string.IsNullOrWhiteSpace(parameters.Name))
        symbols = symbols.Where(s => s.Name.ToLower() == parameters.Name.ToLower());

      if (!string.IsNullOrWhiteSpace(parameters.Code))
        symbols = symbols.Where(s => s.Code.ToLower() == parameters.Code.ToLower());

      if (!string.IsNullOrWhiteSpace(parameters.SymbolType))
        symbols = symbols.Where(s => s.SymbolType.ToLower() == parameters.SymbolType.ToLower());

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        symbols = symbols.ApplySort(parameters.OrderBy);

      return symbols.AsQueryable();
    }

    public async Task<PagedList<Symbols>> GetSymbolsWithLatestRatesAsync(SymbolsParameters parameters)
   {
      var symbolsQuery = Db.Symbolss
          .Include(s => s.CommoditiesRates)
          .Where(query => query.Appvisible && query.SymbolType == parameters.SymbolType)
          .AsQueryable();

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
      {
        symbolsQuery = symbolsQuery.ApplySort(parameters.OrderBy);
      }

      return PagedList<Symbols>.Create(symbolsQuery, parameters.PageNumber, parameters.PageSize);
    }
  }
}
