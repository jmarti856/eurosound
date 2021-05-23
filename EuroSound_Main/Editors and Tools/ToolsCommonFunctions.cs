using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
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

        //*===============================================================================================
        //* Progress Bar
        //*===============================================================================================
        internal static void ProgressBarReset(ProgressBar progressBarToReset)
        {
            if (progressBarToReset != null)
            {
                //Update Progress Bar
                if (progressBarToReset.InvokeRequired)
                {
                    try
                    {
                        progressBarToReset.Invoke((MethodInvoker)delegate
                        {
                            progressBarToReset.Value = 0;
                        });
                    }
                    catch
                    {

                    }
                }
                else
                {
                    progressBarToReset.Value = 0;
                }
            }
        }

        internal static void ProgressBarAddValue(ProgressBar progressBarToModify, int valueToAdd)
        {
            //Update Progress Bar
            if (progressBarToModify != null)
            {
                if (progressBarToModify.InvokeRequired)
                {
                    try
                    {
                        progressBarToModify.Invoke((MethodInvoker)delegate
                        {
                            progressBarToModify.Value += valueToAdd;
                        });
                    }
                    catch
                    {

                    }
                }
                else
                {
                    progressBarToModify.Value += valueToAdd;
                }
            }
        }

        //*===============================================================================================
        //* Tree View
        //*===============================================================================================
        internal static void TreeViewNodeRename(TreeView treeViewControl, NodeLabelEditEventArgs e)
        {
            //Check that we have selected a node, and we have not selected the root folder
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                //Check label is not null, sometimes can crash without this check
                if (e.Label != null)
                {
                    //Get text label
                    string labelText = e.Label.Trim();

                    //Check we are not renaming with an empty string
                    if (string.IsNullOrEmpty(labelText))
                    {
                        //Cancel edit
                        e.CancelEdit = true;
                    }
                    else
                    {
                        //Check that not exists an item with the same name
                        if (TreeNodeFunctions.CheckIfNodeExistsByText(treeViewControl, labelText))
                        {
                            MessageBox.Show(GenericFunctions.resourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.CancelEdit = true;
                        }
                        else
                        {
                            //Update tree node props
                            e.Node.Text = labelText;
                        }
                    }
                }
            }
            else
            {
                //Cancel edit
                e.CancelEdit = true;
            }
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        internal static void NodesDragginOver(TreeView treeViewControl, DragEventArgs e)
        {
            const float scrollRegion = 20;

            Point p = treeViewControl.PointToClient(new Point(e.X, e.Y));

            //See if we need to scroll up or down
            if ((p.Y + scrollRegion) > treeViewControl.Height)
            {
                //Call the API to scroll down
                SendMessage(treeViewControl.Handle, 277, 1, 0);
            }
            else if (p.Y < scrollRegion)
            {
                //Call thje API to scroll up
                SendMessage(treeViewControl.Handle, 277, 0, 0);
            }

            TreeNode node = treeViewControl.GetNodeAt(p.X, p.Y);
            treeViewControl.SelectedNode = node;
        }

        internal static void NodesDragginDrop(ProjectFile projectInfo, TreeView treeViewControl, DragEventArgs e)
        {
            //Retrieve the client coordinates of the drop location.
            Point targetPoint = treeViewControl.PointToClient(new Point(e.X, e.Y));

            //Retrieve the node at the drop location.
            TreeNode targetNode = treeViewControl.GetNodeAt(targetPoint);
            TreeNode findTargetNode = TreeNodeFunctions.FindRootNode(targetNode);

            TreeNode parentNode = targetNode;

            if (findTargetNode != null)
            {
                string destSection = findTargetNode.Text;
                string destNodeType = targetNode.Tag.ToString();

                //Retrieve the node that was dragged
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                string sourceSection = TreeNodeFunctions.FindRootNode(draggedNode).Text;

                //Confirm that the node at the drop location is not
                //the dragged node and that target node isn't null
                //(for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && draggedNode != null && targetNode != null)
                {
                    bool canDrop = true;
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !Object.ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }

                    if (canDrop)
                    {
                        /*
                        Confirm we are not outside the node section and that the destination place is a folder or the root
                        node section
                        */
                        if (sourceSection.Equals(destSection) && (destNodeType.Equals("Folder") || destNodeType.Equals("Root")))
                        {
                            //Remove the node from its current
                            //location and add it to the node at the drop location.
                            draggedNode.Remove();
                            targetNode.Nodes.Add(draggedNode);
                            targetNode.Expand();
                            treeViewControl.SelectedNode = draggedNode;
                            projectInfo.FileHasBeenModified = true;
                        }
                    }
                }
            }
        }

        internal static void NodesDraggin_Enter(ProjectFile projectInfo, TreeView treeViewControl, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null)
            {
                Point targetPoint = treeViewControl.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = treeViewControl.GetNodeAt(targetPoint);

                if (targetNode != null)
                {
                    //Type of nodes that are allowed to be re-ubicated
                    if (projectInfo.TypeOfData == (int)GenericFunctions.ESoundFileType.SoundBanks)
                    {
                        if (draggedNode.Tag.Equals("Folder") || draggedNode.Tag.Equals("Sound") || draggedNode.Tag.Equals("Audio") || draggedNode.Tag.Equals("Target"))
                        {
                            e.Effect = DragDropEffects.Move;
                        }
                    }
                    else if (projectInfo.TypeOfData == (int)GenericFunctions.ESoundFileType.MusicBanks)
                    {
                        if (draggedNode.Tag.Equals("Folder") || draggedNode.Tag.Equals("Music") || draggedNode.Tag.Equals("Target"))
                        {
                            e.Effect = DragDropEffects.Move;
                        }
                    }
                    treeViewControl.SelectedNode = targetNode;
                }
            }
        }
    }
}
