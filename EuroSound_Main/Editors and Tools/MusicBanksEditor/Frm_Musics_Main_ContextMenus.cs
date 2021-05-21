using EuroSound_Application.Clases;
using EuroSound_Application.Editors_and_Tools;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.TreeViewLibraryFunctions;
using EuroSound_Application.TreeViewSorter;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_Musics_Main
    {
        //*===============================================================================================
        //* Context Menu Musics
        //*===============================================================================================
        private void ContextMenuMusics_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_MusicData, TreeView_MusicData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuMusics_Delete_Click(object sender, EventArgs e)
        {
            ToolsCommonFunctions.RemoveEngineXObject((int)GenericFunctions.EXObjectType.EXMusic, TreeView_MusicData, TreeView_MusicData.SelectedNode, MusicsList, ProjectInfo, UndoListMusics, UndoListNodes, MenuItem_Edit_Undo);
        }

        private void ContextMenuMusics_Properties_Click(object sender, EventArgs e)
        {
            if (TreeView_MusicData.SelectedNode != null)
            {
                OpenMusicPropertiesForm(TreeView_MusicData.SelectedNode);
            }
        }

        private void ContextMenuMusics_ExportESIF_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = TreeView_MusicData.SelectedNode;
            string ExportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, SelectedNode.Text);

            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportMusicBank(ExportPath, uint.Parse(SelectedNode.Name), MusicsList, TreeView_MusicData);
            }
        }

        private void ContextMenuMusics_TextColor_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.ChangeNodeColor(TreeView_MusicData.SelectedNode, ProjectInfo);
        }

        //*===============================================================================================
        //* Context Menu Folders
        //*===============================================================================================
        private void ContextMenuFolder_NewFolder_Click(object sender, EventArgs e)
        {
            string Name = BrowsersAndDialogs.InputBoxDialog("Enter a name for new folder.", "New Folder");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_MusicData, Name))
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    uint FolderID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_MusicData.SelectedNode.Name, FolderID.ToString(), Name, 0, 0, "Folder", true, true, false, SystemColors.WindowText, TreeView_MusicData);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuFolder_CollapseAll_Click(object sender, EventArgs e)
        {
            TreeView_MusicData.SelectedNode.Collapse();
        }

        private void ContextMenuFolder_ExpandAll_Click(object sender, EventArgs e)
        {
            TreeView_MusicData.SelectedNode.Expand();
        }

        private void ContextMenuFolder_Delete_Click(object sender, EventArgs e)
        {
            ToolsCommonFunctions.RemoveEngineXObject((int)GenericFunctions.EXObjectType.EXMusicFolder, TreeView_MusicData, TreeView_MusicData.SelectedNode, MusicsList, ProjectInfo, UndoListMusics, UndoListNodes, MenuItem_Edit_Undo);
        }

        private void ContextMenuFolder_SortItems_Click(object sender, EventArgs e)
        {
            //TreeView_File.Sort();
            TreeView_MusicData.TreeViewNodeSorter = new NodeSorter();
        }

        private void ContextMenuFolder_AddSound_Click(object sender, EventArgs e)
        {
            string Name = BrowsersAndDialogs.InputBoxDialog("Enter a name for new a new music.", "New Music");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_MusicData, Name))
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_MusicData.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Music", true, true, false, SystemColors.WindowText, TreeView_MusicData);

                    //Add Empty Music
                    if (!MusicsList.ContainsKey(SoundID))
                    {
                        MusicsList.Add(SoundID, new EXMusic());
                    }

                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuFolder_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_MusicData, TreeView_MusicData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuFolder_TextColor_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.ChangeNodeColor(TreeView_MusicData.SelectedNode, ProjectInfo);
        }

        //*===============================================================================================
        //* ContextMenu_Target
        //*===============================================================================================
        private void ContextMenuTargets_Delete_Click(object sender, EventArgs e)
        {
            ToolsCommonFunctions.RemoveTargetSelectedNode(TreeView_MusicData.SelectedNode, OutputTargets, TreeView_MusicData, ProjectInfo);
        }

        private void ContextMenuTargets_TextColor_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.ChangeNodeColor(TreeView_MusicData.SelectedNode, ProjectInfo);
        }

        private void ContextMenuTargets_Properties_Click(object sender, EventArgs e)
        {
            OpenTargetProperties(TreeView_MusicData.SelectedNode);
        }

        private void ContextMenuTargets_Output_Click(object sender, EventArgs e)
        {

        }

        //*===============================================================================================
        //* Context Menu Rich Text Box
        //*===============================================================================================
        private void RichTextBox_Copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Rtbx_Jump_Music_Codes.SelectedText);
        }

        private void RichTextBox_SelectAll_Click(object sender, EventArgs e)
        {
            Rtbx_Jump_Music_Codes.Focus();
            Rtbx_Jump_Music_Codes.SelectAll();
        }
    }
}
