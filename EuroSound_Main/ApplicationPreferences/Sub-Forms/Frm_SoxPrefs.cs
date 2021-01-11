using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_SoxPrefs : Form
    {
        private Form OpenForm;
        public Frm_SoxPrefs()
        {
            InitializeComponent();
        }

        private void Frm_SoxPrefs_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Textbox_SoXPath.Text = ((Frm_MainPreferences)OpenForm).SoXPathTEMPORAL;
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            string SoXPath = GenericFunctions.OpenFileBrowser("Executable Files (*.exe)|*.exe", 0);

            if (!string.IsNullOrEmpty(SoXPath))
            {
                Textbox_SoXPath.Text = SoXPath;
            }
        }

        private void Frm_SoxPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).SoXPathTEMPORAL = Textbox_SoXPath.Text;
        }

        private void DownloadLinkSox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(((LinkLabel)sender).Text);
        }
    }
}
