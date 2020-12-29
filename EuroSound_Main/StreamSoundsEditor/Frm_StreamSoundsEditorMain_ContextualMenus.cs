﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSoundsEditorMain
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
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new streaming sound.", "New Streaming Sound");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    uint SoundID = GenericFunctions.GetSoundID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_StreamData.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Sound", Color.Black, TreeView_StreamData);
                    //Add Empty Sound
                    EXSoundStream Sound = new EXSoundStream
                    {
                        DisplayName = Name,
                        Hashcode = 0,
                    };

                    if (!StreamSoundsList.ContainsKey(SoundID))
                    {
                        StreamSoundsList.Add(SoundID, Sound);
                    }

                    ProjectInfo.StreamedSoundID += 1;
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuMain_DeleteSound_Click(object sender, EventArgs e)
        {
            RemoveStreamSoundSelectedNode();
        }

        private void ContextMenuMain_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuMain_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            SelectedColor = GenericFunctions.GetColorFromColorPicker();
            if (SelectedColor != -1)
            {
                TreeView_StreamData.SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
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

        private void ContextMenuSounds_Properties_Click(object sender, EventArgs e)
        {
            OpenSoundPropertiesForm();
        }

        private void ContextMenuSounds_MoveUp_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.MoveUp(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_MoveDown_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.MoveDown(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            SelectedColor = GenericFunctions.GetColorFromColorPicker();
            if (SelectedColor != -1)
            {
                TreeView_StreamData.SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }
    }
}
