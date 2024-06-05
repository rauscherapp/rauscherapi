using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface ISymbolsRepository : IRepository<Symbols>
	{
		Task<PagedList<Symbols>> ListarSymbolss(SymbolsParameters parameters);
		Symbols ObterSymbols(Guid id);
		Symbols ObterSymbolsByCode(string code);
		Task<List<Symbols>> ObterSymbolsAppVisible();
    Task<PagedList<Symbols>> GetSymbolsWithLatestRatesAsync(SymbolsParameters parameters);

  }
}
