using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_Soundbanks_Main
    {
        //*===============================================================================================
        //* ContextMenu_Folders
        //*===============================================================================================
        #region ContextMenu_Folders_EVENTS
        private void ContextMenu_Folders_Move_Click(object sender, System.EventArgs e)
        {
            EuroSound_NodesToFolder SoundsToFolders = new EuroSound_NodesToFolder(TreeView_File)
            {
                ShowInTaskbar = false,
                Owner = this
            };
            SoundsToFolders.ShowDialog();
            SoundsToFolders.Dispose();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Folders_Sort_Click(object sender, System.EventArgs e)
        {
            //TreeView_File.Sort();
            TreeView_File.TreeViewNodeSorter = new NodeSorter();
        }

        private void ContextMenu_Folders_AddSound_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new sound.", "New Sound");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    int SoundID = EXObjectsFunctions.GetSoundID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Sound", Color.Black, TreeView_File);
                    EXObjectsFunctions.AddEmptySoundWithName(SoundID, Name, "0x1A000001", SoundsList);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenu_Folders_AddSample_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new sample.", "New Sample");
            if (!string.IsNullOrEmpty(Name))
            {
                if (TreeNodeFunctions.FindRootNode(TreeView_File.SelectedNode).Name.Equals("StreamedSounds"))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXObjectsFunctions.RemoveWhiteSpaces(Name), Name, 4, 4, "Sample", Color.Black, TreeView_File);
                    EXObjectsFunctions.AddSampleToSound(EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Name), SoundsList), Name, true);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXObjectsFunctions.RemoveWhiteSpaces(Name), Name, 4, 4, "Sample", Color.Black, TreeView_File);
                    EXObjectsFunctions.AddSampleToSound(EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Name), SoundsList), Name, false);
                }
            }
        }

        private void ContextMenuFolder_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Folders_AddAudio_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new audio.", "New Audio");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files|*.wav", 0);
                    if (!string.IsNullOrEmpty(AudioPath))
                    {
                        string MD5Hash = GenericFunctions.CalculateMD5(AudioPath);
                        if (EXObjectsFunctions.LoadAudioAndAddToList(AudioPath, Name, AudioDataDict, MD5Hash))
                        {
                            TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, MD5Hash, Name, 7, 7, "Audio", Color.Black, TreeView_File);
                            ProjectInfo.FileHasBeenModified = true;
                        }
                        else
                        {
                            MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AudioExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void ContextMenuFolder_Purge_Click(object sender, System.EventArgs e)
        {
            List<string> PurgedAudios = new List<string>();
            List<string> GetAudiosListToRemove = EXObjectsFunctions.GetAudiosToPurge(AudioDataDict, SoundsList);
            if (GetAudiosListToRemove.Count > 0)
            {
                foreach (string AudioToRemove in GetAudiosListToRemove)
                {
                    TreeNode NodeToRemove = TreeView_File.Nodes.Find(AudioToRemove, true)[0];
                    if (NodeToRemove != null)
                    {
                        PurgedAudios.Add("2Purged Audio: " + NodeToRemove.Text);
                        TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, NodeToRemove, "Audio");
                        EXObjectsFunctions.DeleteAudio(AudioDataDict, NodeToRemove.Name);
                    }
                }

                if (PurgedAudios.Count > 0)
                {
                    EuroSound_ErrorsAndWarningsList ShowDependencies = new EuroSound_ErrorsAndWarningsList(PurgedAudios)
                    {
                        Text = "Purging Audios",
                        ShowInTaskbar = false,
                        TopMost = true
                    };
                    ShowDependencies.ShowDialog();
                    ShowDependencies.Dispose();
                }

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenuFolders_TextColor_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.ForeColor = GenericFunctions.GetColorFromColorPicker(); ;
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Folders_New_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new folder.", "New Folder");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXObjectsFunctions.RemoveWhiteSpaces(Name), Name, 1, 1, "Folder", Color.Black, TreeView_File);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenu_Folders_Delete_Click(object sender, System.EventArgs e)
        {
            RemoveFolderSelectedNode();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Folders_Expand_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.Expand();
        }

        private void MenuItem_Folder_Collapse_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.Collapse();
        }

        #endregion

        //*===============================================================================================
        //* ContextMenu_Sound
        //*===============================================================================================
        #region ContextMenu_Sound_EVENTS
        private void ContextMenu_Sound_Remove_Click(object sender, System.EventArgs e)
        {
            RemoveSoundSelectedNode();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sound_Properties_Click(object sender, System.EventArgs e)
        {
            OpenSelectedNodeSampleProperties();
        }

        private void ContextMenu_Sound_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sound_TextColor_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.ForeColor = GenericFunctions.GetColorFromColorPicker();

        }

        private void ContextMenu_Sound_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TreeNodeFunctions.FindRootNode(TreeView_File.SelectedNode).Name.Equals("StreamedSounds"))
            {
                ContextMenuSound_AddSample.Visible = false;
            }
            else
            {
                ContextMenuSound_AddSample.Visible = true;
            }
        }
        #endregion

        //*===============================================================================================
        //* ContextMenu_Sample
        //*===============================================================================================
        #region ContextMenu_Sample_EVENTS
        private void ContextMenu_Sample_Remove_Click(object sender, System.EventArgs e)
        {
            RemoveSampleSelectedNode();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sample_Properties_Click(object sender, System.EventArgs e)
        {
            OpenSelectedNodeSampleProperties();
        }

        private void ContextMenu_Sample_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sample_TextColor_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.ForeColor = GenericFunctions.GetColorFromColorPicker();
        }
        #endregion

        //*===============================================================================================
        //* ContextMenu_Audio
        //*===============================================================================================
        #region ContextMenu_Audio_EVENTS
        private void ContextMenuAudio_Usage_Click(object sender, System.EventArgs e)
        {
            List<string> Dependencies = EXObjectsFunctions.GetAudioDependencies(TreeView_File.SelectedNode.Name, TreeView_File.SelectedNode.Text, SoundsList, true);
            if (Dependencies.Count > 0)
            {
                EuroSound_ItemUsage ShowDependencies = new EuroSound_ItemUsage(Dependencies)
                {
                    Text = "Audio Usage",
                    Owner = this.Owner,
                    ShowInTaskbar = false
                };
                ShowDependencies.ShowDialog();
                ShowDependencies.Dispose();
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ItemHasNoDependencies"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ContextMenuAudio_TextColor_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.ForeColor = GenericFunctions.GetColorFromColorPicker();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuAudio_Properties_Click(object sender, System.EventArgs e)
        {
            OpenAudioProperties();
        }

        private void ContextMenuAudio_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuAudio_Remove_Click(object sender, System.EventArgs e)
        {
            RemoveAudioAndWarningDependencies();
            ProjectInfo.FileHasBeenModified = true;
        }
        #endregion

    }
}
