using FunctionsLibrary;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SoundBanks_Editor
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
            if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, Name, SoundsList, AudioDataDict))
            {
                MessageBox.Show(ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    int SoundID = EXObjectsFunctions.GetSoundID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Sound", Color.Black, TreeView_File, ProjectInfo);
                    EXObjectsFunctions.AddEmptySoundWithName(SoundID, Name, "0x1A000001", SoundsList);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    MessageBox.Show(ResourcesManager.GetString("Gen_Error_NameIsEmpty"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXObjectsFunctions.RemoveWhiteSpaces(Name), Name, 4, 4, "Sample", Color.Black, TreeView_File, ProjectInfo);
                    EXObjectsFunctions.AddSampleToSound(EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Name), SoundsList), Name, true);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXObjectsFunctions.RemoveWhiteSpaces(Name), Name, 4, 4, "Sample", Color.Black, TreeView_File, ProjectInfo);
                    EXObjectsFunctions.AddSampleToSound(EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Name), SoundsList), Name, false);
                }
            }
            else
            {
                MessageBox.Show(ResourcesManager.GetString("Gen_Error_NameIsEmpty"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ContextMenu_Folders_AddAudio_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new audio.", "New Audio");
            if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, Name, SoundsList, AudioDataDict))
            {
                MessageBox.Show(ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, MD5Hash, Name, 7, 7, "Audio", Color.Black, TreeView_File, ProjectInfo);
                            ProjectInfo.FileHasBeenModified = true;
                        }
                        else
                        {
                            MessageBox.Show(ResourcesManager.GetString("Error_Adding_AudioExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ResourcesManager.GetString("Gen_Error_NameIsEmpty"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ContextMenuFolders_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenu_Folders_New_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new folder.", "New Folder");
            if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, Name, SoundsList, AudioDataDict))
            {
                MessageBox.Show(ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXObjectsFunctions.RemoveWhiteSpaces(Name), Name, 1, 1, "Folder", Color.Black, TreeView_File, ProjectInfo);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    MessageBox.Show(ResourcesManager.GetString("Gen_Error_NameIsEmpty"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            OpenSelectedNodeSampleProperties(ProjectInfo);
        }

        private void ContextMenu_Sound_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode, ResourcesManager);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sound_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
            }
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
            OpenSelectedNodeSampleProperties(ProjectInfo);
        }

        private void ContextMenu_Sample_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode, ResourcesManager);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sample_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
            }
        }
        #endregion

        //*===============================================================================================
        //* ContextMenu_Audio
        //*===============================================================================================
        #region ContextMenu_Audio_EVENTS
        private void ContextMenuAudio_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenuAudio_Properties_Click(object sender, System.EventArgs e)
        {
            OpenAudioProperties();
        }

        private void ContextMenuAudio_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode, ResourcesManager);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuAudio_Remove_Click(object sender, System.EventArgs e)
        {
            List<string> Dependencies = EXObjectsFunctions.GetAudioDependencies(TreeView_File.SelectedNode.Name, SoundsList);
            if (Dependencies.Count > 0)
            {
                EuroSound_ErrorsAndWarningsList ShowDependencies = new EuroSound_ErrorsAndWarningsList(Dependencies)
                {
                    Text = "Deleting Audio",
                    ShowInTaskbar = false,
                    TopMost = true
                };
                ShowDependencies.ShowDialog();
                ShowDependencies.Dispose();
            }
            else
            {
                RemoveAudioSelectedNode();
                ProjectInfo.FileHasBeenModified = true;
            }
        }
        #endregion

    }
}
