using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_FlagsForm : Form
    {
        //--Global Flags--
        private CheckBox[] AllCheckboxes;
        private TextBox[] AllTextboxes;
        private int Flags, FlagsToEnable;
        private string[] Labels;

        //--Properties--
        public EuroSound_FlagsForm(int CheckedFlags, string[] FlagsLabel, int EnabledFlags)
        {
            InitializeComponent();
            Flags = CheckedFlags;
            Labels = FlagsLabel;
            FlagsToEnable = EnabledFlags;

            AllCheckboxes = new CheckBox[]
            {
                Checkbox_Flag01, Checkbox_Flag02, Checkbox_Flag03, Checkbox_Flag04, Checkbox_Flag05, Checkbox_Flag06,
                Checkbox_Flag07, Checkbox_Flag08, Checkbox_Flag09, Checkbox_Flag10, Checkbox_Flag11, Checkbox_Flag12,
                Checkbox_Flag13, Checkbox_Flag14, Checkbox_Flag15, Checkbox_Flag16
            };

            AllTextboxes = new TextBox[]
            {
                Textbox_Flag01, Textbox_Flag02, Textbox_Flag03, Textbox_Flag04, Textbox_Flag05, Textbox_Flag06,
                Textbox_Flag07, Textbox_Flag08, Textbox_Flag09, Textbox_Flag10, Textbox_Flag11, Textbox_Flag12,
                Textbox_Flag13, Textbox_Flag14, Textbox_Flag15, Textbox_Flag16
            };
        }

        public int CheckedFlags { get; set; }
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

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            CheckedFlags = GetFlags();
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            CheckedFlags = GetFlags();
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }

        private void EuroSound_FlagsForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < AllCheckboxes.Length; i++)
            {
                bool fChecked = Convert.ToBoolean((Flags >> i) & 1);
                AllCheckboxes[i].Checked = fChecked;
            }

            for (int i = 0; i < AllTextboxes.Length; i++)
            {
                if (i < Labels.Length)
                {
                    AllTextboxes[i].Text = Labels[i];
                }
            }

            for (int i = 0; i < FlagsToEnable; i++)
            {
                AllCheckboxes[i].Enabled = true;
            }

            AllTextboxes = null;
        }
    }
}