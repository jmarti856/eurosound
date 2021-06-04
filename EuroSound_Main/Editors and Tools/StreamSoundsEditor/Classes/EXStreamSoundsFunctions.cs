using EngineXImaAdpcm;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.StreamSounds
{
    internal static class EXStreamSoundsFunctions
    {
        internal static EXSoundStream GetSoundByName(uint NameToSearch, Dictionary<uint, EXSoundStream> SoundsList)
        {
            EXSoundStream searchedSound = null;

            if (SoundsList.ContainsKey(NameToSearch))
            {
                searchedSound = SoundsList[NameToSearch];
                return searchedSound;
            }

            return searchedSound;
        }

        internal static bool SoundWillBeOutputed(Dictionary<uint, EXSoundStream> SoundsList, string SoundName)
        {
            bool output = false;

            EXSoundStream ObjectToCheck = GetSoundByName(uint.Parse(SoundName), SoundsList);
            if (ObjectToCheck != null)
            {
                if (ObjectToCheck.OutputThisSound)
                {
                    output = true;
                }
            }

            return output;
        }

        internal static string GetMarkerType(uint MarkerValue)
        {
            string markerType;

            switch (MarkerValue)
            {
                case 10:
                    markerType = "Start";
                    break;
                case 9:
                    markerType = "End";
                    break;
                case 7:
                    markerType = "Goto";
                    break;
                case 6:
                    markerType = "Loop";
                    break;
                case 5:
                    markerType = "Pause";
                    break;
                default:
                    markerType = "Jump";
                    break;
            }

            return markerType;
        }

        internal static void LoadAudioData(string AudioPath, EXSoundStream TemporalSound, bool ConvertAudio, AudioFunctions AudioLibrary)
        {
            TemporalSound.WAVFileMD5 = GenericFunctions.CalculateMD5(AudioPath);
            TemporalSound.WAVFileName = Path.GetFileName(AudioPath);

            using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
            {
                ImaADPCM_Functions imaADPCMFunctions = new ImaADPCM_Functions();
                if (ConvertAudio)
                {
                    using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(AudioReader, new WaveFormat(GlobalPreferences.StreambankFrequency, GlobalPreferences.StreambankBits, GlobalPreferences.StreambankChannels)))
                    {
                        TemporalSound.Channels = (byte)conversionStream.WaveFormat.Channels;
                        TemporalSound.Frequency = (uint)conversionStream.WaveFormat.SampleRate;
                        TemporalSound.Bits = (uint)conversionStream.WaveFormat.BitsPerSample;
                        TemporalSound.Encoding = conversionStream.WaveFormat.Encoding.ToString();

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
                            TemporalSound.PCM_Data = outStream.ToArray();
                        }

                        //Get Properties
                        TemporalSound.Duration = (uint)(decimal.Divide(TemporalSound.PCM_Data.Length, conversionStream.WaveFormat.AverageBytesPerSecond) * 1000);

                        //Get IMA ADPCM Data
                        TemporalSound.IMA_ADPCM_DATA = imaADPCMFunctions.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalSound.PCM_Data));
                    }
                }
                else
                {
                    TemporalSound.Channels = (byte)AudioReader.WaveFormat.Channels;
                    TemporalSound.Frequency = (uint)AudioReader.WaveFormat.SampleRate;
                    TemporalSound.Bits = (uint)AudioReader.WaveFormat.BitsPerSample;
                    TemporalSound.Encoding = AudioReader.WaveFormat.Encoding.ToString();
                    TemporalSound.Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);

                    //Get PCM Data
                    TemporalSound.PCM_Data = new byte[AudioReader.Length];
                    AudioReader.Read(TemporalSound.PCM_Data, 0, (int)AudioReader.Length);

                    //Get IMA ADPCM Data
                    TemporalSound.IMA_ADPCM_DATA = imaADPCMFunctions.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalSound.PCM_Data));
                }
                AudioReader.Close();
            }
        }
    }
}
