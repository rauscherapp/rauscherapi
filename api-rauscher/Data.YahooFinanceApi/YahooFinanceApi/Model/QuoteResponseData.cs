using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.YahooFinanceApi.Api.Model
{
  public class QuoteResponseData
  {
    [JsonProperty("result")]
    public List<Result> Results { get; set; }

    [JsonProperty("error")]
    public object Error { get; set; }
  }
}
