using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CustomControls.FlagsForm;
using NAudio.Wave;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
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
            euroSound_WaveViewer1.BackColor = Color.FromArgb(GlobalPreferences.BackColorWavesControl);
            AudioFunctionsLibrary = new AudioFunctions();

            TemporalAudio = new EXAudio();
            Reflection.CopyProperties(SelectedAudio, TemporalAudio);
            UpdateControls();

            //--Editable Data--
            Textbox_Flags.Text = TemporalAudio.Flags.ToString();
            numeric_psi.Value = TemporalAudio.PSIsample;
            numeric_loopOffset.Value = TemporalAudio.LoopOffset;

            if (TemporalAudio.PCMdata != null)
            {
                AudioFunctionsLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalAudio, 0);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_AudioProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
            _waveOut.Dispose();
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_ReplaceAudio_Click(object sender, EventArgs e)
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
                        string FileTempFile = AudioFunctionsLibrary.ConvertWavToSoundBankValid(AudioPath, Path.GetFileNameWithoutExtension(AudioPath), 22050, 1, 16);
                        if (!string.IsNullOrEmpty(FileTempFile))
                        {
                            LoadAudio(FileTempFile);
                        }
                    }
                }
            }
        }

        private void Button_SaveAudio_Click(object sender, EventArgs e)
        {
            SaveAudio();
        }

        private void Textbox_Flags_MouseClick(object sender, MouseEventArgs e)
        {
            EuroSound_FlagsForm FormFlags = new EuroSound_FlagsForm(int.Parse(Textbox_Flags.Text), "AudioFlags", 1)
            {
                Text = "Audio Data Flags",
                Tag = Tag,
                Owner = this,
            };
            if (FormFlags.ShowDialog() == DialogResult.OK)
            {
                Textbox_Flags.Text = FormFlags.CheckedFlags.ToString();
            }
            FormFlags.Dispose();
        }

        private void Button_TestLoopOffset_Click(object sender, EventArgs e)
        {
            if (byte.Parse(Textbox_Flags.Text) == 1)
            {
                byte[] LoopSamples;

                try
                {
                    int SamplesToSkip = (int.Parse(numeric_loopOffset.Value.ToString()) / 2) * 2;

                    LoopSamples = TemporalAudio.PCMdata.Skip(SamplesToSkip).ToArray();
                    AudioFunctionsLibrary.PlayAudioLoopOffset(_waveOut, LoopSamples, (int)TemporalAudio.Frequency, 0, (int)TemporalAudio.Bits, (int)TemporalAudio.Channels, 0);
                }
                catch
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ErrorLoopOffsetNoValid"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                AudioFunctionsLibrary.PlayAudio(_waveOut, TemporalAudio.PCMdata, (int)TemporalAudio.Frequency, 1, (int)TemporalAudio.Bits, (int)TemporalAudio.Channels, 0, 1);
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
                    using (Pen linePen = new Pen(Color.FromArgb(GlobalPreferences.ColorWavesControl), 1))
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
            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
            //--Add The Audio to the list if has been replaced--
            if (!SelectedAudioMD5Hash.Equals(TemporalAudioHash))
            {
                if (!((Frm_Soundbanks_Main)ParentForm).AudioDataDict.ContainsKey(TemporalAudioHash))
                {
                    //--Update Dictionary--
                    ((Frm_Soundbanks_Main)ParentForm).AudioDataDict.Remove(SelectedAudioMD5Hash);
                    if (TemporalAudio != null)
                    {
                        ((Frm_Soundbanks_Main)ParentForm).AudioDataDict.Add(TemporalAudioHash, TemporalAudio);
                    }

                    //--Update Tree View--
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
                //--Add PCM data if the stored one is null--
                if (SelectedAudio.PCMdata == null)
                {
                    SelectedAudio.PCMdata = TemporalAudio.PCMdata;
                }

                //--Modify Temporal Audio Values--
                TemporalAudio.Flags = Convert.ToUInt16(Textbox_Flags.Text);
                TemporalAudio.PSIsample = (uint)numeric_psi.Value;
                TemporalAudio.LoopOffset = (uint)numeric_loopOffset.Value;

                //--Update Selected Audio-
                SelectedAudio.Flags = TemporalAudio.Flags;
                SelectedAudio.PSIsample = TemporalAudio.PSIsample;
                SelectedAudio.LoopOffset = TemporalAudio.LoopOffset;
            }

            //--Stop Audio and liberate Memmory
            AudioFunctionsLibrary.StopAudio(_waveOut);

            //--Close this form--
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
            Close();
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void ContextMenuAudioSave_Click(object sender, EventArgs e)
        {
            SaveAudio();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void SaveAudio()
        {
            string SavePath;

            SavePath = GenericFunctions.SaveFileBrowser("WAV Files (*.wav)|*.wav", 0, true, TemporalAudio.LoadedFileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                AudioFunctionsLibrary.CreateWavFile((int)TemporalAudio.Frequency, (int)TemporalAudio.Bits, (int)TemporalAudio.Channels, TemporalAudio.PCMdata, SavePath);
            }
        }

        private void LoadAudio(string AudioPath)
        {
            TemporalAudioHash = GenericFunctions.CalculateMD5(AudioPath);
            TemporalAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);

            if (TemporalAudio != null && TemporalAudio.PCMdata != null)
            {
                UpdateControls();
                numeric_psi.Value = TemporalAudio.PSIsample;
                Textbox_MD5Hash.Text = TemporalAudioHash;
                AudioFunctionsLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalAudio, 0);

                //Ask user if wants to maintain config
                DialogResult MaintainConfig = MessageBox.Show("Do you want to maintain the flags and loop offset values?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MaintainConfig == DialogResult.No)
                {
                    //--Editable Data--
                    Textbox_Flags.Text = TemporalAudio.Flags.ToString();
                    numeric_loopOffset.Value = TemporalAudio.LoopOffset;
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ErrorReadingFileIsUsedByAnotherProcess"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControls()
        {
            Textbox_MediaName.Text = TemporalAudio.LoadedFileName;
            Textbox_Encoding.Text = TemporalAudio.Encoding.ToUpper();
            Textbox_DataSize.Text = string.Join(" ", new string[] { TemporalAudio.DataSize.ToString(), "bytes" });
            Textbox_Frequency.Text = string.Join(" ", new string[] { TemporalAudio.Frequency.ToString(), "Hz" });
            Textbox_RealSize.Text = string.Join(" ", new string[] { TemporalAudio.RealSize.ToString(), "bytes" });
            Textbox_Channels.Text = TemporalAudio.Channels.ToString();
            Textbox_Bits.Text = TemporalAudio.Bits.ToString();
            Textbox_Duration.Text = string.Join(" ", new string[] { TemporalAudio.Duration.ToString(), "ms" });
            Textbox_MD5Hash.Text = SelectedAudioMD5Hash;
        }
    }
}