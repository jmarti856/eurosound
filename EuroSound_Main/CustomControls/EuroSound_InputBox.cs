using System;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.InputBoxForm
{
    public partial class EuroSound_InputBox : Form
    {
        public EuroSound_InputBox(string LabelText, string Title)
        {
            InitializeComponent();
            Label_Text.Text = LabelText;
            Text = Title;
        }

        public string Result { get; set; }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Result = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            Result = TextBox_InputText.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }
    }
}