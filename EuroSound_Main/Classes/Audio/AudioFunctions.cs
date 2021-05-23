﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioMixingFunctions;
using EuroSound_Application.CustomControls.WavesViewerForm;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;
using System.Text;

namespace EuroSound_Application.AudioFunctionsLibrary
{
    internal class AudioFunctions
    {
        private MemoryStream AudioSample;

        internal void PlayAudio(WaveOut AudioPlayer, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, decimal AudioPan, decimal Volume)
        {
            if (Frequency != 0)
            {
                if (AudioPlayer.PlaybackState == PlaybackState.Stopped)
                {
                    AudioSample = new MemoryStream(PCMData);
                    IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(CalculateValidRate(Frequency, Pitch), Bits, Channels));
                    VolumeSampleProvider volumeProvider = new VolumeSampleProvider(provider.ToSampleProvider());
                    AudioPlayer.DeviceNumber = GlobalPreferences.DefaultAudioDevice;
                    AudioPlayer.Volume = (float)Volume;

                    //Pan is only for mono audio
                    if (Channels == 1)
                    {
                        PanningSampleProvider panProvider = new PanningSampleProvider(volumeProvider)
                        {
                            Pan = (float)AudioPan
                        };
                        AudioPlayer.Init(panProvider);
                    }
                    else
                    {
                        AudioPlayer.Init(provider);
                    }
                    AudioPlayer.Play();
                }
            }
        }

