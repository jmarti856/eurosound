using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.HashCodesFunctions;
using System;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.SoundBanksEditor.Debug_Writer
{
    class SoundBanks_DebugWriter
    {
        internal void CreateDebugFile(string FilePath, int DebugFlags)
        {
            //*===============================================================================================
            //* Global Variables
            //*===============================================================================================
            string FileName = Path.GetFileNameWithoutExtension(FilePath);
            uint SFXStart, SampleInfoStart, SampleDataStart, SampleDataLength;
            uint SFXCount, SFXHashcode, SFXOffset;
            int SampleCount;
            uint SFXSamplesCount;
            Dictionary<uint, uint> SFXElements = new Dictionary<uint, uint>();

            using (StreamWriter DebugFile = new StreamWriter(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".dbg"))
            {
                //Write Debug File Header
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine(string.Join(" ", "//", "EngineX Output", ":", FilePath));
                DebugFile.WriteLine(string.Join(" ", "//", "Source File:", Path.GetFileName(FilePath)));
                DebugFile.WriteLine(string.Join(" ", "//", "Output by:", Environment.UserName));
                DebugFile.WriteLine(string.Join(" ", "//", "Output date:", DateTime.Now.ToString("dddd, MMMM dd, yyyy - HH:mm:ss")));
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("");

                //Read File
                using (BinaryReader BReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read)))
                {
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine(string.Join(" ", "//", "EngineXBase Header"));
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine("");

                    DebugFile.WriteLine(string.Join(" ", "//", "'MUSX' Marker"));
                    BReader.ReadBytes(4);
                    DebugFile.WriteLine(string.Join(" ", "\tDD", "4D555358" + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "File HashCode"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "Constant offset"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "File Size"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "SFX section start"));
                    SFXStart = BReader.ReadUInt32();
                    DebugFile.WriteLine(string.Join(" ", "\tDD", SFXStart.ToString("X8") + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "SFX section length"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "Sample info start"));
                    SampleInfoStart = BReader.ReadUInt32();
                    DebugFile.WriteLine(string.Join(" ", "\tDD", SampleInfoStart.ToString("X8") + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "Sample info length"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "Special sample info start"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "Special sample info length"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "Sample data start"));
                    SampleDataStart = BReader.ReadUInt32();
                    DebugFile.WriteLine(string.Join(" ", "\tDD", SampleDataStart.ToString("X8") + "h"));
                    DebugFile.WriteLine(string.Join(" ", "//", "Sample data length"));
                    SampleDataLength = BReader.ReadUInt32();
                    DebugFile.WriteLine(string.Join(" ", "\tDD", SampleDataLength.ToString("X8") + "h"));
                    DebugFile.WriteLine("");

                    //Flag 0 = SFX elements
                    if (Convert.ToBoolean((DebugFlags >> 0) & 1))
                    {
                        //Write Header Section
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "SFX elements - SFX header "));
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine("");

                        //Read and print SFXCount
                        BReader.BaseStream.Seek(SFXStart, SeekOrigin.Begin);
                        SFXCount = BReader.ReadUInt32();

                        DebugFile.WriteLine(string.Join(" ", "//", "SFX entry count"));
                        DebugFile.WriteLine(string.Join(" ", "\tDD", SFXCount.ToString("X8") + "h"));

                        // SFXHashcode, SFXOffset;
                        for (int i = 0; i < SFXCount; i++)
                        {
                            //Read Data
                            SFXHashcode = BReader.ReadUInt32();
                            SFXOffset = BReader.ReadUInt32();

                            //Add Data To Dictionary
                            if (!SFXElements.ContainsKey(SFXOffset))
                            {
                                SFXElements.Add(SFXOffset, SFXHashcode);
                            }

                            //Write Debug
                            DebugFile.WriteLine(string.Join(" ", "//", "HashCode =", Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, 0x1A000000 | SFXHashcode), "-", (0x1A000000 | SFXHashcode).ToString("X8") + "h"));
                            DebugFile.WriteLine(string.Join(" ", "\tHashCode", SFXHashcode.ToString("X8") + "h"));
                            DebugFile.WriteLine(string.Join(" ", "\tOffset", SFXOffset.ToString("X8") + "h"));
                        }
                        DebugFile.WriteLine("");

                        foreach (KeyValuePair<uint, uint> DictionaryItem in SFXElements)
                        {
                            DebugFile.WriteLine(new String('/', 70));
                            DebugFile.WriteLine(string.Join(" ", "//", Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, 0x1A000000 | DictionaryItem.Value), "-", (0x1A000000 | DictionaryItem.Value).ToString("X8") + "h"));
                            DebugFile.WriteLine(new String('/', 70));
                            DebugFile.WriteLine("");
                            BReader.BaseStream.Seek(DictionaryItem.Key + SFXStart, SeekOrigin.Begin);

                            DebugFile.WriteLine(string.Join(" ", "//", "Ducker length"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Min delay"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Max delay"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Inner radius real"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Outer radius real"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Reverb send"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Tracking type"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Max voices"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Priority"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Ducker"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Master volume"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Flags"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt16().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Sample count"));
                            SampleCount = BReader.ReadInt16();
                            DebugFile.WriteLine(string.Join(" ", "\tDD", SampleCount.ToString("X8") + "h"));

                            for (int i = 0; i < SampleCount; i++)
                            {
                                DebugFile.WriteLine(new String('/', 70));
                                DebugFile.WriteLine(string.Join(" ", "//", "Sample", i));
                                DebugFile.WriteLine("");
                                DebugFile.WriteLine(string.Join(" ", "//", "File reference"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                                DebugFile.WriteLine(string.Join(" ", "//", "Pitch offset"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                                DebugFile.WriteLine(string.Join(" ", "//", "Random pitch offset"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadInt16().ToString("X8") + "h"));

                                DebugFile.WriteLine(string.Join(" ", "//", "Base volume"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                                DebugFile.WriteLine(string.Join(" ", "//", "Random volume offset"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                                DebugFile.WriteLine(string.Join(" ", "//", "Pan"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));

                                DebugFile.WriteLine(string.Join(" ", "//", "Random pan"));
                                DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadSByte().ToString("X8") + "h"));
                                BReader.ReadBytes(2);
                            }
                            DebugFile.WriteLine("");
                        }
                    }
                    //Flag 1 = Sample info elements
                    if (Convert.ToBoolean((DebugFlags >> 1) & 1))
                    {
                        //Write Header Section
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "Sample info elements "));
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine("");

                        BReader.BaseStream.Seek(SampleInfoStart, SeekOrigin.Begin);

                        DebugFile.WriteLine(string.Join(" ", "//", "Sample info count"));
                        SFXSamplesCount = BReader.ReadUInt32();
                        DebugFile.WriteLine(string.Join(" ", "\tDD", SFXSamplesCount.ToString("X8") + "h"));

                        for (int i = 0; i < SFXSamplesCount; i++)
                        {
                            DebugFile.WriteLine(new String('/', 70));
                            DebugFile.WriteLine(string.Join(" ", "//", "Sample -", i));
                            DebugFile.WriteLine("");
                            DebugFile.WriteLine(string.Join(" ", "//", "Flags"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Address"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "PCM data size"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Frequency"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Real size"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Number of channels"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Bits per sample"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "PSI sample header"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Loop offset"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Duration"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                            DebugFile.WriteLine("");
                        }
                        DebugFile.WriteLine("");
                    }
                    //Flag 2 = Sampla Data
                    if (Convert.ToBoolean((DebugFlags >> 2) & 1))
                    {
                        //Write Header Section
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "Sample data"));
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine("");

                        BReader.BaseStream.Seek(SampleDataStart, SeekOrigin.Begin);

                        byte[] SampleDataSection = BReader.ReadBytes((int)SampleDataLength);
                        for (int i = 0; i < SampleDataSection.Length; i++)
                        {
                            DebugFile.Write(SampleDataSection[i]);
                        }
                        DebugFile.WriteLine("");
                    }
                    BReader.Close();
                }
                DebugFile.Close();
            }
        }
    }
}
