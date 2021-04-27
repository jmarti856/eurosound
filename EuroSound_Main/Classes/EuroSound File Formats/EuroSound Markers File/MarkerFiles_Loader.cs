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
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private List<string> ImportResults = new List<string>();
        MarkersFunctions MKFunctions = new MarkersFunctions();

        internal IList<string> LoadSTRMarkersFile(string FilePath, EXSoundStream StreamSoundToModify)
        {
            string[] FileLines = File.ReadAllLines(FilePath);
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingMRKFile"));

            //Check file is not empty
            if (FileLines.Length > 0)
            {
                //Check file is correct
                CurrentKeyWord = GetKeyWord(FileLines[0]);
                if (CurrentKeyWord.Equals("EUROSOUND"))
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
                                ReadSTRStartMarkersBlock(FileLines, i, StreamSoundToModify);
                            }

                            //Check for markers block
                            if (CurrentKeyWord.Equals("STRMARKERS"))
                            {
                                i++;
                                StreamSoundToModify.Markers.Clear();
                                ReadSTRMarkersBlock(FileLines, i, StreamSoundToModify);
                            }
                        }
                    }
                }
                else
                {
                    ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
                }
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }

        private void ReadSTRStartMarkersBlock(string[] FileLines, int CurrentIndex, EXSoundStream StreamSound)
        {
            uint Position = 0, MarkerType = 0, MarkerPos = 0, LoopStart = 0, LoopMarkerCount = 0, StateA = 0, StateB = 0;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                string CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("MARKER"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                        string[] KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                        if (KeyWordValues.Length > 0)
                        {
                            uint NumericValue;
                            switch (CurrentKeyWord)
                            {
                                case "POSITION":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        Position = NumericValue;
                                    }
                                    break;
                                case "TYPE":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        MarkerType = NumericValue;
                                    }
                                    break;
                                case "LOOPSTART":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        LoopStart = NumericValue;
                                    }
                                    break;
                                case "LOOPMARKERCOUNT":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        LoopMarkerCount = NumericValue;
                                    }
                                    break;
                                case "MARKERPOS":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        MarkerPos = NumericValue;
                                    }
                                    break;
                                case "STATEA":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        StateA = NumericValue;
                                    }
                                    break;
                                case "STATEB":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        StateB = NumericValue;
                                    }
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

        private void ReadSTRMarkersBlock(string[] FileLines, int CurrentIndex, EXSoundStream StreamSound)
        {
            int Name = 0;
            uint Position = 0, MarkerType = 0, MarkerCount = 0, LoopStart = 0, LoopMarkerCount = 0;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                string CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("MARKER"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                        string[] KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                        if (KeyWordValues.Length > 0)
                        {
                            uint NumericValue;
                            switch (CurrentKeyWord)
                            {
                                case "NAME":
                                    int NameValue;
                                    if (int.TryParse(KeyWordValues[0], out NameValue))
                                    {
                                        Name = NameValue;
                                    }
                                    break;
                                case "POSITION":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        Position = NumericValue;
                                    }
                                    break;
                                case "TYPE":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        MarkerType = NumericValue;
                                    }
                                    break;
                                case "MARKERCOUNT":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        MarkerCount = NumericValue;
                                    }
                                    break;
                                case "LOOPSTART":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        LoopStart = NumericValue;
                                    }
                                    break;
                                case "LOOPMARKERCOUNT":
                                    if (uint.TryParse(KeyWordValues[0], out NumericValue))
                                    {
                                        LoopMarkerCount = NumericValue;
                                    }
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
