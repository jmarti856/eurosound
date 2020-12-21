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
    }
}
