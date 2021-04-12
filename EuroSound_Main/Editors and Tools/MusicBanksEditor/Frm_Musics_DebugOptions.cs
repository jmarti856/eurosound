using System;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_Musics_DebugOptions : Form
    {
        public int CheckedOptions { get; set; }

        public Frm_Musics_DebugOptions()
        {
            InitializeComponent();
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            CheckedOptions = GetFlags();
            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Btn_SelectAllOptions_Click(object sender, EventArgs e)
        {
            CheckListBox_DebugElements.Focus();
            for (int i = 0; i < CheckListBox_DebugElements.Items.Count; i++)
            {
                CheckListBox_DebugElements.Items[i].Selected = true;
            }
        }

        internal int GetFlags()
        {
            int flags = 0;

            for (int i = 0; i < CheckListBox_DebugElements.Items.Count; i++)
            {
                if (CheckListBox_DebugElements.Items[i].Checked)
                {
                    flags += 1 << i;
                }
            }

            return flags;
        }
    }
}
