using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSounds_Properties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private EXSoundStream SelectedSound;

        public Frm_StreamSounds_Properties(EXSoundStream SoundToCheck)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSounds_Properties_Load(object sender, System.EventArgs e)
        {
            //Sound Defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
            Hashcodes.AddHashcodesToCombobox(Combobox_Hashcode, Hashcodes.SFX_Defines);

            /*Editable Data*/
            Combobox_Hashcode.SelectedValue = SelectedSound.Hashcode;
            Numeric_BaseVolume.Value = SelectedSound.BaseVolume;
        }

        private void Button_MarkersEditor_Click(object sender, System.EventArgs e)
        {
            Frm_StreamSounds_MarkersEditor MarkersEditr = new Frm_StreamSounds_MarkersEditor(SelectedSound)
            {
                Text = "Streamed Sound Markers Editor",
                Tag = Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            MarkersEditr.ShowDialog();
            MarkersEditr.Dispose();
        }

        private void Button_AudioData_Click(object sender, System.EventArgs e)
        {
            Frm_StreamSounds_AudioData AudioEditor = new Frm_StreamSounds_AudioData(SelectedSound)
            {
                Text = "Streamed Sound Audio Data",
                Tag = Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            AudioEditor.ShowDialog();
            AudioEditor.Dispose();
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            if (Combobox_Hashcode.SelectedValue != null)
            {
                SelectedSound.Hashcode = (uint)Combobox_Hashcode.SelectedValue;
            }
            SelectedSound.BaseVolume = (uint)Numeric_BaseVolume.Value;
            SelectedSound.OutputThisSound = CheckBox_OutputThisSound.Checked;

            /*--Close this form--*/
            this.Close();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
