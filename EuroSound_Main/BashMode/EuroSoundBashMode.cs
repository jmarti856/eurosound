using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.ApplicationPreferences.Ini_File;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.HashCodesFunctions;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            if (File.Exists(Application.StartupPath + "\\Esound.ini"))
            {
                GenericFunctions.AvailableProfiles = ProfilesReader.GetAvailableProfiles(Application.StartupPath + "\\Esound.ini");
            }

            //*===============================================================================================
            //* Load Profile
            //*===============================================================================================
            string ProfileNameFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfileName");
            string ProfilePathFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfile");

            //Reload last profile if file exists
            if (File.Exists(ProfilePathFromReg))
            {
                GlobalPreferences.SelectedProfileName = ProfileNameFromReg;
                GlobalPreferences.SelectedProfile = ProfilePathFromReg;
            }
            else
            {
                //Load profile from ini file
                if (!string.IsNullOrEmpty(ProfileNameFromReg))
                {
                    foreach (KeyValuePair<string, string> Profile in GenericFunctions.AvailableProfiles)
                    {
                        if (Profile.Key.Equals(ProfileNameFromReg))
                        {
                            if (File.Exists(Profile.Value))
                            {
                                GlobalPreferences.SelectedProfileName = Profile.Key;
                                GlobalPreferences.SelectedProfile = Profile.Value;
                            }
                            break;
                        }
                    }
                }
            }

            //*===============================================================================================
            //* Load Profile Data
            //*===============================================================================================
            if (File.Exists(GlobalPreferences.SelectedProfile))
            {
                //Reload profile if paths are not equal
                if (!ProfilePathFromReg.Equals(GlobalPreferences.SelectedProfile))
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
                    if (File.Exists(Commands[2]))
                    {
                        using (BinaryReader BReader = new BinaryReader(File.Open(Commands[2], FileMode.Open, FileAccess.Read), Encoding.ASCII))
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
                                            bashFunctions.OutputStreamFile(BReader, Path.Combine(GlobalPreferences.OutputDirectory, "_bin_PC", "_eng"));
                                            break;
                                        case (int)Enumerations.ESoundFileType.MusicBanks:
                                            bashFunctions.OutputMusicBank(BReader, Path.Combine(GlobalPreferences.OutputDirectory, "_bin_PC", "music"));
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
                                            bashFunctions.OutputSoundBank(BReader, Commands[1]);
                                            break;
                                        case (int)Enumerations.ESoundFileType.StreamSounds:
                                            bashFunctions.OutputStreamFile(BReader, Commands[1]);
                                            break;
                                        case (int)Enumerations.ESoundFileType.MusicBanks:
                                            bashFunctions.OutputMusicBank(BReader, Commands[1]);
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
            Application.Exit();
        }
    }
}
