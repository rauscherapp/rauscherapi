using System;

namespace Domain.Models
{
	public class EventRegistry
	{
		public EventRegistry(
		Guid id,
		string eventName,
		string eventDescription,
		string eventType,
		DateTime eventDate,
		string eventLocation,
		string eventLink
		)
		{
			Id = id;
			EventName = eventName;
			EventDescription = eventDescription;
			EventType = eventType;
			EventDate = eventDate;
			EventLocation = eventLocation;
			EventLink = eventLink;
		}
		public Guid Id { get; set; }
		public string EventName { get; set; }
    public string EventDescription { get; set; }
		public string EventType { get; set; }
		public DateTime EventDate { get; set; }
		public string EventLocation { get; set; }
		public string EventLink { get; set; }
	}
}
