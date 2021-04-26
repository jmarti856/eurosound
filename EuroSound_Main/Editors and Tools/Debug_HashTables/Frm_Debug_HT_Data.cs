using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.Clases;
using EuroSound_Application.HashCodesFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application.Debug_HashTables.HT_Data
{
    public partial class Frm_Debug_HT_Data : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private Thread DataToCombobox;
        private string HashTableOutputPath;

        public Frm_Debug_HT_Data(string OutputPath)
        {
            InitializeComponent();
            HashTableOutputPath = OutputPath;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_Debug_HT_Data_Load(object sender, EventArgs e)
        {
            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Load Preferences
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                bool IsIconic = Convert.ToBoolean(WindowStateConfig.GetValue("DBDView_IsIconic", 0));
                bool IsMaximized = Convert.ToBoolean(WindowStateConfig.GetValue("DBDView_IsMaximized", 0));

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
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("DBDView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("DBDView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("DBDView_Width", 678));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("DBDView_Height", 631));

                WindowStateConfig.Close();
            }

            //MFX Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_MusicMD5, GlobalPreferences.HT_MusicPath))
            {
                Hashcodes.LoadMusicHashcodes(GlobalPreferences.HT_MusicPath);
            }

            //Load data
            AddDataToCombobox();

            //Prevent null selection
            ComboBox_Looping.SelectedIndex = 0;
        }

        private void Frm_Debug_HT_Data_Shown(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound - Music Data Table Generator";
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_Debug_HT_Data_Enter(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - Music Data Table Generator";
            }

        }

        private void Frm_Debug_HT_Data_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - Music Data Table Generator";
            }
        }

        private void Frm_Debug_HT_Data_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_StoppingThreads"));

            //Stop threads
            if (DataToCombobox != null)
            {
                DataToCombobox.Abort();
            }

            //Reset title
            MdiParent.Text = "EuroSound";

            //Set Program status
            WindowsRegistryFunctions.SaveWindowState("DBDView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);
        }

        //*===============================================================================================
        //* Buttons Events
        //*===============================================================================================
        private void Button_HashTablePath_Click(object sender, EventArgs e)
        {
            //Open file browser
            Textbox_FilePath.Text = BrowsersAndDialogs.FileBrowserDialog("HashTable File (*.h)|*.h", 0, true);

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_GenericReadingFile"));

            //Read File
            if (!string.IsNullOrEmpty(Textbox_FilePath.Text))
            {
                IEnumerable<string> FileLines = File.ReadLines(Textbox_FilePath.Text);
                foreach (string CurrentLine in FileLines)
                {
                    if (CurrentLine.Trim().StartsWith("{"))
                    {
                        string[] SplittedLine = CurrentLine.Trim().Split(',');
                        if (SplittedLine.Length == 4)
                        {
                            uint Hashcode = Convert.ToUInt32(SplittedLine[0].Substring(1), 16);
                            int Looping = Convert.ToInt32(Convert.ToBoolean(SplittedLine[2].Substring(0, SplittedLine[2].Length - 1)));
                            DataGridView_HT_Content.Rows.Add(Hashcode, Hashcodes.GetHashcodeLabel(Hashcodes.MFX_Defines, Hashcode), SplittedLine[1].Substring(0, SplittedLine[1].Length - 1), ComboBox_Looping.Items[Looping]);
                        }
                        Task.Delay(1);
                    }
                }
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            //Add new rows
            DataGridView_HT_Content.Rows.Add(Combobox_HashCode.SelectedValue.ToString(), Combobox_HashCode.Text, Numeric_MusicDuration.Value, ComboBox_Looping.SelectedItem);
        }

        private void Button_DeleteItems_Click(object sender, EventArgs e)
        {
            //Delete selected rows
            foreach (DataGridViewRow SelectedRow in DataGridView_HT_Content.SelectedRows)
            {
                DataGridView_HT_Content.Rows.Remove(SelectedRow);
            }
        }

        private void Button_Generate_Click(object sender, EventArgs e)
        {
            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_GenericSavingFile"));

            //Check directory
            if (Directory.Exists(HashTableOutputPath))
            {
                //Disable button
                Button_Generate.Invoke((MethodInvoker)delegate
                {
                    Button_Generate.Enabled = false;
                });

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(HashTableOutputPath + "\\MFX_Data.h"))
                {
                    sw.WriteLine("// Music Data table from EuroSound 1");
                    sw.WriteLine("// " + DateTime.Now.ToString("dd MMMM yyyy"));
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("typedef struct{");
                    sw.WriteLine("\tu32      MusicHashCode;");
                    sw.WriteLine("\tfloat    DurationInSeconds;");
                    sw.WriteLine("\tbool     Looping;");
                    sw.WriteLine("} MusicDetails;");
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("MusicDetails MusicData[]={");
                    foreach (DataGridViewRow RowToWrite in DataGridView_HT_Content.Rows)
                    {
                        sw.WriteLine(string.Join("", "\t{0x", Convert.ToUInt32(RowToWrite.Cells[0].Value).ToString("X8"), "," + RowToWrite.Cells[2].Value, "f,", RowToWrite.Cells[3].Value.ToString().ToUpper(), "},"));
                        Task.Delay(1);
                    }
                    sw.WriteLine("};");
                    sw.Close();
                }

                //Enable button
                Button_Generate.Invoke((MethodInvoker)delegate
                {
                    Button_Generate.Enabled = true;
                });
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            //Close form
            Close();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void AddDataToCombobox()
        {
            //---AddDataToCombobox
            DataToCombobox = new Thread(() =>
            {
                Combobox_HashCode.BeginInvoke((MethodInvoker)delegate
                {
                    Combobox_HashCode.Enabled = false;
                    Combobox_HashCode.DataSource = null;
                    Combobox_HashCode.Items.Clear();
                    Combobox_HashCode.DataSource = Hashcodes.MFX_Defines.OrderBy(o => o.Value).ToList();
                });
                Combobox_HashCode.BeginInvoke((MethodInvoker)async delegate
                {
                    await Task.Delay(50);
                    Combobox_HashCode.ValueMember = "Key";
                    Combobox_HashCode.DisplayMember = "Value";
                    Combobox_HashCode.Update();
                    Combobox_HashCode.SelectedIndex = 0;
                    Combobox_HashCode.Enabled = true;
                });
            })
            {
                IsBackground = true
            };
            DataToCombobox.Start();
        }
    }
}
