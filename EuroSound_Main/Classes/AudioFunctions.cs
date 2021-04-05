using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioMixingFunctions;
using EuroSound_Application.CustomControls.WavesViewerForm;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SoxSharp;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
            int NewPitch;

            NewPitch = DefaultRate + Pitch;
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

        internal void DrawAudioWaves(EuroSound_WaveViewer ControlToDraw, object SelectedSound, int Delay)
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
                //EXMusic AudioObject = ((EXMusic)SelectedSound);
                //ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(AudioObject.PCM_Data_RightChannel), new WaveFormat((int)AudioObject.Frequency, (int)AudioObject.Bits, AudioObject.Channels));
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

        internal string ConvertWavToSoundBankValid(string SourcePath, string FileName, uint Frequency, ushort Channels, int Bits)
        {
            string FinalFile = string.Empty;

            //Create folder in %temp%
            GenericFunctions.CreateTemporalFolder();

            //Resample wav
            if (File.Exists(GlobalPreferences.SoXPath))
            {
                FinalFile = Path.Combine(Path.GetTempPath(), @"EuroSound\", FileName + "f.wav");
                try
                {
                    using (Sox sox = new Sox(GlobalPreferences.SoXPath))
                    {
                        sox.Output.Type = FileType.WAV;
                        sox.Output.SampleRate = Frequency;
                        sox.Output.Channels = Channels;
                        sox.Output.CustomArgs = " -b " + Bits;

                        InputFile testInput = new InputFile(SourcePath);
                        sox.Process(testInput, FinalFile);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("SoXInvalidPath"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return FinalFile;
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

        internal byte[] SplitChannels(byte[] PCM_Data, bool LeftChannel, int BytesPerSample)
        {
            byte[] ChannelData;

            using (BinaryReader BReader = new BinaryReader(new MemoryStream(PCM_Data)))
            {
                using (MemoryStream MS_ChannelData = new MemoryStream())
                {
                    using (BinaryWriter BWriter = new BinaryWriter(MS_ChannelData))
                    {
                        while (BReader.BaseStream.Position != BReader.BaseStream.Length)
                        {
                            if (LeftChannel)
                            {
                                BWriter.Write(BReader.ReadBytes(2));
                            }
                            else
                            {
                                BReader.BaseStream.Position += 2;
                            }
                            LeftChannel = !LeftChannel;
                        }
                    }
                    ChannelData = MS_ChannelData.ToArray();
                }
            }
            return ChannelData;
        }
    }
}
