using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;

namespace EuroSound_Application.StreamSounds.YMLReader
{
    class StreamSoundsYMLReader
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
                Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect") + ": " + LevelSoundBankPath);
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
            uint SoundID;
            uint[] CurrentSoundParams;
            IEnumerable<uint[]> StartMarkers;
            IEnumerable<int[]> Markers;
            string WavDataPath, FileCheck;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_ReadingYamlFile") + ": " + SoundName);
            if (Reports == null)
            {
                Reports = new List<string>();
            }

            StreamReader reader = new StreamReader(FilePath);
            FileCheck = reader.ReadLine();
            if (FileCheck.Equals("#ftype:4", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    //Load the stream
                    YamlStream yaml = new YamlStream();
                    yaml.Load(reader);

                    //Examine the stream
                    YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

                    CurrentSoundParams = GetSoundParams(mapping);
                    StartMarkers = GetStartMarkers(mapping);
                    Markers = GetMarkers(mapping);
                    WavDataPath = GetSoundFilePath(mapping, FilePath);

                    EXSoundStream SoundToAdd = new EXSoundStream
                    {
                        BaseVolume = CurrentSoundParams[4],
                    };

                    //------------------READ WAV FILE----------------------
                    if (GenericFunctions.AudioIsValid(WavDataPath, 1, 22050))
                    {
                        LoadAudio(WavDataPath, SoundToAdd);
                    }
                    else
                    {
                        string FileTempFile = AudioLibrary.ConvertWavToSoundBankValid(WavDataPath, Path.GetFileNameWithoutExtension(WavDataPath), 22050, 1, 16);
                        if (!string.IsNullOrEmpty(FileTempFile))
                        {
                            LoadAudio(WavDataPath, SoundToAdd);
                        }
                        else
                        {
                            Reports.Add("1Can't load: " + WavDataPath + " format not valid");
                        }
                    }

                    foreach (uint[] StartMarkerData in StartMarkers)
                    {
                        EXStreamStartMarker StartMarker = new EXStreamStartMarker
                        {
                            Name = StartMarkerData[0],
                            Position = StartMarkerData[1],
                            MusicMakerType = StartMarkerData[2],
                            Flags = StartMarkerData[3],
                            Extra = StartMarkerData[4],
                            LoopStart = StartMarkerData[5],
                            MarkerCount = StartMarkerData[6],
                            LoopMarkerCount = StartMarkerData[7],
                            MarkerPos = StartMarkerData[8],
                            IsInstant = StartMarkerData[9],
                            InstantBuffer = StartMarkerData[10],
                            StateA = StartMarkerData[11],
                            StateB = StartMarkerData[12]
                        };

                        SoundToAdd.StartMarkers.Add(StartMarker);
                    }

                    foreach (int[] MarkerData in Markers)
                    {
                        EXStreamMarker Marker = new EXStreamMarker
                        {
                            Name = MarkerData[0],
                            Position = (uint)MarkerData[1],
                            MusicMakerType = (uint)MarkerData[2],
                            Flags = (uint)MarkerData[3],
                            Extra = (uint)MarkerData[4],
                            LoopStart = (uint)MarkerData[5],
                            MarkerCount = (uint)MarkerData[6],
                            LoopMarkerCount = (uint)MarkerData[7]
                        };

                        SoundToAdd.Markers.Add(Marker);
                    }

                    SoundID = GenericFunctions.GetNewObjectID(FileProperties);
                    SoundsList.Add(SoundID, SoundToAdd);

                    TreeNodeFunctions.TreeNodeAddNewNode("Sounds", SoundID.ToString(), SoundName, 2, 2, "Sound", Color.Black, TreeViewControl);
                }
                catch
                {
                    Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
                }
            }
            else
            {
                Reports.Add("1" + GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"));
            }

            reader.Close();
            reader.Dispose();

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private uint[] GetSoundParams(YamlMappingNode mapping)
        {
            uint[] Parameters = new uint[5];
            //Sound Parameters
            YamlMappingNode SoundParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("properties")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SoundParameters.Children)
            {
                if (entry.Key.ToString().Equals("StartMarkerCount"))
                {
                    Parameters[0] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("MarkerCount"))
                {
                    Parameters[1] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("StartMarkerOffset"))
                {
                    Parameters[2] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("MarkerOffset"))
                {
                    Parameters[3] = Convert.ToUInt32(entry.Value.ToString());
                }
                if (entry.Key.ToString().Equals("BaseVolume"))
                {
                    Parameters[4] = Convert.ToUInt32(entry.Value.ToString());
                }
            }

            return Parameters;
        }

