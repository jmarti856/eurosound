using System;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public partial class Frm_FileProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        public Frm_FileProperties()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_FileProperties_Load(object sender, EventArgs e)
        {
            /*Datasource Combobox*/
            Hashcodes.AddHashcodesToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);

            Textbox_FileName.Text = EXFile.FileName;
            Combobox_TypeOfData.SelectedIndex = EXFile.TypeOfData;
            Textbox_Musics_Path.Text = EXFile.HT_SoundsDataPath;
            Textbox_Sounds_Path.Text = EXFile.HT_SoundsPath;
            /*Put the selected hashcode in case is not null*/
            if (EXFile.Hashcode != null)
            {
                Combobox_FileHashcode.SelectedValue = EXFile.Hashcode;
            }
        }

        private void Button_Browse_SFX_Click(object sender, EventArgs e)
        {
            Textbox_Sounds_Path.Text = Generic.OpenFileBrowser("HasTableFiles|*.h", 0);
        }

        private void Button_Browse_MusicsPath_Click(object sender, EventArgs e)
        {
            Textbox_Musics_Path.Text = Generic.OpenFileBrowser("HasTableFiles|*.h", 0);
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            /*Update properties*/
            EXFile.FileName = Textbox_FileName.Text;
            EXFile.TypeOfData = Combobox_TypeOfData.SelectedIndex;
            EXFile.HT_SoundsDataPath = Textbox_Musics_Path.Text;
            EXFile.HT_SoundsPath = Textbox_Sounds_Path.Text;
            EXFile.Hashcode = Combobox_FileHashcode.SelectedValue.ToString();

            /*Update Current File label*/
            ((Frm_Soundbanks_Main)Application.OpenForms["Frm_Soundbanks_Main"]).CurrentFileLabel.Text = EXFile.FileName;

            /*Close current form*/
            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            /*Close current form*/
            this.Close();
        }

        private void Combobox_FileHashcode_Click(object sender, EventArgs e)
        {
            if (Generic.FileIsModified(EXFile.HT_SoundsMD5, EXFile.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes();
                Hashcodes.AddHashcodesToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);
            }
        }
    }
}
