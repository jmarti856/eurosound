using FunctionsLibrary;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    public static class EXObjectsFunctions
    {
        public static bool SoundWillBeOutputed(Dictionary<int, EXSound> SoundsList, string SoundName)
        {
            bool Output = false;

            EXSound Test = GetSoundByName(int.Parse(SoundName), SoundsList);
            if (Test != null)
            {
                if (Test.OutputThisSound)
                {
                    Output = true;
                }
            }

            return Output;
        }

        internal static void RemoveSampleFromSound(EXSound SoundName, string SampleName)
        {
            EXSample SampleToRemove = TreeNodeFunctions.GetSelectedSample(SoundName, SampleName);
            if (SampleToRemove != null)
            {
                SoundName.Samples.Remove(SampleToRemove);
            }
        }

        internal static void RemoveSound(string Name, Dictionary<int, EXSound> SoundsList)
        {
            EXSound itemToRemove = GetSoundByName(int.Parse(Name), SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(int.Parse(Name));
            }
        }

        internal static void AddEmptySoundWithName(int Index, string v_DisplayName, string v_Hashcode, Dictionary<int, EXSound> SoundsList)
        {
            EXSound Sound = new EXSound
            {
                DisplayName = v_DisplayName,
                Hashcode = v_Hashcode,
            };

            SoundsList.Add(Index, Sound);
        }

        internal static int GetSoundID(ProjectFile FileProperties)
        {
            int index;

            index = (FileProperties.SoundID += 1);

            return index;
        }

        internal static bool AddSampleToSound(EXSound Sound, string SampleName, bool StreamedSample)
        {
            bool AddedCorrectly = false;

            EXSample Sample = new EXSample
            {
                Name = RemoveWhiteSpaces(SampleName),
                DisplayName = SampleName,
                IsStreamed = StreamedSample,
            };


            Sound.Samples.Add(Sample);

            return AddedCorrectly;
        }

        internal static string RemoveWhiteSpaces(string TextToModify)
        {
            string NewString = string.Empty;

            /*Before remove whitespaces, first check that is not null*/
            if (!(string.IsNullOrEmpty(TextToModify)))
            {
                NewString = Regex.Replace(TextToModify, @"\s", "");
            }

            return NewString;
        }

        internal static EXSound GetSoundByName(int NameToSearch, Dictionary<int, EXSound> SoundsList)
        {
            EXSound SearchedSound;

            if (SoundsList.ContainsKey(NameToSearch))
            {
                SearchedSound = SoundsList[NameToSearch];
                return SearchedSound;
            }
            else
            {
                return null;
            }
        }

        internal static byte[] GetRawPCMData(string AudioFilePath)
        {
            int dataSize;

            BinaryReader Reader = new BinaryReader(File.Open(AudioFilePath, FileMode.Open, FileAccess.Read));

            /*Go to RAW PCM data*/
            Reader.BaseStream.Seek(0x28, SeekOrigin.Begin);

            /*Read size*/
            dataSize = Reader.ReadInt32();

            /*Get data*/
            byte[] byteArray = Reader.ReadBytes(dataSize);

            Reader.Close();
            Reader.Dispose();

            return byteArray;
        }

        internal static Dictionary<int, EXSound> GetFinalListToExport(Dictionary<int, EXSound> SoundsList)
        {
            Dictionary<int, EXSound> FinalList = new Dictionary<int, EXSound>();

            foreach (KeyValuePair<int, EXSound> Sound in SoundsList)
            {
                if (Sound.Value.OutputThisSound)
                {
                    FinalList.Add(Sound.Key, Sound.Value);
                }
            }

            return FinalList;
        }

        internal static string LoadAudioAddToListAndTreeNode(string AudioFilePath, string DisplayName, Dictionary<string, EXAudio> AudioDataDict, TreeView TreeViewControl, int[] Props, ProjectFile ProjectProperties)
        {
            string FileMD5Hash;

            FileMD5Hash = GenericFunctions.CalculateMD5(AudioFilePath);

            if (!AudioDataDict.ContainsKey(FileMD5Hash))
            {
                EXAudio NewAudio = LoadAudioData(AudioFilePath);
                NewAudio.DisplayName = DisplayName;
                NewAudio.Flags = Props[0];
                NewAudio.PSIsample = Props[1];
                NewAudio.LoopOffset = Props[2];
                AddAudioToList(NewAudio, FileMD5Hash, AudioDataDict);
                TreeNodeFunctions.TreeNodeAddNewNode("AudioData", FileMD5Hash, "AD_" + DisplayName, 7, 7, "Audio", Color.Black, TreeViewControl, ProjectProperties);
            }

            return FileMD5Hash;
        }

        internal static bool LoadAudioAndAddToList(string AudioFilePath, string DisplayName, Dictionary<string, EXAudio> AudioDataDict, string FileMD5Hash)
        {
            bool AddedCorrectly = true;

            if (!AudioDataDict.ContainsKey(FileMD5Hash))
            {
                EXAudio NewAudio = LoadAudioData(AudioFilePath);
                NewAudio.DisplayName = DisplayName;
                AddAudioToList(NewAudio, FileMD5Hash, AudioDataDict);
            }
            else
            {
                AddedCorrectly = false;
            }

            return AddedCorrectly;
        }

        internal static void AddAudioToList(EXAudio AudioToAdd, string Hash, Dictionary<string, EXAudio> AudioDataDict)
        {
            if (AudioToAdd != null)
            {
                AudioDataDict.Add(Hash, AudioToAdd);
            }
        }

        internal static EXAudio LoadAudioData(string FilePath)
        {
            WaveFileReader AudioReader = new WaveFileReader(FilePath);
            if (AudioReader.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                EXAudio Audio = new EXAudio
                {
                    Name = Path.GetFileName(FilePath),
                    DataSize = Convert.ToInt32(AudioReader.Length),
                    Frequency = AudioReader.WaveFormat.SampleRate,
                    RealSize = Convert.ToInt32(new FileInfo(FilePath).Length),
                    Channels = AudioReader.WaveFormat.Channels,
                    Bits = AudioReader.WaveFormat.BitsPerSample,
                    Duration = Convert.ToInt32(Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1)),
                    Encoding = AudioReader.WaveFormat.Encoding.ToString(),
                    Flags = 0,
                    LoopOffset = 0,
                    PSIsample = 0
                };
                AudioReader.Close();
                AudioReader.Dispose();
                AudioReader.Flush();

                /*Get PCM data*/
                Audio.PCMdata = GetRawPCMData(FilePath);

                return Audio;
            }

            return null;
        }

        internal static Dictionary<string, string> GetListAudioData(Dictionary<string, EXAudio> AudiosList, TreeView ControlToSearch)
        {
            Dictionary<string, string> DictionaryToShow = new Dictionary<string, string>
            {
                { "<SUB SFX>", "<SUB SFX>" }
            };
            foreach (KeyValuePair<string, EXAudio> item in AudiosList)
            {
                TreeNode NodeResult = ControlToSearch.Nodes.Find(item.Key, true)[0];
                if (NodeResult != null)
                {
                    DictionaryToShow.Add(item.Key, NodeResult.Text);
                }
            }

            return DictionaryToShow;
        }

        internal static bool SubSFXFlagChecked(int Flags)
        {
            return Convert.ToBoolean((Flags >> 10) & 1);
        }

        internal static List<string> GetAudioDependencies(string AudioToCheck, Dictionary<int, EXSound> SoundsList)
        {
            List<string> Dependencies = new List<string>();

            foreach (KeyValuePair<int, EXSound> Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Value.Samples)
                {
                    if (Sample.ComboboxSelectedAudio.Equals(AudioToCheck))
                    {
                        Dependencies.Add("0"+Sound.Value.DisplayName + " uses this audio");
                    }
                }
            }

            return Dependencies;
        }

        internal static string GetSoundKeyByName(Dictionary<int, EXSound> SoundsList, string SoundName)
        {
            string Key = null;

            foreach (KeyValuePair<int, EXSound> Sound in SoundsList)
            {
                if (Sound.Value.DisplayName.Equals(SoundName))
                {
                    Key = Sound.Key.ToString();
                    break;
                }
            }

            return Key;
        }

        internal static string GetAudioKeyByName(Dictionary<string, EXAudio> AudioList, string AudioName)
        {
            string Key = null;

            foreach (KeyValuePair<string, EXAudio> Audio in AudioList)
            {
                if (Audio.Value.DisplayName.Equals(AudioName))
                {
                    Key = Audio.Key.ToString();
                    break;
                }
            }

            return Key;
        }
        public static void SaveDocument(string LoadedFile, TreeView TreeView_File, Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile ProjectProperties, Dictionary<string, string> SB_Defines)
        {
            if (!string.IsNullOrEmpty(LoadedFile))
            {
                SaveAndLoadESF.SaveDocument(TreeView_File, SoundsList, AudioDataDict, LoadedFile, ProjectProperties);
            }
            else
            {
                OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectProperties, SB_Defines);
            }
        }

        internal static string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile FileProperties, Dictionary<string, string> SB_Defines)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.ESF)|*.ESF|All files (*.*)|*.*", 1, true, Hashcodes.GetHashcodeLabel(SB_Defines, FileProperties.Hashcode));
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SaveAndLoadESF.SaveDocument(TreeView_File, SoundsList, AudioDataDict, SavePath, FileProperties);
                }
            }
            return SavePath;
        }
    }
}
