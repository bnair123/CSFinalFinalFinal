using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealWorldIntFinal
{
    public class StockPriceManager
    {
        private readonly HttpClient _httpClient;

        public StockPriceManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<decimal> getPriceAsync(string symbol, string apiKey)
        {
            try
            {
                string jsonResponse = await fetchStockPrice(symbol, apiKey);
                StockPrice stockPrice = JsonSerializer.Deserialize<StockPrice>(jsonResponse);
                decimal price = decimal.Parse(stockPrice.Price);
                return price;
            }
            catch
            {
                throw new JsonException("Error in getPriceAsync method");
            }
        }

        private async Task<string> fetchStockPrice(string symbol, string apiKey)
        {
            string url = $"https://api.twelvedata.com/price?symbol={symbol}&apikey={apiKey}";
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            else 
            { 
                throw new JsonException("Error in fetch stock price."); 
            }
        }
    }
}
