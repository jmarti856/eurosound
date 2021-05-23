using System;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.Editors_and_Tools.StreamSoundsEditor.Debug_Writer
{
    internal class StreamSounds_DebugWriter
    {
        internal void CreateDebugFile(string FilePath, int DebugFlags)
        {
            //*===============================================================================================
            //* Global Variables
            //*===============================================================================================
            List<uint> soundsOffsets = new List<uint>();

            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            string directoryName = Path.GetDirectoryName(FilePath);

            using (StreamWriter debugFile = new StreamWriter(Path.Combine(directoryName, fileName + ".DBG")))
            {
                //Write Debug File Header
                debugFile.WriteLine(new String('/', 70));
                debugFile.WriteLine(string.Join(" ", "//", "EngineX Output", ":", FilePath));
                debugFile.WriteLine(string.Join(" ", "//", "Source File:", Path.GetFileName(FilePath)));
                debugFile.WriteLine(string.Join(" ", "//", "Output by:", Environment.UserName));
                debugFile.WriteLine(string.Join(" ", "//", "Output date:", DateTime.Now.ToString("dddd, MMMM dd, yyyy - HH:mm:ss")));
                debugFile.WriteLine(new String('/', 70));
                debugFile.WriteLine("");

                //Read File
                using (BinaryReader binaryReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read)))
                {
                    debugFile.WriteLine(new String('/', 70));
                    debugFile.WriteLine(string.Join(" ", "//", "EngineXBase Header"));
                    debugFile.WriteLine(new String('/', 70));
                    debugFile.WriteLine("");

                    debugFile.WriteLine(string.Join(" ", "//", "'MUSX' Marker"));
                    binaryReader.ReadBytes(4);
                    debugFile.WriteLine(string.Join(" ", "\tDD", "4D555358" + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File HashCode"));
                    debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "Constant offset"));
                    debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File Size"));
                    debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File start 1"));
                    uint fileStart1 = binaryReader.ReadUInt32();
                    debugFile.WriteLine(string.Join(" ", "\tDD", fileStart1.ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File length 1"));
                    uint fileStart1Length = binaryReader.ReadUInt32();
                    uint soundsCount = fileStart1Length / 4;
                    debugFile.WriteLine(string.Join(" ", "\tDD", fileStart1Length.ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File start 2"));
                    uint fileStart2 = binaryReader.ReadUInt32();
                    debugFile.WriteLine(string.Join(" ", "\tDD", fileStart2.ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File length 2"));
                    debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File start 3"));
                    debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                    debugFile.WriteLine(string.Join(" ", "//", "File length 3"));
                    debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                    debugFile.WriteLine("");

                    //Read Lookup Table
                    binaryReader.BaseStream.Seek(fileStart1, SeekOrigin.Begin);
                    for (int i = 0; i < soundsCount; i++)
                    {
                        soundsOffsets.Add(binaryReader.ReadUInt32());
                    }
                    soundsOffsets.TrimExcess();

                    //Flag 0 = File start 1
                    if (Convert.ToBoolean((DebugFlags >> 0) & 1))
                    {
                        //Write Header Section
                        debugFile.WriteLine(new String('/', 70));
                        debugFile.WriteLine(string.Join(" ", "//", "File start 1"));
                        debugFile.WriteLine(new String('/', 70));
                        debugFile.WriteLine("");

                        for (int i = 0; i < soundsOffsets.Count; i++)
                        {
                            debugFile.WriteLine(string.Join(" ", "//", "Sound", i));
                            debugFile.WriteLine(string.Join(" ", "\tDD", soundsOffsets[i].ToString("X8") + "h"));
                        }
                        debugFile.WriteLine("");
                    }

                    //Flag 1 = File start 2
                    if (Convert.ToBoolean((DebugFlags >> 1) & 1))
                    {
                        //Write Header Section
                        debugFile.WriteLine(new String('/', 70));
                        debugFile.WriteLine(string.Join(" ", "//", "File start 2"));
                        debugFile.WriteLine(new String('/', 70));
                        debugFile.WriteLine("");

                        for (int i = 0; i < soundsOffsets.Count; i++)
                        {
                            binaryReader.BaseStream.Seek(soundsOffsets[i] + fileStart2, SeekOrigin.Begin);

                            debugFile.WriteLine(string.Join(" ", "//", "Marker size", i));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Audio offset", i));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Audio size", i));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                            debugFile.WriteLine("");

                            //Stream marker header data 
                            debugFile.WriteLine(string.Join(" ", "//", "Start marker count"));
                            uint startMarkersCount = binaryReader.ReadUInt32();
                            debugFile.WriteLine(string.Join(" ", "\tDD", startMarkersCount.ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Marker count"));
                            uint markersCount = binaryReader.ReadUInt32();
                            debugFile.WriteLine(string.Join(" ", "\tDD", markersCount.ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Start marker offset"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Marker offset"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Base volume"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                            debugFile.WriteLine("");

                            //Stream marker start data 
                            debugFile.WriteLine(new String('/', 70));
                            debugFile.WriteLine(string.Join(" ", "//", "Stream marker start data "));
                            debugFile.WriteLine("");

                            debugFile.WriteLine(string.Join(" ", "//", "Name"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Position"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Music marker type"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Flags"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Extra"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Loop start"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Marker count"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Loop marker count"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Marker position"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Is instant"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Instant buffer"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "State A"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "State B"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            //Stream marker data 
                            debugFile.WriteLine(new String('/', 70));
                            debugFile.WriteLine(string.Join(" ", "//", "Stream marker data "));
                            debugFile.WriteLine("");

                            debugFile.WriteLine(string.Join(" ", "//", "Name"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Position"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Music marker type"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Flags"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Extra"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Loop start"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Marker count"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));

                            debugFile.WriteLine(string.Join(" ", "//", "Loop marker count"));
                            debugFile.WriteLine(string.Join(" ", "\tDD", binaryReader.ReadUInt32().ToString("X8") + "h"));
                        }
                    }
                    binaryReader.Close();
                }
                debugFile.Close();
            }
        }
    }
}
