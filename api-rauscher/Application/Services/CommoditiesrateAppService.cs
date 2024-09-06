using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
  public class CommoditiesRateAppService : ICommoditiesRateAppService
  {
    private IEnumerable<CommoditiesRateViewModel> _dataResponse;
    private CommoditiesRateViewModel _response;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CommoditiesRateAppService> _logger;
    private readonly IUriAppService _uriAppService;

    public CommoditiesRateAppService(ILogger<CommoditiesRateAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
    {
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
      _response = new CommoditiesRateViewModel();
      _uriAppService = uriAppService;
    }
    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }


    public async Task<bool> AtualizarOHLCCommoditiesRate()
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(AtualizarCommoditiesRate));
      var command = new AtualizarOHLCCommoditiesRateCommand();
      await _mediator.Send(command);
      return true;
    }
    public async Task<CommoditiesRateViewModel> AtualizarCommoditiesRate(CommoditiesRateViewModel CommoditiesRateViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(AtualizarCommoditiesRate));
      var command = _mapper.Map<AtualizarCommoditiesRateCommand>(CommoditiesRateViewModel);
      await _mediator.Send(command);
      return CommoditiesRateViewModel;
    }
    public async Task<CommoditiesRateViewModel> CadastrarCommoditiesRate(CommoditiesRateViewModel CommoditiesRateViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(CadastrarCommoditiesRate));
      var command = _mapper.Map<CadastrarCommoditiesRateCommand>(CommoditiesRateViewModel);
      await _mediator.Send(command);
      return CommoditiesRateViewModel;
    }
    public async Task<bool> RemoverCommoditiesRateAntigos()
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ExcluirCommoditiesRate));
      var command = new ExcluirCommoditiesRateAntigosCommand();
      await _mediator.Send(command);
      return true;
    }
    public async Task<bool> ExcluirCommoditiesRate(Guid CommoditiesRate)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ExcluirCommoditiesRate));
      var command = new ExcluirCommoditiesRateCommand(CommoditiesRate);
      await _mediator.Send(command);
      return true;
    }
    public async Task<PagedResponse<CommoditiesRateViewModel>> ListarCommoditiesRate(CommoditiesRateParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarCommoditiesRate));
      var data = await _mediator.Send(new ListarCommoditiesRateQuery(parameters));
      var resultadoDB = data.Select(x => _mapper.Map<CommoditiesRate, CommoditiesRateViewModel>(x));

      var viewModelPagedList = PagedList<CommoditiesRateViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);

      return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
    }
    public async Task<CommoditiesRateViewModel> ObterCommoditiesRate(Guid CommoditiesRate)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ObterCommoditiesRate));
      var data = await _mediator.Send(new ObterCommoditiesRateQuery(CommoditiesRate));
      var resultadoDB = _mapper.Map<CommoditiesRate, CommoditiesRateViewModel>(data);
      if (resultadoDB == null) return resultadoDB;

      return resultadoDB;
    }
  }
}
