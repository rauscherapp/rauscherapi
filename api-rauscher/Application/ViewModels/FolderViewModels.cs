using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class FolderViewModel
	{
		[JsonPropertyName("id")]
		public Guid id { get; set; }
		[JsonPropertyName("title")]
		public string title { get; set; }
		[JsonPropertyName("slug")]
		public string slug { get; set; }
		[JsonPropertyName("icon")]
		public string icon { get; set; }
	}
}
