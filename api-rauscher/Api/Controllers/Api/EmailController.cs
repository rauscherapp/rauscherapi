using Application.Interfaces;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [Produces("application/json", "application/xml")]
  [Consumes("application/json", "application/xml")]
  [AllowAnonymous]
  public class EmailController : ApiController
  {
    private readonly IEmailService _emailService;
    private readonly IMediatorHandler _bus;

    public EmailController(
        IEmailService emailService,
        IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
    {
      _emailService = emailService;
      _bus = bus;
    }

    [HttpPost("SendEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendEmail([FromBody] AppEmailParameters emailRequest)
    {
      if (!ModelState.IsValid)
      {
        NotifyModelStateErrors();
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }

      try
      {
        await _emailService.SendEmailAsync(emailRequest);
        return Ok(new { success = true, message = "Email sent successfully." });
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
    }
  }
}