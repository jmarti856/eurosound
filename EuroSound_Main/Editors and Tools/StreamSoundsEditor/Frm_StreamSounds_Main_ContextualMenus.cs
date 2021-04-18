using EuroSound_Application.Clases;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_Main
    {
        //*===============================================================================================
        //* ContextMenu_Folders
        //*===============================================================================================
        private void ContextMenuFolder_CollapseAll_Click(object sender, EventArgs e)
        {
            TreeView_StreamData.SelectedNode.Collapse();
        }

        private void ContextMenuFolder_ExpandAll_Click(object sender, EventArgs e)
        {
            TreeView_StreamData.SelectedNode.Expand();
        }

        private void ContextMenuMain_AddSound_Click(object sender, EventArgs e)
        {
            string Name = BrowsersAndDialogs.InputBoxDialog("Enter a name for new a new streaming sound.", "New Streaming Sound");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_StreamData.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Sound", true, true, false, Color.Black, TreeView_StreamData);
                    //Add Empty Sound
                    EXSoundStream Sound = new EXSoundStream();

                    if (!StreamSoundsList.ContainsKey(SoundID))
                    {
                        StreamSoundsList.Add(SoundID, Sound);
                    }

                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuMain_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuMain_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_StreamData.SelectedNode;
            SelectedColor = BrowsersAndDialogs.ColorPickerDialog(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        //*===============================================================================================
        //* ContextMenu_Sounds
        //*===============================================================================================
        private void ContextMenuSounds_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuSounds_Delete_Click(object sender, System.EventArgs e)
        {
            RemoveStreamSoundSelectedNode(TreeView_StreamData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuSounds_Properties_Click(object sender, EventArgs e)
        {
            if (TreeView_StreamData.SelectedNode != null)
            {
                OpenSoundPropertiesForm(TreeView_StreamData.SelectedNode);
            }
        }

        private void ContextMenuSounds_ExportESIF_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode;
            string ExportPath;

            SelectedNode = TreeView_StreamData.SelectedNode;
            ExportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, SelectedNode.Text);

            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportStreamSoundbank(ExportPath, uint.Parse(SelectedNode.Name), StreamSoundsList, TreeView_StreamData);
            }
        }

        private void ContextMenuSounds_MoveUp_Click(object sender, EventArgs e)
        {
            MoveUp(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_MoveDown_Click(object sender, EventArgs e)
        {
            MoveDown(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_StreamData.SelectedNode;

            SelectedColor = BrowsersAndDialogs.ColorPickerDialog(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }
    }
}
