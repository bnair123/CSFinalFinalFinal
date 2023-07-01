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

        public async Task<List<String>> getStockDetails(string apiKey)
        {
            List<String> usStockSymbols = await stockManager.getUSStockSymbols(apiKey);
            loadSymbolsToXml(usStockSymbols);
            return usStockSymbols;
        }


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

