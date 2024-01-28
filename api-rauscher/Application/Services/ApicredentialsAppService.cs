using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Commands.Apicredentials;
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
	public class ApicredentialsAppService : IApiCredentialsAppService
	{
		private IEnumerable<ApicredentialsViewModel> _dataResponse;
		private ApicredentialsViewModel _response;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger<ApicredentialsAppService> _logger;
		private readonly IUriAppService _uriAppService;
		
		public ApicredentialsAppService(ILogger<ApicredentialsAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
		{
			_mediator = mediator;
			_mapper = mapper;
			_logger = logger;
			_response = new ApicredentialsViewModel();
			_uriAppService = uriAppService;
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		public async Task<ApicredentialsViewModel> AtualizarApicredentials(ApicredentialsViewModel ApicredentialsViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(AtualizarApicredentials));
			var command = _mapper.Map<AtualizarApicredentialsCommand>(ApicredentialsViewModel);
			await _mediator.Send(command);
			return ApicredentialsViewModel;
		}
		public async Task<ApicredentialsViewModel> CadastrarApicredentials(ApicredentialsViewModel ApicredentialsViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(CadastrarApicredentials));
			var command = _mapper.Map<CadastrarApicredentialsCommand>(ApicredentialsViewModel);
			await _mediator.Send(command);
			return ApicredentialsViewModel;
		}
		public async Task<bool> ExcluirApicredentials(string apiKey)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ExcluirApicredentials));
			var command = new ExcluirApicredentialsCommand(apiKey);
			await _mediator.Send(command);
			return true;
		}
		public async Task<bool> GerarApiCredentials(string document)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ExcluirApicredentials));
			var command = new GerarSecretAndApiKeyCommand(document);
			await _mediator.Send(command);
			return true;
		}
		public async Task<PagedResponse<ApicredentialsViewModel>> ListarApicredentials(ApiCredentialsParameters parameters)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ListarApicredentials));
			var data = await _mediator.Send(new ListarApicredentialsQuery(parameters));
			var resultadoDB = data.Select(x => _mapper.Map<ApiCredentials, ApicredentialsViewModel>(x));
			
			var viewModelPagedList = PagedList<ApicredentialsViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);
			
			return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
		}
		public async Task<ApicredentialsViewModel> ObterApicredentials(string apiKey)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ObterApicredentials));
			var data = await _mediator.Send(new ObterApicredentialsQuery(apiKey));
			var resultadoDB = _mapper.Map<ApiCredentials, ApicredentialsViewModel>(data);
			if (resultadoDB == null) return resultadoDB;
			
			return resultadoDB;
		}
	}
}
