using System;

namespace Domain.Models
{
	public class CommoditiesRate
	{
		public CommoditiesRate(
		int? timestamp,
    string baseCurrency,
		DateTime? date,
		string code,
		string unit,
		decimal price,
		decimal? variationprice,
		decimal? variationpricepercent,
		Boolean isup
		)
		{
			Id = Guid.NewGuid();
			Timestamp = timestamp;
			BaseCurrency = baseCurrency;
			Date = date;
			Code = code;
			Unit = unit;
			Price = price;
			Variationprice = variationprice;
			Variationpricepercent = variationpricepercent;
			Isup = isup;
		}

    public void CalculateVariation(decimal lastPrice)
    {
        Variationprice = Price - lastPrice;
        if (lastPrice != 0)
        {
          Variationpricepercent = (Variationprice.Value / lastPrice) * 100;
          Isup = Variationprice > 0;
        }
        else
        {
          Variationpricepercent = null;
        }
    }
    public void SetZerosVariation()
    {
			Variationprice = 0M;
			Variationpricepercent = 0M;
    }
    public Guid Id { get; set; }
		public int? Timestamp { get; set; }
		public string BaseCurrency { get; set; }
		public DateTime? Date { get; set; }
		public string Code { get; set; }
		public string Unit { get; set; }
		public decimal Price { get; set; }
		public decimal? Variationprice { get; set; }
		public decimal? Variationpricepercent { get; set; }
		public Boolean Isup { get; set; }
	}
}
