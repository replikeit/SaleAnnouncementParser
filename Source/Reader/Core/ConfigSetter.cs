using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Parser.Core
{
    static class ConfigSetter
    {
        private static void SetConfigSettings(string name, string item)
        {
            switch (name.ToLower())
            {
                case "ip":
                    ClientInfoLoader.Address = item;
                    break;
                case "port":
                    ClientInfoLoader.Port = int.Parse(item);
                    break;
            }
        }

        private static void SetConfigDefault()
        {
            ClientInfoLoader.Port = 8005;
            ClientInfoLoader.Address = "127.0.0.1";
        }

        public static void OpenConfig()
        {
            SetConfigDefault();
            try
            {
                using (StreamReader sw = new StreamReader("settings.cfg"))
                {


                    string line = sw.ReadLine();

                    while (line != null)
                    {
                        var items = line.Split('=').Select(x => x.Trim()).ToArray();
                        SetConfigSettings(items[0], items[1]);
                        line = sw.ReadLine();
                    }
                }
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show($"Не корректные данные в файле настроек.\n" +
                                "Подключение к серверу не настроено.\n" +
                                $"{exception.Message}", "Ошибка!");

            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show($"Не удалось найти конфиг настроек.\n" +
                                "Подключение к серверу не настроено.\n" +
                                $"{exception.Message}", "Ошибка!");
            }
        }
    }
}
