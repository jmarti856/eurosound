using FunctionsLibrary;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    public partial class Frm_SampleProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        EXSample SelectedSample;
        Dictionary<string, EXAudio> AudiosDataList;
        Dictionary<string, string> SFX_Defines, SB_Defines;
        TreeView ControlToSearch;
        ProjectFile CurrentFileProperties;
        ResourceManager ResourcesManager;

        bool IsSubSFX;
        string HascodesSFX;

        public Frm_SampleProperties(EXSample Sample, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl, bool SubSFX, ProjectFile FileProps, string v_HashcodesSFX, Dictionary<string, string> v_SFX_Defines, Dictionary<string, string> v_SB_Defines, ResourceManager v_ResourcesManager)
        {
            InitializeComponent();
            SelectedSample = Sample;
            AudiosDataList = AudiosList;
            ControlToSearch = TreeViewControl;
            IsSubSFX = SubSFX;
            CurrentFileProperties = FileProps;
            HascodesSFX = v_HashcodesSFX;
            SFX_Defines = v_SFX_Defines;
            SB_Defines = v_SB_Defines;
            ResourcesManager = v_ResourcesManager;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_SampleProperties_Load(object sender, EventArgs e)
        {
            numeric_pitchoffset.Value = SelectedSample.PitchOffset;
            numeric_randomPitchOffset.Value = SelectedSample.RandomPitchOffset;
            Numeric_BaseVolume.Value = SelectedSample.BaseVolume;
            numeric_randomvolumeoffset.Value = SelectedSample.RandomVolumeOffset;
            numeric_pan.Value = SelectedSample.Pan;
            numeric_randompan.Value = SelectedSample.RandomPan;
            Checkbox_IsStreamedSound.Checked = SelectedSample.IsStreamed;
            Hashcodes.AddHashcodesToCombobox(Combobox_SelectedAudio, EXObjectsFunctions.GetListAudioData(AudiosDataList, ControlToSearch));


            /*---Put the selected audio in case is not null---*/
            if (SelectedSample.ComboboxSelectedAudio != null)
            {
                Combobox_SelectedAudio.SelectedValue = SelectedSample.ComboboxSelectedAudio;
            }

            //Sound defines
            if (GenericFunctions.FileIsModified(Hashcodes.HT_SoundsMD5, HascodesSFX))
            {
                Hashcodes.LoadSoundHashcodes(HascodesSFX, SFX_Defines, SB_Defines, ResourcesManager);
            }
            Hashcodes.AddHashcodesToCombobox(Combobox_Hashcode, SFX_Defines);

            if (!string.IsNullOrEmpty(SelectedSample.HashcodeSubSFX))
            {
                Combobox_Hashcode.SelectedValue = SelectedSample.HashcodeSubSFX;
            }

            EnableOrDisableSubSFXSection(IsSubSFX);
        }

        //*===============================================================================================
        //* Control Events
        //*===============================================================================================
        private void Button_ok_Click(object sender, EventArgs e)
        {
            SelectedSample.PitchOffset = Convert.ToInt32(numeric_pitchoffset.Value);
            SelectedSample.RandomPitchOffset = Convert.ToInt32(numeric_randomPitchOffset.Value);
            SelectedSample.BaseVolume = Convert.ToInt32(Numeric_BaseVolume.Value);
            SelectedSample.RandomVolumeOffset = Convert.ToInt32(numeric_randomvolumeoffset.Value);
            SelectedSample.Pan = Convert.ToInt32(numeric_pan.Value);
            SelectedSample.RandomPan = Convert.ToInt32(numeric_randompan.Value);
            SelectedSample.IsStreamed = Checkbox_IsStreamedSound.Checked;

            if (!string.IsNullOrEmpty(Combobox_SelectedAudio.SelectedValue.ToString()))
            {
                SelectedSample.ComboboxSelectedAudio = Combobox_SelectedAudio.SelectedValue.ToString();
            }

            if (!string.IsNullOrEmpty(Combobox_Hashcode.SelectedValue.ToString()))
            {
                SelectedSample.HashcodeSubSFX = Combobox_Hashcode.SelectedValue.ToString();
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
        }

        private void EnableOrDisableSubSFXSection(bool Action)
        {
            Combobox_Hashcode.Enabled = Action;
        }
    }
}