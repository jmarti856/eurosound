using System;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound
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
            Combobox_FileHashcode.DataSource = Hashcodes.SB_Defines.ToList();
            Combobox_FileHashcode.ValueMember = "Key";
            Combobox_FileHashcode.DisplayMember = "Value";

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
            Textbox_Sounds_Path.Text = Browsers.OpenFileBrowser("HasTableFiles|*.h", 0);
        }

        private void Button_Browse_MusicsPath_Click(object sender, EventArgs e)
        {
            Textbox_Musics_Path.Text = Browsers.OpenFileBrowser("HasTableFiles|*.h", 0);
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
    }
}
