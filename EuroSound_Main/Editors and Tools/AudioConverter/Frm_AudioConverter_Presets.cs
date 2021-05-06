using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.AudioConverter
{
    public partial class Frm_AudioConverter_Presets : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private bool CancelExit = false;
        private List<string[]> AvailablePresets = new List<string[]>();

        public Frm_AudioConverter_Presets(List<string[]> Presets)
        {
            InitializeComponent();
            AvailablePresets = Presets;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_AudioConverter_Presets_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CancelExit)
            {
                e.Cancel = true;
            }
        }

        private void Frm_AudioConverter_Presets_Shown(object sender, EventArgs e)
        {
            foreach (string[] PresetName in AvailablePresets)
            {
                ListBox_Presets.Items.Add(PresetName[0]);
            }
        }

        //*===============================================================================================
        //* Form Controls Events
        //*===============================================================================================
        private void ListBox_Presets_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear Textbox
            Textbox_Frequency.Text = string.Empty;
            Textbox_Bits.Text = string.Empty;
            Textbox_Channels.Text = string.Empty;
            Textbox_Desc.Text = string.Empty;

            //Add values
            if (ListBox_Presets.SelectedItems.Count > 0)
            {
                string[] Values = AvailablePresets[ListBox_Presets.SelectedIndex];
                Textbox_Frequency.Text = Values[1];
                Textbox_Bits.Text = Values[2];
                Textbox_Channels.Text = Values[3];
                Textbox_Desc.Text = Values[4];
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (ListBox_Presets.SelectedItems.Count > 0)
            {
                //Get parent form
                Form FormToSearch = GenericFunctions.GetFormByName("Frm_AudioConverter", Tag.ToString());
                ((Frm_AudioConverter)FormToSearch).ComboBox_Bits.SelectedItem = Textbox_Bits.Text;
                ((Frm_AudioConverter)FormToSearch).Combobox_Rate.SelectedItem = Textbox_Frequency.Text;
                if (Textbox_Channels.Text.Equals("1"))
                {
                    ((Frm_AudioConverter)FormToSearch).RadioButton_Mono.Checked = true;
                }
                else
                {
                    ((Frm_AudioConverter)FormToSearch).RadioButton_Stereo.Checked = true;
                }

                //Close current form
                CancelExit = false;
                Close();
            }
            else
            {
                CancelExit = true;
                MessageBox.Show("Please, select a preset", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            CancelExit = false;
            Close();
        }
    }
}
