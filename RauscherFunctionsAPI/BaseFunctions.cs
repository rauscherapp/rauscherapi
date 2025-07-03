using Application.Helpers;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{

  public class BaseFunctions
  {
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;

    public BaseFunctions(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
    {
      _notifications = (DomainNotificationHandler)notifications; ;
      _mediator = mediator;
    }

    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotificationMessages();
    protected IEnumerable<string> GetNotificationMessages()
    {
      return _notifications.GetNotificationMessages().Select(n => n.Value);
    }

    protected bool IsValidOperation()
    {
      return !_notifications.HasNotifications();
    }

    protected IActionResult CreateResponse(object result = null)
    {
      if (IsValidOperation())
      {
        return new OkObjectResult(new
        {
          success = true,
            data = result ?? new { }
        });
      }

      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }
    protected IActionResult CreateResponseList(PaginationMetadata paginationResult = null, object result = null)
    {
      if (IsValidOperation())
      {
        return new OkObjectResult(new
        {
          success = true,
          data = result,
          pagination = paginationResult
        });
      }

      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }
  }
}
