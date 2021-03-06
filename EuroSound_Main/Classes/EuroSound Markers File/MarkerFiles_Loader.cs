using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.MarkerFiles
{
    class MarkerFiles_Loader
    {
        private List<string> ImportResults = new List<string>();
        MarkersFunctions MKFunctions = new MarkersFunctions();

        internal List<string> LoadSTRMarkersFile(string FilePath, EXSoundStream StreamSoundToModify)
        {
            string[] lines = File.ReadAllLines(FilePath);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingMRKFile"));

            //Check File
            if (lines[0].Equals("*EUROSOUND_MARKERS_FILE V1.0"))
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i]) || lines[i].StartsWith("*COMMENT"))
                    {
                        continue;
                    }
                    else
                    {
                        //Check for start markers block
                        if (lines[i].Trim().StartsWith("*STRSTARTMARKERS"))
                        {
                            i++;
                            StreamSoundToModify.StartMarkers.Clear();
                            ReadSTRStartMarkersBlock(lines, i, StreamSoundToModify);
                        }

                        //Check for markers block
                        if (lines[i].Trim().StartsWith("*STRMARKERS"))
                        {
                            i++;
                            StreamSoundToModify.Markers.Clear();
                            ReadSTRMarkersBlock(lines, i, StreamSoundToModify);
                        }
                    }
                }
            }
            else
            {
                ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }

        private void ReadSTRStartMarkersBlock(string[] FileLines, int CurrentIndex, EXSoundStream StreamSound)
        {
            string[] SplitedData;
            uint Position = 0, MarkerType = 0, MarkerPos = 0, StateA = 0, StateB = 0;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                if (FileLines[CurrentIndex].Trim().StartsWith("*MARKER"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        SplitedData = FileLines[CurrentIndex].Trim().Split('=');
                        if (SplitedData[0].StartsWith("*POSITION"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                Position = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *POSITION does not have a valid value"));
                                break;
                            }
                        }
                        if (SplitedData[0].StartsWith("*TYPE"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                MarkerType = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MUSICMARKERTYPE does not have a valid value"));
                                break;
                            }
                        }
                        if (SplitedData[0].StartsWith("*MARKERPOS"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                MarkerPos = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MARKERPOS does not have a valid value"));
                                break;
                            }
                        }
                        if (SplitedData[0].StartsWith("*STATEA"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                StateA = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *STATEA does not have a valid value"));
                                break;
                            }
                        }
                        if (SplitedData[0].StartsWith("*STATEB"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                StateB = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *STATEB does not have a valid value"));
                                break;
                            }
                        }
                        CurrentIndex++;
                    }
                    MKFunctions.CreateStartMarker(StreamSound.StartMarkers, Position, MarkerType, MarkerPos, StateA, StateB);
                    CurrentIndex++;
                }
            }
        }

        private void ReadSTRMarkersBlock(string[] FileLines, int CurrentIndex, EXSoundStream StreamSound)
        {
            int Name = 0;
            string[] SplitedData;
            uint Position = 0, MarkerType = 0, MarkerCount = 0, LoopStart = 0, LoopMarkerCount = 0;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                if (FileLines[CurrentIndex].Trim().StartsWith("*MARKER"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        SplitedData = FileLines[CurrentIndex].Trim().Split('=');
                        if (FileLines[CurrentIndex].Trim().StartsWith("*NAME"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                Name = int.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *NAME does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*POSITION"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                Position = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *POSITION does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*TYPE"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                MarkerType = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MUSICMARKERTYPE does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*MARKERCOUNT"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                MarkerCount = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MARKERCOUNT does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*LOOPSTART"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                LoopStart = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *LOOPSTART does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*LOOPMARKERCOUNT"))
                        {
                            if (SplitedData.Length > 1)
                            {
                                LoopMarkerCount = uint.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *LOOPMARKERCOUNT does not have a valid value"));
                                break;
                            }
                        }
                        CurrentIndex++;
                    }
                    MKFunctions.CreateMarker(StreamSound.Markers, Name, Position, MarkerType, MarkerCount, LoopStart, LoopMarkerCount);
                    CurrentIndex++;
                }
            }
        }
    }
}
