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
        internal IList<string> LoadSFX_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string[] FileLines = File.ReadAllLines(FilePath);
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESIFFile"));

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
                                ReadAudioDataBlock(FileLines, i, AudiosList, TreeViewControl);
                            }

                            //SFX SOUND BLOCK
                            if (CurrentKeyWord.Equals("SFXSOUND"))
                            {
                                i++;
                                ReadSFXSoundBlock(FileLines, i, FileProperties, TreeViewControl, SoundsList);
                            }
                        }
                    }
                    AudiosAssocTable.Clear();
                }
                else
                {
                    ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
                }
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }

        private void ReadAudioDataBlock(string[] FileLines, int CurrentIndex, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string MD5AudioFilehash = string.Empty, AudioPath = string.Empty, NodeName;
            ushort AudioFlags = 0;
            uint AudioPSI = 0, LoopOffset = 0;
            int FileRef;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                string CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("AUDIO"))
                {
                    string[] KeyWordValues = FileLines[CurrentIndex].Trim().Split(' ');
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

                                    if (ushort.TryParse(KeyWordValues[0], out ushort Flags))
                                    {
                                        AudioFlags = Flags;
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid ushort value type"));
                                    }
                                }
                                if (CurrentKeyWord.Equals("LOOPOFFSET"))
                                {

                                    if (uint.TryParse(KeyWordValues[0], out uint LoopOffst))
                                    {
                                        LoopOffset = LoopOffst;
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                    }
                                }
                                if (CurrentKeyWord.Equals("PSI"))
                                {
                                    if (uint.TryParse(KeyWordValues[0], out uint PSIValue))
                                    {
                                        AudioPSI = PSIValue;
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
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

        private void ReadSFXSoundBlock(string[] FileLines, int CurrentIndex, ProjectFile FileProperties, TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList)
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
                string CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                string[] KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
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
                                        short DuckLength;

                                        if (short.TryParse(KeyWordValues[0], out DuckLength))
                                        {
                                            SFXSound.DuckerLenght = DuckLength;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
                                        }
                                        break;
                                    case "MINDELAY":
                                        short MinDel;

                                        if (short.TryParse(KeyWordValues[0], out MinDel))
                                        {
                                            SFXSound.MinDelay = MinDel;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
                                        }
                                        break;
                                    case "MAXDELAY":
                                        short MaxDel;

                                        if (short.TryParse(KeyWordValues[0], out MaxDel))
                                        {
                                            SFXSound.MaxDelay = MaxDel;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
                                        }
                                        break;
                                    case "REVERBSEND":
                                        sbyte RevSend;

                                        if (sbyte.TryParse(KeyWordValues[0], out RevSend))
                                        {
                                            SFXSound.ReverbSend = RevSend;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                        }
                                        break;
                                    case "TRACKINGTYPE":
                                        sbyte TrackType;

                                        if (sbyte.TryParse(KeyWordValues[0], out TrackType))
                                        {
                                            SFXSound.TrackingType = TrackType;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                        }
                                        break;
                                    case "MAXVOICES":
                                        sbyte MaxV;

                                        if (sbyte.TryParse(KeyWordValues[0], out MaxV))
                                        {
                                            SFXSound.MaxVoices = MaxV;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                        }
                                        break;
                                    case "PRIORITY":
                                        sbyte Prio;

                                        if (sbyte.TryParse(KeyWordValues[0], out Prio))
                                        {
                                            SFXSound.Priority = Prio;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                        }
                                        break;
                                    case "DUCKER":
                                        sbyte Duck;

                                        if (sbyte.TryParse(KeyWordValues[0], out Duck))
                                        {
                                            SFXSound.Ducker = Duck;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                        }
                                        break;
                                    case "MASTERVOLUME":
                                        sbyte MasterV;

                                        if (sbyte.TryParse(KeyWordValues[0], out MasterV))
                                        {
                                            SFXSound.MasterVolume = MasterV;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                        }
                                        break;
                                    case "FLAGS":
                                        ushort FlagsV;

                                        if (ushort.TryParse(KeyWordValues[0], out FlagsV))
                                        {
                                            SFXSound.Flags = FlagsV;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid ushort value type"));
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

                        //Parameters from SFX Data
                        uint KeyToCheck = SFXSound.Hashcode & 0x00ffffff; //Apply bytes mask, example: 0x1A00005C -> 0x0000005C
                        float[] SFXValues = GenericFunctions.GetSoundData(KeyToCheck);
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
                                            short PitchOff;

                                            if (short.TryParse(KeyWordValues[0], out PitchOff))
                                            {
                                                NewSample.PitchOffset = PitchOff;
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
                                            }
                                            break;
                                        case "BASEVOLUME":
                                            sbyte BasVolume;

                                            if (sbyte.TryParse(KeyWordValues[0], out BasVolume))
                                            {
                                                NewSample.BaseVolume = BasVolume;
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                            }
                                            break;
                                        case "PAN":
                                            sbyte PanValue;

                                            if (sbyte.TryParse(KeyWordValues[0], out PanValue))
                                            {
                                                NewSample.Pan = PanValue;
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                            }
                                            break;
                                        case "RANDOMPITCHOFFSET":
                                            short RndmPitchOff;

                                            if (short.TryParse(KeyWordValues[0], out RndmPitchOff))
                                            {
                                                NewSample.RandomPitchOffset = RndmPitchOff;
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
                                            }
                                            break;
                                        case "RANDOMVOLUMEOFFSET":
                                            sbyte RndmVolOff;

                                            if (sbyte.TryParse(KeyWordValues[0], out RndmVolOff))
                                            {
                                                NewSample.RandomVolumeOffset = RndmVolOff;
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                            }
                                            break;
                                        case "RANDOMPAN":
                                            sbyte RndPan;

                                            if (sbyte.TryParse(KeyWordValues[0], out RndPan))
                                            {
                                                NewSample.RandomPan = RndPan;
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid sbyte value type"));
                                            }
                                            break;
                                        case "FILEREF":
                                            short FileRefValue;

                                            if (short.TryParse(KeyWordValues[0], out FileRefValue))
                                            {
                                                FileRef = FileRefValue;
                                                NewSample.FileRef = FileRef;
                                                if (FileRef < 0)
                                                {
                                                    NewSample.IsStreamed = true;
                                                }
                                                else if (EXSoundbanksFunctions.SubSFXFlagChecked(SFXSound.Flags))
                                                {
                                                    uint RefHashC = (uint)FileRef;
                                                    NewSample.HashcodeSubSFX = GlobalPreferences.SfxPrefix | RefHashC;
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
                                            }
                                            else
                                            {
                                                ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
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
        internal IList<string> LoadStreamSoundBank_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl)
        {
            string[] FileLines = File.ReadAllLines(FilePath);
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESIFFile"));

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
                            //Check for project settings block
                            if (CurrentKeyWord.Equals("PROJECTSETTINGS"))
                            {
                                i++;
                                ReadProjectSettingsBlock(FileLines, i, FileProperties);
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
                string CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                string[] KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
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

            NodeAddedInFolder = AddItemInCustomFolder(FolderName, ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), "Sounds", "Sound", TreeViewControl, DefaultNodeColor);
            if (!NodeAddedInFolder)
            {
                TreeNodeFunctions.TreeNodeAddNewNode("Sounds", ObjectID.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Sound", false, false, false, DefaultNodeColor, TreeViewControl);
            }
        }

        //*===============================================================================================
        //* Music Banks
        //*===============================================================================================
        internal IList<string> LoadMusicBank_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXMusic> MusicsList, TreeView TreeViewControl)
        {
            string[] FileLines = File.ReadAllLines(FilePath);
            string CurrentKeyWord;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESIFFile"));

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
                            //Check for project settings block
                            if (CurrentKeyWord.Equals("PROJECTSETTINGS"))
                            {
                                i++;
                                ReadProjectSettingsBlock(FileLines, i, FileProperties);
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
                string CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                string[] KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
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
                TreeNode Folders = TreeNodeFunctions.SearchNodeRecursiveByText(TreeViewControl.Nodes, ParentFolderName, TreeViewControl, false);
                if (Folders != null)
                {
                    //Check that the folder is in the correct section (Sounds, Audios, StreamSounds)
                    if (TreeNodeFunctions.FindRootNode(Folders).Name.Equals(RootFolderName))
                    {
                        TreeNodeFunctions.TreeNodeAddNewNode(Folders.Name, NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, TypeOfObject, false, false, false, DefaultNodeColor, TreeViewControl);
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
                        GenericFunctions.SetCurrentFileLabel(FileProperties.FileName, "SBPanel_File");
                    }
                    if (CurrentKeyWord.Equals("HASHCODE"))
                    {
                        FileProperties.Hashcode = Convert.ToUInt32(KeyWordValues[0], 16);
                        GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, FileProperties.Hashcode), "SBPanel_Hashcode");
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
