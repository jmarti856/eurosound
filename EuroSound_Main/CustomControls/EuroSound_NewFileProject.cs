using System;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class EuroSound_NewFileProject : Form
    {
        public string[] FileProps { get; set; }

        public EuroSound_NewFileProject(string FormTitle)
        {
            InitializeComponent();
            this.Text = FormTitle;
        }

        private void EuroSound_NewFileProject_Load(object sender, EventArgs e)
        {
            Combobox_TypeOfData.SelectedIndex = 0;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            string[] Properties = new string[2];

            Properties[0] = Textbox_ProjectName.Text;
            Properties[1] = Combobox_TypeOfData.SelectedIndex.ToString();

            FileProps = Properties;

            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }
    }
}
