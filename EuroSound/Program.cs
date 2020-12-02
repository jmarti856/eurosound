using System;
using System.Windows.Forms;

namespace EuroSound
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
            Application.Run(args.Length == 0 ? new Frm_Main(string.Empty) : new Frm_Main(args[0]));
        }
    }
}
