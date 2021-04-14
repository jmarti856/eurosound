using System;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.DebugTypes
{
    public partial class EuroSound_DebugTypes : Form
    {
        public int CheckedOptions { get; set; }
        public string[] FlagItems;

        public EuroSound_DebugTypes(string[] FlagsToAdd)
        {
            InitializeComponent();
            FlagItems = FlagsToAdd;
        }
        private void EuroSound_DebugTypes_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < FlagItems.Length; i++)
            {
                CheckListBox_DebugElements.Items.Add(FlagItems[i]);
            }
        }

        private void Btn_SelectAllOptions_Click(object sender, EventArgs e)
        {
            CheckListBox_DebugElements.Focus();
            for (int i = 0; i < CheckListBox_DebugElements.Items.Count; i++)
            {
                CheckListBox_DebugElements.Items[i].Selected = true;
            }
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
