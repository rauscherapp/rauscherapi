using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class EventRegistryViewModel
	{
		[JsonPropertyName("eventRegistryId")]
		public Guid eventRegistryId { get; set; }
		[JsonPropertyName("eventName")]
		public string eventName { get; set; }
		[JsonPropertyName("eventDescription")]
		public string eventDescription { get; set; }
		[JsonPropertyName("eventType")]
		public string eventType { get; set; }
		[JsonPropertyName("eventDate")]
		public DateTime eventDate { get; set; }
		[JsonPropertyName("eventLocation")]
		public string eventLocation { get; set; }
		[JsonPropertyName("eventLink")]
		public string eventLink { get; set; }		
		[JsonPropertyName("eventDateMonth")]
		public string eventDateMonth { get; set; }
		[JsonPropertyName("eventDateDay")]
		public string eventDateDay { get; set; }
		[JsonPropertyName("eventDateHour")]
		public string eventDateHour { get; set; }
		[JsonPropertyName("eventDateYear")]
		public string eventDateYear { get; set; }
		[JsonPropertyName("published")]
		public bool published { get; set; }
	}
}
