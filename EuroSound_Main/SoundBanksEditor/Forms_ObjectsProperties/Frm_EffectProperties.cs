using System;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EffectProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        EXSound SelectedSound;
        Dictionary<string, string> SFX_Defines, SB_Defines;
        Dictionary<string, double[]> SFX_Data;
        ResourceManager ResourcesManager;
        string TreeNodeSoundName, HashcodesSFX, HashcodesSFXData, ParentFormID;


        public Frm_EffectProperties(EXSound SoundToCheck, string SoundName, string v_HashcodesSFX, string v_HashcodesSFXData, Dictionary<string, string> v_SFX_Defines, Dictionary<string, string> v_SB_Defines, Dictionary<string, double[]> v_SFX_Data, string ParentID, ResourceManager v_ResourcesManager)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
            TreeNodeSoundName = SoundName;
            HashcodesSFX = v_HashcodesSFX;
            HashcodesSFXData = v_HashcodesSFXData;
            SFX_Defines = v_SFX_Defines;
            SB_Defines = v_SB_Defines;
            SFX_Data = v_SFX_Data;
            ParentFormID = ParentID;
            ResourcesManager = v_ResourcesManager;
        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_EffectProperties_Load(object sender, EventArgs e)
        {
            //Sound Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsDataMD5, HashcodesSFXData))
            {
                Hashcodes.LoadSoundDataFile(HashcodesSFXData, SFX_Data, SFX_Defines, ResourcesManager);
            }

            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, HashcodesSFX))
            {
                Hashcodes.LoadSoundHashcodes(HashcodesSFX, SFX_Defines, SB_Defines, ResourcesManager);
            }
            Hashcodes.AddHashcodesToCombobox(cbx_hashcode, SFX_Defines);

            /*---Put the selected hashcode in case is not null---*/
            if (SelectedSound.Hashcode != null)
            {
                cbx_hashcode.SelectedValue = SelectedSound.Hashcode;
            }

            /*---Required for Engine X---*/
            numeric_duckerlength.Value = SelectedSound.DuckerLenght;
            numeric_mindelay.Value = SelectedSound.MinDelay;
            numeric_maxdelay.Value = SelectedSound.MaxDelay;
            numeric_innerradiusreal.Value = SelectedSound.InnerRadiusReal;
            numeric_outerradiusreal.Value = SelectedSound.OuterRadiusReal;
            numeric_reverbsend.Value = SelectedSound.ReverbSend;
            cbx_trackingtype.SelectedIndex = SelectedSound.TrackingType;
            numeric_maxvoices.Value = SelectedSound.MaxVoices;
            numeric_priority.Value = SelectedSound.Priority;
            numeric_ducker.Value = SelectedSound.Ducker;
            numeric_mastervolume.Value = SelectedSound.MasterVolume;
            textbox_flags.Text = SelectedSound.Flags.ToString();
            Checkbox_OutputThisSound.Checked = SelectedSound.OutputThisSound;

            /*---Print Sample--*/
            if (SelectedSound.Samples != null)
            {
                foreach (EXSample sample in SelectedSound.Samples)
                {
                    List_Samples.Items.Add(sample.DisplayName);
                }
            }
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            SelectedSound.Hashcode = cbx_hashcode.SelectedValue.ToString();
            SelectedSound.DuckerLenght = Convert.ToInt32(numeric_duckerlength.Value);
            SelectedSound.MinDelay = Convert.ToInt32(numeric_mindelay.Value);
            SelectedSound.MaxDelay = Convert.ToInt32(numeric_maxdelay.Value);
            SelectedSound.InnerRadiusReal = Convert.ToInt32(numeric_innerradiusreal.Value);
            SelectedSound.OuterRadiusReal = Convert.ToInt32(numeric_outerradiusreal.Value);
            SelectedSound.ReverbSend = Convert.ToInt32(numeric_reverbsend.Value);
            SelectedSound.TrackingType = cbx_trackingtype.SelectedIndex;
            SelectedSound.MaxVoices = Convert.ToInt32(numeric_maxvoices.Value);
            SelectedSound.Priority = Convert.ToInt32(numeric_priority.Value);
            SelectedSound.Ducker = Convert.ToInt32(numeric_ducker.Value);
            SelectedSound.MasterVolume = Convert.ToInt32(numeric_mastervolume.Value);
            SelectedSound.Flags = Convert.ToInt32(textbox_flags.Text);
            SelectedSound.OutputThisSound = Checkbox_OutputThisSound.Checked;

            /*--Change icon in the parent form--*/
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.Name.Equals("Frm_Soundbanks_Main"))
                {
                    if (OpenForm.Tag.Equals(ParentFormID))
                    {
                        TreeNode[] Results = ((Frm_Soundbanks_Main)OpenForm).TreeView_File.Nodes.Find(TreeNodeSoundName, true);
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
                        break;
                    }
                }
            }
            this.Close();
        }

        private void Textbox_flags_Click(object sender, EventArgs e)
        {
            EuroSound_FlagsForm FormFlags = new EuroSound_FlagsForm(int.Parse(textbox_flags.Text))
            {
                Text = "Flags",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            if (FormFlags.ShowDialog() == DialogResult.OK)
            {
                textbox_flags.Text = FormFlags.CheckedFlags.ToString();
            }
        }

        private void Cbx_hashcode_Click(object sender, EventArgs e)
        {
            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, HashcodesSFX))
            {
                Hashcodes.LoadSoundHashcodes(HashcodesSFX, SFX_Defines, SB_Defines, ResourcesManager);
                Hashcodes.AddHashcodesToCombobox(cbx_hashcode, SFX_Defines);
            }

            //Sound Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsDataMD5, HashcodesSFXData))
            {
                Hashcodes.LoadSoundDataFile(HashcodesSFXData, SFX_Data, SFX_Defines, ResourcesManager);
            }
        }

        private void Cbx_hashcode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            double[] items = SFX_Data[cbx_hashcode.SelectedValue.ToString()];
            /*
            [0] HashCode;
            [1] InnerRadius; --USED--
            [2] OuterRadius; --USED--
            [3] Altertness;
            [4] Duration;
            [5] Looping;
            [6] Tracking3d; --USED--
            [7] SampleStreamed; --ONLY Informs user if there're no samples added to the sound.
            */
            numeric_innerradiusreal.Value = int.Parse(items[1].ToString());
            numeric_outerradiusreal.Value = int.Parse(items[2].ToString());
            cbx_trackingtype.SelectedIndex = int.Parse(items[6].ToString());

            if (int.Parse(items[6].ToString()) > 0)
            {
                if (SelectedSound.Samples.Count < 1)
                {
                    MessageBox.Show(ResourcesManager.GetString("Gen_Warning_StreamedSound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
