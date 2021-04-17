using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
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

            //Get Parent form
            SoundsParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());

            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
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
            Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
            if (Combobox_SelectedAudio.SelectedValue != null)
            {
                EXAudio AudioSelected = TreeNodeFunctions.GetSelectedAudio(Combobox_SelectedAudio.SelectedValue.ToString(), ((Frm_Soundbanks_Main)ParentForm).AudioDataDict);
                if (AudioSelected != null && AudioSelected.PCMdata != null)
                {
                    AudioFunctionsLibrary.PlayAudio(_waveOut, AudioSelected.PCMdata, (int)AudioSelected.Frequency, int.Parse(numeric_pitchoffset.Value.ToString()), (int)AudioSelected.Bits, (int)AudioSelected.Channels, numeric_pan.Value, Numeric_BaseVolume.Value);
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
            bool Invert;

            if (IsSubSFX)
            {
                //Invert boolean
                Invert = !IsSubSFX;

                //Activate and deactivate controls
                Combobox_SelectedAudio.Enabled = Invert;
                Combobox_Hashcode.Enabled = IsSubSFX;

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
                        Frm_AudioProperties FormAudioProps = new Frm_AudioProperties(SelectedSound, AudioKey)
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
            Thread AudioData = new Thread(() =>
            {
                //Add data to combobox
                Combobox_SelectedAudio.DataSource = EXSoundbanksFunctions.GetListAudioData(((Frm_Soundbanks_Main)SoundsParentForm).AudioDataDict, ((Frm_Soundbanks_Main)SoundsParentForm).TreeView_File).ToList();
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
            AudioData.Start();
        }

        private void AddHashcodesToCombobox()
        {
            //---AddDataToCombobox
            Thread DataToCombobox = new Thread(() =>
            {
                Combobox_Hashcode.DataSource = Hashcodes.SFX_Defines.ToList();
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
            DataToCombobox.Start();
        }
    }
}