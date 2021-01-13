using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.FunctionsListView;
using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
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
            }
        }

        private void Frm_AudioConverter_FormClosing(object sender, FormClosingEventArgs e)
        {
            WRegFunctions.SaveWindowState("ACView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* Form Controls Events
        //*===============================================================================================
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


        //*===============================================================================================
        //* Menu Item File
        //*===============================================================================================
        private void MenuItemFile_ImportFolders_Click(object sender, EventArgs e)
        {
            string FolderToOpen = GenericFunctions.OpenFolderBrowser();
            if (Directory.Exists(FolderToOpen))
            {
                string[] FilesCollection = Directory.GetFiles(FolderToOpen, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".flac") || s.EndsWith(".wma") || s.EndsWith(".aac")).ToArray();
                PrintFilesCollection(FilesCollection);
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
        private void PrintFilesCollection(string[] Files)
        {
            Thread PrintFiles = new Thread(() =>
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_ReadingDirectory"));

                for (int i = 0; i < Files.Length; i++)
                {
                    ListViewItem ItemToAdd = new ListViewItem(new[] { Path.GetFileName(Files[i]), Path.GetDirectoryName(Files[i]), Path.GetExtension(Files[i]), new FileInfo(Files[i]).Length.ToString() + " bytes" });
                    ListView_ItemsToConvert.Invoke((MethodInvoker)delegate
                    {
                        ListView_ItemsToConvert.Items.Add(ItemToAdd);
                    });
                }

                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            PrintFiles.Start();
        }

        private void OpenFile(string PathToFile)
        {
            if (File.Exists(PathToFile))
            {
                Process.Start(PathToFile);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_FileNotExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenFolder(string PathToFolder)
        {
            if (Directory.Exists(PathToFolder))
            {
                Process.Start(PathToFolder);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_DirectoryNotExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
