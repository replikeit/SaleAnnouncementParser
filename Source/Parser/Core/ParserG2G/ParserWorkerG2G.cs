using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Parser.Core.ParserG2G
{
    class ParserWorkerG2G
    {
        public static object locker = new object();
        private static string[] lines;
        private static bool parseIsDone;
        private static int beginTasksCount = 20;
        private static int countOfParsedLines = 0;
        private static Task[] tasks;
        public static event Action OnParseOneLink;
        public static event Action ParseComplete;
        //public static List<string> linesList;

        public static int GetAnnouncementCount(IHtmlDocument document)
        {
            var item = document.QuerySelector("div.check-online span.products__amount");
            if (item == null) return 0;

            int result;
            try
            {
                result = int.Parse(item.TextContent.Replace("Results", "").Trim());
            }
            catch
            {
                return 0;
            }

            return result;
        }
            
        private static string SetSpaces(string x) => x.Replace("_", " ");

        private static async Task<IHtmlDocument> GetCefDocument(string url, int requestsCount, int seconds, int index)
        {
            string source = await CefSourceLoader.GetHtmlText(url, requestsCount, seconds, index);
            var domParser = new HtmlParser();

            return await domParser.ParseDocumentAsync(source);
        }

        private static Task RunParsePage(int taskId, int lineIndex)
        {
            return Task.Run(async () =>
            {
                if (!Program.ParserActive) return;
               
                var items = lines[lineIndex].Split(' ');
                var client = new HttpClient();
                var response = await client.GetAsync("https://www.g2g.com/" + items[3]);

                string source = null;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                }

                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(source);
                var count = GetAnnouncementCount(document);

                if (count != 0)
                {
                    if (count > 20)
                    {
                        int requestsCount = count / 20;

                        while (true)
                        {
                            int index = -1;

                            while (!CefSourceLoader.FreeBrowsers.TryDequeue(out index))
                                await Task.Delay(100);

                            if (index != -1)
                            {
                                document = await GetCefDocument(items[3], requestsCount, count, index);
                                break;
                            }
                        }
                    }
                    new ParserGamesG2G().Parse(document, SetSpaces(items[0]), SetSpaces(items[1]), SetSpaces(items[2]), count);

                }

                lock (locker)
                    CreateNewTask(taskId,countOfParsedLines++ + beginTasksCount);

                OnParseOneLink?.Invoke();
                //linesList.Remove(lines[lineIndex]);
            });
        }

        private static void CreateNewTask(int taskId, int index)
        {
            if (index < lines.Length)
                tasks[taskId] = RunParsePage(taskId, index);
            else if (countOfParsedLines == lines.Length)
                parseIsDone = true;
        }

        private static Task WaitForParseAllLines() =>
            Task.Run(async () =>
            {
                while (true)
                {
                    if (parseIsDone) return;
                    await Task.Delay(1000);
                }
            });

        public static int asd1 = 0;
        public static int asd = 0;

        public static void Start() => Task.Run(() => { Worker();});

        private static async void Worker()
        {
            CefSourceLoader.BrowsersInit();
            var filesArr = Directory.GetFiles(@"Games Struct");

            var linesList = new List<string>();

            foreach (var fileName in filesArr)
            {

                using (StreamReader sr = new StreamReader(fileName))
                    lines = sr.ReadToEnd().Split('\n').Select((item) => item.Replace("\r", "")).Where(x => x != "")
                        .ToArray(); 

                foreach (var line in lines)
                {
                    linesList.Add(line);
                }

            }

            lines = linesList.ToArray();

            countOfParsedLines = 0;
            parseIsDone = false;

            var taskCount = (lines.Length > beginTasksCount) ? beginTasksCount : lines.Length;
            tasks = Enumerable.Range(0, taskCount).Select(RunParsePage).ToArray();

            await WaitForParseAllLines();
            if (Program.ParserActive) ParseComplete?.Invoke();
        }


    }

  
}
