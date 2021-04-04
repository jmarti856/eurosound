using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_Musics_Properties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private WaveOut _waveOut = new WaveOut();
        private EXMusic SelectedMusic, TemporalMusic;
        private string SelectedMusicKey, MusicName;
        private AudioFunctions AudioLibrary = new AudioFunctions();

        public Frm_Musics_Properties(EXMusic MusicToCheck, string MusicKey, string CurrentMusicName)
        {
            InitializeComponent();

            SelectedMusic = MusicToCheck;
            SelectedMusicKey = MusicKey;
            MusicName = CurrentMusicName;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_Musics_Properties_Load(object sender, EventArgs e)
        {
            //Editable Data
            Numeric_BaseVolume.Value = decimal.Divide(SelectedMusic.BaseVolume, 100);

            //Clone object
            TemporalMusic = new EXMusic
            {
                StartMarkers = new List<EXStreamStartMarker>(SelectedMusic.StartMarkers),
                Markers = new List<EXStreamMarker>(SelectedMusic.Markers),
            };
            Reflection.CopyProperties(SelectedMusic, TemporalMusic);

            //Show Info in Textboxes
            ShowAudioInfo();

            CheckBox_OutputThisMusic.Checked = SelectedMusic.OutputThisSound;
        }

        private void Frm_Musics_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_ReplaceAudio_Click(object sender, System.EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                if (GenericFunctions.AudioIsValid(AudioPath, 2, 32000))
                {
                    LoadData(AudioPath);
                }
                else
                {
                    DialogResult TryToReload = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ErrorWavFileIncorrectMusicsMono"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (TryToReload == DialogResult.Yes)
                    {
                        string FileTempFile = AudioLibrary.ConvertWavToSoundBankValid(AudioPath, Path.GetFileNameWithoutExtension(AudioPath), 32000, 1, 16);
                        if (!string.IsNullOrEmpty(FileTempFile))
                        {
                            LoadData(FileTempFile);
                        }
                    }
                }
            }
        }

        private void Button_SaveAudio_Click(object sender, System.EventArgs e)
        {
            SaveAudio();
        }

        private void Button_MarkersEditor_Click(object sender, System.EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
            Frm_StreamSounds_MarkersEditor MarkersEditr = new Frm_StreamSounds_MarkersEditor(null, TemporalMusic, true)
            {
                Text = GenericFunctions.TruncateLongString(MusicName, 25) + " - Markers",
                Tag = Tag.ToString(),
                Owner = this,
                ShowInTaskbar = false
            };
            MarkersEditr.ShowDialog();
            MarkersEditr.Dispose();
        }

        private void Button_Play_Click(object sender, System.EventArgs e)
        {
            if (TemporalMusic.PCM_Data != null)
            {
                AudioLibrary.PlayAudio(_waveOut, TemporalMusic.PCM_Data, (int)TemporalMusic.Frequency, 1, (int)TemporalMusic.Bits, TemporalMusic.Channels, 0, Numeric_BaseVolume.Value);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Stop_Click(object sender, System.EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            TreeNode[] Results;
            Form OpenForm;

            SelectedMusic.BaseVolume = (uint)(Numeric_BaseVolume.Value * 100);
            SelectedMusic.OutputThisSound = CheckBox_OutputThisMusic.Checked;

            if (TemporalMusic.PCM_Data != null)
            {
                SelectedMusic.Frequency = TemporalMusic.Frequency;
                SelectedMusic.Channels = TemporalMusic.Channels;
                SelectedMusic.Bits = TemporalMusic.Bits;
                SelectedMusic.Duration = TemporalMusic.Duration;
                SelectedMusic.RealSize = TemporalMusic.RealSize;
                SelectedMusic.Encoding = TemporalMusic.Encoding;
                SelectedMusic.WAVFileMD5 = TemporalMusic.WAVFileMD5;
                SelectedMusic.WAVFileName = TemporalMusic.WAVFileName;
                SelectedMusic.PCM_Data_LeftChannel = TemporalMusic.PCM_Data_LeftChannel;
                SelectedMusic.IMA_ADPCM_DATA_LeftChannel = TemporalMusic.IMA_ADPCM_DATA_LeftChannel;
                SelectedMusic.PCM_Data_RightChannel = TemporalMusic.PCM_Data_RightChannel;
                SelectedMusic.IMA_ADPCM_DATA_RightChannel = TemporalMusic.IMA_ADPCM_DATA_RightChannel;
                SelectedMusic.PCM_Data = TemporalMusic.PCM_Data;
                SelectedMusic.StartMarkers = new List<EXStreamStartMarker>(TemporalMusic.StartMarkers);
                SelectedMusic.Markers = new List<EXStreamMarker>(TemporalMusic.Markers);
            }

            //Change node icon
            OpenForm = GenericFunctions.GetFormByName("Frm_Musics_Main", Tag.ToString());
            Results = ((Frm_Musics_Main)OpenForm).TreeView_MusicData.Nodes.Find(SelectedMusicKey, true);

            if (Results.Length > 0)
            {
                if (SelectedMusic.OutputThisSound)
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

        private void ContextMenuAudioSave_Click(object sender, EventArgs e)
        {
            SaveAudio();
        }

        //*===============================================================================================
        //* FUNCTIONS EVENTS
        //*===============================================================================================
        private void LoadData(string AudioPath)
        {
            EngineXImaAdpcm.ImaADPCM_Functions ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Functions();

            TemporalMusic.WAVFileMD5 = GenericFunctions.CalculateMD5(AudioPath);
            TemporalMusic.WAVFileName = Path.GetFileName(AudioPath);

            using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
            {
                TemporalMusic.Channels = (byte)AudioReader.WaveFormat.Channels;
                TemporalMusic.Frequency = (uint)AudioReader.WaveFormat.SampleRate;
                TemporalMusic.RealSize = (uint)new FileInfo(AudioPath).Length;
                TemporalMusic.Bits = (uint)AudioReader.WaveFormat.BitsPerSample;
                TemporalMusic.Encoding = AudioReader.WaveFormat.Encoding.ToString();
                TemporalMusic.Duration = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);

                //Get PCM Data Stereo
                TemporalMusic.PCM_Data = new byte[AudioReader.Length];
                AudioReader.Read(TemporalMusic.PCM_Data, 0, (int)AudioReader.Length);

                //Get Left Channel
                TemporalMusic.PCM_Data_LeftChannel = AudioLibrary.SplitChannels(TemporalMusic.PCM_Data, true);
                TemporalMusic.IMA_ADPCM_DATA_LeftChannel = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalMusic.PCM_Data_LeftChannel), TemporalMusic.PCM_Data_LeftChannel.Length / 2);

                //Get Right Channel
                TemporalMusic.PCM_Data_RightChannel = AudioLibrary.SplitChannels(TemporalMusic.PCM_Data, false);
                TemporalMusic.IMA_ADPCM_DATA_RightChannel = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalMusic.PCM_Data_RightChannel), TemporalMusic.PCM_Data_RightChannel.Length / 2);

                AudioReader.Close();
            }

            if (TemporalMusic != null && TemporalMusic.PCM_Data_LeftChannel != null)
            {
                ShowAudioInfo();
            }
            else
            {
                MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAudioInfo()
        {
            Textbox_IMA_ADPCM.Text = TemporalMusic.WAVFileName;
            Textbox_MD5_Hash.Text = TemporalMusic.WAVFileMD5;
            Textbox_Bits.Text = TemporalMusic.Bits.ToString();
            Textbox_Encoding.Text = TemporalMusic.Encoding.ToUpper();
            Textbox_Channels.Text = TemporalMusic.Channels.ToString();
            Textbox_Frequency.Text = string.Join(" ", new string[] { TemporalMusic.Frequency.ToString(), "Hz" });
            Textbox_RealSize.Text = string.Join(" ", new string[] { TemporalMusic.RealSize.ToString(), "bytes" });

            //Draw audio waves in the UI
            if (TemporalMusic.PCM_Data != null && TemporalMusic.Channels > 0)
            {
                AudioLibrary.DrawAudioWaves(WaveViewer, TemporalMusic, 0);
            }
        }

        private void SaveAudio()
        {
            string SavePath;

            SavePath = GenericFunctions.SaveFileBrowser("WAV Files (*.wav)|*.wav", 0, true, TemporalMusic.WAVFileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                AudioLibrary.CreateWavFile((int)TemporalMusic.Frequency, (int)TemporalMusic.Bits, (int)TemporalMusic.Channels, TemporalMusic.PCM_Data, SavePath);
            }
        }
    }
}
