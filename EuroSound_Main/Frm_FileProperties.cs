using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_FileProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private ProjectFile CurrentFileProperties;

        public Frm_FileProperties(ProjectFile FileProperties)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_FileProperties_Load(object sender, EventArgs e)
        {
            /*Datasource Combobox*/
            Hashcodes.AddHashcodesToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);

            Textbox_FileName.Text = CurrentFileProperties.FileName;
            Combobox_TypeOfData.SelectedIndex = CurrentFileProperties.TypeOfData;
            Textbox_Sounds_Path.Text = GlobalPreferences.HT_SoundsPath;
            Textbox_SFXData_Path.Text = GlobalPreferences.HT_SoundsDataPath;
            Textbox_Musics_Path.Text = GlobalPreferences.HT_MusicPath;

            /*Put the selected hashcode in case is not null*/
            if (CurrentFileProperties.Hashcode != 0x00000000)
            {
                Combobox_FileHashcode.SelectedValue = CurrentFileProperties.Hashcode;
            }
        }

        //*===============================================================================================
        //* Form Controls Events
        //*===============================================================================================
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            /*Close current form*/
            this.Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            /*Update properties*/
            CurrentFileProperties.FileName = Textbox_FileName.Text;
            //CurrentFileProperties.TypeOfData = Combobox_TypeOfData.SelectedIndex;

            CurrentFileProperties.Hashcode = Convert.ToUInt32(Combobox_FileHashcode.SelectedValue.ToString());

            /*Update Current File label*/
            GenericFunctions.SetCurrentFileLabel(CurrentFileProperties.FileName);

            /*Close current form*/
            this.Close();
        }

        private void Combobox_FileHashcode_Click(object sender, EventArgs e)
        {
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                Hashcodes.AddHashcodesToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);
            }
        }
    }
}