using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
  public class SymbolsAppService : ISymbolsAppService
  {
    private IEnumerable<SymbolsViewModel> _dataResponse;
    private SymbolsViewModel _response;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<SymbolsAppService> _logger;
    private readonly IUriAppService _uriAppService;

    public SymbolsAppService(ILogger<SymbolsAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
    {
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
      _response = new SymbolsViewModel();
      _uriAppService = uriAppService;
    }
    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
    public async Task<SymbolsViewModel> AtualizarSymbols(SymbolsViewModel SymbolsViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(AtualizarSymbols));
      var command = _mapper.Map<AtualizarSymbolsCommand>(SymbolsViewModel);
      await _mediator.Send(command);
      return SymbolsViewModel;
    }
    public async Task<SymbolsViewModel> AtualizarSymbolsApi(SymbolsViewModel SymbolsViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(AtualizarSymbols));
      var command = _mapper.Map<AtualizarTabelaSymbolsAPICommand>(SymbolsViewModel);
      await _mediator.Send(command);
      return SymbolsViewModel;
    }
    public async Task<SymbolsViewModel> CadastrarSymbols(SymbolsViewModel SymbolsViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(CadastrarSymbols));
      var command = _mapper.Map<CadastrarSymbolsCommand>(SymbolsViewModel);
      await _mediator.Send(command);
      return SymbolsViewModel;
    }
    public async Task<bool> ExcluirSymbols(Guid Symbols)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ExcluirSymbols));
      var command = new ExcluirSymbolsCommand(Symbols);
      await _mediator.Send(command);
      return true;
    }
    public async Task<PagedResponse<SymbolsViewModel>> ListarSymbols(SymbolsParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarSymbols));
      var data = await _mediator.Send(new ListarSymbolsQuery(parameters));
      var resultadoDB = data.Select(x => _mapper.Map<Symbols, SymbolsViewModel>(x));

      var viewModelPagedList = PagedList<SymbolsViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);

      return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
    }
    public async Task<PagedResponse<SymbolsViewModel>> ListarSymbolsWithRate(SymbolsParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarSymbols));
      var data = await _mediator.Send(new ListarSymbolsWithRateQuery(parameters));
      var resultadoDB = data.Select(x => _mapper.Map<Symbols, SymbolsViewModel>(x));

      var viewModelPagedList = PagedList<SymbolsViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);

      return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
    }

    public async Task<IEnumerable<SymbolsViewModel>> ListarSymbolsWithRateForWorker(SymbolsParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarSymbols));
      var data = await _mediator.Send(new ListarSymbolsWithRateQuery(parameters));
      var resultadoDB = data.Select(x => _mapper.Map<Symbols, SymbolsViewModel>(x));
      return resultadoDB;
    }
    public async Task<SymbolsViewModel> ObterSymbols(Guid Symbols)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ObterSymbols));
      var data = await _mediator.Send(new ObterSymbolsQuery(Symbols));
      var resultadoDB = _mapper.Map<Symbols, SymbolsViewModel>(data);
      if (resultadoDB == null) return resultadoDB;

      return resultadoDB;
    }
  }
}
