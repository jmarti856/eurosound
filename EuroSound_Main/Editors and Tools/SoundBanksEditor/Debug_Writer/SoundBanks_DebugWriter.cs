using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.HashCodesFunctions;
using System;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.SoundBanksEditor.Debug_Writer
{
    internal class SoundBanks_DebugWriter
    {
        internal void CreateDebugFile(string FilePath, int DebugFlags)
        {
            //*===============================================================================================
            //* Global Variables
            //*===============================================================================================
            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            Dictionary<uint, uint> sfxElements = new Dictionary<uint, uint>();

            using (StreamWriter debugFileWriter = new StreamWriter(GlobalPreferences.SFXOutputPath + "\\" + fileName + ".dbg"))
            {
                //Write Debug File Header
                debugFileWriter.WriteLine(new String('/', 70));
                debugFileWriter.WriteLine(string.Join(" ", "//", "EngineX Output", ":", FilePath));
                debugFileWriter.WriteLine(string.Join(" ", "//", "Source File:", Path.GetFileName(FilePath)));
                debugFileWriter.WriteLine(string.Join(" ", "//", "Output by:", Environment.UserName));
                debugFileWriter.WriteLine(string.Join(" ", "//", "Output date:", DateTime.Now.ToString("dddd, MMMM dd, yyyy - HH:mm:ss")));
                debugFileWriter.WriteLine(new String('/', 70));
                debugFileWriter.WriteLine("");

                //Read File
                using (BinaryReader binaryReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read)))
                {
                    debugFileWriter.WriteLine(new String('/', 70));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "EngineXBase Header"));
                    debugFileWriter.WriteLine(new String('/', 70));
                    debugFileWriter.WriteLine("");

                    debugFileWriter.WriteLine(string.Join(" ", "//", "'MUSX' Marker"));
                    binaryReader.ReadBytes(4);
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", "4D555358" + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "File HashCode"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "Constant offset"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "File Size"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFileWriter.WriteLine(string.Join(" ", "//", "SFX section start"));
                    uint sfxStart = binaryReader.ReadUInt32();
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", sfxStart.ToString("X8") + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "SFX section length"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFileWriter.WriteLine(string.Join(" ", "//", "Sample info start"));
                    uint sampleInfoStart = binaryReader.ReadUInt32();
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", sampleInfoStart.ToString("X8") + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "Sample info length"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFileWriter.WriteLine(string.Join(" ", "//", "Special sample info start"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "Special sample info length"));
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFileWriter.WriteLine(string.Join(" ", "//", "Sample data start"));
                    uint sampleDataStart = binaryReader.ReadUInt32();
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", sampleDataStart.ToString("X8") + "h"));
                    debugFileWriter.WriteLine(string.Join(" ", "//", "Sample data length"));
                    uint sampleDataLength = binaryReader.ReadUInt32();
                    debugFileWriter.WriteLine(string.Join(" ", "\tDD", sampleDataLength.ToString("X8") + "h"));
                    debugFileWriter.WriteLine("");

                    //Flag 0 = SFX elements
                    if (Convert.ToBoolean((DebugFlags >> 0) & 1))
                    {
                        //Write Header Section
                        debugFileWriter.WriteLine(new String('/', 70));
                        debugFileWriter.WriteLine(string.Join(" ", "//", "SFX elements - SFX header "));
                        debugFileWriter.WriteLine(new String('/', 70));
                        debugFileWriter.WriteLine("");

                        //Read and print SFXCount
                        binaryReader.BaseStream.Seek(sfxStart, SeekOrigin.Begin);
                        uint sfxCount = binaryReader.ReadUInt32();

                        debugFileWriter.WriteLine(string.Join(" ", "//", "SFX entry count"));
                        debugFileWriter.WriteLine(string.Join(" ", "\tDD", sfxCount.ToString("X8") + "h"));

                        // SFXHashcode, SFXOffset;
                        for (int i = 0; i < sfxCount; i++)
                        {
                            //Read Data
                            uint SFXHashcode = binaryReader.ReadUInt32();
                            uint SFXOffset = binaryReader.ReadUInt32();

                            //Add Data To Dictionary
                            if (!sfxElements.ContainsKey(SFXOffset))
                            {
                                sfxElements.Add(SFXOffset, SFXHashcode);
                            }

                            //Write Debug
                            debugFileWriter.WriteLine(string.Join(" ", "//", "HashCode =", Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, GlobalPreferences.SfxPrefix | SFXHashcode), "-", (GlobalPreferences.SfxPrefix | SFXHashcode).ToString("X8") + "h"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tHashCode", SFXHashcode.ToString("X8") + "h"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tOffset", SFXOffset.ToString("X8") + "h"));
                        }
                        debugFileWriter.WriteLine("");

                        foreach (KeyValuePair<uint, uint> dictionaryItem in sfxElements)
                        {
                            debugFileWriter.WriteLine(new String('/', 70));
                            debugFileWriter.WriteLine(string.Join(" ", "//", Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, GlobalPreferences.SfxPrefix | dictionaryItem.Value), "-", (GlobalPreferences.SfxPrefix | dictionaryItem.Value).ToString("X8") + "h"));
                            debugFileWriter.WriteLine(new String('/', 70));
                            debugFileWriter.WriteLine("");
                            binaryReader.BaseStream.Seek(dictionaryItem.Key + sfxStart, SeekOrigin.Begin);

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Ducker length"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Min delay"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Max delay"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Inner radius real"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Outer radius real"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Reverb send"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Tracking type"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Max voices"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Priority"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Ducker"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Master volume"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Flags"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt16().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Sample count"));
                            short sampleCount = binaryReader.ReadInt16();
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", sampleCount.ToString("X8") + "h"));

                            for (int i = 0; i < sampleCount; i++)
                            {
                                debugFileWriter.WriteLine(new String('/', 70));
                                debugFileWriter.WriteLine(string.Join(" ", "//", "Sample", i));
                                debugFileWriter.WriteLine("");
                                debugFileWriter.WriteLine(string.Join(" ", "//", "File reference"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                                debugFileWriter.WriteLine(string.Join(" ", "//", "Pitch offset"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                                debugFileWriter.WriteLine(string.Join(" ", "//", "Random pitch offset"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadInt16().ToString("X8") + "h"));

                                debugFileWriter.WriteLine(string.Join(" ", "//", "Base volume"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                                debugFileWriter.WriteLine(string.Join(" ", "//", "Random volume offset"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                                debugFileWriter.WriteLine(string.Join(" ", "//", "Pan"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));

                                debugFileWriter.WriteLine(string.Join(" ", "//", "Random pan"));
                                debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadSByte().ToString("X8") + "h"));
                                binaryReader.ReadBytes(2);
                            }
                            debugFileWriter.WriteLine("");
                        }
                    }
                    //Flag 1 = Sample info elements
                    if (Convert.ToBoolean((DebugFlags >> 1) & 1))
                    {
                        //Write Header Section
                        debugFileWriter.WriteLine(new String('/', 70));
                        debugFileWriter.WriteLine(string.Join(" ", "//", "Sample info elements "));
                        debugFileWriter.WriteLine(new String('/', 70));
                        debugFileWriter.WriteLine("");

                        binaryReader.BaseStream.Seek(sampleInfoStart, SeekOrigin.Begin);

                        debugFileWriter.WriteLine(string.Join(" ", "//", "Sample info count"));
                        uint sfxSamplesCount = binaryReader.ReadUInt32();
                        debugFileWriter.WriteLine(string.Join(" ", "\tDD", sfxSamplesCount.ToString("X8") + "h"));

                        for (int i = 0; i < sfxSamplesCount; i++)
                        {
                            debugFileWriter.WriteLine(new String('/', 70));
                            debugFileWriter.WriteLine(string.Join(" ", "//", "Sample -", i));
                            debugFileWriter.WriteLine("");
                            debugFileWriter.WriteLine(string.Join(" ", "//", "Flags"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Address"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "PCM data size"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Frequency"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Real size"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Number of channels"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Bits per sample"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "PSI sample header"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Loop offset"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFileWriter.WriteLine(string.Join(" ", "//", "Duration"));
                            debugFileWriter.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                            debugFileWriter.WriteLine("");
                        }
                        debugFileWriter.WriteLine("");
                    }
                    //Flag 2 = Sampla Data
                    if (Convert.ToBoolean((DebugFlags >> 2) & 1))
                    {
                        //Write Header Section
                        debugFileWriter.WriteLine(new String('/', 70));
                        debugFileWriter.WriteLine(string.Join(" ", "//", "Sample data"));
                        debugFileWriter.WriteLine(new String('/', 70));
                        debugFileWriter.WriteLine("");

                        binaryReader.BaseStream.Seek(sampleDataStart, SeekOrigin.Begin);

                        byte[] sampleDataSection = binaryReader.ReadBytes((int)sampleDataLength);
                        for (int i = 0; i < sampleDataSection.Length; i++)
                        {
                            debugFileWriter.Write(sampleDataSection[i]);
                        }
                        debugFileWriter.WriteLine("");
                    }
                    binaryReader.Close();
                }
                debugFileWriter.Close();
            }
        }
    }
}
