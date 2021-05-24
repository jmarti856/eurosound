using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private ProjectFile fileProperties;
        private string CurrentLoadedData, DestinationFolder;

        public EuroSound_NodesToFolder(TreeView TreeViewControl, ProjectFile FileProperties, string Section, string Destination)
        {
            InitializeComponent();
            ParentTreeViewControl = TreeViewControl;
            CurrentLoadedData = Section;
            DestinationFolder = Destination;
            fileProperties = FileProperties;
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
            string selectedParent = Combobox_AvailableFolders.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(selectedParent))
            {
                TreeNode NewParentNode = ParentTreeViewControl.Nodes.Find(selectedParent, true)[0];

                /*Recursively move nodes to the new folder*/
                foreach (KeyValuePair<string, string> item in ListBox_Items.SelectedItems)
                {
                    TreeNode NodeToMove = ParentTreeViewControl.Nodes.Find(item.Key.ToString(), true)[0];
                    ParentTreeViewControl.Nodes.Remove(NodeToMove);
                    TreeNodeFunctions.TreeNodeAddNewNode(NewParentNode.Name, NodeToMove.Name, NodeToMove.Text, NodeToMove.SelectedImageIndex, NodeToMove.ImageIndex, (byte)NodeToMove.Tag, false, false, false, NodeToMove.ForeColor, ParentTreeViewControl);
                    if (NodeToMove.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in NodeToMove.Nodes)
                        {
                            TreeNodeFunctions.TreeNodeAddNewNode(child.Parent.Name, child.Name, child.Text, child.SelectedImageIndex, child.ImageIndex, (byte)child.Tag, false, false, false, child.ForeColor, ParentTreeViewControl);
                        }
                    }
                }

                //Update project status variable
                fileProperties.FileHasBeenModified = true;
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

            Combobox_AvailableFolders.DataSource = GetAvailableFolders(ParentTreeViewControl, CurrentLoadedData).ToList();
            Combobox_AvailableFolders.ValueMember = "Key";
            Combobox_AvailableFolders.DisplayMember = "Value";
            Combobox_AvailableFolders.SelectedValue = DestinationFolder;

            /*Check for sounds*/
            if (Combobox_DataType.SelectedIndex > 0)
            {
                foreach (TreeNode node in ParentTreeViewControl.Nodes)
                {
                    if (node.Name.Equals(CurrentLoadedData))
                    {
                        GetObjectsName(node, (byte)Enumerations.TreeNodeType.Sound);
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
                        GetObjectsName(node, (byte)Enumerations.TreeNodeType.Audio);
                    }
                }
            }

            //Show Sound Nodes
            if (SoundsDictionary.Count > 0)
            {
                ShowDataInList();
            }
        }

        private Dictionary<string, string> GetAvailableFolders(TreeView control, string ParentName)
        {
            Dictionary<string, string> AvailableFolders = new Dictionary<string, string>();
            foreach (TreeNode node in control.Nodes)
            {
                if (node.Name.Equals(ParentName))
                {
                    GetFolders(node, AvailableFolders);
                }
            }

            return AvailableFolders;
        }

        private void GetFolders(TreeNode node, Dictionary<string, string> foldersList)
        {
            if (Convert.ToByte(node.Tag) == (byte)Enumerations.TreeNodeType.Folder)
            {
                foldersList.Add(node.Name, node.Text);
            }
            foreach (TreeNode tn in node.Nodes)
            {
                GetFolders(tn, foldersList);
            }
        }

        private void GetObjectsName(TreeNode nodeToCheck, byte tagToCheck)
        {
            if ((byte)nodeToCheck.Tag == tagToCheck)
            {
                SoundsDictionary.Add(nodeToCheck.Name, nodeToCheck.Text);
            }
            foreach (TreeNode tn in nodeToCheck.Nodes)
            {
                GetObjectsName(tn, tagToCheck);
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