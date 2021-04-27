using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundInterchangeFile
{
    internal class ESIF_Exporter
    {
        private AudioFunctions AudioF = new AudioFunctions();
        private List<string> AudiosAssocTable = new List<string>();

        //*===============================================================================================
        //* SFX Soundbank
        //*===============================================================================================
        internal void ExportSingleSFX(string FilePath, uint SoundKey, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            string AudioPath;
            string MediaFolder = BrowsersAndDialogs.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                EXSound SFXObject = EXSoundbanksFunctions.ReturnSoundFromDictionary(SoundKey, SoundsList);
                TreeNode SoundNode = TreeViewControl.Nodes.Find(SoundKey.ToString(), true)[0];

                //Header
                WriteFileHeader(TextFileWriter);

                //Write Audio Data
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
                            AudioF.CreateWavFile(GlobalPreferences.SoundbankFrequency, GlobalPreferences.SoundbankBits, GlobalPreferences.SoundbankChannels, SampleAudio.PCMdata, AudioPath);
                        }

                        //Add "FileRef to the list"
                        AudiosAssocTable.Add(SFXObjectSample.Value.ComboboxSelectedAudio);

                        //Write Data
                        TextFileWriter.WriteLine("\t*AUDIO " + AudiosAssocTable.IndexOf(SFXObjectSample.Value.ComboboxSelectedAudio) + " {");
                        TextFileWriter.WriteLine("\t\t*PATH \"{0}\"", AudioPath);
                        TextFileWriter.WriteLine("\t\t*NODECOLOR {0} {1} {2}", AudioNode.ForeColor.R, AudioNode.ForeColor.G, AudioNode.ForeColor.B);
                        TextFileWriter.WriteLine("\t\t*FLAGS {0}", SampleAudio.Flags);
                        TextFileWriter.WriteLine("\t\t*LOOPOFFSET {0}", SampleAudio.LoopOffset * 2);
                        TextFileWriter.WriteLine("\t\t*PSI {0}", SampleAudio.PSIsample);
                        TextFileWriter.WriteLine("\t\t*CHANNELS {0}", SampleAudio.Channels);
                        TextFileWriter.WriteLine("\t\t*FREQUENCY {0}", SampleAudio.Frequency);
                        TextFileWriter.WriteLine("\t}");
                    }
                }
                TextFileWriter.WriteLine("}");
                TextFileWriter.WriteLine("");
                TextFileWriter.WriteLine("*SFXSOUND {");
                TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", SoundNode.Text);
                TextFileWriter.WriteLine("\t*FOLDERNAME \"{0}\"", SoundNode.Parent.Text);
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
            string AudioPath;
            string MediaFolder = BrowsersAndDialogs.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                //Header
                WriteFileHeader(TextFileWriter);

                if (IncludeProjectSettings)
                {
                    TextFileWriter.WriteLine("*PROJECTSETTINGS {");
                    TextFileWriter.WriteLine("\t*FILENAME \"{0}\"", ProjectSettings.FileName);
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
                        AudioF.CreateWavFile(GlobalPreferences.SoundbankFrequency, GlobalPreferences.SoundbankBits, GlobalPreferences.SoundbankChannels, SampleAudio.Value.PCMdata, AudioPath);
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
                    TextFileWriter.WriteLine("\t*FOLDERNAME \"{0}\"", SoundNode.Parent.Text);
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
                                if (AudiosList.ContainsKey(AudioKey))
                                {
                                    UsedAudios.Add(AudioKey, AudiosList[AudioKey]);
                                }
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

        //*===============================================================================================
        //* Stream Soundbank
        //*===============================================================================================
        internal void ExportStreamSoundbank(string FilePath, uint SoundKey, Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl)
        {
            string AudioPath;
            string MediaFolder = BrowsersAndDialogs.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                EXSoundStream StreamObject = SoundsList[SoundKey];
                TreeNode SoundNode = TreeViewControl.Nodes.Find(SoundKey.ToString(), true)[0];

                //Audio Path
                AudioPath = Path.Combine(MediaFolder, Path.GetFileNameWithoutExtension(StreamObject.WAVFileName) + ".wav");

                //Export Audio if Not Exists
                if (!File.Exists(AudioPath))
                {
                    AudioF.CreateWavFile(GlobalPreferences.StreambankFrequency, GlobalPreferences.StreambankBits, GlobalPreferences.StreambankChannels, StreamObject.PCM_Data, AudioPath);
                }

                //Header
                WriteFileHeader(TextFileWriter);

                //Write Object Data
                TextFileWriter.WriteLine("*STREAMSOUND {");
                TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", SoundNode.Text);
                TextFileWriter.WriteLine("\t*FOLDERNAME \"{0}\"", SoundNode.Parent.Text);
                TextFileWriter.WriteLine("\t*NODECOLOR {0} {1} {2}", SoundNode.ForeColor.R, SoundNode.ForeColor.G, SoundNode.ForeColor.B);
                TextFileWriter.WriteLine("\t*FILEPATH \"{0}\"", AudioPath);
                TextFileWriter.WriteLine("\t*BASEVOLUME {0}", StreamObject.BaseVolume);
                TextFileWriter.WriteLine("\t*NUMSTARTMARKERS {0}", StreamObject.StartMarkers.Count);
                TextFileWriter.WriteLine("\t*NUMMARKERS {0}", StreamObject.Markers.Count);

                //Write Start Markers
                TextFileWriter.WriteLine("\t*STARTMARKERS {");
                foreach (EXStreamStartMarker StrtMarker in StreamObject.StartMarkers)
                {
                    TextFileWriter.WriteLine("\t\t*STARTMARKER {");
                    TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrtMarker.Name);
                    TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrtMarker.Position);
                    TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrtMarker.MusicMakerType);
                    TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrtMarker.Flags);
                    TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrtMarker.Extra);
                    TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrtMarker.LoopStart);
                    TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrtMarker.MarkerCount);
                    TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrtMarker.LoopMarkerCount);
                    TextFileWriter.WriteLine("\t\t\t*MARKERPOS {0}", StrtMarker.MarkerPos);
                    TextFileWriter.WriteLine("\t\t\t*ISINSTANT {0}", StrtMarker.IsInstant);
                    TextFileWriter.WriteLine("\t\t\t*INSTANTBUFFER {0}", StrtMarker.InstantBuffer);
                    TextFileWriter.WriteLine("\t\t\t*STATEA {0}", StrtMarker.StateA);
                    TextFileWriter.WriteLine("\t\t\t*STATEB {0}", StrtMarker.StateB);
                    TextFileWriter.WriteLine("\t\t}");
                }
                TextFileWriter.WriteLine("\t}");

                //Write Markers 
                TextFileWriter.WriteLine("\t*MARKERS {");
                foreach (EXStreamMarker StrmMarker in StreamObject.Markers)
                {
                    TextFileWriter.WriteLine("\t\t*MARKER {");
                    TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrmMarker.Name);
                    TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrmMarker.Position);
                    TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrmMarker.MusicMakerType);
                    TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrmMarker.Flags);
                    TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrmMarker.Extra);
                    TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrmMarker.LoopStart);
                    TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrmMarker.MarkerCount);
                    TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrmMarker.LoopMarkerCount);
                    TextFileWriter.WriteLine("\t\t}");
                }
                TextFileWriter.WriteLine("\t}");
                TextFileWriter.WriteLine("}");
                TextFileWriter.Close();
            }
        }

        internal void ExportProjectStream(string FilePath, ProjectFile ProjectSettings, Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl)
        {
            string AudioPath;
            string MediaFolder = BrowsersAndDialogs.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                //Header
                WriteFileHeader(TextFileWriter);

                TextFileWriter.WriteLine("*PROJECTSETTINGS {");
                TextFileWriter.WriteLine("\t*FILENAME \"{0}\"", ProjectSettings.FileName);
                TextFileWriter.WriteLine("\t*STREAMOBJECTS {0}", SoundsList.Count);
                TextFileWriter.WriteLine("}");
                TextFileWriter.WriteLine("");

                foreach (KeyValuePair<uint, EXSoundStream> StreamFileObject in SoundsList)
                {
                    TreeNode SoundNode = TreeViewControl.Nodes.Find(StreamFileObject.Key.ToString(), true)[0];

                    //Audio Path
                    AudioPath = Path.Combine(MediaFolder, Path.GetFileNameWithoutExtension(StreamFileObject.Value.WAVFileName) + ".wav");

                    //Export Audio if Not Exists
                    if (!File.Exists(AudioPath))
                    {
                        AudioF.CreateWavFile(GlobalPreferences.StreambankFrequency, GlobalPreferences.StreambankBits, GlobalPreferences.StreambankChannels, StreamFileObject.Value.PCM_Data, AudioPath);
                    }

                    //Write Object Data
                    TextFileWriter.WriteLine("*STREAMSOUND {");
                    TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", SoundNode.Text);
                    TextFileWriter.WriteLine("\t*FOLDERNAME \"{0}\"", SoundNode.Parent.Text);
                    TextFileWriter.WriteLine("\t*NODECOLOR {0} {1} {2}", SoundNode.ForeColor.R, SoundNode.ForeColor.G, SoundNode.ForeColor.B);
                    TextFileWriter.WriteLine("\t*FILEPATH \"{0}\"", AudioPath);
                    TextFileWriter.WriteLine("\t*BASEVOLUME {0}", StreamFileObject.Value.BaseVolume);
                    TextFileWriter.WriteLine("\t*NUMSTARTMARKERS {0}", StreamFileObject.Value.StartMarkers.Count);
                    TextFileWriter.WriteLine("\t*NUMMARKERS {0}", StreamFileObject.Value.Markers.Count);

                    //Write Start Markers
                    TextFileWriter.WriteLine("\t*STARTMARKERS {");
                    foreach (EXStreamStartMarker StrtMarker in StreamFileObject.Value.StartMarkers)
                    {
                        TextFileWriter.WriteLine("\t\t*STARTMARKER {");
                        TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrtMarker.Name);
                        TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrtMarker.Position);
                        TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrtMarker.MusicMakerType);
                        TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrtMarker.Flags);
                        TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrtMarker.Extra);
                        TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrtMarker.LoopStart);
                        TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrtMarker.MarkerCount);
                        TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrtMarker.LoopMarkerCount);
                        TextFileWriter.WriteLine("\t\t\t*MARKERPOS {0}", StrtMarker.MarkerPos);
                        TextFileWriter.WriteLine("\t\t\t*ISINSTANT {0}", StrtMarker.IsInstant);
                        TextFileWriter.WriteLine("\t\t\t*INSTANTBUFFER {0}", StrtMarker.InstantBuffer);
                        TextFileWriter.WriteLine("\t\t\t*STATEA {0}", StrtMarker.StateA);
                        TextFileWriter.WriteLine("\t\t\t*STATEB {0}", StrtMarker.StateB);
                        TextFileWriter.WriteLine("\t\t}");
                    }
                    TextFileWriter.WriteLine("\t}");

                    //Write Markers 
                    TextFileWriter.WriteLine("\t*MARKERS {");
                    foreach (EXStreamMarker StrmMarker in StreamFileObject.Value.Markers)
                    {
                        TextFileWriter.WriteLine("\t\t*MARKER {");
                        TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrmMarker.Name);
                        TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrmMarker.Position);
                        TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrmMarker.MusicMakerType);
                        TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrmMarker.Flags);
                        TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrmMarker.Extra);
                        TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrmMarker.LoopStart);
                        TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrmMarker.MarkerCount);
                        TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrmMarker.LoopMarkerCount);
                        TextFileWriter.WriteLine("\t\t}");
                    }
                    TextFileWriter.WriteLine("\t}");
                    TextFileWriter.WriteLine("}");
                }
                TextFileWriter.Close();
            }
        }
        //*===============================================================================================
        //* Music Bank
        //*===============================================================================================
        internal void ExportMusicBank(string FilePath, uint MusicKey, Dictionary<uint, EXMusic> MusicList, TreeView TreeViewControl)
        {
            string AudioPathLeft, AudioPathRight;
            string MediaFolder = BrowsersAndDialogs.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                EXMusic MusicObject = MusicList[MusicKey];
                TreeNode MusicNode = TreeViewControl.Nodes.Find(MusicKey.ToString(), true)[0];

                //Audio Path
                AudioPathLeft = Path.Combine(MediaFolder, Path.GetFileNameWithoutExtension(MusicObject.WAVFileName_LeftChannel) + ".wav");
                AudioPathRight = Path.Combine(MediaFolder, Path.GetFileNameWithoutExtension(MusicObject.WAVFileName_RightChannel) + ".wav");

                //Export Audio if Not Exists
                if (!File.Exists(AudioPathLeft))
                {
                    AudioF.CreateWavFile(GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels, MusicObject.PCM_Data_LeftChannel, AudioPathLeft);
                }
                if (!File.Exists(AudioPathRight))
                {
                    AudioF.CreateWavFile(GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels, MusicObject.PCM_Data_RightChannel, AudioPathRight);
                }

                //Header
                WriteFileHeader(TextFileWriter);

                //Write Object Data
                TextFileWriter.WriteLine("*MUSIC {");
                TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", MusicNode.Text);
                TextFileWriter.WriteLine("\t*FOLDERNAME \"{0}\"", MusicNode.Parent.Text);
                TextFileWriter.WriteLine("\t*NODECOLOR {0} {1} {2}", MusicNode.ForeColor.R, MusicNode.ForeColor.G, MusicNode.ForeColor.B);
                TextFileWriter.WriteLine("\t*FILEPATH_LEFT \"{0}\"", AudioPathLeft);
                TextFileWriter.WriteLine("\t*FILEPATH_RIGHT \"{0}\"", AudioPathRight);
                TextFileWriter.WriteLine("\t*BASEVOLUME {0}", MusicObject.BaseVolume);
                TextFileWriter.WriteLine("\t*NUMSTARTMARKERS {0}", MusicObject.StartMarkers.Count);
                TextFileWriter.WriteLine("\t*NUMMARKERS {0}", MusicObject.Markers.Count);

                //Write Start Markers
                TextFileWriter.WriteLine("\t*STARTMARKERS {");
                foreach (EXStreamStartMarker StrtMarker in MusicObject.StartMarkers)
                {
                    TextFileWriter.WriteLine("\t\t*STARTMARKER {");
                    TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrtMarker.Name);
                    TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrtMarker.Position);
                    TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrtMarker.MusicMakerType);
                    TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrtMarker.Flags);
                    TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrtMarker.Extra);
                    TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrtMarker.LoopStart);
                    TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrtMarker.MarkerCount);
                    TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrtMarker.LoopMarkerCount);
                    TextFileWriter.WriteLine("\t\t\t*MARKERPOS {0}", StrtMarker.MarkerPos);
                    TextFileWriter.WriteLine("\t\t\t*ISINSTANT {0}", StrtMarker.IsInstant);
                    TextFileWriter.WriteLine("\t\t\t*INSTANTBUFFER {0}", StrtMarker.InstantBuffer);
                    TextFileWriter.WriteLine("\t\t\t*STATEA {0}", StrtMarker.StateA);
                    TextFileWriter.WriteLine("\t\t\t*STATEB {0}", StrtMarker.StateB);
                    TextFileWriter.WriteLine("\t\t}");
                }
                TextFileWriter.WriteLine("\t}");

                //Write Markers 
                TextFileWriter.WriteLine("\t*MARKERS {");
                foreach (EXStreamMarker StrmMarker in MusicObject.Markers)
                {
                    TextFileWriter.WriteLine("\t\t*MARKER {");
                    TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrmMarker.Name);
                    TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrmMarker.Position);
                    TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrmMarker.MusicMakerType);
                    TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrmMarker.Flags);
                    TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrmMarker.Extra);
                    TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrmMarker.LoopStart);
                    TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrmMarker.MarkerCount);
                    TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrmMarker.LoopMarkerCount);
                    TextFileWriter.WriteLine("\t\t}");
                }
                TextFileWriter.WriteLine("\t}");
                TextFileWriter.WriteLine("}");
                TextFileWriter.Close();
            }
        }

        internal void ExportProjectMusic(string FilePath, ProjectFile ProjectSettings, Dictionary<uint, EXMusic> MusicList, TreeView TreeViewControl)
        {
            string AudioPathLeft, AudioPathRight;
            string MediaFolder = BrowsersAndDialogs.OpenFolderBrowser();

            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                //Header
                WriteFileHeader(TextFileWriter);

                TextFileWriter.WriteLine("*PROJECTSETTINGS {");
                TextFileWriter.WriteLine("\t*FILENAME \"{0}\"", ProjectSettings.FileName);
                TextFileWriter.WriteLine("\t*HASHCODE {0}", "0x" + ProjectSettings.Hashcode.ToString("X8"));
                TextFileWriter.WriteLine("\t*MUSICOBJECTS {0}", MusicList.Count);
                TextFileWriter.WriteLine("}");
                TextFileWriter.WriteLine("");

                foreach (KeyValuePair<uint, EXMusic> StreamFileObject in MusicList)
                {
                    TreeNode MusicNode = TreeViewControl.Nodes.Find(StreamFileObject.Key.ToString(), true)[0];

                    //Audio Path
                    AudioPathLeft = Path.Combine(MediaFolder, Path.GetFileNameWithoutExtension(StreamFileObject.Value.WAVFileName_LeftChannel) + ".wav");
                    AudioPathRight = Path.Combine(MediaFolder, Path.GetFileNameWithoutExtension(StreamFileObject.Value.WAVFileName_RightChannel) + ".wav");

                    //Export Audio if Not Exists
                    if (!File.Exists(AudioPathLeft))
                    {
                        AudioF.CreateWavFile(GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels, StreamFileObject.Value.PCM_Data_LeftChannel, AudioPathLeft);
                    }
                    if (!File.Exists(AudioPathRight))
                    {
                        AudioF.CreateWavFile(GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels, StreamFileObject.Value.PCM_Data_RightChannel, AudioPathRight);
                    }

                    //Write Object Data
                    TextFileWriter.WriteLine("*MUSIC {");
                    TextFileWriter.WriteLine("\t*NODENAME \"{0}\"", MusicNode.Text);
                    TextFileWriter.WriteLine("\t*FOLDERNAME \"{0}\"", MusicNode.Parent.Text);
                    TextFileWriter.WriteLine("\t*NODECOLOR {0} {1} {2}", MusicNode.ForeColor.R, MusicNode.ForeColor.G, MusicNode.ForeColor.B);
                    TextFileWriter.WriteLine("\t*FILEPATH_LEFT \"{0}\"", AudioPathLeft);
                    TextFileWriter.WriteLine("\t*FILEPATH_RIGHT \"{0}\"", AudioPathRight);
                    TextFileWriter.WriteLine("\t*BASEVOLUME {0}", StreamFileObject.Value.BaseVolume);
                    TextFileWriter.WriteLine("\t*NUMSTARTMARKERS {0}", StreamFileObject.Value.StartMarkers.Count);
                    TextFileWriter.WriteLine("\t*NUMMARKERS {0}", StreamFileObject.Value.Markers.Count);

                    //Write Start Markers
                    TextFileWriter.WriteLine("\t*STARTMARKERS {");
                    foreach (EXStreamStartMarker StrtMarker in StreamFileObject.Value.StartMarkers)
                    {
                        TextFileWriter.WriteLine("\t\t*STARTMARKER {");
                        TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrtMarker.Name);
                        TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrtMarker.Position);
                        TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrtMarker.MusicMakerType);
                        TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrtMarker.Flags);
                        TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrtMarker.Extra);
                        TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrtMarker.LoopStart);
                        TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrtMarker.MarkerCount);
                        TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrtMarker.LoopMarkerCount);
                        TextFileWriter.WriteLine("\t\t\t*MARKERPOS {0}", StrtMarker.MarkerPos);
                        TextFileWriter.WriteLine("\t\t\t*ISINSTANT {0}", StrtMarker.IsInstant);
                        TextFileWriter.WriteLine("\t\t\t*INSTANTBUFFER {0}", StrtMarker.InstantBuffer);
                        TextFileWriter.WriteLine("\t\t\t*STATEA {0}", StrtMarker.StateA);
                        TextFileWriter.WriteLine("\t\t\t*STATEB {0}", StrtMarker.StateB);
                        TextFileWriter.WriteLine("\t\t}");
                    }
                    TextFileWriter.WriteLine("\t}");

                    //Write Markers 
                    TextFileWriter.WriteLine("\t*MARKERS {");
                    foreach (EXStreamMarker StrmMarker in StreamFileObject.Value.Markers)
                    {
                        TextFileWriter.WriteLine("\t\t*MARKER {");
                        TextFileWriter.WriteLine("\t\t\t*NAME {0}", StrmMarker.Name);
                        TextFileWriter.WriteLine("\t\t\t*POSITION {0}", StrmMarker.Position);
                        TextFileWriter.WriteLine("\t\t\t*MUSICMARKERTYPE {0}", StrmMarker.MusicMakerType);
                        TextFileWriter.WriteLine("\t\t\t*FLAGS {0}", StrmMarker.Flags);
                        TextFileWriter.WriteLine("\t\t\t*EXTRA {0}", StrmMarker.Extra);
                        TextFileWriter.WriteLine("\t\t\t*LOOPSTART {0}", StrmMarker.LoopStart);
                        TextFileWriter.WriteLine("\t\t\t*MARKERCOUNT {0}", StrmMarker.MarkerCount);
                        TextFileWriter.WriteLine("\t\t\t*LOOPMARKERCOUNT {0}", StrmMarker.LoopMarkerCount);
                        TextFileWriter.WriteLine("\t\t}");
                    }
                    TextFileWriter.WriteLine("\t}");
                    TextFileWriter.WriteLine("}");
                }
                TextFileWriter.Close();
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void WriteFileHeader(StreamWriter TextFileWriter)
        {
            TextFileWriter.WriteLine("*EUROSOUND_INTERCHANGE_FILE V1.0");
            TextFileWriter.WriteLine("*COMMENT EUROSOUND VERSION: {0}", GenericFunctions.GetEuroSoundVersion());
            TextFileWriter.WriteLine("*COMMENT " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss").ToUpper());
            TextFileWriter.WriteLine("");
        }
    }
}
