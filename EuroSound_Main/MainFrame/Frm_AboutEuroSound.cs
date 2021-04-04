using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace EuroSound_Application.AboutForm
{
    internal partial class Frm_AboutEuroSound : Form
    {
        public Frm_AboutEuroSound()
        {
            InitializeComponent();
        }

        private void Frm_AboutEuroSound_Load(object sender, EventArgs e)
        {
            Label_Version.Text = GenericFunctions.GetEuroSoundVersion();
        }

        private void Button_ReleaseInfo_Click(object sender, EventArgs e)
        {
            Process.Start(string.Join("", "https://github.com/jmarti856/eurosound/releases/tag/", GenericFunctions.GetEuroSoundVersion()));
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}