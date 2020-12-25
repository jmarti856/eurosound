using Microsoft.Win32;
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
            /*--Check For .NET Framework v4.5--*/
            if (Get45or451FromRegistry())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length > 0)
                {
                    if (args[0].StartsWith("/"))
                    {
                        EuroSoundBashMode BashMode = new EuroSoundBashMode();
                        BashMode.ExecuteCommand(args);
                    }
                    else
                    {
                        Application.Run(new Frm_EuroSound_Splash(args[0]));
                    }
                }
                else
                {
                    Application.Run(new Frm_EuroSound_Splash(string.Empty));
                }
            }
            else
            {
                MessageBox.Show("This application requires at leat .NET Framework v4.5." + Environment.NewLine + "Please install it and try to run this application again.", "This application could not be started", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        static bool Get45or451FromRegistry()
        {
            bool versionOK = false;

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    if (!CheckFor45DotVersion(releaseKey).Equals("No 4.5 or later version detected"))
                    {
                        versionOK = true;
                    }
                }
            }

            return versionOK;
        }

        static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
            {
                return "4.7.2 or later";
            }
            if (releaseKey >= 461308)
            {
                return "4.7.1 or later";
            }
            if (releaseKey >= 460798)
            {
                return "4.7 or later";
            }
            if (releaseKey >= 394802)
            {
                return "4.6.2 or later";
            }
            if (releaseKey >= 394254)
            {
                return "4.6.1 or later";
            }
            if (releaseKey >= 393295)
            {
                return "4.6 or later";
            }
            if (releaseKey >= 393273)
            {
                return "4.6 RC or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return "No 4.5 or later version detected";
        }
    }
}
