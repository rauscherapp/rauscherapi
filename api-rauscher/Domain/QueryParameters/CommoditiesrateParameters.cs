using System;
namespace Domain.QueryParameters
{
	public class CommoditiesRateParameters : QueryParameters
	{
		public Guid Id { get; set; }
		public int? Timestamp { get; set; }
		public string BaseCurrency { get; set; }
		public DateTime? Date { get; set; }
		public string Code { get; set; }
		public string Unit { get; set; }
		public decimal? Price { get; set; }
		public decimal? Variationprice { get; set; }
		public decimal? Variationpricepercent { get; set; }
		public Boolean Isup { get; set; }
	}
}
