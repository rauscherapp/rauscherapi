using Newtonsoft.Json;

namespace Data.YahooFinanceApi.Api.Model
{
  public class Result
  {
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("quoteType")]
    public string QuoteType { get; set; }

    [JsonProperty("typeDisp")]
    public string TypeDisp { get; set; }

    [JsonProperty("quoteSourceName")]
    public string QuoteSourceName { get; set; }

    [JsonProperty("triggerable")]
    public bool Triggerable { get; set; }

    [JsonProperty("customPriceAlertConfidence")]
    public string CustomPriceAlertConfidence { get; set; }

    [JsonProperty("marketState")]
    public string MarketState { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("hasPrePostMarketData")]
    public bool HasPrePostMarketData { get; set; }

    [JsonProperty("firstTradeDateMilliseconds")]
    public long FirstTradeDateMilliseconds { get; set; }

    [JsonProperty("priceHint")]
    public int PriceHint { get; set; }

    [JsonProperty("exchange")]
    public string Exchange { get; set; }

    [JsonProperty("shortName")]
    public string ShortName { get; set; }

    [JsonProperty("longName")]
    public string LongName { get; set; }

    [JsonProperty("messageBoardId")]
    public string MessageBoardId { get; set; }

    [JsonProperty("exchangeTimezoneName")]
    public string ExchangeTimezoneName { get; set; }

    [JsonProperty("exchangeTimezoneShortName")]
    public string ExchangeTimezoneShortName { get; set; }

    [JsonProperty("gmtOffSetMilliseconds")]
    public int GmtOffSetMilliseconds { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("esgPopulated")]
    public bool EsgPopulated { get; set; }

    [JsonProperty("regularMarketChangePercent")]
    public double RegularMarketChangePercent { get; set; }

    [JsonProperty("regularMarketPrice")]
    public double RegularMarketPrice { get; set; }

    [JsonProperty("regularMarketChange")]
    public double RegularMarketChange { get; set; }

    [JsonProperty("regularMarketTime")]
    public long RegularMarketTime { get; set; }

    [JsonProperty("regularMarketDayHigh")]
    public double RegularMarketDayHigh { get; set; }

    [JsonProperty("regularMarketDayRange")]
    public string RegularMarketDayRange { get; set; }

    [JsonProperty("regularMarketDayLow")]
    public double RegularMarketDayLow { get; set; }

    [JsonProperty("regularMarketVolume")]
    public long RegularMarketVolume { get; set; }

    [JsonProperty("regularMarketPreviousClose")]
    public double RegularMarketPreviousClose { get; set; }

    [JsonProperty("bid")]
    public double Bid { get; set; }

    [JsonProperty("ask")]
    public double Ask { get; set; }

    [JsonProperty("bidSize")]
    public int BidSize { get; set; }

    [JsonProperty("askSize")]
    public int AskSize { get; set; }

    [JsonProperty("fullExchangeName")]
    public string FullExchangeName { get; set; }

    [JsonProperty("regularMarketOpen")]
    public double RegularMarketOpen { get; set; }

    [JsonProperty("averageDailyVolume3Month")]
    public long AverageDailyVolume3Month { get; set; }

    [JsonProperty("averageDailyVolume10Day")]
    public long AverageDailyVolume10Day { get; set; }

    [JsonProperty("fiftyTwoWeekLowChange")]
    public double FiftyTwoWeekLowChange { get; set; }

    [JsonProperty("fiftyTwoWeekLowChangePercent")]
    public double FiftyTwoWeekLowChangePercent { get; set; }

    [JsonProperty("fiftyTwoWeekRange")]
    public string FiftyTwoWeekRange { get; set; }

    [JsonProperty("fiftyTwoWeekHighChange")]
    public double FiftyTwoWeekHighChange { get; set; }

    [JsonProperty("fiftyTwoWeekHighChangePercent")]
    public double FiftyTwoWeekHighChangePercent { get; set; }

    [JsonProperty("fiftyTwoWeekLow")]
    public double FiftyTwoWeekLow { get; set; }

    [JsonProperty("fiftyTwoWeekHigh")]
    public double FiftyTwoWeekHigh { get; set; }

    [JsonProperty("fiftyTwoWeekChangePercent")]
    public double FiftyTwoWeekChangePercent { get; set; }

    [JsonProperty("sourceInterval")]
    public int SourceInterval { get; set; }

    [JsonProperty("exchangeDataDelayedBy")]
    public int ExchangeDataDelayedBy { get; set; }

    [JsonProperty("fiftyDayAverage")]
    public double FiftyDayAverage { get; set; }

    [JsonProperty("fiftyDayAverageChange")]
    public double FiftyDayAverageChange { get; set; }

    [JsonProperty("fiftyDayAverageChangePercent")]
    public double FiftyDayAverageChangePercent { get; set; }

    [JsonProperty("twoHundredDayAverage")]
    public double TwoHundredDayAverage { get; set; }

    [JsonProperty("twoHundredDayAverageChange")]
    public double TwoHundredDayAverageChange { get; set; }

    [JsonProperty("twoHundredDayAverageChangePercent")]
    public double TwoHundredDayAverageChangePercent { get; set; }

    [JsonProperty("tradeable")]
    public bool Tradeable { get; set; }

    [JsonProperty("cryptoTradeable")]
    public bool CryptoTradeable { get; set; }
  }
}
