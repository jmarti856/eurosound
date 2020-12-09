using FunctionsLibrary;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    public partial class Frm_AudioProperties : Form
    {
        EXAudio SelectedAudio, TemporalAudio;
        ProjectFile CurrentFileProperties;
        string SelectedAudioMD5Hash;
        WaveOut _waveOut = new WaveOut();
        MemoryStream AudioSample;
        Dictionary<string, EXAudio> AudiosList;

        public Frm_AudioProperties(EXAudio AudioToCheck, string AudioKey, Dictionary<string, EXAudio> List, ProjectFile FileProperties)
        {
            InitializeComponent();
            SelectedAudio = AudioToCheck;
            SelectedAudioMD5Hash = AudioKey;
            AudiosList = List;
            CurrentFileProperties = FileProperties;
        }

        private void Frm_AudioProperties_Load(object sender, EventArgs e)
        {
            TemporalAudio = new EXAudio
            {
                Name = SelectedAudio.Name,
                Encoding = SelectedAudio.Encoding,
                Flags = SelectedAudio.Flags,
                DataSize = SelectedAudio.DataSize,
                Frequency = SelectedAudio.Frequency,
                RealSize = SelectedAudio.RealSize,
                Channels = SelectedAudio.Channels,
                Bits = SelectedAudio.Bits,
                PSIsample = SelectedAudio.PSIsample,
                LoopOffset = SelectedAudio.LoopOffset,
                Duration = SelectedAudio.Duration,
                PCMdata = SelectedAudio.PCMdata
            };

            UpdateControls();

            /*--Editable Data--*/
            numeric_flags.Value = TemporalAudio.Flags;
            numeric_psi.Value = TemporalAudio.PSIsample;
            numeric_loopOffset.Value = TemporalAudio.LoopOffset;

        }

        private void Button_ReplaceAudio_Click(object sender, EventArgs e)
        {
            string Hash;

            string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files|*.wav", 0);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                Hash = GenericFunctions.CalculateMD5(AudioPath);
                if (!AudiosList.ContainsKey(Hash))
                {
                    TemporalAudio = EXObjectsFunctions.LoadAudioData(AudioPath);
                    EXObjectsFunctions.AddAudioToList(TemporalAudio, Hash, AudiosList);

                    UpdateControls();

                    /*--Editable Data--*/
                    numeric_flags.Value = TemporalAudio.Flags;
                    numeric_psi.Value = TemporalAudio.PSIsample;
                    numeric_loopOffset.Value = TemporalAudio.LoopOffset;
                    Textbox_MD5Hash.Text = Hash;
                }
            }
        }

        private void Button_PlayAudio_Click(object sender, EventArgs e)
        {
            if (_waveOut.PlaybackState == PlaybackState.Stopped)
            {
                AudioSample = new MemoryStream(TemporalAudio.PCMdata);
                IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat(TemporalAudio.Frequency, TemporalAudio.Bits, TemporalAudio.Channels));
                _waveOut.Init(provider);
                _waveOut.Play();
            }
        }

        private void Button_StopAudio_Click(object sender, EventArgs e)
        {
            StopAudio();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            /*--Modify Temporal Audio Values--*/
            TemporalAudio.Flags = int.Parse(numeric_flags.Value.ToString());
            TemporalAudio.PSIsample = int.Parse(numeric_psi.Value.ToString());
            TemporalAudio.LoopOffset = int.Parse(numeric_loopOffset.Value.ToString());

            /*--Update Selected Audio-*/
            SelectedAudio.Name = TemporalAudio.Name;
            SelectedAudio.Encoding = TemporalAudio.Encoding;
            SelectedAudio.Flags = TemporalAudio.Flags;
            SelectedAudio.DataSize = TemporalAudio.DataSize;
            SelectedAudio.Frequency = TemporalAudio.Frequency;
            SelectedAudio.RealSize = TemporalAudio.RealSize;
            SelectedAudio.Channels = TemporalAudio.Channels;
            SelectedAudio.Bits = TemporalAudio.Bits;
            SelectedAudio.PSIsample = TemporalAudio.PSIsample;
            SelectedAudio.LoopOffset = TemporalAudio.LoopOffset;
            SelectedAudio.Duration = TemporalAudio.Duration;
            SelectedAudio.PCMdata = TemporalAudio.PCMdata;

            /*--Close this form--*/
            this.Close();

        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            StopAudio();
            this.Close();
        }

        internal void StopAudio()
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Stop();
                AudioSample.Close();
                AudioSample.Dispose();
            }
        }

        private void UpdateControls()
        {
            Textbox_MediaName.Text = TemporalAudio.Name;
            Textbox_Encoding.Text = TemporalAudio.Encoding;
            Textbox_DataSize.Text = TemporalAudio.DataSize.ToString();
            Textbox_Frequency.Text = TemporalAudio.Frequency.ToString();
            Textbox_RealSize.Text = TemporalAudio.RealSize.ToString();
            Textbox_Channels.Text = TemporalAudio.Channels.ToString();
            Textbox_Bits.Text = TemporalAudio.Bits.ToString();
            Textbox_Duration.Text = TemporalAudio.Duration.ToString();
            Textbox_MD5Hash.Text = SelectedAudioMD5Hash;
        }
    }
}
