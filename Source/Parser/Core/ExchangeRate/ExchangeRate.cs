using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;

namespace Parser.Core.ExchangeRate
{
    static class ExchangeRate
    {
        public static double Eur { get; set; }
        public static double Rub { get; set; }

        private static async Task<double> GetRate(string url)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var client = new HttpClient();
            var response = await client.GetAsync(url);

            string source = null;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            if (source == null) return 0;

            var domParser = new HtmlParser();
            var document = await domParser.ParseDocumentAsync(source);

            string item = document.QuerySelector("div.ccOutputBx span.ccOutputRslt").TextContent.Substring(0,5);
            return double.Parse(item);
        }

        public static async Task Init()
        {
            Eur = await GetRate("https://www.x-rates.com/calculator/?from=USD&to=EUR&amount=1");
            Rub = await GetRate("https://www.x-rates.com/calculator/?from=USD&to=RUB&amount=1");
        }
    }
}
