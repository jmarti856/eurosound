﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;

namespace EuroSound_Application.SoundBanksEditor.YMLReader
{
    public class SoundBanksYMLReader
    {
        internal List<string> Reports;

        internal IEnumerable<string> GetFilePathsFromList(string LevelSoundBankPath, string FilePath)
        {
            Reports = new List<string>();

            string[] lines = File.ReadAllLines(LevelSoundBankPath);
            if (lines[0].Equals("#ftype:1", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < lines.Length; i += 1)
                {
                    if (!lines[i].StartsWith("#"))
                    {
                        string[] line = lines[i].Split(null);
                        if (line.Length > 1)
                        {
                            yield return Path.GetDirectoryName(FilePath) + "\\" + line[1] + "\\effectProperties.yml";
                        }
                    }
                }
                Array.Clear(lines, 0, lines.Length);
            }
            else
            {
                Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect") + ": " + LevelSoundBankPath);
            }
        }

        internal void LoadDataFromSwyterUnpacker(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDict, TreeView TreeViewControl, string FilePath, ProjectFile FileProperties)
        {
            uint SoundHashcode;
            string SoundName;

            //Read sounds from the unpacker folder
            IEnumerable<string> SoundsPaths = GetFilePathsFromList(FilePath, FilePath);
            foreach (string path in SoundsPaths)
            {
                SoundName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                SoundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, SoundName);
                if (SoundHashcode == 0x00000000)
                {
                    Reports.Add(string.Join(" ", "0Hashcode not found for the sound:", SoundName));
                }
                ReadYmlFile(SoundsList, AudioDict, TreeViewControl, path, SoundName, SoundHashcode, false, FileProperties);
            }

