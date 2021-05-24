using EuroSound_Application.CurrentProjectFunctions;
using System;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.ProjectSettings
{
    public partial class EuroSound_ProjectsDesc : Form
    {
        private ProjectFile currentProjectSetting;
        public EuroSound_ProjectsDesc(ProjectFile currentProject)
        {
            InitializeComponent();
            currentProjectSetting = currentProject;
        }

        private void EuroSound_ProjectsDesc_Load(object sender, EventArgs e)
        {
            RichTextbox_Desc.Rtf = currentProjectSetting.ProjectDescription;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            currentProjectSetting.ProjectDescription = RichTextbox_Desc.Rtf;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
