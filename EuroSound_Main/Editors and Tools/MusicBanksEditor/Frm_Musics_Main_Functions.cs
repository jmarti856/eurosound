using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_Musics_Main
    {
        private string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXMusic> StreamSoundsList, ProjectFile FileProperties)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, FileProperties.FileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SerializeInfo.SaveMusics(TreeView_File, StreamSoundsList, SavePath, FileProperties);

                    //Add file to recent list
                    RecentFilesMenu.AddFile(SavePath);
                }
            }
            return SavePath;
        }

        private void RemoveMusicSelectedNode(TreeNode NodeToRemove)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Music:", TreeView_MusicData.SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveMusic(NodeToRemove);
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveMusic(NodeToRemove);
            }
        }

        private void RemoveMusic(TreeNode SelectedNode)
        {
            if (TreeView_MusicData.SelectedNode != null)
            {
                EXMusic SoundtoSave = MusicsList[uint.Parse(SelectedNode.Name)];
                SaveSnapshot(uint.Parse(SelectedNode.Name), SoundtoSave, SelectedNode);

                EXMusicsFunctions.RemoveMusic(SelectedNode.Name, MusicsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_MusicData, SelectedNode, SelectedNode.Tag.ToString());
            }
        }

        internal void OpenMusicPropertiesForm(TreeNode SelectedNode)
        {
            string MusicKeyInDictionary = SelectedNode.Name;
            EXMusic SelectedMusic = EXMusicsFunctions.GetMusicByName(Convert.ToUInt32(MusicKeyInDictionary), MusicsList);
            if (SelectedMusic != null)
            {
                Frm_Musics_Properties FormAudioProps = new Frm_Musics_Properties(SelectedMusic, MusicKeyInDictionary, SelectedNode.Text)
                {
                    Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                    Tag = Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormAudioProps.ShowDialog();
                FormAudioProps.Dispose();
            }
        }

        private void RemoveFolderSelectedNode(TreeNode SelectedNode)
        {
            //Check we are not trying to delete a root folder
            if (!(SelectedNode == null || SelectedNode.Tag.Equals("Root")))
            {
                //Show warning
                if (GlobalPreferences.ShowWarningMessagesBox)
                {
                    EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Folder:", SelectedNode.Text }), "Warning", true);
                    if (WarningDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                        RemoveRecursivelyFolder();
                    }
                    WarningDialog.Dispose();
                }
                else
                {
                    RemoveRecursivelyFolder();
                }
            }
        }

        private void RemoveRecursivelyFolder()
        {
            //Remove child nodes sounds and samples
            IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
            foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_MusicData, TreeView_MusicData.SelectedNode, ChildNodesCollection))
            {
                EXMusicsFunctions.RemoveMusic(ChildNode.Name, MusicsList);
            }
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_MusicData, TreeView_MusicData.SelectedNode, TreeView_MusicData.SelectedNode.Tag.ToString());
        }

        //*===============================================================================================
        //* UNDO AND REDO
        //*===============================================================================================
        private void SaveSnapshot(uint ItemKey, EXMusic MusicToSave, TreeNode NodeToSave)
        {
            //Save the snapshot.
            UndoListSounds.Push(new KeyValuePair<uint, EXMusic>(ItemKey, MusicToSave));
            UndoListNodes.Push(NodeToSave);

            //Enable or disable the Undo and Redo menu items.
            EnableUndo();
        }

        //Enable or disable the Undo and Redo menu items.
        private void EnableUndo()
        {
            MenuItem_Edit_Undo.Enabled = (UndoListSounds.Count > 0);
        }
    }
}
