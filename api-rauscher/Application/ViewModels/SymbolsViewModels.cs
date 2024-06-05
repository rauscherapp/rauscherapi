using System.Text.Json.Serialization;
using System;

namespace Application.ViewModels
{
	public class SymbolsViewModel
	{
		[JsonPropertyName("Id")]
		public Guid Id { get; set; }
		[JsonPropertyName("Code")]
		public string Code { get; set; }
		[JsonPropertyName("Name")]
		public string Name { get; set; }
		[JsonPropertyName("FriendlyName")]
		public string FriendlyName { get; set; }
		[JsonPropertyName("Appvisible")]
		public Boolean Appvisible { get; set; }
		[JsonPropertyName("LastRate")]
		public CommoditiesRateViewModel LastRate { get; set; }
	}
}
