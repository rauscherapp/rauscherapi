using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class EventRegistryViewModel
	{
		[JsonPropertyName("EventRegistryId")]
		public Guid EventRegistryId { get; set; }
		[JsonPropertyName("EventName")]
		public string EventName { get; set; }
		[JsonPropertyName("EventDescription")]
		public string EventDescription { get; set; }
		[JsonPropertyName("EventType")]
		public string EventType { get; set; }
		[JsonPropertyName("EventDate")]
		public DateTime EventDate { get; set; }
		[JsonPropertyName("EventLocation")]
		public string EventLocation { get; set; }
		[JsonPropertyName("EventLink")]
		public string EventLink { get; set; }		
		[JsonPropertyName("EventDateMonth")]
		public string EventDateMonth { get; set; }
		[JsonPropertyName("EventDateDay")]
		public string EventDateDay { get; set; }
		[JsonPropertyName("EventDateHour")]
		public string EventDateHour { get; set; }
		[JsonPropertyName("EventDateYear")]
		public string EventDateYear { get; set; }
	}
}
