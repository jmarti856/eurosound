using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EuroSound_Application.MarkerFiles
{
    class MarkerFiles_Loader
    {
        private List<string> ImportResults = new List<string>();
        MarkersFunctions MKFunctions = new MarkersFunctions();

        internal List<string> LoadSTRMarkersFile(string FilePath, EXSoundStream StreamSoundToModify)
        {
            string[] FileLines = File.ReadAllLines(FilePath);
            string[] KeyWordValues = null;
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingMRKFile"));

            //Check File
            if (FileLines[0].Equals("*EUROSOUND_MARKERS_FILE V1.0"))
            {
                for (int i = 1; i < FileLines.Length; i++)
                {
                    CurrentKeyWord = GetKeyWord(FileLines[i]);
                    if (string.IsNullOrEmpty(CurrentKeyWord) || CurrentKeyWord.Equals("COMMENT"))
                    {
                        continue;
                    }
                    else
                    {
                        //Check for start markers block
                        if (CurrentKeyWord.Equals("STRSTARTMARKERS"))
                        {
                            i++;
                            StreamSoundToModify.StartMarkers.Clear();
                            ReadSTRStartMarkersBlock(FileLines, i, CurrentKeyWord, KeyWordValues, StreamSoundToModify);
                        }

                        //Check for markers block
                        if (CurrentKeyWord.Equals("STRMARKERS"))
                        {
                            i++;
                            StreamSoundToModify.Markers.Clear();
                            ReadSTRMarkersBlock(FileLines, i, CurrentKeyWord, KeyWordValues, StreamSoundToModify);
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

        private void ReadSTRStartMarkersBlock(string[] FileLines, int CurrentIndex, string CurrentKeyWord, string[] KeyWordValues, EXSoundStream StreamSound)
        {
            uint Position = 0, MarkerType = 0, MarkerPos = 0, LoopStart = 0, LoopMarkerCount = 0, StateA = 0, StateB = 0;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("MARKER"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                        KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                        if (KeyWordValues.Length > 0)
                        {
                            switch (CurrentKeyWord)
                            {
                                case "POSITION":
                                    Position = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "TYPE":
                                    MarkerType = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "LOOPSTART":
                                    LoopStart = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "LOOPMARKERCOUNT":
                                    LoopMarkerCount = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "MARKERPOS":
                                    MarkerPos = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "STATEA":
                                    StateA = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "STATEB":
                                    StateB = uint.Parse(KeyWordValues[0]);
                                    break;
                            }
                        }
                        else
                        {
                            if (!CurrentKeyWord.Equals("COMMENT"))
                            {
                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains any value"));
                                break;
                            }
                        }
                        CurrentIndex++;
                    }
                    MKFunctions.CreateStartMarker(StreamSound.StartMarkers, Position, MarkerType, 2, LoopStart, LoopMarkerCount, MarkerPos, StateA, StateB);
                    CurrentIndex++;
                }
            }
        }

        private void ReadSTRMarkersBlock(string[] FileLines, int CurrentIndex, string CurrentKeyWord, string[] KeyWordValues, EXSoundStream StreamSound)
        {
            int Name = 0;
            uint Position = 0, MarkerType = 0, MarkerCount = 0, LoopStart = 0, LoopMarkerCount = 0;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("MARKER"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                        KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                        if (KeyWordValues.Length > 0)
                        {
                            switch (CurrentKeyWord)
                            {
                                case "NAME":
                                    Name = int.Parse(KeyWordValues[0]);
                                    break;
                                case "POSITION":
                                    Position = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "TYPE":
                                    MarkerType = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "MARKERCOUNT":
                                    MarkerCount = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "LOOPSTART":
                                    LoopStart = uint.Parse(KeyWordValues[0]);
                                    break;
                                case "LOOPMARKERCOUNT":
                                    LoopMarkerCount = uint.Parse(KeyWordValues[0]);
                                    break;
                            }
                        }
                        else
                        {
                            if (!CurrentKeyWord.Equals("COMMENT"))
                            {
                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains any value"));
                                break;
                            }
                        }
                        CurrentIndex++;
                    }
                    MKFunctions.CreateMarker(StreamSound.Markers, Name, Position, MarkerType, 2, MarkerCount, LoopStart, LoopMarkerCount);
                    CurrentIndex++;
                }
            }
        }

        private string GetKeyWord(string FileLine)
        {
            string KeyWord = string.Empty;

            MatchCollection Matches = Regex.Matches(FileLine, @"(?<=[*])\w[A-Z]+");
            if (Matches.Count > 0)
            {
                KeyWord = Matches[0].ToString();
            }

            return KeyWord;
        }

        private string[] GetKeyValues(string FileLine)
        {
            string[] Values = Regex.Matches(FileLine, @"(?<=[*])\w+[\s-[\r\n]]*=[\s-[\r\n]]*(.*?)\r?$").Cast<Match>().Select(x => x.Groups[1].Value).ToArray();
            return Values;
        }
    }
}
