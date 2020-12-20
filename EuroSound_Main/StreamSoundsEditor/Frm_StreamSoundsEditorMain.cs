using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSoundsEditorMain : Form
    {
        public ProjectFile ProjectInfo = new ProjectFile();

        public Frm_StreamSoundsEditorMain(string FilePath, string Name)
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* ContextMenu
        //*===============================================================================================
        private void ContextMenuFolder_CollapseAll_Click(object sender, System.EventArgs e)
        {
            TreeView_StreamData.SelectedNode.Collapse();
        }

        private void ContextMenuFolder_ExpandAll_Click(object sender, System.EventArgs e)
        {
            TreeView_StreamData.SelectedNode.Expand();
        }

        private void ContextMenuMain_TextColor_Click(object sender, System.EventArgs e)
        {
            TreeView_StreamData.SelectedNode.ForeColor = GenericFunctions.GetColorFromColorPicker(); ;
            ProjectInfo.FileHasBeenModified = true;
        }

        private void ContextMenuMain_AddSound_Click(object sender, System.EventArgs e)
        {
            string Name = GenericFunctions.OpenInputBox("Enter a name for new a new streaming sound.", "New Streaming Sound");
            if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, Name))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Adding_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
            }
        }

        private void TreeView_StreamData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {

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

        }

        private void TreeView_StreamData_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void TreeView_StreamData_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
