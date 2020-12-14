namespace EuroSound_Application
{
    public static class GlobalPreferences
    {
        /*Final Properties*/
        public static string HT_SoundsPath { get; set; }
        public static string HT_SoundsDataPath { get; set; }
        public static string HT_SoundsMD5 { get; set; }
        public static string HT_SoundsDataMD5 { get; set; }
        public static string HT_MusicPath { get; set; }
        public static string HT_MusicMD5 { get; set; }


        /*Temporal Properties*/
        public static string HT_SoundsPathTEMPORAL { get; set; }
        public static string HT_SoundsDataPathTEMPORAL { get; set; }
        public static string HT_SoundsMD5TEMPORAL { get; set; }
        public static string HT_SoundsDataMD5TEMPORAL { get; set; }
        public static string HT_MusicPathTEMPORAL { get; set; }


        /*Volatile preferences*/
        public static bool ShowWarningMessagesBox { get; set; } = true;

    }
}
