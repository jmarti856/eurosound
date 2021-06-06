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
            foreach (KeyValuePair<uint, EXSound> soundToCheck in SoundsList)
            {
                if (soundToCheck.Value.OutputThisSound)
                {
                    foreach (KeyValuePair<uint, EXSample> Sample in soundToCheck.Value.Samples)
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
            List<string> audiosToPurge = new List<string>();

            foreach (string audioKey in AudioDataDict.Keys)
            {
                bool purgeCurrent = true;

                foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                {
                    foreach (KeyValuePair<uint, EXSample> sample in Sound.Value.Samples)
                    {
                        if (sample.Value.ComboboxSelectedAudio.Equals(audioKey))
                        {
                            purgeCurrent = false;
                            break;
                        }
                    }
                    if (!purgeCurrent)
                    {
                        break;
                    }
                }

                if (purgeCurrent)
                {
                    audiosToPurge.Add(audioKey);
                }
            }
            audiosToPurge.TrimExcess();

            return audiosToPurge;
        }

        internal static Dictionary<string, string> GetListAudioData(Dictionary<string, EXAudio> AudiosList, TreeView ControlToSearch)
        {
            Dictionary<string, string> dictionaryToShow = new Dictionary<string, string>();

            foreach (KeyValuePair<string, EXAudio> item in AudiosList)
            {
                TreeNode nodesSearchResult = ControlToSearch.Nodes.Find(item.Key, true)[0];
                if (nodesSearchResult != null)
                {
                    dictionaryToShow.Add(item.Key, nodesSearchResult.Text);
                }
            }

            return dictionaryToShow;
        }

        internal static EXAudio LoadAudioData(string FilePath)
        {
            try
            {
                using (WaveFileReader audioReader = new WaveFileReader(FilePath))
                {
                    EXAudio Audio = new EXAudio
                    {
                        LoadedFileName = Path.GetFileName(FilePath),
                        Frequency = (uint)audioReader.WaveFormat.SampleRate,
                        Channels = (uint)audioReader.WaveFormat.Channels,
                        Bits = (uint)audioReader.WaveFormat.BitsPerSample,
                        Duration = (uint)Math.Round(audioReader.TotalTime.TotalMilliseconds, 1),
                        Encoding = audioReader.WaveFormat.Encoding.ToString(),
                        Flags = 0,
                        LoopOffset = 0,
                        PSIsample = 0
                    };

                    //Get PCM Data
                    Audio.PCMdata = new byte[audioReader.Length];
                    audioReader.Read(Audio.PCMdata, 0, (int)audioReader.Length);
                    audioReader.Close();

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
                using (WaveFileReader wavReader = new WaveFileReader(SourcePath))
                {
                    using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(wavReader, new WaveFormat(GlobalPreferences.SoundbankFrequency, GlobalPreferences.SoundbankBits, GlobalPreferences.SoundbankChannels)))
                    {
                        EXAudio Audio = new EXAudio
                        {
                            LoadedFileName = Path.GetFileName(SourcePath),
                            Frequency = (uint)conversionStream.WaveFormat.SampleRate,
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
        #endregion

        #region SOUNDS
        internal static bool SoundWillBeOutputed(Dictionary<uint, EXSound> SoundsList, uint SoundKey)
        {
            bool outputThisSound = false;

            EXSound soundToCheck = ReturnSoundFromDictionary(SoundKey, SoundsList);
            if (soundToCheck != null)
            {
                if (soundToCheck.OutputThisSound)
                {
                    outputThisSound = true;
                }
            }

            return outputThisSound;
        }

        internal static EXSound ReturnSoundFromDictionary(uint SoundID, Dictionary<uint, EXSound> SoundsList)
        {
            //REnamed to ReturnSoundFromDictionary
            EXSound searchedSound = null;

            if (SoundsList.ContainsKey(SoundID))
            {
                searchedSound = SoundsList[SoundID];
                return searchedSound;
            }

            return searchedSound;
        }

        internal static bool AddSampleToSound(EXSound Sound, uint Index, bool StreamedSample)
        {
            bool addedCorrectly = false;

            EXSample sample = new EXSample
            {
                IsStreamed = StreamedSample,
            };

            Sound.Samples.Add(Index, sample);

            return addedCorrectly;
        }
        #endregion

        internal static EXSample ReturnSampleFromSound(EXSound SelectedSound, uint SampleID)
        {
            //Renamed to ReturnSampleFromSound
            EXSample selectedSample = SelectedSound.Samples[SampleID];

            return selectedSample;
        }

        internal static bool SubSFXFlagChecked(int Flags)
        {
            return Convert.ToBoolean((Flags >> 10) & 1);
        }
    }
}