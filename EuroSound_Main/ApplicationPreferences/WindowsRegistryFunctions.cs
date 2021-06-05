using EuroSound_Application.ApplicationPreferences;
using Microsoft.Win32;
using System;

namespace EuroSound_Application.ApplicationRegistryFunctions
{
    internal static class WindowsRegistryFunctions
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private static RegistryKey EurocomKey;
        private static RegistryKey EuroSoundKey;
        private static RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);

        //*===============================================================================================
        //* EUROCOM AND EUROSOUND KEYS
        //*===============================================================================================
        private static void OpenEuroSoundKeys()
        {
            CreateEurocomKeyIfNotExists();
            CreateEuroSoundKeyIfNotExists();
        }

        internal static void CreateEuroSoundKeyIfNotExists()
        {
            if (EurocomKey.OpenSubKey("EuroSound", true) == null)
            {
                EurocomKey.CreateSubKey("EuroSound");
            }
            EuroSoundKey = EurocomKey.OpenSubKey("EuroSound", true);
        }

        internal static void CreateEurocomKeyIfNotExists()
        {
            if (SoftwareKey.OpenSubKey("Eurocomm", true) == null)
            {
                SoftwareKey.CreateSubKey("Eurocomm");
            }
            EurocomKey = SoftwareKey.OpenSubKey("Eurocomm", true);
        }

        internal static void CreateEuroSoundSubkeyIfNotExists(string SubKeyName, bool Writable)
        {
            if (EuroSoundKey.OpenSubKey(SubKeyName, Writable) == null)
            {
                EuroSoundKey.CreateSubKey(SubKeyName);
            }
        }

        //*===============================================================================================
        //* GENERAL FUNCTIONS
        //*===============================================================================================
        internal static RegistryKey ReturnRegistryKey(string Name)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists(Name, true);
            RegistryKey KeyToReturn = EuroSoundKey.OpenSubKey(Name, true);

            return KeyToReturn;
        }

        //*===============================================================================================
        //* Updates Alerts
        //*===============================================================================================
        internal static void SaveUpdatesAlerts()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Updates", true);
            using (RegistryKey ShowAlerts = EuroSoundKey.OpenSubKey("Updates", true))
            {
                ShowAlerts.SetValue("ShowAlerts", GlobalPreferences.ShowUpdatesAlerts, RegistryValueKind.DWord);
                ShowAlerts.Close();
            }
        }

        internal static bool LoadUpdatesAlerts()
        {
            bool ShowAlerts = false;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Updates", true);
            using (RegistryKey ShowUpdatesAlerts = EuroSoundKey.OpenSubKey("Updates", true))
            {
                ShowAlerts = Convert.ToBoolean(int.Parse(ShowUpdatesAlerts.GetValue("ShowAlerts", 0).ToString()));
                ShowUpdatesAlerts.Close();
            }

            return ShowAlerts;
        }

        //*===============================================================================================
        //* Active Document
        //*===============================================================================================
        internal static void SaveActiveDocument(string currentDocument)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ActiveDocument", true);
            using (RegistryKey ActiveDocument = EuroSoundKey.OpenSubKey("ActiveDocument", true))
            {
                ActiveDocument.SetValue("Pathname", currentDocument, RegistryValueKind.String);
                ActiveDocument.Close();
            }
        }

        internal static string LoadActiveDocument()
        {
            string activeDocument = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ActiveDocument", true);
            using (RegistryKey ActiveDocumentKey = EuroSoundKey.OpenSubKey("ActiveDocument", true))
            {
                activeDocument = ActiveDocumentKey.GetValue("Pathname", string.Empty).ToString();
                ActiveDocumentKey.Close();
            }

            return activeDocument;
        }

        //*===============================================================================================
        //* Editing Options
        //*===============================================================================================
        internal static void SaveEditingOptions()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Editing", true);
            using (RegistryKey SaveEditing = EuroSoundKey.OpenSubKey("Editing", true))
            {
                SaveEditing.SetValue("AutoSortNodes", GlobalPreferences.AutomaticallySortNodes, RegistryValueKind.DWord);
                SaveEditing.SetValue("UseExtColorPicker", GlobalPreferences.UseExtendedColorPicker, RegistryValueKind.String);
                SaveEditing.Close();
            }
        }

        internal static bool LoadEditingOptions(string keyName)
        {
            bool editOption = false;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Editing", true);
            using (RegistryKey EditingOptions = EuroSoundKey.OpenSubKey("Editing", true))
            {
                editOption = Convert.ToBoolean(EditingOptions.GetValue(keyName, false));
                EditingOptions.Close();
            }

            return editOption;
        }

        //*===============================================================================================
        //* Current Profile
        //*===============================================================================================
        internal static void SaveCurrentProfile(string currentProfile, string currentProfileName)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Profile", true);
            using (RegistryKey SaveProfile = EuroSoundKey.OpenSubKey("Profile", true))
            {
                SaveProfile.SetValue("CurrentProfile", currentProfile, RegistryValueKind.String);
                SaveProfile.SetValue("CurrentProfileName", currentProfileName, RegistryValueKind.String);
                SaveProfile.SetValue("CurrentProfileMD5", GenericFunctions.CalculateMD5(currentProfile), RegistryValueKind.String);
                SaveProfile.Close();
            }
        }

        internal static string LoadCurrentProfie(string KeyName)
        {
            string currentProfile = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Profile", true);
            using (RegistryKey LoadProfile = EuroSoundKey.OpenSubKey("Profile", true))
            {
                currentProfile = LoadProfile.GetValue(KeyName, string.Empty).ToString();
                LoadProfile.Close();
            }

            return currentProfile;
        }

        //*===============================================================================================
        //* Prefixes
        //*===============================================================================================
        internal static void SaveHashCodesPrefixes(string Name, uint Prefix)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashCodesPrefixes", true);
            using (RegistryKey SavePrefix = EuroSoundKey.OpenSubKey("HashCodesPrefixes", true))
            {
                SavePrefix.SetValue(Name, Prefix, RegistryValueKind.DWord);
                SavePrefix.Close();
            }
        }

        internal static uint LoadHashCodesPrefixes(string KeyName)
        {
            uint hashcodePrefix = 0;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashCodesPrefixes", true);
            using (RegistryKey LoadPrefix = EuroSoundKey.OpenSubKey("HashCodesPrefixes", true))
            {
                hashcodePrefix = uint.Parse(LoadPrefix.GetValue(KeyName, 0).ToString());
                LoadPrefix.Close();
            }

            return hashcodePrefix;
        }

        //*===============================================================================================
        //* Sound Settings
        //*===============================================================================================
        internal static void SaveSoundSettings(string Prefix, int Frequency, string Encoding, int Bits, int Channels)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SoundSettings", true);
            using (RegistryKey SoundSettings = EuroSoundKey.OpenSubKey("SoundSettings", true))
            {
                //Save Values
                SoundSettings.SetValue(Prefix + "Frequency", Frequency, RegistryValueKind.DWord);
                SoundSettings.SetValue(Prefix + "Encoding", Encoding, RegistryValueKind.String);
                SoundSettings.SetValue(Prefix + "Bits", Bits, RegistryValueKind.DWord);
                SoundSettings.SetValue(Prefix + "Channels", Channels, RegistryValueKind.DWord);
                SoundSettings.Close();
            }
        }

        internal static string LoadSoundSettings(string Prefix, string KeyValueName)
        {
            string keyValue;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SoundSettings", true);
            using (RegistryKey SoundSettings = EuroSoundKey.OpenSubKey("SoundSettings", true))
            {
                //Save Values
                keyValue = SoundSettings.GetValue(Prefix + KeyValueName, "").ToString();
                SoundSettings.Close();
            }

            return keyValue;
        }

        //*===============================================================================================
        //* HASHTABLES FILES
        //*===============================================================================================
        internal static void SaveHashtablesFiles(string Prefix, string FilePath, string FileMD5)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashTablesFiles", true);
            using (RegistryKey Hashtables = EuroSoundKey.OpenSubKey("HashTablesFiles", true))
            {
                //Save Values
                Hashtables.SetValue(Prefix + "Path", FilePath, RegistryValueKind.String);
                Hashtables.SetValue(Prefix + "MD5", FileMD5, RegistryValueKind.String);
                Hashtables.Close();
            }
        }

        internal static string LoadHashtablesFiles(string Prefix, string KeyValueName)
        {
            string keyValue;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashTablesFiles", true);
            using (RegistryKey Hashtables = EuroSoundKey.OpenSubKey("HashTablesFiles", true))
            {
                //Save Values
                keyValue = Hashtables.GetValue(Prefix + KeyValueName, "").ToString();
                Hashtables.Close();
            }

            return keyValue;
        }

        //*===============================================================================================
        //* EXTERNAL FILES
        //*===============================================================================================
        internal static void SaveExternalFiles(string Prefix, string KeyValueName, string KeyValue)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ExternalFiles", true);
            using (RegistryKey ExternalFiles = EuroSoundKey.OpenSubKey("ExternalFiles", true))
            {
                //Save Values
                ExternalFiles.SetValue(Prefix + KeyValueName, KeyValue, RegistryValueKind.String);
                ExternalFiles.Close();
            }
        }

        internal static string LoadExternalFiles(string Prefix, string KeyValueName)
        {
            string folderPath;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ExternalFiles", true);
            using (RegistryKey ExternalFiles = EuroSoundKey.OpenSubKey("ExternalFiles", true))
            {
                //Save Values
                folderPath = ExternalFiles.GetValue(Prefix + KeyValueName, "").ToString();
                ExternalFiles.Close();
            }

            return folderPath;
        }

        //*===============================================================================================
        //* OUTPUT FOLDERS
        //*===============================================================================================
        internal static void SaveOutputFolders(string Prefix, string KeyValueName, string KeyValue)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("OutputFolders", true);
            using (RegistryKey OutputFolders = EuroSoundKey.OpenSubKey("OutputFolders", true))
            {
                //Save Values
                OutputFolders.SetValue(Prefix + KeyValueName, KeyValue, RegistryValueKind.String);
                OutputFolders.Close();
            }
        }

        internal static string LoadOutputFolders(string Prefix, string KeyValueName)
        {
            string folderPath;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("OutputFolders", true);
            using (RegistryKey OutputFolders = EuroSoundKey.OpenSubKey("OutputFolders", true))
            {
                //Save Values
                folderPath = OutputFolders.GetValue(Prefix + KeyValueName, "").ToString();
                OutputFolders.Close();
            }

            return folderPath;
        }

        //*===============================================================================================
        //* USER SETTINGS -> System Config
        //*===============================================================================================
        internal static void SaveSystemConfig()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey SystemConfig = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Values
                SystemConfig.SetValue("UseSysTray", GlobalPreferences.UseSystemTray, RegistryValueKind.DWord);
                SystemConfig.Close();
            }
        }

        internal static int GetSystemConfig(string ValueName)
        {
            int selectedValue;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey SystemConfig = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Values
                selectedValue = (int)SystemConfig.GetValue(ValueName, 0);
                SystemConfig.Close();
            }
            return selectedValue;
        }

        //*===============================================================================================
        //* Back-Ups Settings
        //*===============================================================================================
        internal static void SaveBackupSettings()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Backup", true);
            using (RegistryKey BackupSettings = EuroSoundKey.OpenSubKey("Backup", true))
            {
                //Save Values
                BackupSettings.SetValue("MakeBackups", GlobalPreferences.MakeBackups, RegistryValueKind.DWord);
                BackupSettings.SetValue("BackupsFolder", GlobalPreferences.MakeBackupsDirectory, RegistryValueKind.String);
                BackupSettings.SetValue("MaxBackups", GlobalPreferences.MakeBackupsMaxNumber, RegistryValueKind.DWord);
                BackupSettings.SetValue("BackupsInterval", GlobalPreferences.MakeBackupsInterval, RegistryValueKind.DWord);
                BackupSettings.SetValue("NextBackup", GlobalPreferences.MakeBackupsIndex, RegistryValueKind.DWord);
                BackupSettings.Close();
            }
        }

        internal static string LoadBackupSettings(string ValueName)
        {
            string requestValue = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Backup", true);
            using (RegistryKey BackupSettings = EuroSoundKey.OpenSubKey("Backup", true))
            {
                //Load Values
                if (BackupSettings.GetValue(ValueName) != null)
                {
                    requestValue = BackupSettings.GetValue(ValueName).ToString();
                    BackupSettings.Close();
                }
                //Default Values
                else if (ValueName.Equals("BackupsFolder"))
                {
                    requestValue = string.Empty;
                }
                else
                {
                    requestValue = "0";
                }
            }
            return requestValue;
        }

        //*===============================================================================================
        //* USER SETTINGS -> TREE VIEW
        //*===============================================================================================
        internal static void SaveTreeViewPreferences()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey TreeViewPrefs = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Values
                TreeViewPrefs.SetValue("TV_SelectedFont", GlobalPreferences.TV_SelectedFont, RegistryValueKind.String);
                TreeViewPrefs.SetValue("TV_Indent", GlobalPreferences.TV_Indent, RegistryValueKind.DWord);
                TreeViewPrefs.SetValue("TV_ItemHeight", GlobalPreferences.TV_ItemHeight, RegistryValueKind.DWord);
                TreeViewPrefs.SetValue("TV_ShowLines", GlobalPreferences.TV_ShowLines, RegistryValueKind.DWord);
                TreeViewPrefs.SetValue("TV_ShowRootLines", GlobalPreferences.TV_ShowRootLines, RegistryValueKind.DWord);
                TreeViewPrefs.Close();
            }
        }

        internal static string LoadTreeViewPreferences(string ValueName)
        {
            string requestValue = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey TreeViewPrefs = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Values
                if (TreeViewPrefs.GetValue(ValueName) != null)
                {
                    requestValue = TreeViewPrefs.GetValue(ValueName).ToString();
                    TreeViewPrefs.Close();
                }
                //Default Values
                else if (ValueName.Equals("TV_ShowLines"))
                {
                    requestValue = "1";
                }
                else if (ValueName.Equals("TV_ShowRootLines"))
                {
                    requestValue = "1";
                }
                else if (ValueName.Equals("TV_Indent"))
                {
                    requestValue = "19";
                }
                else if (ValueName.Equals("TV_ItemHeight"))
                {
                    requestValue = "16";
                }
                else if (ValueName.Equals("TV_SelectedFont"))
                {
                    requestValue = "Microsoft Sans Serif; 8,25pt";
                }
                else if (ValueName.Equals("TV_IgnoreStlyesFromESF"))
                {
                    requestValue = "0";
                }
            }
            return requestValue;
        }

        //*===============================================================================================
        //* USER SETTINGS -> WAVES CONTROL
        //*===============================================================================================
        internal static void SaveWavesControlColors()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey WavesColorKey = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Value
                WavesColorKey.SetValue("WavesColors", GlobalPreferences.WavesViewerControl_WavesColor, RegistryValueKind.DWord);
                WavesColorKey.SetValue("BackgroundColor", GlobalPreferences.WavesViewerControl_BackgroundColor, RegistryValueKind.DWord);
                WavesColorKey.Close();
            }
        }

        internal static int LoadWavesControlColors(string ValueName)
        {
            int currentColor = 0;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey WavesControlKey = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Values
                if (WavesControlKey.GetValue(ValueName) != null)
                {
                    currentColor = (int)WavesControlKey.GetValue(ValueName);
                    WavesControlKey.Close();
                }
                //Default Values
                else if (ValueName.Equals("WavesColors"))
                {
                    currentColor = -16777077;
                }
                else if (ValueName.Equals("BackgroundColor"))
                {
                    currentColor = -8355712;
                }
            }

            return currentColor;
        }

        //*===============================================================================================
        //* USER SETTINGS -> DEFAULT AUDIO DEVICE OUTPUT
        //*===============================================================================================
        internal static void SaveDefaultAudioDevice()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey AudioDevice = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Value
                AudioDevice.SetValue("AudioDevIndex", GlobalPreferences.DefaultAudioDevice, RegistryValueKind.DWord);
                AudioDevice.Close();
            }
        }

        internal static int LoadDefaultAudioDevice()
        {
            int audioDeviceNum = 0;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey AudioDevice = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Value
                if (AudioDevice.GetValue("AudioDevIndex") != null)
                {
                    audioDeviceNum = (int)AudioDevice.GetValue("AudioDevIndex");
                    AudioDevice.Close();
                }
                //Default Values
                else
                {
                    audioDeviceNum = 0;
                }
            }

            return audioDeviceNum;
        }

        //*===============================================================================================
        //* OTHERS -> WINDOW STATE
        //*===============================================================================================
        internal static void SaveWindowState(string WindowName, int LocationX, int LocationY, int Width, int Height, bool IsIconic, bool IsMaximized, int SplitterDist, int SplitterInfoDist)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("WindowState", true);
            using (RegistryKey SaveWindowState = EuroSoundKey.OpenSubKey("WindowState", true))
            {
                SaveWindowState.SetValue(WindowName + "_PositionX", LocationX, RegistryValueKind.DWord);
                SaveWindowState.SetValue(WindowName + "_PositionY", LocationY, RegistryValueKind.DWord);
                SaveWindowState.SetValue(WindowName + "_IsIconic", IsIconic, RegistryValueKind.DWord);
                SaveWindowState.SetValue(WindowName + "_IsMaximized", IsMaximized, RegistryValueKind.DWord);
                SaveWindowState.SetValue(WindowName + "_Width", Width, RegistryValueKind.DWord);
                SaveWindowState.SetValue(WindowName + "_Height", Height, RegistryValueKind.DWord);
                if (SplitterDist > 0)
                {
                    SaveWindowState.SetValue(WindowName + "_SplitterDistance", SplitterDist, RegistryValueKind.DWord);
                }
                if (SplitterInfoDist > 0)
                {
                    SaveWindowState.SetValue(WindowName + "_SplitterInfoDistance", SplitterInfoDist, RegistryValueKind.DWord);
                }
                SaveWindowState.Close();
            }
        }

        //*===============================================================================================
        //* OTHERS -> FOLDER BROWSER LAST PATH
        //*===============================================================================================
        internal static void SaveFolderBrowserLastPath(string SelectedPath)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("FolderBrowserLastPath", true);
            using (RegistryKey LastSelectedPath = EuroSoundKey.OpenSubKey("FolderBrowserLastPath", true))
            {
                LastSelectedPath.SetValue("LastPath", SelectedPath, RegistryValueKind.String);
                LastSelectedPath.Close();
            }
        }

        internal static string LoadFolderBrowserLastPath()
        {
            string lastPath = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("FolderBrowserLastPath", true);
            using (RegistryKey LastSelectedPath = EuroSoundKey.OpenSubKey("FolderBrowserLastPath", true))
            {
                lastPath = LastSelectedPath.GetValue("LastPath", "").ToString();
                LastSelectedPath.Close();
            }

            return lastPath;
        }

        //*===============================================================================================
        //* USER SETTINGS -> Load Last ESF And IgnoreStyles
        //*===============================================================================================
        internal static void SaveAutomaticalyLoadLastESF()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey LoadLastESF = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Values
                LoadLastESF.SetValue("AutomaticalyLoadLastESF", GlobalPreferences.LoadLastLoadedESF, RegistryValueKind.DWord);
                LoadLastESF.SetValue("TV_IgnoreStlyesFromESF", GlobalPreferences.TV_IgnoreStlyesFromESF, RegistryValueKind.DWord);
                LoadLastESF.SetValue("UseThreadingWhenLoad", GlobalPreferences.UseThreadingWhenLoad, RegistryValueKind.DWord);
                LoadLastESF.SetValue("UseVisualStyles", GlobalPreferences.EnableAppVisualStyles, RegistryValueKind.DWord);
                LoadLastESF.Close();
            }
        }

        internal static bool LoadAutomaticalyLoadLastESF(string KeyWord)
        {
            bool loadLastESFChecked = false;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey LoadLastESF = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                loadLastESFChecked = Convert.ToBoolean(int.Parse(LoadLastESF.GetValue(KeyWord, "0").ToString()));
                LoadLastESF.Close();
            }
            if (KeyWord.Equals("UseVisualStyles"))
            {
                loadLastESFChecked = true;
            }
            return loadLastESFChecked;
        }

        //*===============================================================================================
        //* USER SETTINGS -> Output Settings
        //*===============================================================================================
        internal static void SaveOutputSettings()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey OutputSettings = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                OutputSettings.SetValue("PlayOutputSoundFilePath", GlobalPreferences.OutputSoundPath, RegistryValueKind.String);
                OutputSettings.SetValue("PlaySoundWhenOutput", GlobalPreferences.PlaySoundWhenOutput, RegistryValueKind.DWord);
                OutputSettings.Close();
            }
        }

        internal static string LoadSaveOutputSettings(string KeyValueName, string DefaultValue)
        {
            string folderPath;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey OutputSettings = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Values
                folderPath = OutputSettings.GetValue(KeyValueName, DefaultValue).ToString();
                OutputSettings.Close();
            }

            return folderPath;
        }

        //*===============================================================================================
        //* OTHERS -> CUSTOM COLORS
        //*===============================================================================================
        internal static void SaveCustomColors(int[] CustomColors)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("CustomColors", true);
            using (RegistryKey CustomColorKey = EuroSoundKey.OpenSubKey("CustomColors", true))
            {
                for (int i = 0; i < CustomColors.Length; i++)
                {
                    CustomColorKey.SetValue("CustCol" + i, CustomColors[i], RegistryValueKind.DWord);
                }
                CustomColorKey.Close();
            }
        }

        internal static int[] LoadCustomColors()
        {
            int[] customColors = new int[16];

            OpenEuroSoundKeys();
            using (RegistryKey CustomColorKey = EuroSoundKey.OpenSubKey("CustomColors", true))
            {
                if (CustomColorKey != null)
                {
                    for (int i = 0; i < customColors.Length; i++)
                    {
                        if (CustomColorKey.GetValue("CustCol" + i) != null)
                        {
                            customColors[i] = int.Parse(CustomColorKey.GetValue("CustCol" + i).ToString());
                        }
                        else
                        {
                            customColors[i] = 16777215;
                        }
                    }
                    CustomColorKey.Close();
                }
                else
                {
                    for (int i = 0; i < customColors.Length; i++)
                    {
                        customColors[i] = 16777215;
                    }
                }
            }
            return customColors;
        }

        //*===============================================================================================
        //* FLAGS
        //*===============================================================================================
        internal static void SaveFlags(string[] Flags, string FolderName)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists(FolderName, true);
            using (RegistryKey FlagsKey = EuroSoundKey.OpenSubKey(FolderName, true))
            {
                for (int i = 0; i < Flags.Length; i++)
                {
                    FlagsKey.SetValue("Flag" + (i + 1), Flags[i], RegistryValueKind.String);
                }
                FlagsKey.Close();
            }
        }
    }
}