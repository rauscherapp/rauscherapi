using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class EventRegistryCommand : Command
	{
		public Guid EventRegistryId { get; set; }
		public string Eventname { get; set; }
		public string Eventdescription { get; set; }
		public string Eventtype { get; set; }
		public DateTime Eventdate { get; set; }
		public string Eventlocation { get; set; }
		public string Eventlink { get; set; }
	}
}
