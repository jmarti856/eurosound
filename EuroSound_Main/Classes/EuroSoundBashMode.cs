using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.ApplicationPreferences.Ini_File;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.Classes.SFX_Files;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundMusicFilesFunctions;
using EuroSound_Application.EuroSoundSoundBanksFilesFunctions;
using EuroSound_Application.GenerateSoundBankSFX;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
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
                GlobalPreferences.OutputDirectory = WindowsRegistryFunctions.LoadOutputFolders("StreamSoundsOutputDirectory", "Path");
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
                if (File.Exists(Commands[1]))
                {
                    using (BinaryReader BReader = new BinaryReader(File.Open(Commands[1], FileMode.Open, FileAccess.Read), Encoding.ASCII))
                    {
                        EuroSoundFiles ESoundFiles = new EuroSoundFiles();
                        if (ESoundFiles.FileIsCorrect(BReader))
                        {
                            sbyte TypeOfStoredData = BReader.ReadSByte();

                            //*===============================================================================================
                            //* EXPORT SOUNDBANK
                            //*===============================================================================================
                            if (TypeOfStoredData == 0)
                            {
                                ESF_LoadSoundBanks SectionsReader = new ESF_LoadSoundBanks();
                                GenerateSFXSoundBank SFXGenerator = new GenerateSFXSoundBank();
                                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();

                                Dictionary<uint, EXSound> SoundsList = new Dictionary<uint, EXSound>();
                                Dictionary<string, EXAudio> AudiosList = new Dictionary<string, EXAudio>();

                                //*===============================================================================================
                                //* ESF FILE
                                //*===============================================================================================
                                //File Hashcode
                                uint File_Hashcode = BReader.ReadUInt32();
                                //Latest SoundID value
                                BReader.ReadUInt32();
                                //TreeView Data
                                BReader.ReadUInt32();
                                //SoundsListData Offset -- Not used for now
                                uint SoundsListDataOffset = BReader.ReadUInt32();
                                //AudioData Offset -- Not used for now
                                uint AudioDataOffset = BReader.ReadUInt32();
                                //FullSize
                                BReader.ReadUInt32();
                                //File Name
                                BReader.ReadString();
                                //Profile Path
                                string ProfileSelected = BReader.ReadString();
                                //Profile Name
                                string ProfileSelectedName = BReader.ReadString();

                                GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

                                //--------------------------[SOUNDS LIST DATA]--------------------------
                                BReader.BaseStream.Position = (SoundsListDataOffset);
                                SectionsReader.ReadSoundsListData(BReader, SoundsList);

                                //--------------------------[AUDIO DATA]--------------------------
                                BReader.BaseStream.Position = (AudioDataOffset);
                                SectionsReader.ReadAudioDataDictionary(BReader, AudiosList, (int)ESoundFiles.FileVersion);

                                //*===============================================================================================
                                //* CREATE SFX FILE
                                //*===============================================================================================
                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.OutputDirectory))
                                {
                                    using (BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.OutputDirectory + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                                    {
                                        //*===============================================================================================
                                        //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
                                        //*===============================================================================================
                                        //Discard SFXs that has checked as "no output"
                                        Dictionary<uint, EXSound> FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(SoundsList, null, null);

                                        //*===============================================================================================
                                        //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED
                                        //*===============================================================================================
                                        IEnumerable<string> UsedAudios = EXSoundbanksFunctions.GetAudiosToExport(FinalSoundsDict);

                                        //Add data
                                        Dictionary<string, EXAudio> FinalAudioDataDict = SFXGenerator.GetFinalAudioDictionaryPCMData(UsedAudios, AudiosList, null);

                                        //*===============================================================================================
                                        //* STEP 3: CHECK DATA THAT WILL BE OUTPUTED
                                        //*===============================================================================================
                                        bool CanOutputFile = true;
                                        List<uint> SoundsHashcodes = new List<uint>();

                                        //Check Data, first the SFX Objects
                                        foreach (KeyValuePair<uint, EXSound> SoundToCheck in FinalSoundsDict)
                                        {
                                            CanOutputFile = SFX_Check.ValidateSFX(SoundToCheck.Value, FinalSoundsDict, SoundsHashcodes, null, null);
                                            if (CanOutputFile == false)
                                            {
                                                break;
                                            }
                                        }

                                        if (CanOutputFile)
                                        {
                                            //Check Data, audio objects
                                            foreach (KeyValuePair<string, EXAudio> AudioToCheck in FinalAudioDataDict)
                                            {
                                                CanOutputFile = SFX_Check.ValidateAudios(AudioToCheck.Value, null, null);
                                                if (CanOutputFile == false)
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        //*===============================================================================================
                                        //* STEP 4: START WRITTING
                                        //*===============================================================================================
                                        if (CanOutputFile)
                                        {
                                            //--------------------------------------[WRITE FILE HEADER]--------------------------------------
                                            //Write Data
                                            SFXGenerator.WriteFileHeader(BWriter, File_Hashcode, null);

                                            //--------------------------------------[Write SECTIONS]--------------------------------------
                                            //Write Data
                                            SFXGenerator.WriteFileSections(BWriter, GenericFunctions.CountNumberOfSamples(FinalSoundsDict), null);

                                            //--------------------------------------[SECTION SFX elements]--------------------------------------
                                            //Write Data
                                            SFXGenerator.WriteSFXSection(BWriter, FinalSoundsDict, FinalAudioDataDict, null, null);

                                            //--------------------------------------[SECTION Sample info elements]--------------------------------------
                                            //Write Data
                                            SFXGenerator.WriteSampleInfoSection(BWriter, FinalAudioDataDict, null, null);

                                            //--------------------------------------[SECTION Sample data]--------------------------------------
                                            //Write Data
                                            SFXGenerator.WriteSampleDataSectionPC(BWriter, FinalAudioDataDict, null, null);

                                            //*===============================================================================================
                                            //* STEP 5: WRITE FINAL OFFSETS
                                            //*===============================================================================================
                                            //Write Data
                                            SFXGenerator.WriteFinalOffsets(BWriter, null, null);
                                        }

                                        //Close File
                                        BWriter.Close();
                                    }
                                }
                            }
                            //*===============================================================================================
                            //* EXPORT STREAM FILE
                            //*===============================================================================================
                            else if (TypeOfStoredData == 1)
                            {
                                ESF_LoadStreamSounds SectionsReader = new ESF_LoadStreamSounds();
                                GenerateSFXStreamedSounds SFXGenerator = new GenerateSFXStreamedSounds();
                                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                                Dictionary<uint, EXSoundStream> DictionaryData = new Dictionary<uint, EXSoundStream>();

                                //*===============================================================================================
                                //* ESF FILE
                                //*===============================================================================================
                                //File Hashcode
                                uint File_Hashcode = BReader.ReadUInt32();
                                //Sound ID
                                BReader.ReadUInt32();
                                //Sounds List Offset
                                BReader.ReadUInt32();//Only Used in the "Frm_NewStreamSound" Form
                                //TreeViewData Offset
                                BReader.ReadUInt32();
                                //Dictionary Data Offset
                                uint StreamSoundsDictionaryOffset = BReader.ReadUInt32();
                                //FileSize
                                BReader.ReadUInt32();
                                //File Name
                                BReader.ReadString();
                                //Profile Path
                                string ProfileSelected = BReader.ReadString();
                                //Profile Name
                                string ProfileSelectedName = BReader.ReadString();

                                GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

                                //--------------------------[SOUNDS DATA]--------------------------
                                BReader.BaseStream.Position = (StreamSoundsDictionaryOffset);
                                SectionsReader.ReadDictionaryData(BReader, DictionaryData);

                                //*===============================================================================================
                                //* CREATE SFX FILE
                                //*===============================================================================================
                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.OutputDirectory))
                                {
                                    using (BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.OutputDirectory + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                                    {
                                        //*===============================================================================================
                                        //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
                                        //*===============================================================================================
                                        Dictionary<uint, EXSoundStream> FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(DictionaryData, null, null);

                                        //*===============================================================================================
                                        //* STEP 2: CHECK DATA THAT WILL BE OUTPUTED
                                        //*===============================================================================================
                                        bool CanOutputFile = true;

                                        //Check Data
                                        foreach (KeyValuePair<uint, EXSoundStream> SoundToCheck in FinalSoundsDict)
                                        {
                                            CanOutputFile = SFX_Check.ValidateStreamingSounds(SoundToCheck.Value, null, null);
                                            if (CanOutputFile == false)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                CanOutputFile = SFX_Check.ValidateMarkers(SoundToCheck.Value.Markers, null, null);
                                                if (CanOutputFile == false)
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        //*===============================================================================================
                                        //* STEP 3: START WRITTING
                                        //*===============================================================================================
                                        if (CanOutputFile)
                                        {
                                            //Write Header
                                            SFXGenerator.WriteFileHeader(BWriter, File_Hashcode, null);

                                            //Write Sections
                                            SFXGenerator.WriteFileSections(BWriter, null);

                                            //Write Table
                                            SFXGenerator.WriteLookUptable(BWriter, FinalSoundsDict, null);

                                            //Write Data
                                            SFXGenerator.WriteStreamFile(BWriter, FinalSoundsDict, null);

                                            //*===============================================================================================
                                            //* STEP 4: WRITE FINAL OFFSETS
                                            //*===============================================================================================                                //Write Offsets
                                            SFXGenerator.WriteFinalOffsets(BWriter, null);
                                        }
                                        BWriter.Close();
                                    }
                                }
                            }
                            //*===============================================================================================
                            //* EXPORT MUSIC BANK
                            //*===============================================================================================
                            else if (TypeOfStoredData == 2)
                            {
                                uint MusicsDictionaryOffset, File_Hashcode;
                                ESF_LoadMusics SectionsReader = new ESF_LoadMusics();
                                GenerateSFXMusicBank SFXCreator = new GenerateSFXMusicBank();
                                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                                Dictionary<uint, EXMusic> DictionaryData = new Dictionary<uint, EXMusic>();
                                Dictionary<uint, EXMusic> FinalMusicsDict;

                                //*===============================================================================================
                                //* ESF FILE
                                //*===============================================================================================
                                //File Hashcode
                                File_Hashcode = BReader.ReadUInt32();
                                //Sound ID
                                BReader.ReadUInt32();
                                //Sounds List Offset
                                BReader.ReadUInt32();
                                //TreeViewData Offset
                                BReader.ReadUInt32();
                                //Dictionary Data Offset
                                MusicsDictionaryOffset = BReader.ReadUInt32();
                                //FileSize
                                BReader.ReadUInt32();
                                //File Name
                                BReader.ReadString();
                                //Profile Path
                                string ProfileSelected = BReader.ReadString();
                                //Profile Name
                                string ProfileSelectedName = BReader.ReadString();

                                GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

                                //--------------------------[SOUNDS DATA]--------------------------
                                BReader.BaseStream.Position = (MusicsDictionaryOffset);
                                SectionsReader.ReadDictionaryData(BReader, DictionaryData);

                                //*===============================================================================================
                                //* CREATE SFX FILE
                                //*===============================================================================================
                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.OutputDirectory))
                                {
                                    using (BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.OutputDirectory + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                                    {
                                        //*===============================================================================================
                                        //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
                                        //*===============================================================================================
                                        FinalMusicsDict = SFXCreator.GetFinalMusicsDictionary(DictionaryData, null, null);

                                        //*===============================================================================================
                                        //* STEP 2: CHECK DATA THAT WILL BE OUTPUTED
                                        //*===============================================================================================
                                        bool CanOutputFile = true;

                                        //Check Data
                                        foreach (KeyValuePair<uint, EXMusic> MusicToCheck in FinalMusicsDict)
                                        {
                                            CanOutputFile = SFX_Check.ValidateMusics(MusicToCheck.Value, null, null);
                                            if (CanOutputFile == false)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                CanOutputFile = SFX_Check.ValidateMarkers(MusicToCheck.Value.Markers, null, null);
                                                if (CanOutputFile == false)
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        //*===============================================================================================
                                        //* STEP 3: START WRITTING
                                        //*===============================================================================================
                                        if (CanOutputFile)
                                        {
                                            //Write Header
                                            SFXCreator.WriteFileHeader(BWriter, File_Hashcode, null);

                                            //Write Sections
                                            SFXCreator.WriteFileSections(BWriter, null);

                                            //Write Table
                                            SFXCreator.WriteFileSection1(BWriter, FinalMusicsDict, null);

                                            //Write Data
                                            SFXCreator.WriteFileSection2(BWriter, FinalMusicsDict, null);

                                            //*===============================================================================================
                                            //* STEP 4: WRITE FINAL OFFSETS
                                            //*===============================================================================================
                                            SFXCreator.WriteFinalOffsets(BWriter, null);
                                        }
                                        BWriter.Close();
                                    }
                                }
                            }
                        }

                        //Close File
                        BReader.Close();
                    }
                }
            }
            Application.Exit();
        }
    }
}
