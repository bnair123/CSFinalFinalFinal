using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace RealWorldIntFinal
{
    public class StockDictionary
    {
        Dictionary<string, decimal> stocks = new Dictionary<string, decimal>();
        private XmlDocument userStockDoc = new XmlDocument();

        public StockDictionary()
        {
        }

        public void addOrUpdateStock(string symbol, decimal price, string userName)
        {
            try
            {
                // Check if stock already exists in the dictionary
                if (!stocks.ContainsKey(symbol))
                {
                    // If not, add new stock to the dictionary
                    stocks.Add(symbol, price);
                    // And save only the new stock to the XML file
                    saveSingleStockToXml($"{userName}_stocks.xml", symbol, price);
                }
                else
                {
                    // If the stock already exists, update the price in the dictionary
                    stocks[symbol] = price;
                    // And update the price in the XML file
                    updateStockInXml($"{userName}_stocks.xml", symbol, price);
                }
            }
            catch
            {
                throw new Exception("Stock addition/update failed.");
            }
        }

        public void saveSingleStockToXml(string filePath, string symbol, decimal price)
        {
            XDocument doc;
            // If the file does not exist, create a new file
            if (!File.Exists(filePath))
            {
                doc = new XDocument();
                doc.Add(new XElement("root"));
            }
            else
            {
                // If the file exists, load it
                doc = XDocument.Load(filePath);
            }

            // Add new stock to the root
            doc.Root.Add(new XElement(symbol, price));
            // Save the document
            doc.Save(filePath);
        }

        public void updateStockInXml(string filePath, string symbol, decimal price)
        {
            if (File.Exists(filePath))
            {
                // Load the existing XML document
                XDocument doc = XDocument.Load(filePath);
                // Find the stock and update its price
                var element = doc.Root.Element(symbol);
                if (element != null)
                {
                    element.Value = price.ToString();
                }
                // Save the document
                doc.Save(filePath);
            }
        }

        public void removeStock(string symbol)
        {
            try
            {
                if (stocks.ContainsKey(symbol))
                {
                    stocks.Remove(symbol);
                }
            }
            catch
            {
                throw new Exception("stock deletion failed.");
            }
        }


        public List<Stock> loadFromXml(string filePath)
        {
            List<Stock> userStocks = new List<Stock>();
            if (File.Exists(filePath))
            {
                XDocument doc = XDocument.Load(filePath);
                foreach (XElement node in doc.Root.Elements())
                {
                    userStocks.Add(new Stock(node.Name.LocalName, decimal.Parse(node.Value)));
                }
            }
            return userStocks;
        }
    }
}

