using System;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public partial class EuroSound_FlagsForm : Form
    {
        [Flags]
        internal enum ControlFlags16
        {
            Flag01 = 1,
            Flag02 = 2,
            Flag03 = 4,
            Flag04 = 8,
            Flag05 = 16,
            Flag06 = 32,
            Flag07 = 64,
            Flag08 = 128,
            Flag09 = 256,
            Flag10 = 512,
            Flag11 = 1024,
            Flag12 = 2048,
            Flag13 = 4096,
            Flag14 = 8192,
            Flag15 = 16384,
            Flag16 = 32768

        }
        internal static ControlFlags16 Flags16;

        int Flags;
        public int CheckedFlags { get; set; }

        public EuroSound_FlagsForm(string CheckedFlags)
        {
            InitializeComponent();
            Flags = int.Parse(CheckedFlags);
        }

        private void EuroSound_FlagsForm_Load(object sender, EventArgs e)
        {
            SetFlags(Flags);
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

            foreach (Control c in this.Controls)
            {
                if (c is CheckBox box)
                {
                    if (box.Checked)
                    {
                        flags += Convert.ToInt32(c.Text);
                    }
                }
            }

            return flags;
        }

        internal void SetFlags(int Flags)
        {
            if (Flags > 0)
            {
                string[] MultipleFlags;
                string controlName;

                Flags16 = (ControlFlags16)Flags;
                UncheckAllCheckboxes(this);

                //Check Items
                foreach (Enum F_Value in Enum.GetValues(Flags16.GetType()))
                {
                    if (Flags16.HasFlag(F_Value))
                    {
                        if (Flags16.ToString().Length < 7)
                        {
                            controlName = "Checkbox_" + Flags16.ToString().Trim();
                            ((CheckBox)this.Controls.Find(controlName, true)[0]).Checked = true;
                        }
                        else
                        {
                            MultipleFlags = Flags16.ToString().Split(',');
                            foreach (string item in MultipleFlags)
                            {
                                controlName = "Checkbox_" + item.Trim();
                                ((CheckBox)this.Controls.Find(controlName, true)[0]).Checked = true;
                            }
                        }
                    }
                }
            }
        }

        internal void UncheckAllCheckboxes(Control cntrl)
        {
            foreach (Control c in cntrl.Controls)
            {
                if (c is CheckBox box)
                {
                    if (box.Checked)
                    {
                        box.Checked = false;
                    }
                }
            }
        }
    }
}
