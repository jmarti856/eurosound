using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroSound_Application
{
    internal static class AudioFunctions
    {
        private static MemoryStream AudioSample;

        internal static void PlayAudio( WaveOut _waveOut, byte[] PCMData, int Frequency, int Pitch, int Bits, int Channels, int Pan)
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

        internal static void StopAudio(WaveOut _waveOut)
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
