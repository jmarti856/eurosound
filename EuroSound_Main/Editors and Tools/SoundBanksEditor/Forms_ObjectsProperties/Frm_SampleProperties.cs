﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_SampleProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private EXSample SelectedSample;
        private WaveOut _waveOut = new WaveOut();
        private AudioFunctions AudioFunctionsLibrary;
        private Form SoundsParentForm;
        private ProjectFile fileProperties;
        private bool IsSubSFX;

        public Frm_SampleProperties(EXSample Sample, ProjectFile FileProperties, bool SubSFX)
        {
            InitializeComponent();
            SelectedSample = Sample;
            IsSubSFX = SubSFX;
            fileProperties = FileProperties;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_SampleProperties_Load(object sender, EventArgs e)
        {
            AudioFunctionsLibrary = new AudioFunctions();

            //Get Parent form
            SoundsParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
        }

        private void Frm_SampleProperties_Shown(object sender, EventArgs e)
        {
            numeric_pitchoffset.Value = SelectedSample.PitchOffset;
            numeric_randomPitchOffset.Value = SelectedSample.RandomPitchOffset;
            Numeric_BaseVolume.Value = decimal.Divide(SelectedSample.BaseVolume, 100);
            numeric_randomvolumeoffset.Value = decimal.Divide(SelectedSample.RandomVolumeOffset, 100);
            numeric_pan.Value = decimal.Divide(SelectedSample.Pan, 100);
            numeric_randompan.Value = decimal.Divide(SelectedSample.RandomPan, 100);

            //Ensure that this boolean is correctly stated
            if (SelectedSample.IsStreamed)
            {
                SelectedSample.IsStreamed = false;
            }

            //---Put the selected audio in case is not null---
            EnableOrDisableSubSFXSection(IsSubSFX);
        }

        private void Frm_SampleProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
            _waveOut.Dispose();
        }

        //*===============================================================================================
        //* Control Events
        //*===============================================================================================
        private void Button_PlayAudio_Click(object sender, EventArgs e)
        {
            Form parentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
            if (Combobox_SelectedAudio.SelectedValue != null)
            {
                EXAudio audioSelected = TreeNodeFunctions.GetSelectedAudio(Combobox_SelectedAudio.SelectedValue.ToString(), ((Frm_Soundbanks_Main)parentForm).AudioDataDict);
                if (audioSelected != null && audioSelected.PCMdata != null)
                {
                    AudioFunctionsLibrary.PlayAudio(_waveOut, audioSelected.PCMdata, (int)audioSelected.Frequency, int.Parse(numeric_pitchoffset.Value.ToString()), (int)audioSelected.Bits, (int)audioSelected.Channels, numeric_pan.Value, Numeric_BaseVolume.Value);
                }
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            AudioFunctionsLibrary.StopAudio(_waveOut);
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            SelectedSample.PitchOffset = (short)numeric_pitchoffset.Value;
            SelectedSample.RandomPitchOffset = (short)numeric_randomPitchOffset.Value;
            SelectedSample.BaseVolume = (sbyte)(Numeric_BaseVolume.Value * 100);
            SelectedSample.RandomVolumeOffset = (sbyte)(numeric_randomvolumeoffset.Value * 100);
            SelectedSample.Pan = (sbyte)(numeric_pan.Value * 100);
            SelectedSample.RandomPan = (sbyte)(numeric_randompan.Value * 100);

            if (Combobox_SelectedAudio.SelectedValue != null)
            {
                SelectedSample.ComboboxSelectedAudio = Combobox_SelectedAudio.SelectedValue.ToString();
            }

            if (Combobox_Hashcode.SelectedValue != null)
            {
                SelectedSample.HashcodeSubSFX = (uint)Combobox_Hashcode.SelectedValue;
            }

            //Update project status variable
            fileProperties.FileHasBeenModified = true;

            //Close current form
            Close();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        private void EnableOrDisableSubSFXSection(bool IsSubSFX)
        {
            if (IsSubSFX)
            {
                //Invert boolean
                bool Invert = !IsSubSFX;

                //Activate and deactivate controls
                Combobox_SelectedAudio.Enabled = Invert;
                Combobox_Hashcode.Enabled = IsSubSFX;

                //Reload HashTable if required
                if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
                {
                    Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                }

                //Set subsfx hashcode to the combobox
                AddHashcodesToCombobox();
            }
            else
            {
                //Datasource Combobox
                AddAvailableAudiosToCombobox();
            }
        }

        private void Button_Edit_Click(object sender, EventArgs e)
        {
            if (Combobox_SelectedAudio.SelectedValue != null)
            {
                string AudioKey = Combobox_SelectedAudio.SelectedValue.ToString();
                if (((Frm_Soundbanks_Main)SoundsParentForm).AudioDataDict.ContainsKey(AudioKey))
                {
                    EXAudio SelectedSound = ((Frm_Soundbanks_Main)SoundsParentForm).AudioDataDict[AudioKey];
                    if (SelectedSound != null)
                    {
                        Frm_AudioProperties FormAudioProps = new Frm_AudioProperties(SelectedSound, fileProperties, AudioKey)
                        {
                            Text = "Audio Properties",
                            Tag = Tag,
                            Owner = this,
                            ShowInTaskbar = false
                        };
                        FormAudioProps.ShowDialog();
                        FormAudioProps.Dispose();
                    }
                }
            }
        }

        private void AddAvailableAudiosToCombobox()
        {
            //Datasource Combobox
            Thread audioData = new Thread(() =>
            {
                //Add data to combobox
                Combobox_SelectedAudio.DataSource = EXSoundbanksFunctions.GetListAudioData(((Frm_Soundbanks_Main)SoundsParentForm).AudioDataDict, ((Frm_Soundbanks_Main)SoundsParentForm).TreeView_File).OrderBy(o => o.Value).ToList();
                Combobox_SelectedAudio.BeginInvoke((MethodInvoker)delegate
                {
                    Combobox_SelectedAudio.ValueMember = "Key";
                    Combobox_SelectedAudio.DisplayMember = "Value";
                    Combobox_SelectedAudio.Enabled = true;

                    //Add selected value
                    if (SelectedSample.ComboboxSelectedAudio != null)
                    {
                        Combobox_SelectedAudio.SelectedValue = SelectedSample.ComboboxSelectedAudio;
                    }
                });

                //Enable buttons again
                Button_PlayAudio.BeginInvoke((MethodInvoker)delegate
                {
                    Button_PlayAudio.Enabled = true;
                });

                Button_Stop.BeginInvoke((MethodInvoker)delegate
                {
                    Button_Stop.Enabled = true;
                });

                Button_Edit.BeginInvoke((MethodInvoker)delegate
                {
                    Button_Edit.Enabled = true;
                });
            })
            {
                IsBackground = true
            };
            audioData.Start();
        }

        private void AddHashcodesToCombobox()
        {
            //---AddDataToCombobox
            Thread dataToCombobox = new Thread(() =>
            {
                Combobox_Hashcode.DataSource = Hashcodes.SFX_Defines.OrderBy(o => o.Value).ToList();
                Combobox_Hashcode.Invoke((MethodInvoker)async delegate
                {
                    await Task.Delay(50);
                    Combobox_Hashcode.ValueMember = "Key";
                    Combobox_Hashcode.DisplayMember = "Value";
                    Combobox_Hashcode.SelectedValue = SelectedSample.HashcodeSubSFX;
                });
            })
            {
                IsBackground = true
            };
            dataToCombobox.Start();
        }
    }
}