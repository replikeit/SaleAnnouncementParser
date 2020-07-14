using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core.FunPayParser
{
    class ParseSettingsFunPay : IParserSettings
    {
        
        public string BaseUrl { get; set; }
        public string Prefix { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }

        public ParseSettingsFunPay(string baseUrl, int startPoint, int endPoint)
        {
            BaseUrl = baseUrl + "{CurrentId}/";
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}
