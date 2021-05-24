﻿using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.TreeViewLibraryFunctions
{
    public static class TreeNodeFunctions
    {
        public static EXAudio GetSelectedAudio(string SelectedNodeName, Dictionary<string, EXAudio> AudiosList)
        {
            if (AudiosList.ContainsKey(SelectedNodeName))
            {
                return AudiosList[SelectedNodeName];
            }
            else
            {
                return null;
            }
        }

        internal static bool CheckIfNodeExistsByText(TreeView SearchControl, string Name)
        {
            return (SearchNodeRecursiveByText(SearchControl.Nodes, Name.ToLower(), SearchControl, false) != null);
        }

        internal static void EditNodeLabel(TreeView TreeViewFile, TreeNode NodeToEdit)
        {
            if (NodeToEdit != null && NodeToEdit.Parent != null)
            {
                TreeViewFile.SelectedNode = NodeToEdit;
                TreeViewFile.LabelEdit = true;
                if (!NodeToEdit.IsEditing)
                {
                    NodeToEdit.BeginEdit();
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("TreeView_Error_EditingRootNode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static TreeNode FindRootNode(TreeNode treeNode)
        {
            if (treeNode != null)
            {
                while (treeNode.Parent != null)
                {
                    treeNode = treeNode.Parent;
                }
            }

            return treeNode;
        }

        internal static IList<TreeNode> GetNodesInsideFolder(TreeView SearchControl, TreeNode Selected, IList<TreeNode> Childs)
        {
            TreeNodeCollection NodesCollection = Selected.Nodes;
            for (int i = 0; i < NodesCollection.Count; i++)
            {
                if (NodesCollection[i].Tag.Equals("Sound"))
                {
                    Childs.Add(NodesCollection[i]);
                }
                else if (NodesCollection[i].Tag.Equals("Folder"))
                {
                    GetNodesInsideFolder(SearchControl, NodesCollection[i], Childs);
                }
                else
                {
                    break;
                }
            }

            return Childs;
        }

        internal static TreeNode SearchNodeRecursiveByText(IEnumerable nodes, string searchFor, TreeView TreeViewControl, bool MatchOnly)
        {
            foreach (TreeNode node in nodes)
            {
                if (MatchOnly)
                {
                    if (node.Text.ToLower().Contains(searchFor))
                    {
                        return node;
                    }
                }
                else
                {
                    if (node.Text.Equals(searchFor, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return node;
                    }
                }

                TreeNode result = SearchNodeRecursiveByText(node.Nodes, searchFor, TreeViewControl, MatchOnly);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        internal static void TreeNodeAddNewNode(string Parent, string n_Name, string DisplayName, int SelectedImageIndex, int ImageIndex, string Tag, bool ParentIsExpanded, bool NodeIsExpanded, bool NodeIsSelected, Color TextColor, TreeView TreeViewToEdit)
        {
            TreeNode[] ParentNode = TreeViewToEdit.Nodes.Find(Parent, true);

            if (ParentNode.Length > 0)
            {
                //--Add new properties--
                TreeNode NewNode = new TreeNode
                {
                    Name = n_Name,
                    Text = DisplayName,
                    ForeColor = TextColor,
                    Tag = Tag,
                    SelectedImageIndex = SelectedImageIndex,
                    ImageIndex = ImageIndex
                };

                //--Add element to the tree node--
                if (TreeViewToEdit.InvokeRequired)
                {
                    try
                    {
                        TreeViewToEdit.Invoke((MethodInvoker)delegate
                        {
                            ParentNode[0].Nodes.Add(NewNode);

                            //--Apply Node State--
                            if (ParentIsExpanded)
                            {
                                NewNode.Parent.Expand();
                            }

                            if (NodeIsExpanded)
                            {
                                NewNode.Expand();
                            }

                            if (NodeIsSelected)
                            {
                                TreeViewToEdit.SelectedNode = NewNode;
                                NewNode.EnsureVisible();
                            }
                        });
                    }
                    catch (ObjectDisposedException)
                    {

                    }
                }
                else
                {
                    ParentNode[0].Nodes.Add(NewNode);

                    //--Apply Node State--
                    if (ParentIsExpanded)
                    {
                        NewNode.Parent.Expand();
                    }

                    if (NodeIsExpanded)
                    {
                        NewNode.Expand();
                    }

                    if (NodeIsSelected)
                    {
                        TreeViewToEdit.SelectedNode = NewNode;
                        NewNode.EnsureVisible();
                    }
                }
            }
        }

        internal static void TreeNodeDeleteNode(TreeView TreeViewFile, TreeNode nodeToCheck)
        {
            if (nodeToCheck != null)
            {
                //--Root node can't be deleted--
                if (nodeToCheck.Level > 0)
                {

                    TreeViewFile.Nodes.Remove(nodeToCheck);
                }
            }
        }

        internal static void TreeNodeSetNodeImage(TreeNode Node, int SelectedImageIndex, int ImageIndex)
        {
            if (Node != null)
            {
                Node.SelectedImageIndex = SelectedImageIndex;
                Node.ImageIndex = ImageIndex;
            }
        }

        internal static void ChangeNodeColor(TreeNode nodeToChange, ProjectFile ProjectInfo)
        {
            int selectedColor = BrowsersAndDialogs.ColorPickerDialog(nodeToChange.ForeColor);

            if (selectedColor != -1)
            {
                nodeToChange.ForeColor = Color.FromArgb(selectedColor);
                ProjectInfo.FileHasBeenModified = true;
            }
        }
    }
}