using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.WarningsForm
{
    public partial class EuroSound_WarningBox : Form
    {
        public bool NoWarnings { get; set; }

        public EuroSound_WarningBox(string LabelText, string Title, bool warningsStatus)
        {
            InitializeComponent();
            Label_Text.Text = LabelText;
            ShowInTaskbar = false;
            Text = Title;
            Checkbox_ShowAgain.Visible = warningsStatus;
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            NoWarnings = Checkbox_ShowAgain.Checked;
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }
    }
}