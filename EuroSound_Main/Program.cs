using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(args.Length <1 ? new Frm_EuroSound_Main(string.Empty) : new Frm_EuroSound_Main(args[0]));
        }
    }
}
