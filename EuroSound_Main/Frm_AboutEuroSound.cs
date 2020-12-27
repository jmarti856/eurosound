using System;
using System.Reflection;
using System.Windows.Forms;

namespace EuroSound_Application
{
    partial class Frm_AboutEuroSound : Form
    {
        public Frm_AboutEuroSound()
        {
            InitializeComponent();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Frm_AboutEuroSound_Load(object sender, EventArgs e)
        {
            Label_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}