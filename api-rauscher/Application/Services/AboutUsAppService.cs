using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AboutUsAppService : IAboutUsAppService
  {
    private IEnumerable<AboutUsViewModel> _dataResponse;
    private AboutUsViewModel _response;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<AboutUsAppService> _logger;
    private readonly IUriAppService _uriAppService;

    public AboutUsAppService(ILogger<AboutUsAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
    {
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
      _response = new AboutUsViewModel();
      _uriAppService = uriAppService;
    }
    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
    public async Task<AboutUsViewModel> AtualizarAboutUs(AboutUsViewModel aboutUsViewModel)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(AtualizarAboutUs));
      var command = _mapper.Map<AtualizarAboutUsCommand>(aboutUsViewModel);
      await _mediator.Send(command);
      return aboutUsViewModel;
    }
    public async Task<AboutUsViewModel> ObterAboutUs()
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ObterAboutUs));
      var data = await _mediator.Send(new ObterAboutUsQuery());
      var resultadoDB = _mapper.Map<AboutUs, AboutUsViewModel>(data);
      if (resultadoDB == null) return resultadoDB;

      return resultadoDB;
    }
  }
}
