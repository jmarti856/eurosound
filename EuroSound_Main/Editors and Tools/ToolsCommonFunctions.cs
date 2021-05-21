using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.Editors_and_Tools
{
    internal static class ToolsCommonFunctions
    {
        //*===============================================================================================
        //* TARGETS
        //*===============================================================================================
        internal static void RemoveTargetSelectedNode(TreeNode SelectedNode, Dictionary<uint, EXAppTarget> OutputTargets, TreeView formTreeView, ProjectFile currrentProject)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox warningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Target:", SelectedNode.Text }), "Warning", true);
                if (warningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = warningDialog.ShowWarningAgain;
                    RemoveTarget(OutputTargets, formTreeView);
                }
                warningDialog.Dispose();
            }
            else
            {
                RemoveTarget(OutputTargets, formTreeView);
            }
            currrentProject.FileHasBeenModified = true;
        }

        private static void RemoveTarget(Dictionary<uint, EXAppTarget> OutputTargets, TreeView formTreeView)
        {
            EXAppTarget parentTarget = EXAppTarget_Functions.ReturnTargetFromDictionary(uint.Parse(formTreeView.SelectedNode.Name), OutputTargets);
            if (parentTarget != null)
            {
                if (OutputTargets.ContainsKey(uint.Parse(formTreeView.SelectedNode.Name)))
                {
                    OutputTargets.Remove(uint.Parse(formTreeView.SelectedNode.Name));
                }
            }
            formTreeView.SelectedNode.Remove();
        }

        //*===============================================================================================
        //* Remove EngineXObject
        //*===============================================================================================
        internal static void RemoveEngineXObject(uint objectID, TreeView treeViewControl, TreeNode selectedNode, object dataDictionary, int fileType, ProjectFile currentProject)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Music:", treeViewControl.SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveObject(objectID, treeViewControl, selectedNode, dataDictionary, fileType, currentProject);
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveObject(objectID, treeViewControl, selectedNode, dataDictionary, fileType, currentProject);
            }
        }

        private static void RemoveObject(uint objectType, TreeView treeViewControl, TreeNode selectedNode, object dataDictionary, int fileType, ProjectFile currentProject)
        {
            if (fileType == (int)GenericFunctions.ESoundFileType.MusicBanks)
            {
                //Remove Item
                Dictionary<uint, EXMusic> MusicsDictionary = (Dictionary<uint, EXMusic>)dataDictionary;
                if (MusicsDictionary.ContainsKey(uint.Parse(selectedNode.Name)))
                {
                    MusicsDictionary.Remove(uint.Parse(selectedNode.Name));
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                }
            }
            if (fileType == (int)GenericFunctions.ESoundFileType.StreamSounds)
            {
                //Remove Item
                Dictionary<uint, EXSoundStream> StreamSoundsDictionary = (Dictionary<uint, EXSoundStream>)dataDictionary;
                if (StreamSoundsDictionary.ContainsKey(uint.Parse(selectedNode.Name)))
                {
                    StreamSoundsDictionary.Remove(uint.Parse(selectedNode.Name));
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                }
            }
            if (fileType == (int)GenericFunctions.ESoundFileType.SoundBanks)
            {
                if (objectType == (int)GenericFunctions.EXObjectType.EXAudio)//Audio
                {
                    EXSoundbanksFunctions.DeleteAudio((Dictionary<string, EXAudio>)dataDictionary, selectedNode.Name);
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXSample)//Sample
                {
                    EXSound parentSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(treeViewControl.SelectedNode.Parent.Name), (Dictionary<uint, EXSound>)dataDictionary);
                    if (parentSound != null)
                    {
                        parentSound.Samples.Remove(uint.Parse(treeViewControl.SelectedNode.Name));
                    }
                    treeViewControl.SelectedNode.Remove();
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXSound)//Sound
                {
                    EXSoundbanksFunctions.DeleteSound(selectedNode.Name, (Dictionary<uint, EXSound>)dataDictionary);
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, treeViewControl.SelectedNode.Tag.ToString());
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXSoundFolder)//Folder
                {
                    //Remove child nodes sounds and samples
                    IList<TreeNode> childNodesCollection = new List<TreeNode>();
                    foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(treeViewControl, treeViewControl.SelectedNode, childNodesCollection))
                    {
                        EXSoundbanksFunctions.DeleteSound(ChildNode.Name, (Dictionary<uint, EXSound>)dataDictionary);
                    }
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, treeViewControl.SelectedNode, treeViewControl.SelectedNode.Tag.ToString());
                }
            }
            currentProject.FileHasBeenModified = true;
        }
    }
}
