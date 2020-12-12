using CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Main : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        string DraggedFile;
        bool MenuStripOpened;
        int FormID = 0;
        ResourceManager ResourcesManager;

        public Dictionary<string, string> SFX_Defines;
        public Dictionary<string, string> SB_Defines;
        public Dictionary<string, double[]> SFX_Data;

        public Frm_EuroSound_Main(string LoadedFileByArgument, ResourceManager v_ResourcesManager, Dictionary<string, string> d_SFX_Defines, Dictionary<string, string> v_SB_Defines, Dictionary<string, double[]> v_SFX_Data)
        {
            InitializeComponent();

            ResourcesManager = v_ResourcesManager;
            DraggedFile = LoadedFileByArgument;
            SFX_Defines = d_SFX_Defines;
            SB_Defines = v_SB_Defines;
            SFX_Data = v_SFX_Data;

            /*Menu Item: File*/
            MainMenu_File.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MainMenu_File.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItemFile_New.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_File_NewProject"));
            MenuItemFile_OpenESF.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemFile_OpenESF"));
            MenuItemFile_Exit.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemFile_Exit"));

            MenuItemFile_New.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemFile_OpenESF.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemFile_Exit.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);

            /*Menu Item: View*/
            MainMenu_View.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MainMenu_View.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItemView_StatusBar.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_View_StatusBar"));
            MenuItemView_Preferences.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_View_GlobalPreferences"));

            MenuItemView_StatusBar.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemView_Preferences.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);

            /*Menu Item: Window*/
            MainMenu_Window.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MainMenu_Window.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItemWindow_Cascade.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemWindow_Cascade"));
            MenuItemWindow_TileH.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemWindow_TileHorizontal"));
            MenuItemWindow_TileV.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemWindow_TileVertical"));
            MenuItemWindow_Arrange.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemWindow_ArrangeIcons"));

            MenuItemWindow_Cascade.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemWindow_TileH.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemWindow_TileV.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemWindow_Arrange.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);

            MenuItemWindow_Arrange.Click += (se, ev) => { this.LayoutMdi(MdiLayout.ArrangeIcons); GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); };
            MenuItemWindow_Cascade.Click += (se, ev) => { this.LayoutMdi(MdiLayout.Cascade); GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); };
            MenuItemWindow_TileH.Click += (se, ev) => { this.LayoutMdi(MdiLayout.TileHorizontal); GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); };
            MenuItemWindow_TileV.Click += (se, ev) => { this.LayoutMdi(MdiLayout.TileVertical); GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); };

            /*Menu Item: Help*/
            MainMenu_Help.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MainMenu_Help.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItemHelp_About.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemHelp_About"));
            MenuItemHelp_OnlineHelp.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItemHelp_OnlineHelp"));

            MenuItemHelp_About.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemHelp_OnlineHelp.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItemHelp_OnlineHelp.Click += (se, ev) => Process.Start("https://sphinxandthecursedmummy.fandom.com/wiki/SFX");
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_EuroSound_Main_Load(object sender, EventArgs e)
        {
            /*GetControl*/
            GenericFunctions.GetStatusBarControls(EuroSound_Main_StatusBar, EuroSound_StatusBar_Status, EuroSound_StatusBar_FileName);

            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(ResourcesManager.GetString("StatusBar_Status_Ready"));

            if (!string.IsNullOrEmpty(DraggedFile))
            {
                LoadSoundBank(DraggedFile);
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

        private void Frm_EuroSound_Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string FileToLoad in FileList)
            {
                if (Path.GetExtension(FileToLoad).Equals(".ESF", StringComparison.InvariantCultureIgnoreCase))
                {
                    LoadSoundBank(FileToLoad);
                }
            }
        }

        private void Frm_EuroSound_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == this.GetType() && frm != this)
                {
                    frm.Close();
                }
            }
            e.Cancel = false;
        }
        //*===============================================================================================
        //* MAIN MENU -- FILE
        //*===============================================================================================
        private void MenuItemFile_OpenESF_Click(object sender, EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("EuroSoundFile|*.ESF", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                LoadSoundBank(FilePath);
            }
        }

        private void MenuItemFile_New_Click(object sender, EventArgs e)
        {
            EuroSound_NewFileProject CreateNewFile = new EuroSound_NewFileProject(ResourcesManager.GetString("InputBoxNewProject"))
            {
                Owner = this,
                ShowInTaskbar = false
            };
            CreateNewFile.ShowDialog();
            if (CreateNewFile.DialogResult == DialogResult.OK)
            {
                /*--[COMBOBOX SELECTED VALUES]--
                0 = Soundbanks
                1 = Streamed sounds
                2 = Music tracks*/
                string[] NewFileProperties = CreateNewFile.FileProps;

                /*--THE NEW FILE WILL BE A SOUNDBANK--*/
                if (NewFileProperties[1].Equals("0"))
                {
                    OpenSoundBanksForm(string.Empty, NewFileProperties[0]);
                }
            }
        }

        private void MenuItemFile_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //*===============================================================================================
        //* MAIN MENU -- HELP
        //*===============================================================================================
        private void MenuItemHelp_About_Click(object sender, EventArgs e)
        {
            Frm_AboutEuroSound About = new Frm_AboutEuroSound()
            {
                Owner = this,
                ShowInTaskbar = false
            };
            About.ShowDialog();
        }

        //*===============================================================================================
        //* MAIN MENU -- VIEW
        //*===============================================================================================
        private void MenuItemView_StatusBar_CheckStateChanged(object sender, EventArgs e)
        {
            EuroSound_Main_StatusBar.Visible = MenuItemView_StatusBar.Checked;
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadSoundBank(string FilePathToLoad)
        {
            OpenSoundBanksForm(FilePathToLoad, "");
        }

        private void MenuItemView_Preferences_Click(object sender, EventArgs e)
        {
            Frm_MainPreferences AppPreferences = new Frm_MainPreferences()
            {
                Owner = this,
                ShowInTaskbar = false
            };
            AppPreferences.ShowDialog();
            AppPreferences.Dispose();
        }

        private void OpenSoundBanksForm(string FilePath, string ProjectName)
        {
            string FormTitle;
            if (File.Exists(GlobalPreferences.HT_SoundsPath) && File.Exists(GlobalPreferences.HT_SoundsDataPath))
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    FormTitle = ProjectName;
                }
                else
                {
                    FormTitle = string.Format("{0} - {1}", Path.GetFileName(FilePath), Path.GetDirectoryName(FilePath));
                }

                /*Show Form*/
                Frm_Soundbanks_Main NewSoundBankForm = new Frm_Soundbanks_Main(FilePath, ProjectName, GlobalPreferences.HT_SoundsPath, GlobalPreferences.HT_SoundsDataPath, SFX_Defines, SB_Defines, SFX_Data, ResourcesManager)
                {
                    Text = FormTitle,
                    Owner = this,
                    MdiParent = this,
                    Tag = "frm" + FormID.ToString()
                };
                NewSoundBankForm.Show();
                FormID++;
            }
            else
            {
                MessageBox.Show("The hashtable paths are not correct, please fix them first before continue.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
