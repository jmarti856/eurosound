using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;

namespace EuroSound_Application
{
    internal class AudioFunctions
    {
        private MemoryStream AudioSample;

        internal void PlayAudio(WaveOut _waveOut, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, int Pan)
        {
            if (_waveOut.PlaybackState == PlaybackState.Stopped)
            {
                AudioSample = new MemoryStream(PCMData);
                IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency + Pitch, Bits, Channels));
                VolumeSampleProvider volumeProvider = new VolumeSampleProvider(provider.ToSampleProvider());
                PanningSampleProvider panProvider = new PanningSampleProvider(volumeProvider)
                {
                    Pan = (Pan / 100)
                };
                _waveOut.Volume = 1;
                _waveOut.Init(panProvider);
                _waveOut.Play();
            }
        }

        internal void PlayAudioLoopOffset(WaveOut _waveOut, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, int Pan)
        {
            if (_waveOut.PlaybackState == PlaybackState.Stopped)
            {
                AudioSample = new MemoryStream(PCMData);
                LoopStream loop = new LoopStream(new RawSourceWaveStream(AudioSample, new WaveFormat(Frequency + Pitch, Bits, Channels)));
                VolumeSampleProvider volumeProvider = new VolumeSampleProvider(loop.ToSampleProvider());
                PanningSampleProvider panProvider = new PanningSampleProvider(volumeProvider)
                {
                    Pan = (Pan / 100)
                };
                _waveOut.Volume = 1;
                _waveOut.Init(panProvider);
                _waveOut.Play();
            }
        }

        internal void StopAudio(WaveOut _waveOut)
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Stop();
                AudioSample.Close();
                AudioSample.Dispose();
            }
        }
    }
}
