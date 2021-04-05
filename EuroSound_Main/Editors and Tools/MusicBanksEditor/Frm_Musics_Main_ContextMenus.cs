using EuroSound_Application.SoundBanksEditor;
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
        private void ContextMenuSounds_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_MusicData, TreeView_MusicData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuSounds_Delete_Click(object sender, EventArgs e)
        {
            RemoveMusicSelectedNode(TreeView_MusicData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuSounds_Properties_Click(object sender, EventArgs e)
        {
            if (TreeView_MusicData.SelectedNode != null)
            {
                OpenMusicPropertiesForm(TreeView_MusicData.SelectedNode);
            }
        }

        private void ContextMenuSounds_ExportESIF_Click(object sender, EventArgs e)
        {

        }

        private void ContextMenuSounds_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_MusicData.SelectedNode;

            SelectedColor = GenericFunctions.GetColorFromColorPicker(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        //*===============================================================================================
        //* Context Menu Folders
        //*===============================================================================================
        private void ContextMenuFolder_NewFolder_Click(object sender, EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new folder.", "New Folder");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_MusicData, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_MusicData.SelectedNode.Name, EXSoundbanksFunctions.RemoveWhiteSpaces(Name), Name, 1, 1, "Folder", Color.Black, TreeView_MusicData);
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
            RemoveFolderSelectedNode(TreeView_MusicData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuFolder_SortItems_Click(object sender, EventArgs e)
        {
            //TreeView_File.Sort();
            TreeView_MusicData.TreeViewNodeSorter = new NodeSorter();
        }

        private void ContextMenuFolder_AddSound_Click(object sender, EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new music.", "New Music");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_MusicData, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_MusicData.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Music", Color.Black, TreeView_MusicData);

                    //Add Empty Sound
                    EXMusic EmptyMusic = new EXMusic();

                    if (!MusicsList.ContainsKey(SoundID))
                    {
                        MusicsList.Add(SoundID, EmptyMusic);
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
            int SelectedColor;

            TreeNode SelectedNode = TreeView_MusicData.SelectedNode;
            SelectedColor = GenericFunctions.GetColorFromColorPicker(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }
    }
}
