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
        internal static void RemoveEngineXObject(uint objectID, TreeView treeViewControl, TreeNode selectedNode, object dataDictionary, ProjectFile currentProject, Stack<object> UndoListSounds, object UndoListNodes, ToolStripMenuItem MenuItem_Edit_Undo)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Music:", treeViewControl.SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveObject(objectID, treeViewControl, selectedNode, dataDictionary, currentProject, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo);
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveObject(objectID, treeViewControl, selectedNode, dataDictionary, currentProject, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo);
            }
        }

        private static void RemoveObject(uint objectType, TreeView treeViewControl, TreeNode selectedNode, object dataDictionary, ProjectFile currentProject, Stack<object> UndoListSounds, object UndoListNodes, ToolStripMenuItem MenuItem_Edit_Undo)
        {
            if (currentProject.TypeOfData == (int)GenericFunctions.ESoundFileType.MusicBanks)
            {
                Dictionary<uint, EXMusic> MusicsDictionary = (Dictionary<uint, EXMusic>)dataDictionary;
                //Remove Item
                if (objectType == (int)GenericFunctions.EXObjectType.EXMusic)
                {
                    uint objectKey = uint.Parse(selectedNode.Name);
                    if (MusicsDictionary.ContainsKey(objectKey))
                    {
                        SaveSnapshot(selectedNode.Name, MusicsDictionary[objectKey], selectedNode, currentProject.TypeOfData, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo);
                        MusicsDictionary.Remove(objectKey);
                        TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                    }
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXMusicFolder)
                {
                    //Remove child nodes sounds and samples
                    IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
                    foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(treeViewControl, selectedNode, ChildNodesCollection))
                    {
                        uint objectKey = uint.Parse(ChildNode.Name);
                        if (MusicsDictionary.ContainsKey(objectKey))
                        {
                            MusicsDictionary.Remove(objectKey);
                        }
                    }
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                }
            }
            if (currentProject.TypeOfData == (int)GenericFunctions.ESoundFileType.StreamSounds)
            {
                //Remove Item
                Dictionary<uint, EXSoundStream> StreamSoundsDictionary = (Dictionary<uint, EXSoundStream>)dataDictionary;
                uint objectKey = uint.Parse(selectedNode.Name);
                if (StreamSoundsDictionary.ContainsKey(objectKey))
                {
                    SaveSnapshot(selectedNode.Name, StreamSoundsDictionary[objectKey], selectedNode, currentProject.TypeOfData, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo);
                    StreamSoundsDictionary.Remove(objectKey);
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                }
            }
            if (currentProject.TypeOfData == (int)GenericFunctions.ESoundFileType.SoundBanks)
            {
                if (objectType == (int)GenericFunctions.EXObjectType.EXAudio)//Audio
                {
                    Dictionary<string, EXAudio> audioDictionary = (Dictionary<string, EXAudio>)dataDictionary;
                    if (audioDictionary.ContainsKey(selectedNode.Name))
                    {
                        SaveSnapshot(selectedNode.Name, audioDictionary[selectedNode.Name], selectedNode, currentProject.TypeOfData, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo);
                        audioDictionary.Remove(selectedNode.Name);
                        TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                    }
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXSample)//Sample
                {
                    EXSound parentSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(selectedNode.Parent.Name), (Dictionary<uint, EXSound>)dataDictionary);
                    if (parentSound != null)
                    {
                        parentSound.Samples.Remove(uint.Parse(selectedNode.Name));
                    }
                    selectedNode.Remove();
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXSound)//Sound
                {
                    Dictionary<uint, EXSound> soundsDictionary = (Dictionary<uint, EXSound>)dataDictionary;
                    uint objectKey = uint.Parse(selectedNode.Name);
                    if (soundsDictionary.ContainsKey(objectKey))
                    {
                        SaveSnapshot(selectedNode.Name, soundsDictionary[objectKey], selectedNode, currentProject.TypeOfData, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo);
                        soundsDictionary.Remove(objectKey);
                        TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                    }
                }
                else if (objectType == (int)GenericFunctions.EXObjectType.EXSoundFolder)//Folder
                {
                    //Remove child nodes sounds and samples
                    IList<TreeNode> childNodesCollection = new List<TreeNode>();
                    foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(treeViewControl, selectedNode, childNodesCollection))
                    {
                        Dictionary<uint, EXSound> soundsDictionary = (Dictionary<uint, EXSound>)dataDictionary;
                        if (soundsDictionary.ContainsKey(uint.Parse(ChildNode.Name)))
                        {
                            soundsDictionary.Remove(uint.Parse(ChildNode.Name));
                        }
                    }
                    TreeNodeFunctions.TreeNodeDeleteNode(treeViewControl, selectedNode, selectedNode.Tag.ToString());
                }
            }
            currentProject.FileHasBeenModified = true;
        }

        //*===============================================================================================
        //* UNDO AND REDO
        //*===============================================================================================
        internal static void SaveSnapshot(string ItemKey, object SoundToSave, TreeNode NodeToSave, int typeOfFile, Stack<object> UndoListSounds, object UndoListNodes, ToolStripMenuItem MenuItem_Edit_Undo)
        {
            switch (typeOfFile)
            {
                case (int)GenericFunctions.ESoundFileType.StreamSounds:
                    UndoListSounds.Push(new KeyValuePair<uint, EXSoundStream>(uint.Parse(ItemKey), (EXSoundStream)SoundToSave));
                    ((Stack<TreeNode>)UndoListNodes).Push(NodeToSave);
                    break;
                case (int)GenericFunctions.ESoundFileType.MusicBanks:
                    UndoListSounds.Push(new KeyValuePair<uint, EXMusic>(uint.Parse(ItemKey), (EXMusic)SoundToSave));
                    ((Stack<TreeNode>)UndoListNodes).Push(NodeToSave);
                    break;
                case (int)GenericFunctions.ESoundFileType.SoundBanks:
                    if (SoundToSave.GetType() == typeof(EXSound))
                    {
                        UndoListSounds.Push(new KeyValuePair<uint, EXSound>(uint.Parse(ItemKey), (EXSound)SoundToSave));
                    }
                    else if (SoundToSave.GetType() == typeof(EXAudio))
                    {
                        UndoListSounds.Push(new KeyValuePair<string, EXAudio>(ItemKey, (EXAudio)SoundToSave));
                    }
                    ((Stack<KeyValuePair<string, TreeNode>>)UndoListNodes).Push(new KeyValuePair<string, TreeNode>(NodeToSave.Parent.Name, NodeToSave));
                    break;
            }

            //Enable or disable the Undo and Redo menu items.
            EnableUndo(MenuItem_Edit_Undo, UndoListNodes, typeOfFile);
        }

        //Enable or disable the Undo and Redo menu items.
        internal static void EnableUndo(ToolStripMenuItem MenuItem_Edit_Undo, object UndoListNodes, int typeOfFile)
        {
            switch (typeOfFile)
            {
                case (int)GenericFunctions.ESoundFileType.StreamSounds:
                    MenuItem_Edit_Undo.Enabled = (((Stack<TreeNode>)UndoListNodes).Count > 0);
                    break;
                case (int)GenericFunctions.ESoundFileType.MusicBanks:
                    MenuItem_Edit_Undo.Enabled = (((Stack<TreeNode>)UndoListNodes).Count > 0);
                    break;
                case (int)GenericFunctions.ESoundFileType.SoundBanks:
                    MenuItem_Edit_Undo.Enabled = (((Stack<KeyValuePair<string, TreeNode>>)UndoListNodes).Count > 0);
                    break;
            }
        }
    }
}
