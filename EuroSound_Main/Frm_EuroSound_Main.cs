using CustomControls;
using FunctionsLibrary;
using SoundBanks_Editor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Threading;
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

        public Dictionary<string, string> SFX_Defines = new Dictionary<string, string>();
        public Dictionary<string, string> SB_Defines = new Dictionary<string, string>();
        public Dictionary<string, double[]> SFX_Data = new Dictionary<string, double[]>();

        public string HT_SoundsPath { get; set; } = @"X:\Sphinx\Sonix\SFX_Defines.h";
        public string HT_SoundsDataPath { get; set; } = @"X:\Sphinx\Sonix\SFX_Data.h";
        ResourceManager ResourcesManager = new ResourceManager(typeof(Properties.Resources));

        public Frm_EuroSound_Main(string Argument0)
        {
            InitializeComponent();
            DraggedFile = Argument0;

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
            MenuItemView_StatusBar.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);


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
        private void Frm_EuroSound_Main_Load(object sender, System.EventArgs e)
        {
            /*Get status label to modify it later*/
            GenericFunctions.GetProgramStatusLabel(EuroSound_StatusBar_Status, EuroSound_StatusBar_FileName, EuroSound_Main_StatusBar);

            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(ResourcesManager.GetString("StatusBar_Status_Ready"));

            if (!string.IsNullOrEmpty(DraggedFile))
            {
                LoadSoundBank(DraggedFile);
            }

            /*Load Hashcodes*/
            Thread LoadHashcodeData = new Thread(() =>Hashcodes.LoadSoundDataFile(HT_SoundsDataPath, SFX_Data, SFX_Defines, ResourcesManager))
            {
                IsBackground = true
            };
            Thread LoadHashcodes = new Thread(() =>Hashcodes.LoadSoundHashcodes(HT_SoundsPath, SFX_Defines, SB_Defines, ResourcesManager))
            {
                IsBackground = true
            };
            LoadHashcodes.Start();
            LoadHashcodes.Join();
            LoadHashcodeData.Start();


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
                    Frm_Soundbanks_Main NewSoundBankForm = new Frm_Soundbanks_Main(string.Empty, NewFileProperties[0], HT_SoundsPath, HT_SoundsDataPath, SFX_Defines, SB_Defines, SFX_Data, ResourcesManager)
                    {
                        Text = NewFileProperties[0],
                        Owner = this,
                        MdiParent = this,
                        Tag = FormID.ToString()
                    };
                    NewSoundBankForm.Show();
                    FormID++;
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
            Frm_Soundbanks_Main NewSoundBankForm = new Frm_Soundbanks_Main(FilePathToLoad, "", HT_SoundsPath, HT_SoundsDataPath, SFX_Defines, SB_Defines, SFX_Data, ResourcesManager)
            {
                Text = string.Format("{0} - {1}", Path.GetFileName(FilePathToLoad), Path.GetDirectoryName(FilePathToLoad)),
                Owner = this,
                MdiParent = this,
                Tag = FormID.ToString()
            };
            NewSoundBankForm.Show();
            FormID++;
        }
    }
}
