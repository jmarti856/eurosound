using EuroSound_Application.ApplicationPreferences;
using Microsoft.Win32;

namespace EuroSound_Application.ApplicationRegistryFunctions
{
    internal class WindowsRegistryFunctions
    {
        private RegistryKey EurocomKey;
        private RegistryKey EuroSoundKey;
        private RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);

        private void OpenEuroSoundKeys()
        {
            CreateEurocomKeyIfNotExists();
            CreateEuroSoundKeyIfNotExists();
        }

        #region Create Keys and SubKeys
        internal void CreateEuroSoundKeyIfNotExists()
        {
            if (EurocomKey.OpenSubKey("EuroSound", true) == null)
            {
                EurocomKey.CreateSubKey("EuroSound");
            }
            EuroSoundKey = EurocomKey.OpenSubKey("EuroSound", true);
        }

        internal void CreateEurocomKeyIfNotExists()
        {
            if (SoftwareKey.OpenSubKey("Eurocomm", true) == null)
            {
                SoftwareKey.CreateSubKey("Eurocomm");
            }
            EurocomKey = SoftwareKey.OpenSubKey("Eurocomm", true);
        }

        internal void CreateEuroSoundSubkeyIfNotExists(string SubKeyName, bool Writable)
        {
            if (EuroSoundKey.OpenSubKey(SubKeyName, Writable) == null)
            {
                EuroSoundKey.CreateSubKey(SubKeyName);
            }
        }

        #endregion Create Keys and SubKeys

        #region Color Picker Preferences
        internal void SaveCustomColors(int[] CustomColors)
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

        internal int[] SetCustomColors()
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

        #endregion Color Picker Preferences

        #region Hash Table Paths
        internal string[] LoadHashTablePathAndMD5(string HashtableName)
        {
            string[] Info = new string[2];

            OpenEuroSoundKeys();
            using (RegistryKey HashTables = EuroSoundKey.OpenSubKey("HashTables", true))
            {
                if (HashTables != null)
                {
                    Info[0] = HashTables.GetValue(HashtableName).ToString();
                    Info[1] = HashTables.GetValue(HashtableName + "MD5").ToString();
                    HashTables.Close();
                }
            }
            return Info;
        }

