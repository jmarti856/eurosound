using EngineXImaAdpcm;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundInterchangeFile
{
    internal class ESIF_Loader
    {
        //Key = FILEREF, Value = MD5HASH
        private AudioFunctions AudioLibrary = new AudioFunctions();
        private MarkersFunctions MarkerFunctionsClass = new MarkersFunctions();
        private Dictionary<int, string> AudiosAssocTable = new Dictionary<int, string>();
        private Regex RemoveCharactersFromPathString = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
        private List<string> ImportResults = new List<string>();

        //*===============================================================================================
        //* SFX Soundbank
        //*===============================================================================================
        internal List<string> LoadSFX_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string[] FileLines = File.ReadAllLines(FilePath);
            string[] KeyWordValues = null;
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESIFFile"));

            //Check File
            if (FileLines[0].Equals("*EUROSOUND_INTERCHANGE_FILE V1.0"))
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
                        //Check for project settings block
                        if (CurrentKeyWord.Equals("PROJECTSETTINGS"))
                        {
                            i++;
                            ReadProjectSettingsBlock(FileLines, i, FileProperties);
                        }

                        //Check for audio data block
                        if (CurrentKeyWord.Equals("AUDIODATA"))
                        {
                            i++;
                            ReadAudioDataBlock(FileLines, i, CurrentKeyWord, KeyWordValues, AudiosList, TreeViewControl);
                        }

                        //SFX SOUND BLOCK
                        if (CurrentKeyWord.Equals("SFXSOUND"))
                        {
                            i++;
                            ReadSFXSoundBlock(FileLines, i, CurrentKeyWord, KeyWordValues, FileProperties, TreeViewControl, SoundsList);
                        }
                    }
                }
                AudiosAssocTable.Clear();
            }
            else
            {
                ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }

        private void ReadAudioDataBlock(string[] FileLines, int CurrentIndex, string CurrentKeyWord, string[] KeyWordValues, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string MD5AudioFilehash = string.Empty, AudioPath = string.Empty, NodeName;
            ushort AudioFlags = 0;
            uint AudioPSI = 0, LoopOffset = 0;
            int FileRef;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("AUDIO"))
                {
                    KeyWordValues = FileLines[CurrentIndex].Trim().Split(' ');
                    if (KeyWordValues.Length > 1)
                    {
                        EXAudio NewAudio = null;
                        FileRef = int.Parse(KeyWordValues[1]);

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                            KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                            if (KeyWordValues.Length > 0)
                            {
                                if (CurrentKeyWord.Equals("PATH"))
                                {
                                    AudioPath = RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                                    if (File.Exists(AudioPath))
                                    {
                                        if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.SoundbankChannels, GlobalPreferences.SoundbankFrequency))
                                        {
                                            MD5AudioFilehash = GenericFunctions.CalculateMD5(AudioPath);
                                            NewAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join("", "1", "The file: ", AudioPath, " does not have a valid value and will not be loaded"));
                                        }
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "1", "The file: ", AudioPath, " was not found"));
                                    }
                                }
                                if (CurrentKeyWord.Equals("NODECOLOR"))
                                {
                                    int[] RGBColors = KeyWordValues[0].Split(' ').Select(n => int.Parse(n)).ToArray();
                                    if (RGBColors.Length == 3)
                                    {
                                        DefaultNodeColor = Color.FromArgb(1, RGBColors[0], RGBColors[1], RGBColors[2]);
                                    }
                                }
                                if (CurrentKeyWord.Equals("FLAGS"))
                                {
                                    AudioFlags = ushort.Parse(KeyWordValues[0]);
                                }
                                if (CurrentKeyWord.Equals("LOOPOFFSET"))
                                {
                                    LoopOffset = uint.Parse(KeyWordValues[0]);
                                }
                                if (CurrentKeyWord.Equals("PSI"))
                                {
                                    AudioPSI = uint.Parse(KeyWordValues[0]);
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

                        //Add info to the association table
                        if (!AudiosAssocTable.ContainsKey(FileRef))
                        {
                            AudiosAssocTable.Add(FileRef, MD5AudioFilehash);
                        }
                        else
                        {
                            ImportResults.Add(string.Join("", "1", "The audio with the file ref: ", FileRef, " is duplicated"));
                        }

                        //Add audio to dictionary
                        if (NewAudio != null)
                        {
                            if (!AudiosList.ContainsKey(MD5AudioFilehash))
                            {
                                NewAudio.LoopOffset = LoopOffset / 2;
                                NewAudio.Flags = AudioFlags;
                                NewAudio.PSIsample = AudioPSI;

                                //Add Audio to dictionary
                                AudiosList.Add(MD5AudioFilehash, NewAudio);

                                //Add Node
                                NodeName = "AD_" + Path.GetFileNameWithoutExtension(AudioPath);
                                TreeNodeFunctions.TreeNodeAddNewNode("AudioData", MD5AudioFilehash, GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 7, 7, "Audio", false, false, false, DefaultNodeColor, TreeViewControl);
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "2", "The project also contains: ", NewAudio.LoadedFileName, " with the MD5 hash: ", MD5AudioFilehash));
                            }
                        }
                        else
                        {
                            ImportResults.Add(string.Join("", "1", "The audio with the file ref: ", FileRef, " is null and can't be loaded"));
                        }
                    }
                }
                CurrentIndex++;
            }
        }

        private void ReadSFXSoundBlock(string[] FileLines, int CurrentIndex, string CurrentKeyWord, string[] KeyWordValues, ProjectFile FileProperties, TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList)
        {
            string NodeName = string.Empty, FolderName = string.Empty, SampleName = string.Empty;
            bool NodeAddedInFolder;
            uint NewSoundKey = GenericFunctions.GetNewObjectID(FileProperties);
            short FileRef = 0;
            EXSound SFXSound = new EXSound();
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
            Color DefaultSampleNodeColor = Color.FromArgb(1, 0, 0, 0);

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
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
                    if (CurrentKeyWord.Equals("HASHCODE"))
                    {
                        SFXSound.Hashcode = Convert.ToUInt32(KeyWordValues[0], 16);
                    }
                    if (CurrentKeyWord.Equals("NODECOLOR"))
                    {
                        int[] RGBColors = KeyWordValues[0].Split(' ').Select(n => int.Parse(n)).ToArray();
                        if (RGBColors.Length == 3)
                        {
                            DefaultNodeColor = Color.FromArgb(1, RGBColors[0], RGBColors[1], RGBColors[2]);
                        }
                    }
                    if (CurrentKeyWord.Equals("PARAMETERS"))
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
                                    case "DUCKERLENGTH":
                                        SFXSound.DuckerLenght = short.Parse(KeyWordValues[0]);
                                        break;
                                    case "MINDELAY":
                                        SFXSound.MinDelay = short.Parse(KeyWordValues[0]);
                                        break;
                                    case "MAXDELAY":
                                        SFXSound.MaxDelay = short.Parse(KeyWordValues[0]);
                                        break;
                                    case "REVERBSEND":
                                        SFXSound.ReverbSend = sbyte.Parse(KeyWordValues[0]);
                                        break;
                                    case "TRACKINGTYPE":
                                        SFXSound.TrackingType = sbyte.Parse(KeyWordValues[0]);
                                        break;
                                    case "MAXVOICES":
                                        SFXSound.MaxVoices = sbyte.Parse(KeyWordValues[0]);
                                        break;
                                    case "PRIORITY":
                                        SFXSound.Priority = sbyte.Parse(KeyWordValues[0]);
                                        break;
                                    case "DUCKER":
                                        SFXSound.Ducker = sbyte.Parse(KeyWordValues[0]);
                                        break;
                                    case "MASTERVOLUME":
                                        SFXSound.MasterVolume = sbyte.Parse(KeyWordValues[0]);
                                        break;
                                    case "FLAGS":
                                        SFXSound.Flags = ushort.Parse(KeyWordValues[0]);
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

                        //Parameters from SFX Data
                        uint KeyToCheck = SFXSound.Hashcode - 0x1A000000;
                        float[] SFXValues = Hashcodes.SFX_Data.FirstOrDefault(x => x.Value[0] == KeyToCheck).Value;
                        if (SFXValues != null)
                        {
                            SFXSound.InnerRadiusReal = (short)SFXValues[1];
                            SFXSound.OuterRadiusReal = (short)SFXValues[2];
                        }

                        //Add Sound to dictionary
                        SoundsList.Add(NewSoundKey, SFXSound);
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
                //Check for sound samples
                if (CurrentKeyWord.Equals("SAMPLES"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                        if (CurrentKeyWord.Equals("SAMPLE"))
                        {
                            uint SampleID = GenericFunctions.GetNewObjectID(FileProperties);
                            EXSample NewSample = new EXSample();
                            SampleName = string.Empty;

                            CurrentIndex++;
                            while (!FileLines[CurrentIndex].Trim().Equals("}"))
                            {
                                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                                KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                                if (KeyWordValues.Length > 0)
                                {
                                    switch (CurrentKeyWord)
                                    {
                                        case "NODENAME":
                                            NewSample.Name = KeyWordValues[0];
                                            break;
                                        case "NODECOLOR":
                                            int[] RGBColors = KeyWordValues[0].Split(' ').Select(n => int.Parse(n)).ToArray();
                                            if (RGBColors.Length == 3)
                                            {
                                                NewSample.NodeColor = Color.FromArgb(1, RGBColors[0], RGBColors[1], RGBColors[2]);
                                            }
                                            break;
                                        case "PITCHOFFSET":
                                            NewSample.PitchOffset = short.Parse(KeyWordValues[0]);
                                            break;
                                        case "BASEVOLUME":
                                            NewSample.BaseVolume = sbyte.Parse(KeyWordValues[0]);
                                            break;
                                        case "PAN":
                                            NewSample.Pan = sbyte.Parse(KeyWordValues[0]);
                                            break;
                                        case "RANDOMPITCHOFFSET":
                                            NewSample.RandomPitchOffset = short.Parse(KeyWordValues[0]);
                                            break;
                                        case "RANDOMVOLUMEOFFSET":
                                            NewSample.RandomVolumeOffset = sbyte.Parse(KeyWordValues[0]);
                                            break;
                                        case "RANDOMPAN":
                                            NewSample.RandomPan = sbyte.Parse(KeyWordValues[0]);
                                            break;
                                        case "FILEREF":
                                            FileRef = short.Parse(KeyWordValues[0]);
                                            NewSample.FileRef = FileRef;
                                            if (FileRef < 0)
                                            {
                                                NewSample.IsStreamed = true;
                                            }
                                            else if (EXSoundbanksFunctions.SubSFXFlagChecked(SFXSound.Flags))
                                            {
                                                uint RefHashC = (uint)FileRef;
                                                NewSample.HashcodeSubSFX = 0x1A000000 | RefHashC;
                                                NewSample.ComboboxSelectedAudio = "<SUB SFX>";
                                            }
                                            else
                                            {
                                                if (AudiosAssocTable.ContainsKey(FileRef))
                                                {
                                                    NewSample.ComboboxSelectedAudio = AudiosAssocTable[FileRef];
                                                }
                                                else
                                                {
                                                    ImportResults.Add(string.Join("", "0", "The audio with the file ref: ", FileRef, " was not found"));
                                                }
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
                            SFXSound.Samples.Add(SampleID, NewSample);
                        }
                        CurrentIndex++;
                    }
                }
                CurrentIndex++;
            }

            //Add Sound Node
            if (FileRef >= 0)
            {
                NodeAddedInFolder = AddItemInCustomFolder(FolderName, NewSoundKey.ToString(), NodeName, "Sounds", "Sound", TreeViewControl, DefaultNodeColor);
                if (!NodeAddedInFolder)
                {
                    TreeNodeFunctions.TreeNodeAddNewNode("Sounds", NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Sound", false, false, false, DefaultNodeColor, TreeViewControl);
                }
            }
            else
            {
                NodeAddedInFolder = AddItemInCustomFolder(FolderName, NewSoundKey.ToString(), NodeName, "StreamedSounds", "Sound", TreeViewControl, DefaultNodeColor);
                if (!NodeAddedInFolder)
                {
                    TreeNodeFunctions.TreeNodeAddNewNode("StreamedSounds", NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Sound", false, false, false, DefaultNodeColor, TreeViewControl);
                }
            }

            //Add Sample Nodes
            foreach (KeyValuePair<uint, EXSample> Sample in SFXSound.Samples)
            {
                if (string.IsNullOrEmpty(Sample.Value.Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(NewSoundKey.ToString(), Sample.Key.ToString(), GenericFunctions.GetNextAvailableName("SMP_" + NodeName, TreeViewControl), 4, 4, "Sample", false, false, false, Sample.Value.NodeColor, TreeViewControl);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(NewSoundKey.ToString(), Sample.Key.ToString(), GenericFunctions.GetNextAvailableName(Sample.Value.Name, TreeViewControl), 4, 4, "Sample", false, false, false, Sample.Value.NodeColor, TreeViewControl);
                }
            }
        }

        //*===============================================================================================
        //* Stream Sound Soundbank
        //*===============================================================================================
        internal List<string> LoadStreamSoundBank_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl)
        {
            string[] lines = File.ReadAllLines(FilePath);
            string[] KeyWordValues = null;
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESIFFile"));

            //Check File
            if (lines[0].Equals("*EUROSOUND_INTERCHANGE_FILE V1.0"))
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    CurrentKeyWord = GetKeyWord(lines[i]);
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
                            ReadProjectSettingsBlock(lines, i, FileProperties);
                        }

                        //Check for project settings block
                        if (CurrentKeyWord.Equals("STREAMSOUND"))
                        {
                            i++;
                            ReadStreamSoundsBlock(lines, i, CurrentKeyWord, KeyWordValues, SoundsList, FileProperties, TreeViewControl);
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

        private void ReadStreamSoundsBlock(string[] FileLines, int CurrentIndex, string CurrentKeyWord, string[] KeyWordValues, Dictionary<uint, EXSoundStream> SoundsList, ProjectFile FileProperties, TreeView TreeViewControl)
        {
            string NodeName = string.Empty, FolderName = string.Empty, AudioPath;
            uint ObjectID;
            bool NodeAddedInFolder;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
            EXSoundStream NewSSound = new EXSoundStream();

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
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
                        AudioPath = RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                        if (File.Exists(AudioPath))
                        {
                            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.StreambankChannels, GlobalPreferences.StreambankFrequency))
                            {
                                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                                {
                                    NewSSound.Channels = (byte)AudioReader.WaveFormat.Channels;
                                    NewSSound.Frequency = (uint)AudioReader.WaveFormat.SampleRate;
                                    NewSSound.RealSize = (uint)new FileInfo(AudioPath).Length;
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
                        NewSSound.BaseVolume = uint.Parse(KeyWordValues[0]);
                    }
                    if (CurrentKeyWord.Equals("STARTMARKERS"))
                    {
                        uint Position = 0, MarkerType = 0, MarkerPos = 0, LoopStart = 0, LoopMarkerCount = 0, StateA = 0, StateB = 0;

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                            if (CurrentKeyWord.Equals("STARTMARKER"))
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
                                            case "MUSICMARKERTYPE":
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
                                            case "MUSICMARKERTYPE":
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

            NodeAddedInFolder = AddItemInCustomFolder(FolderName, ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), "Sounds", "Sound", TreeViewControl, DefaultNodeColor);
            if (!NodeAddedInFolder)
            {
                TreeNodeFunctions.TreeNodeAddNewNode("Sounds", ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Sound", false, false, false, DefaultNodeColor, TreeViewControl);
            }
        }

        //*===============================================================================================
        //* Music Banks
        //*===============================================================================================
        internal List<string> LoadMusicBank_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXMusic> MusicsList, TreeView TreeViewControl)
        {
            string[] lines = File.ReadAllLines(FilePath);
            string[] KeyWordValues = null;
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESIFFile"));

            //Check File
            if (lines[0].Equals("*EUROSOUND_INTERCHANGE_FILE V1.0"))
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    CurrentKeyWord = GetKeyWord(lines[i]);
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
                            ReadProjectSettingsBlock(lines, i, FileProperties);
                        }

                        //Check for project settings block
                        if (CurrentKeyWord.Equals("MUSIC"))
                        {
                            i++;
                            ReadMusicBankBlock(lines, i, CurrentKeyWord, KeyWordValues, MusicsList, FileProperties, TreeViewControl);
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

        private void ReadMusicBankBlock(string[] FileLines, int CurrentIndex, string CurrentKeyWord, string[] KeyWordValues, Dictionary<uint, EXMusic> MusicsList, ProjectFile FileProperties, TreeView TreeViewControl)
        {
            string NodeName = string.Empty, FolderName = string.Empty, AudioPath;
            uint ObjectID;
            bool NodeAddedInFolder;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
            EXMusic NewMusicBank = new EXMusic();

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
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
                        AudioPath = RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                        if (File.Exists(AudioPath))
                        {
                            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.MusicbankChannels, GlobalPreferences.MusicbankFrequency))
                            {
                                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                                {
                                    NewMusicBank.Channels_LeftChannel = (byte)AudioReader.WaveFormat.Channels;
                                    NewMusicBank.Frequency_LeftChannel = (uint)AudioReader.WaveFormat.SampleRate;
                                    NewMusicBank.RealSize_LeftChannel = (uint)new FileInfo(AudioPath).Length;
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
                                    NewMusicBank.IMA_ADPCM_DATA_LeftChannel = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData, NewMusicBank.PCM_Data_LeftChannel.Length / 2);
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
                        AudioPath = RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
                        if (File.Exists(AudioPath))
                        {
                            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.MusicbankChannels, GlobalPreferences.MusicbankFrequency))
                            {
                                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                                {
                                    NewMusicBank.Channels_RightChannel = (byte)AudioReader.WaveFormat.Channels;
                                    NewMusicBank.Frequency_RightChannel = (uint)AudioReader.WaveFormat.SampleRate;
                                    NewMusicBank.RealSize_RightChannel = (uint)new FileInfo(AudioPath).Length;
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
                                    NewMusicBank.IMA_ADPCM_DATA_RightChannel = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData, NewMusicBank.PCM_Data_RightChannel.Length / 2);
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
                        NewMusicBank.BaseVolume = uint.Parse(KeyWordValues[0]);
                    }
                    if (CurrentKeyWord.Equals("STARTMARKERS"))
                    {
                        uint Position = 0, MarkerType = 0, LoopStart = 0, LoopMarkerCount = 0, MarkerPos = 0, StateA = 0, StateB = 0;

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                            if (CurrentKeyWord.Equals("STARTMARKER"))
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
                                            case "MUSICMARKERTYPE":
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
                                            case "MUSICMARKERTYPE":
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

            NodeAddedInFolder = AddItemInCustomFolder(FolderName, ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), "Musics", "Music", TreeViewControl, DefaultNodeColor);
            if (!NodeAddedInFolder)
            {
                TreeNodeFunctions.TreeNodeAddNewNode("Musics", ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Music", false, false, false, DefaultNodeColor, TreeViewControl);
            }
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        private bool AddItemInCustomFolder(string ParentFolderName, string NewSoundKey, string NodeName, string RootFolderName, string TypeOfObject, TreeView TreeViewControl, Color DefaultNodeColor)
        {
            bool NodeAddedInFolder = false;

            if (!string.IsNullOrEmpty(ParentFolderName))
            {
                //Check the folder exists
                TreeNode[] Folders = TreeViewControl.Nodes.Find(ParentFolderName, true);
                if (Folders.Length > 0)
                {
                    //Check that the folder is in the correct section (Sounds, Audios, StreamSounds)
                    if (TreeNodeFunctions.FindRootNode(Folders[0]).Name.Equals(RootFolderName))
                    {
                        TreeNodeFunctions.TreeNodeAddNewNode(ParentFolderName, NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, TypeOfObject, false, false, false, DefaultNodeColor, TreeViewControl);
                        NodeAddedInFolder = true;
                    }
                }
                else
                {
                    ImportResults.Add(string.Join("", "1", "The folder ", ParentFolderName, " could not been found"));
                }
            }
            return NodeAddedInFolder;
        }

        private void ReadProjectSettingsBlock(string[] FileLines, int CurrentIndex, ProjectFile FileProperties)
        {
            string CurrentKeyWord;
            string[] KeyWordValues;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                if (KeyWordValues.Length > 0)
                {
                    if (CurrentKeyWord.Equals("FILENAME"))
                    {
                        FileProperties.FileName = KeyWordValues[0];
                        GenericFunctions.SetCurrentFileLabel(FileProperties.FileName, "File");
                    }
                    if (CurrentKeyWord.Equals("HASHCODE"))
                    {
                        FileProperties.Hashcode = Convert.ToUInt32(KeyWordValues[0], 16);
                        GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, FileProperties.Hashcode), "Hashcode");
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
            string[] Values = Regex.Matches(FileLine, @"(?<=[*])\w+[\s-[\r\n]]*""?(.*?)""?\r?$").Cast<Match>().Select(x => x.Groups[1].Value).ToArray();
            return Values;
        }
    }
}
