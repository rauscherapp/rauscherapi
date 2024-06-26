﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotificationMessages()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotificationMessages().Count > 0;
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}