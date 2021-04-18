﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.ApplicationRegistryFunctions;
using System;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_MainPreferences : Form
    {
        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        internal string HT_SoundsPathTEMPORAL;
        internal string HT_SoundsDataPathTEMPORAL;
        internal string HT_MusicPathTEMPORAL;

        //Frm_TreeViewPrefs
        internal string SelectedFontTEMPORAL;
        internal int TreeViewIndentTEMPORAL;
        internal int TreeViewItemHeightTEMPORAL;
        internal bool ShowLinesTEMPORAL;
        internal bool ShowRootLinesTEMPORAL;
        internal bool TV_IgnoreStlyesFromESFTEMPORAL;
        internal bool LoadLastLoadedESFTEMPORAL;

        //Frm_GeneralPreferences
        internal string SFXOutputPathTEMPORAL;
        internal string MusicOutputPathTEMPORAL;
        internal int ColorWavesControlTEMPORAL;
        internal int BackColorWavesControlTEMPORAL;

        //Frm_StreamFile
        internal string StreamFilePathTEMPORAL;

        //Frm_SoxPrefs
        internal string SoXPathTEMPORAL;

        //Frm_System
        internal bool UseSystemTrayTEMPORAL;

        //Frm_OutputDevicecs
        internal int DefaultAudioDeviceTEMPORAL;

        //Frm_Profiles
        internal string SelectedProfileTEMPORAL;
        internal string SelectedProfileNameTEMPORAL;

        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private Frm_General GeneralPreferences;
        private Frm_TreeViewPrefs TreeViewPrefs;
        private Frm_SoxPrefs SoXPreferences;
        private Frm_System SystemPreferences;
        private Frm_OutputDevices AudioDevices;
        private Frm_Profiles ProfilesForm;
        private bool FrmGeneralPreferencesOpened = false;
        private bool FrmTreeViewPrefsOpened = false;
        private bool FrmSoXPreferencesOpened = false;
        private bool FrmSystemPreferencesOpened = false;
        private bool FrmAudioDevicesOpened = false;
        private bool FrmProfilesFormOpened = false;

        public Frm_MainPreferences()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_MainPreferences_Load(object sender, EventArgs e)
        {
            BackColorWavesControlTEMPORAL = GlobalPreferences.WavesViewerControl_BackgroundColor;
            ColorWavesControlTEMPORAL = GlobalPreferences.WavesViewerControl_WavesColor;
            HT_MusicPathTEMPORAL = GlobalPreferences.HT_MusicPath;
            HT_SoundsDataPathTEMPORAL = GlobalPreferences.HT_SoundsDataPath;
            HT_SoundsPathTEMPORAL = GlobalPreferences.HT_SoundsPath;
            MusicOutputPathTEMPORAL = GlobalPreferences.MusicOutputPath;
            SelectedFontTEMPORAL = GlobalPreferences.TV_SelectedFont;
            SFXOutputPathTEMPORAL = GlobalPreferences.SFXOutputPath;
            ShowLinesTEMPORAL = GlobalPreferences.TV_ShowLines;
            ShowRootLinesTEMPORAL = GlobalPreferences.TV_ShowRootLines;
            SoXPathTEMPORAL = GlobalPreferences.SoXPath;
            StreamFilePathTEMPORAL = GlobalPreferences.StreamFilePath;
            TreeViewIndentTEMPORAL = GlobalPreferences.TV_Indent;
            TreeViewItemHeightTEMPORAL = GlobalPreferences.TV_ItemHeight;
            UseSystemTrayTEMPORAL = GlobalPreferences.UseSystemTray;
            SelectedProfileTEMPORAL = GlobalPreferences.SelectedProfile;
            SelectedProfileNameTEMPORAL = GlobalPreferences.SelectedProfileName;
            TV_IgnoreStlyesFromESFTEMPORAL = GlobalPreferences.TV_IgnoreStlyesFromESF;
            LoadLastLoadedESFTEMPORAL = GlobalPreferences.LoadLastLoadedESF;

            TreeViewPreferences.ExpandAll();
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
            RemoveAllFormsInsidePanel(Panel_SecondaryForms);

            //-----------------[Frm_TreeViewPrefs]-----------------
            if (FrmTreeViewPrefsOpened)
            {
                GlobalPreferences.TV_SelectedFont = SelectedFontTEMPORAL;
                GlobalPreferences.TV_Indent = TreeViewIndentTEMPORAL;
                GlobalPreferences.TV_ItemHeight = TreeViewItemHeightTEMPORAL;
                GlobalPreferences.TV_ShowLines = ShowLinesTEMPORAL;
                GlobalPreferences.TV_ShowRootLines = ShowRootLinesTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveTreeViewPreferences();

                //Update Boolean
                FrmTreeViewPrefsOpened = false;
            }

            //-----------------[Frm_GeneralPreferences]-----------------
            if (FrmGeneralPreferencesOpened)
            {
                GlobalPreferences.WavesViewerControl_WavesColor = ColorWavesControlTEMPORAL;
                GlobalPreferences.WavesViewerControl_BackgroundColor = BackColorWavesControlTEMPORAL;
                GlobalPreferences.LoadLastLoadedESF = LoadLastLoadedESFTEMPORAL;
                GlobalPreferences.TV_IgnoreStlyesFromESF = TV_IgnoreStlyesFromESFTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveWavesControlColors();
                WRegistryFunctions.SaveAutomaticalyLoadLastESF();

                //Update Boolean
                FrmGeneralPreferencesOpened = false;
            }

            //-----------------[Frm_SoxPrefs]-----------------
            if (FrmSoXPreferencesOpened)
            {
                GlobalPreferences.SoXPath = SoXPathTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveSoxFilePath();

                //Update Boolean
                FrmSoXPreferencesOpened = false;
            }

            //-----------------[Frm_System]-----------------
            if (FrmSystemPreferencesOpened)
            {
                GlobalPreferences.UseSystemTray = UseSystemTrayTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveSystemConfig();

                //Update Boolean
                FrmSystemPreferencesOpened = false;
            }

            //-----------------[Frm_OutputDevices]-----------------
            if (FrmAudioDevicesOpened)
            {
                GlobalPreferences.DefaultAudioDevice = DefaultAudioDeviceTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveDefaultAudioDevice();

                //Update Boolean
                FrmAudioDevicesOpened = false;
            }

            //-----------------[Frm_Profiles]-----------------
            if (FrmProfilesFormOpened)
            {
                ProfilesFunctions PFunctions = new ProfilesFunctions();

                //Apply the selected profile
                if (!GlobalPreferences.SelectedProfileName.Equals(SelectedProfileNameTEMPORAL))
                {
                    PFunctions.ApplyProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName, true);
                }
                //Ask user if wants to reload the profile
                else
                {
                    DialogResult Answer = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ReloadProfileAgain"), "EuroSound", MessageBoxButtons.YesNo);
                    if (Answer == DialogResult.Yes)
                    {
                        PFunctions.ApplyProfile(SelectedProfileTEMPORAL, SelectedProfileNameTEMPORAL, true);
                    }
                }

                //Update Variables
                GlobalPreferences.SelectedProfile = SelectedProfileTEMPORAL;
                GlobalPreferences.SelectedProfileName = SelectedProfileNameTEMPORAL;

                //Save config in Registry
                WRegistryFunctions.SaveCurrentProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName);

                //Update Boolean
                FrmProfilesFormOpened = false;
            }
            Close();
        }

        private void TreeViewPreferences_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Open Sub-Form "Frm_TreeViewPrefs"
            if (string.Equals(e.Node.Name, "ESFTree"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(TreeViewPrefs))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    TreeViewPrefs = new Frm_TreeViewPrefs
                    {
                        TopLevel = false,
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(TreeViewPrefs);
                    TreeViewPrefs.Dock = DockStyle.Fill;
                    TreeViewPrefs.Show();

                    //Update Boolean
                    FrmTreeViewPrefsOpened = true;
                }
            }
            //Open Sub-Form "Frm_GeneralPreferences"
            else if (string.Equals(e.Node.Name, "General"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(GeneralPreferences))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    GeneralPreferences = new Frm_General
                    {
                        TopLevel = false,
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(GeneralPreferences);
                    GeneralPreferences.Dock = DockStyle.Fill;
                    GeneralPreferences.Show();

                    //Update Boolean
                    FrmGeneralPreferencesOpened = true;
                }
            }
            //Open Sub-Form "Frm_SoxPrefs"
            else if (string.Equals(e.Node.Name, "SoX"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(SoXPreferences))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    SoXPreferences = new Frm_SoxPrefs
                    {
                        TopLevel = false,
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(SoXPreferences);
                    SoXPreferences.Dock = DockStyle.Fill;
                    SoXPreferences.Show();

                    //Update Boolean
                    FrmSoXPreferencesOpened = true;
                }
            }
            //Open Sub-Form "Frm_System"
            else if (string.Equals(e.Node.Name, "System"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(SystemPreferences))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    SystemPreferences = new Frm_System
                    {
                        TopLevel = false,
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(SystemPreferences);
                    SystemPreferences.Dock = DockStyle.Fill;
                    SystemPreferences.Show();

                    //Update Boolean
                    FrmSystemPreferencesOpened = true;
                }
            }
            //Open Sub-Form "Frm_OutputDevices"
            else if (string.Equals(e.Node.Name, "AudioDevices"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(AudioDevices))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    AudioDevices = new Frm_OutputDevices
                    {
                        TopLevel = false,
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(AudioDevices);
                    AudioDevices.Dock = DockStyle.Fill;
                    AudioDevices.Show();

                    //Update Boolean
                    FrmAudioDevicesOpened = true;
                }
            }
            //Open Sub-Form "Frm_Profiles"
            else if (string.Equals(e.Node.Name, "Profile"))
            {
                RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                ProfilesForm = new Frm_Profiles
                {
                    TopLevel = false,
                    AutoScroll = true,
                    Tag = Tag
                };
                Panel_SecondaryForms.Controls.Add(ProfilesForm);
                ProfilesForm.Dock = DockStyle.Fill;
                ProfilesForm.Show();

                //Update Boolean
                FrmProfilesFormOpened = true;
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private static void RemoveAllFormsInsidePanel(Panel p_control)
        {
            Control.ControlCollection FormsToClose = p_control.Controls;
            if (FormsToClose.Count > 0)
            {
                for (int i = 0; i < FormsToClose.Count; i++)
                {
                    ((Form)FormsToClose[i]).Close();
                }
            }
        }
    }
}