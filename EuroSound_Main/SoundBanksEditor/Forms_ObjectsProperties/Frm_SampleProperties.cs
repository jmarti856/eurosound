﻿using NAudio.Wave;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_SampleProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private EXSample SelectedSample;
        private WaveOut _waveOut = new WaveOut();
        private AudioFunctions AudioFunctionsLibrary;
        private bool IsSubSFX;

        public Frm_SampleProperties(EXSample Sample, bool SubSFX)
        {
            InitializeComponent();
            SelectedSample = Sample;
            IsSubSFX = SubSFX;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_SampleProperties_Load(object sender, EventArgs e)
        {
            AudioFunctionsLibrary = new AudioFunctions();

            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
            numeric_pitchoffset.Value = SelectedSample.PitchOffset;
            numeric_randomPitchOffset.Value = SelectedSample.RandomPitchOffset;
            Numeric_BaseVolume.Value = SelectedSample.BaseVolume;
            numeric_randomvolumeoffset.Value = SelectedSample.RandomVolumeOffset;
            numeric_pan.Value = SelectedSample.Pan;
            numeric_randompan.Value = SelectedSample.RandomPan;
            Checkbox_IsStreamedSound.Checked = SelectedSample.IsStreamed;

            /*Datasource Combobox*/
            Combobox_SelectedAudio.DataSource = EXSoundbanksFunctions.GetListAudioData(((Frm_Soundbanks_Main)ParentForm).AudioDataDict, ((Frm_Soundbanks_Main)ParentForm).TreeView_File).ToList();
            Combobox_SelectedAudio.ValueMember = "Key";
            Combobox_SelectedAudio.DisplayMember = "Value";

            /*---Put the selected audio in case is not null---*/
            if (SelectedSample.ComboboxSelectedAudio != null)
            {
                Combobox_SelectedAudio.SelectedValue = SelectedSample.ComboboxSelectedAudio;
            }

            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
            Hashcodes.AddHashcodesToCombobox(Combobox_Hashcode, Hashcodes.SFX_Defines);

            Combobox_Hashcode.SelectedValue = SelectedSample.HashcodeSubSFX;

            EnableOrDisableSubSFXSection(IsSubSFX);
        }

        private void Frm_SampleProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
        }

        //*===============================================================================================
        //* Control Events
        //*===============================================================================================
        private void Button_PlayAudio_Click(object sender, EventArgs e)
        {
            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
            if (Combobox_SelectedAudio.SelectedValue != null)
            {
                EXAudio AudioSelected = TreeNodeFunctions.GetSelectedAudio(Combobox_SelectedAudio.SelectedValue.ToString(), ((Frm_Soundbanks_Main)ParentForm).AudioDataDict);
                if (AudioSelected != null && AudioSelected.PCMdata != null)
                {
                    AudioFunctionsLibrary.PlayAudio(_waveOut, AudioSelected.PCMdata, (int)AudioSelected.Frequency, int.Parse(numeric_pitchoffset.Value.ToString()), (int)AudioSelected.Bits, (int)AudioSelected.Channels, int.Parse(numeric_pan.Value.ToString()));
                }
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            SelectedSample.PitchOffset = (Int16)numeric_pitchoffset.Value;
            SelectedSample.RandomPitchOffset = (Int16)numeric_randomPitchOffset.Value;
            SelectedSample.BaseVolume = (sbyte)Numeric_BaseVolume.Value;
            SelectedSample.RandomVolumeOffset = (sbyte)numeric_randomvolumeoffset.Value;
            SelectedSample.Pan = (sbyte)numeric_pan.Value;
            SelectedSample.RandomPan = (sbyte)numeric_randompan.Value;
            SelectedSample.IsStreamed = Checkbox_IsStreamedSound.Checked;

            if (Combobox_SelectedAudio.SelectedValue != null)
            {
                SelectedSample.ComboboxSelectedAudio = Combobox_SelectedAudio.SelectedValue.ToString();
            }

            if (Combobox_Hashcode.SelectedValue != null)
            {
                SelectedSample.HashcodeSubSFX = (uint)Combobox_Hashcode.SelectedValue;
            }


            this.Close();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        private void Checkbox_IsStreamedSound_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_IsStreamedSound.Checked)
            {
                EnableOrDisableMediaSection(false);
            }
            else
            {
                EnableOrDisableMediaSection(true);
            }
        }

        private void EnableOrDisableMediaSection(bool Action)
        {
            Combobox_SelectedAudio.Enabled = Action;
            Combobox_Hashcode.Enabled = Action;
            Button_PlayAudio.Enabled = Action;
            Button_Stop.Enabled = Action;
            Button_Edit.Enabled = Action;
        }

        private void EnableOrDisableSubSFXSection(bool Action)
        {
            if (!Checkbox_IsStreamedSound.Checked)
            {
                Combobox_Hashcode.Enabled = Action;
                Combobox_SelectedAudio.Enabled = _ = !Action;
            }
        }

        private void Button_Edit_Click(object sender, EventArgs e)
        {
            string AudioKey = Combobox_SelectedAudio.SelectedValue.ToString();
            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
            if (((Frm_Soundbanks_Main)ParentForm).AudioDataDict.ContainsKey(AudioKey))
            {
                EXAudio SelectedSound = ((Frm_Soundbanks_Main)ParentForm).AudioDataDict[AudioKey];
                if (SelectedSound != null)
                {
                    Frm_AudioProperties FormAudioProps = new Frm_AudioProperties(SelectedSound, AudioKey)
                    {
                        Text = "Audio Properties",
                        Tag = this.Tag,
                        Owner = this,
                        ShowInTaskbar = false
                    };
                    FormAudioProps.ShowDialog();
                    FormAudioProps.Dispose();
                }
            }
        }
    }
}