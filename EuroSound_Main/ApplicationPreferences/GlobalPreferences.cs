namespace EuroSound_Application
{
    internal static class GlobalPreferences
    {
        //*===============================================================================================
        //* FINAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        internal static string HT_SoundsPath;
        internal static string HT_SoundsDataPath;
        internal static string HT_SoundsMD5;
        internal static string HT_SoundsDataMD5;
        internal static string HT_MusicPath;
        internal static string HT_MusicMD5;

        //Frm_TreeViewPrefs
        internal static string SelectedFont;
        internal static int TreeViewIndent;
        internal static bool ShowLines;
        internal static bool ShowRootLines;

        //Frm_GeneralPreferences
        internal static string SFXOutputPath;

        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        internal static string HT_SoundsPathTEMPORAL;
        internal static string HT_SoundsDataPathTEMPORAL;
        internal static string HT_MusicPathTEMPORAL;

        //Frm_TreeViewPrefs
        internal static string SelectedFontTEMPORAL;
        internal static int TreeViewIndentTEMPORAL;
        internal static bool ShowLinesTEMPORAL;
        internal static bool ShowRootLinesTEMPORAL;

        //Frm_GeneralPreferences
        internal static string SFXOutputPathTEMPORAL;

        //*===============================================================================================
        //* OTHER TEMPORAL VARIABLES
        //*===============================================================================================
        internal static bool CancelApplicationClose = false;
        internal static bool ShowWarningMessagesBox = true;
        internal static bool StatusBar_ToolTipMode;
    }
}