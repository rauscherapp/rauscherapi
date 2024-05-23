using System;
using Domain.Core.Events;

namespace Domain.Events
{
	    public class AtualizarEventRegistryEvent : Event
	{
		public AtualizarEventRegistryEvent(
		Guid id,
		string eventname,
		string eventdescription,
		string eventtype,
		DateTime eventdate,
		string eventlocation,
		string eventlink
		)
		{
			Id = id;
			Eventname = eventname;
			Eventdescription = eventdescription;
			Eventtype = eventtype;
			Eventdate = eventdate;
			Eventlocation = eventlocation;
			Eventlink = eventlink;
		}
		public Guid Id { get; set; }
		public string Eventname { get; set; }
		public string Eventdescription { get; set; }
		public string Eventtype { get; set; }
		public DateTime Eventdate { get; set; }
		public string Eventlocation { get; set; }
		public string Eventlink { get; set; }
	}
}
