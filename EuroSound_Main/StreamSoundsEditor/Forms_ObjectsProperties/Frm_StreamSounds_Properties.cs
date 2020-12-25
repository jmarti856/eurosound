using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSounds_Properties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        string SoundKey;
        private EXSoundStream SelectedSound;

        public Frm_StreamSounds_Properties(EXSoundStream SoundToCheck, string SoundKeyDictionary)
        {
            InitializeComponent();

            SelectedSound = SoundToCheck;
            SoundKey = SoundKeyDictionary;
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

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            SelectedSound.Hashcode = (uint)Combobox_Hashcode.SelectedValue;
            SelectedSound.BaseVolume = (uint)Numeric_BaseVolume.Value;

            /*--Close this form--*/
            this.Close();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
