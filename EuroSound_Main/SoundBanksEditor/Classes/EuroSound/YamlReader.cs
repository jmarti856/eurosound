﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;

namespace EuroSound_Application
{
    public static class YamlReader
    {
        internal static List<string> Reports = new List<string>();

        internal static List<string> GetFilePaths(string LevelSoundBankPath, string FilePath)
        {
            List<string> Paths = new List<string>();

            string[] lines = File.ReadAllLines(LevelSoundBankPath);
            if (lines[0].Equals("#ftype:1"))
            {
                for (int i = 0; i < lines.Length; i += 1)
                {
                    if (!lines[i].StartsWith("#"))
                    {
                        string[] line = lines[i].Split(null);
                        Paths.Add(Path.GetDirectoryName(FilePath) + "\\" + line[1] + "\\effectProperties.yml");
                    }
                }
            }
            else
            {
                Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
            }

            return Paths;
        }

        internal static void LoadDataFromSwyterUnpacker(Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDict, TreeView TreeViewControl, string FilePath, ProjectFile FileProperties)
        {
            List<string> SoundsPaths;
            string SoundName, SoundHashcode;

            //Read sounds from the unpacker folder
            SoundsPaths = GetFilePaths(FilePath, FilePath);
            foreach (string path in SoundsPaths)
            {
                SoundName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                SoundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, SoundName);
                FileProperties.Hashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SB_Defines, Path.GetFileNameWithoutExtension(FilePath));
                if (string.IsNullOrEmpty(SoundHashcode))
                {
                    Reports.Add("0Hashcode not found for the sound ");
                }
                ReadYamlFile(SoundsList, AudioDict, TreeViewControl, path, SoundName, SoundHashcode, false, FileProperties);
            }

            //Expand only root nodes
            TreeViewControl.Invoke((MethodInvoker)delegate
            {
                TreeViewControl.Nodes[0].Collapse();
                TreeViewControl.Nodes[0].Expand();

                TreeViewControl.Nodes[1].Collapse();
                TreeViewControl.Nodes[1].Expand();

                TreeViewControl.Nodes[2].Collapse();
                TreeViewControl.Nodes[2].Expand();
            });

