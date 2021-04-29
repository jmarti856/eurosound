using EuroSound_Application.AboutForm;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferencesForms;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.AudioConverter;
using EuroSound_Application.Clases;
using EuroSound_Application.CustomControls.NewProjectForm;
using EuroSound_Application.Debug_HashTables;
using EuroSound_Application.SFXData;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Main : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private int FormID = 0;
        private string ArgumentFromSplash;
        protected MostRecentFilesMenu RecentFilesMenu;
        private string RecentFilesMenuRegKey = "SOFTWARE\\Eurocomm\\EuroSound\\RecentFiles";

        public Frm_EuroSound_Main(string ArgumentToLoad)
        {
            InitializeComponent();
            ArgumentFromSplash = ArgumentToLoad;

            //Menu Item: File
            MainMenu_File.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_File.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemFile_New.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_NewProject")); };
            MenuItemFile_OpenESF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_OpenESF")); };
            MenuItemFile_Exit.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_Exit")); };

            MenuItemFile_New.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_OpenESF.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_Exit.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: View
            MainMenu_View.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_View.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemView_StatusBar.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_View_StatusBar")); };
            MenuItemView_Preferences.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_View_GlobalPreferences")); };

            MenuItemView_StatusBar.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemView_Preferences.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: Window
            MainMenu_Window.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_Window.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemWindow_Cascade.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_Cascade")); };
            MenuItemWindow_TileH.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_TileHorizontal")); };
            MenuItemWindow_TileV.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_TileVertical")); };
            MenuItemWindow_Arrange.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_ArrangeIcons")); };

            MenuItemWindow_Cascade.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemWindow_TileH.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemWindow_TileV.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemWindow_Arrange.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            MenuItemWindow_Arrange.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); LayoutMdi(MdiLayout.ArrangeIcons); };
            MenuItemWindow_Cascade.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); LayoutMdi(MdiLayout.Cascade); };
            MenuItemWindow_TileH.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); LayoutMdi(MdiLayout.TileHorizontal); };
            MenuItemWindow_TileV.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); LayoutMdi(MdiLayout.TileVertical); };

            //Menu Item: Tools
            MainMenu_Tools.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_Tools.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MainMenuTools_SFXDataGen.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuTools_SFXDataGenerator")); };
            MainMenuTools_BackupSettings.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemTools_BackupSettings")); };
            MainMenuTools_RestoreSettings.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemTools_RestoreSettings")); };
            MainMenuTools_ClearTempFiles.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemTools_ClearTemp")); };

            MainMenuTools_SFXDataGen.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MainMenuTools_BackupSettings.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MainMenuTools_RestoreSettings.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MainMenuTools_ClearTempFiles.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: Help
            MainMenu_Help.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_Help.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemHelp_About.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_About")); };
            MenuItemHelp_OnlineHelp.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_OnlineHelp")); };
            MenuItemHelp_ReleaseInfo.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_ReleaseInfo")); };
            MenuItemHelp_CurrentReleaseInfo.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_CurrentReleaseInfo")); };

            MenuItemHelp_About.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemHelp_OnlineHelp.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemHelp_OnlineHelp.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; Process.Start("https://sphinxandthecursedmummy.fandom.com"); };
            MenuItemHelp_ReleaseInfo.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemHelp_ReleaseInfo.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; Process.Start("https://github.com/jmarti856/eurosound/releases"); };
            MenuItemHelp_CurrentReleaseInfo.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemHelp_CurrentReleaseInfo.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; Process.Start(string.Join("", "https://github.com/jmarti856/eurosound/releases/tag/", GenericFunctions.GetEuroSoundVersion())); };
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_EuroSound_Main_Load(object sender, EventArgs e)
        {
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                if (Convert.ToBoolean(WindowStateConfig.GetValue("MainFrame_IsIconic", 0)))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (Convert.ToBoolean(WindowStateConfig.GetValue("MainFrame_IsMaximized", 0)))
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("MainFrame_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("MainFrame_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("MainFrame_Width", 1119));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("MainFrame_Height", 617));

                WindowStateConfig.Close();
            }

            //Load Recent Files
            RecentFilesMenu = new MruStripMenuInline(MainMenu_File, MenuItemFile_RecentFiles, new MostRecentFilesMenu.ClickedHandler(RecentFile_click), RecentFilesMenuRegKey, 8);
            RecentFilesMenu.LoadFromRegistry();
        }

        private void Frm_EuroSound_Main_Shown(object sender, EventArgs e)
        {
            //GetControl
            GenericFunctions.ParentFormStatusBar = MainStatusBar;

            //This means we have an argument to read
            if (!string.IsNullOrEmpty(ArgumentFromSplash))
            {
                OpenFormsWithFileToLoad(ArgumentFromSplash);
            }
            else
            {
                if (GlobalPreferences.LoadLastLoadedESF)
                {
                    string LastActiveDocument = WindowsRegistryFunctions.LoadActiveDocument();
                    OpenFormsWithFileToLoad(LastActiveDocument);
                }
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_EuroSound_Main_Resize(object sender, EventArgs e)
        {
            //Minimize on System Tray
            if (GlobalPreferences.UseSystemTray)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    EuroSoundTrayIcon.Visible = true;
                    ShowInTaskbar = false;
                }
            }

            //Hide Sizing Grip if maximized
            if (WindowState == FormWindowState.Maximized)
            {
                MainStatusBar.SizingGrip = false;
            }
            else
            {
                MainStatusBar.SizingGrip = true;
            }
        }

        private void Frm_EuroSound_Main_DragDrop(object sender, DragEventArgs e)
        {
            //Get an array of the droped files
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < FileList.Length; i++)
            {
                OpenFormsWithFileToLoad(FileList[i]);
            }
        }

        private void Frm_EuroSound_Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Frm_EuroSound_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Save Active Document
            if (MdiChildren.Length == 0)
            {
                WindowsRegistryFunctions.SaveActiveDocument("");
            }

            //Save Window position
            WindowsRegistryFunctions.SaveWindowState("MainFrame", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized, 0);

            //Save Recent Files
            RecentFilesMenu.SaveToRegistry();

            //Clear temporal folder
            ClearTemporalFiles();
        }

        //*===============================================================================================
        //* MAIN MENU -- FILE
        //*===============================================================================================
        private void MenuItemFile_Exit_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            Close();
        }

        private void MenuItemFile_New_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            using (EuroSound_NewFileProject CreateNewFile = new EuroSound_NewFileProject(GenericFunctions.ResourcesManager.GetString("InputBoxNewProject")))
            {
                CreateNewFile.Owner = this;
                CreateNewFile.ShowDialog();

                if (CreateNewFile.DialogResult == DialogResult.OK)
                {
                    //Position 0 = Project Name, Position 1= Type of data
                    string[] NewFileProperties = CreateNewFile.FileProps;
                    OpenEmptyForms(NewFileProperties[0], int.Parse(NewFileProperties[1]));
                }
            }
        }

        private void MenuItemFile_OpenESF_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            ArgumentFromSplash = BrowsersAndDialogs.FileBrowserDialog("Eurosound Files (*.esf)|*.esf", 0, false);
            if (!string.IsNullOrEmpty(ArgumentFromSplash))
            {
                OpenFormsWithFileToLoad(ArgumentFromSplash);
            }
        }

        //*===============================================================================================
        //* MAIN MENU -- RECENT FILES
        //*===============================================================================================
        private void RecentFile_click(int number, String filename)
        {
            //Update status bar
            GlobalPreferences.StatusBar_ToolTipMode = false;

            //Load file
            if (System.IO.File.Exists(filename))
            {
                OpenFormsWithFileToLoad(filename);
            }
            else
            {
                MessageBox.Show(string.Join(" ", "Loading File:", filename, "\n", "\n", "Error:", filename, "was not found"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RecentFilesMenu.RemoveFile(number);
            }
        }

        //*===============================================================================================
        //* MAIN MENU -- HELP
        //*===============================================================================================
        private void MenuItemHelp_About_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            using (Frm_AboutEuroSound About = new Frm_AboutEuroSound())
            {
                About.Owner = this;
                About.ShowInTaskbar = false;
                About.ShowDialog();
            };
        }

        //*===============================================================================================
        //* MAIN MENU -- VIEW
        //*===============================================================================================
        private void MenuItemView_Preferences_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            using (Frm_MainPreferences AppPreferences = new Frm_MainPreferences())
            {
                AppPreferences.Owner = this;
                AppPreferences.ShowInTaskbar = false;
                AppPreferences.ShowDialog();
            };

            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);
        }

        private void MenuItemView_StatusBar_CheckStateChanged(object sender, EventArgs e)
        {
            MainStatusBar.Visible = MenuItemView_StatusBar.Checked;
        }

        //*===============================================================================================
        //* MAIN MENU -- TOOLS
        //*===============================================================================================
        private void MainMenuTools_DebugHashtables_Click(object sender, EventArgs e)
        {
            if (!GenericFunctions.CheckChildFormIsOpened("Frm_Debug_HashTables_Main", "HT_Debugger"))
            {
                Frm_Debug_HashTables_Main ES_Debug_HashTables = new Frm_Debug_HashTables_Main
                {
                    Owner = this,
                    MdiParent = this
                };
                ES_Debug_HashTables.Show();
            }
        }

        private void MainMenuTools_AudioConverter_Click(object sender, EventArgs e)
        {
            if (!GenericFunctions.CheckChildFormIsOpened("Frm_AudioConverter", "AudioC"))
            {
                Frm_AudioConverter ES_AudioConverter = new Frm_AudioConverter
                {
                    Owner = this,
                    MdiParent = this
                };
                ES_AudioConverter.Show();
            }
        }
        private void MainMenuTools_SFXDataGen_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            if (!GenericFunctions.CheckChildFormIsOpened("Frm_SFX_DataGenerator", "SFXData"))
            {
                Frm_SFX_DataGenerator BinaryFileGenerator = new Frm_SFX_DataGenerator()
                {
                    Owner = this,
                    MdiParent = this
                };
                BinaryFileGenerator.Show();
            }
        }

        private void MainMenuTools_BackupSettings_Click(object sender, EventArgs e)
        {
            string SavePath = BrowsersAndDialogs.SaveFileBrowser("Eurosound Registry Files (*.esrf)|*.esrf", 1, true, null);
            if (!string.IsNullOrEmpty(SavePath))
            {
                BackupReloadSettings SettingsFunctions = new BackupReloadSettings();
                SettingsFunctions.BackupSettings(SavePath);
            }
        }

        private void MainMenuTools_RestoreSettings_Click(object sender, EventArgs e)
        {
            string FileToLoad = BrowsersAndDialogs.FileBrowserDialog("Eurosound Registry Files (*.esrf)|*.esrf", 0, false);
            if (!string.IsNullOrEmpty(FileToLoad))
            {
                BackupReloadSettings SettingsFunctions = new BackupReloadSettings();
                SettingsFunctions.RestoreSettings(FileToLoad);
                RecentFilesMenu.LoadFromRegistry();
            }
        }

        private void MainMenuTools_ClearTempFiles_Click(object sender, EventArgs e)
        {
            if (ClearTemporalFiles())
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_TemporalFilesRemovedSuccess"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_NoTemporalFilesStored"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //*===============================================================================================
        //* SYSTEM TRAY MODE
        //*===============================================================================================
        private void EuroSoundTrayIcon_Restore_Click(object sender, EventArgs e)
        {
            RestoreApplication();
        }

        private void EuroSoundTrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RestoreApplication();
            }
        }

        private void EuroSoundTrayIcon_Close_Click(object sender, EventArgs e)
        {
            RestoreApplication();
            Close();
        }
    }
}