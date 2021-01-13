using System;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.ProgramInstancesForm
{
    public partial class EuroSound_Instances : Form
    {
        public EuroSound_Instances()
        {
            InitializeComponent();
        }

        private void Button_NoStart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void Button_ShowOtherInstance_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void Button_StartAnyway_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
