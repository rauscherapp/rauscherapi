using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface ISymbolsRepository : IRepository<Symbols>
	{
    public Task<IQueryable<Symbols>> ListarSymbolss(SymbolsParameters parameters);
		Symbols ObterSymbols(Guid id);
		Symbols ObterSymbolsByCode(string code);
		Task<List<Symbols>> ObterSymbolsAppVisible();
    Task<PagedList<Symbols>> GetSymbolsWithLatestRatesAsync(SymbolsParameters parameters);
    Task<List<Symbols>> FindAllCommodities();
    Task<List<Symbols>> FindAllExchanges();
  }
}
