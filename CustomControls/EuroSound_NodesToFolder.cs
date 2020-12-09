using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    public partial class EuroSound_NodesToFolder : Form
    {
        TreeView treeviewcontrol;


        public EuroSound_NodesToFolder(TreeView TreeViewControl)
        {
            InitializeComponent();
            treeviewcontrol = TreeViewControl;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            string selectedParent;

            selectedParent = Combobox_AvailableFolders.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(selectedParent))
            {
                TreeNode NewParentNode = treeviewcontrol.Nodes.Find(selectedParent, true)[0];
                foreach (string item in ListBox_Items.SelectedItems)
                {

                    TreeNode NodeToMove = treeviewcontrol.Nodes.Find(item, true)[0];
                    treeviewcontrol.Nodes.Remove(NodeToMove);
                    TreeNodeAddNewNode(NewParentNode.Name, NodeToMove.Name, NodeToMove.Text, NodeToMove.SelectedImageIndex, NodeToMove.ImageIndex, NodeToMove.Tag.ToString(), NodeToMove.ForeColor, treeviewcontrol);
                    if (NodeToMove.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in NodeToMove.Nodes)
                        {
                            TreeNodeAddNewNode(child.Parent.Name, child.Name, child.Text, child.SelectedImageIndex, child.ImageIndex, child.Tag.ToString(), child.ForeColor, treeviewcontrol);
                        }
                    }
                }
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Combobox_SoudsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> SoundsName = new List<string>();
            string ParentName = Combobox_SoudsType.SelectedItem.ToString();

            Combobox_AvailableFolders.DataSource = GetAvailableFolders(treeviewcontrol, ParentName);
            foreach (TreeNode node in treeviewcontrol.Nodes)
            {
                if (node.Name.Equals(ParentName))
                {
                    GetSoundsName(node, SoundsName);
                }
            }
            PrintListToListBox(ListBox_Items, SoundsName);
        }

        private List<string> GetAvailableFolders(TreeView control, string ParentName)
        {
            List<string> AvailableFolders = new List<string>();

            foreach (TreeNode node in control.Nodes)
            {
                if (node.Name.Equals(ParentName))
                {
                    GetFolders(node, AvailableFolders);
                }
            }

            return AvailableFolders;
        }

        private void GetFolders(TreeNode node, List<string> foldersList)
        {
            if (node.Tag.Equals("Folder"))
            {
                foldersList.Add(node.Name);
            }
            foreach (TreeNode tn in node.Nodes)
            {
                GetFolders(tn, foldersList);
            }
        }

        private void GetSoundsName(TreeNode node, List<string> sounds)
        {
            if (node.Tag.Equals("Sound"))
            {
                sounds.Add(node.Name);
            }
            foreach (TreeNode tn in node.Nodes)
            {
                GetSoundsName(tn, sounds);
            }
        }

        private void PrintListToListBox(ListBox ListControl, List<string> SoundsList)
        {
            ListBox_Items.Items.Clear();
            foreach (string item in SoundsList)
            {
                ListControl.Items.Add(item);
            }
        }

        private void TreeNodeAddNewNode(string Parent, string n_Name, string DisplayName, int SelectedImageIndex, int ImageIndex, string Tag, Color TextColor, TreeView TreeViewToEdit)
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
                TreeViewToEdit.Invoke((MethodInvoker)delegate
                {
                    ParentNode[0].Nodes.Add(NewNode);
                    ParentNode[0].Expand();

                    /*--Set image--*/
                    TreeNodeSetNodeImage(NewNode, SelectedImageIndex, ImageIndex);
                });

            }
        }
        private void TreeNodeSetNodeImage(TreeNode Node, int SelectedImageIndex, int ImageIndex)
        {
            Node.SelectedImageIndex = SelectedImageIndex;
            Node.ImageIndex = ImageIndex;
        }
    }
}
