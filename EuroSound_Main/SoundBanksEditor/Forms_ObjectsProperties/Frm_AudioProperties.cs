using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_AudioProperties : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private WaveOut _waveOut = new WaveOut();
        private EXAudio SelectedAudio, TemporalAudio;
        private string SelectedAudioMD5Hash, TemporalAudioHash;
        private AudioFunctions AudioFunctionsLibrary;

        public Frm_AudioProperties(EXAudio AudioToCheck, string AudioKey)
        {
            InitializeComponent();
            SelectedAudio = AudioToCheck;
            TemporalAudioHash = AudioKey;
            SelectedAudioMD5Hash = AudioKey;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_AudioProperties_Load(object sender, EventArgs e)
        {
            AudioFunctionsLibrary = new AudioFunctions();

            TemporalAudio = new EXAudio();
            Reflection.CopyProperties(SelectedAudio, TemporalAudio);
            UpdateControls();

            /*--Editable Data--*/
            Textbox_Flags.Text = TemporalAudio.Flags.ToString();
            numeric_psi.Value = TemporalAudio.PSIsample;
            numeric_loopOffset.Value = TemporalAudio.LoopOffset;

            if (TemporalAudio.PCMdata != null)
            {
                euroSound_WaveViewer1.RenderDelay = 0;
                euroSound_WaveViewer1.WaveStream = new RawSourceWaveStream(new MemoryStream(TemporalAudio.PCMdata), new WaveFormat(TemporalAudio.Frequency, TemporalAudio.Bits, TemporalAudio.Channels));
                euroSound_WaveViewer1.InitControl();
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_ReplaceAudio_Click(object sender, EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files (*.wav)|*.wav", 0);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                TemporalAudioHash = GenericFunctions.CalculateMD5(AudioPath);
                TemporalAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);

                if (TemporalAudio.PCMdata != null)
                {
                    UpdateControls();

                    /*--Editable Data--*/
                    Textbox_Flags.Text = TemporalAudio.Flags.ToString();
                    numeric_psi.Value = TemporalAudio.PSIsample;
                    numeric_loopOffset.Value = TemporalAudio.LoopOffset;
                    Textbox_MD5Hash.Text = TemporalAudioHash;

                    euroSound_WaveViewer1.WaveStream = new RawSourceWaveStream(new MemoryStream(TemporalAudio.PCMdata), new WaveFormat(TemporalAudio.Frequency, TemporalAudio.Bits, TemporalAudio.Channels));
                    euroSound_WaveViewer1.InitControl();
                }
                else
                {
                    MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Textbox_Flags_MouseClick(object sender, MouseEventArgs e)
        {
            string[] FlagsLabels = new string[]
            {
                "UseLoopOffset"
            };
            EuroSound_FlagsForm FormFlags = new EuroSound_FlagsForm(int.Parse(Textbox_Flags.Text), FlagsLabels, 1)
            {
                Text = "Audio Data Flags",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            if (FormFlags.ShowDialog() == DialogResult.OK)
            {
                Textbox_Flags.Text = FormFlags.CheckedFlags.ToString();
            }
            FormFlags.Dispose();
        }

        private void Button_TestLoopOffset_Click(object sender, EventArgs e)
        {
            if (int.Parse(Textbox_Flags.Text) > 0)
            {
                byte[] LoopSamples;
                int SamplesToSkip = (int.Parse(numeric_loopOffset.Value.ToString()) * 2);

                LoopSamples = TemporalAudio.PCMdata.Skip(SamplesToSkip).ToArray();
                AudioFunctionsLibrary.PlayAudioLoopOffset(_waveOut, LoopSamples, TemporalAudio.Frequency, 0, TemporalAudio.Bits, TemporalAudio.Channels, 0);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioNotUseOffset"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button_PlayAudio_Click(object sender, EventArgs e)
        {
            if (TemporalAudio.PCMdata != null)
            {
                AudioFunctionsLibrary.PlayAudio(_waveOut, TemporalAudio.PCMdata, TemporalAudio.Frequency, 0, TemporalAudio.Bits, TemporalAudio.Channels, 0);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_StopAudio_Click(object sender, EventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
        }

        private void EuroSound_WaveViewer1_OnLineDrawEvent(Point point1, Point point2)
        {
            try
            {
                using (Graphics gr = euroSound_WaveViewer1.CreateGraphics())
                {
                    using (Pen linePen = new Pen(Color.DarkBlue, 1))
                    {
                        gr.DrawLine(linePen, point1, point2);
                    }
                }
            }
            catch
            {
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
            /*--Add The Audio to the list if has been replaced--*/
            if (!SelectedAudioMD5Hash.Equals(TemporalAudioHash))
            {
                if (!((Frm_Soundbanks_Main)ParentForm).AudioDataDict.ContainsKey(TemporalAudioHash))
                {
                    /*--Update Dictionary--*/
                    ((Frm_Soundbanks_Main)ParentForm).AudioDataDict.Remove(SelectedAudioMD5Hash);
                    EXSoundbanksFunctions.AddAudioToList(TemporalAudio, TemporalAudioHash, ((Frm_Soundbanks_Main)ParentForm).AudioDataDict);

                    /*--Update Tree View--*/
                    TreeNode[] Node = ((Frm_Soundbanks_Main)ParentForm).TreeView_File.Nodes.Find(SelectedAudioMD5Hash, true);
                    Node[0].Name = TemporalAudioHash;
                }
                else
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioPropertiesFormAudioExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                /*--Add PCM data if the stored one is null--*/
                if (SelectedAudio.PCMdata == null)
                {
                    SelectedAudio.PCMdata = TemporalAudio.PCMdata;
                }

                /*--Modify Temporal Audio Values--*/
                TemporalAudio.Flags = int.Parse(Textbox_Flags.Text.ToString());
                TemporalAudio.PSIsample = int.Parse(numeric_psi.Value.ToString());
                TemporalAudio.LoopOffset = int.Parse(numeric_loopOffset.Value.ToString());

                /*--Update Selected Audio-*/
                SelectedAudio.Flags = TemporalAudio.Flags;
                SelectedAudio.PSIsample = TemporalAudio.PSIsample;
                SelectedAudio.LoopOffset = TemporalAudio.LoopOffset;
            }

            /*--Stop Audio and liberate Memmory*/
            AudioFunctionsLibrary.StopAudio(_waveOut);

            /*--Close this form--*/
            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
            this.Close();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
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