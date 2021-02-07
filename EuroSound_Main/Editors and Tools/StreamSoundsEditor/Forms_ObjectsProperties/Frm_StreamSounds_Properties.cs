using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_Properties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private WaveOut _waveOut = new WaveOut();
        private EXSoundStream SelectedSound, TemporalSound;
        private string SelectedSoundKey;
        private AudioFunctions AudioLibrary = new AudioFunctions();

        public Frm_StreamSounds_Properties(EXSoundStream SoundToCheck, string SoundKey)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
            SelectedSoundKey = SoundKey;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSounds_Properties_Load(object sender, System.EventArgs e)
        {
            //Editable Data
            Numeric_BaseVolume.Value = decimal.Divide(SelectedSound.BaseVolume, 100);

            TemporalSound = new EXSoundStream();
            Reflection.CopyProperties(SelectedSound, TemporalSound);

            //Show Info in Textboxes
            ShowAudioInfo();

            CheckBox_OutputThisSound.Checked = SelectedSound.OutputThisSound;
        }

        private void Frm_StreamSounds_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_SaveAudio_Click(object sender, EventArgs e)
        {
            SaveAudio();
        }

        private void ContextMenuAudioSave_Click(object sender, EventArgs e)
        {
            SaveAudio();
        }

        private void Button_MarkersEditor_Click(object sender, System.EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
            Frm_StreamSounds_MarkersEditor MarkersEditr = new Frm_StreamSounds_MarkersEditor(SelectedSound)
            {
                Text = "Markers Editor",
                Tag = Tag.ToString(),
                Owner = this,
                ShowInTaskbar = false
            };
            MarkersEditr.ShowDialog();
            MarkersEditr.Dispose();
        }

        private void Button_SearchIMA_Click(object sender, System.EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                if (GenericFunctions.AudioIsValid(AudioPath, 1, 22050))
                {
                    LoadAudio(AudioPath);
                }
                else
                {
                    DialogResult TryToReload = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ErrorWavFileIncorrect"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (TryToReload == DialogResult.Yes)
                    {
                        string FileTempFile = AudioLibrary.ConvertWavToSoundBankValid(AudioPath, Path.GetFileNameWithoutExtension(AudioPath), 22050, 1, 16);
                        if (!string.IsNullOrEmpty(FileTempFile))
                        {
                            LoadAudio(FileTempFile);
                        }
                    }
                }
            }
        }

        private void Button_Play_Click(object sender, EventArgs e)
        {
            if (TemporalSound.PCM_Data != null)
            {
                AudioLibrary.PlayAudio(_waveOut, TemporalSound.PCM_Data, (int)TemporalSound.Frequency, 1, (int)TemporalSound.Bits, (int)TemporalSound.Channels, 0, Numeric_BaseVolume.Value);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            SelectedSound.BaseVolume = (uint)(Numeric_BaseVolume.Value * 100);
            SelectedSound.OutputThisSound = CheckBox_OutputThisSound.Checked;

            if (TemporalSound.PCM_Data != null)
            {
                SelectedSound.Frequency = TemporalSound.Frequency;
                SelectedSound.Channels = TemporalSound.Channels;
                SelectedSound.Bits = TemporalSound.Bits;
                SelectedSound.Duration = TemporalSound.Duration;
                SelectedSound.Encoding = TemporalSound.Encoding;
                SelectedSound.WAVFileMD5 = TemporalSound.WAVFileMD5;
                SelectedSound.WAVFileName = TemporalSound.WAVFileName;
                SelectedSound.PCM_Data = TemporalSound.PCM_Data;
                SelectedSound.IMA_ADPCM_DATA = TemporalSound.IMA_ADPCM_DATA;
                SelectedSound.RealSize = TemporalSound.RealSize;
            }

            //--Change icon in the parent form--
            Form OpenForm = GenericFunctions.GetFormByName("Frm_StreamSoundsEditorMain", Tag.ToString());
            TreeNode[] Results = ((Frm_StreamSoundsEditorMain)OpenForm).TreeView_StreamData.Nodes.Find(SelectedSoundKey, true);
            if (Results.Length > 0)
            {
                if (SelectedSound.OutputThisSound)
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(Results[0], 2, 2);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(Results[0], 5, 5);
                }
            }

            //--Close this form--
            Close();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* FUNCTIONS EVENTS
        //*===============================================================================================
        private void ShowAudioInfo()
        {
            Textbox_Bits.Text = TemporalSound.Bits.ToString();
            Textbox_Encoding.Text = TemporalSound.Encoding.ToUpper();
            Textbox_Channels.Text = TemporalSound.Channels.ToString();
            Textbox_Frequency.Text = string.Join(" ", new string[] { TemporalSound.Frequency.ToString(), "Hz" });
            Textbox_RealSize.Text = string.Join(" ", new string[] { TemporalSound.RealSize.ToString(), "bytes" });
            Textbox_IMA_ADPCM.Text = TemporalSound.WAVFileName;
            Textbox_MD5_Hash.Text = TemporalSound.WAVFileMD5;

            if (TemporalSound.PCM_Data != null)
            {
                AudioLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalSound, 0);
            }
        }

        private void LoadAudio(string AudioPath)
        {
            EngineXImaAdpcm.ImaADPCM_Decoder ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Decoder();

            TemporalSound.WAVFileMD5 = GenericFunctions.CalculateMD5(AudioPath);
            TemporalSound.WAVFileName = Path.GetFileName(AudioPath);

            using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
            {
                TemporalSound.Channels = (byte)AudioReader.WaveFormat.Channels;
                TemporalSound.Frequency = (uint)AudioReader.WaveFormat.SampleRate;
                TemporalSound.RealSize = (uint)new FileInfo(AudioPath).Length;
                TemporalSound.Bits = (uint)AudioReader.WaveFormat.BitsPerSample;
                TemporalSound.Encoding = AudioReader.WaveFormat.Encoding.ToString();
                TemporalSound.Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);

                AudioReader.Close();

                //Get PCM Data
                TemporalSound.PCM_Data = AudioLibrary.GetWavPCMData(AudioPath);

                //Get IMA ADPCM Data
                TemporalSound.IMA_ADPCM_DATA = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalSound.PCM_Data), TemporalSound.PCM_Data.Length / 2);
            }

            if (TemporalSound != null && TemporalSound.PCM_Data != null)
            {
                ShowAudioInfo();
            }
            else
            {
                MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAudio()
        {
            string SavePath;

            SavePath = GenericFunctions.SaveFileBrowser("WAV Files (*.wav)|*.wav", 0, true, TemporalSound.WAVFileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                AudioLibrary.CreateWavFile((int)TemporalSound.Frequency, (int)TemporalSound.Bits, (int)TemporalSound.Channels, TemporalSound.PCM_Data, SavePath);
            }
        }
    }
}
