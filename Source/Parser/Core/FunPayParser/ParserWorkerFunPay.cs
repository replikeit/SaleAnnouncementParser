using System;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;

namespace Parser.Core.FunPayParser
{
    class ParserWorkerFunPay
    {
        private static object locker = new object();
        private IParserSettings parserSettings;
        public static event Action OnParseOneLink;
        public static event Action ParseComplete;

        private HtmlLoader loader;
        private ParseSettingsFunPay parseSettingsFunPay;

        public IParser Parser { get; set; }

        public IParserSettings ParserSettings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public ParserWorkerFunPay(IParser parser)
        {
            Parser = parser;
        }

        public ParserWorkerFunPay(IParser parser, IParserSettings parserSettings)
        :this(parser)
        {
            this.parserSettings = parserSettings;

        }

        public ParserWorkerFunPay(ParseSettingsFunPay parseSettingsFunPay)
        {
            this.parseSettingsFunPay = parseSettingsFunPay;
        }

        public void Start() => Task.Run(() => { Worker(); });
       
        private async void Worker()
        {
            loader = new HtmlLoader(parserSettings);
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!Program.ParserActive) return;
                
                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);
                Parser.Parse(document);
                lock (locker)
                    OnParseOneLink?.Invoke();
            }

            if (Program.ParserActive) ParseComplete?.Invoke();
        }
    }
}
