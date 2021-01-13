using System;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.NewProjectForm
{
    public partial class EuroSound_NewFileProject : Form
    {
        public EuroSound_NewFileProject(string FormTitle)
        {
            InitializeComponent();
            Text = FormTitle;
        }

        public string[] FileProps { get; set; }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            string[] Properties = new string[2];

            Properties[0] = Textbox_ProjectName.Text;
            Properties[1] = Combobox_TypeOfData.SelectedIndex.ToString();

            FileProps = Properties;

            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }

        private void EuroSound_NewFileProject_Load(object sender, EventArgs e)
        {
            Combobox_TypeOfData.SelectedIndex = 0;
        }
    }
}