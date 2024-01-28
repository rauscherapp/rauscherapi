using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class PostViewModel
	{
		[JsonPropertyName("id")]
		public Guid id { get; set; }
		[JsonPropertyName("title")]
		public string title { get; set; }
		[JsonPropertyName("createdate")]
		public DateTime createdate { get; set; }
		[JsonPropertyName("content")]
		public string content { get; set; }
		[JsonPropertyName("author")]
		public string author { get; set; }
		[JsonPropertyName("visible")]
		public Boolean? visible { get; set; }
		[JsonPropertyName("publishedat")]
		public DateTime? publishedat { get; set; }
		[JsonPropertyName("folderid")]
		public Guid folderid { get; set; }
	}
}
