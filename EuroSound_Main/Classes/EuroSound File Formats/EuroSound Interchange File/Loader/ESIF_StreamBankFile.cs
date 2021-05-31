using EngineXImaAdpcm;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.EuroSoundInterchangeFile.Functions;
using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.StreamSounds;
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
    internal class ESIF_StreamBankFile
    {
        private AudioFunctions AudioLibrary = new AudioFunctions();
        private MarkersFunctions MarkerFunctionsClass = new MarkersFunctions();
        private ESIF_LoaderFunctions LoaderFunctions = new ESIF_LoaderFunctions();
        private List<string> ImportResults = new List<string>();


        //*===============================================================================================
        //* Stream Sound Soundbank
        //*===============================================================================================
        internal IList<string> LoadStreamSoundBank_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl)
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
                            if (CurrentKeyWord.Equals("STREAMSOUND"))
                            {
                                i++;
                                ReadStreamSoundsBlock(FileLines, i, SoundsList, FileProperties, TreeViewControl);
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

        private void ReadStreamSoundsBlock(string[] FileLines, int CurrentIndex, Dictionary<uint, EXSoundStream> SoundsList, ProjectFile FileProperties, TreeView TreeViewControl)
        {
            string NodeName = string.Empty, FolderName = string.Empty, AudioPath;
            uint ObjectID;
            bool NodeAddedInFolder;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
            EXSoundStream NewSSound = new EXSoundStream();

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
                    if (CurrentKeyWord.Equals("FILEPATH"))
                    {
                        AudioPath = LoaderFunctions.RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                        if (File.Exists(AudioPath))
                        {
                            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.StreambankChannels, GlobalPreferences.StreambankFrequency))
                            {
                                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                                {
                                    NewSSound.Channels = (byte)AudioReader.WaveFormat.Channels;
                                    NewSSound.Frequency = (uint)AudioReader.WaveFormat.SampleRate;
                                    NewSSound.Bits = (uint)AudioReader.WaveFormat.BitsPerSample;
                                    NewSSound.Encoding = AudioReader.WaveFormat.Encoding.ToString();
                                    NewSSound.Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);
                                    NewSSound.WAVFileName = Path.GetFileName(AudioPath);
                                    NewSSound.WAVFileMD5 = GenericFunctions.CalculateMD5(AudioPath);

                                    //Get PCM Data
                                    NewSSound.PCM_Data = new byte[AudioReader.Length];
                                    AudioReader.Read(NewSSound.PCM_Data, 0, (int)AudioReader.Length);
                                    AudioReader.Close();

                                    //Get IMA ADPCM Data
                                    ImaADPCM_Functions ImaADPCM = new ImaADPCM_Functions();
                                    short[] ShortArrayPCMData = AudioLibrary.ConvertPCMDataToShortArray(NewSSound.PCM_Data);
                                    NewSSound.IMA_ADPCM_DATA = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData, NewSSound.PCM_Data.Length / 2);
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
                            NewSSound.BaseVolume = Volume;
                        }
                        else
                        {
                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                        }
                    }
                    if (CurrentKeyWord.Equals("STARTMARKERS"))
                    {
                        uint Position = 0, MarkerType = 0, MarkerPos = 0, LoopStart = 0, LoopMarkerCount = 0, StateA = 0, StateB = 0;

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
                                        uint ValueNumber;

                                        switch (CurrentKeyWord)
                                        {
                                            case "POSITION":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    Position = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MUSICMARKERTYPE":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    MarkerType = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPSTART":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    LoopStart = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPMARKERCOUNT":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    LoopMarkerCount = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MARKERPOS":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    MarkerPos = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "STATEA":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    StateA = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "STATEB":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    StateB = ValueNumber;
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
                                MarkerFunctionsClass.CreateStartMarker(NewSSound.StartMarkers, Position, MarkerType, 2, LoopStart, LoopMarkerCount, MarkerPos, StateA, StateB);
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
                                        uint ValueNumber;

                                        switch (CurrentKeyWord)
                                        {
                                            case "NAME":
                                                int NameValue;

                                                if (int.TryParse(KeyWordValues[0], out NameValue))
                                                {
                                                    Name = NameValue;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid int value type"));
                                                }
                                                break;
                                            case "POSITION":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    Position = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MUSICMARKERTYPE":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    MarkerType = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "MARKERCOUNT":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    MarkerCount = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPSTART":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    LoopStart = ValueNumber;
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                                }
                                                break;
                                            case "LOOPMARKERCOUNT":
                                                if (uint.TryParse(KeyWordValues[0], out ValueNumber))
                                                {
                                                    LoopMarkerCount = ValueNumber;
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
                                MarkerFunctionsClass.CreateMarker(NewSSound.Markers, Name, Position, MarkerType, 2, MarkerCount, LoopStart, LoopMarkerCount);
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
            SoundsList.Add(ObjectID, NewSSound);

            NodeAddedInFolder = LoaderFunctions.AddItemInCustomFolder(FolderName, ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), "Sounds", (byte)Enumerations.TreeNodeType.Sound, TreeViewControl, DefaultNodeColor, ImportResults);
            if (!NodeAddedInFolder)
            {
                TreeNodeFunctions.TreeNodeAddNewNode("Sounds", ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, (byte)Enumerations.TreeNodeType.Sound, false, false, false, DefaultNodeColor, TreeViewControl);
            }
        }
    }
}
