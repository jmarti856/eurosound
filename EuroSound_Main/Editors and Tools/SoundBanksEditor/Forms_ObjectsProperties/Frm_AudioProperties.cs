using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.Clases;
using EuroSound_Application.CustomControls.FlagsForm;
using NAudio.Wave;
using System;
using System.Drawing;
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
        private AudioFunctions AudioFunctionsLibrary;
        private string SelectedAudioMD5Hash, TemporalAudioHash;

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
            euroSound_WaveViewer1.BackColor = Color.FromArgb(GlobalPreferences.WavesViewerControl_BackgroundColor);
            AudioFunctionsLibrary = new AudioFunctions();

            TemporalAudio = new EXAudio();
            Reflection.CopyProperties(SelectedAudio, TemporalAudio);
            UpdateControls();

            //--Editable Data--
            Textbox_Flags.Tag = TemporalAudio.Flags;
            numeric_psi.Value = TemporalAudio.PSIsample;
            numeric_loopOffset.Value = TemporalAudio.LoopOffset;

            //--Print Flags--
            Textbox_Flags.Text = GenericFunctions.PrintCheckedFlags("AudioFlags", 1, Convert.ToUInt16(Textbox_Flags.Tag));

            //Draw audio waves in the UI
            if (TemporalAudio.PCMdata != null && TemporalAudio.Channels > 0)
            {
                AudioFunctionsLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalAudio, 0, false);
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string audioPath = BrowsersAndDialogs.FileBrowserDialog("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(audioPath))
            {
                if (GenericFunctions.AudioIsValid(audioPath, GlobalPreferences.SoundbankChannels, GlobalPreferences.SoundbankFrequency))
                {
                    LoadAudio(audioPath, false);
                }
                else
                {
                    DialogResult TryToReload = MessageBox.Show(string.Join("", "Error, this audio file is not correct, the specifies are: ", GlobalPreferences.SoundbankChannels, " channels, the rate must be ", GlobalPreferences.SoundbankFrequency, "Hz, must have ", GlobalPreferences.SoundbankBits, " bits per sample and encoded in ", GlobalPreferences.SoundbankEncoding, ".\n\nDo you want that EuroSound tries to convert it to a valid format?"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (TryToReload == DialogResult.Yes)
                    {
                        LoadAudio(audioPath, true);
                    }
                }
            }
        }

        private void Button_SaveAudio_Click(object sender, EventArgs e)
        {
            GenericFunctions.SaveAudio(AudioFunctionsLibrary, TemporalAudio.LoadedFileName, (int)TemporalAudio.Frequency, (int)TemporalAudio.Bits, (int)TemporalAudio.Channels, TemporalAudio.PCMdata);
        }

        private void Textbox_Flags_MouseClick(object sender, MouseEventArgs e)
        {
            EuroSound_FlagsForm formFlags = new EuroSound_FlagsForm(int.Parse(Textbox_Flags.Tag.ToString()), "AudioFlags", 1)
            {
                Text = "Audio Data Flags",
                Tag = Tag,
                Owner = this,
            };
            if (formFlags.ShowDialog() == DialogResult.OK)
            {
                Textbox_Flags.Tag = formFlags.CheckedFlags.ToString();
                Textbox_Flags.Text = formFlags.CheckedFlagsString;
            }
            formFlags.Dispose();
        }

        private void Button_TestLoopOffset_Click(object sender, EventArgs e)
        {
            if (byte.Parse(Textbox_Flags.Text) == 1)
            {
                try
                {
                    int SamplesToSkip = int.Parse(numeric_loopOffset.Value.ToString()) * 2;
                    byte[] LoopSamples = TemporalAudio.PCMdata.Skip(SamplesToSkip).ToArray();
                    AudioFunctionsLibrary.PlayAudioLoopOffset(_waveOut, LoopSamples, (int)TemporalAudio.Frequency, 0, (int)TemporalAudio.Bits, (int)TemporalAudio.Channels, 0);
                }
                catch
                {
                    MessageBox.Show(GenericFunctions.resourcesManager.GetString("ErrorLoopOffsetNoValid"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("AudioNotUseOffset"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    using (Pen linePen = new Pen(Color.FromArgb(GlobalPreferences.WavesViewerControl_WavesColor), 1))
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
            //--Add The Audio to the list if has been replaced--
            if (!SelectedAudioMD5Hash.Equals(TemporalAudioHash))
            {
                Form parentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
                if (!((Frm_Soundbanks_Main)parentForm).AudioDataDict.ContainsKey(TemporalAudioHash))
                {
                    //--Update Dictionary--
                    ((Frm_Soundbanks_Main)parentForm).AudioDataDict.Remove(SelectedAudioMD5Hash);
                    if (TemporalAudio != null)
                    {
                        ((Frm_Soundbanks_Main)parentForm).AudioDataDict.Add(TemporalAudioHash, TemporalAudio);
                    }

                    //--Update Tree View--
                    TreeNode[] nodeSearchResults = ((Frm_Soundbanks_Main)parentForm).TreeView_File.Nodes.Find(SelectedAudioMD5Hash, true);
                    nodeSearchResults[0].Name = TemporalAudioHash;
                }
                else
                {
                    MessageBox.Show(GenericFunctions.resourcesManager.GetString("AudioPropertiesFormAudioExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                TemporalAudio.Flags = Convert.ToUInt16(Textbox_Flags.Tag);
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
            GenericFunctions.SaveAudio(AudioFunctionsLibrary, TemporalAudio.LoadedFileName, (int)TemporalAudio.Frequency, (int)TemporalAudio.Bits, (int)TemporalAudio.Channels, TemporalAudio.PCMdata);
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadAudio(string AudioPath, bool ConvertData)
        {
            TemporalAudioHash = GenericFunctions.CalculateMD5(AudioPath);
            if (ConvertData)
            {
                TemporalAudio = EXSoundbanksFunctions.LoadAndConvertData(AudioPath);
            }
            else
            {
                TemporalAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);
            }

            if (TemporalAudio != null && TemporalAudio.PCMdata != null)
            {
                UpdateControls();
                numeric_psi.Value = TemporalAudio.PSIsample;
                Textbox_MD5Hash.Text = TemporalAudioHash;

                //Draw audio waves in the UI
                AudioFunctionsLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalAudio, 0, false);

                //Ask user if wants to maintain config
                DialogResult maintainConfigQuestion = MessageBox.Show("Do you want to maintain the flags and loop offset values?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (maintainConfigQuestion == DialogResult.No)
                {
                    //--Editable Data--
                    Textbox_Flags.Text = TemporalAudio.Flags.ToString();
                    numeric_loopOffset.Value = TemporalAudio.LoopOffset;
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("ErrorReadingFileIsUsedByAnotherProcess"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateControls()
        {
            //Loaded Data
            Textbox_MediaName.Text = TemporalAudio.LoadedFileName;
            Textbox_MD5Hash.Text = SelectedAudioMD5Hash;

            //Audio Properties
            Textbox_DataSize.Text = string.Join(" ", new string[] { TemporalAudio.DataSize.ToString(), "bytes" });
            Textbox_RealSize.Text = string.Join(" ", new string[] { TemporalAudio.RealSize.ToString(), "bytes" });
            Textbox_Frequency.Text = string.Join(" ", new string[] { TemporalAudio.Frequency.ToString(), "Hz" });
            Textbox_Channels.Text = TemporalAudio.Channels.ToString();
            Textbox_Bits.Text = TemporalAudio.Bits.ToString();
            Textbox_Duration.Text = string.Join(" ", new string[] { TemporalAudio.Duration.ToString(), "ms" });
            Textbox_Encoding.Text = TemporalAudio.Encoding.ToUpper();
        }
    }
}