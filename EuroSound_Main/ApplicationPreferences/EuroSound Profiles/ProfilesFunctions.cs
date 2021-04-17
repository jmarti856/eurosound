using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSound_Profiles;
using EuroSound_Application.HashCodesFunctions;
using System.IO;

namespace EuroSound_Application.ApplicationPreferences.EuroSound_Profiles
{
    class ProfilesFunctions
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        ESP_Loader ProfilesLoader = new ESP_Loader();
        WindowsRegistryFunctions RegistryFunctions = new WindowsRegistryFunctions();

        internal void ApplyProfile(string ProfileFilePath, string ProfileFileName, bool ReloadHashTables)
        {
            string[] FileLines = File.ReadAllLines(ProfileFilePath);
            string[] SectionData;

            if (ProfilesLoader.FileIsValid(FileLines))
            {
                //*===============================================================================================
                //* Refresh Variables
                //*===============================================================================================
                GlobalPreferences.SelectedProfile = ProfileFilePath;
                GlobalPreferences.SelectedProfileName = ProfileFileName;

                //Save config in Registry
                RegistryFunctions.SaveCurrentProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName);

                //*===============================================================================================
                //* [SoundbanksSettings]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("SoundbanksSettings", 4, FileLines);
                GlobalPreferences.SoundbankFrequency = int.Parse(SectionData[0]);
                GlobalPreferences.SoundbankEncoding = SectionData[1];
                GlobalPreferences.SoundbankBits = int.Parse(SectionData[2]);
                GlobalPreferences.SoundbankChannels = int.Parse(SectionData[3]);

                //Save Data
                RegistryFunctions.SaveSoundSettings("SoundBanks", GlobalPreferences.SoundbankFrequency, GlobalPreferences.SoundbankEncoding, GlobalPreferences.SoundbankBits, GlobalPreferences.SoundbankChannels);
                //*===============================================================================================
                //* [StreamFileSettings]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("StreamFileSettings", 4, FileLines);
                GlobalPreferences.StreambankFrequency = int.Parse(SectionData[0]);
                GlobalPreferences.StreambankEncoding = SectionData[1];
                GlobalPreferences.StreambankBits = int.Parse(SectionData[2]);
                GlobalPreferences.StreambankChannels = int.Parse(SectionData[3]);

                //Save Data
                RegistryFunctions.SaveSoundSettings("StreamSoundbanks", GlobalPreferences.StreambankFrequency, GlobalPreferences.StreambankEncoding, GlobalPreferences.StreambankBits, GlobalPreferences.StreambankChannels);
                //*===============================================================================================
                //* [MusicFileSettings]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("MusicFileSettings", 4, FileLines);
                GlobalPreferences.MusicbankFrequency = int.Parse(SectionData[0]);
                GlobalPreferences.MusicbankEncoding = SectionData[1];
                GlobalPreferences.MusicbankBits = int.Parse(SectionData[2]);
                GlobalPreferences.MusicbankChannels = int.Parse(SectionData[3]);

                //Save Data
                RegistryFunctions.SaveSoundSettings("MusicBanks", GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankEncoding, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels);
                //*===============================================================================================
                //* [HashTableFiles]
                //*===============================================================================================  
                //Load Data
                SectionData = ProfilesLoader.ReadSection("HashTableFiles", 3, FileLines);
                GlobalPreferences.HT_SoundsPath = SectionData[0];
                GlobalPreferences.HT_SoundsMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsPath);
                GlobalPreferences.HT_SoundsDataPath = SectionData[1];
                GlobalPreferences.HT_SoundsDataMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsDataPath);
                GlobalPreferences.HT_MusicPath = SectionData[2];
                GlobalPreferences.HT_MusicMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_MusicPath);

                //Save Data
                RegistryFunctions.SaveHashtablesFiles("HT_Sound", GlobalPreferences.HT_SoundsPath, GlobalPreferences.HT_SoundsMD5);
                RegistryFunctions.SaveHashtablesFiles("HT_SoundData", GlobalPreferences.HT_SoundsDataPath, GlobalPreferences.HT_SoundsDataMD5);
                RegistryFunctions.SaveHashtablesFiles("HT_MusicEvent", GlobalPreferences.HT_MusicPath, GlobalPreferences.HT_MusicMD5);

                //*===============================================================================================
                //* [ExternalFiles]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("ExternalFiles", 3, FileLines);
                GlobalPreferences.StreamFilePath = SectionData[0];
                GlobalPreferences.MkFileListPath = SectionData[1];
                GlobalPreferences.MkFileList2Path = SectionData[2];

                //Save Data
                RegistryFunctions.SaveExternalFiles("StreamFile", "Path", GlobalPreferences.StreamFilePath);
                RegistryFunctions.SaveExternalFiles("MkFileList", "Path", GlobalPreferences.MkFileListPath);
                RegistryFunctions.SaveExternalFiles("MkFileList2", "Path", GlobalPreferences.MkFileList2Path);

                //*===============================================================================================
                //* [OutputFolders]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("OutputFolders", 3, FileLines);
                GlobalPreferences.MusicOutputPath = SectionData[0];
                GlobalPreferences.SFXOutputPath = SectionData[1];
                GlobalPreferences.StreamFileOutputPath = SectionData[2];

                //Save Data
                RegistryFunctions.SaveOutputFolders("SoundsOutputDirectory", "Path", GlobalPreferences.SFXOutputPath);
                RegistryFunctions.SaveOutputFolders("StreamSoundsOutputDirectory", "Path", GlobalPreferences.StreamFileOutputPath);
                RegistryFunctions.SaveOutputFolders("MusicOutputDirectory", "Path", GlobalPreferences.MusicOutputPath);

                //*===============================================================================================
                //* [SoundFlags]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("SoundFlags", 16, FileLines);

                //Save Data
                RegistryFunctions.SaveFlags(SectionData, "SoundFlags");

                //*===============================================================================================
                //* [AudioFlags]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadSection("AudioFlags", 16, FileLines);

                //Save Data
                RegistryFunctions.SaveFlags(SectionData, "AudioFlags");

                //*===============================================================================================
                //* Clear Current HashTables
                //*===============================================================================================
                Hashcodes.SFX_Data.Clear();
                Hashcodes.SFX_Defines.Clear();
                Hashcodes.MFX_Defines.Clear();
                Hashcodes.SB_Defines.Clear();

                //*===============================================================================================
                //* READ HASHTABLES
                //*===============================================================================================
                if (ReloadHashTables)
                {
                    Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);
                    Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                    Hashcodes.LoadMusicHashcodes(GlobalPreferences.HT_MusicPath);
                }
            }
        }
    }
}
