using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSoundsEditorMain : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        public Dictionary<uint, EXSoundStream> StreamSoundsList = new Dictionary<uint, EXSoundStream>();
        public ProjectFile ProjectInfo = new ProjectFile();
        private string FileToLoadArg, ProjectName;


        public Frm_StreamSoundsEditorMain(string FilePath, string Name)
        {
            InitializeComponent();

            FileToLoadArg = FilePath;
            ProjectName = Name;
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSoundsEditorMain_Load(object sender, System.EventArgs e)
        {
            /*Check Hashcodes are not null*/
            /*Load Hashcodes*/
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Keys.Count == 0)
            {
                /*Update Status Bar*/
                GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

                /*Load Data*/
                Thread LoadHashcodeData = new Thread(() => Hashcodes.LoadSoundDataFile())
                {
                    IsBackground = true
                };
                Thread LoadHashcodes = new Thread(() => Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath))
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


            }
            else
            {
                ProjectInfo.FileName = ProjectName;
            }

            /*Apply User Preferences*/
            FontConverter cvt = new FontConverter();
            TreeView_StreamData.Indent = GlobalPreferences.TreeViewIndent;
            TreeView_StreamData.Font = cvt.ConvertFromString(GlobalPreferences.SelectedFont) as Font;
            TreeView_StreamData.ShowLines = GlobalPreferences.ShowLines;
            TreeView_StreamData.ShowRootLines = GlobalPreferences.ShowRootLines;

            /*Update Status Bar*/
            GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_StreamSoundsEditorMain_Shown(object sender, System.EventArgs e)
        {
            /*Expand Sounds node*/
            TreeView_StreamData.Nodes["Sounds"].Expand();

            /*Set Program status*/
            GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* Tree View Controls
        //*===============================================================================================
        private void TreeView_StreamData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            uint SoundID;

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
                    if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, e.Label))
                    {
                        MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                    }
                    else
                    {
                        /*Rename Sound item*/
                        if (e.Node.Tag.Equals("Sound"))
                        {
                            SoundID = uint.Parse(e.Node.Name);
                            if (StreamSoundsList.ContainsKey(SoundID))
                            {
                                StreamSoundsList[SoundID].DisplayName = e.Label;
                            }
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

        private void TreeView_StreamData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void TreeView_StreamData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void TreeView_StreamData_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void TreeView_StreamData_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void TreeView_StreamData_DragOver(object sender, DragEventArgs e)
        {

        }

        private void TreeView_StreamData_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void TreeView_StreamData_KeyDown(object sender, KeyEventArgs e)
        {
            /*Rename selected Node*/
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, TreeView_StreamData.SelectedNode);
                ProjectInfo.FileHasBeenModified = true;
            }
            /*Delete selected Node*/
            if (e.KeyCode == Keys.Delete)
            {
                if (TreeView_StreamData.SelectedNode.Tag.Equals("Sound"))
                {
                    RemoveStreamSoundSelectedNode();
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void TreeView_StreamData_MouseClick(object sender, MouseEventArgs e)
        {
            /*Select node*/
            TreeView_StreamData.SelectedNode = TreeView_StreamData.GetNodeAt(e.X, e.Y);

            /*Open context menu depending of the selected node*/
            if (e.Button == MouseButtons.Right)
            {
                if (TreeView_StreamData.SelectedNode.Tag.Equals("Folder") || TreeView_StreamData.SelectedNode.Tag.Equals("Root"))
                {
                    ContextMenu_Folders.Show(Cursor.Position);
                }
                else if (TreeView_StreamData.SelectedNode.Tag.Equals("Sound"))
                {
                    ContextMenu_Sounds.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_StreamData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TreeView_StreamData.SelectedNode.Tag.Equals("Sound"))
            {
                OpenSoundPropertiesForm();
            }
        }
    }
}
