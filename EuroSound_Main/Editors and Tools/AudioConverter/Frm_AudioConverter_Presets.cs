using System;
using System.Windows.Forms;

namespace EuroSound_Application.AudioConverter
{
    public partial class Frm_AudioConverter_Presets : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        bool CancelExit = false;

        public Frm_AudioConverter_Presets()
        {
            InitializeComponent();
        }

        private void ListBox_Presets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox_Presets.SelectedItems.Count > 0)
            {
                //At the moment we only have one preset, no no need to check with is the selected item
                Textbox_Bits.Text = "16";
                Textbox_Channels.Text = "1";
                Textbox_Frequency.Text = "22050";
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (ListBox_Presets.SelectedItems.Count > 0)
            {
                //Get parent form
                Form FormToSearch = GenericFunctions.GetFormByName("Frm_AudioConverter", Tag.ToString());
                ((Frm_AudioConverter)FormToSearch).ComboBox_Bits.SelectedIndex = 0;
                ((Frm_AudioConverter)FormToSearch).Combobox_Rate.SelectedIndex = 1;
                ((Frm_AudioConverter)FormToSearch).RadioButton_Mono.Checked = true;

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

        private void Frm_AudioConverter_Presets_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CancelExit)
            {
                e.Cancel = true;
            }
        }
    }
}
