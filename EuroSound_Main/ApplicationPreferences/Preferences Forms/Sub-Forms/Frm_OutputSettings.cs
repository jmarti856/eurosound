using EuroSound_Application.Clases;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_OutputSettings : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;
        private WaveOut outputSound;
        private AudioFileReader wave;

        public Frm_OutputSettings()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_OutputSettings_Load(object sender, EventArgs e)
        {
            //Get Form
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());

            //Load settings
            Checkbox_PlaySoundOutput.Checked = ((Frm_MainPreferences)OpenForm).PlaySoundWhenOutputTEMPORAL;
            Textbox_SoundPath.Text = ((Frm_MainPreferences)OpenForm).OutputSoundPathTEMPORAL;

            //Activate or deactivate controls
            Textbox_SoundPath.Enabled = Checkbox_PlaySoundOutput.Checked;
            Button_BrowseSound.Enabled = Checkbox_PlaySoundOutput.Checked;
            Button_PlaySound.Enabled = Checkbox_PlaySoundOutput.Checked;
        }

        private void Frm_OutputSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save settings
            ((Frm_MainPreferences)OpenForm).PlaySoundWhenOutputTEMPORAL = Checkbox_PlaySoundOutput.Checked;
            ((Frm_MainPreferences)OpenForm).OutputSoundPathTEMPORAL = Textbox_SoundPath.Text;

            //Stop sound playing
            if (outputSound != null)
            {
                if (outputSound.PlaybackState == PlaybackState.Playing)
                {
                    outputSound.Stop();
                }
                outputSound.Dispose();
            }

            if (wave != null)
            {
                wave.Close();
                wave.Dispose();
            }
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_BrowseSound_Click(object sender, EventArgs e)
        {
            string SelectedPath = BrowsersAndDialogs.FileBrowserDialog("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                Textbox_SoundPath.Text = SelectedPath;
            }
        }

        private void Button_PlaySound_Click(object sender, EventArgs e)
        {
            if (File.Exists(Textbox_SoundPath.Text))
            {
                wave = new AudioFileReader(Textbox_SoundPath.Text);
                OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(wave);
                outputSound = new WaveOut();
                if (outputSound.PlaybackState == PlaybackState.Stopped)
                {
                    outputSound.Init(offsetSampleProvider);
                    outputSound.Play();
                }
            }
        }

        private void Checkbox_PlaySoundOutput_CheckedChanged(object sender, EventArgs e)
        {
            //Activate or deactivate controls
            Textbox_SoundPath.Enabled = Checkbox_PlaySoundOutput.Checked;
            Button_BrowseSound.Enabled = Checkbox_PlaySoundOutput.Checked;
            Button_PlaySound.Enabled = Checkbox_PlaySoundOutput.Checked;
        }
    }
}
