using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.OffScreen;
using Parser.Core.ParserG2G;

namespace Parser.Core
{
    static class Program
    {
        public static object Locker = new object();
        public static bool ParserActive = false;
        public static int CountOfParsedSites = 0;
        public static int countOfParsedLines = 0;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = @"Cache";
            Cef.Initialize(settings);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
