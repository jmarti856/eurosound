using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.ApplicationRegistryFunctions;
using System;
using System.IO;
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
        internal int ColorWavesControlTEMPORAL;
        internal int BackColorWavesControlTEMPORAL;
        internal bool UseThreadingWhenLoadTEMPORAL;

        //Frm_StreamFile
        internal string StreamFilePathTEMPORAL;

        //Frm_System
        internal bool UseSystemTrayTEMPORAL;

        //Frm_OutputDevicecs
        internal int DefaultAudioDeviceTEMPORAL;

        //Frm_Profiles
        internal string SelectedProfileTEMPORAL;
        internal string SelectedProfileNameTEMPORAL;

        //Frm_OutputSettings
        internal bool PlaySoundWhenOutputTEMPORAL;
        internal string OutputSoundPathTEMPORAL;

        //Frm_AutoBackUps
        internal bool MakeBackupsTEMPORAL;
        internal string MakeBackupsDirectoryTEMPORAL;
        internal int MakeBackupsMaxNumberTEMPORAL;
        internal int MakeBackupsIntervalTEMPORAL;

        //Frm_EditingOptions
        internal bool AutomaticallySortNodesTEMPORAL;
        internal bool UseExtendedColorPickerTEMPORAL;

        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private Frm_General GeneralPreferences;
        private Frm_TreeViewPrefs TreeViewPrefs;
        private Frm_System SystemPreferences;
        private Frm_OutputDevices AudioDevices;
        private Frm_Profiles ProfilesForm;
        private Frm_OutputSettings OutputSettingsForm;
        private Frm_AutoBackUps BackupsSettingsForm;
        private Frm_EditingOptions EditingOptionsForm;
        private bool FrmGeneralPreferencesOpened = false;
        private bool FrmTreeViewPrefsOpened = false;
        private bool FrmSystemPreferencesOpened = false;
        private bool FrmAudioDevicesOpened = false;
        private bool FrmProfilesFormOpened = false;
        private bool FrmOutputSettingsFormOpened = false;
        private bool FrmBackupsSettingsFormOpened = false;
        private bool EditingOptionsFormOpened = false;

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
            SelectedFontTEMPORAL = GlobalPreferences.TV_SelectedFont;
            ShowLinesTEMPORAL = GlobalPreferences.TV_ShowLines;
            ShowRootLinesTEMPORAL = GlobalPreferences.TV_ShowRootLines;
            StreamFilePathTEMPORAL = GlobalPreferences.StreamFilePath;
            TreeViewIndentTEMPORAL = GlobalPreferences.TV_Indent;
            TreeViewItemHeightTEMPORAL = GlobalPreferences.TV_ItemHeight;
            UseSystemTrayTEMPORAL = GlobalPreferences.UseSystemTray;
            SelectedProfileTEMPORAL = GlobalPreferences.SelectedProfile;
            SelectedProfileNameTEMPORAL = GlobalPreferences.SelectedProfileName;
            TV_IgnoreStlyesFromESFTEMPORAL = GlobalPreferences.TV_IgnoreStlyesFromESF;
            LoadLastLoadedESFTEMPORAL = GlobalPreferences.LoadLastLoadedESF;
            PlaySoundWhenOutputTEMPORAL = GlobalPreferences.PlaySoundWhenOutput;
            OutputSoundPathTEMPORAL = GlobalPreferences.OutputSoundPath;
            UseThreadingWhenLoadTEMPORAL = GlobalPreferences.UseThreadingWhenLoad;
            MakeBackupsTEMPORAL = GlobalPreferences.MakeBackups;
            MakeBackupsDirectoryTEMPORAL = GlobalPreferences.MakeBackupsDirectory;
            MakeBackupsMaxNumberTEMPORAL = GlobalPreferences.MakeBackupsMaxNumber;
            MakeBackupsIntervalTEMPORAL = GlobalPreferences.MakeBackupsInterval;
            AutomaticallySortNodesTEMPORAL = GlobalPreferences.AutomaticallySortNodes;
            UseExtendedColorPickerTEMPORAL = GlobalPreferences.UseExtendedColorPicker;

            TreeViewPreferences.ExpandAll();
        }

        private void Frm_MainPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveAllFormsInsidePanel(Panel_SecondaryForms);
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
            RemoveAllFormsInsidePanel(Panel_SecondaryForms);

            //-----------------[Frm_TreeViewPrefs]-----------------
            if (FrmTreeViewPrefsOpened)
            {
                //Update Variables
                GlobalPreferences.TV_SelectedFont = SelectedFontTEMPORAL;
                GlobalPreferences.TV_Indent = TreeViewIndentTEMPORAL;
                GlobalPreferences.TV_ItemHeight = TreeViewItemHeightTEMPORAL;
                GlobalPreferences.TV_ShowLines = ShowLinesTEMPORAL;
                GlobalPreferences.TV_ShowRootLines = ShowRootLinesTEMPORAL;

                //SaveConfig in Registry
                WindowsRegistryFunctions.SaveTreeViewPreferences();

                //Update Boolean
                FrmTreeViewPrefsOpened = false;
            }

            //-----------------[Frm_GeneralPreferences]-----------------
            if (FrmGeneralPreferencesOpened)
            {
                //Update Variables
                GlobalPreferences.WavesViewerControl_WavesColor = ColorWavesControlTEMPORAL;
                GlobalPreferences.WavesViewerControl_BackgroundColor = BackColorWavesControlTEMPORAL;
                GlobalPreferences.LoadLastLoadedESF = LoadLastLoadedESFTEMPORAL;
                GlobalPreferences.TV_IgnoreStlyesFromESF = TV_IgnoreStlyesFromESFTEMPORAL;
                GlobalPreferences.UseThreadingWhenLoad = UseThreadingWhenLoadTEMPORAL;

                //SaveConfig in Registry
                WindowsRegistryFunctions.SaveWavesControlColors();
                WindowsRegistryFunctions.SaveAutomaticalyLoadLastESF();

                //Update Boolean
                FrmGeneralPreferencesOpened = false;
            }

            //-----------------[Frm_System]-----------------
            if (FrmSystemPreferencesOpened)
            {
                //Update Variables
                GlobalPreferences.UseSystemTray = UseSystemTrayTEMPORAL;

                //SaveConfig in Registry
                WindowsRegistryFunctions.SaveSystemConfig();

                //Update Boolean
                FrmSystemPreferencesOpened = false;
            }

            //-----------------[Frm_OutputDevices]-----------------
            if (FrmAudioDevicesOpened)
            {
                //Update Variables
                GlobalPreferences.DefaultAudioDevice = DefaultAudioDeviceTEMPORAL;

                //SaveConfig in Registry
                WindowsRegistryFunctions.SaveDefaultAudioDevice();

                //Update Boolean
                FrmAudioDevicesOpened = false;
            }

            //-----------------[Frm_Profiles]-----------------
            if (FrmProfilesFormOpened)
            {
                ProfilesFunctions PFunctions = new ProfilesFunctions();

                //Reload profile
                if (GlobalPreferences.SelectedProfileName.Equals(SelectedProfileNameTEMPORAL))
                {
                    if (File.Exists(GlobalPreferences.SelectedProfile))
                    {
                        PFunctions.ApplyProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName, true);
                    }
                }
                //Apply selected profile
                else
                {
                    DialogResult Answer = MessageBox.Show(string.Join("", "Profile has changed from [", GlobalPreferences.SelectedProfileName, "] to [", SelectedProfileNameTEMPORAL, "].\n\nDo you want to apply change?"), "EuroSound", MessageBoxButtons.YesNo);
                    if (Answer == DialogResult.Yes)
                    {
                        //Update Variables
                        GlobalPreferences.SelectedProfile = SelectedProfileTEMPORAL;
                        GlobalPreferences.SelectedProfileName = SelectedProfileNameTEMPORAL;

                        //Save config in Registry
                        WindowsRegistryFunctions.SaveCurrentProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName);

                        //Apply Profile
                        if (File.Exists(SelectedProfileTEMPORAL))
                        {
                            PFunctions.ApplyProfile(SelectedProfileTEMPORAL, SelectedProfileNameTEMPORAL, true);
                        }
                    }
                }

                //Update Boolean
                FrmProfilesFormOpened = false;
            }

            //-----------------[Frm_OutputSettings]-----------------
            if (FrmOutputSettingsFormOpened)
            {
                //Update Variables
                GlobalPreferences.PlaySoundWhenOutput = PlaySoundWhenOutputTEMPORAL;
                GlobalPreferences.OutputSoundPath = OutputSoundPathTEMPORAL;

                //Save config in Registry
                WindowsRegistryFunctions.SaveOutputSettings();

                //Update Boolean 
                FrmOutputSettingsFormOpened = false;
            }

            //-----------------[BackupsSettingsForm]-----------------
            if (FrmBackupsSettingsFormOpened)
            {
                //Update Variables
                GlobalPreferences.MakeBackups = MakeBackupsTEMPORAL;
                GlobalPreferences.MakeBackupsDirectory = MakeBackupsDirectoryTEMPORAL;
                GlobalPreferences.MakeBackupsInterval = MakeBackupsIntervalTEMPORAL;
                GlobalPreferences.MakeBackupsMaxNumber = MakeBackupsMaxNumberTEMPORAL;

                //Save config in Registry
                WindowsRegistryFunctions.SaveBackupSettings();

                //Update Boolean 
                FrmBackupsSettingsFormOpened = false;
            }

            //-----------------[Frm_EditingOptions]-----------------
            if (EditingOptionsFormOpened)
            {
                //Update Variables 
                GlobalPreferences.AutomaticallySortNodes = AutomaticallySortNodesTEMPORAL;
                GlobalPreferences.UseExtendedColorPicker = UseExtendedColorPickerTEMPORAL;

                //Save config in Registry
                WindowsRegistryFunctions.SaveEditingOptions();

                //Update Boolean 
                EditingOptionsFormOpened = false;
            }

            Close();
        }

        private void TreeViewPreferences_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Open Sub-Form "Frm_TreeViewPrefs"
            if (string.Equals(e.Node.Name, "ESFTree"))
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
            //Open Sub-Form "Frm_GeneralPreferences"
            else if (string.Equals(e.Node.Name, "General"))
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
            //Open Sub-Form "Frm_System"
            else if (string.Equals(e.Node.Name, "System"))
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
            //Open Sub-Form "Frm_OutputDevices"
            else if (string.Equals(e.Node.Name, "AudioDevices"))
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
            //Open Sub-Form "Frm_OutputSettings"
            else if (string.Equals(e.Node.Name, "Output"))
            {
                RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                OutputSettingsForm = new Frm_OutputSettings
                {
                    TopLevel = false,
                    AutoScroll = true,
                    Tag = Tag
                };
                Panel_SecondaryForms.Controls.Add(OutputSettingsForm);
                OutputSettingsForm.Dock = DockStyle.Fill;
                OutputSettingsForm.Show();

                //Update Boolean
                FrmOutputSettingsFormOpened = true;
            }
            //Open Sub-Form "Frm_AutoBackUps"
            else if (string.Equals(e.Node.Name, "AutoBackup"))
            {
                RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                BackupsSettingsForm = new Frm_AutoBackUps
                {
                    TopLevel = false,
                    AutoScroll = true,
                    Tag = Tag
                };
                Panel_SecondaryForms.Controls.Add(BackupsSettingsForm);
                BackupsSettingsForm.Dock = DockStyle.Fill;
                BackupsSettingsForm.Show();

                //Update Boolean
                FrmBackupsSettingsFormOpened = true;
            }
            //Open Sub-Form "Frm_EditingOptions"
            else if (string.Equals(e.Node.Name, "Editing"))
            {
                RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                EditingOptionsForm = new Frm_EditingOptions
                {
                    TopLevel = false,
                    AutoScroll = true,
                    Tag = Tag
                };
                Panel_SecondaryForms.Controls.Add(EditingOptionsForm);
                EditingOptionsForm.Dock = DockStyle.Fill;
                EditingOptionsForm.Show();

                //Update Boolean
                EditingOptionsFormOpened = true;
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