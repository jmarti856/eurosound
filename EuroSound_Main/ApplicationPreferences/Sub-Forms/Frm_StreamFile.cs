using System;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_StreamFile : Form
    {
        private Form OpenForm;
        public Frm_StreamFile()
        {
            InitializeComponent();
        }

        private void Frm_StreamFile_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Textbox_ExternalFilePath.Text = ((Frm_MainPreferences)OpenForm).StreamFilePathTEMPORAL;
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("EuroSound Files (*.ESF)|*.esf", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                Textbox_ExternalFilePath.Text = FilePath;
            }
        }

        private void Frm_StreamFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).StreamFilePathTEMPORAL = Textbox_ExternalFilePath.Text;
        }
    }
}
