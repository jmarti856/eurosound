using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.Musics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EuroSound_Application.MarkerFiles
{
    class MarkerFilesMusic_Loader
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private List<string> ImportResults = new List<string>();
        MarkersFunctions MKFunctions = new MarkersFunctions();

        internal IList<string> LoadMusicMarkersFile(string FilePath, EXMusic MusicToModify)
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
                                MusicToModify.StartMarkers.Clear();
                                ReadSTRStartMarkersBlock(FileLines, i, MusicToModify);
                            }

                            //Check for markers block
                            if (CurrentKeyWord.Equals("STRMARKERS"))
                            {
                                i++;
                                MusicToModify.Markers.Clear();
                                ReadMusicMarkersBlock(FileLines, i, MusicToModify);
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

        private void ReadSTRStartMarkersBlock(string[] FileLines, int CurrentIndex, EXMusic Music)
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
                            switch (CurrentKeyWord)
                            {
                                case "POSITION":
                                    if (uint.TryParse(KeyWordValues[0], out uint PositionParsed))
                                    {
                                        Position = PositionParsed;
                                    }
                                    break;
                                case "TYPE":
                                    if (uint.TryParse(KeyWordValues[0], out uint MarkerTypeParsed))
                                    {
                                        MarkerType = MarkerTypeParsed;
                                    }
                                    break;
                                case "LOOPSTART":
                                    if (uint.TryParse(KeyWordValues[0], out uint LoopStartParsed))
                                    {
                                        LoopStart = LoopStartParsed;
                                    }
                                    break;
                                case "LOOPMARKERCOUNT":
                                    if (uint.TryParse(KeyWordValues[0], out uint LoopMarkerCountParsed))
                                    {
                                        LoopMarkerCount = LoopMarkerCountParsed;
                                    }
                                    break;
                                case "MARKERPOS":
                                    if (uint.TryParse(KeyWordValues[0], out uint MarkerPosParsed))
                                    {
                                        MarkerPos = MarkerPosParsed;
                                    }
                                    break;
                                case "STATEA":
                                    if (uint.TryParse(KeyWordValues[0], out uint StateAParsed))
                                    {
                                        StateA = StateAParsed;
                                    }
                                    break;
                                case "STATEB":
                                    if (uint.TryParse(KeyWordValues[0], out uint StateBParsed))
                                    {
                                        StateB = StateBParsed;
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
                    MKFunctions.CreateStartMarker(Music.StartMarkers, Position, MarkerType, 0, LoopStart, LoopMarkerCount, MarkerPos, StateA, StateB);
                    CurrentIndex++;
                }
            }
        }

        private void ReadMusicMarkersBlock(string[] FileLines, int CurrentIndex, EXMusic Music)
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
                            switch (CurrentKeyWord)
                            {
                                case "NAME":
                                    if (int.TryParse(KeyWordValues[0], out int NameParsed))
                                    {
                                        Name = NameParsed;
                                    }
                                    break;
                                case "POSITION":
                                    if (uint.TryParse(KeyWordValues[0], out uint PositionParsed))
                                    {
                                        Position = PositionParsed;
                                    }
                                    break;
                                case "TYPE":
                                    if (uint.TryParse(KeyWordValues[0], out uint MarkerTypeParsed))
                                    {
                                        MarkerType = MarkerTypeParsed;
                                    }
                                    break;
                                case "MARKERCOUNT":
                                    if (uint.TryParse(KeyWordValues[0], out uint MarkerCountParsed))
                                    {
                                        MarkerCount = MarkerCountParsed;
                                    }
                                    break;
                                case "LOOPSTART":
                                    if (uint.TryParse(KeyWordValues[0], out uint LoopStartParsed))
                                    {
                                        LoopStart = LoopStartParsed;
                                    }
                                    break;
                                case "LOOPMARKERCOUNT":
                                    if (uint.TryParse(KeyWordValues[0], out uint LoopMarkerCountParsed))
                                    {
                                        LoopMarkerCount = LoopMarkerCountParsed;
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
                    MKFunctions.CreateMarker(Music.Markers, Name, Position, MarkerType, 0, MarkerCount, LoopStart, LoopMarkerCount);
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
