namespace RealWorldIntFinal
{
    public class Stock
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public Stock(string symbol, decimal price)
        {
            this.Symbol = symbol;
            this.Price = price;
        }
    }
}
