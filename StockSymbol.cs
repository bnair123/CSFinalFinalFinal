using System.Text.Json.Serialization;

namespace RealWorldIntFinal
{
    public class StockSymbol
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
