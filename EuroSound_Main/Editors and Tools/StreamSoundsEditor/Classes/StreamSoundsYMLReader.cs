using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;

namespace EuroSound_Application.StreamSounds.YMLReader
{
    internal class StreamSoundsYMLReader
    {
        private AudioFunctions AudioLibrary = new AudioFunctions();
        internal List<string> Reports;

        internal IEnumerable<string> GetFilePathsFromList(string LevelSoundBankPath, string FilePath)
        {
            Reports = new List<string>();

            string[] lines = File.ReadAllLines(LevelSoundBankPath);
            if (lines[0].Equals("#ftype:5", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < lines.Length; i += 1)
                {
                    if (!lines[i].StartsWith("#"))
                    {
                        if (!string.IsNullOrEmpty(lines[i]))
                        {
                            string[] line = lines[i].Split(null);
                            yield return Path.GetDirectoryName(FilePath) + "\\" + line[1];
                        }
                    }
                }
                Array.Clear(lines, 0, lines.Length);
            }
            else
            {
                Reports.Add("1" + GenericFunctions.resourcesManager.GetString("Gen_ErrorReading_FileIncorrect") + ": " + LevelSoundBankPath);
            }
        }

        internal void LoadDataFromSwyterUnpacker(Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl, string FilePath, ProjectFile FileProperties)
        {
            //Read sounds from the unpacker folder
            IEnumerable<string> SoundsPaths = GetFilePathsFromList(FilePath, FilePath);
            foreach (string path in SoundsPaths)
            {
                ReadYmlFile(SoundsList, TreeViewControl, path, Path.GetFileName(path), FileProperties);
            }

            //Collapse root nodes
            TreeViewControl.Invoke((MethodInvoker)delegate
            {
                TreeViewControl.Nodes[0].Collapse();
            });

            ShowErrorsWarningsList(FilePath);
        }