        internal void PlayAudioMultiplexing(WaveOut AudioPlayer, byte[] PCMDataLeft, byte[] PCMDataRight, int Frequency, int Bits, int Channels, decimal Volume)
        {
            if (Frequency != 0)
            {
                if (AudioPlayer.PlaybackState == PlaybackState.Stopped)
                {
                    AudioSample = new MemoryStream(PCMDataLeft);
                    IWaveProvider providerLeft = new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency, Bits, Channels));
                    AudioSample = new MemoryStream(PCMDataRight);
                    IWaveProvider providerRight = new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency, Bits, Channels));

                    MultiplexingWaveProvider waveProvider = new MultiplexingWaveProvider(new IWaveProvider[] { providerLeft, providerRight }, 2);
                    AudioPlayer.DeviceNumber = GlobalPreferences.DefaultAudioDevice;
                    AudioPlayer.Volume = (float)Volume;
                    AudioPlayer.Init(waveProvider);
                    AudioPlayer.Play();
                }
            }
        }

        private int CalculateValidRate(int DefaultRate, int Pitch)
        {
            int NewPitch = DefaultRate + Pitch;
            while (NewPitch <= 0)
            {
                NewPitch = DefaultRate + (Pitch / 10);
            }

            return NewPitch;
        }

        internal void PlayAudioLoopOffset(WaveOut AudioPlayer, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, int Pan)
        {
            if (AudioPlayer.PlaybackState == PlaybackState.Stopped)
            {
                AudioSample = new MemoryStream(PCMData);
                using (LoopStream loop = new LoopStream(new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency + Pitch, Bits, Channels))))
                {
                    VolumeSampleProvider volumeProvider = new VolumeSampleProvider(loop.ToSampleProvider());
                    PanningSampleProvider panProvider = new PanningSampleProvider(volumeProvider)
                    {
                        Pan = (Pan / 100)
                    };
                    AudioPlayer.DeviceNumber = GlobalPreferences.DefaultAudioDevice;
                    AudioPlayer.Volume = 1;
                    AudioPlayer.Init(panProvider);
                    AudioPlayer.Play();
                }
            }
        }

        internal void StopAudio(WaveOut AudioPlayer)
        {
            if (AudioPlayer.PlaybackState == PlaybackState.Playing)
            {
                AudioPlayer.Stop();
                AudioSample.Close();
                AudioSample.Dispose();
            }
        }

        internal void DrawAudioWaves(EuroSound_WaveViewer ControlToDraw, object SelectedSound, int Delay, bool Right)
        {
            ControlToDraw.RenderDelay = Delay;
            if (SelectedSound.GetType() == typeof(EXSoundStream))
            {
                EXSoundStream StreamSounds = ((EXSoundStream)SelectedSound);
                ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(StreamSounds.PCM_Data), new WaveFormat((int)StreamSounds.Frequency, (int)StreamSounds.Bits, StreamSounds.Channels));
            }
            else if (SelectedSound.GetType() == typeof(EXAudio))
            {
                EXAudio AudioObject = ((EXAudio)SelectedSound);
                ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(AudioObject.PCMdata), new WaveFormat((int)AudioObject.Frequency, (int)AudioObject.Bits, (int)AudioObject.Channels));
            }
            else if (SelectedSound.GetType() == typeof(EXMusic))
            {
                EXMusic AudioObject = ((EXMusic)SelectedSound);
                if (Right)
                {
                    ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(AudioObject.PCM_Data_RightChannel), new WaveFormat((int)AudioObject.Frequency_RightChannel, (int)AudioObject.Bits_RightChannel, AudioObject.Channels_RightChannel));
                }
                else
                {
                    ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(AudioObject.PCM_Data_LeftChannel), new WaveFormat((int)AudioObject.Frequency_LeftChannel, (int)AudioObject.Bits_LeftChannel, AudioObject.Channels_LeftChannel));
                }
            }
            ControlToDraw.InitControl();
        }

        internal short[] ConvertPCMDataToShortArray(byte[] PCMData)
        {
            short[] PCMDataShortArray;

            using (MemoryStream MSPCMData = new MemoryStream(PCMData))
            {
                using (BinaryReader BReader = new BinaryReader(MSPCMData))
                {
                    PCMDataShortArray = new short[PCMData.Length / 2];

                    //Get data
                    for (int i = 0; i < PCMDataShortArray.Length; i++)
                    {
                        PCMDataShortArray[i] = BReader.ReadInt16();
                    }

                    //Close Reader
                    BReader.Close();
                }
            }

            return PCMDataShortArray;
        }

        internal void CreateWavFile(int Frequency, int BitsPerChannel, int NumberOfChannels, byte[] PCMData, string FilePath)
        {
            using (FileStream WavFile = new FileStream(FilePath, FileMode.Create))
            {
                using (BinaryWriter BWritter = new BinaryWriter(WavFile))
                {
                    //Write WAV Header
                    BWritter.Write(Encoding.UTF8.GetBytes("RIFF")); //Chunk ID
                    BWritter.Write((uint)(36 + PCMData.Length)); //Chunk Size
                    BWritter.Write(Encoding.UTF8.GetBytes("WAVE")); //Format
                    BWritter.Write(Encoding.UTF8.GetBytes("fmt ")); //Subchunk1 ID
                    BWritter.Write((uint)16); //Subchunk1 Size
                    BWritter.Write((ushort)1); //Audio Format
                    BWritter.Write((ushort)NumberOfChannels); //Num Channels
                    BWritter.Write((uint)(Frequency)); //Sample Rate
                    BWritter.Write((uint)((Frequency * NumberOfChannels * BitsPerChannel) / 8)); //Byte Rate
                    BWritter.Write((ushort)((NumberOfChannels * BitsPerChannel) / 8)); //Block Align
                    BWritter.Write((ushort)(BitsPerChannel)); //Bits Per Sample
                    BWritter.Write(Encoding.UTF8.GetBytes("data")); //Subchunk2 ID
                    BWritter.Write((uint)PCMData.Length); //Subchunk2 Size

                    //Write PCM Data
                    for (int i = 0; i < PCMData.Length; i++)
                    {
                        BWritter.Write(PCMData[i]);
                    }

                    //Close Writter
                    BWritter.Close();
                }

                //Close File
                WavFile.Close();
            }
        }

        internal byte[] WavToVag(string inputFilePath, string outputFilePath, int Frequency, int outputFrequency, byte[] PCMData)
        {
            byte[] convertedData;

            if (!File.Exists(inputFilePath))
            {
                AudioSample = new MemoryStream(PCMData);
                IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency, 16, 1));
                if (outputFrequency == 0)
                {
                    outputFrequency = 11025;
                }
                using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(provider, new WaveFormat(outputFrequency, 16, 1)))
                {
                    WaveFileWriter.CreateWaveFile(inputFilePath, conversionStream);
                }
            }
            //var t = ExternalCHelper.get_vag_file(inputFilePath, outputFilePath, Frequency);
            using (BinaryReader BReader = new BinaryReader(File.Open(outputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                BReader.BaseStream.Seek(0x40, SeekOrigin.Begin);
                int dataLength = (int)BReader.BaseStream.Length - 0x40;
                convertedData = BReader.ReadBytes(dataLength);

                //Close Reader
                BReader.Close();
            }

            return convertedData;
        }
    }
}