            //Collapse root nodes
            if (TreeViewControl.IsHandleCreated)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Nodes[0].Collapse();
                    TreeViewControl.Nodes[1].Collapse();
                    TreeViewControl.Nodes[2].Collapse();
                });
            }

            ShowErrorsWarningsList(FilePath);
        }

        internal void ReadYmlFile(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDict, TreeView TreeViewControl, string FilePath, string SoundName, uint SoundHashcode, bool ShowResultsAtEnd, ProjectFile FileProperties)
        {
            uint SoundID;
            int[] CurrentSoundParams;
            bool SoundNodeAdded = false;
            Dictionary<int, int[]> SamplesProperties;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_ReadingYamlFile") + ": " + SoundName);

            if (Reports == null)
            {
                Reports = new List<string>();
            }

            try
            {
                StreamReader reader = new StreamReader(FilePath);
                string FileCheck = reader.ReadLine();
                if (FileCheck.Equals("#ftype:2", StringComparison.OrdinalIgnoreCase))
                {
                    //Load the stream
                    YamlStream yaml = new YamlStream();
                    yaml.Load(reader);

                    //Examine the stream
                    YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

                    CurrentSoundParams = GetSoundParams(mapping);
                    SamplesProperties = GetSamples(mapping);

                    if (!TreeNodeFunctions.CheckIfNodeExistsByText(TreeViewControl, SoundName))
                    {
                        //--Add Sound--
                        SoundID = GenericFunctions.GetNewObjectID(FileProperties);
                        EXSound NewSound = new EXSound()
                        {
                            Hashcode = SoundHashcode,
                            DuckerLenght = (short)CurrentSoundParams[0],
                            MinDelay = (short)CurrentSoundParams[1],
                            MaxDelay = (short)CurrentSoundParams[2],
                            InnerRadiusReal = (short)CurrentSoundParams[3],
                            OuterRadiusReal = (short)CurrentSoundParams[4],
                            ReverbSend = (sbyte)CurrentSoundParams[5],
                            TrackingType = (sbyte)CurrentSoundParams[6],
                            MaxVoices = (sbyte)CurrentSoundParams[7],
                            Priority = (sbyte)CurrentSoundParams[8],
                            Ducker = (sbyte)CurrentSoundParams[9],
                            MasterVolume = (sbyte)CurrentSoundParams[10],
                            Flags = (ushort)CurrentSoundParams[11]
                        };

                        SoundsList.Add(SoundID, NewSound);

                        //--Add Sample--
                        foreach (KeyValuePair<int, int[]> Entry in SamplesProperties)
                        {
                            string SampleName = "SMP_" + SoundName + Entry.Key;
                            uint SampleID = GenericFunctions.GetNewObjectID(FileProperties);
                            EXSample NewSample = new EXSample
                            {
                                FileRef = (short)Entry.Value[0],
                                PitchOffset = (short)Entry.Value[1],
                                RandomPitchOffset = (short)Entry.Value[2],
                                BaseVolume = (sbyte)Entry.Value[3],
                                RandomVolumeOffset = (sbyte)Entry.Value[4],
                                Pan = (sbyte)Entry.Value[5],
                                RandomPan = (sbyte)Entry.Value[6]
                            };

                            if (Entry.Value[0] < 0)
                            {
                                if (!SoundNodeAdded)
                                {
                                    TreeNodeFunctions.TreeNodeAddNewNode("StreamedSounds", SoundID.ToString(), SoundName, 2, 2, "Sound", Color.Black, TreeViewControl);
                                    NewSample.IsStreamed = true;
                                    SoundNodeAdded = true;
                                }
                            }
                            else
                            {
                                if (!SoundNodeAdded)
                                {
                                    TreeNodeFunctions.TreeNodeAddNewNode("Sounds", SoundID.ToString(), SoundName, 2, 2, "Sound", Color.Black, TreeViewControl);
                                    SoundNodeAdded = true;
                                }

                                if (EXSoundbanksFunctions.SubSFXFlagChecked(CurrentSoundParams[11]))
                                {
                                    uint GetHashcode = Convert.ToUInt32("0x" + Entry.Value[0].ToString("X8"), 16);
                                    NewSample.HashcodeSubSFX = 0x1A000000 | GetHashcode;
                                    NewSample.ComboboxSelectedAudio = "<SUB SFX>";
                                }
                                else
                                {
                                    int[] AudioProps = new int[3];
                                    string AudioPath = GetAudioFilePath(FilePath, Entry.Key, 0);

                                    if (File.Exists(AudioPath))
                                    {
                                        string MD5AudioFilehash = GenericFunctions.CalculateMD5(AudioPath);
                                        LoadAudio(AudioPath, AudioProps, SoundName, SampleName, TreeViewControl, AudioDict, NewSample, FilePath, Entry.Key, MD5AudioFilehash);
                                    }
                                    else
                                    {
                                        Reports.Add("0The file: " + AudioPath + " can't be loaded because does not exists.");
                                    }
                                }
                            }

                            //--Add Sample To Dictionary--
                            NewSound.Samples.Add(SampleID, NewSample);
                            TreeNodeFunctions.TreeNodeAddNewNode(SoundID.ToString(), SampleID.ToString(), SampleName, 4, 4, "Sample", Color.Black, TreeViewControl);
                        }
                    }
                    else
                    {
                        Reports.Add("0The sound: " + SoundName + " can't be loaded because seems that exists (one item with the same name already exists).");
                    }

                    //Show results at end
                    if (ShowResultsAtEnd)
                    {
                        ShowErrorsWarningsList(FilePath);
                    }
                }
                else
                {
                    Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
                }

                reader.Close();
                reader.Dispose();
            }
            catch
            {
                Reports.Add("0" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorRedingFile") + FilePath);
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private string GetAudioFilePath(string EffectPropertiesPath, int SampleName, int type)
        {
            string alphabet_string = "abcdefghijklmnopqrstuvwxyz";
            string AudioPath;

            if (type == 0)
            {
                AudioPath = Path.GetDirectoryName(EffectPropertiesPath) + "\\" + alphabet_string[SampleName] + ".wav";
            }
            else
            {
                AudioPath = Path.GetDirectoryName(EffectPropertiesPath) + "\\" + alphabet_string[SampleName] + ".txt";
            }

            return AudioPath;
        }

        private int[] GetAudioProperties(string PropertiesFilePath)
        {
            int[] Properties = new int[3];
            string[] Lines = File.ReadAllLines(PropertiesFilePath);

            if (Lines[0].Equals("#ftype:3", StringComparison.OrdinalIgnoreCase))
            {
                if (Lines.Length == 5)
                {
                    for (int i = 0; i < Lines.Length; i++)
                    {
                        if (Lines[i].StartsWith("#"))
                        {
                            continue;
                        }
                        else
                        {
                            Properties[i - 2] = int.Parse(Lines[i]);
                        }
                    }
                }
                else
                {
                    Reports.Add("1The file: " + PropertiesFilePath + " seems to be corrupt and has been skiped");
                }
            }
            else
            {
                Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
            }

            return Properties;
        }

        private int GetFlagsFromBoolArray(bool[] Flags)
        {
            int bitfield = 0;
            for (int i = 0; i < Flags.Length; i++)
            {
                if (Flags[i] == true)
                {
                    bitfield |= (1 << i);
                }
            }
            return bitfield;
        }

        private Dictionary<int, int[]> GetSamples(YamlMappingNode mapping)
        {
            int SampleIndex;
            int index;

            Dictionary<int, int[]> Samples = new Dictionary<int, int[]>();

            YamlMappingNode SampleParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("samples")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SampleParameters.Children)
            {
                //Temporal array to store values
                int[] SampleParams = new int[7];

                //Get sample name
                SampleIndex = int.Parse(entry.Key.ToString());
                index = 0;

                YamlMappingNode SampleProps = (YamlMappingNode)SampleParameters.Children[new YamlScalarNode(SampleIndex.ToString())];
                foreach (KeyValuePair<YamlNode, YamlNode> flagsEntry in SampleProps.Children)
                {
                    //Get sample properties
                    if (index < 7)
                    {
                        SampleParams[index] = int.Parse(flagsEntry.Value.ToString());
                        index++;
                    }
                }
                Samples.Add(SampleIndex, SampleParams);
            }

            return Samples;
        }

        private int[] GetSoundParams(YamlMappingNode mapping)
        {
            bool[] SndFlags = new bool[12];
            bool readingFlags = false;
            int[] SndParams = new int[12];

            int index = 0;

            //Sound Parameters
            YamlMappingNode SoundParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("params")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SoundParameters.Children)
            {
                //Read Parameters
                if (index < 11)
                {
                    switch (entry.Value.ToString())
                    {
                        case "2D":
                            SndParams[index] = 0;
                            break;

                        case "Amb":
                            SndParams[index] = 1;
                            break;

                        case "3D":
                            SndParams[index] = 2;
                            break;

                        case "3D_Rnd_Pos":
                            SndParams[index] = 3;
                            break;

                        case "2D_PL2":
                            SndParams[index] = 4;
                            break;

                        default:
                            SndParams[index] = int.Parse(entry.Value.ToString());
                            break;
                    }
                    index++;
                }

                //Check for flags node
                if (entry.Key.ToString().Equals("flags"))
                {
                    readingFlags = true;
                    index = 0;
                }

                //Read Flags value
                if (readingFlags)
                {
                    YamlMappingNode Flags = (YamlMappingNode)SoundParameters.Children[new YamlScalarNode("flags")];
                    foreach (KeyValuePair<YamlNode, YamlNode> flagsEntry in Flags.Children)
                    {
                        //Add the thirdteen flags to the array
                        if (index < 12)
                        {
                            SndFlags[index] = Convert.ToBoolean(flagsEntry.Value.ToString());
                            index++;
                        }
                    }
                }
            }
            //Array of values to number
            SndParams[11] = GetFlagsFromBoolArray(SndFlags);

            return SndParams;
        }

        private void LoadAudio(string AudioPath, int[] AudioProps, string SoundName, string SampleName, TreeView TreeViewControl, Dictionary<string, EXAudio> AudioDict, EXSample NewSample, string FilePath, int SampleKey, string MD5Hash)
        {
            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.SoundbankChannels, GlobalPreferences.SoundbankFrequency))
            {
                string AudioPropertiesPath = GetAudioFilePath(FilePath, SampleKey, 1);
                if (File.Exists(AudioPropertiesPath))
                {
                    AudioProps = GetAudioProperties(AudioPropertiesPath);
                }
                else
                {
                    Reports.Add("1The file: " + AudioPropertiesPath + " can't be loaded because does not exists.");
                }

                EXAudio NewAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);
                if (NewAudio.PCMdata != null)
                {
                    if (!AudioDict.ContainsKey(MD5Hash))
                    {
                        NewAudio.Flags = Convert.ToUInt16(AudioProps[0]);
                        NewAudio.PSIsample = Convert.ToUInt32(AudioProps[1]);
                        NewAudio.LoopOffset = Convert.ToUInt32(AudioProps[2]) / 2;

                        //Add Audio to dictionary and tree node
                        AudioDict.Add(MD5Hash, NewAudio);
                        TreeNodeFunctions.TreeNodeAddNewNode("AudioData", MD5Hash, "AD_" + SampleName, 7, 7, "Audio", Color.Black, TreeViewControl);
                    }
                    else
                    {
                        Reports.Add("1The file: " + AudioPath + " used by: " + SoundName + " has not been added because already exists.");
                    }
                }
                else
                {
                    Reports.Add("0The file: " + AudioPath + " can't be readed, seems that is being used by another process or has a bad format.");
                }

                NewSample.ComboboxSelectedAudio = MD5Hash;
            }
            else
            {
                Reports.Add("0The file: " + AudioPath + " has not a valid format.");
            }
        }

        private void ShowErrorsWarningsList(string FilePath)
        {
            if (Reports.Count > 0)
            {
                GenericFunctions.ShowErrorsAndWarningsList(Reports, Path.GetFileName(FilePath) + " Import results", null);
                Reports = null;
            }
        }
    }
}