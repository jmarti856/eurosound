using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_Soundbanks_Main : Form
    {
        //*===============================================================================================
        //* Dictionaries
        //*===============================================================================================
        public Dictionary<string, string> SFX_Defines, SB_Defines;
        public Dictionary<string, double[]> SFX_Data;

        /*Key will be an unique int ID, this way will be faster to access to a sound*/
        Dictionary<int, EXSound> SoundsList = new Dictionary<int, EXSound>();
        /*Key will be the md5 hash to ensure we don't have duplicated sounds*/
        Dictionary<string, EXAudio> AudioDataDict = new Dictionary<string, EXAudio>();

        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        bool MenuStripOpened;
        string LoadedFile = string.Empty;
        string FileToLoadArg, ProjectName, HashcodesSFX, HashcodesSFXData;

        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        Thread UpdateList, UpdateWavList;

        ResourceManager ResourcesManager;

        /*--ProjectFile Classs where we will store all info related with the project*/
        ProjectFile ProjectInfo = new ProjectFile();

        public Frm_Soundbanks_Main(string FilePath, string v_ProjectName, string v_HashcodesSFX, string v_HashcodesSFXData, Dictionary<string, string> d_SFX_Defines, Dictionary<string, string> d_SB_Defines, Dictionary<string, double[]> d_SFX_Data, ResourceManager v_ResourcesManager)
        {
            InitializeComponent();

            FileToLoadArg = FilePath;
            ProjectName = v_ProjectName;
            HashcodesSFX = v_HashcodesSFX;
            HashcodesSFXData = v_HashcodesSFXData;
            SFX_Defines = d_SFX_Defines;
            SFX_Data = d_SFX_Data;
            SB_Defines = d_SB_Defines;
            ResourcesManager = v_ResourcesManager;

            /*Menu Item: File*/
            MenuItem_File.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MenuItem_File.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItem_File_Save.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_File_Save"));
            MenuItem_File_SaveAs.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_File_SaveAs"));
            MenuItem_File_Export.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_File_Export"));

            MenuItem_File_Save.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItem_File_SaveAs.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
            MenuItem_File_Export.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);


            /*Menu Item: Edit*/
            MenuItem_Edit.DropDownOpened += (se, ev) => { GenericFunctions.StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MenuItem_Edit.DropDownClosed += (se, ev) => { GenericFunctions.SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItem_Edit_FileProps.MouseHover += (se, ev) => GenericFunctions.StatusBarTutorialModeShowText(ResourcesManager.GetString("MenuItem_Edit_FileProps"));

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => GenericFunctions.StatusBarTutorialMode(MenuStripOpened);
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_Soundbanks_Main_Load(object sender, System.EventArgs e)
        {
            /*Check Hashcodes are not null*/
            /*Load Hashcodes*/
            if (SFX_Defines.Keys.Count == 0 || SFX_Data.Keys.Count == 0)
            {
                /*Update Status Bar*/
                GenericFunctions.SetProgramStateShowToStatusBar(ResourcesManager.GetString("StatusBar_ReadingESFFile"));

                /*Load Data*/
                Thread LoadHashcodeData = new Thread(() => Hashcodes.LoadSoundDataFile(HashcodesSFXData, SFX_Data, SFX_Defines, ResourcesManager))
                {
                    IsBackground = true
                };
                Thread LoadHashcodes = new Thread(() => Hashcodes.LoadSoundHashcodes(HashcodesSFX, SFX_Defines, SB_Defines, ResourcesManager))
                {
                    IsBackground = true
                };
                LoadHashcodes.Start();
                LoadHashcodes.Join();
                LoadHashcodeData.Start();
            }

            /*Load file in argument 0*/
            if (!string.IsNullOrEmpty(FileToLoadArg))
            {
                LoadedFile = FileToLoadArg;
                SaveAndLoadESF.LoadSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, FileToLoadArg, ProjectInfo, ResourcesManager);
            }
            else
            {
                ProjectInfo.FileName = ProjectName;
            }
        }

        private void Frm_Soundbanks_Main_Shown(object sender, System.EventArgs e)
        {
            /*Expand Sounds node*/
            TreeView_File.Nodes["Sounds"].Expand();

            /*Set Program status*/
            GenericFunctions.SetProgramStateShowToStatusBar(ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_Soundbanks_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProjectInfo.FileHasBeenModified)
            {
                DialogResult dialogResult = MessageBox.Show("Save changes to " + ProjectInfo.FileName + "?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;
                    LoadedFile = EXObjectsFunctions.SaveDocument(LoadedFile, TreeView_File, SoundsList, AudioDataDict, ProjectInfo, SB_Defines);
                    ProjectInfo.FileHasBeenModified = false;
                    e.Cancel = false;
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }

        private void Frm_Soundbanks_Main_Enter(object sender, System.EventArgs e)
        {
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName);
        }

        //*===============================================================================================
        //* Tree View Events
        //*===============================================================================================
        private void TreeView_File_MouseClick(object sender, MouseEventArgs e)
        {
            /*Select node*/
            TreeView_File.SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);

            /*Open context menu depending of the selected node*/
            if (e.Button == MouseButtons.Right)
            {
                if (TreeView_File.SelectedNode.Tag.Equals("Folder") || TreeView_File.SelectedNode.Tag.Equals("Root"))
                {
                    if (TreeView_File.SelectedNode != null)
                    {
                        ContextMenu_Folders.Show(Cursor.Position);
                        if (TreeNodeFunctions.FindRootNode(TreeView_File.SelectedNode).Name.Equals("AudioData"))
                        {
                            ContextMenuFolder_AddSound.Visible = false;
                            ContextMenuFolder_AddAudio.Visible = true;
                        }
                        else
                        {
                            ContextMenuFolder_AddSound.Visible = true;
                            ContextMenuFolder_AddAudio.Visible = false;
                        }
                    }
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Sound"))
                {
                    ContextMenu_Sound.Show(Cursor.Position);
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Sample"))
                {
                    ContextMenu_Sample.Show(Cursor.Position);
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Audio"))
                {
                    ContextMenu_Audio.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*Select node*/
            TreeView_File.SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);

            if (TreeView_File.SelectedNode.Tag.Equals("Sample"))
            {
                OpenSelectedNodeSampleProperties();
            }
            if (TreeView_File.SelectedNode.Tag.Equals("Audio"))
            {
                OpenAudioProperties();
            }
        }

        private void TreeView_File_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            int SoundID;
            /*Check that we have selected a node, and we have not selected the root folder*/
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                /*Check we are not renaming with an empty string*/
                if (string.IsNullOrEmpty(e.Label))
                {
                    /*Cancel edit*/
                    e.CancelEdit = true;
                }
                else
                {
                    /*Check that not exists an item with the same name*/
                    if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, e.Label))
                    {
                        MessageBox.Show(ResourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                    }
                    else
                    {
                        /*Rename Sound item*/
                        if (e.Node.Tag.Equals("Sound"))
                        {
                            SoundID = int.Parse(e.Node.Name);
                            if (SoundsList.ContainsKey(SoundID))
                            {
                                SoundsList[SoundID].DisplayName = e.Label;
                            }
                        }
                        /*Rename sound sample*/
                        else if (e.Node.Tag.Equals("Sample"))
                        {
                            EXSound ParentSound = EXObjectsFunctions.GetSoundByName(int.Parse(e.Node.Parent.Name), SoundsList);
                            for (int i = 0; i < ParentSound.Samples.Count; i++)
                            {
                                if (ParentSound.Samples[i].Name.Equals(e.Node.Name))
                                {
                                    ParentSound.Samples[i].DisplayName = e.Label;
                                    break;
                                }
                            }
                        }
                        else if (e.Node.Tag.Equals("Audio"))
                        {
                            e.Node.Text = e.Label;
                        }
                        else
                        {
                            /*Update tree node props*/
                            e.Node.Text = e.Label;
                        }
                    }
                }
            }
            else
            {
                /*Cancel edit*/
                e.CancelEdit = true;
            }
        }

        private void TreeView_File_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeView_File_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null)
            {
                Point targetPoint = TreeView_File.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = TreeView_File.GetNodeAt(targetPoint);

                if (targetNode != null)
                {
                    //Type of nodes that are allowed to be re-ubicated
                    if (targetNode.Tag.Equals("Folder") || targetNode.Tag.Equals("Sound") || targetNode.Tag.Equals("Audio"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    TreeView_File.SelectedNode = targetNode;
                }
            }
        }

        private void TreeView_File_DragDrop(object sender, DragEventArgs e)
        {
            string DestSection, SourceSection, DestNodeType;

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = TreeView_File.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = TreeView_File.GetNodeAt(targetPoint);
            DestSection = TreeNodeFunctions.FindRootNode(targetNode).Text;
            DestNodeType = targetNode.Tag.ToString();

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            SourceSection = TreeNodeFunctions.FindRootNode(draggedNode).Text;

            // Confirm that the node at the drop location is not 
            // the dragged node and that target node isn't null
            // (for example if you drag outside the control)
            if (!draggedNode.Equals(targetNode) && targetNode != null)
            {
                /*
                Confirm we are not outside the node section and that the destination place is a folder or the root
                node section
                */
                if (SourceSection.Equals(DestSection) && (DestNodeType.Equals("Folder") || DestNodeType.Equals("Root")))
                {
                    // Remove the node from its current 
                    // location and add it to the node at the drop location.
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                    targetNode.Expand();
                    TreeView_File.SelectedNode = draggedNode;
                }
            }
        }

        private void TreeView_File_DragOver(object sender, DragEventArgs e)
        {
            Point p = TreeView_File.PointToClient(new Point(e.X, e.Y));
            TreeNode node = TreeView_File.GetNodeAt(p.X, p.Y);
            TreeView_File.SelectedNode = node;
        }

        private void TreeView_File_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            /*Change node images depending of the type*/
            if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 1, 1);
            }
            else if (e.Node.Tag.Equals("Sound"))
            {
                if (EXObjectsFunctions.SoundWillBeOutputed(SoundsList, e.Node.Name))
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 3, 3);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 6, 6);
                }
            }
            else if (e.Node.Tag.Equals("Sample"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 4, 4);
            }
        }
        //---------------------------------------------Change Nodes Images---------------------------------------------
        private void TreeView_File_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            /*Change node images depending of the type*/
            if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 0, 0);
            }
            else if (e.Node.Tag.Equals("Sound"))
            {
                if (EXObjectsFunctions.SoundWillBeOutputed(SoundsList, e.Node.Name))
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 2, 2);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 5, 5);
                }
            }
            else if (e.Node.Tag.Equals("Sample"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 4, 4);
            }
        }

        //*===============================================================================================
        //* HOT KEYS
        //*===============================================================================================
        private void TreeView_File_KeyDown(object sender, KeyEventArgs e)
        {
            /*Rename selected Node*/
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode, ResourcesManager);
                ProjectInfo.FileHasBeenModified = true;
            }
            /*Delete selected Node*/
            if (e.KeyCode == Keys.Delete)
            {
                if (TreeView_File.SelectedNode.Tag.Equals("Sound"))
                {
                    RemoveSoundSelectedNode();
                    ProjectInfo.FileHasBeenModified = true;
                }
                else if (TreeView_File.SelectedNode.Equals("Sample"))
                {
                    RemoveSampleSelectedNode();
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    RemoveFolderSelectedNode();
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*===============================================================================================
        private void MenuItem_File_Save_Click(object sender, System.EventArgs e)
        {
            LoadedFile = EXObjectsFunctions.SaveDocument(LoadedFile, TreeView_File, SoundsList, AudioDataDict, ProjectInfo, SB_Defines);
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItem_File_SaveAs_Click(object sender, System.EventArgs e)
        {
            LoadedFile = EXObjectsFunctions.OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectInfo, SB_Defines);
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItemFile_Export_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProjectInfo.Hashcode))
            {
                string FileName = "HC" + ProjectInfo.Hashcode.Substring(4);
                string SavePath = GenericFunctions.SaveFileBrowser("SFX Files (*.SFX)|*.SFX", 1, true, FileName);
                if (!string.IsNullOrEmpty(SavePath))
                {
                    if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                    {
                        EXBuildSFX.ExportContentToSFX(EXObjectsFunctions.GetFinalListToExport(SoundsList), AudioDataDict, SavePath, ProjectInfo);
                    }
                }
            }
            else
            {
                MessageBox.Show(ResourcesManager.GetString("Error_BuildSFX_NoHashcode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemFile_ReadYml_Click(object sender, System.EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("YML Files|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                /*--Ask user for a fully reimport--*/
                DialogResult ReimportQuestion = MessageBox.Show(ResourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ReimportQuestion == DialogResult.Yes)
                {
                    //Clear Data
                    ProjectInfo.ClearAllStoredData(SoundsList, AudioDataDict, TreeView_File);
                }


                /*--Load New data--*/
                Thread LoadYamlFile = new Thread(() => YamlReader.LoadDataFromSwyterUnpacker(SoundsList, AudioDataDict, TreeView_File, FilePath, ProjectInfo, SFX_Defines, SB_Defines, ResourcesManager))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void Button_Search_Click(object sender, System.EventArgs e)
        {
            var searchFor = Textbox_SearchHint.Text.Trim().ToUpper();
            if (searchFor != "")
            {
                if (TreeView_File.Nodes.Count > 0)
                {
                    if (TreeNodeFunctions.SearchRecursive(TreeView_File.Nodes, searchFor, TreeView_File, RadioButton_MatchText.Checked))
                    {
                        TreeView_File.SelectedNode.Expand();
                        TreeView_File.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No results found", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void Frm_Soundbanks_Main_Leave(object sender, System.EventArgs e)
        {

        }

        private void MenuItemFile_ReadSound_Click(object sender, System.EventArgs e)
        {
            string SoundName, SoundHashcode;

            string FilePath = GenericFunctions.OpenFileBrowser("YML Files|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                SoundName = new DirectoryInfo(Path.GetDirectoryName(FilePath)).Name;
                SoundHashcode = Hashcodes.GetHashcodeByLabel(SFX_Defines, SoundName);
                YamlReader.ReadYamlFile(SoundsList, AudioDataDict, TreeView_File, FilePath, SoundName, SoundHashcode, true, ProjectInfo, ResourcesManager);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        //*===============================================================================================
        //* MAIN MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, System.EventArgs e)
        {
            Frm_FileProperties Props = new Frm_FileProperties(ProjectInfo, HashcodesSFX, HashcodesSFXData, SB_Defines, SFX_Defines, ResourcesManager)
            {
                Owner = this,
                ShowInTaskbar = false,
                Tag = this.Tag
            };
            Props.ShowDialog();
        }

        //*===============================================================================================
        //* WAV HEADER DATA
        //*===============================================================================================
        private void Button_UpdateList_WavData_Click(object sender, System.EventArgs e)
        {
            /*Load Audio properties*/
            UpdateWavDataList();
        }

        private void Button_UpdateList_Hashcodes_Click(object sender, System.EventArgs e)
        {
            /*Load New data*/
            UpdateHashcodesValidList();
        }
    }
}