using EuroSound_Application.ApplicationPreferences;
using System;
using System.IO;

namespace EuroSound_Application.Musics
{
    class MusicBanks_DebugWriter
    {
        internal void CreateDebugFile(string FilePath, int DebugFlags)
        {
            //*===============================================================================================
            //* Global Variables
            //*===============================================================================================
            string FileName = Path.GetFileNameWithoutExtension(FilePath);
            uint StartMarkersCount, MarkersCount;
            uint FileStart1, FileStart2;

            using (StreamWriter DebugFile = new StreamWriter(GlobalPreferences.MusicOutputPath + "\\" + FileName + ".dbg"))
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

                    DebugFile.WriteLine(string.Join(" ", "//", "File start 1"));
                    FileStart1 = BReader.ReadUInt32();
                    DebugFile.WriteLine(string.Join(" ", "\tDD", FileStart1.ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "File length 1"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "File start 2"));
                    FileStart2 = BReader.ReadUInt32();
                    DebugFile.WriteLine(string.Join(" ", "\tDD", FileStart2.ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "File length 2"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "File start 3"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    DebugFile.WriteLine(string.Join(" ", "//", "File length 3"));
                    DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                    //Flag 0 = File start 1
                    if (Convert.ToBoolean((DebugFlags >> 0) & 1))
                    {
                        //Write Header Section
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "File start 1"));
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine("");

                        BReader.BaseStream.Seek(FileStart1, SeekOrigin.Begin);

                        DebugFile.WriteLine(string.Join(" ", "//", "Start marker count"));
                        StartMarkersCount = BReader.ReadUInt32();
                        DebugFile.WriteLine(string.Join(" ", "\tDD", StartMarkersCount.ToString("X8") + "h"));

                        DebugFile.WriteLine(string.Join(" ", "//", "Marker count"));
                        MarkersCount = BReader.ReadUInt32();
                        DebugFile.WriteLine(string.Join(" ", "\tDD", MarkersCount.ToString("X8") + "h"));

                        DebugFile.WriteLine(string.Join(" ", "//", "Start marker offset"));
                        DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                        DebugFile.WriteLine(string.Join(" ", "//", "Marker offset"));
                        DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                        DebugFile.WriteLine(string.Join(" ", "//", "Base volume"));
                        DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                        DebugFile.WriteLine("");

                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "Start Markers"));
                        DebugFile.WriteLine("");

                        for (int i = 0; i < StartMarkersCount; i++)
                        {
                            DebugFile.WriteLine(string.Join(" ", "//", "Name"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Position"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Music marker type"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Flags"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Extra"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Loop start"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Marker count"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Loop marker count"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Marker position"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Is instant"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Instant buffer"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "State A"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "State B"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                            DebugFile.WriteLine("");
                        }

                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "Markers"));
                        DebugFile.WriteLine("");

                        for (int i = 0; i < MarkersCount; i++)
                        {
                            DebugFile.WriteLine(string.Join(" ", "//", "Name"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Position"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Music marker type"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Flags"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Extra"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Loop start"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Marker count"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));

                            DebugFile.WriteLine(string.Join(" ", "//", "Loop marker count"));
                            DebugFile.WriteLine(string.Join(" ", "\tDD", BReader.ReadUInt32().ToString("X8") + "h"));
                            DebugFile.WriteLine("");
                        }
                    }

                    //Flag 1 = File start 2
                    if (Convert.ToBoolean((DebugFlags >> 1) & 1))
                    {
                        //Write Header Section
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine(string.Join(" ", "//", "Sample data"));
                        DebugFile.WriteLine(new String('/', 70));
                        DebugFile.WriteLine("");

                        BReader.BaseStream.Seek(FileStart2, SeekOrigin.Begin);

                        byte[] SampleDataSection = BReader.ReadBytes((int)FileStart2);
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
