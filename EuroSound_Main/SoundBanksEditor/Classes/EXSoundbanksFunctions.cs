using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal static class EXSoundbanksFunctions
    {
        internal static bool SoundWillBeOutputed(Dictionary<uint, EXSound> SoundsList, string SoundName)
        {
            bool Output = false;

            EXSound Test = GetSoundByName(uint.Parse(SoundName), SoundsList);
            if (Test != null)
            {
                if (Test.OutputThisSound)
                {
                    Output = true;
                }
            }

            return Output;
        }

        internal static void AddAudioToList(EXAudio AudioToAdd, string Hash, Dictionary<string, EXAudio> AudioDataDict)
        {
            if (AudioToAdd != null)
            {
                AudioDataDict.Add(Hash, AudioToAdd);
            }
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

        internal static bool DeleteAudio(Dictionary<string, EXAudio> AudiosDictionary, string AudioKeyToRemove)
        {
            bool DeletedSuccessfully = false;

            if (AudiosDictionary.ContainsKey(AudioKeyToRemove))
            {
                DeletedSuccessfully = true;
                AudiosDictionary.Remove(AudioKeyToRemove);
            }

            return DeletedSuccessfully;
        }

        internal static List<string> GetAudioDependencies(string AudioKey, string AudioName, Dictionary<uint, EXSound> SoundsList, bool ItemUsage)
        {
            List<string> Dependencies = new List<string>();

            foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Value.Samples)
                {
                    if (Sample.ComboboxSelectedAudio.Equals(AudioKey))
                    {
                        if (ItemUsage)
                        {
                            Dependencies.Add(AudioName + "," + Sound.Value.DisplayName);
                        }
                        else
                        {
                            Dependencies.Add("0" + Sound.Value.DisplayName + " uses this audio");
                        }
                    }
                }
            }
            return Dependencies;
        }

        internal static List<string> GetAudiosToPurge(Dictionary<string, EXAudio> AudioDataDict, Dictionary<uint, EXSound> SoundsList)
        {
            List<string> AudiosToPurge = new List<string>();
            List<string> UsedAudios = GetUsedAudios(SoundsList, false);

            /*Now compare*/
            foreach (string key in AudioDataDict.Keys)
            {
                if (!UsedAudios.Contains(key))
                {
                    AudiosToPurge.Add(key);
                }
            }

            return AudiosToPurge;
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

        internal static byte[] GetRawPCMData(string AudioFilePath)
        {
            int dataSize;
            byte[] byteArray;

            try
            {
                BinaryReader Reader = new BinaryReader(File.Open(AudioFilePath, FileMode.Open, FileAccess.Read));

                /*Go to RAW PCM data*/
                Reader.BaseStream.Seek(0x28, SeekOrigin.Begin);

                /*Read size*/
                dataSize = Reader.ReadInt32();

                /*Get data*/
                byteArray = Reader.ReadBytes(dataSize);

                Reader.Close();
                Reader.Dispose();
            }
            catch
            {
                byteArray = null;
            }

            return byteArray;
        }

        internal static EXSound GetSoundByName(uint NameToSearch, Dictionary<uint, EXSound> SoundsList)
        {
            EXSound SearchedSound = null;

            if (SoundsList.ContainsKey(NameToSearch))
            {
                SearchedSound = SoundsList[NameToSearch];
                return SearchedSound;
            }

            return SearchedSound;
        }

        internal static List<string> GetUsedAudios(Dictionary<uint, EXSound> SoundsList, bool OnlyOutputAudios)
        {
            List<string> UsedAudios = new List<string>();

            /*First we need to know which audios are used*/
            foreach (KeyValuePair<uint, EXSound> SoundToCheck in SoundsList)
            {
                if (OnlyOutputAudios)
                {
                    if (SoundToCheck.Value.OutputThisSound)
                    {
                        foreach (EXSample Sample in SoundToCheck.Value.Samples)
                        {
                            if (!UsedAudios.Contains(Sample.ComboboxSelectedAudio))
                            {
                                UsedAudios.Add(Sample.ComboboxSelectedAudio);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    foreach (EXSample Sample in SoundToCheck.Value.Samples)
                    {
                        if (!UsedAudios.Contains(Sample.ComboboxSelectedAudio))
                        {
                            UsedAudios.Add(Sample.ComboboxSelectedAudio);
                        }
                    }
                }
            }

            return UsedAudios;
        }

        internal static string LoadAudioAddToListAndTreeNode(string AudioFilePath, string DisplayName, Dictionary<string, EXAudio> AudioDataDict, TreeView TreeViewControl, int[] Props, List<string> Reports)
        {
            string FileMD5Hash;

            FileMD5Hash = GenericFunctions.CalculateMD5(AudioFilePath);

            if (!AudioDataDict.ContainsKey(FileMD5Hash))
            {
                EXAudio NewAudio = LoadAudioData(AudioFilePath);
                NewAudio.DisplayName = DisplayName;
                NewAudio.Flags = Convert.ToUInt16(Props[0]);
                NewAudio.PSIsample = Convert.ToUInt32(Props[1]);
                NewAudio.LoopOffset = Convert.ToUInt32(Props[2]);
                AddAudioToList(NewAudio, FileMD5Hash, AudioDataDict);
                TreeNodeFunctions.TreeNodeAddNewNode("AudioData", FileMD5Hash, "AD_" + DisplayName, 7, 7, "Audio", Color.Black, TreeViewControl);

                if (NewAudio.PCMdata == null)
                {
                    Reports.Add("0The file: " + AudioFilePath + " can't be readed, seems that is being used by another process.");
                }
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

        internal static EXAudio LoadAudioData(string FilePath)
        {
            WaveFileReader AudioReader = new WaveFileReader(FilePath);
            if (AudioReader.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                EXAudio Audio = new EXAudio
                {
                    Name = Path.GetFileName(FilePath),
                    DataSize = Convert.ToUInt32(AudioReader.Length),
                    Frequency = Convert.ToUInt32(AudioReader.WaveFormat.SampleRate),
                    RealSize = Convert.ToUInt32(new FileInfo(FilePath).Length),
                    Channels = Convert.ToUInt32(AudioReader.WaveFormat.Channels),
                    Bits = Convert.ToUInt32(AudioReader.WaveFormat.BitsPerSample),
                    Duration = Convert.ToUInt32(Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1)),
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

        internal static void RemoveSound(string Name, Dictionary<uint, EXSound> SoundsList)
        {
            EXSound itemToRemove = GetSoundByName(uint.Parse(Name), SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(uint.Parse(Name));
            }
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

        internal static bool SubSFXFlagChecked(int Flags)
        {
            return Convert.ToBoolean((Flags >> 10) & 1);
        }
    }
}