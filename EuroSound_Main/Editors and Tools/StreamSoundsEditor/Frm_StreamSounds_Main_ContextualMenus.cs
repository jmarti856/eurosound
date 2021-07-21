using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.CustomControls.DebugTypes;
using EuroSound_Application.Editors_and_Tools;
using EuroSound_Application.Editors_and_Tools.ApplicationTargets;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_Main
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
            string Name = BrowsersAndDialogs.InputBoxDialog("Enter a name for new a new streaming sound.", "New Streaming Sound");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, Name, true))
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_StreamData.SelectedNode.Name, SoundID.ToString(), Name, 2, 2, (byte)Enumerations.TreeNodeType.Sound, true, true, false, SystemColors.WindowText, TreeView_StreamData);

                    //Add Empty Sound
                    EXSoundStream Sound = new EXSoundStream();
                    if (!StreamSoundsList.ContainsKey(SoundID))
                    {
                        StreamSoundsList.Add(SoundID, Sound);
                    }

                    //Update project status variable
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuFolder_AddTarget_Click(object sender, EventArgs e)
        {
            EXAppTarget outTarget = new EXAppTarget
            {
                BinaryName = EXAppTarget_Functions.GetBinaryName(ProjectInfo, GlobalPreferences.SelectedProfileName)
            };
            using (Frm_ApplicationTarget newOutTarget = new Frm_ApplicationTarget(outTarget, null, TreeView_StreamData) { Owner = this })
            {
                newOutTarget.ShowDialog();

                if (newOutTarget.DialogResult == DialogResult.OK)
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_StreamData.SelectedNode.Name, SoundID.ToString(), outTarget.Name, 10, 10, (byte)Enumerations.TreeNodeType.Target, true, true, false, SystemColors.WindowText, TreeView_StreamData);

                    //Add Target
                    OutputTargets.Add(SoundID, outTarget);

                    //Update project status variable
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void ContextMenuMain_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuMain_TextColor_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.ChangeNodeColor(TreeView_StreamData.SelectedNode, ProjectInfo);
        }

        //*===============================================================================================
        //* ContextMenu_Sounds
        //*===============================================================================================
        private void ContextMenuSounds_Rename_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_Delete_Click(object sender, System.EventArgs e)
        {
            ToolsCommonFunctions.RemoveEngineXObject("Remove sound:", 0, TreeView_StreamData, TreeView_StreamData.SelectedNode, StreamSoundsList, null, ProjectInfo, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo, Tag.ToString());
        }

        private void ContextMenuSounds_Properties_Click(object sender, EventArgs e)
        {
            if (TreeView_StreamData.SelectedNode != null)
            {
                OpenSoundPropertiesForm(TreeView_StreamData.SelectedNode);
            }
        }

        private void ContextMenuSounds_ExportESIF_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = TreeView_StreamData.SelectedNode;
            string ExportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, SelectedNode.Text);

            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportStreamSoundbank(ExportPath, uint.Parse(SelectedNode.Name), StreamSoundsList, TreeView_StreamData);
            }
        }

        private void ContextMenuSounds_MoveUp_Click(object sender, EventArgs e)
        {
            MoveUp(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_MoveDown_Click(object sender, EventArgs e)
        {
            MoveDown(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuSounds_TextColor_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.ChangeNodeColor(TreeView_StreamData.SelectedNode, ProjectInfo);
        }

        //*===============================================================================================
        //* ContextMenu_Target
        //*===============================================================================================
        private void ContextMenuTargets_Delete_Click(object sender, EventArgs e)
        {
            ToolsCommonFunctions.RemoveTargetSelectedNode(TreeView_StreamData.SelectedNode, OutputTargets, TreeView_StreamData, ProjectInfo);
        }

        private void ContextMenuTargets_TextColor_Click(object sender, EventArgs e)
        {
            TreeNodeFunctions.ChangeNodeColor(TreeView_StreamData.SelectedNode, ProjectInfo);
        }

        private void ContextMenuTargets_Properties_Click(object sender, EventArgs e)
        {
            OpenTargetProperties(TreeView_StreamData.SelectedNode);
        }

        private void ContextMenuTargets_Output_Click(object sender, EventArgs e)
        {
            //Debug options form
            int debugOptions = 0;
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                using (EuroSound_DebugTypes DebugOpts = new EuroSound_DebugTypes(new string[] { "File start 1", "File start 2" }))
                {
                    DebugOpts.Owner = Owner;
                    if (DebugOpts.ShowDialog() == DialogResult.OK)
                    {
                        debugOptions = DebugOpts.CheckedOptions;
                    }
                }
            }

            //Build form file
            EXAppTarget selectedTarget = OutputTargets[uint.Parse(TreeView_StreamData.SelectedNode.Name.ToString())];
            using (Frm_OutputTargetFileBuilder buildSFX = new Frm_OutputTargetFileBuilder(ProjectInfo, selectedTarget, OutputTargets, debugOptions, Tag.ToString()) { Owner = this })
            {
                buildSFX.ShowDialog();
            }
        }
    }
}