        private string GetSoundFilePath(YamlMappingNode mapping, string FilePath)
        {
            string SoundFilePath = string.Empty;

            //Sound Parameters
            YamlMappingNode SoundParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("Data")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SoundParameters.Children)
            {
                if (entry.Key.ToString().Equals("FileName"))
                {
                    SoundFilePath = Path.GetDirectoryName(FilePath) + "\\" + entry.Value.ToString();
                }
            }

            return SoundFilePath;
        }

        private IEnumerable<uint[]> GetStartMarkers(YamlMappingNode mapping)
        {
            int SampleIndex;
            int index;

            YamlMappingNode SampleParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("StartMarkers")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SampleParameters.Children)
            {
                //Temporal array to store values
                uint[] SampleParams = new uint[13];

                //Get sample name
                SampleIndex = int.Parse(entry.Key.ToString());
                SampleParams[0] = (uint)SampleIndex;
                index = 1;

                YamlMappingNode SampleProps = (YamlMappingNode)SampleParameters.Children[new YamlScalarNode(SampleIndex.ToString())];
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
            int SampleIndex;
            int index;

            YamlMappingNode SampleParameters = (YamlMappingNode)mapping.Children[new YamlScalarNode("Markers")];
            foreach (KeyValuePair<YamlNode, YamlNode> entry in SampleParameters.Children)
            {
                //Temporal array to store values
                int[] SampleParams = new int[8];

                //Get sample name
                SampleIndex = int.Parse(entry.Key.ToString());
                index = 0;

                YamlMappingNode SampleProps = (YamlMappingNode)SampleParameters.Children[new YamlScalarNode(SampleIndex.ToString())];
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

        private void LoadAudio(string AudioPath, EXSoundStream SoundToLoad)
        {
            EngineXImaAdpcm.ImaADPCM_Functions ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Functions();

            SoundToLoad.WAVFileMD5 = GenericFunctions.CalculateMD5(AudioPath);
            SoundToLoad.WAVFileName = Path.GetFileName(AudioPath);

            try
            {
                using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
                {
                    SoundToLoad.Channels = (byte)AudioReader.WaveFormat.Channels;
                    SoundToLoad.Frequency = (uint)AudioReader.WaveFormat.SampleRate;
                    SoundToLoad.RealSize = (uint)new FileInfo(AudioPath).Length;
                    SoundToLoad.Bits = (uint)AudioReader.WaveFormat.BitsPerSample;
                    SoundToLoad.Encoding = AudioReader.WaveFormat.Encoding.ToString();
                    SoundToLoad.Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);

                    //Get PCM Data
                    SoundToLoad.PCM_Data = new byte[AudioReader.Length];
                    AudioReader.Read(SoundToLoad.PCM_Data, 0, (int)AudioReader.Length);
                    AudioReader.Close();

                    //Get IMA ADPCM Data
                    short[] ShortArrayPCMData = AudioLibrary.ConvertPCMDataToShortArray(SoundToLoad.PCM_Data);
                    SoundToLoad.IMA_ADPCM_DATA = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData, SoundToLoad.PCM_Data.Length / 2);
                }
            }
            catch (FormatException)
            {

            }

            if (SoundToLoad.PCM_Data == null)
            {
                Reports.Add("0Error reading wav file: " + AudioPath + ", seems that is being used by another process");
            }
        }
    }
}
