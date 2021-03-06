﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Collections.Generic;
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
        private string SelectedSoundKey, SoundName;
        private ProjectFile fileProperties;
        private AudioFunctions AudioLibrary = new AudioFunctions();

        public Frm_StreamSounds_Properties(EXSoundStream SoundToCheck, string SoundKey, string CurrentSoundName, ProjectFile FileProperties)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
            SelectedSoundKey = SoundKey;
            SoundName = CurrentSoundName;
            fileProperties = FileProperties;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSounds_Properties_Load(object sender, EventArgs e)
        {
            //Editable Data
            Numeric_BaseVolume.Value = decimal.Divide(SelectedSound.BaseVolume, 100);

            //Clone object
            TemporalSound = new EXSoundStream
            {
                StartMarkers = new List<EXStreamStartMarker>(SelectedSound.StartMarkers),
                Markers = new List<EXStreamMarker>(SelectedSound.Markers),
            };
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
            GenericFunctions.SaveAudio(AudioLibrary, TemporalSound.WAVFileName, (int)TemporalSound.Frequency, (int)TemporalSound.Bits, TemporalSound.Channels, TemporalSound.PCM_Data);
        }

        private void ContextMenuAudioSave_Click(object sender, EventArgs e)
        {
            GenericFunctions.SaveAudio(AudioLibrary, TemporalSound.WAVFileName, (int)TemporalSound.Frequency, (int)TemporalSound.Bits, TemporalSound.Channels, TemporalSound.PCM_Data);
        }

        private void Button_MarkersEditor_Click(object sender, EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
            Frm_StreamSounds_MarkersEditor MarkersEditr = new Frm_StreamSounds_MarkersEditor(TemporalSound, null, false)
            {
                Text = GenericFunctions.TruncateLongString(SoundName, 25) + " - Markers",
                Tag = Tag.ToString(),
                Owner = this,
                ShowInTaskbar = false
            };
            MarkersEditr.ShowDialog();
            MarkersEditr.Dispose();
        }

        private void Button_ReplaceAudio_Click(object sender, EventArgs e)
        {
            string AudioPath = BrowsersAndDialogs.FileBrowserDialog("WAV Files (*.wav)|*.wav", 0, true);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                if (GenericFunctions.AudioIsValid(AudioPath, GlobalPreferences.StreambankChannels, GlobalPreferences.StreambankFrequency))
                {
                    LoadAudio(AudioPath, false);
                }
                else
                {
                    DialogResult TryToReload = MessageBox.Show(string.Join("", "Error, this audio file is not correct, the specifies are: ", GlobalPreferences.StreambankChannels, " channels, the rate must be ", GlobalPreferences.StreambankFrequency, "Hz, must have ", GlobalPreferences.StreambankBits, " bits per sample and encoded in ", GlobalPreferences.StreambankEncoding, Environment.NewLine, Environment.NewLine, ".Do you want that EuroSound tries to convert it to a valid format?"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (TryToReload == DialogResult.Yes)
                    {
                        LoadAudio(AudioPath, true);
                    }
                }
            }
        }

        private void Button_Play_Click(object sender, EventArgs e)
        {
            if (TemporalSound.PCM_Data != null)
            {
                AudioLibrary.PlayAudio(_waveOut, TemporalSound.PCM_Data, (int)TemporalSound.Frequency, 1, (int)TemporalSound.Bits, TemporalSound.Channels, 0, Numeric_BaseVolume.Value);
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("AudioProperties_FileCorrupt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            AudioLibrary.StopAudio(_waveOut);
        }

        private void Button_OK_Click(object sender, EventArgs e)
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
                SelectedSound.StartMarkers = new List<EXStreamStartMarker>(TemporalSound.StartMarkers);
                SelectedSound.Markers = new List<EXStreamMarker>(TemporalSound.Markers);
            }

            //Change node icon
            Form OpenForm = GenericFunctions.GetFormByName("Frm_StreamSounds_Main", Tag.ToString());
            TreeNode[] Results = ((Frm_StreamSounds_Main)OpenForm).TreeView_StreamData.Nodes.Find(SelectedSoundKey, true);

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

            //Update project status variable
            fileProperties.FileHasBeenModified = true;

            //--Close this form--
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* FUNCTIONS EVENTS
        //*===============================================================================================
        private void ShowAudioInfo()
        {
            //Loaded Data
            Textbox_IMA_ADPCM.Text = TemporalSound.WAVFileName;
            Textbox_MD5_Hash.Text = TemporalSound.WAVFileMD5;

            //Sound Properties
            Textbox_IMA_Data_Length.Text = string.Join(" ", new string[] { TemporalSound.IMA_ADPCM_DATA.Length.ToString(), "bytes" });
            Textbox_DataSize.Text = string.Join(" ", new string[] { TemporalSound.PCM_Data.Length.ToString(), "bytes" });
            Textbox_Frequency.Text = string.Join(" ", new string[] { TemporalSound.Frequency.ToString(), "Hz" });
            Textbox_Channels.Text = TemporalSound.Channels.ToString();
            Textbox_Bits.Text = TemporalSound.Bits.ToString();
            Textbox_Duration.Text = string.Join(" ", new string[] { TemporalSound.Duration.ToString(), "ms" });
            Textbox_Encoding.Text = TemporalSound.Encoding.ToUpper();

            //Draw audio waves in the UI
            if (TemporalSound.PCM_Data != null && TemporalSound.Channels > 0)
            {
                AudioLibrary.DrawAudioWaves(euroSound_WaveViewer1, TemporalSound, 0, false);
            }
        }

        private void LoadAudio(string AudioPath, bool ConvertAudio)
        {
            //LoadData
            EXStreamSoundsFunctions.LoadAudioData(AudioPath, TemporalSound, ConvertAudio, AudioLibrary);

            //Check Loaded Data
            if (TemporalSound != null && TemporalSound.PCM_Data != null)
            {
                ShowAudioInfo();
            }
            else
            {
                MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
