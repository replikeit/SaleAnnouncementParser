using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Parser.Core.ExchangeRate;

namespace Parser.Core.FunPayParser
{
    class ParserGamesFunPay : IParser
    {
        public static object locker = new object();
        static List<int> l = new List<int>();

        public void Parse(IHtmlDocument document)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var hItems = document.QuerySelectorAll("h1");
            if (hItems.Length == 0)
                return;

            string fullHead = hItems[0].TextContent;
            var catName = document.QuerySelectorAll("a.active div.inside div.counter-param");

            if (catName.Length == 0)
                return;

            string priceTitle = document.QuerySelector("div[data-sort-field='tc-price']").TextContent.Replace(" ", "");
            var inStockItem = document.QuerySelector("div[data-sort-field='tc-amount']");
            int coef = 1;

            if (inStockItem != null)
            {
                if (priceTitle == "Price")
                {
                    coef = 1000;
                    priceTitle = "Price/1000";
                }
            }

            string category = catName[0].TextContent;
            string fullName = hItems[0].TextContent.Replace(category + " ", "");
            string serv = "Any";
            if (fullName.Contains(")"))
            {
                var tmpArr = fullName.Split('(');
                fullName = tmpArr[0].Substring(0, tmpArr[0].Length - 1);
                serv = tmpArr[1];
                serv = serv.Replace(")", "");
            }

            var aItems = document.QuerySelectorAll("a")
                .Where(item => item.ClassName != null && item.ClassName.Contains("tc-item"));

            foreach (var item in aItems)
            {
                long amount = GetCoinCount(item);
                double cost = GetCost(item, coef);
                var description = GetDescriptionContent(item);
                string servName = GetContent(item, "tc-server");
                string side = GetContent(item, "tc-side");
                string nick = GetMediaItem(item, "media-user-name");
                int rev = GetReviewsCount(item);
                string regtime = GetMediaItem(item, "media-user-info");
                bool isOnline = GetOnlineStatus(item);
                    
                lock (Program.Locker)
                {
                    var x = new GameInfo(
                        DateTime.Now,
                        DateTime.Now,
                        ((IHtmlAnchorElement) item).Href.Replace("https://", ""),
                        fullName,
                        serv,
                        category,
                        servName,
                        side,
                        amount,
                        cost,
                        description,
                        nick, 
                        rev,
                        regtime,
                        priceTitle,
                        isOnline);
                    Form1.testArr.Add(x);
                }
            }
        }

        private int GetReviewsCount(IElement item)
        {
            string revStr = GetMediaItem(item, "media-user-reviews");
            return (revStr != "no reviews")? int.Parse(string.Join(String.Empty, revStr.Where(c => char.IsDigit(c)))) : 0;
        }

        private bool GetOnlineStatus(IElement item)=>
            item.Children.FirstOrDefault(x => x.ClassName == "tc-user").QuerySelector("div[class*='media-user']").ClassName.Contains("online");

        private double GetCost(IElement item, int coef) =>
            Math.Round(double.Parse(GetContent(item, "tc-price").
                Replace(" ", "").
                Replace("\n", "").
                Replace("₽", "")) * coef / ExchangeRate.ExchangeRate.Rub, 5);

        private static string GetMediaItem(IElement item, string mediaClass)=>
            GetContent(item.QuerySelector("div[class='media-body']"), mediaClass);

        private static string GetDescriptionContent(IElement item)
        {
            string res;
            try
            {
                var item2 = item.Children.FirstOrDefault(x => x.ClassName == "tc-desc");
                if (item2 == null)
                    return "-";
                res = GetContent(item2, "tc-desc-text");
            }
            catch
            {
                res = "-";
            }
            return res;
        }

        private static long GetCoinCount(IElement item)
        {
            string str = GetContent(item, "tc-amount").Replace(" ", "");
            long amout = -1;

            if (str == "-") return amout;

            if (str.Contains("k"))
            {
                amout = long.Parse(str.Replace("k", "")) * 1000000;
            }
            else
            {
                try
                {
                    amout = long.Parse(str);
                }
                catch
                {
                    amout = -1;
                }
            }
          
            return amout;
        }

        private static string GetContent(IElement item, string classStr)
        {
            string side;
            try
            {
                var tag = item.Children.FirstOrDefault(x => x.ClassName.Contains(classStr));
                side = tag == null ? "-" : tag.TextContent;
            }
            catch
            {
                side = "-";
            }
            return side;
        }

        public void Parse(IHtmlDocument document, string gameName, string category, string serverLocation) { }
    }
}
