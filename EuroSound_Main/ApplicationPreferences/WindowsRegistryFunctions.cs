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
            RegistryKey CustomColorKey = EuroSoundKey.OpenSubKey("CustomColors", true);

            for (int i = 0; i < CustomColors.Length; i++)
            {
                CustomColorKey.SetValue("CustCol" + i, CustomColors[i], RegistryValueKind.DWord);
            }
        }

        internal int[] SetCustomColors()
        {
            int[] CustomColors = new int[16];

            OpenEuroSoundKeys();
            RegistryKey CustomColorKey = EuroSoundKey.OpenSubKey("CustomColors", true);
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
            }
            else
            {
                for (int i = 0; i < CustomColors.Length; i++)
                {
                    CustomColors[i] = 16777215;
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
            RegistryKey HashTables = EuroSoundKey.OpenSubKey("HashTables", true);
            if (HashTables != null)
            {
                Info[0] = HashTables.GetValue(HashtableName).ToString();
                Info[1] = HashTables.GetValue(HashtableName + "MD5").ToString();
            }

            return Info;
        }

        internal void SaveHashTablePathAndMD5(string HashTableName, string HashTablePath, string HashTableMD5)
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("HashTables", true);
            RegistryKey HashTables = EuroSoundKey.OpenSubKey("HashTables", true);

            HashTables.SetValue(HashTableName, HashTablePath, RegistryValueKind.String);
            HashTables.SetValue(HashTableName + "MD5", HashTableMD5, RegistryValueKind.String);
        }
        #endregion Hash Table Paths

        #region TreeView Preferences
        internal string[] LoadTreeViewPreferences()
        {
            string[] TreeViewPreferences = new string[4];

            OpenEuroSoundKeys();
            RegistryKey TreeViewConfig = EuroSoundKey.OpenSubKey("SBEditorTreeView", true);
            if (TreeViewConfig != null)
            {
                TreeViewPreferences[0] = TreeViewConfig.GetValue("SelectedFont", "Microsoft Sans Serif; 8,25pt").ToString();
                TreeViewPreferences[1] = TreeViewConfig.GetValue("ShowLines", "true").ToString();
                TreeViewPreferences[2] = TreeViewConfig.GetValue("ShowRootLines", "true").ToString();
                TreeViewPreferences[3] = TreeViewConfig.GetValue("TreeViewIndent", "19").ToString();
            }
            else
            {
                TreeViewPreferences[0] = "Microsoft Sans Serif; 8,25pt";
                TreeViewPreferences[1] = "true";
                TreeViewPreferences[2] = "true";
                TreeViewPreferences[3] = "19";
            }

            return TreeViewPreferences;
        }

        internal void SaveTreeViewPreferences()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("SBEditorTreeView", true);
            RegistryKey SoundBankTreeViewPrefs = EuroSoundKey.OpenSubKey("SBEditorTreeView", true);

            SoundBankTreeViewPrefs.SetValue("SelectedFont", GlobalPreferences.SelectedFont, RegistryValueKind.String);
            SoundBankTreeViewPrefs.SetValue("TreeViewIndent", GlobalPreferences.TreeViewIndent, RegistryValueKind.DWord);
            SoundBankTreeViewPrefs.SetValue("ShowLines", GlobalPreferences.ShowLines, RegistryValueKind.String);
            SoundBankTreeViewPrefs.SetValue("ShowRootLines", GlobalPreferences.ShowRootLines, RegistryValueKind.String);
        }
        #endregion TreeView Preferences

        #region General Preferences
        internal string[] LoadGeneralPreferences()
        {
            string[] GlobalPreferences = new string[4];

            OpenEuroSoundKeys();
            RegistryKey GeneralConfigs = EuroSoundKey.OpenSubKey("General", true);
            if (GeneralConfigs != null)
            {
                GlobalPreferences[0] = GeneralConfigs.GetValue("SFXOutputPath", @"X:\Sphinx\Binary\_bin_PC\_Eng").ToString();
                GlobalPreferences[1] = GeneralConfigs.GetValue("MusicOutputPath", @"X:\Sphinx\Binary\_bin_PC\music").ToString();
                GlobalPreferences[2] = GeneralConfigs.GetValue("ControlWavesColor", "-16777077").ToString();
                GlobalPreferences[3] = GeneralConfigs.GetValue("ControlWavesBackColor", "-8355712").ToString();
            }
            else
            {
                GlobalPreferences[0] = @"X:\Sphinx\Binary\_bin_PC\_Eng";
                GlobalPreferences[1] = @"X:\Sphinx\Binary\_bin_PC\music";
                GlobalPreferences[2] = "-16777077";
                GlobalPreferences[3] = "-8355712";
            }

            return GlobalPreferences;
        }

        internal void SaveGeneralPreferences()
        {
            OpenEuroSoundKeys();
            CreateEuroSoundSubkeyIfNotExists("General", true);
            RegistryKey SoundBankTreeViewPrefs = EuroSoundKey.OpenSubKey("General", true);

            SoundBankTreeViewPrefs.SetValue("SFXOutputPath", GlobalPreferences.SFXOutputPath, RegistryValueKind.String);
            SoundBankTreeViewPrefs.SetValue("MusicOutputPath", GlobalPreferences.MusicOutputPath, RegistryValueKind.String);
            SoundBankTreeViewPrefs.SetValue("ControlWavesColor", GlobalPreferences.ColorWavesControl, RegistryValueKind.DWord);
            SoundBankTreeViewPrefs.SetValue("ControlWavesBackColor", GlobalPreferences.BackColorWavesControl, RegistryValueKind.DWord);
        }
        #endregion General Preferences
    }
}