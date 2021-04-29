using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSound_Profiles;
using EuroSound_Application.HashCodesFunctions;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.ApplicationPreferences.EuroSound_Profiles
{
    class ProfilesFunctions
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        ESP_Loader ProfilesLoader = new ESP_Loader();

        internal void ApplyProfile(string ProfileFilePath, string ProfileFileName, bool ReloadHashTables)
        {
            IEnumerable<string> FileLines = File.ReadLines(ProfileFilePath);
            string[] SectionData;

            if (ProfilesLoader.FileIsValid(FileLines))
            {
                //*===============================================================================================
                //* Refresh Variables
                //*===============================================================================================
                GlobalPreferences.SelectedProfile = ProfileFilePath;
                GlobalPreferences.SelectedProfileName = ProfileFileName;

                //Save config in Registry
                WindowsRegistryFunctions.SaveCurrentProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName);

                //*===============================================================================================
                //* [SoundbanksSettings]
                //*===============================================================================================
                //Load Data
                ProfilesLoader.ReadSoundBankSettings(FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveSoundSettings("SoundBanks", GlobalPreferences.SoundbankFrequency, GlobalPreferences.SoundbankEncoding, GlobalPreferences.SoundbankBits, GlobalPreferences.SoundbankChannels);
                //*===============================================================================================
                //* [StreamFileSettings]
                //*===============================================================================================
                //Load Data
                ProfilesLoader.ReadStreamFileSettings(FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveSoundSettings("StreamSoundbanks", GlobalPreferences.StreambankFrequency, GlobalPreferences.StreambankEncoding, GlobalPreferences.StreambankBits, GlobalPreferences.StreambankChannels);
                //*===============================================================================================
                //* [MusicFileSettings]
                //*===============================================================================================
                //Load Data
                ProfilesLoader.ReadMusicFileSettings(FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveSoundSettings("MusicBanks", GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankEncoding, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels);
                //*===============================================================================================
                //* [HashTableFiles]
                //*===============================================================================================  
                //Load Data
                ProfilesLoader.ReadHashTableFiles(FileLines);
                GlobalPreferences.HT_SoundsMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsPath);
                GlobalPreferences.HT_SoundsDataMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsDataPath);
                GlobalPreferences.HT_MusicMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_MusicPath);

                //Save Data
                WindowsRegistryFunctions.SaveHashtablesFiles("HT_Sound", GlobalPreferences.HT_SoundsPath, GlobalPreferences.HT_SoundsMD5);
                WindowsRegistryFunctions.SaveHashtablesFiles("HT_SoundData", GlobalPreferences.HT_SoundsDataPath, GlobalPreferences.HT_SoundsDataMD5);
                WindowsRegistryFunctions.SaveHashtablesFiles("HT_MusicEvent", GlobalPreferences.HT_MusicPath, GlobalPreferences.HT_MusicMD5);

                //*===============================================================================================
                //* [ExternalFiles]
                //*===============================================================================================
                //Load Data
                ProfilesLoader.ReadExternalFiles(FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveExternalFiles("StreamFile", "Path", GlobalPreferences.StreamFilePath);
                WindowsRegistryFunctions.SaveExternalFiles("MkFileList", "Path", GlobalPreferences.MkFileListPath);
                WindowsRegistryFunctions.SaveExternalFiles("MkFileList2", "Path", GlobalPreferences.MkFileList2Path);

                //*===============================================================================================
                //* [OutputFolders]
                //*===============================================================================================
                //Load Data
                ProfilesLoader.ReadOutputFolders(FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveOutputFolders("SoundsOutputDirectory", "Path", GlobalPreferences.SFXOutputPath);
                WindowsRegistryFunctions.SaveOutputFolders("StreamSoundsOutputDirectory", "Path", GlobalPreferences.StreamFileOutputPath);
                WindowsRegistryFunctions.SaveOutputFolders("MusicOutputDirectory", "Path", GlobalPreferences.MusicOutputPath);
                WindowsRegistryFunctions.SaveOutputFolders("DebugFilesFolder", "Path", GlobalPreferences.DebugFilesFolder);

                //*===============================================================================================
                //* [SoundFlags]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadFlagsBlock("SoundFlags", FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveFlags(SectionData, "SoundFlags");

                //*===============================================================================================
                //* [AudioFlags]
                //*===============================================================================================
                //Load Data
                SectionData = ProfilesLoader.ReadFlagsBlock("AudioFlags", FileLines);

                //Save Data
                WindowsRegistryFunctions.SaveFlags(SectionData, "AudioFlags");

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
