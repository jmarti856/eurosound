using NAudio.Wave;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_AudioProperties : Form
    {
        EXAudio SelectedAudio, TemporalAudio;
        string SelectedAudioMD5Hash, TemporalAudioHash;
        WaveOut _waveOut = new WaveOut();
        MemoryStream AudioSample;

        public Frm_AudioProperties(EXAudio AudioToCheck, string AudioKey)
        {
            InitializeComponent();
            SelectedAudio = AudioToCheck;
            TemporalAudioHash = AudioKey;
            SelectedAudioMD5Hash = AudioKey;
        }

        private void Frm_AudioProperties_Load(object sender, EventArgs e)
        {
            TemporalAudio = new EXAudio();
            Reflection.CopyProperties(SelectedAudio, TemporalAudio);
            UpdateControls();

            /*--Editable Data--*/
            numeric_flags.Value = TemporalAudio.Flags;
            numeric_psi.Value = TemporalAudio.PSIsample;
            numeric_loopOffset.Value = TemporalAudio.LoopOffset;

            /*--Test WaveViewer--*/
            //set initial speed - it can be configured using UI slider
            euroSound_WaveViewer1.RenderDelay = 0;
            euroSound_WaveViewer1.WaveStream = new RawSourceWaveStream(new MemoryStream(TemporalAudio.PCMdata), new WaveFormat(TemporalAudio.Frequency, TemporalAudio.Bits, TemporalAudio.Channels));
            euroSound_WaveViewer1.InitControl();
        }

        private void Button_ReplaceAudio_Click(object sender, EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files|*.wav", 0);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                TemporalAudioHash = GenericFunctions.CalculateMD5(AudioPath);
                TemporalAudio = EXObjectsFunctions.LoadAudioData(AudioPath);
                UpdateControls();

                /*--Editable Data--*/
                numeric_flags.Value = TemporalAudio.Flags;
                numeric_psi.Value = TemporalAudio.PSIsample;
                numeric_loopOffset.Value = TemporalAudio.LoopOffset;
                Textbox_MD5Hash.Text = TemporalAudioHash;

                euroSound_WaveViewer1.WaveStream = new RawSourceWaveStream(new MemoryStream(TemporalAudio.PCMdata), new WaveFormat(TemporalAudio.Frequency, TemporalAudio.Bits, TemporalAudio.Channels));
                euroSound_WaveViewer1.InitControl();
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
            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
            /*--Add The Audio to the list if has been replaced--*/
            if (!SelectedAudioMD5Hash.Equals(TemporalAudioHash))
            {
                if (!((Frm_Soundbanks_Main)ParentForm).AudioDataDict.ContainsKey(TemporalAudioHash))
                {
                    ((Frm_Soundbanks_Main)ParentForm).AudioDataDict.Remove(SelectedAudioMD5Hash);
                    EXObjectsFunctions.AddAudioToList(TemporalAudio, TemporalAudioHash, ((Frm_Soundbanks_Main)ParentForm).AudioDataDict);
                }
                else
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioPropertiesFormAudioExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                /*--Modify Temporal Audio Values--*/
                TemporalAudio.Flags = int.Parse(numeric_flags.Value.ToString());
                TemporalAudio.PSIsample = int.Parse(numeric_psi.Value.ToString());
                TemporalAudio.LoopOffset = int.Parse(numeric_loopOffset.Value.ToString());

                /*--Update Selected Audio-*/
                SelectedAudio.Flags = TemporalAudio.Flags;
                SelectedAudio.PSIsample = TemporalAudio.PSIsample;
                SelectedAudio.LoopOffset = TemporalAudio.LoopOffset;
            }

            /*--Stop Audio and liberate Memmory*/
            StopAudio();

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

        private void EuroSound_WaveViewer1_OnLineDrawEvent(Point point1, Point point2)
        {
            using (Graphics gr = euroSound_WaveViewer1.CreateGraphics())
            {
                using (Pen linePen = new Pen(Color.DarkBlue, 1))
                {
                    gr.DrawLine(linePen, point1, point2);
                }
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
