using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealWorldIntFinal
{
    public class StockSymbolManager
    {
        private readonly HttpClient _httpClient;
        public StockSymbolManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<string>> getUSStockSymbols(string apiKey)
        {
            string jsonResponse = await fetchStockSymbols(apiKey);
            StockSymbolsResponse response = JsonSerializer.Deserialize<StockSymbolsResponse>(jsonResponse);

            List<string> usStockSymbols = new List<string>();
            foreach (StockSymbol symbol in response.Data)
            {
                usStockSymbols.Add(symbol.Symbol);
            }
            return usStockSymbols;
        }


        private async Task<string> fetchStockSymbols(string apiKey)
        {
            string url = $"https://api.twelvedata.com/stocks?country=United%20States&apikey={apiKey}";
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            else
            {
                throw new Exception("Failed to retrieve stock symbols");
            }
        }
    }
}
