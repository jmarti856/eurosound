using EuroSound_Application.StreamSounds;
using System;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.Editors_and_Tools.StreamSoundsEditor.Classes
{
    class MarkerFiles
    {
        public List<string> ReadMarkersFile(string FilePath)
        {
            string[] FileData = File.ReadAllLines(FilePath);
            string[] SplittedInfo;
            List<string> Markers = new List<string>();

            //File "Header"
            if (FileData[0].Trim().Equals("Markers", StringComparison.OrdinalIgnoreCase))
            {
                if (FileData[1].Trim().Equals("{"))
                {
                    //Read stored Markers
                    for (int i = 2; i < FileData.Length; i++)
                    {
                        while (!FileData[i].Trim().Equals("}", StringComparison.OrdinalIgnoreCase))
                        {
                            //Read info between [Markers { }]
                            if (FileData[i].Trim().Equals("Marker", StringComparison.OrdinalIgnoreCase))
                            {
                                i++;
                                //Read info: {Name, Pos, Type, Flags, Extra}
                                if (FileData[i].Trim().Equals("{", StringComparison.OrdinalIgnoreCase))
                                {
                                    i++;
                                    while (!FileData[i].Trim().Equals("}", StringComparison.OrdinalIgnoreCase))
                                    {
                                        SplittedInfo = FileData[i].Trim().Split('=');
                                        if (SplittedInfo.Length == 2)
                                        {
                                            Markers.Add(SplittedInfo[1]);
                                        }
                                        i++;
                                    }
                                }
                                //File may be corrupt
                                else
                                {
                                    Markers.Clear();
                                    break;
                                }
                            }
                            //File may be corrupt
                            else
                            {
                                Markers.Clear();
                                break;
                            }
                        }
                    }
                }
            }
            //Clear array
#pragma warning disable IDE0059 // Asignación innecesaria de un valor
            FileData = null;
            SplittedInfo = null;
#pragma warning restore IDE0059 // Asignación innecesaria de un valor

            //Integrity check
            if (!(Markers.Count % 5 == 0))
            {
                Markers.Clear();
            }

            return Markers;
        }

        public void ApplyMarkersReadedData(EXSoundStream SoundToModify, List<string> MarkersData)
        {

        }
    }
}
