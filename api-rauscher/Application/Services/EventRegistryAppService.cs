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
  public class EventRegistryAppService : IEventRegistryAppService
  {
    private IEnumerable<EventRegistryViewModel> _dataResponse;
    private EventRegistryViewModel _response;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<EventRegistryAppService> _logger;
    private readonly IUriAppService _uriAppService;

    public EventRegistryAppService(ILogger<EventRegistryAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
    {
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
      _response = new EventRegistryViewModel();
      _uriAppService = uriAppService;
    }
    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
    public async Task<EventRegistryViewModel> AtualizarEventRegistry(EventRegistryViewModel EventRegistryViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(AtualizarEventRegistry));
      var command = _mapper.Map<AtualizarEventRegistryCommand>(EventRegistryViewModel);
      await _mediator.Send(command);
      return EventRegistryViewModel;
    }
    public async Task<EventRegistryViewModel> CadastrarEventRegistry(EventRegistryViewModel EventRegistryViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(CadastrarEventRegistry));
      var command = _mapper.Map<CadastrarEventRegistryCommand>(EventRegistryViewModel);
      await _mediator.Send(command);
      return EventRegistryViewModel;
    }
    public async Task<bool> ExcluirEventRegistry(Guid eventRegistryId)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ExcluirEventRegistry));
      var command = new ExcluirEventRegistryCommand(eventRegistryId);
      await _mediator.Send(command);
      return true;
    }
    public async Task<PagedResponse<EventRegistryViewModel>> ListarEventRegistry(EventRegistryParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarEventRegistry));
      var data = await _mediator.Send(new ListarEventRegistryQuery(parameters));
      var resultadoDB = data.Select(x => _mapper.Map<EventRegistry, EventRegistryViewModel>(x));

      var viewModelPagedList = PagedList<EventRegistryViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);

      return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
    }
    public async Task<PagedResponse<EventRegistryViewModel>> ListarEventRegistryApp(EventRegistryParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarEventRegistryApp));
      var data = await _mediator.Send(new ListarEventRegistryAppQuery(parameters));
      var resultadoDB = data.Select(x => _mapper.Map<EventRegistry, EventRegistryViewModel>(x));

      var viewModelPagedList = PagedList<EventRegistryViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);

      return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
    }
    public async Task<EventRegistryViewModel> ObterEventRegistry(Guid eventRegistryId)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ObterEventRegistry));
      var data = await _mediator.Send(new ObterEventRegistryQuery(eventRegistryId));
      var resultadoDB = _mapper.Map<EventRegistry, EventRegistryViewModel>(data);
      if (resultadoDB == null) return resultadoDB;

      return resultadoDB;
    }
  }
}
