using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Application.TreeViewLibraryFunctions
{
    public static class TreeNodeFunctions
    {
        [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetScrollPos(int hWnd, int nBar);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;

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
                if (Convert.ToByte(NodesCollection[i].Tag) == (byte)Enumerations.TreeNodeType.Sound)
                {
                    Childs.Add(NodesCollection[i]);
                }
                else if (Convert.ToByte(NodesCollection[i].Tag) == (byte)Enumerations.TreeNodeType.Folder)
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

        internal static void TreeNodeAddNewNode(string Parent, string n_Name, string displayName, int selectedImageIndex, int imageIndex, byte nodeTag, bool parentIsExpanded, bool nodeIsExpanded, bool nodeIsSelected, Color textColor, TreeView treeViewToEdit)
        {
            TreeNode[] ParentNode = treeViewToEdit.Nodes.Find(Parent, true);

            if (ParentNode.Length > 0)
            {
                //--Add new properties--
                TreeNode NewNode = new TreeNode
                {
                    Name = n_Name,
                    Text = displayName,
                    ForeColor = textColor,
                    Tag = nodeTag,
                    SelectedImageIndex = selectedImageIndex,
                    ImageIndex = imageIndex
                };

                //--Add element to the tree node--
                if (treeViewToEdit.InvokeRequired)
                {
                    if (!treeViewToEdit.Disposing || treeViewToEdit.IsDisposed)
                    {
                        treeViewToEdit.Invoke((MethodInvoker)delegate
                        {
                            ParentNode[0].Nodes.Add(NewNode);

                            //--Apply Node State--
                            if (parentIsExpanded)
                            {
                                NewNode.Parent.Expand();
                            }

                            if (nodeIsExpanded)
                            {
                                NewNode.Expand();
                            }

                            if (nodeIsSelected)
                            {
                                treeViewToEdit.SelectedNode = NewNode;
                                NewNode.EnsureVisible();
                            }
                        });
                    }
                }
                else
                {
                    ParentNode[0].Nodes.Add(NewNode);

                    //--Apply Node State--
                    if (parentIsExpanded)
                    {
                        NewNode.Parent.Expand();
                    }

                    if (nodeIsExpanded)
                    {
                        NewNode.Expand();
                    }

                    if (nodeIsSelected)
                    {
                        treeViewToEdit.SelectedNode = NewNode;
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

                //Update project status variable
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        internal static Point GetTreeViewScrollPos(TreeView treeView)
        {
            return new Point(
                GetScrollPos((int)treeView.Handle, SB_HORZ),
                GetScrollPos((int)treeView.Handle, SB_VERT));
        }

        internal static void SetTreeViewScrollPos(TreeView treeView, Point scrollPosition)
        {
            SetScrollPos(treeView.Handle, SB_HORZ, scrollPosition.X, true);
            SetScrollPos(treeView.Handle, SB_VERT, scrollPosition.Y, true);
        }
    }
}