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

    public async Task<IQueryable<Symbols>> ListarSymbolss(SymbolsParameters parameters)
   {
      // Validação básica de parâmetros
      if (parameters == null) throw new ArgumentNullException(nameof(parameters));

      var symbols = Db.Symbolss
          .AsNoTracking()
          .AsQueryable();

      // Aplica filtros conforme os parâmetros
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

      // Aplica filtros conforme os parâmetros
      if (!string.IsNullOrWhiteSpace(parameters.Name))
        symbols = symbols.Where(s => s.Name.ToLower() == parameters.Name.ToLower());

      if (!string.IsNullOrWhiteSpace(parameters.Code))
        symbols = symbols.Where(s => s.Code.ToLower() == parameters.Code.ToLower());

      if (!string.IsNullOrWhiteSpace(parameters.SymbolType))
        symbols = symbols.Where(s => s.SymbolType.ToLower() == parameters.SymbolType.ToLower());

      // Aplica ordenação, se especificada
      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        symbols = symbols.ApplySort(parameters.OrderBy);

      // Cria a lista paginada de forma assíncrona
      return symbols.AsQueryable();
    }


    public async Task<PagedList<Symbols>> GetSymbolsWithLatestRatesAsync(SymbolsParameters parameters)
    {
      var symbolsQuery = Db.Symbolss
          .Include(s => s.CommoditiesRates)
          .Where(query => query.Appvisible && query.SymbolType == parameters.SymbolType)
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
