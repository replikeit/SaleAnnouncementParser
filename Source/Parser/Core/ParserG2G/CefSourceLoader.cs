using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Internals;
using CefSharp.OffScreen;

namespace Parser.Core.ParserG2G
{
    class CefSourceLoader
    {
            
        public static ChromiumWebBrowser[] BrowsersPull = null;
        public static ConcurrentQueue<int> FreeBrowsers = null;
        private static int size = 10;

        public static void BrowsersInit()
        {
            FreeBrowsers = new ConcurrentQueue<int>();

            BrowsersPull = new ChromiumWebBrowser[size];
            for (int i = 0; i < size; i++)
            {
                BrowsersPull[i] = new ChromiumWebBrowser();
                FreeBrowsers.Enqueue(i);
            }
        } 

        public static async Task<string> GetHtmlText(string url, int requestCount, int blocksCount, int index)
        {
            if (blocksCount > 300)
            {
                blocksCount = 300;
                requestCount = 300 / 20;
            }

            var chromium = BrowsersPull[index];

            await LoadPageAsync(chromium, url, requestCount, blocksCount, "https://www.g2g.com/" + url);

            var source = await chromium.GetSourceAsync();

            FreeBrowsers.Enqueue(index);

            return source;

        }

        public static Task LoadPageAsync(IWebBrowser browser, string url, int requestCount, int blocksCount, string address)
        {
            var tcs = new TaskCompletionSource<bool>();
            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = async (sender, args) =>
            {
                if (!args.IsLoading)
                { browser.ExecuteScriptAsync
                    (
                        @"
                        for(let i = 0; i <" + requestCount + @"; i++)
                        {
                            pListing.loadMore.listings('https://www.g2g.com/" + url + @"?sorting=price%40asc', '.adv_search_select_option');
                        }
                        "
                    );

                    int checker = 0;
                    int matchesCount = 0;
                    int startIndex = 0;
                    while (matchesCount < blocksCount)
                    {
                        Thread.Sleep(1000);

                        string page = await browser.GetSourceAsync();
                        if (!String.IsNullOrEmpty(page))
                        {
                            var maches = new Regex("<li id=\"data_").Matches(page, startIndex + 10);

                            if (maches.Count != 0)
                            {
                                matchesCount += maches.Count;
                                startIndex = maches[maches.Count - 1].Index;
                                checker = 0;
                            }
                            else
                            {
                                checker++;
                            }

                            if (checker == 2)
                                browser.ExecuteScriptAsync
                                (@"
                                    for(let i = 0; i < " + 5 + @"; i++)
                                    {
                                        pListing.loadMore.listings('https://www.g2g.com/" + url + @"?sorting=price%40asc', '.adv_search_select_option');
                                    }
                                ");
                            if (checker == 4)
                                break;
                        }
                    }
                    browser.LoadingStateChanged -= handler;
                    tcs.TrySetResultAsync(true);
                }
            };
            browser.LoadingStateChanged += handler;
            if (!string.IsNullOrEmpty(address))
            {
                browser.Load(address);
            }
            return tcs.Task;
        }
    }
}
