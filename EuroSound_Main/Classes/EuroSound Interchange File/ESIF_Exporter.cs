using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundInterchangeFile
{
    internal class ESIF_Exporter
    {
        AudioFunctions AudioF = new AudioFunctions();
        List<string> AudiosAssocTable = new List<string>();

        internal void ExportSingleSFX(string FilePath, uint SoundKey, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string AudioPath;
            string MediaFolder = GenericFunctions.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                EXSound SFXObject = EXSoundbanksFunctions.ReturnSoundFromDictionary(SoundKey, SoundsList);

                TextFileWriter.WriteLine("*EUROSOUND_INTERCHANGE_FILE V1.0");
                TextFileWriter.WriteLine("*COMMENT " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                TextFileWriter.WriteLine("");
                TextFileWriter.WriteLine("*AUDIODATA {");
                foreach (KeyValuePair<uint, EXSample> SFXObjectSample in SFXObject.Samples)
                {
                    if (!SFXObjectSample.Value.IsStreamed && !EXSoundbanksFunctions.SubSFXFlagChecked(SFXObject.Flags))
                    {
                        EXAudio SampleAudio = AudiosList[SFXObjectSample.Value.ComboboxSelectedAudio];
                        TreeNode AudioNode = TreeViewControl.Nodes.Find(SFXObjectSample.Value.ComboboxSelectedAudio, true)[0];

                        AudioPath = Path.Combine(MediaFolder, AudioNode.Text + ".wav");

                        //Export Audio if Not Exists
                        if (!File.Exists(AudioPath))
                        {
                            AudioF.CreateWavFile(22050, 16, 1, SampleAudio.PCMdata, AudioPath);
                        }

                        //Add "FileRef to the list"
                        AudiosAssocTable.Add(SFXObjectSample.Value.ComboboxSelectedAudio);

                        //Write Data
                        TextFileWriter.WriteLine("\t*AUDIO " + AudiosAssocTable.IndexOf(SFXObjectSample.Value.ComboboxSelectedAudio) + " {");
                        TextFileWriter.WriteLine("\t\t*PATH \"{0}\"", AudioPath);
                        TextFileWriter.WriteLine("\t\t*NODECOLOR {0} {1} {2}", AudioNode.ForeColor.R, AudioNode.ForeColor.G, AudioNode.ForeColor.B);
                        TextFileWriter.WriteLine("\t\t*FLAGS {0}", SampleAudio.Flags);
                        TextFileWriter.WriteLine("\t\t*LOOPOFFSET {0}", SampleAudio.LoopOffset);
                        TextFileWriter.WriteLine("\t\t*PSI {0}", SampleAudio.PSIsample);
                        TextFileWriter.WriteLine("\t\t*CHANNELS {0}", SampleAudio.Channels);
                        TextFileWriter.WriteLine("\t\t*FREQUENCY {0}", SampleAudio.Frequency);
                        TextFileWriter.WriteLine("\t}");
                    }
                }
                TreeNode SoundNode = TreeViewControl.Nodes.Find(SoundKey.ToString(), true)[0];
                TextFileWriter.WriteLine("}");
                TextFileWriter.WriteLine("");
                TextFileWriter.WriteLine("*SFXSOUND {");
                TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", SoundNode.Text);
                TextFileWriter.WriteLine("\t*HASHCODE " + "0x" + SFXObject.Hashcode.ToString("X8"));
                TextFileWriter.WriteLine("\t*NODECOLOR {0} {1} {2}", SoundNode.ForeColor.R, SoundNode.ForeColor.G, SoundNode.ForeColor.B);
                TextFileWriter.WriteLine("\t*NUMSAMPLES " + SFXObject.Samples.Count);
                TextFileWriter.WriteLine("\t*PARAMETERS {");
                TextFileWriter.WriteLine("\t\t*DUCKERLENGTH " + SFXObject.DuckerLenght);
                TextFileWriter.WriteLine("\t\t*MINDELAY " + SFXObject.MinDelay);
                TextFileWriter.WriteLine("\t\t*MAXDELAY " + SFXObject.MaxDelay);
                TextFileWriter.WriteLine("\t\t*REVERBSEND " + SFXObject.ReverbSend);
                TextFileWriter.WriteLine("\t\t*TRACKINGTYPE " + SFXObject.TrackingType);
                TextFileWriter.WriteLine("\t\t*MAXVOICES " + SFXObject.MaxVoices);
                TextFileWriter.WriteLine("\t\t*PRIORITY " + SFXObject.Priority);
                TextFileWriter.WriteLine("\t\t*DUCKER " + SFXObject.Ducker);
                TextFileWriter.WriteLine("\t\t*MASTERVOLUME " + SFXObject.MasterVolume);
                TextFileWriter.WriteLine("\t\t*FLAGS " + SFXObject.Flags);
                TextFileWriter.WriteLine("\t}");
                TextFileWriter.WriteLine("\t*SAMPLES {");
                foreach (KeyValuePair<uint, EXSample> SFXObjectSample in SFXObject.Samples)
                {
                    TreeNode SampleNode = TreeViewControl.Nodes.Find(SFXObjectSample.Key.ToString(), true)[0];
                    TextFileWriter.WriteLine("\t\t*SAMPLE {");
                    TextFileWriter.WriteLine("\t\t\t*NODENAME \"{0}\"", SampleNode.Text);
                    TextFileWriter.WriteLine("\t\t\t*NODECOLOR {0} {1} {2}", SampleNode.ForeColor.R, SampleNode.ForeColor.G, SampleNode.ForeColor.B);
                    TextFileWriter.WriteLine("\t\t\t*PITCHOFFSET " + SFXObjectSample.Value.PitchOffset);
                    TextFileWriter.WriteLine("\t\t\t*BASEVOLUME " + SFXObjectSample.Value.BaseVolume);
                    TextFileWriter.WriteLine("\t\t\t*PAN " + SFXObjectSample.Value.Pan);
                    TextFileWriter.WriteLine("\t\t\t*RANDOMPITCHOFFSET " + SFXObjectSample.Value.RandomPitchOffset);
                    TextFileWriter.WriteLine("\t\t\t*RANDOMVOLUMEOFFSET " + SFXObjectSample.Value.RandomVolumeOffset);
                    TextFileWriter.WriteLine("\t\t\t*RANDOMPAN " + SFXObjectSample.Value.RandomPan);

                    if (EXSoundbanksFunctions.SubSFXFlagChecked(SFXObject.Flags))
                    {
                        TextFileWriter.WriteLine("\t\t\t*FILEREF " + (SFXObjectSample.Value.HashcodeSubSFX - 0x1A000000));
                    }
                    else if (SFXObjectSample.Value.IsStreamed)
                    {
                        TextFileWriter.WriteLine("\t\t\t*FILEREF " + SFXObjectSample.Value.FileRef);
                    }
                    else
                    {
                        TextFileWriter.WriteLine("\t\t\t*FILEREF " + AudiosAssocTable.IndexOf(SFXObjectSample.Value.ComboboxSelectedAudio));
                    }
                    TextFileWriter.WriteLine("\t\t}");
                }
                TextFileWriter.WriteLine("\t}");
                TextFileWriter.WriteLine("}");
                TextFileWriter.Close();
                AudiosAssocTable.Clear();
            }
        }

        internal void ExportProject(string FilePath, bool IncludeProjectSettings, ProjectFile ProjectSettings, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            int AudioIndex = 0;
            string SndHash, AudioPath;
            string MediaFolder = GenericFunctions.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                TextFileWriter.WriteLine("*EUROSOUND_INTERCHANGE_FILE V1.0");
                TextFileWriter.WriteLine("*COMMENT " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                TextFileWriter.WriteLine("");

                if (IncludeProjectSettings)
                {
                    SndHash = Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, ProjectSettings.Hashcode);
                    TextFileWriter.WriteLine("*PROJECTSETTINGS {");
                    TextFileWriter.WriteLine("\t*FILENAME \"{0}\"", SndHash);
                    TextFileWriter.WriteLine("\t*HASHCODE {0}", "0x" + ProjectSettings.Hashcode.ToString("X8"));
                    TextFileWriter.WriteLine("\t*SFXOBJECTS {0}", SoundsList.Count);
                    TextFileWriter.WriteLine("\t*NUMSAMPLES {0}", AudiosList.Count);
                    TextFileWriter.WriteLine("}");
                }
                TextFileWriter.WriteLine("");
                TextFileWriter.WriteLine("*AUDIODATA {");
                foreach (KeyValuePair<string, EXAudio> SampleAudio in AudiosList)
                {
                    TreeNode AudioNode = TreeViewControl.Nodes.Find(SampleAudio.Key, true)[0];
                    AudioPath = Path.Combine(MediaFolder, AudioNode.Text + ".wav");

                    //Export Audio if Not Exists
                    if (!File.Exists(AudioPath))
                    {
                        AudioF.CreateWavFile(22050, 16, 1, SampleAudio.Value.PCMdata, AudioPath);
                    }

                    TextFileWriter.WriteLine("\t*AUDIO " + AudioIndex + " {");
                    //Write Data
                    TextFileWriter.WriteLine("\t\t*PATH \"{0}\"", AudioPath);
                    TextFileWriter.WriteLine("\t\t*NODECOLOR {0} {1} {2}", AudioNode.ForeColor.R, AudioNode.ForeColor.G, AudioNode.ForeColor.B);
                    TextFileWriter.WriteLine("\t\t*FLAGS {0}", SampleAudio.Value.Flags);
                    TextFileWriter.WriteLine("\t\t*LOOPOFFSET {0}", SampleAudio.Value.LoopOffset);
                    TextFileWriter.WriteLine("\t\t*PSI {0}", SampleAudio.Value.PSIsample);
                    TextFileWriter.WriteLine("\t\t*CHANNELS {0}", SampleAudio.Value.Channels);
                    TextFileWriter.WriteLine("\t\t*FREQUENCY {0}", SampleAudio.Value.Frequency);
                    TextFileWriter.WriteLine("\t}");

                    //Add Item to Assoc table
                    AudiosAssocTable.Add(SampleAudio.Key);

                    //Update counter
                    AudioIndex++;
                }
                TextFileWriter.WriteLine("}");
                TextFileWriter.WriteLine("");

                foreach (KeyValuePair<uint, EXSound> SFXObject in SoundsList)
                {
                    TreeNode SoundNode = TreeViewControl.Nodes.Find(SFXObject.Key.ToString(), true)[0];
                    TextFileWriter.WriteLine("*SFXSOUND {");
                    TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", SoundNode.Text);
                    TextFileWriter.WriteLine("\t*HASHCODE " + "0x" + SFXObject.Value.Hashcode.ToString("X8"));
                    TextFileWriter.WriteLine("\t*NODECOLOR {0} {1} {2}", SoundNode.ForeColor.R, SoundNode.ForeColor.G, SoundNode.ForeColor.B);
                    TextFileWriter.WriteLine("\t*NUMSAMPLES " + SFXObject.Value.Samples.Count);
                    TextFileWriter.WriteLine("\t*PARAMETERS {");
                    TextFileWriter.WriteLine("\t\t*DUCKERLENGTH " + SFXObject.Value.DuckerLenght);
                    TextFileWriter.WriteLine("\t\t*MINDELAY " + SFXObject.Value.MinDelay);
                    TextFileWriter.WriteLine("\t\t*MAXDELAY " + SFXObject.Value.MaxDelay);
                    TextFileWriter.WriteLine("\t\t*REVERBSEND " + SFXObject.Value.ReverbSend);
                    TextFileWriter.WriteLine("\t\t*TRACKINGTYPE " + SFXObject.Value.TrackingType);
                    TextFileWriter.WriteLine("\t\t*MAXVOICES " + SFXObject.Value.MaxVoices);
                    TextFileWriter.WriteLine("\t\t*PRIORITY " + SFXObject.Value.Priority);
                    TextFileWriter.WriteLine("\t\t*DUCKER " + SFXObject.Value.Ducker);
                    TextFileWriter.WriteLine("\t\t*MASTERVOLUME " + SFXObject.Value.MasterVolume);
                    TextFileWriter.WriteLine("\t\t*FLAGS " + SFXObject.Value.Flags);
                    TextFileWriter.WriteLine("\t}");
                    TextFileWriter.WriteLine("\t*SAMPLES {");
                    foreach (KeyValuePair<uint, EXSample> SFXObjectSample in SFXObject.Value.Samples)
                    {
                        TreeNode SampleNode = TreeViewControl.Nodes.Find(SFXObjectSample.Key.ToString(), true)[0];
                        TextFileWriter.WriteLine("\t\t*SAMPLE {");
                        TextFileWriter.WriteLine("\t\t\t*NODENAME \"{0}\"", SampleNode.Text);
                        TextFileWriter.WriteLine("\t\t\t*NODECOLOR {0} {1} {2}", SampleNode.ForeColor.R, SampleNode.ForeColor.G, SampleNode.ForeColor.B);
                        TextFileWriter.WriteLine("\t\t\t*PITCHOFFSET " + SFXObjectSample.Value.PitchOffset);
                        TextFileWriter.WriteLine("\t\t\t*BASEVOLUME " + SFXObjectSample.Value.BaseVolume);
                        TextFileWriter.WriteLine("\t\t\t*PAN " + SFXObjectSample.Value.Pan);
                        TextFileWriter.WriteLine("\t\t\t*RANDOMPITCHOFFSET " + SFXObjectSample.Value.RandomPitchOffset);
                        TextFileWriter.WriteLine("\t\t\t*RANDOMVOLUMEOFFSET " + SFXObjectSample.Value.RandomVolumeOffset);
                        TextFileWriter.WriteLine("\t\t\t*RANDOMPAN " + SFXObjectSample.Value.RandomPan);

                        if (EXSoundbanksFunctions.SubSFXFlagChecked(SFXObject.Value.Flags))
                        {
                            TextFileWriter.WriteLine("\t\t\t*FILEREF " + (SFXObjectSample.Value.HashcodeSubSFX - 0x1A000000));
                        }
                        else if (SFXObjectSample.Value.IsStreamed)
                        {
                            TextFileWriter.WriteLine("\t\t\t*FILEREF " + SFXObjectSample.Value.FileRef);
                        }
                        else
                        {
                            TextFileWriter.WriteLine("\t\t\t*FILEREF " + AudiosAssocTable.IndexOf(SFXObjectSample.Value.ComboboxSelectedAudio));
                        }
                        TextFileWriter.WriteLine("\t\t}");
                    }
                    TextFileWriter.WriteLine("\t}");
                    TextFileWriter.WriteLine("}");
                }

                TextFileWriter.Close();
            }
        }

        internal void ExportFolder(IList<TreeNode> ListOfNodes, string FilePath, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            Dictionary<uint, EXSound> FinalSoundsList = new Dictionary<uint, EXSound>();
            foreach (TreeNode ChildNode in ListOfNodes)
            {
                if (ChildNode.Tag.ToString().Equals("Sound"))
                {
                    FinalSoundsList.Add(uint.Parse(ChildNode.Name), SoundsList[uint.Parse(ChildNode.Name)]);
                }
            }
            ExportProject(FilePath, false, null, FinalSoundsList, GetUsedAudios(FinalSoundsList, AudiosList), TreeViewControl);
            FinalSoundsList.Clear();
        }

        private Dictionary<string, EXAudio> GetUsedAudios(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList)
        {
            Dictionary<string, EXAudio> UsedAudios = new Dictionary<string, EXAudio>();
            string AudioKey;

            foreach (KeyValuePair<uint, EXSound> SFXObject in SoundsList)
            {
                if (SFXObject.Value.OutputThisSound)
                {
                    foreach (KeyValuePair<uint, EXSample> SFXObjectSample in SFXObject.Value.Samples)
                    {
                        if (!SFXObjectSample.Value.IsStreamed && !EXSoundbanksFunctions.SubSFXFlagChecked(SFXObject.Value.Flags))
                        {
                            AudioKey = SFXObjectSample.Value.ComboboxSelectedAudio;

                            if (!UsedAudios.ContainsKey(AudioKey))
                            {
                                UsedAudios.Add(AudioKey, AudiosList[AudioKey]);
                            }
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            return UsedAudios;
        }
    }
}
