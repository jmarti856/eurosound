using EuroSound_Application.AudioFunctionsLibrary;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    internal static class EXSoundbanksFunctions
    {
        internal static bool SoundWillBeOutputed(Dictionary<uint, EXSound> SoundsList, uint SoundKey)
        {
            bool Output = false;

            EXSound Test = GetSoundByName(SoundKey, SoundsList);
            if (Test != null)
            {
                if (Test.OutputThisSound)
                {
                    Output = true;
                }
            }

            return Output;
        }

        internal static bool AddSampleToSound(EXSound Sound, uint Index, bool StreamedSample)
        {
            bool AddedCorrectly = false;

            EXSample Sample = new EXSample
            {
                IsStreamed = StreamedSample,
            };

            Sound.Samples.Add(Index, Sample);

            return AddedCorrectly;
        }

        internal static List<string> GetAudioDependencies(string AudioKey, string AudioName, Dictionary<uint, EXSound> SoundsList, TreeView ControlNodes, bool ItemUsage)
        {
            List<string> Dependencies = new List<string>();
            string DisplayName;

            foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
            {
                foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                {
                    if (Sample.Value.ComboboxSelectedAudio.Equals(AudioKey))
                    {
                        if (ItemUsage)
                        {
                            Dependencies.Add(AudioName + "," + Sound.Key);
                        }
                        else
                        {
                            DisplayName = ControlNodes.Nodes.Find(Sample.Key.ToString(), true)[0].Text;
                            Dependencies.Add("0" + DisplayName + " uses this audio");
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

        internal static EXSound GetSoundByName(uint SoundID, Dictionary<uint, EXSound> SoundsList)
        {
            EXSound SearchedSound = null;

            if (SoundsList.ContainsKey(SoundID))
            {
                SearchedSound = SoundsList[SoundID];
                return SearchedSound;
            }

            return SearchedSound;
        }

        internal static EXSample GetSoundSample(EXSound SelectedSound, uint SampleID)
        {
            EXSample SelectedSample;

            SelectedSample = SelectedSound.Samples[SampleID];

            return SelectedSample;
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
                        foreach (KeyValuePair<uint, EXSample> Sample in SoundToCheck.Value.Samples)
                        {
                            if (!UsedAudios.Contains(Sample.Value.ComboboxSelectedAudio))
                            {
                                UsedAudios.Add(Sample.Value.ComboboxSelectedAudio);
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
                    foreach (KeyValuePair<uint, EXSample> Sample in SoundToCheck.Value.Samples)
                    {
                        if (!UsedAudios.Contains(Sample.Value.ComboboxSelectedAudio))
                        {
                            UsedAudios.Add(Sample.Value.ComboboxSelectedAudio);
                        }
                    }
                }
            }

            return UsedAudios;
        }

        internal static EXAudio LoadAudioData(string FilePath)
        {
            int NumberOfChannels;

            try
            {
                using (WaveFileReader AudioReader = new WaveFileReader(FilePath))
                {
                    NumberOfChannels = AudioReader.WaveFormat.Channels;

                    AudioFunctions AudioLibrary = new AudioFunctions();
                    EXAudio Audio = new EXAudio
                    {
                        LoadedFileName = Path.GetFileName(FilePath),
                        DataSize = (uint)AudioReader.Length,
                        Frequency = (uint)AudioReader.WaveFormat.SampleRate,
                        RealSize = (uint)new FileInfo(FilePath).Length,
                        Channels = (uint)NumberOfChannels,
                        Bits = (uint)AudioReader.WaveFormat.BitsPerSample,
                        Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1),
                        Encoding = AudioReader.WaveFormat.Encoding.ToString(),
                        Flags = 0,
                        LoopOffset = 0,
                        PSIsample = 0
                    };
                    AudioReader.Close();

                    /*Get PCM data*/
                    Audio.PCMdata = AudioLibrary.GetWavPCMData(FilePath);
                    if (Audio.PCMdata != null)
                    {
                        Audio.DataSize = Convert.ToUInt32(Audio.PCMdata.Length);
                    }
                    return Audio;
                }
            }
            catch
            {

            }

            return null;
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
            if (!string.IsNullOrEmpty(TextToModify))
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