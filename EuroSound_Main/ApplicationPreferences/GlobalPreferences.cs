namespace EuroSound_Application.ApplicationPreferences
{
    internal static class GlobalPreferences
    {
        //*===============================================================================================
        //* PERSISTENT PROPERTIES
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
        internal static string MusicOutputPath;
        internal static int ColorWavesControl;
        internal static int BackColorWavesControl;

        //Frm_StreamFile
        internal static string StreamFilePath;

        //Frm_SoxPrefs
        internal static string SoXPath;

        //Frm_System
        internal static bool UseSystemTray;

        //Frm_OutputDevicecs
        internal static int DefaultAudioDevice;

        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        internal static bool ShowWarningMessagesBox = true;
        internal static bool StatusBar_ToolTipMode;
    }
}