            ShowErrorsWarningsList(FilePath);
        }

        internal static void ReadYamlFile(Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDict, TreeView TreeViewControl, string FilePath, string SoundName, string SoundHashcode, bool ShowResultsAtEnd, ProjectFile FileProperties)
        {
            int SoundID;
            int[] CurrentSoundParams;
            bool SoundNodeAdded = false;
            Dictionary<int, int[]> SamplesProperties;

            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_ReadingYamlFile") + ": " + SoundName);

            StreamReader reader = new StreamReader(FilePath);
            string FileCheck = reader.ReadLine();
            if (FileCheck.Equals("#ftype:2"))
            {
                // Load the stream
                YamlStream yaml = new YamlStream();
                yaml.Load(reader);

                // Examine the stream
                YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

                CurrentSoundParams = GetSoundParams(mapping);
                SamplesProperties = GetSamples(mapping);

                if (!TreeNodeFunctions.CheckIfNodeExistsByText(TreeViewControl, SoundName))
                {
                    /*--Add Sound--*/
                    SoundID = EXObjectsFunctions.GetSoundID(FileProperties);
                    EXSound NewSound = new EXSound()
                    {
                        DisplayName = SoundName,
                        Hashcode = SoundHashcode,
                        DuckerLenght = CurrentSoundParams[0],
                        MinDelay = CurrentSoundParams[1],
                        MaxDelay = CurrentSoundParams[2],
                        InnerRadiusReal = CurrentSoundParams[3],
                        OuterRadiusReal = CurrentSoundParams[4],
                        ReverbSend = CurrentSoundParams[5],
                        TrackingType = CurrentSoundParams[6],
                        MaxVoices = CurrentSoundParams[7],
                        Priority = CurrentSoundParams[8],
                        Ducker = CurrentSoundParams[9],
                        MasterVolume = CurrentSoundParams[10],
                        Flags = CurrentSoundParams[11]
                    };

                    SoundsList.Add(SoundID, NewSound);

                    /*--Add Sample--*/
                    foreach (KeyValuePair<int, int[]> Entry in SamplesProperties)
                    {
                        string SampleName = "SMP_" + NewSound.DisplayName + Entry.Key;
                        EXSample NewSample = new EXSample
                        {
                            Name = SampleName,
                            DisplayName = SampleName,
                            FileRef = Entry.Value[0],
                            PitchOffset = Entry.Value[1],
                            RandomPitchOffset = Entry.Value[2],
                            BaseVolume = Entry.Value[3],
                            RandomVolumeOffset = Entry.Value[4],
                            Pan = Entry.Value[5],
                            RandomPan = Entry.Value[6]
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

                            if (EXObjectsFunctions.SubSFXFlagChecked(CurrentSoundParams[11]))
                            {
                                uint GetHashcode = Convert.ToUInt32("0x" + Entry.Value[0].ToString("X8"), 16);
                                NewSample.HashcodeSubSFX = GetSoundHashcode(GetHashcode);
                                NewSample.ComboboxSelectedAudio = "<SUB SFX>";
                            }
                            else
                            {
                                int[] AudioProps = new int[3];
                                string AudioPath = GetAudioFilePath(FilePath, Entry.Key, 0);
                                if (File.Exists(AudioPath))
                                {
                                    string AudioPropertiesPath = GetAudioFilePath(FilePath, Entry.Key, 1);
                                    if (File.Exists(AudioPropertiesPath))
                                    {
                                        AudioProps = GetAudioProperties(AudioPropertiesPath);
                                    }
                                    else
                                    {
                                        Reports.Add("1The file: " + AudioPropertiesPath + " can't be loaded because does not exists.");
                                    }
                                    string MD5AudioFilehash = EXObjectsFunctions.LoadAudioAddToListAndTreeNode(AudioPath, SampleName, AudioDict, TreeViewControl, AudioProps, Reports);
                                    NewSample.ComboboxSelectedAudio = MD5AudioFilehash;
                                }
                                else
                                {
                                    Reports.Add("0The file: " + AudioPath + " can't be loaded because does not exists.");
                                }
                            }
                        }

                        /*--Add Sample To Dictionary--*/
                        NewSound.Samples.Add(NewSample);
                        TreeNodeFunctions.TreeNodeAddNewNode(SoundID.ToString(), SampleName, SampleName, 4, 4, "Sample", Color.Black, TreeViewControl);
                    }
                }
                else
                {
                    Reports.Add("0The sound: " + SoundName + " can't be loaded because seems that exists (one item with the same name already exists).");
                }

                // Show results at end
                if (ShowResultsAtEnd)
                {
                    ShowErrorsWarningsList(FilePath);
                }

                SamplesProperties = null;
            }
            else
            {
                Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
            }

            reader.Close();
            reader.Dispose();

            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private static string GetAudioFilePath(string EffectPropertiesPath, int SampleName, int type)
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

        private static int[] GetAudioProperties(string PropertiesFilePath)
        {
            int[] Properties = new int[3];
            string[] Lines = File.ReadAllLines(PropertiesFilePath);

            if (Lines[0].Equals("#ftype:3"))
            {
                if (Lines.Length == 5)
                {
                    if (Lines[0].Equals("#ftype:3"))
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

        private static int GetFlagsFromBoolArray(bool[] Flags)
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

        private static Dictionary<int, int[]> GetSamples(YamlMappingNode mapping)
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

        private static string GetSoundHashcode(uint Hashcode)
        {
            int FinalHashcode = ((int)(0x1A000000 | Hashcode));
            return "0x" + FinalHashcode.ToString("X8");
        }

        private static int[] GetSoundParams(YamlMappingNode mapping)
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

        private static void ShowErrorsWarningsList(string FilePath)
        {
            if (Reports.Count > 0)
            {
                GenericFunctions.ShowErrorsAndWarningsList(Reports, Path.GetFileName(FilePath) + " Import results");
                Reports = null;
            }
        }
    }
}