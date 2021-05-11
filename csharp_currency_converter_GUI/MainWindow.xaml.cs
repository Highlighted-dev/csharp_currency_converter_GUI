using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace csharp_currency_converter_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] currencies = new string[] { "AED", "AFN", "ALL", "ARS", "AWG", "AUD", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BOV", "BRL", "BSD", "BTN", "BWP", "BYN", "BZD", "CAD", 
            "CDF", "CHE", "CHF", "CHW", "CLF", "CLP", "CNY", "COP", "COU", "CRC", "CUC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "ERN", "ETB", "EUR", "FJD", "GBP", "GEL", "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", 
            "HNL", "HRK", "HTG", "HTG", "HUF", "IDR", "ILS", "INR", "IQD", "IRR", "ISK", "JMD", "JOD", "JPY","KES","KGS","KHR","KMF","KPW","KRW","KWD","KYD","KZT","LAK","LBP","LKR","LRD","LSI","LYD","MAD","MDL","MGA","MKD","MMK",
            "MNT","MOP","MRU","MUR","MVR","MWK","MXN","MXV","MYR","MZN","NAD","NGN","NIO","NOK","NPR","NZD","OMR","PAB","PEN","PGK","PHP","PKR","PLN","PYG","QAR","RON","RSD","RUB","RWD","SAR","SBD","SCR","SDG","SEK","SGD","SHP",
            "SIL","SOS","SRD","SSP","STN","SVC","SYP","SZL","THB","TJS","TMT","TND","TOP","TRY","TWD","TZS","UAH","UGX","USD","USN","UYI","UYU","UZS","VEF","VND","VUV","WST","XAF","XCD","XDR","XOF","XPF","XSU","XUA","ZAR","ZMW","ZWL"};


        public MainWindow()
        {
            InitializeComponent();
            foreach (string currency in currencies)
            {
                ComboBox_currency1.Items.Add(currency);
                ComboBox_currency2.Items.Add(currency);
            }
            ComboBox_currency1.MaxDropDownHeight = 150;
            ComboBox_currency2.MaxDropDownHeight = 150;
        }


        public async Task<double> API_CALL(double moneyToExchange, string firstCurrencyToSecondCurrency)
        {

            //GET REQUEST
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"https://free.currconv.com/api/v7/convert?q={firstCurrencyToSecondCurrency}&compact=ultra&apiKey=0041fce5c1ce7f5e5121");
            //Parsing Httpclient response to string
            string responseString = await response.Content.ReadAsStringAsync();

            return getExchangePriceFromString(responseString);
            

            
        }

        //The first parameter in json will be differenet every call, for example "USD_PLN", "EUR_USD" etc, so we can't just serialize the json to an object(or I don't know how to do this). So This method will delete everything
        //we don't need and parse it to double.
        public static double getExchangePriceFromString(string str)
        {
            str = str.Remove(0, 11);
            str = str.Remove(str.Length - 1);
            str = str.Replace(".", ",");
            return double.Parse(str);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            double moneyToExchange = double.Parse(TextBox_moneyToExchange.Text);
            string firstCurrencyToSecondCurrency = ComboBox_currency1.Text + "_" + ComboBox_currency2.Text;
            double exchangePrice = await API_CALL(moneyToExchange,firstCurrencyToSecondCurrency);
            TextBox_exchangedMoney.Text = (exchangePrice * moneyToExchange).ToString();


        }

    }

}
