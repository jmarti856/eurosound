using EuroSound_Application.ApplicationRegistryFunctions;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.FlagsForm
{
    public partial class EuroSound_FlagsForm : Form
    {
        //--Global Flags--
        private CheckBox[] AllCheckboxes;
        private TextBox[] AllTextboxes;
        private int Flags, FlagsToEnable;
        private string KeyName;
        private WindowsRegistryFunctions WRegFunctions = new WindowsRegistryFunctions();
        public int CheckedFlags { get; set; }

        //--Properties--
        public EuroSound_FlagsForm(int CheckedFlags, string SubKeyName, int EnabledFlags)
        {
            InitializeComponent();
            Flags = CheckedFlags;
            KeyName = SubKeyName;
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
            using (RegistryKey StringFlags = WRegFunctions.ReturnRegistryKey(KeyName))
            {
                for (int i = 0; i < AllCheckboxes.Length; i++)
                {
                    bool fChecked = Convert.ToBoolean((Flags >> i) & 1);
                    AllCheckboxes[i].Checked = fChecked;
                }

                for (int i = 0; i < FlagsToEnable; i++)
                {
                    AllTextboxes[i].Text = (string)StringFlags.GetValue("Flag" + (i + 1), "<Unnamed>");
                    AllCheckboxes[i].Enabled = true;
                }
                StringFlags.Close();
            }

            AllTextboxes = null;
        }
    }
}