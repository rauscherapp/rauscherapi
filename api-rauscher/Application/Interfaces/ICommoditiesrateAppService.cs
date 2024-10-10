using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface ICommoditiesRateAppService : IDisposable
  {
    Task<CommoditiesRateViewModel> AtualizarCommoditiesRate(CommoditiesRateViewModel CommoditiesRateViewModel);
    Task<CommoditiesRateViewModel> CadastrarCommoditiesRate(CommoditiesRateViewModel CommoditiesRateViewModel);
    Task<bool> ExcluirCommoditiesRate(Guid CommoditiesRate);
    Task<CommoditiesRateViewModel> ObterCommoditiesRate(Guid CommoditiesRate);
    Task<PagedResponse<CommoditiesRateViewModel>> ListarCommoditiesRate(CommoditiesRateParameters parameters);
    Task<bool> RemoverCommoditiesRateAntigos();
    Task<bool> AtualizarOHLCCommoditiesRate();
  }
}
