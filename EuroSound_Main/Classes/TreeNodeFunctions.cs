using EuroSound_Application.SoundBanksEditor;
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
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("TreeView_Error_EditingRootNode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (node.Text.ToLower().Equals(searchFor))
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

        internal static void TreeNodeAddNewNode(string Parent, string n_Name, string DisplayName, int SelectedImageIndex, int ImageIndex, string Tag, Color TextColor, TreeView TreeViewToEdit)
        {
            TreeNode[] ParentNode = TreeViewToEdit.Nodes.Find(Parent, true);

            if (ParentNode.Length > 0)
            {
                /*--Add new properties--*/
                TreeNode NewNode = new TreeNode
                {
                    Name = n_Name,
                    Text = DisplayName,
                    ForeColor = TextColor,
                    Tag = Tag
                };

                /*--Add element to the tree node--*/
                if (!TreeViewToEdit.IsDisposed)
                {
                    TreeViewToEdit.Invoke((MethodInvoker)delegate
                    {
                        ParentNode[0].Nodes.Add(NewNode);
                        ParentNode[0].Expand();

                        /*--Set image--*/
                        TreeNodeSetNodeImage(NewNode, SelectedImageIndex, ImageIndex);
                    });
                }
            }
        }

        internal static void TreeNodeDeleteNode(TreeView TreeViewFile, TreeNode Name, string NodeTag)
        {
            /*--Root node can't be deleted--*/
            if (!NodeTag.Equals("Root"))
            {
                if (Name != null)
                {
                    TreeViewFile.Nodes.Remove(Name);
                }
            }
        }

        internal static void TreeNodeSetNodeImage(TreeNode Node, int SelectedImageIndex, int ImageIndex)
        {
            Node.SelectedImageIndex = SelectedImageIndex;
            Node.ImageIndex = ImageIndex;
        }

        internal static void MoveDown(TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                }
            }
            else if (view != null && view.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                }
            }
        }

        public static void MoveUp(TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                }
            }
        }
    }
}