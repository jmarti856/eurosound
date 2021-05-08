using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.Clases;
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
        private AudioFunctions AudioLibrary = new AudioFunctions();
        private WaveOut _waveOut = new WaveOut();
        private EXMusic SelectedMusic, TemporalMusic;
        private string SelectedMusicKey, MusicName;

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
            ShowAudioInfoLeftChannel();
            ShowAudioInfoRightChannel();

            CheckBox_OutputThisMusic.Checked = SelectedMusic.OutputThisSound;
        }

        private void Frm_Musics_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_ReplaceAudio_LeftChannel_Click(object sender, System.EventArgs e)
        {
            string AudioPath = BrowsersAndDialogs.FileBrowserDialog("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.MusicbankChannels, GlobalPreferences.MusicbankFrequency))
                {
                    LoadDataLeftChannel(AudioPath, false);
                }
                else
                {
                    DialogResult TryToReload = MessageBox.Show(string.Join("", "Error, this audio file is not correct, the specifies are: ", GlobalPreferences.MusicbankChannels, " channels, the rate must be ", GlobalPreferences.MusicbankFrequency, "Hz, must have ", GlobalPreferences.MusicbankBits, " bits per sample and encoded in ", GlobalPreferences.MusicbankEncoding, ".\n\nDo you want that EuroSound tries to convert it to a valid format?"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (TryToReload == DialogResult.Yes)
                    {
                        LoadDataLeftChannel(AudioPath, true);
                    }
                }
            }
        }

        private void Button_SaveAudio_LeftChannel_Click(object sender, System.EventArgs e)
        {
            GenericFunctions.SaveAudio(AudioLibrary, TemporalMusic.WAVFileName_LeftChannel, (int)TemporalMusic.Frequency_LeftChannel, (int)TemporalMusic.Bits_LeftChannel, TemporalMusic.Channels_LeftChannel, TemporalMusic.PCM_Data_LeftChannel);
        }

        private void Button_ReplaceAudio_RightChannel_Click(object sender, EventArgs e)
        {
            string AudioPath = BrowsersAndDialogs.FileBrowserDialog("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.MusicbankChannels, GlobalPreferences.MusicbankFrequency))
                {
                    LoadDataRightChannel(AudioPath, false);
                }
                else
                {
                    DialogResult TryToReload = MessageBox.Show(string.Join("", "Error, this audio file is not correct, the specifies are: ", GlobalPreferences.MusicbankChannels, " channels, the rate must be ", GlobalPreferences.MusicbankFrequency, "Hz, must have ", GlobalPreferences.MusicbankBits, " bits per sample and encoded in ", GlobalPreferences.MusicbankEncoding, ".\n\nDo you want that EuroSound tries to convert it to a valid format?"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (TryToReload == DialogResult.Yes)
                    {
                        LoadDataRightChannel(AudioPath, true);
                    }
                }
            }
        }

        private void Button_SaveAudio_RightChannel_Click(object sender, EventArgs e)
        {
            GenericFunctions.SaveAudio(AudioLibrary, TemporalMusic.WAVFileName_RightChannel, (int)TemporalMusic.Frequency_RightChannel, (int)TemporalMusic.Bits_RightChannel, TemporalMusic.Channels_RightChannel, TemporalMusic.PCM_Data_RightChannel);
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

        private void Button_Play_LeftChannel_Click(object sender, System.EventArgs e)
        {
            if (TemporalMusic.PCM_Data_LeftChannel != null)
            {
                AudioLibrary.PlayAudio(_waveOut, TemporalMusic.PCM_Data_LeftChannel, (int)TemporalMusic.Frequency_LeftChannel, GlobalPreferences.MusicbankChannels, (int)TemporalMusic.Bits_LeftChannel, TemporalMusic.Channels_LeftChannel, 0, Numeric_BaseVolume.Value);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Stop_LeftChannel_Click(object sender, System.EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        private void Button_Play_RightChannel_Click(object sender, EventArgs e)
        {
            if (TemporalMusic.PCM_Data_RightChannel != null)
            {
                AudioLibrary.PlayAudio(_waveOut, TemporalMusic.PCM_Data_RightChannel, (int)TemporalMusic.Frequency_RightChannel, GlobalPreferences.MusicbankChannels, (int)TemporalMusic.Bits_RightChannel, TemporalMusic.Channels_RightChannel, 0, Numeric_BaseVolume.Value);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Stop_RightChannel_Click(object sender, EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        private void Button_Play_Click(object sender, EventArgs e)
        {
            AudioLibrary.PlayAudioMultiplexing(_waveOut, TemporalMusic.PCM_Data_LeftChannel, TemporalMusic.PCM_Data_RightChannel, (int)TemporalMusic.Frequency_LeftChannel, (int)TemporalMusic.Bits_LeftChannel, GlobalPreferences.MusicbankChannels, Numeric_BaseVolume.Value);
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            SelectedMusic.BaseVolume = (uint)(Numeric_BaseVolume.Value * 100);
            SelectedMusic.OutputThisSound = CheckBox_OutputThisMusic.Checked;

            if (TemporalMusic.PCM_Data_LeftChannel != null && TemporalMusic.PCM_Data_RightChannel != null)
            {
                //Save Left Channel
                SelectedMusic.Frequency_LeftChannel = TemporalMusic.Frequency_LeftChannel;
                SelectedMusic.Channels_LeftChannel = TemporalMusic.Channels_LeftChannel;
                SelectedMusic.Bits_LeftChannel = TemporalMusic.Bits_LeftChannel;
                SelectedMusic.Duration_LeftChannel = TemporalMusic.Duration_LeftChannel;
                SelectedMusic.RealSize_LeftChannel = TemporalMusic.RealSize_LeftChannel;
                SelectedMusic.Encoding_LeftChannel = TemporalMusic.Encoding_LeftChannel;
                SelectedMusic.WAVFileMD5_LeftChannel = TemporalMusic.WAVFileMD5_LeftChannel;
                SelectedMusic.WAVFileName_LeftChannel = TemporalMusic.WAVFileName_LeftChannel;
                SelectedMusic.PCM_Data_LeftChannel = TemporalMusic.PCM_Data_LeftChannel;
                SelectedMusic.IMA_ADPCM_DATA_LeftChannel = TemporalMusic.IMA_ADPCM_DATA_LeftChannel;
                SelectedMusic.PCM_Data_RightChannel = TemporalMusic.PCM_Data_RightChannel;
                SelectedMusic.IMA_ADPCM_DATA_RightChannel = TemporalMusic.IMA_ADPCM_DATA_RightChannel;

                //Save Right Channel
                SelectedMusic.Frequency_RightChannel = TemporalMusic.Frequency_RightChannel;
                SelectedMusic.Channels_RightChannel = TemporalMusic.Channels_RightChannel;
                SelectedMusic.Bits_RightChannel = TemporalMusic.Bits_RightChannel;
                SelectedMusic.Duration_RightChannel = TemporalMusic.Duration_RightChannel;
                SelectedMusic.RealSize_RightChannel = TemporalMusic.RealSize_RightChannel;
                SelectedMusic.Encoding_RightChannel = TemporalMusic.Encoding_RightChannel;
                SelectedMusic.WAVFileMD5_RightChannel = TemporalMusic.WAVFileMD5_RightChannel;
                SelectedMusic.WAVFileName_RightChannel = TemporalMusic.WAVFileName_RightChannel;
                SelectedMusic.PCM_Data_RightChannel = TemporalMusic.PCM_Data_RightChannel;
                SelectedMusic.IMA_ADPCM_DATA_RightChannel = TemporalMusic.IMA_ADPCM_DATA_RightChannel;
                SelectedMusic.PCM_Data_RightChannel = TemporalMusic.PCM_Data_RightChannel;
                SelectedMusic.IMA_ADPCM_DATA_RightChannel = TemporalMusic.IMA_ADPCM_DATA_RightChannel;

                //Save Global 
                SelectedMusic.StartMarkers = new List<EXStreamStartMarker>(TemporalMusic.StartMarkers);
                SelectedMusic.Markers = new List<EXStreamMarker>(TemporalMusic.Markers);
            }

            //Change node icon
            Form OpenForm = GenericFunctions.GetFormByName("Frm_Musics_Main", Tag.ToString());
            TreeNode[] Results = ((Frm_Musics_Main)OpenForm).TreeView_MusicData.Nodes.Find(SelectedMusicKey, true);

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

        //*===============================================================================================
        //* FUNCTIONS EVENTS
        //*===============================================================================================
        private void LoadDataLeftChannel(string AudioPath, bool ConvertAudio)
        {
            TemporalMusic.WAVFileMD5_LeftChannel = GenericFunctions.CalculateMD5(AudioPath);
            TemporalMusic.WAVFileName_LeftChannel = Path.GetFileName(AudioPath);

            using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
            {
                EngineXImaAdpcm.ImaADPCM_Functions ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Functions();
                if (ConvertAudio)
                {
                    using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(AudioReader, new WaveFormat(GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels)))
                    {
                        TemporalMusic.Channels_LeftChannel = (byte)conversionStream.WaveFormat.Channels;
                        TemporalMusic.Frequency_LeftChannel = (uint)conversionStream.WaveFormat.SampleRate;
                        TemporalMusic.RealSize_LeftChannel = (uint)new FileInfo(AudioPath).Length;
                        TemporalMusic.Bits_LeftChannel = (uint)conversionStream.WaveFormat.BitsPerSample;
                        TemporalMusic.Encoding_LeftChannel = conversionStream.WaveFormat.Encoding.ToString();

                        //Get PCM Data Stereo
                        using (MemoryStream outStream = new MemoryStream())
                        {
                            byte[] bytes = new byte[conversionStream.WaveFormat.AverageBytesPerSecond * 4];
                            while (true)
                            {
                                int bytesRead = conversionStream.Read(bytes, 0, bytes.Length);
                                if (bytesRead == 0)
                                {
                                    break;
                                }
                                outStream.Write(bytes, 0, bytesRead);
                            }
                            TemporalMusic.PCM_Data_LeftChannel = outStream.ToArray();
                        }

                        //Other props
                        TemporalMusic.Duration_LeftChannel = (uint)(decimal.Divide(TemporalMusic.PCM_Data_LeftChannel.Length, conversionStream.WaveFormat.AverageBytesPerSecond) * 1000);

                        //Get IMA ADPCM Data
                        TemporalMusic.IMA_ADPCM_DATA_LeftChannel = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalMusic.PCM_Data_LeftChannel), TemporalMusic.PCM_Data_LeftChannel.Length / 2);
                    }
                }
                else
                {
                    TemporalMusic.Channels_LeftChannel = (byte)AudioReader.WaveFormat.Channels;
                    TemporalMusic.Frequency_LeftChannel = (uint)AudioReader.WaveFormat.SampleRate;
                    TemporalMusic.RealSize_LeftChannel = (uint)new FileInfo(AudioPath).Length;
                    TemporalMusic.Bits_LeftChannel = (uint)AudioReader.WaveFormat.BitsPerSample;
                    TemporalMusic.Encoding_LeftChannel = AudioReader.WaveFormat.Encoding.ToString();
                    TemporalMusic.Duration_LeftChannel = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);

                    //Get PCM Data Stereo
                    TemporalMusic.PCM_Data_LeftChannel = new byte[AudioReader.Length];
                    AudioReader.Read(TemporalMusic.PCM_Data_LeftChannel, 0, (int)AudioReader.Length);

                    //Get IMA ADPCM Data
                    TemporalMusic.IMA_ADPCM_DATA_LeftChannel = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalMusic.PCM_Data_LeftChannel), TemporalMusic.PCM_Data_LeftChannel.Length / 2);
                }
                AudioReader.Close();
            }

            if (TemporalMusic != null && TemporalMusic.PCM_Data_LeftChannel != null)
            {
                ShowAudioInfoLeftChannel();
            }
            else
            {
                MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataRightChannel(string AudioPath, bool ConvertAudio)
        {
            TemporalMusic.WAVFileMD5_RightChannel = GenericFunctions.CalculateMD5(AudioPath);
            TemporalMusic.WAVFileName_RightChannel = Path.GetFileName(AudioPath);

            using (WaveFileReader AudioReader = new WaveFileReader(AudioPath))
            {
                EngineXImaAdpcm.ImaADPCM_Functions ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Functions();
                if (ConvertAudio)
                {
                    using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(AudioReader, new WaveFormat(GlobalPreferences.MusicbankFrequency, GlobalPreferences.MusicbankBits, GlobalPreferences.MusicbankChannels)))
                    {
                        TemporalMusic.Channels_RightChannel = (byte)conversionStream.WaveFormat.Channels;
                        TemporalMusic.Frequency_RightChannel = (uint)conversionStream.WaveFormat.SampleRate;
                        TemporalMusic.RealSize_RightChannel = (uint)new FileInfo(AudioPath).Length;
                        TemporalMusic.Bits_RightChannel = (uint)conversionStream.WaveFormat.BitsPerSample;
                        TemporalMusic.Encoding_RightChannel = conversionStream.WaveFormat.Encoding.ToString();

                        //Get PCM Data Stereo
                        using (MemoryStream outStream = new MemoryStream())
                        {
                            byte[] bytes = new byte[conversionStream.WaveFormat.AverageBytesPerSecond * 4];
                            while (true)
                            {
                                int bytesRead = conversionStream.Read(bytes, 0, bytes.Length);
                                if (bytesRead == 0)
                                {
                                    break;
                                }
                                outStream.Write(bytes, 0, bytesRead);
                            }
                            TemporalMusic.PCM_Data_RightChannel = outStream.ToArray();
                        }

                        //Other props
                        TemporalMusic.Duration_RightChannel = (uint)(decimal.Divide(TemporalMusic.PCM_Data_RightChannel.Length, conversionStream.WaveFormat.AverageBytesPerSecond) * 1000);

                        //Get IMA ADPCM Data
                        TemporalMusic.IMA_ADPCM_DATA_RightChannel = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalMusic.PCM_Data_RightChannel), TemporalMusic.PCM_Data_RightChannel.Length / 2);
                    }
                }
                else
                {
                    TemporalMusic.Channels_RightChannel = (byte)AudioReader.WaveFormat.Channels;
                    TemporalMusic.Frequency_RightChannel = (uint)AudioReader.WaveFormat.SampleRate;
                    TemporalMusic.RealSize_RightChannel = (uint)new FileInfo(AudioPath).Length;
                    TemporalMusic.Bits_RightChannel = (uint)AudioReader.WaveFormat.BitsPerSample;
                    TemporalMusic.Encoding_RightChannel = AudioReader.WaveFormat.Encoding.ToString();
                    TemporalMusic.Duration_RightChannel = (uint)Math.Round(AudioReader.TotalTime.TotalMilliseconds, 1);

                    //Get PCM Data Stereo
                    TemporalMusic.PCM_Data_RightChannel = new byte[AudioReader.Length];
                    AudioReader.Read(TemporalMusic.PCM_Data_RightChannel, 0, (int)AudioReader.Length);

                    //Get IMA ADPCM Data
                    TemporalMusic.IMA_ADPCM_DATA_RightChannel = ImaADPCM.EncodeIMA_ADPCM(AudioLibrary.ConvertPCMDataToShortArray(TemporalMusic.PCM_Data_RightChannel), TemporalMusic.PCM_Data_RightChannel.Length / 2);
                }
                AudioReader.Close();
            }

            if (TemporalMusic != null && TemporalMusic.PCM_Data_RightChannel != null)
            {
                ShowAudioInfoRightChannel();
            }
            else
            {
                MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAudioInfoLeftChannel()
        {
            //Loaded data
            Textbox_IMA_ADPCM_LeftChannel.Text = TemporalMusic.WAVFileName_LeftChannel;
            Textbox_MD5_Hash_LeftChannel.Text = TemporalMusic.WAVFileMD5_LeftChannel;

            //Properties
            Textbox_IMA_ADPCM_Size_LeftChannels.Text = string.Join(" ", new string[] { TemporalMusic.IMA_ADPCM_DATA_LeftChannel.Length.ToString(), "bytes" });
            Textbox_DataSize_LeftChannel.Text = string.Join(" ", new string[] { TemporalMusic.PCM_Data_LeftChannel.Length.ToString(), "bytes" });
            Textbox_RealSize_LeftChannel.Text = string.Join(" ", new string[] { TemporalMusic.RealSize_LeftChannel.ToString(), "bytes" });
            Textbox_Frequency_LeftChannel.Text = string.Join(" ", new string[] { TemporalMusic.Frequency_LeftChannel.ToString(), "Hz" });
            Textbox_Channels_LeftChannel.Text = TemporalMusic.Channels_LeftChannel.ToString();
            Textbox_Bits_LeftChannel.Text = TemporalMusic.Bits_LeftChannel.ToString();
            Textbox_Duration_LeftChannel.Text = string.Join(" ", new string[] { TemporalMusic.Duration_LeftChannel.ToString(), "ms" });
            Textbox_Encoding_LeftChannel.Text = TemporalMusic.Encoding_LeftChannel.ToUpper();

            //Draw audio waves in the UI
            if (TemporalMusic.PCM_Data_LeftChannel != null && TemporalMusic.Channels_LeftChannel > 0)
            {
                AudioLibrary.DrawAudioWaves(WaveViewer_LeftChannel, TemporalMusic, 0, false);
            }
        }

        private void ShowAudioInfoRightChannel()
        {
            //Loaded data
            Textbox_IMA_ADPCM_RightChannel.Text = TemporalMusic.WAVFileName_RightChannel;
            Textbox_MD5_Hash_RightChannel.Text = TemporalMusic.WAVFileMD5_RightChannel;

            //Properties
            Textbox_IMA_ADPCM_Size_RightChannels.Text = string.Join(" ", new string[] { TemporalMusic.IMA_ADPCM_DATA_RightChannel.Length.ToString(), "bytes" });
            Textbox_DataSize_RightChannel.Text = string.Join(" ", new string[] { TemporalMusic.PCM_Data_RightChannel.Length.ToString(), "bytes" });
            Textbox_RealSize_RightChannel.Text = string.Join(" ", new string[] { TemporalMusic.RealSize_RightChannel.ToString(), "bytes" });
            Textbox_Frequency_RightChannel.Text = string.Join(" ", new string[] { TemporalMusic.Frequency_RightChannel.ToString(), "Hz" });
            Textbox_Channels_RightChannel.Text = TemporalMusic.Channels_RightChannel.ToString();
            Textbox_Bits_RightChannel.Text = TemporalMusic.Bits_RightChannel.ToString();
            Textbox_Duration_RightChannel.Text = string.Join(" ", new string[] { TemporalMusic.Duration_RightChannel.ToString(), "ms" });
            Textbox_Encoding_RightChannel.Text = TemporalMusic.Encoding_RightChannel.ToUpper();

            //Draw audio waves in the UI
            if (TemporalMusic.PCM_Data_RightChannel != null && TemporalMusic.Channels_RightChannel > 0)
            {
                AudioLibrary.DrawAudioWaves(WaveViewer_RightChannel, TemporalMusic, 0, true);
            }
        }
    }
}
