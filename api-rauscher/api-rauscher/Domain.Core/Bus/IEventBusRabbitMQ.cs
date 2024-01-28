using Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public interface IEventBusRabbitMQ
    {
        Task Publish<T>(T @event) where T : Event;
    }
}
