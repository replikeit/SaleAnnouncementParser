using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using CefSharp;
using Parser.Core;
using Parser.Core.ExchangeRate;
using Parser.Core.FunPayParser;
using Parser.Core.ParserG2G;

namespace Parser
{
    public partial class Form1 : Form
    {
        private static DateTime lastStartTime;
        private static ParserWorkerFunPay goldFunPay;
        private static ParserWorkerFunPay lotsFunPay;
        public static string binFileName = "data/CurrentData.bin";
        public static Dictionary<string, Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>>>
            GameCategoriesDictionary =
                new Dictionary<string, Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>>>();
        
        public static List<GameInfo> testArr;

        public Form1()
        {
            InitializeComponent();

            ParserWorkerG2G.OnParseOneLink += SetCount;
            ParserWorkerG2G.ParseComplete += SiteIsParsed;
            ParserWorkerFunPay.OnParseOneLink += SetCount;
            ParserWorkerFunPay.ParseComplete += SiteIsParsed;

            goldFunPay = new ParserWorkerFunPay(new ParserGamesFunPay(), new ParseSettingsFunPay("https://funpay.ru/en/lots/", 1, 652));
            lotsFunPay = new ParserWorkerFunPay(new ParserGamesFunPay(), new ParseSettingsFunPay("https://funpay.ru/en/chips/", 1, 133));
            StartParse();
        }

        private void SetCount() => counterLabel.Invoke(new Action(() => counterLabel.Text = "Count of Parsed Urls: " + (++Program.countOfParsedLines).ToString()));

        private async void StartParse()
        {
            startButton.Enabled = false;

            await ExchangeRate.Init();

            lastStartTime = DateTime.Now;
            Program.ParserActive = true;
            Program.CountOfParsedSites = 0;
            Program.countOfParsedLines = 0;
            
            testArr = new List<GameInfo>();

            goldFunPay.Start();
            lotsFunPay.Start();
            ParserWorkerG2G.Start(); 
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartParse();
        }

        private void SiteIsParsed()
        {
            if (++Program.CountOfParsedSites == 3)
            {
                //File.Move(binFileName, $"data/{lastStartTime.ToString("dd.mm.yyyy_hh-mm")}.bin");

                using (FileStream fs = new FileStream(binFileName, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, testArr.ToArray());
                }

                
                Process.Start("Parser.exe");
                this.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            Program.ParserActive = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
