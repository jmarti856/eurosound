using EuroSound_Application.Clases;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_AutoBackUps : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_AutoBackUps()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_AutoBackUps_Load(object sender, System.EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            CheckBox_AutomaticBackups.Checked = ((Frm_MainPreferences)OpenForm).MakeBackupsTEMPORAL;
            Textbox_BackupFolderPath.Text = ((Frm_MainPreferences)OpenForm).MakeBackupsDirectoryTEMPORAL;
            Numeric_BackupFrequency.Value = ((Frm_MainPreferences)OpenForm).MakeBackupsIntervalTEMPORAL;
            Numeric_MaxBackups.Value = ((Frm_MainPreferences)OpenForm).MakeBackupsMaxNumberTEMPORAL;
        }

        private void Frm_AutoBackUps_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).MakeBackupsTEMPORAL = CheckBox_AutomaticBackups.Checked;
            ((Frm_MainPreferences)OpenForm).MakeBackupsDirectoryTEMPORAL = Textbox_BackupFolderPath.Text;
            ((Frm_MainPreferences)OpenForm).MakeBackupsIntervalTEMPORAL = (int)Numeric_BackupFrequency.Value;
            ((Frm_MainPreferences)OpenForm).MakeBackupsMaxNumberTEMPORAL = (int)Numeric_MaxBackups.Value;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_BrowseFolder_Click(object sender, System.EventArgs e)
        {
            string SelectedPath = BrowsersAndDialogs.OpenFolderBrowser();
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                Textbox_BackupFolderPath.Text = SelectedPath;
            }
        }
    }
}
