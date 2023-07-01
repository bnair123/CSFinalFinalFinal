using System.Text.Json.Serialization;

namespace RealWorldIntFinal
{
    public class StockPrice
    {
        [JsonPropertyName("price")]
        public string Price { get; set; }
    }
}

