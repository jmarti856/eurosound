namespace EuroSound_Application.ApplicationPreferences
{
    internal static class GlobalPreferences
    {
        //*===============================================================================================
        //* PERSISTENT PROPERTIES
        //*===============================================================================================
        //Profile
        internal static string SelectedProfile;
        internal static string SelectedProfileName;

        //[SoundbanksSettings]
        internal static int SoundbankFrequency;
        internal static string SoundbankEncoding;
        internal static int SoundbankBits;
        internal static int SoundbankChannels;

        //[StreamFileSettings]
        internal static int StreambankFrequency;
        internal static string StreambankEncoding;
        internal static int StreambankBits;
        internal static int StreambankChannels;

        //[MusicFileSettings]
        internal static int MusicbankFrequency;
        internal static string MusicbankEncoding;
        internal static int MusicbankBits;
        internal static int MusicbankChannels;

        //[HashTableFiles]
        internal static string HT_SoundsPath;
        internal static string HT_SoundsMD5;
        internal static string HT_SoundsDataPath;
        internal static string HT_SoundsDataMD5;
        internal static string HT_MusicPath;
        internal static string HT_MusicMD5;

        //[ExternalFiles]
        internal static string StreamFilePath;
        internal static string SoXPath;
        internal static string MkFileListPath;
        internal static string MkFileList2Path;

        //[OutputFolders]
        internal static string SFXOutputPath;
        internal static string MusicOutputPath;
        internal static string StreamFileOutputPath;

        //User Settings -> Tree View
        internal static string TV_SelectedFont = "Microsoft Sans Serif; 8,25pt";
        internal static int TV_Indent = 19;
        internal static int TV_ItemHeight = 16;
        internal static bool TV_ShowLines = true;
        internal static bool TV_ShowRootLines = true;
        internal static bool TV_IgnoreStlyesFromESF = false;

        //User Settings -> Waves Control
        internal static int WavesViewerControl_WavesColor = -16777077;
        internal static int WavesViewerControl_BackgroundColor = -8355712;

        //User Settings -> System Tray
        internal static bool UseSystemTray = false;

        //User Settings -> Output Device
        internal static int DefaultAudioDevice = 0;

        //User Settings -> General
        internal static bool LoadLastLoadedESF = false;

        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        internal static bool ShowWarningMessagesBox = true;
        internal static bool StatusBar_ToolTipMode;
    }
}