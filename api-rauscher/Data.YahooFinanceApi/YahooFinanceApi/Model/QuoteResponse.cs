using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.YahooFinanceApi.Api.Model
{
  public class QuoteResponse
  {
    [JsonProperty("quoteResponse")]
    public QuoteResponseData QuoteResponseData { get; set; }
  }
}
