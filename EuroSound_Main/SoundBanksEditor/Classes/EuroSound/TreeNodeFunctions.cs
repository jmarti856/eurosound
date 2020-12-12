using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace EuroSound_Application
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

        public static EXSample GetSelectedSample(EXSound ParentSound, string SelectedNodeName)
        {
            for (int i = 0; i < ParentSound.Samples.Count; i++)
            {
                if (ParentSound.Samples[i].Name.Equals(SelectedNodeName))
                {
                    return ParentSound.Samples[i];
                }
            }

            return null;
        }

        internal static void EditNodeLabel(TreeView TreeViewFile, TreeNode NodeToEdit, ResourceManager ResManager)
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
                MessageBox.Show(ResManager.GetString("TreeView_Error_EditingRootNode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        internal static void TreeNodeSetNodeImage(TreeNode Node, int SelectedImageIndex, int ImageIndex)
        {
            Node.SelectedImageIndex = SelectedImageIndex;
            Node.ImageIndex = ImageIndex;
        }

        internal static IList<TreeNode> GetNodesInsideFolder(TreeView SearchControl, TreeNode Selected, IList<TreeNode> Childs)
        {
            foreach (TreeNode ChildNode in Selected.Nodes)
            {
                if (ChildNode.Tag.Equals("Sound"))
                {
                    Childs.Add(ChildNode);
                }
                else if (ChildNode.Tag.Equals("Folder"))
                {
                    GetNodesInsideFolder(SearchControl, ChildNode, Childs);
                }
                else
                {
                    break;
                }
            }

            return Childs;
        }

        internal static TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }

            return treeNode;
        }

        internal static bool CheckIfNodeExists(TreeView SearchControl, string Name)
        {
            bool Exists;

            Exists = SearchRecursive(SearchControl.Nodes, Name, SearchControl, false);

            return Exists;
        }

        internal static bool SearchRecursive(IEnumerable nodes, string searchFor, TreeView TreeViewControl, bool MatchOnly)
        {
            foreach (TreeNode node in nodes)
            {
                if (MatchOnly)
                {
                    if (node.Text.ToUpper().Contains(searchFor))
                    {
                        TreeViewControl.Invoke((MethodInvoker)delegate
                        {
                            TreeViewControl.SelectedNode = node;
                        });
                        return true;
                    }
                }
                else
                {
                    if (node.Text.ToUpper().Equals(searchFor))
                    {
                        TreeViewControl.Invoke((MethodInvoker)delegate
                        {
                            TreeViewControl.SelectedNode = node;
                        });
                        return true;
                    }
                }

                if (SearchRecursive(node.Nodes, searchFor, TreeViewControl, MatchOnly))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