        internal void SaveHashTablePathAndMD5(string HashTableName, string HashTablePath, string HashTableMD5)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashTables", true);
            using (RegistryKey HashTables = EuroSoundKey.OpenSubKey("HashTables", true))
            {
                HashTables.SetValue(HashTableName, HashTablePath, RegistryValueKind.String);
                HashTables.SetValue(HashTableName + "MD5", HashTableMD5, RegistryValueKind.String);
                HashTables.Close();
            }
        }
        #endregion Hash Table Paths

        #region TreeView Preferences
        internal string[] LoadTreeViewPreferences()
        {
            string[] TreeViewPreferences = new string[4];

            OpenEuroSoundKeys();
            using (RegistryKey TreeViewConfig = EuroSoundKey.OpenSubKey("SBEditorTreeView", true))
            {
                if (TreeViewConfig != null)
                {
                    TreeViewPreferences[0] = TreeViewConfig.GetValue("SelectedFont", "Microsoft Sans Serif; 8,25pt").ToString();
                    TreeViewPreferences[1] = TreeViewConfig.GetValue("ShowTreeLines", "1").ToString();
                    TreeViewPreferences[2] = TreeViewConfig.GetValue("ShowTreeRootLines", "1").ToString();
                    TreeViewPreferences[3] = TreeViewConfig.GetValue("TreeViewIndent", "19").ToString();
                    TreeViewConfig.Close();
                }
                else
                {
                    TreeViewPreferences[0] = "Microsoft Sans Serif; 8,25pt";
                    TreeViewPreferences[1] = "1";
                    TreeViewPreferences[2] = "1";
                    TreeViewPreferences[3] = "19";
                }
            }

            return TreeViewPreferences;
        }

        internal void SaveTreeViewPreferences()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SBEditorTreeView", true);
            using (RegistryKey SoundBankTreeViewPrefs = EuroSoundKey.OpenSubKey("SBEditorTreeView", true))
            {
                SoundBankTreeViewPrefs.SetValue("SelectedFont", GlobalPreferences.SelectedFont, RegistryValueKind.String);
                SoundBankTreeViewPrefs.SetValue("TreeViewIndent", GlobalPreferences.TreeViewIndent, RegistryValueKind.DWord);
                SoundBankTreeViewPrefs.SetValue("ShowTreeLines", GlobalPreferences.ShowLines, RegistryValueKind.DWord);
                SoundBankTreeViewPrefs.SetValue("ShowTreeRootLines", GlobalPreferences.ShowRootLines, RegistryValueKind.DWord);
                SoundBankTreeViewPrefs.Close();
            }
        }
        #endregion TreeView Preferences

        #region General Preferences
        internal string[] LoadGeneralPreferences()
        {
            string[] GlobalPreferences = new string[4];

            OpenEuroSoundKeys();
            using (RegistryKey GeneralConfigs = EuroSoundKey.OpenSubKey("General", true))
            {
                if (GeneralConfigs != null)
                {
                    GlobalPreferences[0] = GeneralConfigs.GetValue("SFXOutputPath", @"X:\Sphinx\Binary\_bin_PC\_Eng").ToString();
                    GlobalPreferences[1] = GeneralConfigs.GetValue("MusicOutputPath", @"X:\Sphinx\Binary\_bin_PC\music").ToString();
                    GlobalPreferences[2] = GeneralConfigs.GetValue("ControlWavesColor", "-16777077").ToString();
                    GlobalPreferences[3] = GeneralConfigs.GetValue("ControlWavesBackColor", "-8355712").ToString();
                    GeneralConfigs.Close();
                }
                else
                {
                    GlobalPreferences[0] = @"X:\Sphinx\Binary\_bin_PC\_Eng";
                    GlobalPreferences[1] = @"X:\Sphinx\Binary\_bin_PC\music";
                    GlobalPreferences[2] = "-16777077";
                    GlobalPreferences[3] = "-8355712";
                }
            }

            return GlobalPreferences;
        }

        internal void SaveGeneralPreferences()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("General", true);
            using (RegistryKey SoundBankTreeViewPrefs = EuroSoundKey.OpenSubKey("General", true))
            {
                SoundBankTreeViewPrefs.SetValue("SFXOutputPath", GlobalPreferences.SFXOutputPath, RegistryValueKind.String);
                SoundBankTreeViewPrefs.SetValue("MusicOutputPath", GlobalPreferences.MusicOutputPath, RegistryValueKind.String);
                SoundBankTreeViewPrefs.SetValue("ControlWavesColor", GlobalPreferences.ColorWavesControl, RegistryValueKind.DWord);
                SoundBankTreeViewPrefs.SetValue("ControlWavesBackColor", GlobalPreferences.BackColorWavesControl, RegistryValueKind.DWord);
                SoundBankTreeViewPrefs.Close();
            }
        }
        #endregion General Preferences

        #region StreamFile
        internal void SaveExternalFilePath()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("StreamFile", true);
            using (RegistryKey StreamFilePath = EuroSoundKey.OpenSubKey("StreamFile", true))
            {
                StreamFilePath.SetValue("FilePath", GlobalPreferences.StreamFilePath, RegistryValueKind.String);
                StreamFilePath.Close();
            }
        }

        internal string SetExternalFilePath()
        {
            string StreamFile = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("StreamFile", true);
            using (RegistryKey StreamFilePath = EuroSoundKey.OpenSubKey("StreamFile", true))
            {
                if (StreamFile != null)
                {
                    StreamFile = StreamFilePath.GetValue("FilePath", "").ToString();
                    StreamFilePath.Close();
                }

            }

            return StreamFile;
        }
        #endregion

        #region SoX 
        internal void SaveSoxFilePath()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SoXPath", true);
            using (RegistryKey StreamFilePath = EuroSoundKey.OpenSubKey("SoXPath", true))
            {
                StreamFilePath.SetValue("ExePath", GlobalPreferences.SoXPath, RegistryValueKind.String);
                StreamFilePath.Close();
            }
        }

        internal string SetSoxFilePath()
        {
            string StreamFile = string.Empty;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SoXPath", true);
            using (RegistryKey StreamFilePath = EuroSoundKey.OpenSubKey("SoXPath", true))
            {
                if (StreamFile != null)
                {
                    StreamFile = StreamFilePath.GetValue("ExePath", "").ToString();
                    StreamFilePath.Close();
                }
            }

            return StreamFile;
        }
        #endregion

        #region System
        internal void SaveSystemConfig()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SystemConfig", true);
            using (RegistryKey StreamFilePath = EuroSoundKey.OpenSubKey("SystemConfig", true))
            {
                StreamFilePath.SetValue("UseSysTray", GlobalPreferences.UseSystemTray, RegistryValueKind.DWord);
                StreamFilePath.Close();
            }
        }

        internal int SetSystemConfig()
        {
            int StreamFile;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SystemConfig", true);
            using (RegistryKey StreamFilePath = EuroSoundKey.OpenSubKey("SystemConfig", true))
            {
                StreamFile = (int)StreamFilePath.GetValue("UseSysTray", 0);
                StreamFilePath.Close();
            }

            return StreamFile;
        }

        #endregion

        #region Flags
        internal void SaveSFXFlagsIfNecessary()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SFXFlags", true);
            using (RegistryKey SFXFlags = EuroSoundKey.OpenSubKey("SFXFlags", true))
            {
                if (SFXFlags.ValueCount < 16)
                {
                    string[] FlagsLabels = new string[]
                    {
                    "MaxReject","UnPausable","IgnoreMasterVolume","MultiSample","RandomPick","Shuffled","Loop",
                    "Polyphonic","UnderWater","PauseInstant","HasSubSFX","StealOnLouder","TreatLikeMusic",
                    "KillMeOwnGroup","GroupStealReject","OneInstancePerFrame"
                    };

                    for (int i = 0; i < FlagsLabels.Length; i++)
                    {
                        SFXFlags.SetValue(string.Join("", new string[] { "Flag", (i + 1).ToString() }), FlagsLabels[i], RegistryValueKind.String);
                    }
                }
                SFXFlags.Close();
            }
        }

        internal void SaveAudioFlagsIfNecessary()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("AudioFlags", true);
            using (RegistryKey SFXFlags = EuroSoundKey.OpenSubKey("AudioFlags", true))
            {
                if (SFXFlags.ValueCount < 1)
                {
                    string[] FlagsLabels = new string[]
                    {
                    "Loop"
                    };

                    for (int i = 0; i < FlagsLabels.Length; i++)
                    {
                        SFXFlags.SetValue(string.Join("", new string[] { "Flag", (i + 1).ToString() }), FlagsLabels[i], RegistryValueKind.String);
                    }
                }
                SFXFlags.Close();
            }
        }
        #endregion

        #region WindowsState
        internal void SaveWindowState(string WindowName, int LocationX, int LocationY, int Width, int Height, bool IsIconic, bool IsMaximized)
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
                SaveWindowState.Close();
            }
        }
        #endregion

        #region AudioDevice
        internal void SaveDefaultAudioDevice()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("DefAudioDev", true);
            using (RegistryKey AudioDevice = EuroSoundKey.OpenSubKey("DefAudioDev", true))
            {
                AudioDevice.SetValue("AudioDevIndex", GlobalPreferences.DefaultAudioDevice, RegistryValueKind.DWord);
                AudioDevice.Close();
            }
        }

        internal int SetDefaultAudioDevice()
        {
            int AudioDeviceNum = 0;

            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("DefAudioDev", true);
            using (RegistryKey AudioDevice = EuroSoundKey.OpenSubKey("DefAudioDev", true))
            {
                if (AudioDevice != null)
                {
                    AudioDeviceNum = (int)AudioDevice.GetValue("AudioDevIndex", 0);
                    AudioDevice.Close();
                }
            }

            return AudioDeviceNum;
        }
        #endregion

        internal RegistryKey ReturnRegistryKey(string Name)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists(Name, true);
            RegistryKey KeyToReturn = EuroSoundKey.OpenSubKey(Name, true);

            return KeyToReturn;
        }
    }
}