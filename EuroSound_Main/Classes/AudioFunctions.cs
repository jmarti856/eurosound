using EuroSound_Application.SoundBanksEditor;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;

namespace EuroSound_Application.AudioFunctionsLibrary
{
    internal class AudioFunctions
    {
        private MemoryStream AudioSample;

        internal void PlayAudio(WaveOut AudioPlayer, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, int Pan)
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

        internal byte[] GetWavPCMData(string AudioFilePath, int NumberOfChannels, bool ForceMonoConvert)
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
                if (ForceMonoConvert)
                {
                    if (NumberOfChannels >= 2)
                    {
                        byteArray = StereoToMono(Reader.ReadBytes(dataSize));
                    }
                    else
                    {
                        byteArray = Reader.ReadBytes(dataSize);
                    }
                }
                else
                {
                    byteArray = Reader.ReadBytes(dataSize);
                }

                Reader.Close();
                Reader.Dispose();
            }
            catch
            {
                byteArray = null;
            }

            return byteArray;
        }

        private byte[] StereoToMono(byte[] input)
        {
            byte[] output = new byte[input.Length / 2];
            int outputIndex = 0;
            for (int n = 0; n < input.Length; n += 4)
            {
                // copy in the first 16 bit sample
                output[outputIndex++] = input[n];
                output[outputIndex++] = input[n + 1];
            }
            return output;
        }
    }
}
