using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server 
{
    static class ServerListener
    {
        public static int Port { get; set; }
        public static string Address { get; set; }
        private static Socket listenSocket;

        public static void RunServer()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Address), Port);

            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    using (FileStream fs = new FileStream(Form1.binFileName, FileMode.OpenOrCreate))
                    {
                        MemoryStream msg = new MemoryStream();
                        msg.Write(BitConverter.GetBytes((int)fs.Length), 0, 4);
                        var x = new byte[fs.Length];
                        fs.Read(x, 0, (int)fs.Length);
                        msg.Write(x,0,x.Length);
                        if (msg != null) handler.Send(msg.ToArray());
                        else MessageBox.Show("Msg is Null.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Disconnect() => listenSocket?.Close();
    }
}
