using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RealWorldIntFinal
{
    public class StockSymbolsResponse
    {
        [JsonPropertyName("data")]
        public List<StockSymbol> Data { get; set; }
    }
}
