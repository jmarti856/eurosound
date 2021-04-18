using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.MoveMultiplesNodesForm
{
    public partial class EuroSound_NodesToFolder : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private Dictionary<string, string> SoundsDictionary = new Dictionary<string, string>();
        private TreeView ParentTreeViewControl;
        private string CurrentLoadedData, DestinationFolder;

        public EuroSound_NodesToFolder(TreeView TreeViewControl, string Section, string Destination)
        {
            InitializeComponent();
            ParentTreeViewControl = TreeViewControl;
            CurrentLoadedData = Section;
            DestinationFolder = Destination;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void EuroSound_NodesToFolder_Load(object sender, EventArgs e)
        {
            if (CurrentLoadedData.Equals("AudioData"))
            {
                Combobox_DataType.SelectedIndex = 0;
            }
            else if (CurrentLoadedData.Equals("Sounds"))
            {
                Combobox_DataType.SelectedIndex = 1;
            }
            else
            {
                Combobox_DataType.SelectedIndex = 2;
            }

            LoadData(CurrentLoadedData);
        }


        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
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
                TreeNode NewParentNode = ParentTreeViewControl.Nodes.Find(selectedParent, true)[0];

                /*Recursively move nodes to the new folder*/
                foreach (KeyValuePair<string, string> item in ListBox_Items.SelectedItems)
                {
                    TreeNode NodeToMove = ParentTreeViewControl.Nodes.Find(item.Key.ToString(), true)[0];
                    ParentTreeViewControl.Nodes.Remove(NodeToMove);
                    TreeNodeFunctions.TreeNodeAddNewNode(NewParentNode.Name, NodeToMove.Name, NodeToMove.Text, NodeToMove.SelectedImageIndex, NodeToMove.ImageIndex, NodeToMove.Tag.ToString(), false, false, false, NodeToMove.ForeColor, ParentTreeViewControl);
                    if (NodeToMove.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in NodeToMove.Nodes)
                        {
                            TreeNodeFunctions.TreeNodeAddNewNode(child.Parent.Name, child.Name, child.Text, child.SelectedImageIndex, child.ImageIndex, child.Tag.ToString(), false, false, false, child.ForeColor, ParentTreeViewControl);
                        }
                    }
                }
            }
        }

        private void Combobox_DataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedTypeData = Combobox_DataType.SelectedItem.ToString();

            if (SelectedTypeData != CurrentLoadedData)
            {
                LoadData(SelectedTypeData);
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadData(string ParentName)
        {
            CurrentLoadedData = ParentName;
            SoundsDictionary.Clear();

            Combobox_AvailableFolders.DataSource = GetAvailableFolders(ParentTreeViewControl, CurrentLoadedData);
            Combobox_AvailableFolders.SelectedItem = DestinationFolder;

            /*Check for sounds*/
            if (Combobox_DataType.SelectedIndex > 0)
            {
                foreach (TreeNode node in ParentTreeViewControl.Nodes)
                {
                    if (node.Name.Equals(CurrentLoadedData))
                    {
                        GetObjectsName(node, "Sound");
                    }
                }
            }
            /*Check for audios*/
            else
            {
                foreach (TreeNode node in ParentTreeViewControl.Nodes)
                {
                    if (node.Name.Equals(CurrentLoadedData))
                    {
                        GetObjectsName(node, "Audio");
                    }
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
            AvailableFolders.TrimExcess();

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

        private void GetObjectsName(TreeNode node, string TagName)
        {
            if (node.Tag.Equals(TagName))
            {
                SoundsDictionary.Add(node.Name, node.Text);
            }
            foreach (TreeNode tn in node.Nodes)
            {
                GetObjectsName(tn, TagName);
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