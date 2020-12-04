using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public static class TreeNodeFunctions
    {
        public static EXSound GetSelectedSound(string SelectedNodeName, List<EXSound> SoundsList)
        {
            for (int i = 0; i < SoundsList.Count; i++)
            {
                if (SoundsList[i].Name.Equals(SelectedNodeName))
                {
                    return SoundsList[i];
                }
            }

            return null;
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
                MessageBox.Show("No tree node selected or selected node is a root node.\n" + "Editing of root nodes is not allowed.", "Invalid selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Debug.WriteLine(string.Format("INFO -- Element {0} removed from the tree node.", Name));
                }
            }
            else
            {
                Debug.WriteLine("WARNING -- Trying to remove root node.");
            }
        }

        internal static void TreeNodeAddNewNode(string Parent, string DisplayName, int SelectedImageIndex, int ImageIndex, string Tag, Color TextColor, TreeView TreeViewToEdit)
        {
            TreeNode[] ParentNode = TreeViewToEdit.Nodes.Find(Parent, true);

            if (ParentNode.Length > 0)
            {
                /*--Add new properties--*/
                TreeNode NewNode = new TreeNode
                {
                    Name = EXObjectsFunctions.RemoveWhiteSpaces(DisplayName),
                    Text = DisplayName,
                    ForeColor = TextColor,
                    Tag = Tag
                };

                /*--Add element to the tree node--*/
                TreeViewToEdit.Invoke((MethodInvoker)delegate
                {
                    ParentNode[0].Nodes.Add(NewNode);
                    ParentNode[0].Expand();
                    Debug.WriteLine(string.Format("INFO -- Element {0} added to the tree node.", NewNode.Name));

                    /*--Set image--*/
                    TreeNodeSetNodeImage(NewNode, SelectedImageIndex, ImageIndex);
                    Debug.WriteLine(string.Format("INFO -- Element {0} has the image index {1}.", NewNode.Name, ImageIndex));
                });
            }
            else
            {
                Debug.WriteLine(string.Format("ERROR -- Element {0} can't be added to the tree node.", DisplayName));
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
            bool Exists = false;

            TreeNode[] nod = SearchControl.Nodes.Find(Name, true);
            if (nod.Length > 0)
            {
                Exists = true;
            }

            return Exists;
        }
    }
}
