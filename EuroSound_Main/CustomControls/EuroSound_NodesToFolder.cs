using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_NodesToFolder : Form
    {
        private Dictionary<string, string> SoundsDictionary = new Dictionary<string, string>();
        private TreeView treeviewcontrol;
        public EuroSound_NodesToFolder(TreeView TreeViewControl)
        {
            InitializeComponent();
            treeviewcontrol = TreeViewControl;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            string selectedParent;

            selectedParent = Combobox_AvailableFolders.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(selectedParent))
            {
                TreeNode NewParentNode = treeviewcontrol.Nodes.Find(selectedParent, true)[0];
                foreach (KeyValuePair<string, string> item in ListBox_Items.SelectedItems)
                {
                    TreeNode NodeToMove = treeviewcontrol.Nodes.Find(item.Key.ToString(), true)[0];
                    treeviewcontrol.Nodes.Remove(NodeToMove);
                    TreeNodeFunctions.TreeNodeAddNewNode(NewParentNode.Name, NodeToMove.Name, NodeToMove.Text, NodeToMove.SelectedImageIndex, NodeToMove.ImageIndex, NodeToMove.Tag.ToString(), NodeToMove.ForeColor, treeviewcontrol);
                    if (NodeToMove.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in NodeToMove.Nodes)
                        {
                            TreeNodeFunctions.TreeNodeAddNewNode(child.Parent.Name, child.Name, child.Text, child.SelectedImageIndex, child.ImageIndex, child.Tag.ToString(), child.ForeColor, treeviewcontrol);
                        }
                    }
                }
            }
        }
        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void Combobox_SoudsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ParentName = Combobox_SoudsType.SelectedItem.ToString();

            SoundsDictionary.Clear();

            Combobox_AvailableFolders.DataSource = GetAvailableFolders(treeviewcontrol, ParentName);
            foreach (TreeNode node in treeviewcontrol.Nodes)
            {
                if (node.Name.Equals(ParentName))
                {
                    GetSoundsName(node);
                }
            }
            ShowDataInList();
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

        private void GetSoundsName(TreeNode node)
        {
            if (node.Tag.Equals("Sound"))
            {
                SoundsDictionary.Add(node.Name, node.Text);
            }
            foreach (TreeNode tn in node.Nodes)
            {
                GetSoundsName(tn);
            }
        }

        private void ShowDataInList()
        {
            ListBox_Items.DataSource = new BindingSource(SoundsDictionary, null);
            ListBox_Items.DisplayMember = "Value";
            ListBox_Items.ValueMember = "Key";
        }
    }
}