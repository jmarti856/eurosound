namespace EuroSound_Application
{
    public static class GlobalPreferences
    {
        //*===============================================================================================
        //* FINAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        public static string HT_SoundsPath { get; set; }

        public static string HT_SoundsDataPath { get; set; }
        public static string HT_SoundsMD5 { get; set; }
        public static string HT_SoundsDataMD5 { get; set; }
        public static string HT_MusicPath { get; set; }
        public static string HT_MusicMD5 { get; set; }

        //Frm_TreeViewPrefs
        public static string SelectedFont { get; set; }

        public static int TreeViewIndent { get; set; }
        public static bool ShowLines { get; set; }
        public static bool ShowRootLines { get; set; }

        //Frm_GeneralPreferences
        public static string SFXOutputPath { get; set; }

        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        public static string HT_SoundsPathTEMPORAL { get; set; }

        public static string HT_SoundsDataPathTEMPORAL { get; set; }
        public static string HT_SoundsMD5TEMPORAL { get; set; }
        public static string HT_SoundsDataMD5TEMPORAL { get; set; }
        public static string HT_MusicPathTEMPORAL { get; set; }

        //Frm_TreeViewPrefs
        public static string SelectedFontTEMPORAL { get; set; }

        public static int TreeViewIndentTEMPORAL { get; set; }
        public static bool ShowLinesTEMPORAL { get; set; }
        public static bool ShowRootLinesTEMPORAL { get; set; }

        //Frm_GeneralPreferences
        public static string SFXOutputPathTEMPORAL { get; set; }

        //*===============================================================================================
        //* OTHER TEMPORAL PROPERTIES
        //*===============================================================================================
        public static bool CancelApplicationClose { get; set; } = false;
        public static bool ShowWarningMessagesBox { get; set; } = true;
    }
}