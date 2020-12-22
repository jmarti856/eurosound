using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EffectProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private EXSound SelectedSound;

        private string TreeNodeSoundName;

        public Frm_EffectProperties(EXSound SoundToCheck, string SoundName)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
            TreeNodeSoundName = SoundName;
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            SelectedSound.Hashcode = Convert.ToInt32(cbx_hashcode.SelectedValue.ToString());
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
            Form OpenForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
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

            this.Close();
        }

        private void Cbx_hashcode_Click(object sender, EventArgs e)
        {
            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                Hashcodes.AddHashcodesToCombobox(cbx_hashcode, Hashcodes.SFX_Defines);
            }

            //Sound Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsDataMD5, GlobalPreferences.HT_SoundsDataPath))
            {
                Hashcodes.LoadSoundDataFile();
            }
        }

        private void Cbx_hashcode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            float[] items = Hashcodes.SFX_Data[Convert.ToInt32(cbx_hashcode.SelectedValue)];
            /*
            [0] HashCode;    --USED--
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
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_Warning_StreamedSound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_EffectProperties_Load(object sender, EventArgs e)
        {
            //Sound Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsDataMD5, GlobalPreferences.HT_SoundsDataPath))
            {
                Hashcodes.LoadSoundDataFile();
            }

            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
            Hashcodes.AddHashcodesToCombobox(cbx_hashcode, Hashcodes.SFX_Defines);

            /*---Put the selected hashcode in case is not null---*/
            cbx_hashcode.SelectedValue = SelectedSound.Hashcode;

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
        private void Textbox_flags_Click(object sender, EventArgs e)
        {
            string[] FlagsLabels = new string[]
            {
                "MaxReject",
                "UnPausable",
                "IgnoreMasterVolume",
                "MultiSample",
                "RandomPick",
                "Shuffled",
                "Loop",
                "Polyphonic",
                "UnderWater",
                "PauseInstant",
                "HasSubSFX",
                "StealOnLouder",
                "TreatLikeMusic",
                "KillMeOwnGroup",
                "GroupStealReject",
                "OneInstancePerFrame"
            };
            EuroSound_FlagsForm FormFlags = new EuroSound_FlagsForm(int.Parse(textbox_flags.Text), FlagsLabels, 13)
            {
                Text = "Sound Flags",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            if (FormFlags.ShowDialog() == DialogResult.OK)
            {
                textbox_flags.Text = FormFlags.CheckedFlags.ToString();
            }
            FormFlags.Dispose();
        }
    }
}