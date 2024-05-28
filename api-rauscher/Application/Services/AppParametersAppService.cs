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
	public class AppParametersAppService : IAppParametersAppService
	{
		private IEnumerable<AppParametersViewModel> _dataResponse;
		private AppParametersViewModel _response;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger<AppParametersAppService> _logger;
		private readonly IUriAppService _uriAppService;
		
		public AppParametersAppService(ILogger<AppParametersAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
		{
			_mediator = mediator;
			_mapper = mapper;
			_logger = logger;
			_response = new AppParametersViewModel();
			_uriAppService = uriAppService;
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		public async Task<AppParametersViewModel> AtualizarAppParameters(AppParametersViewModel AppParametersViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(AtualizarAppParameters));
			var command = _mapper.Map<AtualizarAppParametersCommand>(AppParametersViewModel);
			await _mediator.Send(command);
			return AppParametersViewModel;
		}
		public async Task<AppParametersViewModel> CadastrarAppParameters(AppParametersViewModel AppParametersViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(CadastrarAppParameters));
			var command = _mapper.Map<CadastrarAppParametersCommand>(AppParametersViewModel);
			await _mediator.Send(command);
			return AppParametersViewModel;
		}
		public async Task<bool> ExcluirAppParameters(Guid appParametersId)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ExcluirAppParameters));
			var command = new ExcluirAppParametersCommand(appParametersId);
			await _mediator.Send(command);
			return true;
		}
		public async Task<PagedResponse<AppParametersViewModel>> ListarAppParameters(AppParametersParameters parameters)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ListarAppParameters));
			var data = await _mediator.Send(new ListarAppParametersQuery(parameters));
			var resultadoDB = data.Select(x => _mapper.Map<AppParameters, AppParametersViewModel>(x));
			
			var viewModelPagedList = PagedList<AppParametersViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);
			
			return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
		}
		public async Task<AppParametersViewModel> ObterAppParameters(Guid appParametersId)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ObterAppParameters));
			var data = await _mediator.Send(new ObterAppParametersQuery(appParametersId));
			var resultadoDB = _mapper.Map<AppParameters, AppParametersViewModel>(data);
			if (resultadoDB == null) return resultadoDB;
			
			return resultadoDB;
		}
	}
}
