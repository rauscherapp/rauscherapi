using System;

namespace Domain.QueryParameters
{
	public class EventRegistryParameters : QueryParameters
	{
		public Guid Id { get; set; }
		public string Eventname { get; set; }
		public string Eventdescription { get; set; }
		public string Eventtype { get; set; }
		public DateTime Eventdate { get; set; }
		public string Eventlocation { get; set; }
		public string Eventlink { get; set; }
	}
}
