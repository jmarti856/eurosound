using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.HashCodesFunctions;
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

            string[] fileLines = File.ReadAllLines(LevelSoundBankPath);
            if (fileLines[0].Equals("#ftype:1", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < fileLines.Length; i += 1)
                {
                    if (!fileLines[i].StartsWith("#"))
                    {
                        if (fileLines[i].StartsWith("- "))
                        {
                            string[] line = fileLines[i].Split(null);
                            if (line.Length > 1)
                            {
                                yield return Path.GetDirectoryName(FilePath) + "\\" + line[1] + "\\effectProperties.yml";
                            }
                        }
                        else
                        {
                            yield return Path.GetDirectoryName(FilePath) + "\\" + fileLines[i] + "\\effectProperties.yml";
                        }
                    }
                }
                Array.Clear(fileLines, 0, fileLines.Length);
            }
            else
            {
                Reports.Add("1" + GenericFunctions.resourcesManager.GetString("Gen_ErrorReading_FileIncorrect") + ": " + LevelSoundBankPath);
            }
        }

        internal void LoadDataFromSwyterUnpacker(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDict, TreeView TreeViewControl, string FilePath, ProjectFile FileProperties)
        {
            uint soundHashcode;
            string soundName;

            //Read sounds from the unpacker folder
            IEnumerable<string> soundsPaths = GetFilePathsFromList(FilePath, FilePath);
            foreach (string path in soundsPaths)
            {
                soundName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                soundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, soundName);
                if (soundHashcode == 0x00000000)
                {
                    Reports.Add(string.Join(" ", "0Hashcode not found for the sound:", soundName));
                }
                ReadYmlFile(SoundsList, AudioDict, TreeViewControl, path, soundName, soundHashcode, false, FileProperties);
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

            //Update project status variable
            FileProperties.FileHasBeenModified = true;

            ShowErrorsWarningsList(FilePath);
        }

        internal void ReadYmlFile(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDict, TreeView TreeViewControl, string FilePath, string SoundName, uint SoundHashcode, bool ShowResultsAtEnd, ProjectFile FileProperties)
        {
            uint soundID;
            int[] currentSoundParams;
            bool soundNodeAdded = false;
            Dictionary<int, int[]> samplesProperties;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_ReadingYamlFile") + ": " + SoundName);

            if (Reports == null)
            {
                Reports = new List<string>();
            }

            try
            {
                StreamReader reader = new StreamReader(FilePath);
                string fileCheck = reader.ReadLine();
                if (fileCheck.Equals("#ftype:2", StringComparison.OrdinalIgnoreCase))
                {
                    //Load the stream
                    YamlStream yaml = new YamlStream();
                    yaml.Load(reader);

                    //Examine the stream
                    YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

                    currentSoundParams = GetSoundParams(mapping);
                    samplesProperties = GetSamples(mapping);

                    if (!TreeNodeFunctions.CheckIfNodeExistsByText(TreeViewControl, SoundName))
                    {
                        //--Add Sound--
                        soundID = GenericFunctions.GetNewObjectID(FileProperties);
                        EXSound NewSound = new EXSound()
                        {
                            Hashcode = SoundHashcode,
                            DuckerLength = (short)currentSoundParams[0],
                            MinDelay = (short)currentSoundParams[1],
                            MaxDelay = (short)currentSoundParams[2],
                            InnerRadiusReal = (short)currentSoundParams[3],
                            OuterRadiusReal = (short)currentSoundParams[4],
                            ReverbSend = (sbyte)currentSoundParams[5],
                            TrackingType = (sbyte)currentSoundParams[6],
                            MaxVoices = (sbyte)currentSoundParams[7],
                            Priority = (sbyte)currentSoundParams[8],
                            Ducker = (sbyte)currentSoundParams[9],
                            MasterVolume = (sbyte)currentSoundParams[10],
                            Flags = (ushort)currentSoundParams[11]
                        };

                        SoundsList.Add(soundID, NewSound);

                        //--Add Sample--
                        foreach (KeyValuePair<int, int[]> Entry in samplesProperties)
                        {
                            string sampleName = "SMP_" + SoundName + Entry.Key;
                            uint sampleID = GenericFunctions.GetNewObjectID(FileProperties);
                            EXSample newSample = new EXSample
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
                                if (!soundNodeAdded)
                                {
                                    TreeNodeFunctions.TreeNodeAddNewNode("StreamedSounds", soundID.ToString(), SoundName, 2, 2, (byte)Enumerations.TreeNodeType.Sound, false, false, false, SystemColors.WindowText, TreeViewControl);
                                    newSample.IsStreamed = true;
                                    soundNodeAdded = true;
                                }
                            }
                            else
                            {
                                if (!soundNodeAdded)
                                {
                                    TreeNodeFunctions.TreeNodeAddNewNode("Sounds", soundID.ToString(), SoundName, 2, 2, (byte)Enumerations.TreeNodeType.Sound, false, false, false, SystemColors.WindowText, TreeViewControl);
                                    soundNodeAdded = true;
                                }

                                if (EXSoundbanksFunctions.SubSFXFlagChecked(currentSoundParams[11]))
                                {
                                    uint GetHashcode = Convert.ToUInt32("0x" + Entry.Value[0].ToString("X8"), 16);
                                    newSample.HashcodeSubSFX = GlobalPreferences.SfxPrefix | GetHashcode;
                                    newSample.ComboboxSelectedAudio = string.Empty;
                                }
                                else
                                {
                                    int[] audioProps = new int[3];
                                    string audioPath = GetAudioFilePath(FilePath, Entry.Key, 0);

                                    if (File.Exists(audioPath))
                                    {
                                        string MD5AudioFilehash = GenericFunctions.CalculateMD5(audioPath);
                                        LoadAudio(audioPath, audioProps, SoundName, sampleName, TreeViewControl, AudioDict, newSample, FilePath, Entry.Key, MD5AudioFilehash);
                                    }
                                    else
                                    {
                                        Reports.Add("0The file: " + audioPath + " can't be loaded because does not exist.");
                                    }
                                }
                            }

                            //--Add Sample To Dictionary--
                            NewSound.Samples.Add(sampleID, newSample);
                            TreeNodeFunctions.TreeNodeAddNewNode(soundID.ToString(), sampleID.ToString(), sampleName, 4, 4, (byte)Enumerations.TreeNodeType.Sample, false, false, false, SystemColors.WindowText, TreeViewControl);
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
                    Reports.Add("1" + GenericFunctions.resourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
                }

                reader.Close();
                reader.Dispose();
            }
            catch
            {
                Reports.Add("0" + GenericFunctions.resourcesManager.GetString("Gen_ErrorRedingFile") + FilePath);
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private string GetAudioFilePath(string EffectPropertiesPath, int SampleName, int type)
        {
            string alphabet_string = "abcdefghijklmnopqrstuvwxyz";
            string audioPath;

            if (type == 0)
            {
                audioPath = Path.GetDirectoryName(EffectPropertiesPath) + "\\" + alphabet_string[SampleName] + ".wav";
            }
            else
            {
                audioPath = Path.GetDirectoryName(EffectPropertiesPath) + "\\" + alphabet_string[SampleName] + ".txt";
            }

            return audioPath;
        }

        private int[] GetAudioProperties(string PropertiesFilePath)
        {
            int[] properties = new int[3];
            string[] fileLines = File.ReadAllLines(PropertiesFilePath);

            if (fileLines[0].Equals("#ftype:3", StringComparison.OrdinalIgnoreCase))
            {
                if (fileLines.Length == 5)
                {
                    for (int i = 0; i < fileLines.Length; i++)
                    {
                        if (fileLines[i].StartsWith("#"))
                        {
                            continue;
                        }
                        else
                        {
                            properties[i - 2] = int.Parse(fileLines[i]);
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
                Reports.Add("1" + GenericFunctions.resourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
            }

            return properties;
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
            int sampleIndex;
            int index;

            Dictionary<int, int[]> samples = new Dictionary<int, int[]>();

            YamlMappingNode sampleParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("samples")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in sampleParameters.Children)
            {
                //Temporal array to store values
                int[] sampleParams = new int[7];

                //Get sample name
                sampleIndex = int.Parse(entry.Key.ToString());
                index = 0;

                YamlMappingNode sampleProps = (YamlMappingNode)sampleParameters.Children[new YamlScalarNode(sampleIndex.ToString())];
                foreach (KeyValuePair<YamlNode, YamlNode> flagsEntry in sampleProps.Children)
                {
                    //Get sample properties
                    if (index < 7)
                    {
                        sampleParams[index] = int.Parse(flagsEntry.Value.ToString());
                        index++;
                    }
                }
                samples.Add(sampleIndex, sampleParams);
            }

            return samples;
        }

        private int[] GetSoundParams(YamlMappingNode mapping)
        {
            bool[] soundFlags = new bool[12];
            bool readingFlags = false;
            int[] SndParams = new int[12];

            int index = 0;

            //Sound Parameters
            YamlMappingNode soundParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("params")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in soundParameters.Children)
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
                    YamlMappingNode flags = (YamlMappingNode)soundParameters.Children[new YamlScalarNode("flags")];
                    foreach (KeyValuePair<YamlNode, YamlNode> flagsEntry in flags.Children)
                    {
                        //Add the thirdteen flags to the array
                        if (index < 12)
                        {
                            soundFlags[index] = Convert.ToBoolean(flagsEntry.Value.ToString());
                            index++;
                        }
                    }
                }
            }
            //Array of values to number
            SndParams[11] = GetFlagsFromBoolArray(soundFlags);

            return SndParams;
        }

        private void LoadAudio(string AudioPath, int[] AudioProps, string SoundName, string SampleName, TreeView TreeViewControl, Dictionary<string, EXAudio> AudioDict, EXSample NewSample, string FilePath, int SampleKey, string MD5Hash)
        {
            if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.SoundbankChannels, GlobalPreferences.SoundbankFrequency))
            {
                string audioPropertiesPath = GetAudioFilePath(FilePath, SampleKey, 1);
                if (File.Exists(audioPropertiesPath))
                {
                    AudioProps = GetAudioProperties(audioPropertiesPath);
                }
                else
                {
                    Reports.Add("1The file: " + audioPropertiesPath + " can't be loaded because does not exist.");
                }

                EXAudio newAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);
                if (newAudio.PCMdata != null)
                {
                    if (!AudioDict.ContainsKey(MD5Hash))
                    {
                        newAudio.Flags = Convert.ToUInt16(AudioProps[0]);
                        newAudio.PSIsample = Convert.ToUInt32(AudioProps[1]);
                        newAudio.LoopOffset = Convert.ToUInt32(AudioProps[2]) / 2;

                        //Add Audio to dictionary and tree node
                        AudioDict.Add(MD5Hash, newAudio);
                        TreeNodeFunctions.TreeNodeAddNewNode("AudioData", MD5Hash, "AD_" + SampleName, 7, 7, (byte)Enumerations.TreeNodeType.Audio, false, false, false, SystemColors.WindowText, TreeViewControl);
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