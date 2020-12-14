using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_WarningBox : Form
    {
        public bool ShowWarningAgain { get; set; }

        public EuroSound_WarningBox(string LabelText, string Title, bool ShowWarningAgainCheckbox)
        {
            InitializeComponent();
            Label_Text.Text = LabelText;
            this.ShowInTaskbar = false;
            this.Text = Title;
            Checkbox_ShowAgain.Visible = ShowWarningAgainCheckbox;
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            ShowWarningAgain = (Checkbox_ShowAgain.Checked = !Checkbox_ShowAgain.Checked);
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            ShowWarningAgain = (Checkbox_ShowAgain.Checked = !Checkbox_ShowAgain.Checked);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }
    }
}