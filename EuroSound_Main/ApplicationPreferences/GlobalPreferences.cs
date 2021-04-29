namespace EuroSound_Application.ApplicationPreferences
{
    internal static class GlobalPreferences
    {
        //*===============================================================================================
        //* PERSISTENT PROPERTIES
        //*===============================================================================================
        //Profile
        internal static string SelectedProfile = string.Empty;
        internal static string SelectedProfileName = "ProgDefaults";

        //[SoundbanksSettings]
        internal static int SoundbankFrequency = 0;
        internal static string SoundbankEncoding = string.Empty;
        internal static int SoundbankBits = 0;
        internal static int SoundbankChannels = 0;

        //[StreamFileSettings]
        internal static int StreambankFrequency = 0;
        internal static string StreambankEncoding = string.Empty;
        internal static int StreambankBits = 0;
        internal static int StreambankChannels = 0;

        //[MusicFileSettings]
        internal static int MusicbankFrequency = 0;
        internal static string MusicbankEncoding = string.Empty;
        internal static int MusicbankBits = 0;
        internal static int MusicbankChannels = 0;

        //[HashTableFiles]
        internal static string HT_SoundsPath = string.Empty;
        internal static string HT_SoundsMD5 = string.Empty;
        internal static string HT_SoundsDataPath = string.Empty;
        internal static string HT_SoundsDataMD5 = string.Empty;
        internal static string HT_MusicPath = string.Empty;
        internal static string HT_MusicMD5 = string.Empty;

        //[ExternalFiles]
        internal static string StreamFilePath = string.Empty;
        internal static string SoXPath = string.Empty;
        internal static string MkFileListPath = string.Empty;
        internal static string MkFileList2Path = string.Empty;

        //[OutputFolders]
        internal static string SFXOutputPath = string.Empty;
        internal static string MusicOutputPath = string.Empty;
        internal static string StreamFileOutputPath = string.Empty;
        internal static string DebugFilesFolder = string.Empty;

        //[Frm_OutputSettings]
        internal static bool PlaySoundWhenOutput = false;
        internal static string OutputSoundPath = string.Empty;

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
        internal static bool UseThreadingWhenLoad = true;

        //Backups
        internal static bool MakeBackups = true;
        internal static string MakeBackupsDirectory = @"X:\Backups";
        internal static int MakeBackupsMaxNumber = 8;
        internal static int MakeBackupsInterval = 1;
        internal static int MakeBackupsIndex = 0;

        //ShowAlerts
        internal static bool ShowUpdatesAlerts = true;

        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        internal static bool ShowWarningMessagesBox = true;
        internal static bool StatusBar_ToolTipMode;
    }
}