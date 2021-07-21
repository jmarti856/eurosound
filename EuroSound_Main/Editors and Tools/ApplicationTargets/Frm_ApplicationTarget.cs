using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.Editors_and_Tools.ApplicationTargets
{
    public partial class Frm_ApplicationTarget : Form
    {
        private EXAppTarget targetOutput;
        private TreeNode targetNode;
        private TreeView treeViewControl;

        private string[,] AvailableTargets = new string[3, 4]
        {
            {"Unused", string.Empty, string.Empty, string.Empty},
            {"Riley", "Nightfire Output", "NGTFIRE", string.Empty},
            {"jmarti856", "Sphinx Output - EngineX (0182)", "SPNX", @"X:\Sphinx\Binary\"},
        };

        public Frm_ApplicationTarget(EXAppTarget outputTarget, TreeNode nodeTarget, TreeView treeviewControl)
        {
            InitializeComponent();
            targetOutput = outputTarget;
            targetNode = nodeTarget;
            treeViewControl = treeviewControl;
        }

        private void Frm_ApplicationTarget_Load(object sender, EventArgs e)
        {
            Label_RequestByName.Text = string.Empty;
            Label_ForProjectName.Text = string.Empty;
            Label_TargetIDName.Text = string.Empty;
            Combobox_Target.SelectedIndex = (int)targetOutput.Project;
            checkBox_UpdateFilelist.Checked = targetOutput.UpdateFileList;
            Textbox_TargetName.Text = targetOutput.Name;
            Textbox_BinaryName.Text = targetOutput.BinaryName;
            Textbox_OutputDirectory.Text = targetOutput.OutputDirectory;

            //If we already set a target, disable combobox.
            if (targetOutput.Project != 0)
            {
                Combobox_Target.Enabled = false;
            }
        }
        private void Combobox_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_RequestByName.Text = AvailableTargets[Combobox_Target.SelectedIndex, 0];
            Label_ForProjectName.Text = AvailableTargets[Combobox_Target.SelectedIndex, 1];
            Label_TargetIDName.Text = AvailableTargets[Combobox_Target.SelectedIndex, 2];
            Textbox_OutputDirectory.Text = AvailableTargets[Combobox_Target.SelectedIndex, 3];
        }

        private void Button_Output_Click(object sender, EventArgs e)
        {
            string selectedPath = BrowsersAndDialogs.OpenFolderBrowser();
            if (Directory.Exists(selectedPath))
            {
                Textbox_OutputDirectory.Text = selectedPath;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            //Check target is not duplicated
            bool targetNodeAlreadyExists;
            if (targetNode == null)
            {
                targetNodeAlreadyExists = TreeNodeFunctions.CheckIfNodeExistsByText(treeViewControl, Textbox_TargetName.Text, true);
            }
            else
            {
                targetNodeAlreadyExists = TreeNodeFunctions.CheckIfTargetNodeExists(treeViewControl, Textbox_TargetName.Text, targetNode.Name);
            }

            //Save changes
            if (targetNodeAlreadyExists)
            {
                //Print error
                MessageBox.Show("A target with this node already exists, please change the target name or cancel the creation.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Save Changes
                targetOutput.Project = (uint)Combobox_Target.SelectedIndex;
                targetOutput.Name = Textbox_TargetName.Text;
                targetOutput.OutputDirectory = Textbox_OutputDirectory.Text;
                targetOutput.BinaryName = Textbox_BinaryName.Text;
                targetOutput.UpdateFileList = checkBox_UpdateFilelist.Checked;

                //Update tree view
                if (targetNode != null)
                {
                    targetNode.Text = Textbox_TargetName.Text;
                }

                //Dialog Result
                DialogResult = DialogResult.OK;

                //Close Form
                Close();
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            //Dialog Result
            DialogResult = DialogResult.Cancel;

            //Close Form
            Close();
            Dispose();
        }
    }
}
