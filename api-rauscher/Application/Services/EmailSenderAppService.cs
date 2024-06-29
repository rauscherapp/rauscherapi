using Application.Interfaces;
using Domain.Options;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Domain.Interfaces;
using Application.ViewModels;
using Domain.Commands;
using MediatR;
using AutoMapper;
using Domain.QueryParameters;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Services
{
  public class EmailSenderAppService : IEmailService
  {
    private readonly ParametersOptions _parameters;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<EmailSenderAppService> _logger;

    public EmailSenderAppService(IOptionsSnapshot<ParametersOptions> parameters, IMediator mediator, IMapper mapper, ILogger<EmailSenderAppService> logger)
    {
      _parameters = parameters.Value;
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
    }
    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
    public async Task<bool> SendEmailAsync(AppEmailParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(SendEmailAsync));
      var command = _mapper.Map<SendEmailCommand>(parameters);
      await _mediator.Send(command);
      return true;
    }
  }
}