        public void ReadYmlFile(Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl, string FilePath, string SoundName, ProjectFile FileProperties)
        {
            uint soundID;
            uint[] currentSoundParams;
            IEnumerable<uint[]> startMarkers;
            IEnumerable<int[]> markers;
            string wavDataPath, fileCheck;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_ReadingYamlFile") + ": " + SoundName);
            if (Reports == null)
            {
                Reports = new List<string>();
            }

            StreamReader reader = new StreamReader(FilePath);
            fileCheck = reader.ReadLine();
            if (fileCheck.Equals("#ftype:4", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    //Load the stream
                    YamlStream yaml = new YamlStream();
                    yaml.Load(reader);

                    //Examine the stream
                    YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

                    currentSoundParams = GetSoundParams(mapping);
                    startMarkers = GetStartMarkers(mapping);
                    markers = GetMarkers(mapping);
                    wavDataPath = GetSoundFilePath(mapping, FilePath);

                    EXSoundStream soundToAdd = new EXSoundStream
                    {
                        BaseVolume = currentSoundParams[4],
                    };

                    //------------------READ WAV FILE----------------------
                    if (GenericFunctions.AudioIsValid(wavDataPath, GlobalPreferences.StreambankChannels, GlobalPreferences.StreambankFrequency))
                    {
                        LoadAudio(wavDataPath, soundToAdd, false);
                    }
                    else
                    {
                        LoadAudio(wavDataPath, soundToAdd, true);
                    }

                    foreach (uint[] startMarkerData in startMarkers)
                    {
                        EXStreamStartMarker StartMarker = new EXStreamStartMarker
                        {
                            Name = startMarkerData[0],
                            Position = startMarkerData[1],
                            MusicMakerType = startMarkerData[2],
                            Flags = startMarkerData[3],
                            Extra = startMarkerData[4],
                            LoopStart = startMarkerData[5],
                            MarkerCount = startMarkerData[6],
                            LoopMarkerCount = startMarkerData[7],
                            MarkerPos = startMarkerData[8],
                            IsInstant = startMarkerData[9],
                            InstantBuffer = startMarkerData[10],
                            StateA = startMarkerData[11],
                            StateB = startMarkerData[12]
                        };

                        soundToAdd.StartMarkers.Add(StartMarker);
                    }

                    foreach (int[] markerData in markers)
                    {
                        EXStreamMarker Marker = new EXStreamMarker
                        {
                            Name = markerData[0],
                            Position = (uint)markerData[1],
                            MusicMakerType = (uint)markerData[2],
                            Flags = (uint)markerData[3],
                            Extra = (uint)markerData[4],
                            LoopStart = (uint)markerData[5],
                            MarkerCount = (uint)markerData[6],
                            LoopMarkerCount = (uint)markerData[7]
                        };

                        soundToAdd.Markers.Add(Marker);
                    }

                    soundID = GenericFunctions.GetNewObjectID(FileProperties);
                    SoundsList.Add(soundID, soundToAdd);

                    TreeNodeFunctions.TreeNodeAddNewNode("Sounds", soundID.ToString(), SoundName, 2, 2, (byte)Enumerations.TreeNodeType.Sound, false, false, false, SystemColors.WindowText, TreeViewControl);
                }
                catch
                {
                    Reports.Add("1" + GenericFunctions.resourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
                }
            }
            else
            {
                Reports.Add("1" + GenericFunctions.resourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
            }

            reader.Close();
            reader.Dispose();

            //Update project status variable
            FileProperties.FileHasBeenModified = true;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private uint[] GetSoundParams(YamlMappingNode mapping)
        {
            uint[] parameters = new uint[5];
            //Sound Parameters
            YamlMappingNode soundParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("properties")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in soundParameters.Children)
            {
                if (entry.Key.ToString().Equals("StartMarkerCount"))
                {
                    parameters[0] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("MarkerCount"))
                {
                    parameters[1] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("StartMarkerOffset"))
                {
                    parameters[2] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("MarkerOffset"))
                {
                    parameters[3] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("BaseVolume"))
                {
                    parameters[4] = Convert.ToUInt32(entry.Value.ToString());
                }
            }

            return parameters;
        }

        private string GetSoundFilePath(YamlMappingNode mapping, string FilePath)
        {
            string soundFilePath = string.Empty;

            //Sound Parameters
            YamlMappingNode soundParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("Data")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in soundParameters.Children)
            {
                if (entry.Key.ToString().Equals("FileName"))
                {
                    soundFilePath = Path.GetDirectoryName(FilePath) + "\\" + entry.Value.ToString();
                }
            }

            return soundFilePath;
        }

        private IEnumerable<uint[]> GetStartMarkers(YamlMappingNode mapping)
        {
            YamlMappingNode sampleParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("StartMarkers")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in sampleParameters.Children)
            {
                //Temporal array to store values
                uint[] SampleParams = new uint[13];

                //Get sample name
                int sampleIndex = int.Parse(entry.Key.ToString());
                SampleParams[0] = (uint)sampleIndex;
                int index = 1;

                YamlMappingNode SampleProps = (YamlMappingNode)sampleParameters.Children[new YamlScalarNode(sampleIndex.ToString())];
                foreach (KeyValuePair<YamlNode, YamlNode> flagsEntry in SampleProps.Children)
                {
                    //Get sample properties
                    if (index < SampleParams.Length)
                    {
                        SampleParams[index] = uint.Parse(flagsEntry.Value.ToString());
                        index++;
                    }
                }
                yield return SampleParams;
            }
        }

        private IEnumerable<int[]> GetMarkers(YamlMappingNode mapping)
        {
            YamlMappingNode SampleParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("Markers")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SampleParameters.Children)
            {
                //Temporal array to store values
                int[] SampleParams = new int[8];

                //Get sample name
                int sampleIndex = int.Parse(entry.Key.ToString());
                int index = 0;

                YamlMappingNode SampleProps = (YamlMappingNode)SampleParameters.Children[new YamlScalarNode(sampleIndex.ToString())];
                foreach (KeyValuePair<YamlNode, YamlNode> flagsEntry in SampleProps.Children)
                {
                    //Get sample properties
                    if (index < SampleParams.Length)
                    {
                        SampleParams[index] = int.Parse(flagsEntry.Value.ToString());
                        index++;
                    }
                }
                yield return SampleParams;
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

        private void LoadAudio(string AudioPath, EXSoundStream SoundToLoad, bool ConvertAudio)
        {
            //LoadData
            EXStreamSoundsFunctions.LoadAudioData(AudioPath, SoundToLoad, ConvertAudio, AudioLibrary);

            //Check Loaded Data
            if (SoundToLoad.PCM_Data == null)
            {
                Reports.Add("0Error reading wav file: " + AudioPath + ", seems that is being used by another process");
            }
        }
    }
}
