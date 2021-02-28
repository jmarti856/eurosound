using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.TreeViewLibraryFunctions;
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
        Dictionary<int, string> AudiosAssocTable = new Dictionary<int, string>();
        List<string> ImportResults = new List<string>();

        //*===============================================================================================
        //* MAIN
        //*===============================================================================================
        internal List<string> LoadSFX_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string[] lines = File.ReadAllLines(FilePath);

            if (lines[0].Equals("*EUROSOUND_INTERCHANGE_FILE V1.0"))
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i]) || lines[i].StartsWith("*COMMENT"))
                    {
                        continue;
                    }
                    else
                    {
                        //Check for project settings block
                        if (lines[i].Trim().StartsWith("*PROJECTSETTINGS"))
                        {
                            i++;
                            ReadProjectSettingsBlock(lines, i, FileProperties);
                        }

                        //Check for audio data block
                        if (lines[i].Trim().StartsWith("*AUDIODATA"))
                        {
                            i++;
                            ReadAudioDataBlock(lines, i, AudiosList, TreeViewControl);
                        }

                        //SFX SOUND BLOCK
                        if (lines[i].Trim().StartsWith("*SFXSOUND"))
                        {
                            i++;
                            ReadSFXSoundBlock(lines, i, FileProperties, TreeViewControl, SoundsList);
                        }
                    }
                }
                lines = null;
                AudiosAssocTable.Clear();
            }
            else
            {
                ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
            }

            return ImportResults;
        }

        //*===============================================================================================
        //* PROJECTSETTINGS
        //*===============================================================================================
        private void ReadProjectSettingsBlock(string[] FileLines, int CurrentIndex, ProjectFile FileProperties)
        {
            string[] SplitedData;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                if (FileLines[CurrentIndex].Trim().StartsWith("*FILENAME"))
                {
                    SplitedData = FileLines[CurrentIndex].Trim().Split('"');
                    if (SplitedData.Length > 1)
                    {
                        FileProperties.FileName = SplitedData[1];
                    }
                    else
                    {
                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *FILENAME does not contains any value"));
                        break;
                    }
                }

                if (FileLines[CurrentIndex].Trim().StartsWith("*HASHCODE"))
                {
                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                    if (SplitedData.Length > 1)
                    {
                        FileProperties.Hashcode = Convert.ToUInt32(SplitedData[1], 16);
                    }
                    else
                    {
                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *HASHCODE does not contains any value"));
                        break;
                    }
                }
                CurrentIndex++;
            }
        }

        //*===============================================================================================
        //* AUDIODATA
        //*===============================================================================================
        private void ReadAudioDataBlock(string[] FileLines, int CurrentIndex, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string[] SplitedData;
            string MD5AudioFilehash = string.Empty, AudioPath = string.Empty, NodeName;
            ushort AudioFlags = 0, LoopOffset = 0;
            uint AudioPSI = 0;
            int FileRef;
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                if (FileLines[CurrentIndex].Trim().StartsWith("*AUDIO"))
                {
                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                    if (SplitedData.Length > 1)
                    {
                        EXAudio NewAudio = null;
                        FileRef = int.Parse(SplitedData[1]);

                        CurrentIndex++;
                        while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                        {
                            if (FileLines[CurrentIndex].Trim().StartsWith("*PATH"))
                            {
                                SplitedData = FileLines[CurrentIndex].Trim().Split('"');
                                if (SplitedData.Length > 1)
                                {
                                    AudioPath = RemoveCharactersWithoutDisplayWidth(SplitedData[1].Trim());
                                    if (File.Exists(AudioPath))
                                    {
                                        if (GenericFunctions.AudioIsValid(AudioPath, 1, 22050))
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
                                else
                                {
                                    ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *PATH does not have a valid value"));
                                    break;
                                }
                            }
                            if (FileLines[CurrentIndex].Trim().StartsWith("*NODECOLOR"))
                            {
                                SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                if (SplitedData.Length > 3)
                                {
                                    DefaultNodeColor = Color.FromArgb(1, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), int.Parse(SplitedData[3]));
                                }
                                else
                                {
                                    ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *NODECOLOR does not have a valid value"));
                                    break;
                                }
                            }
                            if (FileLines[CurrentIndex].Trim().StartsWith("*FLAGS"))
                            {
                                SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                if (SplitedData.Length > 1)
                                {
                                    AudioFlags = ushort.Parse(SplitedData[1].Trim());
                                }
                                else
                                {
                                    ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *FLAGS does not have a valid value"));
                                    break;
                                }
                            }
                            if (FileLines[CurrentIndex].Trim().StartsWith("*LOOPOFFSET"))
                            {
                                SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                if (SplitedData.Length > 1)
                                {
                                    LoopOffset = ushort.Parse(SplitedData[1].Trim());
                                }
                                else
                                {
                                    ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *LOOPOFFSET does not have a valid value"));
                                    break;
                                }
                            }
                            if (FileLines[CurrentIndex].Trim().StartsWith("*PSI"))
                            {
                                SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                if (SplitedData.Length > 1)
                                {
                                    AudioPSI = uint.Parse(SplitedData[1].Trim());
                                }
                                else
                                {
                                    ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *PSI does not have a valid value"));
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
                                NewAudio.LoopOffset = LoopOffset;
                                NewAudio.Flags = AudioFlags;
                                NewAudio.PSIsample = AudioPSI;

                                //Add Audio to dictionary
                                AudiosList.Add(MD5AudioFilehash, NewAudio);

                                //Add Node
                                NodeName = "AD_" + Path.GetFileNameWithoutExtension(AudioPath);
                                TreeNodeFunctions.TreeNodeAddNewNode("AudioData", MD5AudioFilehash, GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 7, 7, "Audio", DefaultNodeColor, TreeViewControl);
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

        //*===============================================================================================
        //* SFXSOUND
        //*===============================================================================================
        private void ReadSFXSoundBlock(string[] FileLines, int CurrentIndex, ProjectFile FileProperties, TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList)
        {
            string[] SplitedData;
            string NodeName = string.Empty;
            string SampleName = string.Empty;
            uint NewSoundKey = GenericFunctions.GetNewObjectID(FileProperties);
            short FileRef = 0;
            EXSound SFXSound = new EXSound();
            Color DefaultNodeColor = Color.FromArgb(1, 0, 0, 0);
            Color DefaultSampleNodeColor = Color.FromArgb(1, 0, 0, 0);
            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                if (FileLines[CurrentIndex].Trim().StartsWith("*NODENAME"))
                {
                    SplitedData = FileLines[CurrentIndex].Trim().Split('"');
                    if (SplitedData.Length > 1)
                    {
                        NodeName = SplitedData[1].Trim();
                    }
                    else
                    {
                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *NODENAME does not have a valid value"));
                        break;
                    }
                }
                if (FileLines[CurrentIndex].Trim().StartsWith("*HASHCODE"))
                {
                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                    if (SplitedData.Length > 1)
                    {
                        SFXSound.Hashcode = Convert.ToUInt32(SplitedData[1].Trim(), 16);
                    }
                    else
                    {
                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *HASHCODE does not have a valid value"));
                        break;
                    }
                }
                if (FileLines[CurrentIndex].Trim().StartsWith("*NODECOLOR"))
                {
                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                    if (SplitedData.Length > 3)
                    {
                        DefaultNodeColor = Color.FromArgb(1, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), int.Parse(SplitedData[3]));
                    }
                    else
                    {
                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *NODECOLOR does not have a valid value"));
                        break;
                    }
                }
                if (FileLines[CurrentIndex].Trim().StartsWith("*PARAMETERS"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        if (FileLines[CurrentIndex].Trim().StartsWith("*DUCKERLENGTH"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.DuckerLenght = short.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *DUCKERLENGTH does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*MINDELAY"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.MinDelay = short.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MINDELAY does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*MAXDELAY"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.MaxDelay = short.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MAXDELAY does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*REVERBSEND"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.ReverbSend = sbyte.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *REVERBSEND does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*TRACKINGTYPE"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.TrackingType = sbyte.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *TRACKINGTYPE does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*MAXVOICES"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.MaxVoices = sbyte.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MAXVOICES does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*PRIORITY"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.Priority = sbyte.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *PRIORITY does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().Contains("*DUCKER "))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.Ducker = sbyte.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *DUCKER does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*MASTERVOLUME"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.MasterVolume = sbyte.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *MASTERVOLUME does not have a valid value"));
                                break;
                            }
                        }
                        if (FileLines[CurrentIndex].Trim().StartsWith("*FLAGS"))
                        {
                            SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                            if (SplitedData.Length > 1)
                            {
                                SFXSound.Flags = ushort.Parse(SplitedData[1].Trim());
                            }
                            else
                            {
                                ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *FLAGS does not have a valid value"));
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
                //Check for sound samples
                if (FileLines[CurrentIndex].Trim().StartsWith("*SAMPLES"))
                {
                    CurrentIndex++;
                    while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
                    {
                        if (FileLines[CurrentIndex].Trim().StartsWith("*SAMPLE"))
                        {
                            uint SampleID = GenericFunctions.GetNewObjectID(FileProperties);
                            EXSample NewSample = new EXSample();
                            SampleName = string.Empty;

                            CurrentIndex++;
                            while (!FileLines[CurrentIndex].Trim().Equals("}"))
                            {
                                if (FileLines[CurrentIndex].Trim().StartsWith("*NODENAME"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split('"');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.Name = SplitedData[1].Trim();
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *NODENAME does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*NODECOLOR"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 3)
                                    {
                                        NewSample.NodeColor = Color.FromArgb(1, int.Parse(SplitedData[1]), int.Parse(SplitedData[2]), int.Parse(SplitedData[3]));
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *NODECOLOR does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*PITCHOFFSET"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.PitchOffset = short.Parse(SplitedData[1].Trim());
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *PITCHOFFSET does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*BASEVOLUME"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.BaseVolume = sbyte.Parse(SplitedData[1].Trim());
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *BASEVOLUME does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*PAN"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.Pan = sbyte.Parse(SplitedData[1].Trim());
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *PAN does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*RANDOMPITCHOFFSET"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.RandomPitchOffset = short.Parse(SplitedData[1].Trim());
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *RANDOMPITCHOFFSET does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*RANDOMVOLUMEOFFSET"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.RandomVolumeOffset = sbyte.Parse(SplitedData[1].Trim());
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *RANDOMVOLUMEOFFSET does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*RANDOMPAN"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        NewSample.RandomPan = sbyte.Parse(SplitedData[1].Trim());
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *RANDOMPAN does not have a valid value"));
                                        break;
                                    }
                                }
                                if (FileLines[CurrentIndex].Trim().StartsWith("*FILEREF"))
                                {
                                    SplitedData = FileLines[CurrentIndex].Trim().Split(' ');
                                    if (SplitedData.Length > 1)
                                    {
                                        FileRef = short.Parse(SplitedData[1].Trim());
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
                                    }
                                    else
                                    {
                                        ImportResults.Add(string.Join("", "0", "Error in line: ", (CurrentIndex + 1), " *FILEREF does not have a valid value"));
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
                TreeNodeFunctions.TreeNodeAddNewNode("Sounds", NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Sound", DefaultNodeColor, TreeViewControl);
            }
            else
            {
                TreeNodeFunctions.TreeNodeAddNewNode("StreamedSounds", NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, "Sound", DefaultNodeColor, TreeViewControl);
            }

            //Add Sample Nodes
            foreach (KeyValuePair<uint, EXSample> Sample in SFXSound.Samples)
            {
                if (string.IsNullOrEmpty(Sample.Value.Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(NewSoundKey.ToString(), Sample.Key.ToString(), GenericFunctions.GetNextAvailableName("SMP_" + NodeName, TreeViewControl), 4, 4, "Sample", Sample.Value.NodeColor, TreeViewControl);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(NewSoundKey.ToString(), Sample.Key.ToString(), GenericFunctions.GetNextAvailableName(Sample.Value.Name, TreeViewControl), 4, 4, "Sample", Sample.Value.NodeColor, TreeViewControl);
                }
            }
        }

        private string RemoveCharactersWithoutDisplayWidth(string str)
        {
            Regex regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
            return regex.Replace(str, "");
        }
    }
}
