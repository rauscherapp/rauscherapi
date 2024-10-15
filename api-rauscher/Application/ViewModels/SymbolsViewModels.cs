using System.Text.Json.Serialization;
using System;

namespace Application.ViewModels
{
	public class SymbolsViewModel
	{
		[JsonPropertyName("id")]
		public Guid id { get; set; }
		[JsonPropertyName("code")]
		public string code { get; set; }
		[JsonPropertyName("name")]
		public string name { get; set; }
		[JsonPropertyName("friendlyName")]
		public string friendlyName { get; set; }
		[JsonPropertyName("symbolType")]
		public string symbolType { get; set; }
		[JsonPropertyName("vendor")]
		public string vendor { get; set; }
		[JsonPropertyName("appvisible")]
		public Boolean appvisible { get; set; }
		[JsonPropertyName("lastRate")]
		public CommoditiesRateViewModel lastRate { get; set; }
	}
}
