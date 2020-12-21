﻿using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSoundsEditorMain
    {
        //*===============================================================================================
        //* ContextMenu_Folders
        //*===============================================================================================
        private void ContextMenuFolder_CollapseAll_Click(object sender, System.EventArgs e)
        {
            TreeView_StreamData.SelectedNode.Collapse();
        }

        private void ContextMenuFolder_ExpandAll_Click(object sender, System.EventArgs e)
        {
            TreeView_StreamData.SelectedNode.Expand();
        }

        private void ContextMenuMain_AddSound_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new streaming sound.", "New Streaming Sound");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    int SoundID = GenericFunctions.GetSoundID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_StreamData.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Sound", Color.Black, TreeView_StreamData);
                    //Add Empty Sound
                    EXSoundStream Sound = new EXSoundStream
                    {
                        DisplayName = Name,
                        Hashcode = "0x1A000001",
                    };
                    StreamSoundsList.Add(SoundID, Sound);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuMain_DeleteSound_Click(object sender, System.EventArgs e)
        {
            RemoveStreamSoundSelectedNode();
        }

        private void ContextMenuMain_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuMain_TextColor_Click(object sender, System.EventArgs e)
        {
            TreeView_StreamData.SelectedNode.ForeColor = GenericFunctions.GetColorFromColorPicker(); ;
            ProjectInfo.FileHasBeenModified = true;
        }
    }
}
