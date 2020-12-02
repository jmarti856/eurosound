using System;
using System.Windows.Forms;

namespace EuroSound
{
    partial class Frm_AboutEuroSound : Form
    {
        public Frm_AboutEuroSound()
        {
            InitializeComponent();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}