using System.Text.Json.Serialization;
using System;

namespace Application.ViewModels
{
	public class ApicredentialsViewModel
	{
		[JsonPropertyName("Apikey")]
		public string Apikey { get; set; }
		[JsonPropertyName("Apisecrethash")]
		public string Apisecrethash { get; set; }
		[JsonPropertyName("Createdat")]
		public DateTime Createdat { get; set; }
		[JsonPropertyName("Lastusedat")]
		public DateTime? Lastusedat { get; set; }
		[JsonPropertyName("Isactive")]
		public Boolean Isactive { get; set; }
	}
}
