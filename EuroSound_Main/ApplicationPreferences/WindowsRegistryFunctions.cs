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
        //* Active Document
        //*===============================================================================================
        internal static void SaveActiveDocument(string CurrentDocument)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ActiveDocument", true);
            using (RegistryKey ActiveDocument = EuroSoundKey.OpenSubKey("ActiveDocument", true))
            {
                ActiveDocument.SetValue("Pathname", CurrentDocument, RegistryValueKind.String);
                ActiveDocument.Close();
            }
        }

        internal static string LoadActiveDocument()
        {
            string ActiveDocument = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ActiveDocument", true);
            using (RegistryKey ActiveDocumentKey = EuroSoundKey.OpenSubKey("ActiveDocument", true))
            {
                ActiveDocument = ActiveDocumentKey.GetValue("Pathname", string.Empty).ToString();
                ActiveDocumentKey.Close();
            }

            return ActiveDocument;
        }

        //*===============================================================================================
        //* Current Profile
        //*===============================================================================================
        internal static void SaveCurrentProfile(string CurrentProfile, string CurrentProfileName)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Profile", true);
            using (RegistryKey SaveProfile = EuroSoundKey.OpenSubKey("Profile", true))
            {
                SaveProfile.SetValue("CurrentProfile", CurrentProfile, RegistryValueKind.String);
                SaveProfile.SetValue("CurrentProfileName", CurrentProfileName, RegistryValueKind.String);
                SaveProfile.Close();
            }
        }

        internal static string LoadCurrentProfie(string KeyName)
        {
            string CurrentProfile = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("Profile", true);
            using (RegistryKey LoadProfile = EuroSoundKey.OpenSubKey("Profile", true))
            {
                CurrentProfile = LoadProfile.GetValue(KeyName, string.Empty).ToString();
                LoadProfile.Close();
            }

            return CurrentProfile;
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
            string KValue;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SoundSettings", true);
            using (RegistryKey SoundSettings = EuroSoundKey.OpenSubKey("SoundSettings", true))
            {
                //Save Values
                KValue = SoundSettings.GetValue(Prefix + KeyValueName, "").ToString();
                SoundSettings.Close();
            }

            return KValue;
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
            string KValue;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashTablesFiles", true);
            using (RegistryKey Hashtables = EuroSoundKey.OpenSubKey("HashTablesFiles", true))
            {
                //Save Values
                KValue = Hashtables.GetValue(Prefix + KeyValueName, "").ToString();
                Hashtables.Close();
            }

            return KValue;
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
            string FolderPath;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("ExternalFiles", true);
            using (RegistryKey ExternalFiles = EuroSoundKey.OpenSubKey("ExternalFiles", true))
            {
                //Save Values
                FolderPath = ExternalFiles.GetValue(Prefix + KeyValueName, "").ToString();
                ExternalFiles.Close();
            }

            return FolderPath;
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
            string FolderPath;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("OutputFolders", true);
            using (RegistryKey OutputFolders = EuroSoundKey.OpenSubKey("OutputFolders", true))
            {
                //Save Values
                FolderPath = OutputFolders.GetValue(Prefix + KeyValueName, "").ToString();
                OutputFolders.Close();
            }

            return FolderPath;
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
            int SelectedValue;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey SystemConfig = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Values
                SelectedValue = (int)SystemConfig.GetValue(ValueName, 0);
                SystemConfig.Close();
            }
            return SelectedValue;
        }

        //*===============================================================================================
        //* USER SETTINGS -> SOX PATH
        //*===============================================================================================
        internal static void SaveSoxFilePath()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey SoxPath = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Values
                SoxPath.SetValue("SoxExePath", GlobalPreferences.SoXPath, RegistryValueKind.String);
                SoxPath.Close();
            }
        }

        internal static string LoadSoxFilePath()
        {
            string SoxFilePath = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey SoxPath = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                SoxFilePath = SoxPath.GetValue("SoxExePath", "").ToString();
                SoxPath.Close();
            }
            return SoxFilePath;
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
            string RequestValue = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey TreeViewPrefs = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Values
                if (TreeViewPrefs.GetValue(ValueName) != null)
                {
                    RequestValue = TreeViewPrefs.GetValue(ValueName).ToString();
                    TreeViewPrefs.Close();
                }
                //Default Values
                else if (ValueName.Equals("TV_ShowLines"))
                {
                    RequestValue = "1";
                }
                else if (ValueName.Equals("TV_ShowRootLines"))
                {
                    RequestValue = "1";
                }
                else if (ValueName.Equals("TV_Indent"))
                {
                    RequestValue = "19";
                }
                else if (ValueName.Equals("TV_ItemHeight"))
                {
                    RequestValue = "16";
                }
                else if (ValueName.Equals("TV_SelectedFont"))
                {
                    RequestValue = "Microsoft Sans Serif; 8,25pt";
                }
                else if (ValueName.Equals("TV_IgnoreStlyesFromESF"))
                {
                    RequestValue = "0";
                }
            }
            return RequestValue;
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
            int CurrentColor = 0;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey WavesControlKey = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Values
                if (WavesControlKey.GetValue(ValueName) != null)
                {
                    CurrentColor = (int)WavesControlKey.GetValue(ValueName);
                    WavesControlKey.Close();
                }
                //Default Values
                else if (ValueName.Equals("WavesColors"))
                {
                    CurrentColor = -16777077;
                }
                else if (ValueName.Equals("BackgroundColor"))
                {
                    CurrentColor = -8355712;
                }
            }

            return CurrentColor;
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
            int AudioDeviceNum = 0;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey AudioDevice = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Load Value
                if (AudioDevice.GetValue("AudioDevIndex") != null)
                {
                    AudioDeviceNum = (int)AudioDevice.GetValue("AudioDevIndex");
                    AudioDevice.Close();
                }
                //Default Values
                else
                {
                    AudioDeviceNum = 0;
                }
            }

            return AudioDeviceNum;
        }

        //*===============================================================================================
        //* OTHERS -> WINDOW STATE
        //*===============================================================================================
        internal static void SaveWindowState(string WindowName, int LocationX, int LocationY, int Width, int Height, bool IsIconic, bool IsMaximized, int SplitterDist)
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
            string LastPath = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("FolderBrowserLastPath", true);
            using (RegistryKey LastSelectedPath = EuroSoundKey.OpenSubKey("FolderBrowserLastPath", true))
            {
                LastPath = LastSelectedPath.GetValue("LastPath", "").ToString();
                LastSelectedPath.Close();
            }

            return LastPath;
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
                LoadLastESF.Close();
            }
        }

        internal static bool LoadAutomaticalyLoadLastESF(string KeyWord)
        {
            bool LoadLastESFChecked = false;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey LoadLastESF = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                LoadLastESFChecked = Convert.ToBoolean(int.Parse(LoadLastESF.GetValue(KeyWord, "0").ToString()));
                LoadLastESF.Close();
            }
            return LoadLastESFChecked;
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
            string FolderPath;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("UserSettings", true);
            using (RegistryKey OutputSettings = EuroSoundKey.OpenSubKey("UserSettings", true))
            {
                //Save Values
                FolderPath = OutputSettings.GetValue(KeyValueName, DefaultValue).ToString();
                OutputSettings.Close();
            }

            return FolderPath;
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
            int[] CustomColors = new int[16];

            OpenEuroSoundKeys();
            using (RegistryKey CustomColorKey = EuroSoundKey.OpenSubKey("CustomColors", true))
            {
                if (CustomColorKey != null)
                {
                    for (int i = 0; i < CustomColors.Length; i++)
                    {
                        if (CustomColorKey.GetValue("CustCol" + i) != null)
                        {
                            CustomColors[i] = int.Parse(CustomColorKey.GetValue("CustCol" + i).ToString());
                        }
                        else
                        {
                            CustomColors[i] = 16777215;
                        }
                    }
                    CustomColorKey.Close();
                }
                else
                {
                    for (int i = 0; i < CustomColors.Length; i++)
                    {
                        CustomColors[i] = 16777215;
                    }
                }
            }
            return CustomColors;
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