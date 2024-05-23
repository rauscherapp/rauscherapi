using System;

namespace Domain.Models
{
	public class EventRegistry
	{
		public EventRegistry(
		Guid eventRegistryId,
		string eventName,
		string eventDescription,
		string eventType,
		DateTime eventDate,
		string eventLocation,
		string eventLink
		)
		{
			EventRegistryId = eventRegistryId;
			EventName = eventName;
			EventDescription = eventDescription;
			EventType = eventType;
			EventDate = eventDate;
			EventLocation = eventLocation;
			EventLink = eventLink;
		}
		public Guid EventRegistryId { get; set; }
		public string EventName { get; set; }
    public string EventDescription { get; set; }
		public string EventType { get; set; }
		public DateTime EventDate { get; set; }
		public string EventLocation { get; set; }
		public string EventLink { get; set; }
	}
}