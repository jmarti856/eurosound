using EuroSound_Application.BashMode;
using EuroSound_Application.CustomControls.ProgramInstancesForm;
using EuroSound_Application.SplashForm;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [STAThread]
        private static void Main(string[] args)
        {
            /*
            args = new string[2];
            args[0] = "/o";
            args[1] = @"C:\Users\Jordi Martinez\Desktop\Sphinx and the shadow of set\SoundBanks\Files\Abydos\Abydos_North.esf";
            */

            /*--Check For .NET Framework v4.5--*/
            if (Get45or451FromRegistry())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length > 0)
                {
                    if (args[0].Trim().StartsWith("/"))
                    {
                        EuroSoundBashMode BashMode = new EuroSoundBashMode();
                        BashMode.LoadBasicData();
                        BashMode.ExecuteCommand(args);
                    }
                    else
                    {
                        StartApplicationMDI(args);
                    }
                }
                else
                {
                    args = new string[] { string.Empty };
                    StartApplicationMDI(args);
                }
            }
            else
            {
                MessageBox.Show(string.Format("This application requires at leat .NET Framework v4.5.2.\nPlease install it and try to run this application again."), "This application could not be started", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private static bool Get45or451FromRegistry()
        {
            bool versionOK = false;

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (!CheckFor45DotVersion(releaseKey).StartsWith("No"))
                {
                    versionOK = true;
                }
            }

            return versionOK;
        }

        private static string CheckFor45DotVersion(int releaseKey)
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
            /*
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }*/
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            return "No 4.5 or later version detected";
        }

        private static void StartApplicationMDI(string[] Arguments)
        {
            Process[] EuroSoundInstances = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
            //We have more instances
            if (EuroSoundInstances.Length > 1)
            {
                using (EuroSound_Instances CheckInstances = new EuroSound_Instances())
                {
                    CheckInstances.ShowDialog();

                    //Dont' start another instance
                    if (CheckInstances.DialogResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    //Show other instance
                    else if (CheckInstances.DialogResult == DialogResult.Yes)
                    {
                        int InstanceID = EuroSoundInstances.Length - 2;

                        IntPtr InstanceToShow = FindWindow(null, EuroSoundInstances[InstanceID].ProcessName);
                        if (InstanceToShow != null)
                        {
                            if (IsIconic(InstanceToShow))
                            {
                                ShowWindow(InstanceToShow, SW_SHOWMAXIMIZED);
                            }
                            SetForegroundWindow(InstanceToShow);
                        }
                    }
                    //Start another instance anyway
                    else if (CheckInstances.DialogResult == DialogResult.OK)
                    {
                        StartApplicationForms(Arguments);
                    }

                    CheckInstances.Close();
                }
            }
            //We not have more instances
            else
            {
                StartApplicationForms(Arguments);
            }
        }

        private static void StartApplicationForms(string[] Arguments)
        {
            //Show "Frm_EuroSound_Splash" form and load data
            using (Frm_EuroSound_Splash ProgramSplash = new Frm_EuroSound_Splash())
            {
                ProgramSplash.ShowInTaskbar = false;
                ProgramSplash.ShowDialog();
            };

            //Show "Frm_EuroSound_Main" form
            Application.Run(new Frm_EuroSound_Main(Arguments[0]));
        }
    }
}
