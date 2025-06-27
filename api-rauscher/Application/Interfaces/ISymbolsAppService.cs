using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface ISymbolsAppService : IDisposable
  {
    Task<SymbolsViewModel> AtualizarSymbols(SymbolsViewModel symbolsViewModel);
    Task<SymbolsViewModel> CadastrarSymbols(SymbolsViewModel symbolsViewModel);
    Task<bool> ExcluirSymbols(Guid Symbols);
    Task<SymbolsViewModel> ObterSymbols(Guid Symbols);
    Task<PagedResponse<SymbolsViewModel>> ListarSymbols(SymbolsParameters parameters);
    Task<SymbolsViewModel> AtualizarSymbolsApi(SymbolsViewModel SymbolsViewModel);
    Task<PagedResponse<SymbolsViewModel>> ListarSymbolsWithRate(SymbolsParameters parameters);
    Task<IEnumerable<SymbolsViewModel>> ListarSymbolsWithRateForWorker(SymbolsParameters parameters);
  }
}
