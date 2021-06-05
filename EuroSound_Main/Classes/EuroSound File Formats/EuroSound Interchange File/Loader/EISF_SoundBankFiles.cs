using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.EuroSoundInterchangeFile.Functions;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundInterchangeFile
{
    internal class EISF_SoundBankFiles
    {
        private Dictionary<int, string> AudiosAssocTable = new Dictionary<int, string>();
        private List<string> ImportResults = new List<string>();
        private ESIF_LoaderFunctions LoaderFunctions = new ESIF_LoaderFunctions();

        //*===============================================================================================
        //* SFX Soundbank
        //*===============================================================================================
        internal IList<string> LoadSFX_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
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

            //Update project status variable
            FileProperties.FileHasBeenModified = true;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }

        private void ReadAudioDataBlock(string[] FileLines, int CurrentIndex, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                string CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                if (CurrentKeyWord.Equals("AUDIO"))
                {
                    Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
                    string MD5AudioFilehash = string.Empty, AudioPath = string.Empty, NodeName;

                    string[] KeyWordValues = FileLines[CurrentIndex].Trim().Split(' ');
                    if (KeyWordValues.Length > 1)
                    {
                        EXAudio NewAudio = null;
                        ushort AudioFlags = 0;
                        uint AudioPSI = 0, LoopOffset = 0, FrequencyPS2 = 0;
                        int FileRef = int.Parse(KeyWordValues[1]);

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                            KeyWordValues = LoaderFunctions.GetKeyValues(FileLines[CurrentIndex]);
                            if (KeyWordValues.Length > 0)
                            {
                                switch (CurrentKeyWord)
                                {
                                    case "PATH":
                                        AudioPath = LoaderFunctions.RemoveCharactersFromPathString.Replace(KeyWordValues[0], "");
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
                                        break;
                                    case "NODECOLOR":
                                        int[] RGBColors = KeyWordValues[0].Split(' ').Select(n => int.Parse(n)).ToArray();
                                        if (RGBColors.Length == 3)
                                        {
                                            DefaultNodeColor = Color.FromArgb(1, RGBColors[0], RGBColors[1], RGBColors[2]);
                                        }
                                        break;
                                    case "FLAGS":
                                        if (ushort.TryParse(KeyWordValues[0], out ushort Flags))
                                        {
                                            AudioFlags = Flags;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid ushort value type"));
                                        }
                                        break;
                                    case "LOOPOFFSET":
                                        if (uint.TryParse(KeyWordValues[0], out uint LoopOffst))
                                        {
                                            LoopOffset = LoopOffst;
                                        }
                                        else
                                        {
                                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid uint value type"));
                                        }
                                        break;
                                    case "PS2FREQUENCY":
                                        if (uint.TryParse(KeyWordValues[0], out uint PS2Frequ))
                                        {
                                            FrequencyPS2 = PS2Frequ;
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
                                NewAudio.FrequencyPS2 = FrequencyPS2;

                                //Add Audio to dictionary
                                AudiosList.Add(MD5AudioFilehash, NewAudio);

                                //Add Node
                                NodeName = Path.GetFileNameWithoutExtension(AudioPath);
                                TreeNodeFunctions.TreeNodeAddNewNode("AudioData", MD5AudioFilehash, GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 7, 7, (byte)Enumerations.TreeNodeType.Audio, false, false, false, DefaultNodeColor, TreeViewControl);
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
                    if (CurrentKeyWord.Equals("TARGETOUTPUT"))
                    {
                        byte targetOutput;
                        if (byte.TryParse(KeyWordValues[0], out targetOutput))
                        {
                            SFXSound.OutputTarget = targetOutput;
                        }
                        else
                        {
                            ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains a valid short value type"));
                        }
                    }
                    if (CurrentKeyWord.Equals("PARAMETERS"))
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
                                    case "DUCKERLENGTH":
                                        short DuckLength;

                                        if (short.TryParse(KeyWordValues[0], out DuckLength))
                                        {
                                            SFXSound.DuckerLength = DuckLength;
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
                        CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                        if (CurrentKeyWord.Equals("SAMPLE"))
                        {
                            uint SampleID = GenericFunctions.GetNewObjectID(FileProperties);
                            EXSample NewSample = new EXSample();
                            SampleName = string.Empty;

                            CurrentIndex++;
                            while (!FileLines[CurrentIndex].Trim().Equals("}"))
                            {
                                CurrentKeyWord = LoaderFunctions.GetKeyWord(FileLines[CurrentIndex]);
                                KeyWordValues = LoaderFunctions.GetKeyValues(FileLines[CurrentIndex]);
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
                NodeAddedInFolder = LoaderFunctions.AddItemInCustomFolder(FolderName, NewSoundKey.ToString(), NodeName, "Sounds", (byte)Enumerations.TreeNodeType.Sound, TreeViewControl, DefaultNodeColor, ImportResults);
                if (!NodeAddedInFolder)
                {
                    TreeNodeFunctions.TreeNodeAddNewNode("Sounds", NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, (byte)Enumerations.TreeNodeType.Sound, false, false, false, DefaultNodeColor, TreeViewControl);
                }
            }
            else
            {
                NodeAddedInFolder = LoaderFunctions.AddItemInCustomFolder(FolderName, NewSoundKey.ToString(), NodeName, "StreamedSounds", (byte)Enumerations.TreeNodeType.Sound, TreeViewControl, DefaultNodeColor, ImportResults);
                if (!NodeAddedInFolder)
                {
                    TreeNodeFunctions.TreeNodeAddNewNode("StreamedSounds", NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, (byte)Enumerations.TreeNodeType.Sound, false, false, false, DefaultNodeColor, TreeViewControl);
                }
            }

            //Add Sample Nodes
            foreach (KeyValuePair<uint, EXSample> Sample in SFXSound.Samples)
            {
                if (string.IsNullOrEmpty(Sample.Value.Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(NewSoundKey.ToString(), Sample.Key.ToString(), GenericFunctions.GetNextAvailableName("SMP_" + NodeName, TreeViewControl), 4, 4, (byte)Enumerations.TreeNodeType.Sample, false, false, false, Sample.Value.NodeColor, TreeViewControl);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(NewSoundKey.ToString(), Sample.Key.ToString(), GenericFunctions.GetNextAvailableName(Sample.Value.Name, TreeViewControl), 4, 4, (byte)Enumerations.TreeNodeType.Sample, false, false, false, Sample.Value.NodeColor, TreeViewControl);
                }
            }
        }
    }
}
