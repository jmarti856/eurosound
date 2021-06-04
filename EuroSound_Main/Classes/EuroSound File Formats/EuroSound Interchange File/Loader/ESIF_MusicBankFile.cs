using EngineXImaAdpcm;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.EuroSoundInterchangeFile.Functions;
using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.Musics;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundInterchangeFile
{
    internal class ESIF_MusicBankFile
    {
        private AudioFunctions AudioLibrary = new AudioFunctions();
        private MarkersFunctions MarkerFunctionsClass = new MarkersFunctions();
        private List<string> ImportResults = new List<string>();
        private ESIF_LoaderFunctions LoaderFunctions = new ESIF_LoaderFunctions();

        //*===============================================================================================
        //* Music Banks
        //*===============================================================================================
        internal IList<string> LoadMusicBank_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXMusic> MusicsList, TreeView TreeViewControl)
        {
            string[] FileLines = File.ReadAllLines(FilePath);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESIFFile"));

            //Check file is not empty
            if (FileLines.Length > 0)
            {
                //Check file is correct
                string CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[0]);
                if (CurrentKeyWord.Equals("EUROSOUND"))
                {
                    for (int i = 1; i < FileLines.Length; i++)
                    {
                        CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[i]);
                        if (string.IsNullOrEmpty(CurrentKeyWord) || CurrentKeyWord.Equals("COMMENT"))
                        {
                            continue;
                        }
                        else
                        {
                            //Check for project settings block
                            if (CurrentKeyWord.Equals("PROJECTSETTINGS"))
                            {
                                i++;
                                LoaderFunctions.ReadProjectSettingsBlock(FileLines, i, FileProperties, ImportResults);
                            }

                            //Check for project settings block
                            if (CurrentKeyWord.Equals("MUSIC"))
                            {
                                i++;
                                ReadMusicBankBlock(FileLines, i, MusicsList, FileProperties, TreeViewControl);
                            }
                        }
                    }
                }
                else
                {
                    ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
                }
            }

            //Update project status variable
            FileProperties.FileHasBeenModified = true;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }

        private void ReadMusicBankBlock(string[] FileLines, int CurrentIndex, Dictionary<uint, EXMusic> MusicsList, ProjectFile FileProperties, TreeView TreeViewControl)
        {
            string NodeName = string.Empty, FolderName = string.Empty, AudioPath;
            uint ObjectID;
            bool NodeAddedInFolder;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
            EXMusic NewMusicBank = new EXMusic();

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                string CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                string[] KeyWordValues = LoaderFunctions.GetKeyValues(FileLines[CurrentIndex]);
                if (KeyWordValues.Length > 0)
                {
                    if (CurrentKeyWord.Equals("NODENAME"))
                    {
                        NodeName = KeyWordValues[0];
                    }
                    if (CurrentKeyWord.Equals("FOLDERNAME"))
                    {
                        FolderName = KeyWordValues[0];
                    }
                    if (CurrentKeyWord.Equals("NODECOLOR"))
                    {
                        int[] RGBColors = KeyWordValues[0].Split(' ').Select(n => int.Parse(n)).ToArray();
                        if (RGBColors.Length == 3)
                        {
                            DefaultNodeColor = Color.FromArgb(1, RGBColors[0], RGBColors[1], RGBColors[2]);
                        }
                    }
                    if (CurrentKeyWord.Equals("FILEPATHLEFT"))
                    {
                        AudioPath = LoaderFunctions.RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                        if (File.Exists(AudioPath))
                        {
                            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.MusicbankChannels, GlobalPreferences.MusicbankFrequency))
                            {
                                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                                {
                                    NewMusicBank.Channels_LeftChannel = (byte)AudioReader.WaveFormat.Channels;
                                    NewMusicBank.Frequency_LeftChannel = (uint)AudioReader.WaveFormat.SampleRate;
                                    NewMusicBank.Bits_LeftChannel = (uint)AudioReader.WaveFormat.BitsPerSample;
                                    NewMusicBank.Encoding_LeftChannel = AudioReader.WaveFormat.Encoding.ToString();
                                    NewMusicBank.Duration_LeftChannel = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);
                                    NewMusicBank.WAVFileName_LeftChannel = Path.GetFileName(AudioPath);
                                    NewMusicBank.WAVFileMD5_LeftChannel = GenericFunctions.CalculateMD5(AudioPath);

                                    //Get PCM Data
                                    NewMusicBank.PCM_Data_LeftChannel = new byte[AudioReader.Length];
                                    AudioReader.Read(NewMusicBank.PCM_Data_LeftChannel, 0, (int)AudioReader.Length);
                                    AudioReader.Close();

                                    //Get IMA ADPCM Data
                                    ImaADPCM_Functions ImaADPCM = new ImaADPCM_Functions();
                                    short[] ShortArrayPCMData = AudioLibrary.ConvertPCMDataToShortArray(NewMusicBank.PCM_Data_LeftChannel);
                                    NewMusicBank.IMA_ADPCM_DATA_LeftChannel = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData);
                                }
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *FILEPATH the specified file is not a valid audio file"));
                                break;
                            }
                        }
                        else
                        {
                            ImportResults.Add(string.Join("", "1", "The file: ", AudioPath, " could not be found"));
                        }
                    }
                    if (CurrentKeyWord.Equals("FILEPATHRIGHT"))
                    {
                        AudioPath = LoaderFunctions.RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                        if (File.Exists(AudioPath))
                        {
                            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.MusicbankChannels, GlobalPreferences.MusicbankFrequency))
                            {
                                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                                {
                                    NewMusicBank.Channels_RightChannel = (byte)AudioReader.WaveFormat.Channels;
                                    NewMusicBank.Frequency_RightChannel = (uint)AudioReader.WaveFormat.SampleRate;
                                    NewMusicBank.Bits_RightChannel = (uint)AudioReader.WaveFormat.BitsPerSample;
                                    NewMusicBank.Encoding_RightChannel = AudioReader.WaveFormat.Encoding.ToString();
                                    NewMusicBank.Duration_RightChannel = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);
                                    NewMusicBank.WAVFileName_RightChannel = Path.GetFileName(AudioPath);
                                    NewMusicBank.WAVFileMD5_RightChannel = GenericFunctions.CalculateMD5(AudioPath);

                                    //Get PCM Data
                                    NewMusicBank.PCM_Data_RightChannel = new byte[AudioReader.Length];
                                    AudioReader.Read(NewMusicBank.PCM_Data_RightChannel, 0, (int)AudioReader.Length);
                                    AudioReader.Close();

                                    //Get IMA ADPCM Data
                                    ImaADPCM_Functions ImaADPCM = new ImaADPCM_Functions();
                                    short[] ShortArrayPCMData = AudioLibrary.ConvertPCMDataToShortArray(NewMusicBank.PCM_Data_RightChannel);
                                    NewMusicBank.IMA_ADPCM_DATA_RightChannel = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData);
                                }
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *FILEPATH the specified file is not a valid audio file"));
                                break;
                            }
                        }
                        else
                        {
                            ImportResults.Add(string.Join("", "1", "The file: ", AudioPath, " could not be found"));
                        }
                    }
                    if (CurrentKeyWord.Equals("BASEVOLUME"))
                    {
                        if (uint.TryParse(KeyWordValues[0], out uint Volume))
                        {
                            NewMusicBank.BaseVolume = Volume;
                        }
                        else
                        {
                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                        }
                    }
                    if (CurrentKeyWord.Equals("STARTMARKERS"))
                    {
                        uint Position = 0, MarkerType = 0, LoopStart = 0, LoopMarkerCount = 0, MarkerPos = 0, StateA = 0, StateB = 0;

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                            if (CurrentKeyWord.Equals("STARTMARKER"))
                            {
                                CurrentIndex++;
                                while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                                {
                                    CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                                    KeyWordValues = LoaderFunctions.GetKeyValues(FileLines[CurrentIndex]);
                                    if (KeyWordValues.Length > 0)
                                    {
                                        switch (CurrentKeyWord)
                                        {
                                            case "POSITION":
                                                if (uint.TryParse(KeyWordValues[0], out uint PositionParsed))
                                                {
                                                    Position = PositionParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MUSICMARKERTYPE":
                                                if (uint.TryParse(KeyWordValues[0], out uint MarkerTypeParsed))
                                                {
                                                    MarkerType = MarkerTypeParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPSTART":
                                                if (uint.TryParse(KeyWordValues[0], out uint LoopStartParsed))
                                                {
                                                    LoopStart = LoopStartParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPMARKERCOUNT":
                                                if (uint.TryParse(KeyWordValues[0], out uint LoopMarkerCountParsed))
                                                {
                                                    LoopMarkerCount = LoopMarkerCountParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MARKERPOS":
                                                if (uint.TryParse(KeyWordValues[0], out uint MarkerPosParsed))
                                                {
                                                    MarkerPos = MarkerPosParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "STATEA":
                                                if (uint.TryParse(KeyWordValues[0], out uint StateAParsed))
                                                {
                                                    StateA = StateAParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "STATEB":
                                                if (uint.TryParse(KeyWordValues[0], out uint StateBParsed))
                                                {
                                                    StateB = StateBParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
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
                                MarkerFunctionsClass.CreateStartMarker(NewMusicBank.StartMarkers, Position, MarkerType, 0, LoopStart, LoopMarkerCount, MarkerPos, StateA, StateB);
                                CurrentIndex++;
                            }
                        }
                    }
                    if (CurrentKeyWord.Equals("MARKERS"))
                    {
                        int Name = 0;
                        uint Position = 0, MarkerType = 0, MarkerCount = 0, LoopStart = 0, LoopMarkerCount = 0;

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                            if (CurrentKeyWord.Equals("MARKER"))
                            {
                                CurrentIndex++;
                                while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                                {
                                    CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                                    KeyWordValues = LoaderFunctions.GetKeyValues(FileLines[CurrentIndex]);
                                    if (KeyWordValues.Length > 0)
                                    {
                                        switch (CurrentKeyWord)
                                        {
                                            case "NAME":
                                                if (int.TryParse(KeyWordValues[0], out int NameParsed))
                                                {
                                                    Name = NameParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid int value type"));
                                                }
                                                break;
                                            case "POSITION":
                                                if (uint.TryParse(KeyWordValues[0], out uint PositionParsed))
                                                {
                                                    Position = PositionParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MUSICMARKERTYPE":
                                                if (uint.TryParse(KeyWordValues[0], out uint MarkerTypeParsed))
                                                {
                                                    MarkerType = MarkerTypeParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MARKERCOUNT":
                                                if (uint.TryParse(KeyWordValues[0], out uint MarkerCountParsed))
                                                {
                                                    MarkerCount = MarkerCountParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPSTART":
                                                if (uint.TryParse(KeyWordValues[0], out uint LoopStartParsed))
                                                {
                                                    LoopStart = LoopStartParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPMARKERCOUNT":
                                                if (uint.TryParse(KeyWordValues[0], out uint LoopMarkerCountParsed))
                                                {
                                                    LoopMarkerCount = LoopMarkerCountParsed;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
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
                                MarkerFunctionsClass.CreateMarker(NewMusicBank.Markers, Name, Position, MarkerType, 0, MarkerCount, LoopStart, LoopMarkerCount);
                                CurrentIndex++;
                            }
                        }
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

            //Add Object to list
            ObjectID = GenericFunctions.GetNewObjectID(FileProperties);
            MusicsList.Add(ObjectID, NewMusicBank);

            NodeAddedInFolder = LoaderFunctions.AddItemInCustomFolder(FolderName, ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), "Musics", (byte)Enumerations.TreeNodeType.Music, TreeViewControl, DefaultNodeColor, ImportResults);
            if (!NodeAddedInFolder)
            {
                TreeNodeFunctions.TreeNodeAddNewNode("Musics", ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, (byte)Enumerations.TreeNodeType.Music, false, false, false, DefaultNodeColor, TreeViewControl);
            }
        }
    }
}
