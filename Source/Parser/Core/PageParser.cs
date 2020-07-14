using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Parser.Core
{
    class PageParser 
    {
        public static void Parse(IHtmlDocument document)
        {

            var hItems = document.QuerySelectorAll("h1");
            if (hItems.Length == 0)
                return;

            string fullHead = hItems[0].TextContent;
            var catName = document.QuerySelectorAll("a.active div.inside div.counter-param");

            if (catName.Length == 0)
                return;

            string category = catName[0].TextContent;
            string fullName = hItems[0].TextContent.Replace(category + " ", "");
            string serv = "Any";
            if (fullName.Contains(")"))
            {
                var tmpArr = fullName.Split('(');
                fullName = tmpArr[0].Substring(0, tmpArr[0].Length - 1);
                serv = tmpArr[1];
                serv = serv.Replace(")","");
            }

            Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>> globalDictionary =
                new Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>>();

            var divItems = document.QuerySelectorAll("div[class = 'form-group']");
               // Where(item => item.ClassName != null && item.ClassName.Contains("form-group"));

            Dictionary<string, List<Dictionary<string, string[]>>> dic =
                new Dictionary<string, List<Dictionary<string, string[]>>>();
            foreach (var val in divItems)
            {
                var tmpArr = val.Children[0].Children.Where(x => x.LocalName == "option").ToArray();
                if (tmpArr.Length != 0)
                {
                    Dictionary<string, string[]> tmpDic = new Dictionary<string, string[]>();
                    string underCategory = tmpArr[0].TextContent;
                    tmpDic.Add(underCategory, new string[tmpArr.Length - 1]);
                    for (int i = 1; i < tmpArr.Length; i++)
                    {
                        tmpDic[underCategory][i - 1] = tmpArr[i].TextContent;
                    }

                    if (dic.ContainsKey(serv))
                    {
                        dic[serv].Add(tmpDic);
                    }
                    else
                    {
                        dic.Add(serv, new List<Dictionary<string, string[]>>(){tmpDic});
                    }
                }
            }

            globalDictionary.Add(category,dic);
            if (Form1.GameCategoriesDictionary.ContainsKey(fullName))
            {
                if (Form1.GameCategoriesDictionary[fullName].ContainsKey(category))
                {
                    if (dic.ContainsKey(serv))
                    {
                        if (Form1.GameCategoriesDictionary[fullName][category].ContainsKey(serv))
                        {
                            return;
                        }
                        Form1.GameCategoriesDictionary[fullName][category].Add(serv, dic[serv]);
                    }
                    else
                    {
                        Form1.GameCategoriesDictionary[fullName][category].Add(serv, new List<Dictionary<string, string[]>>(){new Dictionary<string, string[]>()});
                    }
                }
                else
                {
                    Form1.GameCategoriesDictionary[fullName].Add(category, dic);
                }
            }
            else
            {
                Form1.GameCategoriesDictionary.Add(fullName, globalDictionary);
            }
        }

    }
}
