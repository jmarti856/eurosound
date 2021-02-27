using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.FunctionsListView;
using Microsoft.Win32;
using SoxSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.AudioConverter
{
    public partial class Frm_AudioConverter : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private WindowsRegistryFunctions WRegFunctions = new WindowsRegistryFunctions();
        private ListViewFunctions LVFunctions = new ListViewFunctions();
        internal List<string> Reports;
        private string[] FilesCollection;
        public Frm_AudioConverter()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_AudioConverter_Load(object sender, EventArgs e)
        {
            //Load Preferences
            using (RegistryKey WindowStateConfig = WRegFunctions.ReturnRegistryKey("WindowState"))
            {
                bool IsIconic = Convert.ToBoolean(WindowStateConfig.GetValue("ACView_IsIconic", 0));
                bool IsMaximized = Convert.ToBoolean(WindowStateConfig.GetValue("ACView_IsMaximized", 0));

                if (IsIconic)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (IsMaximized)
                {
                    // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
                    Icon = Icon.Clone() as Icon;
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("ACView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("ACView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("ACView_Width", 997));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("ACView_Height", 779));

                WindowStateConfig.Close();

                Combobox_Rate.SelectedIndex = 0;
                ComboBox_Bits.SelectedIndex = 0;
            }
        }

        private void Frm_AudioConverter_Shown(object sender, EventArgs e)
        {
            //Update from title
            if (WindowState != FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound - Audio Converter";
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_AudioConverter_Enter(object sender, EventArgs e)
        {
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - Audio Converter";
            }
        }

        private void Frm_AudioConverter_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - Audio Converter";
            }
        }

        private void Frm_AudioConverter_FormClosing(object sender, FormClosingEventArgs e)
        {
            WRegFunctions.SaveWindowState("ACView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);

            //Update Text
            MdiParent.Text = "EuroSound";

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* Form Controls Events
        //*===============================================================================================
        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!Background_ConvertAudios.IsBusy)
            {
                Reports = new List<string>();
                ProgressBar_Status.Value = 0;
                ProgressBar_Status.Maximum = ListView_ItemsToConvert.Items.Count;

                Background_ConvertAudios.RunWorkerAsync();
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Background_ConvertAudios.CancelAsync();
        }

        private void Button_SearchOutputFolder_Click(object sender, EventArgs e)
        {
            string OutputPath;

            OutputPath = GenericFunctions.OpenFolderBrowser();
            if (!string.IsNullOrEmpty(OutputPath))
            {
                Textbox_OutputFolder.Text = OutputPath;
            }
        }

        private void ListView_ItemsToConvert_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string FileToOpen;

            if (ListView_ItemsToConvert.SelectedItems.Count > 0)
            {
                FileToOpen = Path.Combine(ListView_ItemsToConvert.SelectedItems[0].SubItems[1].Text, ListView_ItemsToConvert.SelectedItems[0].SubItems[0].Text);
                OpenFile(FileToOpen);
            }
        }

        private void Button_ClearList_Click(object sender, EventArgs e)
        {
            if (ListView_ItemsToConvert.Items.Count > 0)
            {
                DialogResult QuestionResult = MessageBox.Show("Are you sure you want to clear the list?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (QuestionResult == DialogResult.Yes)
                {
                    ListView_ItemsToConvert.Items.Clear();
                    UpdateListViewCounter();
                }
            }
        }

        //*===============================================================================================
        //* BACKGROUND WORKER
        //*===============================================================================================
        private void Background_ConvertAudios_DoWorkAsync(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            sbyte Bits = 16;
            uint Frequency = 22050;
            ushort Channels = 2;
            string OutputFilePath;

            //Disable List
            ListView_ItemsToConvert.BeginInvoke((MethodInvoker)delegate
            {
                ListView_ItemsToConvert.Enabled = false;
            });

            //Get Config
            Combobox_Rate.BeginInvoke((MethodInvoker)delegate
            {
                Frequency = Convert.ToUInt32(Combobox_Rate.SelectedItem);
            });

            ComboBox_Bits.BeginInvoke((MethodInvoker)delegate
            {
                Bits = Convert.ToSByte(ComboBox_Bits.SelectedItem);
            });

            if (RadioButton_Mono.Checked)
            {
                Channels = 1;
            }

            //Check that we have an input path
            if (ListView_ItemsToConvert.Items.Count == 0)
            {
                Reports.Add(string.Join("", new string[] { "2", "There are no files to convert" }));
            }
            else
            {
                //Check that we have an output path
                if (string.IsNullOrEmpty(Textbox_OutputFolder.Text))
                {
                    Reports.Add(string.Join("", new string[] { "1", "No output path has been selected" }));
                }
                else
                {
                    CheckDataToExport();

                    foreach (string InputFilePath in FilesCollection)
                    {
                        if (Background_ConvertAudios.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }

                        //Convert audios
                        OutputFilePath = Path.Combine(Textbox_OutputFolder.Text, Path.GetFileNameWithoutExtension(InputFilePath) + "Converted.wav");

                        if (File.Exists(InputFilePath))
                        {
                            if (Directory.Exists(Textbox_OutputFolder.Text))
                            {
                                try
                                {
                                    //Start to convert data
                                    if (File.Exists(GlobalPreferences.SoXPath))
                                    {
                                        using (Sox sox = new Sox(GlobalPreferences.SoXPath))
                                        {
                                            sox.Multithreaded = true;
                                            sox.OnLogMessage += Sox_OnLogMessage;
                                            sox.Output.Type = FileType.WAV;
                                            sox.Output.SampleRate = Frequency;
                                            sox.Output.Channels = Channels;
                                            sox.Output.CustomArgs = "-b " + Bits;

                                            InputFile testInput = new InputFile(InputFilePath);
                                            sox.Process(testInput, OutputFilePath);
                                        }

                                        //Update Progress bar
                                        ProgressBar_Status.Invoke((MethodInvoker)delegate
                                        {
                                            ProgressBar_Status.Value += 1;
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                Reports.Add(string.Join("", new string[] { "0", "The directory: ", Textbox_OutputFolder.Text, " does not exists" }));
                            }
                        }
                        else
                        {
                            Reports.Add(string.Join("", new string[] { "0", "The file: ", InputFilePath, " does not exists" }));
                        }
                    }
                }
            }

            //Enable List
            ListView_ItemsToConvert.Invoke((MethodInvoker)delegate
            {
                ListView_ItemsToConvert.Enabled = true;
            });
        }

        private void Background_ConvertAudios_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Reports.Add(string.Join("", new string[] { "0", "An error ocurred during the output: ", e.Error.ToString() }));
            }
            if (e.Cancelled)
            {
                Reports.Add(string.Join("", new string[] { "2", "Operation cancelled by the user" }));
            }

            if (Reports.Count > 0)
            {
                GenericFunctions.ShowErrorsAndWarningsList(Reports, "Results", this);
                Reports = null;
            }
            ProgressBar_Status.Value = 0;
        }

        //*===============================================================================================
        //* Sox Events
        //*===============================================================================================
        void Sox_OnLogMessage(object sender, LogMessageEventArgs e)
        {
            if (e.LogLevel == LogLevelType.Warning)
            {
                Reports.Add(string.Join("", new string[] { "1", e.Message }));
            }
            else if (e.LogLevel == LogLevelType.Info)
            {
                Reports.Add(string.Join("", new string[] { "2", e.Message }));
            }
            else
            {
                Reports.Add(string.Join("", new string[] { "0", e.Message }));
            }
        }

        //*===============================================================================================
        //* Menu Item File
        //*===============================================================================================
        private void MenuItemFile_ImportFolders_Click(object sender, EventArgs e)
        {
            string FolderToOpen = GenericFunctions.OpenFolderBrowser();
            if (Directory.Exists(FolderToOpen))
            {
                FilesCollection = Directory.GetFiles(FolderToOpen, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".flac") || s.EndsWith(".wma") || s.EndsWith(".aac")).ToArray();
                PrintFilesCollection(FilesCollection);
            }
        }

        private void MenuItemFile_ImportFiles_Click(object sender, EventArgs e)
        {
            string FileToOpen = GenericFunctions.OpenFileBrowser("WAV Files (*.wav)|*.wav|MP3 Files (*.mp3)|*.mp3|FLAC Files (*.flac)|.flac|WMA Files (*.wma)|.wma|AAC Files (*.aac)|.aac", 0, true);
            if (!string.IsNullOrEmpty(FileToOpen))
            {
                AddFileToListView(Path.GetFileName(FileToOpen), Path.GetDirectoryName(FileToOpen), Path.GetExtension(FileToOpen), new FileInfo(FileToOpen).Length.ToString() + " bytes");
            }
        }

        //*===============================================================================================
        //* Menu Item Edit
        //*===============================================================================================
        private void MenuItemEdit_SelectAll_Click(object sender, EventArgs e)
        {
            LVFunctions.SelectAllItems(ListView_ItemsToConvert);
        }

        private void MenuItemEdit_SelectNone_Click(object sender, EventArgs e)
        {
            LVFunctions.SelectNone(ListView_ItemsToConvert);
        }

        private void MenuItemEdit_InvertSelection_Click(object sender, EventArgs e)
        {
            LVFunctions.InvertSelection(ListView_ItemsToConvert);
        }

        //*===============================================================================================
        //* Menu Item Load
        //*===============================================================================================
        private void MenuItemLoad_Presets_Click(object sender, EventArgs e)
        {
            using (Frm_AudioConverter_Presets AC_Presets = new Frm_AudioConverter_Presets())
            {
                AC_Presets.Owner = Owner;
                AC_Presets.Tag = Tag;
                AC_Presets.ShowDialog();
            }
        }

        //*===============================================================================================
        //* Context Menu
        //*===============================================================================================
        private void MenuItem_Open_Click(object sender, EventArgs e)
        {
            string FileToOpen;

            if (ListView_ItemsToConvert.SelectedItems.Count > 0)
            {
                FileToOpen = Path.Combine(ListView_ItemsToConvert.SelectedItems[0].SubItems[1].Text, ListView_ItemsToConvert.SelectedItems[0].SubItems[0].Text);
                OpenFile(FileToOpen);
            }
        }

        private void MenuItem_OpenFolder_Click(object sender, EventArgs e)
        {
            if (ListView_ItemsToConvert.SelectedItems.Count > 0)
            {
                OpenFolder(ListView_ItemsToConvert.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void MenuItem_Delete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in ListView_ItemsToConvert.SelectedItems)
            {
                ListView_ItemsToConvert.Items.Remove(SelectedItem);
            }
        }

        private void MenuItem_SelectAll_Click(object sender, EventArgs e)
        {
            LVFunctions.SelectAllItems(ListView_ItemsToConvert);
        }

        private void MenuItem_SelectNone_Click(object sender, EventArgs e)
        {
            LVFunctions.SelectNone(ListView_ItemsToConvert);
        }

        private void MenuItem_InvertSelection_Click(object sender, EventArgs e)
        {
            LVFunctions.InvertSelection(ListView_ItemsToConvert);
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        private void CheckDataToExport()
        {
            FilesCollection = new string[ListView_ItemsToConvert.Items.Count];

            for (int i = 0; i < FilesCollection.Length; i++)
            {
                ListView_ItemsToConvert.Invoke((MethodInvoker)delegate
                {
                    FilesCollection[i] = Path.Combine(ListView_ItemsToConvert.Items[i].SubItems[1].Text, ListView_ItemsToConvert.Items[i].SubItems[0].Text);
                });
            }
        }

        private void PrintFilesCollection(string[] Files)
        {
            Thread PrintFiles = new Thread(() =>
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_ReadingDirectory"));

                for (int i = 0; i < Files.Length; i++)
                {
                    AddFileToListView(Path.GetFileName(Files[i]), Path.GetDirectoryName(Files[i]), Path.GetExtension(Files[i]), new FileInfo(Files[i]).Length.ToString() + " bytes");
                }

                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            PrintFiles.Start();
        }
    }
}
