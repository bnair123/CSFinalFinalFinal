using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealWorldIntFinal
{
    public class Controller
    {

        public UserManager userManager;
        StockSymbolManager stockManager = new StockSymbolManager();

        public Controller()
        {
            userManager = new UserManager();
        }

        //Fetches the list of US Stocks to XML File
        public async Task<List<String>> getStockDetails(string apiKey)
        {
            List<String> usStockSymbols = await stockManager.getUSStockSymbols(apiKey);
            loadSymbolsToXml(usStockSymbols);
            return usStockSymbols;
        }

        // This function creates a new user with the provided username and password.
        public void handleUserCreation(string username, string password)
        {
            try
            {
                userManager.createUser(username, password);
            }
            catch
            {
                throw new Exception("Error in handleuserCreation");
            }

        }

        // This function attempts to log in a user with the provided username and password.
        public User handleUserLogin(string username, string password)
        {
            try
            {
                return userManager.login(username, password);
            }
            catch
            {
                throw new Exception("Error in handleUserLogin");
            }
        }

        // This function saves a list of US stock symbols to an XML file
        public void loadSymbolsToXml(List<String> usStockSymbols)
        {
            if (!File.Exists(Constants.StockFile))
            {
                XElement xmlElement = new XElement("Symbols", usStockSymbols.Select(i => new XElement("stocksymbol", i)));
                xmlElement.Save(Constants.StockFile);
            }
            else
            {
                File.Delete(Constants.StockFile);
                XElement xmlElement = new XElement("Symbols", usStockSymbols.Select(i => new XElement("stocksymbol", i)));
                xmlElement.Save(Constants.StockFile);
            }
        }
    }
}

