using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    partial class Frm_StreamSoundsEditorMain
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
            EXStreamSoundsFunctions.RemoveStreamedSound(TreeView_StreamData.SelectedNode.Name, StreamSoundsList);
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_StreamData, TreeView_StreamData.SelectedNode, TreeView_StreamData.SelectedNode.Tag.ToString());
        }

        private void OpenSoundPropertiesForm()
        {
            string SoundKeyInDictionary = TreeView_StreamData.SelectedNode.Name;
            EXSoundStream SelectedSound = EXStreamSoundsFunctions.GetSoundByName(Convert.ToUInt32(SoundKeyInDictionary), StreamSoundsList);
            if (SelectedSound != null)
            {
                Frm_StreamSounds_Properties FormAudioProps = new Frm_StreamSounds_Properties(SelectedSound, SoundKeyInDictionary)
                {
                    Text = "Streamed Sound Properties",
                    Tag = this.Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormAudioProps.ShowDialog();
                FormAudioProps.Dispose();
            }
        }
    }
}
