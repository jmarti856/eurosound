using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_InputBox : Form
    {
        public string Result { get; set; }
        public EuroSound_InputBox(string LabelText, string Title)
        {
            InitializeComponent();
            Label_Text.Text = LabelText;
            this.ShowInTaskbar = false;
            this.Text = Title;
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            Result = TextBox_InputText.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Result = string.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }
    }
}