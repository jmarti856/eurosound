using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public partial class Frm_Soundbanks_Main
    {
        string ProgramStatus;

        internal string OpenInputBox(string Text, string Title)
        {
            string SampleName = string.Empty;

            /*Ask user for a name*/
            EuroSound_InputBox dlg = new EuroSound_InputBox(Text, Title);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SampleName = dlg.Result;
            }

            return SampleName;
        }

        internal void OpenItemProperties()
        {
            if (TreeView_File.SelectedNode != null)
            {
                if (TreeView_File.SelectedNode.Tag.Equals("Sound"))
                {
                    OpenSoundProperties();
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Sample"))
                {
                    OpenSampleProperties();
                }
            }
        }

        internal void OpenSoundProperties()
        {
            EXSound SelectedSound = TreeNodeFunctions.GetSelectedSound(TreeView_File.SelectedNode.Name, SoundsList);
            Frm_EffectProperties FormSoundProps = new Frm_EffectProperties(SelectedSound)
            {
                Text = "Sound Properties",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            FormSoundProps.ShowDialog();
            FormSoundProps.Dispose();
        }

        internal void OpenSampleProperties()
        {
            EXSound ParentSound = TreeNodeFunctions.GetSelectedSound(TreeView_File.SelectedNode.Parent.Name, SoundsList);
            EXSample SelectedSample = TreeNodeFunctions.GetSelectedSample(ParentSound, TreeView_File.SelectedNode.Name);

            Frm_SampleProperties FormSampleProps = new Frm_SampleProperties(SelectedSample)
            {
                Text = "Sample Properties",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            FormSampleProps.ShowDialog();
            FormSampleProps.Dispose();
        }

        internal string OpenSaveAsDialog()
        {
            string SavePath = Generic.SaveFileBrowser("EuroSound Files (*.ESF)|*.ESF|All files (*.*)|*.*", 1, true, Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, EXFile.Hashcode));
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SaveData.SaveDataToEuroSoundFile(TreeView_File, SoundsList, SavePath);
                }
            }
            return SavePath;
        }

        internal void SetProgramStateShowToStatusBar(string NewStatus)
        {
            if (!NewStatus.Equals("CurrentStatus"))
            {
                ProgramStatus = NewStatus;
            }
            if (Statusbar.Visible)
            {
                StatusLabel.Text = ProgramStatus;
            }
        }

        internal void StatusBarTutorialModeShowText(string TextToDisplay)
        {
            if (Statusbar.Visible)
            {
                StatusLabel.Text = TextToDisplay;
            }
        }

        internal void StatusBarTutorialMode(bool MenuStripOpened)
        {
            if (Statusbar.Visible)
            {
                if (MenuStripOpened)
                {
                    StatusLabel.Text = "";
                }
                else
                {
                    StatusLabel.Text = ProgramStatus;
                }
            }
        }

        internal void RemoveFolderSelectedNode()
        {
            /*Check we are not trying to delete a root folder*/
            if (!(TreeView_File.SelectedNode == null || TreeView_File.SelectedNode.Tag.Equals("Root")))
            {
                /*Show warning*/
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Folder: " + TreeView_File.SelectedNode.Text, "Warning", false);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    /*Remove child nodes sounds and samples*/
                    IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
                    foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_File, TreeView_File.SelectedNode, ChildNodesCollection))
                    {
                        EXObjectsFunctions.RemoveSound(ChildNode.Name, SoundsList);
                    }
                    TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
                }
            }
        }
        internal void RemoveSampleSelectedNode()
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sample: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXSound ParentSound = TreeNodeFunctions.GetSelectedSound(TreeView_File.SelectedNode.Parent.Name, SoundsList);
                EXObjectsFunctions.RemoveSampleFromSound(ParentSound, TreeView_File.SelectedNode.Name);
                TreeView_File.SelectedNode.Remove();
            }
        }

        internal void RemoveSoundSelectedNode()
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sound: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXObjectsFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
            }
        }
    }
}