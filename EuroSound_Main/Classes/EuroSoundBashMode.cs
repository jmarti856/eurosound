using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundMusicFilesFunctions;
using EuroSound_Application.EuroSoundSoundBanksFilesFunctions;
using EuroSound_Application.GenerateSoundBankSFX;
using EuroSound_Application.HashCodesFunctions;
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
        internal void ExecuteCommand(string[] Commands)
        {
            if (Commands[0].Equals("/?"))
            {
                MessageBox.Show(string.Format("Usage: EuroSound.exe [opts] [filename] where options are:\n /o            -Outputs the .ESF file\n/?            -Help"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Commands[0].Equals("/o"))
            {
                if (File.Exists(Commands[1]))
                {
                    EuroSoundFiles FileReader = new EuroSoundFiles();

                    using (BinaryReader BReader = new BinaryReader(File.Open(Commands[1], FileMode.Open, FileAccess.Read), Encoding.ASCII))
                    {
                        if (FileReader.FileIsCorrect(BReader))
                        {
                            sbyte TypeOfStoredData;

                            //*===============================================================================================
                            //* LOAD HASTABLES AND NEEDED PREFERENCES
                            //*===============================================================================================
                            WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
                            GlobalPreferences.SFXOutputPath = WRegistryFunctions.LoadOutputFolders("SoundsOutputDirectory", "Path");

                            GlobalPreferences.HT_SoundsPath = WRegistryFunctions.LoadHashtablesFiles("HT_Sound", "Path");
                            GlobalPreferences.HT_SoundsMD5 = WRegistryFunctions.LoadHashtablesFiles("HT_Sound", "MD5");
                            Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);

                            //Type of stored data
                            TypeOfStoredData = BReader.ReadSByte();

                            //Soundbank
                            if (TypeOfStoredData == 0)
                            {
                                uint SoundsListDataOffset, AudioDataOffset, File_Hashcode;
                                ESF_LoadSoundBanks SectionsReader = new ESF_LoadSoundBanks();
                                GenerateSFXSoundBank SFXGenerator = new GenerateSFXSoundBank();

                                Dictionary<uint, EXSound> SoundsList = new Dictionary<uint, EXSound>();
                                Dictionary<string, EXAudio> AudiosList = new Dictionary<string, EXAudio>();

                                Dictionary<uint, EXSound> FinalSoundsDict;
                                Dictionary<string, EXAudio> FinalAudioDataDict;

                                //*===============================================================================================
                                //* ESF FILE
                                //*===============================================================================================
                                //File Hashcode
                                File_Hashcode = BReader.ReadUInt32();
                                //Latest SoundID value
                                BReader.ReadUInt32();
                                //TreeView Data
                                BReader.ReadUInt32();
                                //SoundsListData Offset -- Not used for now
                                SoundsListDataOffset = BReader.ReadUInt32();
                                //AudioData Offset -- Not used for now
                                AudioDataOffset = BReader.ReadUInt32();
                                //FullSize
                                BReader.ReadUInt32();
                                //File Name
                                BReader.ReadString();

                                //--------------------------[SOUNDS LIST DATA]--------------------------
                                BReader.BaseStream.Position = (SoundsListDataOffset);
                                SectionsReader.ReadSoundsListData(BReader, SoundsList);

                                //--------------------------[AUDIO DATA]--------------------------
                                BReader.BaseStream.Position = (AudioDataOffset);
                                SectionsReader.ReadAudioDataDictionary(BReader, AudiosList);

                                //*===============================================================================================
                                //* CREATE SFX FILE
                                //*===============================================================================================
                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.SFXOutputPath))
                                {
                                    using (BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                                    {
                                        //*===============================================================================================
                                        //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
                                        //*===============================================================================================
                                        //Discard SFXs that has checked as "no output"
                                        FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(SoundsList, null, null);

                                        //*===============================================================================================
                                        //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED
                                        //*===============================================================================================
                                        IEnumerable<string> UsedAudios = EXSoundbanksFunctions.GetAudiosToExport(FinalSoundsDict);

                                        //Add data
                                        FinalAudioDataDict = SFXGenerator.GetFinalAudioDictionaryPCMData(UsedAudios, AudiosList, null);

                                        //*===============================================================================================
                                        //* STEP 3: START WRITTING
                                        //*===============================================================================================
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
                                        SFXGenerator.WriteSampleDataSection(BWriter, FinalAudioDataDict, null, null);

                                        //*===============================================================================================
                                        //* STEP 4: WRITE FINAL OFFSETS
                                        //*===============================================================================================
                                        //Write Data
                                        SFXGenerator.WriteFinalOffsets(BWriter, null, null);

                                        //Close File
                                        BWriter.Close();
                                    }
                                }
                            }
                            //Stream Soundbank
                            else if (TypeOfStoredData == 1)
                            {
                                uint StreamSoundsDictionaryOffset, File_Hashcode;
                                ESF_LoadStreamSounds SectionsReader = new ESF_LoadStreamSounds();
                                GenerateSFXStreamedSounds SFXGenerator = new GenerateSFXStreamedSounds();
                                Dictionary<uint, EXSoundStream> DictionaryData = new Dictionary<uint, EXSoundStream>();
                                Dictionary<uint, EXSoundStream> FinalSoundsDict;

                                //*===============================================================================================
                                //* ESF FILE
                                //*===============================================================================================
                                //File Hashcode
                                File_Hashcode = BReader.ReadUInt32();
                                //Sound ID
                                BReader.ReadUInt32();
                                //Sounds List Offset
                                BReader.ReadUInt32();//Only Used in the "Frm_NewStreamSound" Form
                                //TreeViewData Offset
                                BReader.ReadUInt32();
                                //Dictionary Data Offset
                                StreamSoundsDictionaryOffset = BReader.ReadUInt32();
                                //FileSize
                                BReader.ReadUInt32();
                                //File Name
                                BReader.ReadString();

                                //--------------------------[SOUNDS DATA]--------------------------
                                BReader.BaseStream.Position = (StreamSoundsDictionaryOffset);
                                SectionsReader.ReadDictionaryData(BReader, DictionaryData);

                                //*===============================================================================================
                                //* CREATE SFX FILE
                                //*===============================================================================================
                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.SFXOutputPath))
                                {
                                    using (BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                                    {
                                        //*===============================================================================================
                                        //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED (20%)
                                        //*===============================================================================================
                                        FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(DictionaryData, null, null);

                                        //*===============================================================================================
                                        //* STEP 2: START WRITTING
                                        //*===============================================================================================
                                        //Write Header
                                        SFXGenerator.WriteFileHeader(BWriter, File_Hashcode, null);

                                        //Write Sections
                                        SFXGenerator.WriteFileSections(BWriter, null);

                                        //Write Table
                                        SFXGenerator.WriteLookUptable(BWriter, FinalSoundsDict, null);

                                        //Write Data
                                        SFXGenerator.WriteStreamFile(BWriter, FinalSoundsDict, null);

                                        //*===============================================================================================
                                        //* STEP 3: WRITE FINAL OFFSETS
                                        //*===============================================================================================                                //Write Offsets
                                        SFXGenerator.WriteFinalOffsets(BWriter, null);
                                        BWriter.Close();
                                    }
                                }
                            }
                            //MusicBank
                            else if (TypeOfStoredData == 2)
                            {

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
