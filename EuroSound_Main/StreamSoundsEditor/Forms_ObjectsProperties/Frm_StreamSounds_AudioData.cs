using NAudio.Wave;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSounds_AudioData : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private WaveOut _waveOut = new WaveOut();
        private AudioFunctions AudioFunctionsLibrary = new AudioFunctions();
        private EXSoundStream SelectedSound, TemporalAudio;
        private string TemporalPCM_Data_MD5, TemporalIMA_Data_MD5;

        public Frm_StreamSounds_AudioData(EXSoundStream SoundToCheck)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_StreamSounds_AudioData_Load(object sender, System.EventArgs e)
        {
            euroSound_WaveViewer1.BackColor = Color.FromArgb(GlobalPreferences.BackColorWavesControl);

            /*Copy Wav data properties*/
            TemporalAudio = new EXSoundStream();
            Reflection.CopyProperties(SelectedSound.WAV_Audio, TemporalAudio.WAV_Audio);

            /*Copy Object properties*/
            TemporalIMA_Data_MD5 = SelectedSound.IMA_Data_MD5;
            TemporalPCM_Data_MD5 = SelectedSound.PCM_Data_MD5;

            TemporalAudio.IMA_Data_MD5 = TemporalIMA_Data_MD5;
            TemporalAudio.IMA_Data_Name = SelectedSound.IMA_Data_Name;
            TemporalAudio.IMA_ADPCM_DATA = SelectedSound.IMA_ADPCM_DATA;

            TemporalAudio.PCM_Data_MD5 = TemporalPCM_Data_MD5;
            TemporalAudio.PCM_Data_Name = SelectedSound.PCM_Data_Name;

            /*Update GUI*/
            ShowAudioDataOnControls();
            AudioFunctionsLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalAudio.WAV_Audio, 0);
        }

        private void Frm_StreamSounds_AudioData_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
        }

        //*===============================================================================================
        //* Form Controls Events
        //*===============================================================================================
        private void Button_SearchPCM_Click(object sender, System.EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files (*.wav)|*.wav", 0);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                TemporalPCM_Data_MD5 = GenericFunctions.CalculateMD5(AudioPath);
                TemporalAudio.PCM_Data_MD5 = TemporalPCM_Data_MD5;
                TemporalAudio.WAV_Audio = EXSoundbanksFunctions.LoadAudioData(AudioPath, false);
                TemporalAudio.PCM_Data_Name = Path.GetFileName(AudioPath);

                if (TemporalAudio.WAV_Audio.PCMdata != null)
                {
                    ShowAudioDataOnControls();

                    /*--Editable Data--*/
                    Textbox_MD5Hash.Text = TemporalPCM_Data_MD5;
                    AudioFunctionsLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalAudio.WAV_Audio, 0);
                }
                else
                {
                    MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button_SearchIMA_Click(object sender, System.EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("IMA ADPCM Files (*.ima)|*.ima", 0);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                TemporalIMA_Data_MD5 = GenericFunctions.CalculateMD5(AudioPath);
                TemporalAudio.IMA_Data_MD5 = TemporalIMA_Data_MD5;
                TemporalAudio.IMA_ADPCM_DATA = File.ReadAllBytes(AudioPath);
                TemporalAudio.IMA_Data_Name = Path.GetFileName(AudioPath);

                if (TemporalAudio.IMA_ADPCM_DATA != null)
                {
                    ShowAudioDataOnControls();
                }
                else
                {
                    MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button_PlayAudio_Click(object sender, System.EventArgs e)
        {
            if (TemporalAudio.WAV_Audio.PCMdata != null)
            {
                AudioFunctionsLibrary.PlayAudio(_waveOut, TemporalAudio.WAV_Audio.PCMdata, (int)TemporalAudio.WAV_Audio.Frequency, 0, (int)TemporalAudio.WAV_Audio.Bits, (int)TemporalAudio.WAV_Audio.Channels, 0);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_StopAudio_Click(object sender, System.EventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
        }

        private void Button_Ok_Click(object sender, System.EventArgs e)
        {
            if (!TemporalPCM_Data_MD5.Equals(SelectedSound.PCM_Data_MD5))
            {
                SelectedSound.PCM_Data_MD5 = TemporalAudio.PCM_Data_MD5;
                SelectedSound.PCM_Data_Name = TemporalAudio.PCM_Data_Name;
                Reflection.CopyProperties(TemporalAudio.WAV_Audio, SelectedSound.WAV_Audio);
            }

            if (!TemporalIMA_Data_MD5.Equals(SelectedSound.IMA_Data_MD5))
            {
                SelectedSound.IMA_ADPCM_DATA = TemporalAudio.IMA_ADPCM_DATA;
                SelectedSound.IMA_Data_MD5 = TemporalAudio.IMA_Data_MD5;
                SelectedSound.IMA_Data_Name = TemporalAudio.IMA_Data_Name;
            }

            Close();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        private void ShowAudioDataOnControls()
        {
            Textbox_PCM_Data.Text = TemporalAudio.PCM_Data_Name;
            Textbox_IMA_ADPCM.Text = TemporalAudio.IMA_Data_Name;
            Textbox_DataSize.Text = TemporalAudio.WAV_Audio.DataSize.ToString();
            Textbox_Channels.Text = TemporalAudio.WAV_Audio.Channels.ToString();
            Textbox_Encoding.Text = TemporalAudio.WAV_Audio.Encoding.ToString();
            Textbox_Frequency.Text = TemporalAudio.WAV_Audio.Frequency.ToString();
            Textbox_Bits.Text = TemporalAudio.WAV_Audio.Bits.ToString();
            Textbox_RealSize.Text = TemporalAudio.WAV_Audio.RealSize.ToString();
            Textbox_Duration.Text = TemporalAudio.WAV_Audio.Duration.ToString();

            Textbox_MD5Hash.Text = TemporalAudio.PCM_Data_MD5;
        }
    }
}
