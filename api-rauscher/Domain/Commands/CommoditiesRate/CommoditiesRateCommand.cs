using System;
using Domain.Core.Commands;

namespace Domain.Commands
{
	public abstract class CommoditiesRateCommand : Command
	{
		public Guid Id { get; set; }
		public long Timestamp { get; set; }
		public string Base { get; set; }
		public DateTime? Date { get; set; }
		public string Code { get; set; }
		public string Unit { get; set; }
		public decimal Price { get; set; }
		public decimal? Variationprice { get; set; }
		public decimal? Variationpricepercent { get; set; }
		public Boolean Isup { get; set; }
	}
}
