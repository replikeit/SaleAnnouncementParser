using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parser.Core
{
    static class ClientInfoLoader
    {
        public static int Port { get; set; }
        public static string Address { get; set; }

        public static event Action<GameInfo[]> LoadSuccess;
        public static Task LoadPageAsync() => Task.Run(() =>
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Address), Port);

                using (TcpClient client = new TcpClient())
                {
                    client.Connect(ipPoint);
                    BinaryReader br = new BinaryReader(client.GetStream());
                    int size = br.ReadInt32();
                    BinaryFormatter formatter = new BinaryFormatter();
                    var resultArr = formatter.Deserialize(new MemoryStream(br.ReadBytes(size))) as GameInfo[];
                    LoadSuccess?.Invoke(resultArr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить данные от сервера.\n" + ex.Message);
            }
        });
    }
}
