using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace EuroSound
{
    public partial class Frm_SampleProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        EXAudio TemporalAudio;
        EXSample SelectedSample;
        WaveOut _waveOut = new WaveOut();
        MemoryStream AudioSample;

        public Frm_SampleProperties(EXSample Sample)
        {
            InitializeComponent();
            SelectedSample = Sample;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_SampleProperties_Load(object sender, EventArgs e)
        {
            numeric_pitchoffset.Value = SelectedSample.PitchOffset;
            numeric_randomPitchOffset.Value = SelectedSample.RandomPitchOffset;
            Numeric_BaseVolume.Value = SelectedSample.BaseVolume;
            numeric_randomvolumeoffset.Value = SelectedSample.RandomVolumeOffset;
            numeric_pan.Value = SelectedSample.Pan;
            numeric_randompan.Value = SelectedSample.RandomPan;
            Checkbox_IsStreamedSound.Checked = SelectedSample.IsStreamed;

            if (SelectedSample.Audio.IsEmpty() == false)
            {
                Textbox_MediaName.Text = SelectedSample.Audio.Name;
                TemporalAudio = new EXAudio
                {
                    Name = SelectedSample.Audio.Name,
                    DataSize = SelectedSample.Audio.DataSize,
                    Frequency = SelectedSample.Audio.Frequency,
                    RealSize = SelectedSample.Audio.RealSize,
                    Channels = SelectedSample.Audio.Channels,
                    Bits = SelectedSample.Audio.Bits,
                    Duration = SelectedSample.Audio.Duration,
                    AllData = SelectedSample.Audio.AllData,
                    Encoding = SelectedSample.Audio.Encoding,
                    PCMdata = SelectedSample.Audio.PCMdata
                };
            }
            else
            {
                TemporalAudio = new EXAudio();
            }
        }

        private void Frm_SampleProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAudio();
            _waveOut.Dispose();
            if (AudioSample != null)
            {
                AudioSample.Close();
                AudioSample.Dispose();
                AudioSample.Flush();
            }
        }

        //*===============================================================================================
        //* Control Events
        //*===============================================================================================
        private void Button_LoadAudio_Click(object sender, EventArgs e)
        {
            string AudioFilePath = Generic.OpenFileBrowser("Wav Files (*.wav)|*.wav", 0);

            if (File.Exists(AudioFilePath))
            {
                WaveFileReader AudioReader = new WaveFileReader(AudioFilePath);

                if (!(AudioReader.WaveFormat.Encoding == WaveFormatEncoding.Pcm))
                {
                    MessageBox.Show("Encoding invalid, the format encoding must be PCM", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TemporalAudio = new EXAudio
                    {
                        Name = Path.GetFileName(AudioFilePath),
                        DataSize = Convert.ToInt32(AudioReader.Length),
                        Frequency = AudioReader.WaveFormat.SampleRate,
                        RealSize = Convert.ToInt32(new FileInfo(AudioFilePath).Length),
                        Channels = AudioReader.WaveFormat.Channels,
                        Bits = AudioReader.WaveFormat.BitsPerSample,
                        Duration = Convert.ToInt32(Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1)),
                        AllData = File.ReadAllBytes(AudioFilePath),
                        Encoding = AudioReader.WaveFormat.Encoding.ToString()
                    };

                    AudioReader.Close();
                    AudioReader.Dispose();
                    AudioReader.Flush();

                    /*Get PCM data*/
                    TemporalAudio.PCMdata = EXObjectsFunctions.GetRawPCMData(AudioFilePath);

                    Textbox_MediaName.Text = TemporalAudio.Name;
                }
            }
        }

        private void Button_PlayAudio_Click(object sender, EventArgs e)
        {
            if (TemporalAudio.IsEmpty())
            {
                MessageBox.Show("That sample does not contain audio", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_waveOut.PlaybackState == PlaybackState.Stopped)
                {
                    AudioSample = new MemoryStream(TemporalAudio.AllData);
                    IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(TemporalAudio.Frequency, TemporalAudio.Bits, TemporalAudio.Channels));
                    _waveOut.Init(provider);
                    _waveOut.Play();
                }
            }
        }

        private void Button_StopAudio_Click(object sender, EventArgs e)
        {
            StopAudio();
        }

        private void Button_RemoveAudio_Click(object sender, EventArgs e)
        {
            if (!TemporalAudio.IsEmpty())
            {
                Textbox_MediaName.Text = "<NO AUDIO>";
                TemporalAudio = new EXAudio();
            }
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            SelectedSample.PitchOffset = Convert.ToInt32(numeric_pitchoffset.Value);
            SelectedSample.RandomPitchOffset = Convert.ToInt32(numeric_randomPitchOffset.Value);
            SelectedSample.BaseVolume = Convert.ToInt32(Numeric_BaseVolume.Value);
            SelectedSample.RandomVolumeOffset = Convert.ToInt32(numeric_randomvolumeoffset.Value);
            SelectedSample.Pan = Convert.ToInt32(numeric_pan.Value);
            SelectedSample.RandomPan = Convert.ToInt32(numeric_randompan.Value);
            SelectedSample.IsStreamed = Checkbox_IsStreamedSound.Checked;

            if (!TemporalAudio.IsEmpty())
            {
                /*Temporal to "final" audio*/
                SelectedSample.Audio.Name = TemporalAudio.Name;
                SelectedSample.Audio.DataSize = TemporalAudio.DataSize;
                SelectedSample.Audio.Frequency = TemporalAudio.Frequency;
                SelectedSample.Audio.RealSize = TemporalAudio.RealSize;
                SelectedSample.Audio.Channels = TemporalAudio.Channels;
                SelectedSample.Audio.Bits = TemporalAudio.Bits;
                SelectedSample.Audio.Duration = TemporalAudio.Duration;
                SelectedSample.Audio.AllData = TemporalAudio.AllData;
                SelectedSample.Audio.Encoding = TemporalAudio.Encoding;
                SelectedSample.Audio.PCMdata = TemporalAudio.PCMdata;
            }
            else
            {
                SelectedSample.RemoveAudio();
            }

            this.Close();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        internal void StopAudio()
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Stop();
            }
        }

        private void Checkbox_IsStreamedSound_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_IsStreamedSound.Checked)
            {
                EnableOrDisableMediaSection(false);
            }
            else
            {
                EnableOrDisableMediaSection(true);
            }
        }

        private void EnableOrDisableMediaSection(bool Action)
        {
            Textbox_MediaName.Enabled = Action;
            Button_LoadAudio.Enabled = Action;
            Button_PlayAudio.Enabled = Action;
            Button_StopAudio.Enabled = Action;
            button_RemoveAudio.Enabled = Action;
        }
    }
}