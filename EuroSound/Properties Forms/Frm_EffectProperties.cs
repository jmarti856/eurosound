using System;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound
{
    public partial class Frm_EffectProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        EXSound SelectedSound;
        public Frm_EffectProperties(EXSound SoundToCheck)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_EffectProperties_Load(object sender, EventArgs e)
        {
            /*Datasource Combobox*/
            cbx_hashcode.DataSource = Hashcodes.SFX_Defines.ToList();
            cbx_hashcode.ValueMember = "Key";
            cbx_hashcode.DisplayMember = "Value";

            /*Put the selected hashcode in case is not null*/
            if (SelectedSound.Hashcode != null)
            {
                cbx_hashcode.SelectedValue = SelectedSound.Hashcode;
            }

            /*---Required for Engine X--*/
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

            this.Close();
        }

        private void Textbox_flags_Click(object sender, EventArgs e)
        {
            EuroSound_FlagsForm FormFlags = new EuroSound_FlagsForm(textbox_flags.Text)
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
    }
}
