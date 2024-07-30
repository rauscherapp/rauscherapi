using Application.Helpers;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
  public abstract class ApiController : ControllerBase
  {
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;

    protected ApiController(INotificationHandler<DomainNotification> notifications,
                            IMediatorHandler mediator)
    {
      _notifications = (DomainNotificationHandler)notifications;
      _mediator = mediator;
    }

    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotificationMessages();

    protected bool IsValidOperation()
    {
      return !_notifications.HasNotifications();
    }

    protected IEnumerable<string> GetNotificationMessages()
    {
      return _notifications.GetNotificationMessages().Select(n => n.Value);
    }

    protected void NotifyModelStateErrors()
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors);
      foreach (var error in errors)
      {
        var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
        NotifyError(string.Empty, errorMsg);
      }
    }

    protected void NotifyError(string code, string message)
    {
      _mediator.RaiseEvent(new DomainNotification(code, message));
    }

    protected IActionResult CreateResponse(object result = null)
    {
      if (IsValidOperation())
      {
        return Ok(new
        {
          success = true,
          data = result
        });
      }

      return BadRequest(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }
    protected IActionResult CreateResponseList(PaginationMetadata paginationResult = null, object result = null)
    {
      if (IsValidOperation())
      {
        return Ok(new
        {
          success = true,
          data = result,
          pagination = paginationResult
        });
      }

      return BadRequest(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }
  }
}
