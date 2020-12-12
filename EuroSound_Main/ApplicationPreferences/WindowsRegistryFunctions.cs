using Microsoft.Win32;

namespace EuroSound_Application
{
    public static class WindowsRegistryFunctions
    {
        private static RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
        private static RegistryKey EurocomKey;
        private static RegistryKey EuroSoundKey;

        #region Create Keys and SubKeys
        private static void CreateEurocomKeyIfNotExists()
        {
            if (SoftwareKey.OpenSubKey("Eurocomm", true) == null)
            {
                SoftwareKey.CreateSubKey("Eurocomm");
            }
            EurocomKey = SoftwareKey.OpenSubKey("Eurocomm", true);
        }

        public static void CreateEuroSoundKeyIfNotExists()
        {
            CreateEurocomKeyIfNotExists();
            if (EurocomKey.OpenSubKey("Eurocomm", true) == null)
            {
                EurocomKey.CreateSubKey("EuroSound");
            }
            EuroSoundKey = EurocomKey.OpenSubKey("EuroSound", true);
        }

        public static void CreateEuroSoundSubkeyIfNotExists(string SubKeyName, bool Writable)
        {
            if (EuroSoundKey.OpenSubKey(SubKeyName, Writable) == null)
            {
                EuroSoundKey.CreateSubKey(SubKeyName);
            }
        }
        #endregion

        #region Color Picker Preferences
        public static void SaveCustomColors(int[] CustomColors)
        {
            CreateEuroSoundSubkeyIfNotExists("CustomColors", true);
            RegistryKey CustomColorKey = EuroSoundKey.OpenSubKey("CustomColors", true);

            for (int i = 0; i < CustomColors.Length; i++)
            {
                CustomColorKey.SetValue("CustomColor" + i, CustomColors[i], RegistryValueKind.DWord);
            }
        }

        public static int[] SetCustomColors()
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
        #endregion

        #region Hash Table Paths 
        internal static void SaveHashTablePathAndMD5(string HashTableName, string HashTablePath, string HashTableMD5)
        {
            CreateEuroSoundSubkeyIfNotExists("HashTables", true);
            RegistryKey HashTables = EuroSoundKey.OpenSubKey("HashTables", true);

            HashTables.SetValue(HashTableName, HashTablePath, RegistryValueKind.String);
            HashTables.SetValue(HashTableName + "MD5", HashTableMD5, RegistryValueKind.String);
        }

        internal static string[] LoadHashTablePathAndMD5(string HashtableName)
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
        #endregion
    }
}
