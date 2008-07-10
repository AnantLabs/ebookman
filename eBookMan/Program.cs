using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EBookMan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DataManager.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Application.Run(new MainWindow());

            DataManager.Instance.Dispose();
        }
    }
}