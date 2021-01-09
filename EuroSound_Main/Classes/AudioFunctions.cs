using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SoxSharp;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.AudioFunctionsLibrary
{
    internal class AudioFunctions
    {
        private MemoryStream AudioSample;

        internal void PlayAudio(WaveOut AudioPlayer, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, int Pan)
        {
            if (Frequency != 0)
            {
                if (AudioPlayer.PlaybackState == PlaybackState.Stopped)
                {
                    AudioSample = new MemoryStream(PCMData);
                    IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency + Pitch, Bits, Channels));
                    VolumeSampleProvider volumeProvider = new VolumeSampleProvider(provider.ToSampleProvider());
                    AudioPlayer.Volume = 1;

                    //Pan is only for mono audio
                    if (Channels == 1)
                    {
                        PanningSampleProvider panProvider = new PanningSampleProvider(volumeProvider)
                        {
                            Pan = (Pan / 100)
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

        internal void DrawAudioWaves(EuroSound_WaveViewer ControlToDraw, EXAudio SelectedSound, int Delay)
        {
            /*Draw Waves*/
            if (SelectedSound.PCMdata != null && SelectedSound.Channels > 0)
            {
                ControlToDraw.RenderDelay = Delay;
                ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(SelectedSound.PCMdata), new WaveFormat((int)SelectedSound.Frequency, (int)SelectedSound.Bits, (int)SelectedSound.Channels));
                ControlToDraw.InitControl();
            }
        }

        internal void DrawAudioWaves(EuroSound_WaveViewer ControlToDraw, EXSoundStream SelectedSound, int Delay)
        {
            /*Draw Waves*/
            if (SelectedSound.PCM_Data != null && SelectedSound.Channels > 0)
            {
                ControlToDraw.RenderDelay = Delay;
                ControlToDraw.WaveStream = new RawSourceWaveStream(new MemoryStream(SelectedSound.PCM_Data), new WaveFormat((int)SelectedSound.Frequency, (int)SelectedSound.Bits, (int)SelectedSound.Channels));
                ControlToDraw.InitControl();
            }
        }

        internal byte[] GetWavPCMData(string AudioFilePath)
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

        internal string ConvertWavToSoundBankValid(string SourcePath, string FileName)
        {
            string FinalFile = string.Empty;

            //Create folder in %temp%
            GenericFunctions.CreateTemporalFolder();

            //Resample wav
            if (File.Exists(GlobalPreferences.SoXPath))
            {
                FinalFile = string.Format("{0}{1}f.wav", Path.GetTempPath() + @"EuroSound\", FileName);
                using (Sox sox = new Sox(GlobalPreferences.SoXPath))
                {
                    sox.Output.Type = FileType.WAV;
                    sox.Output.SampleRate = 22050;
                    sox.Output.Channels = 1;

                    InputFile testInput = new InputFile(SourcePath);
                    sox.Process(testInput, FinalFile);
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("SoXInvalidPath"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return FinalFile;
        }

        public string ConvertWavToIMAADPCM(string SourcePath, string FileName)
        {
            string FinalFile = string.Empty;

            //Create folder in %temp%
            GenericFunctions.CreateTemporalFolder();

            //Resample wav
            if (File.Exists(GlobalPreferences.SoXPath))
            {
                FinalFile = string.Format("{0}{1}f.ima", Path.GetTempPath() + @"EuroSound\", FileName);
                using (Sox sox = new Sox(GlobalPreferences.SoXPath))
                {
                    sox.Output.Type = FileType.IMA;
                    sox.Output.SampleRate = 22050;
                    sox.Output.Channels = 1;

                    InputFile testInput = new InputFile(SourcePath);
                    sox.Process(testInput, FinalFile);
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("SoXInvalidPath"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return FinalFile;
        }

        internal void CreateWavFile(int Frequency, int BitsPerChannel, int NumberOfChannels, byte[] SampleData, string FilePath)
        {
            MemoryStream AudioSample = new MemoryStream(SampleData);
            IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency, BitsPerChannel, NumberOfChannels));

            WriteWavFile(FilePath, provider);
        }

        private void WriteWavFile(string SavePath, IWaveProvider sourceProvider)
        {
            using (WaveFileWriter writer = new WaveFileWriter(SavePath, sourceProvider.WaveFormat))
            {
                byte[] buffer = new byte[sourceProvider.WaveFormat.AverageBytesPerSecond * 4];
                while (true)
                {
                    int bytesRead = sourceProvider.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    writer.Write(buffer, 0, bytesRead);
                }
            }
        }
    }
}
