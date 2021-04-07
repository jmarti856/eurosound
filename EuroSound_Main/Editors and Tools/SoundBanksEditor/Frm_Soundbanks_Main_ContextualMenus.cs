using EuroSound_Application.CustomControls.MoveMultiplesNodesForm;
using EuroSound_Application.CustomControls.ObjectInstancesForm;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.TreeViewLibraryFunctions;
using EuroSound_Application.TreeViewSorter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_Soundbanks_Main
    {
        //*===============================================================================================
        //* ContextMenu_Folders
        //*===============================================================================================
        #region ContextMenu_Folders_EVENTS
        private void ContextMenu_Folders_AddAudio_Click(object sender, System.EventArgs e)
        {
            string NodeName = GenericFunctions.OpenInputBox("Enter a name for new a new audio.", "New Audio");
            if (!string.IsNullOrEmpty(NodeName))
            {
                if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, NodeName))
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string AudioPath = GenericFunctions.OpenFileBrowser("WAV Files (*.wav)|*.wav", 0, true);
                    if (!string.IsNullOrEmpty(AudioPath))
                    {
                        if (GenericFunctions.AudioIsValid(AudioPath, 1, 22050))
                        {
                            LoadAudio(AudioPath, NodeName);
                        }
                        else
                        {
                            DialogResult TryToReload = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ErrorWavFileIncorrect"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (TryToReload == DialogResult.Yes)
                            {
                                string FileTempFile = AudioFunctionsLibrary.ConvertWavToSoundBankValid(AudioPath, Path.GetFileNameWithoutExtension(AudioPath), 22050, 1, 16);
                                if (!string.IsNullOrEmpty(FileTempFile))
                                {
                                    LoadAudio(FileTempFile, NodeName);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadAudio(string AudioPath, string AudioName)
        {
            string MD5Hash = GenericFunctions.CalculateMD5(AudioPath);
            if (!AudioDataDict.ContainsKey(MD5Hash))
            {
                EXAudio NewAudio = EXSoundbanksFunctions.LoadAudioData(AudioPath);
                if (NewAudio != null)
                {
                    //Add data to dictionary and create tree node
                    AudioDataDict.Add(MD5Hash, NewAudio);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, MD5Hash, AudioName, 7, 7, "Audio", Color.Black, TreeView_File);

                    ProjectInfo.FileHasBeenModified = true;
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AudioExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ContextMenu_Folders_AddSample_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new sample.", "New Sample");
            if (!string.IsNullOrEmpty(Name))
            {
                if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, Name))
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    uint SampleID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    if (TreeNodeFunctions.FindRootNode(TreeView_File.SelectedNode).Name.Equals("StreamedSounds"))
                    {
                        TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, SampleID.ToString(), Name, 4, 4, "Sample", Color.Black, TreeView_File);
                        EXSoundbanksFunctions.AddSampleToSound(EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(TreeView_File.SelectedNode.Name), SoundsList), SampleID, true);
                        ProjectInfo.FileHasBeenModified = true;
                    }
                    else
                    {
                        TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, SampleID.ToString(), Name, 4, 4, "Sample", Color.Black, TreeView_File);
                        EXSoundbanksFunctions.AddSampleToSound(EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(TreeView_File.SelectedNode.Name), SoundsList), SampleID, true);
                    }
                }
            }
        }

        private void ContextMenu_Folders_AddSound_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new sound.", "New Sound");
            if (!string.IsNullOrEmpty(Name))
            {
                if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, Name))
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, "Sound", Color.Black, TreeView_File);

                    //Add Empty Sound
                    EXSound Sound = new EXSound
                    {
                        Hashcode = 0x00000000
                    };
                    SoundsList.Add(SoundID, Sound);

                    //File has been modified
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenu_Folders_Delete_Click(object sender, System.EventArgs e)
        {
            RemoveFolderSelectedNode(TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void MenuItem_Folder_Collapse_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.Collapse();
        }

        private void ContextMenu_Folders_Expand_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.Expand();
        }

        private void ContextMenu_Folders_Move_Click(object sender, System.EventArgs e)
        {
            EuroSound_NodesToFolder SoundsToFolders = new EuroSound_NodesToFolder(TreeView_File, TreeNodeFunctions.FindRootNode(TreeView_File.SelectedNode).Name, TreeView_File.SelectedNode.Name)
            {
                Owner = this
            };
            SoundsToFolders.ShowDialog();
            SoundsToFolders.Dispose();
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
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, EXSoundbanksFunctions.RemoveWhiteSpaces(Name), Name, 1, 1, "Folder", Color.Black, TreeView_File);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenu_Folders_Sort_Click(object sender, System.EventArgs e)
        {
            //TreeView_File.Sort();
            TreeView_File.TreeViewNodeSorter = new NodeSorter();
        }

        private void ContextMenuFolder_Purge_Click(object sender, System.EventArgs e)
        {
            List<string> PurgedAudios = new List<string>();
            IEnumerable<string> GetAudiosListToRemove = EXSoundbanksFunctions.GetAudiosToPurge(AudioDataDict, SoundsList);

            if (GetAudiosListToRemove.Any())
            {
                foreach (string ItemToRemove in GetAudiosListToRemove)
                {
                    TreeNode NodeToRemove = TreeView_File.Nodes.Find(ItemToRemove, true)[0];
                    if (NodeToRemove != null)
                    {
                        PurgedAudios.Add("2Purged Audio: " + NodeToRemove.Text);
                        TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, NodeToRemove, "Audio");
                        EXSoundbanksFunctions.DeleteAudio(AudioDataDict, NodeToRemove.Name);
                    }
                }
                PurgedAudios.TrimExcess();

                if (PurgedAudios.Count > 0)
                {
                    GenericFunctions.ShowErrorsAndWarningsList(PurgedAudios, "Purged Audios", this);
                }

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenuFolder_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }
        private void ContextMenuFolders_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_File.SelectedNode;

            SelectedColor = GenericFunctions.GetColorFromColorPicker(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenuFolder_ExportSounds_Click(object sender, EventArgs e)
        {
            string ExportPath;

            ExportPath = GenericFunctions.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, "");

            if (!string.IsNullOrEmpty(ExportPath))
            {
                IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();

                TreeNodeFunctions.GetNodesInsideFolder(TreeView_File, TreeView_File.SelectedNode, ChildNodesCollection);
                ESIF_Exp.ExportFolder(ChildNodesCollection, ExportPath, SoundsList, AudioDataDict, TreeView_File);
            }
        }

        #endregion ContextMenu_Folders_EVENTS

        //*===============================================================================================
        //* ContextMenu_Sound
        //*===============================================================================================
        #region ContextMenu_Sound_EVENTS
        private void ContextMenu_Sound_Properties_Click(object sender, EventArgs e)
        {
            OpenSelectedNodeSampleProperties(TreeView_File.SelectedNode);
        }

        private void ContextMenu_Sound_Remove_Click(object sender, EventArgs e)
        {
            //Remove item
            RemoveSoundSelectedNode(TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sound_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sound_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_File.SelectedNode;

            SelectedColor = GenericFunctions.GetColorFromColorPicker(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenuSound_ExportSingle_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode;
            string ExportPath;

            SelectedNode = TreeView_File.SelectedNode;
            ExportPath = GenericFunctions.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, SelectedNode.Text);

            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportSingleSFX(ExportPath, uint.Parse(SelectedNode.Name), SoundsList, AudioDataDict, TreeView_File);
            }
        }

        #endregion ContextMenu_Sound_EVENTS

        //*===============================================================================================
        //* ContextMenu_Sample
        //*===============================================================================================
        #region ContextMenu_Sample_EVENTS
        private void ContextMenu_Sample_Properties_Click(object sender, System.EventArgs e)
        {
            OpenSelectedNodeSampleProperties(TreeView_File.SelectedNode);
        }

        private void ContextMenu_Sample_Remove_Click(object sender, System.EventArgs e)
        {
            //Remove item
            RemoveSampleSelectedNode(TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }
        private void ContextMenu_Sample_Rename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenu_Sample_TextColor_Click(object sender, System.EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_File.SelectedNode;

            SelectedColor = GenericFunctions.GetColorFromColorPicker(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        #endregion ContextMenu_Sample_EVENTS

        //*===============================================================================================
        //* ContextMenu_Audio
        //*===============================================================================================
        #region ContextMenu_Audio_EVENTS
        private void ContextMenuAudio_Properties_Click(object sender, EventArgs e)
        {
            OpenAudioProperties(TreeView_File.SelectedNode);
        }

        private void ContextMenuAudio_Remove_Click(object sender, EventArgs e)
        {
            //Remove Item
            RemoveAudioAndWarningDependencies(TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuAudio_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuAudio_TextColor_Click(object sender, EventArgs e)
        {
            int SelectedColor;

            TreeNode SelectedNode = TreeView_File.SelectedNode;

            SelectedColor = GenericFunctions.GetColorFromColorPicker(SelectedNode.ForeColor);
            if (SelectedColor != -1)
            {
                SelectedNode.ForeColor = Color.FromArgb(SelectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void ContextMenuAudio_Usage_Click(object sender, EventArgs e)
        {
            IEnumerable<string> Dependencies = EXSoundbanksFunctions.GetAudioDependencies(TreeView_File.SelectedNode.Name, TreeView_File.SelectedNode.Text, SoundsList, TreeView_File, true);
            if (Dependencies.Any())
            {
                EuroSound_ItemUsage ShowDependencies = new EuroSound_ItemUsage(Dependencies, Tag.ToString())
                {
                    Text = "Audio Usage",
                    Owner = Owner
                };
                ShowDependencies.ShowDialog();
                ShowDependencies.Dispose();
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ItemHasNoDependencies"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion ContextMenu_Audio_EVENTS

        //*===============================================================================================
        //* ContextMenu_HashcodesList
        //*===============================================================================================
        #region ContextMenu_HashcodesList
        private void MenuItem_CopyHashcode_Click(object sender, EventArgs e)
        {
            if (ListView_Hashcodes.SelectedItems.Count > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(ListView_Hashcodes.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void MenuItem_CopyLabel_Click(object sender, EventArgs e)
        {
            if (ListView_Hashcodes.SelectedItems.Count > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(ListView_Hashcodes.SelectedItems[0].SubItems[2].Text);
            }
        }
        #endregion
    }
}