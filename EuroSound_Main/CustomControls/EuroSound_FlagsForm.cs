using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_FlagsForm : Form
    {
        /*--Global Flags--*/
        int Flags;
        private CheckBox[] AllCheckboxes;

        /*--Properties--*/
        public int CheckedFlags { get; set; }

        public EuroSound_FlagsForm(int CheckedFlags)
        {
            InitializeComponent();
            Flags = CheckedFlags;

            AllCheckboxes = new CheckBox[] {
            Checkbox_Flag01, Checkbox_Flag02, Checkbox_Flag03, Checkbox_Flag04, Checkbox_Flag05, Checkbox_Flag06,
            Checkbox_Flag07, Checkbox_Flag08, Checkbox_Flag09, Checkbox_Flag10, Checkbox_Flag11, Checkbox_Flag12,
            Checkbox_Flag13, Checkbox_Flag14, Checkbox_Flag15, Checkbox_Flag16};
        }

        private void EuroSound_FlagsForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < AllCheckboxes.Length; i++)
            {
                bool fChecked = Convert.ToBoolean((Flags >> i) & 1);
                AllCheckboxes[i].Checked = fChecked;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            CheckedFlags = GetFlags();
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            CheckedFlags = GetFlags();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        internal int GetFlags()
        {
            int flags = 0;

            for (int i = 0; i < AllCheckboxes.Length; i++)
            {
                if (AllCheckboxes[i].Checked)
                {
                    flags += 1 << i;
                }
                AllCheckboxes[i].Dispose();
            }

            return flags;
        }
    }
}
