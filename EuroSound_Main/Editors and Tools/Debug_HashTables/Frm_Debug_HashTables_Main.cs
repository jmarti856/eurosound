using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.Clases;
using EuroSound_Application.Debug_HashTables.HT_Data;
using EuroSound_Application.HashCodesFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application.Debug_HashTables
{
    public partial class Frm_Debug_HashTables_Main : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private Thread CreateSFXDebugFile;
        public Frm_Debug_HashTables_Main()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_Debug_HashTables_Main_Load(object sender, EventArgs e)
        {
            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Load Preferences
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                bool IsIconic = Convert.ToBoolean(WindowStateConfig.GetValue("DBView_IsIconic", 0));
                bool IsMaximized = Convert.ToBoolean(WindowStateConfig.GetValue("DBView_IsMaximized", 0));

                if (IsIconic)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (IsMaximized)
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("DBView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("DBView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("DBView_Width", 678));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("DBView_Height", 560));

                WindowStateConfig.Close();
            }
        }

        private void Frm_Debug_HashTables_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stop threads
            if (CreateSFXDebugFile != null)
            {
                CreateSFXDebugFile.Abort();
            }

            //Clear Dictionary
            Hashcodes.MFX_JumpCodes.Clear();

            //Save Form Position
            WindowsRegistryFunctions.SaveWindowState("DBView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* Buttons Events
        //*===============================================================================================
        private void Button_MFX_ValidList_Click(object sender, System.EventArgs e)
        {
            //Clear Console
            Textbox_Console.Clear();

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_GenericSavingFile"));

            //Check directory
            if (Directory.Exists(Textbox_OutputFolder.Text))
            {
                //Disable button
                Button_MFX_ValidList.Invoke((MethodInvoker)delegate
                {
                    Button_MFX_ValidList.Enabled = false;
                });

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Textbox_OutputFolder.Text + "\\MFX_ValidList.h"))
                {
                    //Inform User
                    Textbox_Console.Text += "Valid HashCodes:" + Environment.NewLine;

                    sw.WriteLine("s32 MFX_ValidList[]={");
                    //Loop Throught HashCodes
                    Hashcodes.ReadMusicJumpCodes();
                    foreach (KeyValuePair<uint, string> Item in Hashcodes.MFX_JumpCodes)
                    {
                        //Check Jump Codes
                        if (Item.Value.StartsWith("JMP"))
                        {
                            string LineToWrite = string.Join("", "0x", Item.Key.ToString("X8"), ",", "// ", Item.Value);
                            Textbox_Console.Text += LineToWrite + Environment.NewLine;
                            sw.WriteLine(LineToWrite);
                        }
                        Task.Delay(1);
                    }
                    sw.WriteLine("-1};");
                    sw.Close();
                }

                //Enable button
                Button_MFX_ValidList.Invoke((MethodInvoker)delegate
                {
                    Button_MFX_ValidList.Enabled = true;
                });
            }
            else
            {
                Textbox_Console.Text += "The specified directory does not exists or is not valid: " + Textbox_OutputFolder.Text;
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Button_MFX_Data_Click(object sender, System.EventArgs e)
        {
            //Open Form
            if (!GenericFunctions.CheckChildFormIsOpened("Frm_Debug_HT_Data", "MusicData"))
            {
                Frm_Debug_HT_Data DebugHTDataForm = new Frm_Debug_HT_Data(Textbox_OutputFolder.Text)
                {
                    Owner = Owner,
                    MdiParent = MdiParent
                };
                DebugHTDataForm.Show();
            }
        }

        private void Button_HT_Sound_Click(object sender, System.EventArgs e)
        {
            //Clear Console
            Textbox_Console.Clear();

            //Check directory
            if (Directory.Exists(Textbox_OutputFolder.Text))
            {
                CreateSFXDebugFile = new Thread(() =>
                {
                    //Set Program status
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_GenericSavingFile"));

                    //Disable button
                    Button_HT_Sound.Invoke((MethodInvoker)delegate
                    {
                        Button_HT_Sound.Enabled = false;
                    });

                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(Textbox_OutputFolder.Text + "\\SFX_Debug.h"))
                    {
                        //Inform User
                        Textbox_Console.Invoke((MethodInvoker)delegate
                        {
                            Textbox_Console.Text += "Valid HashCodes:" + Environment.NewLine;
                        });

                        //NumberToHashCode
                        sw.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES");
                        sw.WriteLine("long NumberToHashCode[] = {");

                        //Loop Throught HashCodes
                        foreach (KeyValuePair<uint, string> Item in Hashcodes.SFX_Defines)
                        {
                            string StringCombined = string.Join("", (Item.Key - 0x1A000000), " ,");

                            //Check Jump Codes
                            Textbox_Console.Invoke((MethodInvoker)delegate
                            {
                                Textbox_Console.Text += StringCombined + Environment.NewLine;
                            });
                            sw.WriteLine(StringCombined);
                            Thread.Sleep(5);
                        }
                        sw.WriteLine("};");
                        sw.WriteLine("#endif");

                        //HashCodeAndString
                        sw.WriteLine("typedef struct HashCodeAndString {long HashCode;char* String;} HashCodeAndString;");
                        sw.WriteLine(string.Empty);
                        sw.WriteLine("struct HashCodeAndString HashCodeToString[]={");

                        //Loop Throught HashCodes
                        foreach (KeyValuePair<uint, string> Item in Hashcodes.SFX_Defines)
                        {
                            string StringCombined = string.Join("", "{", (Item.Key - 0x1A000000), " , \"" + Item.Value + "\"} ,");
                            //Check Jump Codes
                            Textbox_Console.Invoke((MethodInvoker)delegate
                            {
                                Textbox_Console.Text += StringCombined + Environment.NewLine;
                            });
                            sw.WriteLine(StringCombined);
                            Thread.Sleep(5);
                        }
                        sw.WriteLine("};");
                        sw.WriteLine("#endif");
                        sw.Close();
                    }

                    //Enable button
                    Button_HT_Sound.Invoke((MethodInvoker)delegate
                    {
                        Button_HT_Sound.Enabled = true;
                    });

                    //Set Program status
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
                })
                {
                    IsBackground = true
                };
                CreateSFXDebugFile.Start();
            }
            else
            {
                Textbox_Console.Text += "The specified directory does not exists or is not valid: " + Textbox_OutputFolder.Text;
            }
        }

        private void Button_SearchOutputFolder_Click(object sender, System.EventArgs e)
        {
            //Search directory
            Textbox_OutputFolder.Text = BrowsersAndDialogs.OpenFolderBrowser();
        }
    }
}
