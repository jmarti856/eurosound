namespace EuroSound_Application
{
    partial class Frm_Soundbanks_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Audio Data", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Sounds", 0, 0);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Streamed Sounds", 0, 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Soundbanks_Main));
            this.TreeView_File = new System.Windows.Forms.TreeView();
            this.ImageList_TreeNode = new System.Windows.Forms.ImageList(this.components);
            this.ContextMenu_Folders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuFolders_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolders_New = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_CollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_Sort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_Move = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_AddSound = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_AddAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_Purge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Sound = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuSound_AddSample = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSound_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSound_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSound_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSound_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_File_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_ReadYml = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ReadSound = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_FileProps = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Sample = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuSample_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSample_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSample_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSample_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.Button_UpdateList_WavData = new System.Windows.Forms.Button();
            this.GroupBox_Hashcodes = new System.Windows.Forms.GroupBox();
            this.Button_UpdateList_Hashcodes = new System.Windows.Forms.Button();
            this.ListView_Hashcodes = new System.Windows.Forms.ListView();
            this.Col_Hashcode_OK = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Hashcode_Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_UsedIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList_ListView = new System.Windows.Forms.ImageList(this.components);
            this.TabControlDataViewer = new System.Windows.Forms.TabControl();
            this.TabPage_WavHeaderData = new System.Windows.Forms.TabPage();
            this.ListView_WavHeaderData = new ListViewExtendedMethods.ListView_ColumnSortingClick();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_LoopOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Channels = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Bits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Encoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabPage_StreamData = new System.Windows.Forms.TabPage();
            this.Button_UpdateList_StreamData = new System.Windows.Forms.Button();
            this.ListView_StreamData = new ListViewExtendedMethods.ListView_ColumnSortingClick();
            this.Col_StreamName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_FileRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Sound = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_StoredIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_Audio = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuAudio_Usage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuAudio_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuAudio_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuAudio_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuAudio_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioButton_WholeWord = new System.Windows.Forms.RadioButton();
            this.RadioButton_MatchText = new System.Windows.Forms.RadioButton();
            this.Button_Search = new System.Windows.Forms.Button();
            this.Textbox_SearchHint = new System.Windows.Forms.TextBox();
            this.Label_NameItemToSearch = new System.Windows.Forms.Label();
            this.Button_GenerateList = new System.Windows.Forms.Button();
            this.ContextMenu_Folders.SuspendLayout();
            this.ContextMenu_Sound.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.ContextMenu_Sample.SuspendLayout();
            this.GroupBox_Hashcodes.SuspendLayout();
            this.TabControlDataViewer.SuspendLayout();
            this.TabPage_WavHeaderData.SuspendLayout();
            this.TabPage_StreamData.SuspendLayout();
            this.ContextMenu_Audio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView_File
            // 
            this.TreeView_File.AllowDrop = true;
            this.TreeView_File.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_File.ImageIndex = 0;
            this.TreeView_File.ImageList = this.ImageList_TreeNode;
            this.TreeView_File.LabelEdit = true;
            this.TreeView_File.Location = new System.Drawing.Point(0, 0);
            this.TreeView_File.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TreeView_File.Name = "TreeView_File";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "AudioData";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Tag = "Root";
            treeNode1.Text = "Audio Data";
            treeNode2.ImageIndex = 0;
            treeNode2.Name = "Sounds";
            treeNode2.SelectedImageIndex = 0;
            treeNode2.Tag = "Root";
            treeNode2.Text = "Sounds";
            treeNode3.ImageIndex = 0;
            treeNode3.Name = "StreamedSounds";
            treeNode3.SelectedImageIndex = 0;
            treeNode3.Tag = "Root";
            treeNode3.Text = "Streamed Sounds";
            this.TreeView_File.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.TreeView_File.SelectedImageIndex = 0;
            this.TreeView_File.Size = new System.Drawing.Size(494, 740);
            this.TreeView_File.TabIndex = 1;
            this.TreeView_File.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_File_AfterLabelEdit);
            this.TreeView_File.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_File_BeforeCollapse);
            this.TreeView_File.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_File_BeforeExpand);
            this.TreeView_File.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_File_ItemDrag);
            this.TreeView_File.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_File_DragDrop);
            this.TreeView_File.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_File_DragEnter);
            this.TreeView_File.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView_File_DragOver);
            this.TreeView_File.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_File_KeyDown);
            this.TreeView_File.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_File_MouseClick);
            this.TreeView_File.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_File_MouseDoubleClick);
            // 
            // ImageList_TreeNode
            // 
            this.ImageList_TreeNode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_TreeNode.ImageStream")));
            this.ImageList_TreeNode.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_TreeNode.Images.SetKeyName(0, "directory_closed-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(1, "directory_open_cool-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(2, "cd_audio_cd-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(3, "cd_audio_cd-2.png");
            this.ImageList_TreeNode.Images.SetKeyName(4, "audio_compression-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(5, "cd_audio_cd-1 - no_output.png");
            this.ImageList_TreeNode.Images.SetKeyName(6, "cd_audio_cd-2 - no_output.png");
            this.ImageList_TreeNode.Images.SetKeyName(7, "amplify.png");
            // 
            // ContextMenu_Folders
            // 
            this.ContextMenu_Folders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuFolders_Folder,
            this.toolStripSeparator7,
            this.ContextMenuFolder_AddSound,
            this.ContextMenuFolder_Rename,
            this.ContextMenuFolder_AddAudio,
            this.toolStripSeparator9,
            this.ContextMenuFolder_Purge,
            this.toolStripSeparator5,
            this.ContextMenuFolder_TextColor});
            this.ContextMenu_Folders.Name = "contextMenuStrip1";
            this.ContextMenu_Folders.Size = new System.Drawing.Size(181, 176);
            // 
            // ContextMenuFolders_Folder
            // 
            this.ContextMenuFolders_Folder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuFolders_New,
            this.toolStripSeparator13,
            this.ContextMenuFolder_ExpandAll,
            this.ContextMenuFolder_CollapseAll,
            this.toolStripSeparator8,
            this.ContextMenuFolder_Delete,
            this.toolStripSeparator11,
            this.ContextMenuFolder_Sort,
            this.toolStripSeparator12,
            this.ContextMenuFolder_Move});
            this.ContextMenuFolders_Folder.Name = "ContextMenuFolders_Folder";
            this.ContextMenuFolders_Folder.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuFolders_Folder.Text = "Folder";
            // 
            // ContextMenuFolders_New
            // 
            this.ContextMenuFolders_New.Name = "ContextMenuFolders_New";
            this.ContextMenuFolders_New.Size = new System.Drawing.Size(183, 22);
            this.ContextMenuFolders_New.Text = "New";
            this.ContextMenuFolders_New.Click += new System.EventHandler(this.ContextMenu_Folders_New_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(180, 6);
            // 
            // ContextMenuFolder_ExpandAll
            // 
            this.ContextMenuFolder_ExpandAll.Name = "ContextMenuFolder_ExpandAll";
            this.ContextMenuFolder_ExpandAll.Size = new System.Drawing.Size(183, 22);
            this.ContextMenuFolder_ExpandAll.Text = "Expand All";
            this.ContextMenuFolder_ExpandAll.Click += new System.EventHandler(this.ContextMenu_Folders_Expand_Click);
            // 
            // ContextMenuFolder_CollapseAll
            // 
            this.ContextMenuFolder_CollapseAll.Name = "ContextMenuFolder_CollapseAll";
            this.ContextMenuFolder_CollapseAll.Size = new System.Drawing.Size(183, 22);
            this.ContextMenuFolder_CollapseAll.Text = "Collapse All";
            this.ContextMenuFolder_CollapseAll.Click += new System.EventHandler(this.MenuItem_Folder_Collapse_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(180, 6);
            // 
            // ContextMenuFolder_Delete
            // 
            this.ContextMenuFolder_Delete.Name = "ContextMenuFolder_Delete";
            this.ContextMenuFolder_Delete.Size = new System.Drawing.Size(183, 22);
            this.ContextMenuFolder_Delete.Text = "Delete";
            this.ContextMenuFolder_Delete.Click += new System.EventHandler(this.ContextMenu_Folders_Delete_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(180, 6);
            // 
            // ContextMenuFolder_Sort
            // 
            this.ContextMenuFolder_Sort.Name = "ContextMenuFolder_Sort";
            this.ContextMenuFolder_Sort.Size = new System.Drawing.Size(183, 22);
            this.ContextMenuFolder_Sort.Text = "Sort Child Items";
            this.ContextMenuFolder_Sort.Click += new System.EventHandler(this.ContextMenu_Folders_Sort_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(180, 6);
            // 
            // ContextMenuFolder_Move
            // 
            this.ContextMenuFolder_Move.Name = "ContextMenuFolder_Move";
            this.ContextMenuFolder_Move.Size = new System.Drawing.Size(183, 22);
            this.ContextMenuFolder_Move.Text = "Move Multiple Items";
            this.ContextMenuFolder_Move.Click += new System.EventHandler(this.ContextMenu_Folders_Move_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(177, 6);
            // 
            // ContextMenuFolder_AddSound
            // 
            this.ContextMenuFolder_AddSound.Name = "ContextMenuFolder_AddSound";
            this.ContextMenuFolder_AddSound.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuFolder_AddSound.Text = "Add Sound";
            this.ContextMenuFolder_AddSound.Click += new System.EventHandler(this.ContextMenu_Folders_AddSound_Click);
            // 
            // ContextMenuFolder_Rename
            // 
            this.ContextMenuFolder_Rename.Name = "ContextMenuFolder_Rename";
            this.ContextMenuFolder_Rename.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuFolder_Rename.Text = "Rename";
            this.ContextMenuFolder_Rename.Click += new System.EventHandler(this.ContextMenuFolder_Rename_Click);
            // 
            // ContextMenuFolder_AddAudio
            // 
            this.ContextMenuFolder_AddAudio.Name = "ContextMenuFolder_AddAudio";
            this.ContextMenuFolder_AddAudio.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuFolder_AddAudio.Text = "Add Audio";
            this.ContextMenuFolder_AddAudio.Click += new System.EventHandler(this.ContextMenu_Folders_AddAudio_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(177, 6);
            // 
            // ContextMenuFolder_Purge
            // 
            this.ContextMenuFolder_Purge.Name = "ContextMenuFolder_Purge";
            this.ContextMenuFolder_Purge.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuFolder_Purge.Text = "Purge Audio Data";
            this.ContextMenuFolder_Purge.Click += new System.EventHandler(this.ContextMenuFolder_Purge_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // ContextMenuFolder_TextColor
            // 
            this.ContextMenuFolder_TextColor.Name = "ContextMenuFolder_TextColor";
            this.ContextMenuFolder_TextColor.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuFolder_TextColor.Text = "Text Color...";
            this.ContextMenuFolder_TextColor.Click += new System.EventHandler(this.ContextMenuFolders_TextColor_Click);
            // 
            // ContextMenu_Sound
            // 
            this.ContextMenu_Sound.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuSound_AddSample,
            this.ContextMenuSound_Remove,
            this.ContextMenuSound_Rename,
            this.ContextMenuSound_Properties,
            this.toolStripSeparator6,
            this.ContextMenuSound_TextColor});
            this.ContextMenu_Sound.Name = "ContextMenu_Sound";
            this.ContextMenu_Sound.Size = new System.Drawing.Size(145, 120);
            this.ContextMenu_Sound.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Sound_Opening);
            // 
            // ContextMenuSound_AddSample
            // 
            this.ContextMenuSound_AddSample.Name = "ContextMenuSound_AddSample";
            this.ContextMenuSound_AddSample.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuSound_AddSample.Text = "Add Sample";
            this.ContextMenuSound_AddSample.Click += new System.EventHandler(this.ContextMenu_Folders_AddSample_Click);
            // 
            // ContextMenuSound_Remove
            // 
            this.ContextMenuSound_Remove.Name = "ContextMenuSound_Remove";
            this.ContextMenuSound_Remove.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuSound_Remove.Text = "Delete Sound";
            this.ContextMenuSound_Remove.Click += new System.EventHandler(this.ContextMenu_Sound_Remove_Click);
            // 
            // ContextMenuSound_Rename
            // 
            this.ContextMenuSound_Rename.Name = "ContextMenuSound_Rename";
            this.ContextMenuSound_Rename.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuSound_Rename.Text = "Rename";
            this.ContextMenuSound_Rename.Click += new System.EventHandler(this.ContextMenu_Sound_Rename_Click);
            // 
            // ContextMenuSound_Properties
            // 
            this.ContextMenuSound_Properties.Name = "ContextMenuSound_Properties";
            this.ContextMenuSound_Properties.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuSound_Properties.Text = "Properties...";
            this.ContextMenuSound_Properties.Click += new System.EventHandler(this.ContextMenu_Sound_Properties_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(141, 6);
            // 
            // ContextMenuSound_TextColor
            // 
            this.ContextMenuSound_TextColor.Name = "ContextMenuSound_TextColor";
            this.ContextMenuSound_TextColor.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuSound_TextColor.Text = "Text Color...";
            this.ContextMenuSound_TextColor.Click += new System.EventHandler(this.ContextMenu_Sound_TextColor_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File,
            this.MenuItem_Edit});
            this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1191, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            this.MainMenu.Visible = false;
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File_Save,
            this.MenuItem_File_SaveAs,
            this.toolStripSeparator2,
            this.MenuItem_File_Export,
            this.toolStripSeparator1,
            this.MenuItemFile_ReadYml,
            this.MenuItemFile_ReadSound});
            this.MenuItem_File.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.MenuItem_File.MergeIndex = 0;
            this.MenuItem_File.Name = "MenuItem_File";
            this.MenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.MenuItem_File.Text = "File";
            // 
            // MenuItem_File_Save
            // 
            this.MenuItem_File_Save.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Save.MergeIndex = 2;
            this.MenuItem_File_Save.Name = "MenuItem_File_Save";
            this.MenuItem_File_Save.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_Save.Text = "Save";
            this.MenuItem_File_Save.Click += new System.EventHandler(this.MenuItem_File_Save_Click);
            // 
            // MenuItem_File_SaveAs
            // 
            this.MenuItem_File_SaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_SaveAs.MergeIndex = 3;
            this.MenuItem_File_SaveAs.Name = "MenuItem_File_SaveAs";
            this.MenuItem_File_SaveAs.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_SaveAs.Text = "Save As...";
            this.MenuItem_File_SaveAs.Click += new System.EventHandler(this.MenuItem_File_SaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator2.MergeIndex = 4;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // MenuItem_File_Export
            // 
            this.MenuItem_File_Export.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Export.MergeIndex = 5;
            this.MenuItem_File_Export.Name = "MenuItem_File_Export";
            this.MenuItem_File_Export.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_Export.Text = "Export";
            this.MenuItem_File_Export.Click += new System.EventHandler(this.MenuItemFile_Export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 6;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // MenuItemFile_ReadYml
            // 
            this.MenuItemFile_ReadYml.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_ReadYml.MergeIndex = 7;
            this.MenuItemFile_ReadYml.Name = "MenuItemFile_ReadYml";
            this.MenuItemFile_ReadYml.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_ReadYml.Text = "Import Sounds List (.yml)";
            this.MenuItemFile_ReadYml.Click += new System.EventHandler(this.MenuItemFile_ReadYml_Click);
            // 
            // MenuItemFile_ReadSound
            // 
            this.MenuItemFile_ReadSound.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_ReadSound.MergeIndex = 8;
            this.MenuItemFile_ReadSound.Name = "MenuItemFile_ReadSound";
            this.MenuItemFile_ReadSound.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_ReadSound.Text = "Read Sound (.yml)";
            this.MenuItemFile_ReadSound.Click += new System.EventHandler(this.MenuItemFile_ReadSound_Click);
            // 
            // MenuItem_Edit
            // 
            this.MenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Edit_FileProps});
            this.MenuItem_Edit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_Edit.MergeIndex = 1;
            this.MenuItem_Edit.Name = "MenuItem_Edit";
            this.MenuItem_Edit.Size = new System.Drawing.Size(39, 20);
            this.MenuItem_Edit.Text = "Edit";
            // 
            // MenuItem_Edit_FileProps
            // 
            this.MenuItem_Edit_FileProps.Name = "MenuItem_Edit_FileProps";
            this.MenuItem_Edit_FileProps.Size = new System.Drawing.Size(148, 22);
            this.MenuItem_Edit_FileProps.Text = "File Properties";
            this.MenuItem_Edit_FileProps.Click += new System.EventHandler(this.MenuItem_Edit_FileProps_Click);
            // 
            // ContextMenu_Sample
            // 
            this.ContextMenu_Sample.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuSample_Remove,
            this.ContextMenuSample_Rename,
            this.ContextMenuSample_Properties,
            this.toolStripSeparator4,
            this.ContextMenuSample_TextColor});
            this.ContextMenu_Sample.Name = "ContextMenu_Sample";
            this.ContextMenu_Sample.Size = new System.Drawing.Size(150, 98);
            // 
            // ContextMenuSample_Remove
            // 
            this.ContextMenuSample_Remove.Name = "ContextMenuSample_Remove";
            this.ContextMenuSample_Remove.Size = new System.Drawing.Size(149, 22);
            this.ContextMenuSample_Remove.Text = "Delete Sample";
            this.ContextMenuSample_Remove.Click += new System.EventHandler(this.ContextMenu_Sample_Remove_Click);
            // 
            // ContextMenuSample_Rename
            // 
            this.ContextMenuSample_Rename.Name = "ContextMenuSample_Rename";
            this.ContextMenuSample_Rename.Size = new System.Drawing.Size(149, 22);
            this.ContextMenuSample_Rename.Text = "Rename";
            this.ContextMenuSample_Rename.Click += new System.EventHandler(this.ContextMenu_Sample_Rename_Click);
            // 
            // ContextMenuSample_Properties
            // 
            this.ContextMenuSample_Properties.Name = "ContextMenuSample_Properties";
            this.ContextMenuSample_Properties.Size = new System.Drawing.Size(149, 22);
            this.ContextMenuSample_Properties.Text = "Properties...";
            this.ContextMenuSample_Properties.Click += new System.EventHandler(this.ContextMenu_Sample_Properties_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(146, 6);
            // 
            // ContextMenuSample_TextColor
            // 
            this.ContextMenuSample_TextColor.Name = "ContextMenuSample_TextColor";
            this.ContextMenuSample_TextColor.Size = new System.Drawing.Size(149, 22);
            this.ContextMenuSample_TextColor.Text = "Text Color...";
            this.ContextMenuSample_TextColor.Click += new System.EventHandler(this.ContextMenu_Sample_TextColor_Click);
            // 
            // Button_UpdateList_WavData
            // 
            this.Button_UpdateList_WavData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_WavData.Location = new System.Drawing.Point(380, 225);
            this.Button_UpdateList_WavData.Name = "Button_UpdateList_WavData";
            this.Button_UpdateList_WavData.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_WavData.TabIndex = 1;
            this.Button_UpdateList_WavData.Text = "Update";
            this.Button_UpdateList_WavData.UseVisualStyleBackColor = true;
            this.Button_UpdateList_WavData.Click += new System.EventHandler(this.Button_UpdateList_WavData_Click);
            // 
            // GroupBox_Hashcodes
            // 
            this.GroupBox_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Hashcodes.Controls.Add(this.Button_UpdateList_Hashcodes);
            this.GroupBox_Hashcodes.Controls.Add(this.ListView_Hashcodes);
            this.GroupBox_Hashcodes.Location = new System.Drawing.Point(500, 381);
            this.GroupBox_Hashcodes.Name = "GroupBox_Hashcodes";
            this.GroupBox_Hashcodes.Size = new System.Drawing.Size(469, 318);
            this.GroupBox_Hashcodes.TabIndex = 3;
            this.GroupBox_Hashcodes.TabStop = false;
            this.GroupBox_Hashcodes.Text = "Hashcodes";
            // 
            // Button_UpdateList_Hashcodes
            // 
            this.Button_UpdateList_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_Hashcodes.Location = new System.Drawing.Point(388, 289);
            this.Button_UpdateList_Hashcodes.Name = "Button_UpdateList_Hashcodes";
            this.Button_UpdateList_Hashcodes.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_Hashcodes.TabIndex = 1;
            this.Button_UpdateList_Hashcodes.Text = "Update";
            this.Button_UpdateList_Hashcodes.UseVisualStyleBackColor = true;
            this.Button_UpdateList_Hashcodes.Click += new System.EventHandler(this.Button_UpdateList_Hashcodes_Click);
            // 
            // ListView_Hashcodes
            // 
            this.ListView_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Hashcodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Hashcode_OK,
            this.Col_Hashcode,
            this.Col_Hashcode_Label,
            this.Col_UsedIn});
            this.ListView_Hashcodes.FullRowSelect = true;
            this.ListView_Hashcodes.GridLines = true;
            this.ListView_Hashcodes.HideSelection = false;
            this.ListView_Hashcodes.Location = new System.Drawing.Point(6, 19);
            this.ListView_Hashcodes.Name = "ListView_Hashcodes";
            this.ListView_Hashcodes.Size = new System.Drawing.Size(457, 264);
            this.ListView_Hashcodes.SmallImageList = this.ImageList_ListView;
            this.ListView_Hashcodes.TabIndex = 0;
            this.ListView_Hashcodes.UseCompatibleStateImageBehavior = false;
            this.ListView_Hashcodes.View = System.Windows.Forms.View.Details;
            // 
            // Col_Hashcode_OK
            // 
            this.Col_Hashcode_OK.Text = "Status";
            this.Col_Hashcode_OK.Width = 65;
            // 
            // Col_Hashcode
            // 
            this.Col_Hashcode.Text = "Hashcode";
            this.Col_Hashcode.Width = 79;
            // 
            // Col_Hashcode_Label
            // 
            this.Col_Hashcode_Label.Text = "Label";
            this.Col_Hashcode_Label.Width = 167;
            // 
            // Col_UsedIn
            // 
            this.Col_UsedIn.Text = "Used By";
            this.Col_UsedIn.Width = 160;
            // 
            // ImageList_ListView
            // 
            this.ImageList_ListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_ListView.ImageStream")));
            this.ImageList_ListView.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_ListView.Images.SetKeyName(0, "Message_Error.png");
            this.ImageList_ListView.Images.SetKeyName(1, "Message_Warning.png");
            this.ImageList_ListView.Images.SetKeyName(2, "Message_Info.png");
            // 
            // TabControlDataViewer
            // 
            this.TabControlDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControlDataViewer.Controls.Add(this.TabPage_WavHeaderData);
            this.TabControlDataViewer.Controls.Add(this.TabPage_StreamData);
            this.TabControlDataViewer.Location = new System.Drawing.Point(500, 95);
            this.TabControlDataViewer.Name = "TabControlDataViewer";
            this.TabControlDataViewer.SelectedIndex = 0;
            this.TabControlDataViewer.Size = new System.Drawing.Size(469, 280);
            this.TabControlDataViewer.TabIndex = 2;
            // 
            // TabPage_WavHeaderData
            // 
            this.TabPage_WavHeaderData.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_WavHeaderData.Controls.Add(this.ListView_WavHeaderData);
            this.TabPage_WavHeaderData.Controls.Add(this.Button_UpdateList_WavData);
            this.TabPage_WavHeaderData.Location = new System.Drawing.Point(4, 22);
            this.TabPage_WavHeaderData.Name = "TabPage_WavHeaderData";
            this.TabPage_WavHeaderData.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_WavHeaderData.Size = new System.Drawing.Size(461, 254);
            this.TabPage_WavHeaderData.TabIndex = 1;
            this.TabPage_WavHeaderData.Text = "Wav Header Data";
            // 
            // ListView_WavHeaderData
            // 
            this.ListView_WavHeaderData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_WavHeaderData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_LoopOffset,
            this.Col_Frequency,
            this.Col_Channels,
            this.Col_Bits,
            this.Col_Data,
            this.Col_Encoding,
            this.Col_Duration});
            this.ListView_WavHeaderData.FullRowSelect = true;
            this.ListView_WavHeaderData.GridLines = true;
            this.ListView_WavHeaderData.HideSelection = false;
            this.ListView_WavHeaderData.Location = new System.Drawing.Point(6, 6);
            this.ListView_WavHeaderData.Name = "ListView_WavHeaderData";
            this.ListView_WavHeaderData.Size = new System.Drawing.Size(449, 213);
            this.ListView_WavHeaderData.TabIndex = 2;
            this.ListView_WavHeaderData.UseCompatibleStateImageBehavior = false;
            this.ListView_WavHeaderData.View = System.Windows.Forms.View.Details;
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            // 
            // Col_LoopOffset
            // 
            this.Col_LoopOffset.Text = "Loop Offset";
            this.Col_LoopOffset.Width = 69;
            // 
            // Col_Frequency
            // 
            this.Col_Frequency.Text = "Frequency";
            this.Col_Frequency.Width = 70;
            // 
            // Col_Channels
            // 
            this.Col_Channels.Text = "Channels";
            // 
            // Col_Bits
            // 
            this.Col_Bits.Text = "Bits";
            // 
            // Col_Data
            // 
            this.Col_Data.Text = "Data Length";
            this.Col_Data.Width = 74;
            // 
            // Col_Encoding
            // 
            this.Col_Encoding.Text = "Encoding";
            // 
            // Col_Duration
            // 
            this.Col_Duration.Text = "Duration";
            // 
            // TabPage_StreamData
            // 
            this.TabPage_StreamData.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_StreamData.Controls.Add(this.Button_UpdateList_StreamData);
            this.TabPage_StreamData.Controls.Add(this.ListView_StreamData);
            this.TabPage_StreamData.Location = new System.Drawing.Point(4, 22);
            this.TabPage_StreamData.Name = "TabPage_StreamData";
            this.TabPage_StreamData.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_StreamData.Size = new System.Drawing.Size(461, 254);
            this.TabPage_StreamData.TabIndex = 0;
            this.TabPage_StreamData.Text = "Stream Data";
            // 
            // Button_UpdateList_StreamData
            // 
            this.Button_UpdateList_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_StreamData.Location = new System.Drawing.Point(380, 225);
            this.Button_UpdateList_StreamData.Name = "Button_UpdateList_StreamData";
            this.Button_UpdateList_StreamData.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_StreamData.TabIndex = 1;
            this.Button_UpdateList_StreamData.Text = "Update";
            this.Button_UpdateList_StreamData.UseVisualStyleBackColor = true;
            this.Button_UpdateList_StreamData.Click += new System.EventHandler(this.Button_UpdateList_StreamData_Click);
            // 
            // ListView_StreamData
            // 
            this.ListView_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_StreamData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_StreamName,
            this.Col_FileRef,
            this.Col_Sound,
            this.Col_StoredIn});
            this.ListView_StreamData.GridLines = true;
            this.ListView_StreamData.HideSelection = false;
            this.ListView_StreamData.Location = new System.Drawing.Point(6, 6);
            this.ListView_StreamData.Name = "ListView_StreamData";
            this.ListView_StreamData.Size = new System.Drawing.Size(449, 213);
            this.ListView_StreamData.TabIndex = 0;
            this.ListView_StreamData.UseCompatibleStateImageBehavior = false;
            this.ListView_StreamData.View = System.Windows.Forms.View.Details;
            // 
            // Col_StreamName
            // 
            this.Col_StreamName.Text = "Name";
            // 
            // Col_FileRef
            // 
            this.Col_FileRef.Text = "File Ref";
            this.Col_FileRef.Width = 62;
            // 
            // Col_Sound
            // 
            this.Col_Sound.Text = "Sound";
            this.Col_Sound.Width = 105;
            // 
            // Col_StoredIn
            // 
            this.Col_StoredIn.Text = "Stored In";
            this.Col_StoredIn.Width = 178;
            // 
            // ContextMenu_Audio
            // 
            this.ContextMenu_Audio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuAudio_Usage,
            this.toolStripSeparator3,
            this.ContextMenuAudio_Remove,
            this.ContextMenuAudio_Rename,
            this.ContextMenuAudio_Properties,
            this.toolStripSeparator14,
            this.ContextMenuAudio_TextColor});
            this.ContextMenu_Audio.Name = "ContextMenu_Audio";
            this.ContextMenu_Audio.Size = new System.Drawing.Size(143, 126);
            // 
            // ContextMenuAudio_Usage
            // 
            this.ContextMenuAudio_Usage.Name = "ContextMenuAudio_Usage";
            this.ContextMenuAudio_Usage.Size = new System.Drawing.Size(142, 22);
            this.ContextMenuAudio_Usage.Text = "Usage";
            this.ContextMenuAudio_Usage.Click += new System.EventHandler(this.ContextMenuAudio_Usage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(139, 6);
            // 
            // ContextMenuAudio_Remove
            // 
            this.ContextMenuAudio_Remove.Name = "ContextMenuAudio_Remove";
            this.ContextMenuAudio_Remove.Size = new System.Drawing.Size(142, 22);
            this.ContextMenuAudio_Remove.Text = "Delete Audio";
            this.ContextMenuAudio_Remove.Click += new System.EventHandler(this.ContextMenuAudio_Remove_Click);
            // 
            // ContextMenuAudio_Rename
            // 
            this.ContextMenuAudio_Rename.Name = "ContextMenuAudio_Rename";
            this.ContextMenuAudio_Rename.Size = new System.Drawing.Size(142, 22);
            this.ContextMenuAudio_Rename.Text = "Rename";
            this.ContextMenuAudio_Rename.Click += new System.EventHandler(this.ContextMenuAudio_Rename_Click);
            // 
            // ContextMenuAudio_Properties
            // 
            this.ContextMenuAudio_Properties.Name = "ContextMenuAudio_Properties";
            this.ContextMenuAudio_Properties.Size = new System.Drawing.Size(142, 22);
            this.ContextMenuAudio_Properties.Text = "Properties...";
            this.ContextMenuAudio_Properties.Click += new System.EventHandler(this.ContextMenuAudio_Properties_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(139, 6);
            // 
            // ContextMenuAudio_TextColor
            // 
            this.ContextMenuAudio_TextColor.Name = "ContextMenuAudio_TextColor";
            this.ContextMenuAudio_TextColor.Size = new System.Drawing.Size(142, 22);
            this.ContextMenuAudio_TextColor.Text = "Text Color...";
            this.ContextMenuAudio_TextColor.Click += new System.EventHandler(this.ContextMenuAudio_TextColor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.RadioButton_WholeWord);
            this.groupBox1.Controls.Add(this.RadioButton_MatchText);
            this.groupBox1.Controls.Add(this.Button_Search);
            this.groupBox1.Controls.Add(this.Textbox_SearchHint);
            this.groupBox1.Controls.Add(this.Label_NameItemToSearch);
            this.groupBox1.Location = new System.Drawing.Point(500, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 77);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Item";
            // 
            // RadioButton_WholeWord
            // 
            this.RadioButton_WholeWord.AutoSize = true;
            this.RadioButton_WholeWord.Location = new System.Drawing.Point(138, 48);
            this.RadioButton_WholeWord.Name = "RadioButton_WholeWord";
            this.RadioButton_WholeWord.Size = new System.Drawing.Size(85, 17);
            this.RadioButton_WholeWord.TabIndex = 4;
            this.RadioButton_WholeWord.Text = "Whole Word";
            this.RadioButton_WholeWord.UseVisualStyleBackColor = true;
            // 
            // RadioButton_MatchText
            // 
            this.RadioButton_MatchText.AutoSize = true;
            this.RadioButton_MatchText.Checked = true;
            this.RadioButton_MatchText.Location = new System.Drawing.Point(50, 48);
            this.RadioButton_MatchText.Name = "RadioButton_MatchText";
            this.RadioButton_MatchText.Size = new System.Drawing.Size(82, 17);
            this.RadioButton_MatchText.TabIndex = 3;
            this.RadioButton_MatchText.TabStop = true;
            this.RadioButton_MatchText.Text = "Match Case";
            this.RadioButton_MatchText.UseVisualStyleBackColor = true;
            // 
            // Button_Search
            // 
            this.Button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Search.Location = new System.Drawing.Point(388, 48);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(75, 23);
            this.Button_Search.TabIndex = 2;
            this.Button_Search.Text = "Search";
            this.Button_Search.UseVisualStyleBackColor = true;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // Textbox_SearchHint
            // 
            this.Textbox_SearchHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SearchHint.Location = new System.Drawing.Point(50, 22);
            this.Textbox_SearchHint.Name = "Textbox_SearchHint";
            this.Textbox_SearchHint.Size = new System.Drawing.Size(413, 20);
            this.Textbox_SearchHint.TabIndex = 1;
            // 
            // Label_NameItemToSearch
            // 
            this.Label_NameItemToSearch.AutoSize = true;
            this.Label_NameItemToSearch.Location = new System.Drawing.Point(6, 25);
            this.Label_NameItemToSearch.Name = "Label_NameItemToSearch";
            this.Label_NameItemToSearch.Size = new System.Drawing.Size(38, 13);
            this.Label_NameItemToSearch.TabIndex = 0;
            this.Label_NameItemToSearch.Text = "Name:";
            // 
            // Button_GenerateList
            // 
            this.Button_GenerateList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_GenerateList.Location = new System.Drawing.Point(820, 705);
            this.Button_GenerateList.Name = "Button_GenerateList";
            this.Button_GenerateList.Size = new System.Drawing.Size(149, 23);
            this.Button_GenerateList.TabIndex = 5;
            this.Button_GenerateList.Text = "Generate list of used sounds";
            this.Button_GenerateList.UseVisualStyleBackColor = true;
            this.Button_GenerateList.Click += new System.EventHandler(this.Button_GenerateList_Click);
            // 
            // Frm_Soundbanks_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 740);
            this.Controls.Add(this.Button_GenerateList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TabControlDataViewer);
            this.Controls.Add(this.GroupBox_Hashcodes);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.TreeView_File);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Frm_Soundbanks_Main";
            this.Tag = "1";
            this.Text = "EuroSound Soundbank Edior";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Soundbanks_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Soundbanks_Main_Load);
            this.Shown += new System.EventHandler(this.Frm_Soundbanks_Main_Shown);
            this.Enter += new System.EventHandler(this.Frm_Soundbanks_Main_Enter);
            this.ContextMenu_Folders.ResumeLayout(false);
            this.ContextMenu_Sound.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ContextMenu_Sample.ResumeLayout(false);
            this.GroupBox_Hashcodes.ResumeLayout(false);
            this.TabControlDataViewer.ResumeLayout(false);
            this.TabPage_WavHeaderData.ResumeLayout(false);
            this.TabPage_StreamData.ResumeLayout(false);
            this.ContextMenu_Audio.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList ImageList_TreeNode;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Folders;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_AddSound;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Sound;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSound_AddSample;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSound_Remove;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSound_Properties;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSound_Rename;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_FileProps;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Sample;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSample_Remove;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSample_Properties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSample_Rename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_TextColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSound_TextColor;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSample_TextColor;
        private System.Windows.Forms.Button Button_UpdateList_WavData;
        private System.Windows.Forms.GroupBox GroupBox_Hashcodes;
        private System.Windows.Forms.Button Button_UpdateList_Hashcodes;
        private System.Windows.Forms.ListView ListView_Hashcodes;
        private System.Windows.Forms.ColumnHeader Col_Hashcode_OK;
        private System.Windows.Forms.ColumnHeader Col_Hashcode;
        private System.Windows.Forms.ColumnHeader Col_Hashcode_Label;
        private System.Windows.Forms.ColumnHeader Col_UsedIn;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl TabControlDataViewer;
        private System.Windows.Forms.TabPage TabPage_StreamData;
        private System.Windows.Forms.TabPage TabPage_WavHeaderData;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadYml;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolders_Folder;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_ExpandAll;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_CollapseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Delete;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadSound;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Sort;
        protected internal System.Windows.Forms.TreeView TreeView_File;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Move;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolders_New;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_AddAudio;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Audio;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudio_Remove;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudio_Rename;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudio_Properties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudio_TextColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Textbox_SearchHint;
        private System.Windows.Forms.Label Label_NameItemToSearch;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.RadioButton RadioButton_WholeWord;
        private System.Windows.Forms.RadioButton RadioButton_MatchText;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Rename;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudio_Usage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ImageList ImageList_ListView;
        private ListViewExtendedMethods.ListView_ColumnSortingClick ListView_WavHeaderData;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_LoopOffset;
        private System.Windows.Forms.ColumnHeader Col_Frequency;
        private System.Windows.Forms.ColumnHeader Col_Channels;
        private System.Windows.Forms.ColumnHeader Col_Bits;
        private System.Windows.Forms.ColumnHeader Col_Data;
        private System.Windows.Forms.ColumnHeader Col_Encoding;
        private System.Windows.Forms.ColumnHeader Col_Duration;
        private System.Windows.Forms.Button Button_UpdateList_StreamData;
        private ListViewExtendedMethods.ListView_ColumnSortingClick ListView_StreamData;
        private System.Windows.Forms.ColumnHeader Col_StreamName;
        private System.Windows.Forms.ColumnHeader Col_FileRef;
        private System.Windows.Forms.ColumnHeader Col_Sound;
        private System.Windows.Forms.ColumnHeader Col_StoredIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Purge;
        private System.Windows.Forms.Button Button_GenerateList;
    }
}