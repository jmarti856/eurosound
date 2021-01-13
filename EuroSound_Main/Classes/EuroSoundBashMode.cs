using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundSoundBanksFilesFunctions;
using EuroSound_Application.GenerateSoundBankSFX;
using EuroSound_Application.SoundBanksEditor;
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
                    ESF_LoadSoundBanks SectionsReader = new ESF_LoadSoundBanks();
                    GenerateSFXSoundBank SFXGenerator = new GenerateSFXSoundBank();

                    Dictionary<uint, EXSound> SoundsList = new Dictionary<uint, EXSound>();
                    Dictionary<string, EXAudio> AudiosList = new Dictionary<string, EXAudio>();

                    Dictionary<uint, EXSound> FinalSoundsDict;
                    Dictionary<string, EXAudio> FinalAudioDataDict;

                    using (BinaryReader BReader = new BinaryReader(File.Open(Commands[1], FileMode.Open, FileAccess.Read), Encoding.ASCII))
                    {
                        if (FileReader.FileIsCorrect(BReader))
                        {
                            uint SoundsListDataOffset, AudioDataOffset, File_Hashcode;
                            sbyte TypeOfStoredData;

                            //Type of stored data
                            TypeOfStoredData = BReader.ReadSByte();
                            if (TypeOfStoredData == 0)
                            {
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
                                WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
                                GlobalPreferences.SFXOutputPath = WRegistryFunctions.LoadGeneralPreferences()[0];

                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.SFXOutputPath))
                                {
                                    BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);

                                    //*===============================================================================================
                                    //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED (20%)
                                    //*===============================================================================================
                                    //Discard SFXs that has checked as "no output"
                                    FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(SoundsList, null, null);

                                    //*===============================================================================================
                                    //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED (40%)
                                    //*===============================================================================================
                                    List<string> UsedAudios = EXSoundbanksFunctions.GetUsedAudios(FinalSoundsDict, true);

                                    //Add data
                                    FinalAudioDataDict = SFXGenerator.GetFinalAudioDictionary(UsedAudios, AudiosList, null);

                                    //*===============================================================================================
                                    //* STEP 3: START WRITTING (80%)
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
                                    //* STEP 4: WRITE FINAL DATA (80%)
                                    //*===============================================================================================
                                    //Write Data
                                    SFXGenerator.WriteFinalOffsets(BWriter, null, null);

                                    //Close file
                                    BWriter.Close();
                                }
                            }

                        }
                    }
                }
            }
            Application.Exit();
        }
    }
}
