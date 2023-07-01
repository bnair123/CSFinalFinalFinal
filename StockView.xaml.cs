using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace RealWorldIntFinal
{
    public partial class StockView : Window
    {
        public User UserData { get; set; }

        StockDictionary stockDictionary = new StockDictionary();
        public StockView(User userData)
        {
            InitializeComponent();
            this.UserData = userData;
            gbxAddStock.Visibility = Visibility.Hidden;
            StockView1.Visibility = Visibility.Visible;
            loadUserStocks();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            StockPriceManager stockPriceManager = new StockPriceManager();
            bool flag = false;

            XmlDocument symbolDocs = new XmlDocument();
            symbolDocs.Load(Constants.StockFile);
            XmlNodeList dataNodes = symbolDocs.SelectNodes("//Symbols");
            foreach (XmlNode node in dataNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (txtSymbolSearch.Text == childNode.InnerText)
                    {
                        flag = true;
                        gbxAddStock.Visibility = Visibility.Visible;
                        lblStockSymbols.Content = childNode.InnerText;
                        lblStockPrice.Content = await stockPriceManager.getPriceAsync(childNode.InnerText, Constants.ApiKey);
                        break;
                    }
                }
            }
            //Failed to find stocks
            if (!flag)
            {
                MessageBox.Show("No such stock exists on US market ", "ERROR: 404", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Add stocks if clicked
            stockDictionary.addOrUpdateStock(lblStockSymbols.Content.ToString(), decimal.Parse(lblStockPrice.Content.ToString()), UserData.Username);
            loadUserStocks();
        }

        public void loadUserStocks()
        {
            try
            {
                // Clear the existing items
                gridStock.Items.Clear();

                // Load the stocks from the XML file
                List<Stock> userStocks = stockDictionary.loadFromXml(UserData.UserStockFile);

                // Add each stock to the DataGrid
                foreach (Stock stock in userStocks)
                {
                    gridStock.Items.Add(stock);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Wrong Username or Password");
            }
        }

        private void exitPressed(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TheTruth(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int i = r.Next(0, 6);
            List<string> list = quotes();
            MessageBox.Show(list[i], "Get helped", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //returns a list full of truth at each index
        //Inspired me not to drop out from this university on multiple occasions


        private List<String> quotes()
        {
            List<String> result = new List<String>();
            result.Add("User input is the worst thing that can happen to us ~DZA02 ");
            result.Add("The User is your worst enemy, you ask it to enter a number and it enters screw you pink umbrellas 42 ~DZA02");
            result.Add("it's stud not STD, You are computer science students you don't care about STDs ~DZA02");
            result.Add("I do not remember how this language works but I'll teach it to you ~DZA02");
            result.Add("How does this program work? I don't know go figure it out");
            result.Add("If you could list the most boring topics in C++ you are gauranteed to have lambda captures in there," +
                "So today I'll be talking about lambda captures ~DZA02 at meeting cpp");

            return result;
        }
    }
}
