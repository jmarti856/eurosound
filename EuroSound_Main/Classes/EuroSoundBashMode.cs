﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal class EuroSoundBashMode
    {
        internal void ExecuteCommand(string[] Commands)
        {
            if (Commands[0].Equals("/?"))
            {
                MessageBox.Show("Usage: EuroSound.exe [opts] [filename] where options are:" + Environment.NewLine + "/o            -Outputs the .ESF file" + Environment.NewLine + "/?            -Help", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    BinaryReader BReader = new BinaryReader(File.Open(Commands[1], FileMode.Open, FileAccess.Read), Encoding.ASCII);
                    if (FileReader.FileIsCorrect(BReader))
                    {
                        uint Version, SoundsListDataOffset, AudioDataOffset, File_Hashcode;
                        sbyte TypeOfStoredData;

                        /*FileVersion*/
                        Version = BReader.ReadUInt32();
                        if (Version == 11)
                        {
                            /*Type of stored data*/
                            TypeOfStoredData = BReader.ReadSByte();
                            if (TypeOfStoredData == 0)
                            {
                                //*===============================================================================================
                                //* ESF FILE
                                //*===============================================================================================
                                /*File Hashcode*/
                                File_Hashcode = BReader.ReadUInt32();
                                /*Latest SoundID value*/
                                BReader.ReadUInt32();
                                /*TreeView Data*/
                                BReader.ReadUInt32();
                                /*SoundsListData Offset -- Not used for now*/
                                SoundsListDataOffset = BReader.ReadUInt32();
                                /*AudioData Offset -- Not used for now*/
                                AudioDataOffset = BReader.ReadUInt32();
                                /*FullSize*/
                                BReader.ReadUInt32();
                                /*File Name*/
                                BReader.ReadString();

                                /*--------------------------[SOUNDS LIST DATA]--------------------------*/
                                BReader.BaseStream.Position = (SoundsListDataOffset);
                                SectionsReader.ReadSoundsListData(BReader, SoundsList);

                                /*--------------------------[AUDIO DATA]--------------------------*/
                                BReader.BaseStream.Position = (AudioDataOffset);
                                SectionsReader.ReadAudiosDictionary(BReader, AudiosList);

                                //*===============================================================================================
                                //* CREATE SFX FILE
                                //*===============================================================================================
                                WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
                                GlobalPreferences.SFXOutputPath = WRegistryFunctions.LoadGeneralPreferences();

                                string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

                                if (Directory.Exists(GlobalPreferences.SFXOutputPath))
                                {
                                    BinaryWriter BWriter = new BinaryWriter(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), Encoding.ASCII);

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
                                    /*Write Data*/
                                    SFXGenerator.WriteFileHeader(BWriter, File_Hashcode, null);

                                    //--------------------------------------[Write SECTIONS]--------------------------------------
                                    /*Write Data*/
                                    SFXGenerator.WriteFileSections(BWriter, GenericFunctions.CountNumberOfSamples(FinalSoundsDict), null);

                                    //--------------------------------------[SECTION SFX elements]--------------------------------------
                                    /*Write Data*/
                                    SFXGenerator.WriteSFXSection(BWriter, FinalSoundsDict, FinalAudioDataDict, null, null);

                                    //--------------------------------------[SECTION Sample info elements]--------------------------------------
                                    /*Write Data*/
                                    SFXGenerator.WriteSampleInfoSection(BWriter, FinalAudioDataDict, null, null);

                                    //--------------------------------------[SECTION Sample data]--------------------------------------
                                    /*Write Data*/
                                    SFXGenerator.WriteSampleDataSection(BWriter, FinalAudioDataDict, null, null);

                                    //*===============================================================================================
                                    //* STEP 4: WRITE FINAL DATA (80%)
                                    //*===============================================================================================
                                    /*Write Data*/
                                    SFXGenerator.WriteFinalOffsets(BWriter, null, null);
                                    BWriter.Close();
                                    BWriter.Dispose();
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