using Microsoft.Win32;

namespace EuroSound_Application
{
    internal class WindowsRegistryFunctions
    {
        private RegistryKey EurocomKey;
        private RegistryKey EuroSoundKey;
        private RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);

        #region Create Keys and SubKeys
        internal void CreateEuroSoundKeyIfNotExists()
        {
            CreateEurocomKeyIfNotExists();
            if (EurocomKey.OpenSubKey("Eurocomm", true) == null)
            {
                EurocomKey.CreateSubKey("EuroSound");
            }
            EuroSoundKey = EurocomKey.OpenSubKey("EuroSound", true);
        }

        internal void CreateEuroSoundSubkeyIfNotExists(string SubKeyName, bool Writable)
        {
            if (EuroSoundKey.OpenSubKey(SubKeyName, Writable) == null)
            {
                EuroSoundKey.CreateSubKey(SubKeyName);
            }
        }

        internal void CreateEurocomKeyIfNotExists()
        {
            if (SoftwareKey.OpenSubKey("Eurocomm", true) == null)
            {
                SoftwareKey.CreateSubKey("Eurocomm");
            }
            EurocomKey = SoftwareKey.OpenSubKey("Eurocomm", true);
        }
        #endregion Create Keys and SubKeys

        #region Color Picker Preferences

        internal void SaveCustomColors(int[] CustomColors)
        {
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
            CreateEuroSoundSubkeyIfNotExists("SBEditorTreeView", true);
            RegistryKey SoundBankTreeViewPrefs = EuroSoundKey.OpenSubKey("SBEditorTreeView", true);

            SoundBankTreeViewPrefs.SetValue("SelectedFont", GlobalPreferences.SelectedFont, RegistryValueKind.String);
            SoundBankTreeViewPrefs.SetValue("TreeViewIndent", GlobalPreferences.TreeViewIndent, RegistryValueKind.DWord);
            SoundBankTreeViewPrefs.SetValue("ShowLines", GlobalPreferences.ShowLines, RegistryValueKind.String);
            SoundBankTreeViewPrefs.SetValue("ShowRootLines", GlobalPreferences.ShowRootLines, RegistryValueKind.String);
        }
        #endregion TreeView Preferences

        #region General Preferences
        internal string LoadGeneralPreferences()
        {
            string TreeViewPreferences;
            RegistryKey GeneralConfigs = EuroSoundKey.OpenSubKey("General", true);
            if (GeneralConfigs != null)
            {
                TreeViewPreferences = GeneralConfigs.GetValue("SFXOutputPath", @"X:\Sphinx\Binary\_bin_PC\_Eng").ToString();
            }
            else
            {
                TreeViewPreferences = @"X:\Sphinx\Binary\_bin_PC\_Eng";
            }

            return TreeViewPreferences;
        }

        internal void SaveGeneralPreferences()
        {
            CreateEuroSoundSubkeyIfNotExists("General", true);
            RegistryKey SoundBankTreeViewPrefs = EuroSoundKey.OpenSubKey("General", true);

            SoundBankTreeViewPrefs.SetValue("SFXOutputPath", GlobalPreferences.SFXOutputPath, RegistryValueKind.String);
        }
        #endregion General Preferences
    }
}