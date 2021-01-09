﻿using CustomControls;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferencesForms;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.SFXData;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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

        public Frm_EuroSound_Main(string ArgumentToLoad)
        {
            InitializeComponent();

            ArgumentFromSplash = ArgumentToLoad;

            /*Menu Item: File*/
            MainMenu_File.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_File.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemFile_New.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_NewProject")); };
            MenuItemFile_OpenESF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_OpenESF")); };
            MenuItemFile_Exit.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_Exit")); };

            MenuItemFile_New.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_OpenESF.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_Exit.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            /*Menu Item: View*/
            MainMenu_View.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_View.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemView_StatusBar.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_View_StatusBar")); };
            MenuItemView_Preferences.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_View_GlobalPreferences")); };

            MenuItemView_StatusBar.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemView_Preferences.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            /*Menu Item: Window*/
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

            /*Menu Item: Tools*/
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

            /*Menu Item: Help*/
            MainMenu_Help.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MainMenu_Help.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItemHelp_About.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_About")); };
            MenuItemHelp_OnlineHelp.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_OnlineHelp")); };

            MenuItemHelp_About.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemHelp_OnlineHelp.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemHelp_OnlineHelp.Click += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; Process.Start("https://sphinxandthecursedmummy.fandom.com/wiki/SFX"); };
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_EuroSound_Main_Load(object sender, EventArgs e)
        {
            /*GetControl*/
            GenericFunctions.ParentFormStatusBar = MainStatusBar;

            /*Update Status Bar*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            /*This means we loaded a soundbank file*/
            if (!string.IsNullOrEmpty(ArgumentFromSplash))
            {
                OpenFormsWithFileToLoad(ArgumentFromSplash);
            }
        }

        private void Frm_EuroSound_Main_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                EuroSoundTrayIcon.Visible = true;
                ShowInTaskbar = false;
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
            GenericFunctions.ClearTemporalFiles();
        }

        //*===============================================================================================
        //* MAIN MENU -- TOOLS
        //*===============================================================================================
        private void MainMenuTools_SFXDataGen_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;

            Frm_SFX_DataGenerator BinaryFileGenerator = new Frm_SFX_DataGenerator()
            {
                Owner = this,
                MdiParent = this,
                Tag = FormID.ToString()
            };
            BinaryFileGenerator.Show();
            FormID++;
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
                CreateNewFile.ShowInTaskbar = false;
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
            ArgumentFromSplash = GenericFunctions.OpenFileBrowser("Eurosound Files (*.esf)|*.esf", 0);
            if (!string.IsNullOrEmpty(ArgumentFromSplash))
            {
                OpenFormsWithFileToLoad(ArgumentFromSplash);
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
        private void MainMenuTools_BackupSettings_Click(object sender, EventArgs e)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("Eurosound Registry Files (*.esrf)|*.esrf", 1, true, null);
            if (!string.IsNullOrEmpty(SavePath))
            {
                BackupReloadSettings SettingsFunctions = new BackupReloadSettings();
                SettingsFunctions.BackupSettings(SavePath);
            }
        }

        private void MainMenuTools_RestoreSettings_Click(object sender, EventArgs e)
        {
            string FileToLoad = GenericFunctions.OpenFileBrowser("Eurosound Registry Files (*.esrf)|*.esrf", 0);
            if (!string.IsNullOrEmpty(FileToLoad))
            {
                BackupReloadSettings SettingsFunctions = new BackupReloadSettings();
                SettingsFunctions.RestoreSettings(FileToLoad);
            }
        }

        private void MainMenuTools_ClearTempFiles_Click(object sender, EventArgs e)
        {
            if (GenericFunctions.ClearTemporalFiles())
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

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void OpenFormsWithFileToLoad(string FileToLoad)
        {
            int TypeOfFileToLoad;

            TypeOfFileToLoad = TypeOfEuroSoundFile(FileToLoad);
            if (TypeOfFileToLoad == 0)
            {
                Frm_Soundbanks_Main SoundBanksForms = new Frm_Soundbanks_Main(string.Empty, FileToLoad)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
            else if (TypeOfFileToLoad == 1)
            {
                Frm_StreamSoundsEditorMain SoundBanksForms = new Frm_StreamSoundsEditorMain(string.Empty, ArgumentFromSplash)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };

                SoundBanksForms.Show();
                FormID++;
            }
        }

        private void OpenEmptyForms(string ProjectName, int TypeOfdata)
        {
            /*--[COMBOBOX FILE PROJECT SELECTED VALUES]--
            0 = Soundbanks; 1 = Streamed sounds; 2 = Music tracks*/

            if (TypeOfdata == 0)
            {
                Frm_Soundbanks_Main SoundBanksForms = new Frm_Soundbanks_Main(ProjectName, string.Empty)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
            else if (TypeOfdata == 1)
            {
                Frm_StreamSoundsEditorMain SoundBanksForms = new Frm_StreamSoundsEditorMain(ProjectName, string.Empty)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
        }

        private int TypeOfEuroSoundFile(string FileToLoad)
        {
            int Type = -1;

            /* TYPE VALUES
            Type -1 = bad format
            Type 0  = Soundbank
            Type 1  = Stream Soundbank
            Type 2  = Musics --NOT IMPLEMENTED YET--
            */

            if (System.IO.File.Exists(FileToLoad))
            {
                EuroSoundFiles ESFFiles = new EuroSoundFiles();
                using (BinaryReader BReader = new BinaryReader(System.IO.File.Open(FileToLoad, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    if (ESFFiles.FileIsCorrect(BReader))
                    {
                        Type = BReader.ReadSByte();
                    }

                    BReader.Close();
                }
            }

            return Type;
        }

        private void RestoreApplication()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Maximized;
                EuroSoundTrayIcon.Visible = false;
                ShowInTaskbar = true;
            }
        }
    }
}