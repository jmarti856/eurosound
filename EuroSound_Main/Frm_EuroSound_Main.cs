using CustomControls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Main : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private string DraggedFile;

        private int FormID = 0;
        private int FormSFXDataGenerator = 0;
        private bool MenuItemsOpened = false;

        public Frm_EuroSound_Main(string LoadedFileByArgument)
        {
            InitializeComponent();

            DraggedFile = LoadedFileByArgument;

            /*Menu Item: File*/
            MainMenu_File.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuItemsOpened = true; };
            MainMenu_File.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuItemsOpened = false; };

            MenuItemFile_New.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_NewProject"));
            MenuItemFile_OpenESF.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_OpenESF"));
            MenuItemFile_Exit.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_Exit"));

            MenuItemFile_New.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemFile_OpenESF.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemFile_Exit.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);

            /*Menu Item: View*/
            MainMenu_View.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuItemsOpened = true; };
            MainMenu_View.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuItemsOpened = false; };

            MenuItemView_StatusBar.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItem_View_StatusBar"));
            MenuItemView_Preferences.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItem_View_GlobalPreferences"));

            MenuItemView_StatusBar.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemView_Preferences.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);

            /*Menu Item: Window*/
            MainMenu_Window.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuItemsOpened = true; };
            MainMenu_Window.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuItemsOpened = false; };

            MenuItemWindow_Cascade.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_Cascade"));
            MenuItemWindow_TileH.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_TileHorizontal"));
            MenuItemWindow_TileV.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_TileVertical"));
            MenuItemWindow_Arrange.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemWindow_ArrangeIcons"));

            MenuItemWindow_Cascade.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemWindow_TileH.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemWindow_TileV.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemWindow_Arrange.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);

            MenuItemWindow_Arrange.Click += (se, ev) => { MenuItemsOpened = false; GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); this.LayoutMdi(MdiLayout.ArrangeIcons); };
            MenuItemWindow_Cascade.Click += (se, ev) => { MenuItemsOpened = false; GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); this.LayoutMdi(MdiLayout.Cascade); };
            MenuItemWindow_TileH.Click += (se, ev) => { MenuItemsOpened = false; GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); this.LayoutMdi(MdiLayout.TileHorizontal); };
            MenuItemWindow_TileV.Click += (se, ev) => { MenuItemsOpened = false; GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); this.LayoutMdi(MdiLayout.TileVertical); };

            /*Menu Item: Tools*/
            MainMenu_Tools.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuItemsOpened = true; };
            MainMenu_Tools.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuItemsOpened = false; };

            MainMenuTools_SFXDataGen.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuTools_SFXDataGenerator"));
            MainMenuTools_SFXDataGen.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);

            /*Menu Item: Help*/
            MainMenu_Help.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuItemsOpened = true; };
            MainMenu_Help.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuItemsOpened = false; };

            MenuItemHelp_About.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_About"));
            MenuItemHelp_OnlineHelp.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(GenericFunctions.ResourcesManager.GetString("MenuItemHelp_OnlineHelp"));

            MenuItemHelp_About.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuItemsOpened);
            MenuItemHelp_OnlineHelp.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(false);
            MenuItemHelp_OnlineHelp.Click += (se, ev) => { MenuItemsOpened = false; Process.Start("https://sphinxandthecursedmummy.fandom.com/wiki/SFX"); };
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_EuroSound_Main_Load(object sender, EventArgs e)
        {
            /*GetControl*/
            GenericFunctions.GetStatusBarControls(EuroSound_Main_StatusBar, EuroSound_StatusBar_Status, EuroSound_StatusBar_FileName);

            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            /*Check that we are not reading nulls*/
            if (!string.IsNullOrEmpty(DraggedFile))
            {
                LoadSoundBank(DraggedFile);
            }
        }

        private void Frm_EuroSound_Main_DragDrop(object sender, DragEventArgs e)
        {
            //Get an array of the droped files
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < FileList.Length; i++)
            {
                if (Path.GetExtension(FileList[i]).Equals(".ESF", StringComparison.InvariantCultureIgnoreCase))
                {
                    LoadSoundBank(FileList[i]);
                }
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

        private void Frm_EuroSound_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            foreach (Form FormToClose in this.MdiChildren)
            {
                if (FormToClose != this)
                {
                    //Check that the user has not click the button cancel on the exit question
                    if (!GlobalPreferences.CancelApplicationClose)
                    {
                        FormToClose.Close();
                    }
                }
            }

            //If the user has not click the button cancel close application
            if (!GlobalPreferences.CancelApplicationClose)
            {
                e.Cancel = false;
            }

            //Reset var
            GlobalPreferences.CancelApplicationClose = false;
        }

        private void MainMenuTools_SFXDataGen_Click(object sender, EventArgs e)
        {
            MenuItemsOpened = false;

            Frm_SFX_DataGenerator BinaryFileGenerator = new Frm_SFX_DataGenerator()
            {
                Owner = this,
                MdiParent = this,
                Tag = "frm" + FormSFXDataGenerator.ToString()
            };
            BinaryFileGenerator.Show();
            FormSFXDataGenerator++;
        }

        //*===============================================================================================
        //* MAIN MENU -- FILE
        //*===============================================================================================
        private void MenuItemFile_Exit_Click(object sender, EventArgs e)
        {
            MenuItemsOpened = false;
            Application.Exit();
        }

        private void MenuItemFile_New_Click(object sender, EventArgs e)
        {
            MenuItemsOpened = false;
            EuroSound_NewFileProject CreateNewFile = new EuroSound_NewFileProject(GenericFunctions.ResourcesManager.GetString("InputBoxNewProject"))
            {
                Owner = this,
                ShowInTaskbar = false
            };
            CreateNewFile.ShowDialog();
            CreateNewFile.Dispose();
            if (CreateNewFile.DialogResult == DialogResult.OK)
            {
                /*--[COMBOBOX SELECTED VALUES]--
                0 = Soundbanks; 1 = Streamed sounds; 2 = Music tracks*/
                string[] NewFileProperties = CreateNewFile.FileProps;

                /*--THE NEW FILE WILL BE A SOUNDBANK--*/
                if (NewFileProperties[1].Equals("0"))
                {
                    OpenSoundBanksForm(string.Empty, NewFileProperties[0]);
                }
                else if (NewFileProperties[1].Equals("1"))
                {
                    OpenStreamedSoundsForm(string.Empty, NewFileProperties[0]);
                }
            }
        }

        private void MenuItemFile_OpenESF_Click(object sender, EventArgs e)
        {
            MenuItemsOpened = false;
            string FilePath = GenericFunctions.OpenFileBrowser("EuroSoundFile|*.ESF", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                LoadSoundBank(FilePath);
            }
        }

        //*===============================================================================================
        //* MAIN MENU -- HELP
        //*===============================================================================================
        private void MenuItemHelp_About_Click(object sender, EventArgs e)
        {
            MenuItemsOpened = false;
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
        private void MenuItemView_Preferences_Click(object sender, EventArgs e)
        {
            MenuItemsOpened = false;
            Frm_MainPreferences AppPreferences = new Frm_MainPreferences()
            {
                Owner = this,
                ShowInTaskbar = false
            };
            AppPreferences.ShowDialog();
            AppPreferences.Dispose();
            GenericFunctions.StatusBarTutorialMode(false);
        }

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
                Frm_Soundbanks_Main NewSoundBankForm = new Frm_Soundbanks_Main(FilePath, ProjectName)
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
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("GenericHashtablesNotCorrect"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenStreamedSoundsForm(string FilePath, string ProjectName)
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
                Frm_StreamSoundsEditorMain StreamSounds = new Frm_StreamSoundsEditorMain(FilePath, ProjectName)
                {
                    Text = FormTitle,
                    Owner = this,
                    MdiParent = this,
                    Tag = "frm" + FormID.ToString()
                };
                StreamSounds.Show();
                FormID++;
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("GenericHashtablesNotCorrect"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}