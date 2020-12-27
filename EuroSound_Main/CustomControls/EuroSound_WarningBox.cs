using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_WarningBox : Form
    {
        public EuroSound_WarningBox(string LabelText, string Title, bool ShowWarningAgainCheckbox)
        {
            InitializeComponent();
            Label_Text.Text = LabelText;
            ShowInTaskbar = false;
            Text = Title;
            Checkbox_ShowAgain.Visible = ShowWarningAgainCheckbox;
        }

        public bool ShowWarningAgain { get; set; }
        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            ShowWarningAgain = (Checkbox_ShowAgain.Checked = !Checkbox_ShowAgain.Checked);
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            ShowWarningAgain = (Checkbox_ShowAgain.Checked = !Checkbox_ShowAgain.Checked);
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }
    }
}