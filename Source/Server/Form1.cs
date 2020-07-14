using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private static Thread ServerThread;
        public static string binFileName = "data/CurrentData.bin";

        public Form1()
        {
            InitializeComponent();
        }

        private void onServerButton_Click(object sender, EventArgs e)
        {
            onServerButton.Enabled = false;
            offServerButton.Enabled = true;

            ServerThread = new Thread(ServerListener.RunServer);
            ServerThread.Start();
        }

        private void offServerButton_Click(object sender, EventArgs e)
        {
            ServerListener.Disconnect();
            onServerButton.Enabled = true;
            offServerButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ServerListener.Disconnect();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
