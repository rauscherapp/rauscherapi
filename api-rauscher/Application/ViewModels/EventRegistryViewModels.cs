using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class EventRegistryViewModel
	{
		[JsonPropertyName("Id")]
		public Guid Id { get; set; }
		[JsonPropertyName("Eventname")]
		public string Eventname { get; set; }
		[JsonPropertyName("Eventdescription")]
		public string Eventdescription { get; set; }
		[JsonPropertyName("Eventtype")]
		public string Eventtype { get; set; }
		[JsonPropertyName("Eventdate")]
		public DateTime Eventdate { get; set; }
		[JsonPropertyName("Eventlocation")]
		public string Eventlocation { get; set; }
		[JsonPropertyName("Eventlink")]
		public string Eventlink { get; set; }
	}
}
