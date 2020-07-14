using System;
using System.Windows.Forms;

namespace Parser.Core
{
    static class Program
    {
        public static Form1 form1;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigSetter.OpenConfig();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();
            Application.Run(form1);
        }
    }
}
