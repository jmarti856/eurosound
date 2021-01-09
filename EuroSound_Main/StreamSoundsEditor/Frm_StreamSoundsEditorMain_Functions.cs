using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSoundsEditorMain
    {
        internal void RemoveStreamSoundSelectedNode()
        {
            /*Show warning*/
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Stream Sound: " + TreeView_StreamData.SelectedNode.Text, "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveSound();
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveSound();
            }
        }

        private void RemoveSound()
        {
            if (TreeView_StreamData.SelectedNode != null)
            {
                EXStreamSoundsFunctions.RemoveStreamedSound(TreeView_StreamData.SelectedNode.Name, StreamSoundsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_StreamData, TreeView_StreamData.SelectedNode, TreeView_StreamData.SelectedNode.Tag.ToString());
            }
        }

        private void OpenSoundPropertiesForm()
        {
            string SoundKeyInDictionary = TreeView_StreamData.SelectedNode.Name;
            EXSoundStream SelectedSound = EXStreamSoundsFunctions.GetSoundByName(Convert.ToUInt32(SoundKeyInDictionary), StreamSoundsList);
            if (SelectedSound != null)
            {
                Frm_StreamSounds_Properties FormAudioProps = new Frm_StreamSounds_Properties(SelectedSound, SoundKeyInDictionary)
                {
                    Text = GenericFunctions.TruncateLongString(TreeView_StreamData.SelectedNode.Text, 25) + " - Properties",
                    Tag = Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormAudioProps.ShowDialog();
                FormAudioProps.Dispose();
            }
        }
        public string SaveDocument(string LoadedFile, TreeView TreeView_File, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile ProjectProperties)
        {
            string NewFilePath;

            if (!string.IsNullOrEmpty(LoadedFile))
            {
                NewFilePath = SerializeInfo.SaveStreamedSoundsBank(TreeView_File, StreamSoundsList, LoadedFile, ProjectProperties);
            }
            else
            {
                NewFilePath = OpenSaveAsDialog(TreeView_File, StreamSoundsList, ProjectProperties);
            }

            return NewFilePath;
        }

        internal string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile FileProperties)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, "StreamFile");
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SerializeInfo.SaveStreamedSoundsBank(TreeView_File, StreamSoundsList, SavePath, FileProperties);
                }
            }
            return SavePath;
        }
    }
}
