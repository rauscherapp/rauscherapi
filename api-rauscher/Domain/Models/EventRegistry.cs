using System;

namespace Domain.Models
{
	public class EventRegistry
	{
		public EventRegistry(
		Guid eventRegistryId,
		string eventname,
		string eventdescription,
		string eventtype,
		DateTime eventdate,
		string eventlocation,
		string eventlink
		)
		{
			EventRegistryId = eventRegistryId;
			Eventname = eventname;
			Eventdescription = eventdescription;
			Eventtype = eventtype;
			Eventdate = eventdate;
			Eventlocation = eventlocation;
			Eventlink = eventlink;
		}
		public Guid EventRegistryId { get; set; }
		public string Eventname { get; set; }
		public string Eventdescription { get; set; }
		public string Eventtype { get; set; }
		public DateTime Eventdate { get; set; }
		public string Eventlocation { get; set; }
		public string Eventlink { get; set; }
	}
}
