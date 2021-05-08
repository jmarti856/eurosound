using EuroSound_Application.ApplicationPreferences;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    internal static class EXSoundbanksFunctions
    {
        #region AUDIOS
        internal static IEnumerable<string> GetAudioDependencies(string AudioKey, string AudioName, Dictionary<uint, EXSound> SoundsList, TreeView ControlNodes, bool ItemUsage)
        {
            foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
            {
                foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                {
                    if (Sample.Value.ComboboxSelectedAudio.Equals(AudioKey))
                    {
                        if (ItemUsage)
                        {
                            yield return AudioName + "," + Sound.Key;
                        }
                        else
                        {
                            string DisplayName = ControlNodes.Nodes.Find(Sample.Key.ToString(), true)[0].Text;
                            yield return "0" + DisplayName + " uses this audio";
                        }
                    }
                }
            }
        }

        internal static IEnumerable<string> GetAudiosToExport(Dictionary<uint, EXSound> SoundsList)
        {
            foreach (KeyValuePair<uint, EXSound> SoundToCheck in SoundsList)
            {
                if (SoundToCheck.Value.OutputThisSound)
                {
                    foreach (KeyValuePair<uint, EXSample> Sample in SoundToCheck.Value.Samples)
                    {
                        yield return Sample.Value.ComboboxSelectedAudio;
                    }
                }
                else
                {
                    continue;
                }
            }
        }

        internal static IEnumerable<string> GetAudiosToPurge(Dictionary<string, EXAudio> AudioDataDict, Dictionary<uint, EXSound> SoundsList)
        {
            List<string> AudiosToPurge = new List<string>();

            foreach (string AudioKey in AudioDataDict.Keys)
            {
                bool PurgeCurrent = true;

                foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                {
                    foreach (KeyValuePair<uint, EXSample> sample in Sound.Value.Samples)
                    {
                        if (sample.Value.ComboboxSelectedAudio.Equals(AudioKey))
                        {
                            PurgeCurrent = false;
                            break;
                        }
                    }
                    if (!PurgeCurrent)
                    {
                        break;
                    }
                }

                if (PurgeCurrent)
                {
                    AudiosToPurge.Add(AudioKey);
                }
            }
            AudiosToPurge.TrimExcess();

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

        internal static EXAudio LoadAudioData(string FilePath)
        {
            try
            {
                using (WaveFileReader AudioReader = new WaveFileReader(FilePath))
                {
                    EXAudio Audio = new EXAudio
                    {
                        LoadedFileName = Path.GetFileName(FilePath),
                        DataSize = (uint)AudioReader.Length,
                        Frequency = (uint)AudioReader.WaveFormat.SampleRate,
                        RealSize = (uint)new FileInfo(FilePath).Length,
                        Channels = (uint)AudioReader.WaveFormat.Channels,
                        Bits = (uint)AudioReader.WaveFormat.BitsPerSample,
                        Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1),
                        Encoding = AudioReader.WaveFormat.Encoding.ToString(),
                        Flags = 0,
                        LoopOffset = 0,
                        PSIsample = 0
                    };

                    //Get PCM Data
                    Audio.PCMdata = new byte[AudioReader.Length];
                    AudioReader.Read(Audio.PCMdata, 0, (int)AudioReader.Length);
                    AudioReader.Close();

                    return Audio;
                }
            }
            catch
            {

            }

            return null;
        }

        internal static EXAudio LoadAndConvertData(string SourcePath)
        {
            try
            {
                using (WaveFileReader reader = new WaveFileReader(SourcePath))
                {
                    using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(reader, new WaveFormat(GlobalPreferences.SoundbankFrequency, GlobalPreferences.SoundbankBits, GlobalPreferences.SoundbankChannels)))
                    {
                        EXAudio Audio = new EXAudio
                        {
                            LoadedFileName = Path.GetFileName(SourcePath),
                            Frequency = (uint)conversionStream.WaveFormat.SampleRate,
                            RealSize = (uint)new FileInfo(SourcePath).Length,
                            Channels = (uint)conversionStream.WaveFormat.Channels,
                            Bits = (uint)conversionStream.WaveFormat.BitsPerSample,
                            Encoding = conversionStream.WaveFormat.Encoding.ToString(),
                            Flags = 0,
                            LoopOffset = 0,
                            PSIsample = 0
                        };

                        //Get PCM Data Stereo
                        using (MemoryStream outStream = new MemoryStream())
                        {
                            byte[] bytes = new byte[conversionStream.WaveFormat.AverageBytesPerSecond * 4];
                            while (true)
                            {
                                int bytesRead = conversionStream.Read(bytes, 0, bytes.Length);
                                if (bytesRead == 0)
                                {
                                    break;
                                }
                                outStream.Write(bytes, 0, bytesRead);
                            }
                            Audio.PCMdata = outStream.ToArray();
                        }

                        //Get Properties
                        Audio.DataSize = (uint)Audio.PCMdata.Length;
                        Audio.Duration = (uint)(decimal.Divide(Audio.PCMdata.Length, conversionStream.WaveFormat.AverageBytesPerSecond) * 1000);

                        return Audio;
                    }
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
        #endregion

        #region SOUNDS
        internal static bool SoundWillBeOutputed(Dictionary<uint, EXSound> SoundsList, uint SoundKey)
        {
            bool Output = false;

            EXSound Test = ReturnSoundFromDictionary(SoundKey, SoundsList);
            if (Test != null)
            {
                if (Test.OutputThisSound)
                {
                    Output = true;
                }
            }

            return Output;
        }

        internal static EXSound ReturnSoundFromDictionary(uint SoundID, Dictionary<uint, EXSound> SoundsList)
        {
            //REnamed to ReturnSoundFromDictionary
            EXSound SearchedSound = null;

            if (SoundsList.ContainsKey(SoundID))
            {
                SearchedSound = SoundsList[SoundID];
                return SearchedSound;
            }

            return SearchedSound;
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

        internal static void DeleteSound(string Name, Dictionary<uint, EXSound> SoundsList)
        {
            EXSound itemToRemove = ReturnSoundFromDictionary(uint.Parse(Name), SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(uint.Parse(Name));
            }
        }
        #endregion

        internal static EXSample ReturnSampleFromSound(EXSound SelectedSound, uint SampleID)
        {
            //Renamed to ReturnSampleFromSound
            EXSample SelectedSample = SelectedSound.Samples[SampleID];

            return SelectedSample;
        }

        internal static bool SubSFXFlagChecked(int Flags)
        {
            return Convert.ToBoolean((Flags >> 10) & 1);
        }
    }
}