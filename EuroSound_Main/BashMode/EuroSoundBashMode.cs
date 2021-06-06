using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.ApplicationPreferences.Ini_File;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.HashCodesFunctions;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application.BashMode
{
    internal class EuroSoundBashMode
    {
        //*===============================================================================================
        //* Variables
        //*===============================================================================================
        private IniFile_Functions ProfilesReader = new IniFile_Functions();
        private ProfilesFunctions ProfilesLoader = new ProfilesFunctions();

        internal void LoadBasicData()
        {
            //*===============================================================================================
            //* Load INI FIle
            //*===============================================================================================
            //Get profiles list form ini file
            string iniPath = Application.StartupPath + "\\Esound.ini";
            if (File.Exists(iniPath))
            {
                GenericFunctions.AvailableProfiles = ProfilesReader.GetAvailableProfiles(iniPath);
            }

            //*===============================================================================================
            //* Load Profile
            //*===============================================================================================

            //Get stored profile info
            string profileNameFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfileName");
            string profilePathFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfile");
            string profileHashMD5 = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfileMD5");

            //We don't have any profile, is the first time that we start EuroSound. 
            if (string.IsNullOrEmpty(profileNameFromReg) || string.IsNullOrEmpty(profilePathFromReg))
            {
                //if we only have one profile, load it
                if (GenericFunctions.AvailableProfiles.Count == 1)
                {
                    KeyValuePair<string, string> ProfileInfo = GenericFunctions.AvailableProfiles.ElementAt(0);
                    if (File.Exists(ProfileInfo.Value))
                    {
                        new ProfilesFunctions().ApplyProfile(ProfileInfo.Value, ProfileInfo.Key, true);
                    }
                }
            }
            else
            {
                //Check that the stored profile and the profile of the ini are the same
                foreach (KeyValuePair<string, string> profileToCheck in GenericFunctions.AvailableProfiles)
                {
                    //Key = NAME; Value = PATH
                    if (profileToCheck.Key.Equals(profileNameFromReg))
                    {
                        //Calculate MD5
                        string IniProfileMD5 = GenericFunctions.CalculateMD5(profileToCheck.Value);

                        //Stored profile matches with the ini profile
                        if (IniProfileMD5.Equals(profileToCheck.Value))
                        {
                            GlobalPreferences.SelectedProfileName = profileToCheck.Key;
                            GlobalPreferences.SelectedProfile = profileToCheck.Value;
                        }
                        else
                        {
                            //Read profile again
                            new ProfilesFunctions().ApplyProfile(profileToCheck.Value, profileToCheck.Key, true);
                        }

                        //Quit loop
                        break;
                    }
                }
            }

            //*===============================================================================================
            //* Load Profile Data
            //*===============================================================================================
            if (File.Exists(GlobalPreferences.SelectedProfile))
            {
                //Reload profile if paths are not equal or the file has changed
                if (!profilePathFromReg.Equals(GlobalPreferences.SelectedProfile) || !GenericFunctions.CalculateMD5(GlobalPreferences.SelectedProfile).Equals(profileHashMD5))
                {
                    //Load and apply profile to update regedit data
                    ProfilesLoader.ApplyProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName, true);
                }

                //[HashTableFiles]
                GlobalPreferences.HT_SoundsPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_Sound", "Path");
                GlobalPreferences.HT_SoundsMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_Sound", "MD5");

                GlobalPreferences.HT_SoundsDataPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "Path");
                GlobalPreferences.HT_SoundsDataMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "MD5");

                GlobalPreferences.HT_MusicPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "Path");
                GlobalPreferences.HT_MusicMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "MD5");

                //[OutputFolders]
                GlobalPreferences.OutputDirectory = WindowsRegistryFunctions.LoadOutputFolders("OutputDirectory", "Path");
            }

            //*===============================================================================================
            //* Load Hash Tables
            //*===============================================================================================
            //-----------------------------------------[Sound Data]----------------------------------------
            if (File.Exists(GlobalPreferences.HT_SoundsDataPath))
            {
                Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Hashcodes_SFXData_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //-----------------------------------------[Sound Defines]----------------------------------------
            if (File.Exists(GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Hashcodes_SFXDefines_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //-----------------------------------------[Music Defines]----------------------------------------
            if (File.Exists(GlobalPreferences.HT_MusicPath))
            {
                Hashcodes.LoadMusicHashcodes(GlobalPreferences.HT_MusicPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Hashcodes_SFXData_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void ExecuteCommand(string[] Commands)
        {
            //*===============================================================================================
            //* COMMANDS - HELP
            //*===============================================================================================
            if (Commands[0].Equals("/?"))
            {
                MessageBox.Show(string.Format("Usage: EuroSound.exe [opts] [filename] where options are:\n/o            -Outputs the .ESF file\n/?            -Help"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //*===============================================================================================
            //* COMMANDS - OUTPUT FILE
            //*===============================================================================================
            else if (Commands[0].Equals("/o"))
            {
                if (Commands[1].Equals("PC", System.StringComparison.OrdinalIgnoreCase) || Commands[1].Equals("PS2", System.StringComparison.OrdinalIgnoreCase))
                {
                    OutputFile(Commands[1], Commands[2]);
                }
                if (Commands[1].Equals("ALL", System.StringComparison.OrdinalIgnoreCase))
                {
                    OutputFile("PC", Commands[2]);
                    OutputFile("PS2", Commands[2]);
                }
            }
            Application.Exit();
        }

        internal void OutputFile(string target, string filePath)
        {
            if (File.Exists(filePath))
            {
                using (BinaryStream BReader = new BinaryStream(File.Open(filePath, FileMode.Open, FileAccess.Read)))
                {
                    EuroSoundFiles ESoundFiles = new EuroSoundFiles();
                    if (ESoundFiles.FileIsCorrect(BReader))
                    {
                        sbyte TypeOfStoredData = BReader.ReadSByte();
                        uint fileVersion = ESoundFiles.FileVersion;

                        //Older versions, before 1.0.1.3
                        if (fileVersion < 1013)
                        {
                            BashMode_OutputFilesOldVersion bashFunctions = new BashMode_OutputFilesOldVersion();
                            switch (TypeOfStoredData)
                            {
                                case (int)Enumerations.ESoundFileType.SoundBanks:
                                    bashFunctions.OutputSoundBank(BReader, Path.Combine(GlobalPreferences.OutputDirectory, "_bin_PC", "_eng"), "PC");
                                    break;
                                case (int)Enumerations.ESoundFileType.StreamSounds:
                                    bashFunctions.OutputStreamFile(BReader, Path.Combine(GlobalPreferences.OutputDirectory, "_bin_PC", "_eng"), "PC");
                                    break;
                                case (int)Enumerations.ESoundFileType.MusicBanks:
                                    bashFunctions.OutputMusicBank(BReader, Path.Combine(GlobalPreferences.OutputDirectory, "_bin_PC", "music"), "PC");
                                    break;
                            }
                        }
                        //Newer versions, from 1.0.1.3
                        else
                        {
                            BashMode_OutputFilesNewVersion bashFunctions = new BashMode_OutputFilesNewVersion();
                            switch (TypeOfStoredData)
                            {
                                case (int)Enumerations.ESoundFileType.SoundBanks:
                                    bashFunctions.OutputSoundBank(BReader, target, (int)fileVersion);
                                    break;
                                case (int)Enumerations.ESoundFileType.StreamSounds:
                                    bashFunctions.OutputStreamFile(BReader, target);
                                    break;
                                case (int)Enumerations.ESoundFileType.MusicBanks:
                                    bashFunctions.OutputMusicBank(BReader, target);
                                    break;
                            }
                        }
                    }

                    //Close File
                    BReader.Close();
                }
            }
        }
    }
}
