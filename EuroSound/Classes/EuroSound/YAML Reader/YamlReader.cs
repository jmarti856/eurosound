using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;

namespace EuroSound_SB_Editor
{
    public static class YamlReader
    {
        internal static ListView Reports;
        internal static void LoadDataFromSwyterUnpacker(List<EXSound> SoundsList, TreeView TreeViewControl, string FilePath)
        {
            Reports = new ListView();
            List<string> SoundsPaths;
            string SoundName, SoundHashcode;

            SoundsPaths = GetFilePaths(FilePath, FilePath);

            //Read sounds from the unpacker folder
            foreach (string path in SoundsPaths)
            {
                SoundName = new DirectoryInfo(Path.GetDirectoryName(path)).Name;
                SoundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, SoundName);
                EXFile.Hashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SB_Defines, Path.GetFileNameWithoutExtension(FilePath));
                if (string.IsNullOrEmpty(SoundHashcode))
                {
                    ListViewItem Message = new ListViewItem(new[] { "", "Hashcode not found for the sound " + SoundName });
                    Message.SubItems[0].BackColor = Color.Yellow;
                    Message.UseItemStyleForSubItems = false;
                    Reports.Items.Add(Message);
                }
                ReadYamlFile(SoundsList, TreeViewControl, path, SoundName, SoundHashcode);
            }

            //Expand only root nodes
            TreeViewControl.Nodes[0].Collapse();
            TreeViewControl.Nodes[0].Expand();

            TreeViewControl.Nodes[1].Collapse();
            TreeViewControl.Nodes[1].Expand();

            //Show Import results
            if (Reports.Items.Count > 0)
            {
                EuroSound_ImportResultsList ImportResults = new EuroSound_ImportResultsList(Reports)
                {
                    Text = Path.GetFileName(FilePath) + " Import results",
                    ShowInTaskbar = false
                };
                ImportResults.ShowDialog();
                ImportResults.Dispose();
            }
        }

        internal static List<string> GetFilePaths(string LevelSoundBankPath, string FilePath)
        {
            List<string> Paths = new List<string>();

            string[] lines = File.ReadAllLines(LevelSoundBankPath);
            for (var i = 0; i < lines.Length; i += 1)
            {
                if (!lines[i].StartsWith("#"))
                {
                    string[] line = lines[i].Split(null);
                    Paths.Add(Path.GetDirectoryName(FilePath) + "\\" + line[1] + "\\effectProperties.yml");
                }
            }

            return Paths;
        }

        internal static void ReadYamlFile(List<EXSound> SoundsList, TreeView TreeViewControl, string FilePath, string SoundName, string SoundHashcode)
        {
            int[] SndParams = new int[12];
            bool[] SndFlags = new bool[12];
            Dictionary<int, int[]> Samples = new Dictionary<int, int[]>();

            bool readingFlags = false;
            bool IsStreamed = false;
            int index = 0;
            string AudioFilePath;

            using (StreamReader reader = new StreamReader(FilePath))
            {
                // Load the stream
                YamlStream yaml = new YamlStream();
                yaml.Load(reader);

                // Examine the stream
                YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

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

                //Samples Properties
                int SampleIndex;
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
                    //Check if is a streamed sound
                    if (SampleParams[0] < 0)
                    {
                        IsStreamed = true;
                    }
                    Samples.Add(SampleIndex, SampleParams);
                }
            }

            //Add Sound
            if (IsStreamed)
            {

                TreeNodeFunctions.TreeNodeAddNewNode("StreamedSounds", SoundName, 2, 2, "Sound", Color.Black, TreeViewControl);
            }
            else
            {
                TreeNodeFunctions.TreeNodeAddNewNode("Sounds", SoundName, 2, 2, "Sound", Color.Black, TreeViewControl);
            }
            EXObjectsFunctions.AddNewSound(SoundName, SoundName, SoundHashcode, SndParams, SoundsList);
            EXSound Sound = EXObjectsFunctions.GetSoundByName(SoundName, SoundsList);

            //Add sample
            foreach (KeyValuePair<int, int[]> entry in Samples)
            {
                string SampleName = SoundName + entry.Key;
                int[] SampleValues = entry.Value;

                TreeNodeFunctions.TreeNodeAddNewNode(SoundName, SampleName, 4, 4, "Sample", Color.Black, TreeViewControl);
                if (IsStreamed)
                {
                    EXObjectsFunctions.AddSampleToSound(Sound, SampleName, SampleValues, true);
                }
                else
                {
                    EXObjectsFunctions.AddSampleToSound(Sound, SampleName, SampleValues, false);
                    foreach (EXSample Sample in Sound.Samples)
                    {
                        if (Sample.Name.Equals(SampleName.ToString()))
                        {
                            AudioFilePath = GetAudioFilePath(FilePath, entry.Key);
                            if (File.Exists(AudioFilePath))
                            {
                                WaveFileReader AudioReader = new WaveFileReader(AudioFilePath);

                                /*Read Audio Data*/
                                Sample.Audio.Name = Path.GetFileName(AudioFilePath);
                                Sample.Audio.DataSize = Convert.ToInt32(AudioReader.Length);
                                Sample.Audio.Frequency = AudioReader.WaveFormat.SampleRate;
                                Sample.Audio.RealSize = Convert.ToInt32(new FileInfo(AudioFilePath).Length);
                                Sample.Audio.Channels = AudioReader.WaveFormat.Channels;
                                Sample.Audio.Bits = AudioReader.WaveFormat.BitsPerSample;
                                Sample.Audio.Duration = Convert.ToInt32(Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1));
                                Sample.Audio.AllData = File.ReadAllBytes(AudioFilePath);
                                Sample.Audio.Encoding = AudioReader.WaveFormat.Encoding.ToString();

                                /*Close reader*/
                                AudioReader.Close();
                                AudioReader.Dispose();
                                AudioReader.Flush();


                                Sample.Audio.PCMdata = EXObjectsFunctions.GetRawPCMData(AudioFilePath);
                            }
                            else
                            {
                                //ADD A FORM WITH THE IMPORT RESULTS.
                                ListViewItem Message = new ListViewItem(new[] { "", "Sample (" + entry.Key + ".wav" + ") not found for the sound: " + SoundName });
                                Message.SubItems[0].BackColor = Color.Red;
                                Message.UseItemStyleForSubItems = false;
                                Reports.Items.Add(Message);
                            }
                        }
                    }
                }
            }
        }

        private static string GetAudioFilePath(string EffectPropertiesPath, int SampleName)
        {
            string alphabet_string = "abcdefghijklmnopqrstuvwxyz";
            string AudioPath;

            AudioPath = Path.GetDirectoryName(EffectPropertiesPath) + "//" + alphabet_string[SampleName] + ".wav";

            return AudioPath;
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
    }
}
