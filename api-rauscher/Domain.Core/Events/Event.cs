using MediatR;
using System;

namespace Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}