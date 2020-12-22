namespace EuroSound_Application
{
    internal static class GlobalPreferences
    {
        //*===============================================================================================
        //* FINAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        internal static string HT_SoundsPath { get; set; }
        internal static string HT_SoundsDataPath { get; set; }
        internal static string HT_SoundsMD5 { get; set; }
        internal static string HT_SoundsDataMD5 { get; set; }
        internal static string HT_MusicPath { get; set; }
        internal static string HT_MusicMD5 { get; set; }

        //Frm_TreeViewPrefs
        internal static string SelectedFont { get; set; }
        internal static int TreeViewIndent { get; set; }
        internal static bool ShowLines { get; set; }
        internal static bool ShowRootLines { get; set; }

        //Frm_GeneralPreferences
        internal static string SFXOutputPath { get; set; }

        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        internal static string HT_SoundsPathTEMPORAL { get; set; }

        internal static string HT_SoundsDataPathTEMPORAL { get; set; }
        internal static string HT_SoundsMD5TEMPORAL { get; set; }
        internal static string HT_SoundsDataMD5TEMPORAL { get; set; }
        internal static string HT_MusicPathTEMPORAL { get; set; }

        //Frm_TreeViewPrefs
        internal static string SelectedFontTEMPORAL { get; set; }
        internal static int TreeViewIndentTEMPORAL { get; set; }
        internal static bool ShowLinesTEMPORAL { get; set; }
        internal static bool ShowRootLinesTEMPORAL { get; set; }

        //Frm_GeneralPreferences
        internal static string SFXOutputPathTEMPORAL { get; set; }

        //*===============================================================================================
        //* OTHER TEMPORAL VARIABLES
        //*===============================================================================================
        internal static bool CancelApplicationClose = false;
        internal static bool ShowWarningMessagesBox = true;
        internal static bool StatusBar_ToolTipMode;
    }
}