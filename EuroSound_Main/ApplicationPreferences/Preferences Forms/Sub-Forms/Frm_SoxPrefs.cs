using EuroSound_Application.Clases;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_SoxPrefs : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_SoxPrefs()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_SoxPrefs_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Textbox_SoXPath.Text = ((Frm_MainPreferences)OpenForm).SoXPathTEMPORAL;
        }

        private void Frm_SoxPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).SoXPathTEMPORAL = Textbox_SoXPath.Text;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Search_Click(object sender, EventArgs e)
        {
            string SoXPath = BrowsersAndDialogs.FileBrowserDialog("Executable Files (*.exe)|*.exe", 0, true);

            if (!string.IsNullOrEmpty(SoXPath))
            {
                if (Path.GetExtension(SoXPath).ToLower().Equals(".exe"))
                {
                    Textbox_SoXPath.Text = SoXPath;
                }
                else
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("GenericUploadedFileWrongExt"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DownloadLinkSox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Text);
        }
    }
}
