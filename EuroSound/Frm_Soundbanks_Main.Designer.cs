namespace EuroSound_SB_Editor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Sounds", 0, 0);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Streamed Sounds");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Soundbanks_Main));
            this.TreeView_File = new System.Windows.Forms.TreeView();
            this.ImageList_TreeNode = new System.Windows.Forms.ImageList(this.components);
            this.ContextMenu_Folders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_Folder_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Folder_Expand = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Folder_Collapse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Folder_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_AddSound = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_NewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Sound = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_AddToFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_AddSample = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_RemoveSound = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_SoundRename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_SoundProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSound_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_ReadYml = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ReadSound = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_FileProps = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Help_Online = new System.Windows.Forms.ToolStripMenuItem();
            this.Statusbar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrentFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ContextMenu_Sample = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_RemoveSample = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_RenameSample = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SampleProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSample_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorDialog_TextColor = new System.Windows.Forms.ColorDialog();
            this.Button_UpdateList_WavData = new System.Windows.Forms.Button();
            this.ListView_WavHeaderData = new System.Windows.Forms.ListView();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_ParentSound = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Channels = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Bits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Encoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox_Hashcodes = new System.Windows.Forms.GroupBox();
            this.Button_UpdateList_Hashcodes = new System.Windows.Forms.Button();
            this.ListView_Hashcodes = new System.Windows.Forms.ListView();
            this.Col_Hashcode_OK = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Hashcode_Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_UsedIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabControlDataViewer = new System.Windows.Forms.TabControl();
            this.TabPage_StreamData = new System.Windows.Forms.TabPage();
            this.ListView_StreamData = new System.Windows.Forms.ListView();
            this.Col_StreamName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_AsociatedTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_FileRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_LocatedIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabPage_WavHeaderData = new System.Windows.Forms.TabPage();
            this.MenuItem_MoveSound = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_MoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_MoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Folders.SuspendLayout();
            this.ContextMenu_Sound.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.Statusbar.SuspendLayout();
            this.ContextMenu_Sample.SuspendLayout();
            this.GroupBox_Hashcodes.SuspendLayout();
            this.TabControlDataViewer.SuspendLayout();
            this.TabPage_StreamData.SuspendLayout();
            this.TabPage_WavHeaderData.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView_File
            // 
            this.TreeView_File.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_File.ImageIndex = 0;
            this.TreeView_File.ImageList = this.ImageList_TreeNode;
            this.TreeView_File.LabelEdit = true;
            this.TreeView_File.Location = new System.Drawing.Point(0, 25);
            this.TreeView_File.Name = "TreeView_File";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Sounds";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Tag = "Root";
            treeNode1.Text = "Sounds";
            treeNode2.ImageIndex = 0;
            treeNode2.Name = "StreamedSounds";
            treeNode2.Tag = "Root";
            treeNode2.Text = "Streamed Sounds";
            this.TreeView_File.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.TreeView_File.SelectedImageIndex = 0;
            this.TreeView_File.Size = new System.Drawing.Size(666, 849);
            this.TreeView_File.TabIndex = 1;
            this.TreeView_File.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_File_AfterLabelEdit);
            this.TreeView_File.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_File_BeforeCollapse);
            this.TreeView_File.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_File_BeforeExpand);
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
            // 
            // ContextMenu_Folders
            // 
            this.ContextMenu_Folders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Folder_Options,
            this.toolStripSeparator7,
            this.MenuItem_AddSound,
            this.MenuItem_NewFolder,
            this.toolStripSeparator5,
            this.MenuItem_TextColor});
            this.ContextMenu_Folders.Name = "contextMenuStrip1";
            this.ContextMenu_Folders.Size = new System.Drawing.Size(144, 104);
            // 
            // MenuItem_Folder_Options
            // 
            this.MenuItem_Folder_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Folder_Expand,
            this.MenuItem_Folder_Collapse,
            this.toolStripSeparator8,
            this.MenuItem_Folder_Delete});
            this.MenuItem_Folder_Options.Name = "MenuItem_Folder_Options";
            this.MenuItem_Folder_Options.Size = new System.Drawing.Size(143, 22);
            this.MenuItem_Folder_Options.Text = "Folder";
            // 
            // MenuItem_Folder_Expand
            // 
            this.MenuItem_Folder_Expand.Name = "MenuItem_Folder_Expand";
            this.MenuItem_Folder_Expand.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_Folder_Expand.Text = "Expand All";
            this.MenuItem_Folder_Expand.Click += new System.EventHandler(this.MenuItem_Folder_Expand_Click);
            // 
            // MenuItem_Folder_Collapse
            // 
            this.MenuItem_Folder_Collapse.Name = "MenuItem_Folder_Collapse";
            this.MenuItem_Folder_Collapse.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_Folder_Collapse.Text = "Collapse All";
            this.MenuItem_Folder_Collapse.Click += new System.EventHandler(this.MenuItem_Folder_Collapse_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(133, 6);
            // 
            // MenuItem_Folder_Delete
            // 
            this.MenuItem_Folder_Delete.Name = "MenuItem_Folder_Delete";
            this.MenuItem_Folder_Delete.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_Folder_Delete.Text = "Delete";
            this.MenuItem_Folder_Delete.Click += new System.EventHandler(this.MenuItem_Folder_Delete_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(140, 6);
            // 
            // MenuItem_AddSound
            // 
            this.MenuItem_AddSound.Name = "MenuItem_AddSound";
            this.MenuItem_AddSound.Size = new System.Drawing.Size(143, 22);
            this.MenuItem_AddSound.Text = "Add Sound";
            this.MenuItem_AddSound.Click += new System.EventHandler(this.MenuItem_AddSound_Click);
            // 
            // MenuItem_NewFolder
            // 
            this.MenuItem_NewFolder.Name = "MenuItem_NewFolder";
            this.MenuItem_NewFolder.Size = new System.Drawing.Size(143, 22);
            this.MenuItem_NewFolder.Text = "New Folder";
            this.MenuItem_NewFolder.Click += new System.EventHandler(this.NewFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
            // 
            // MenuItem_TextColor
            // 
            this.MenuItem_TextColor.Name = "MenuItem_TextColor";
            this.MenuItem_TextColor.Size = new System.Drawing.Size(143, 22);
            this.MenuItem_TextColor.Text = "Text Colour...";
            this.MenuItem_TextColor.Click += new System.EventHandler(this.ContextMenuFolders_TextColor_Click);
            // 
            // ContextMenu_Sound
            // 
            this.ContextMenu_Sound.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_MoveSound,
            this.MenuItem_AddToFolder,
            this.toolStripSeparator10,
            this.MenuItem_AddSample,
            this.MenuItem_RemoveSound,
            this.ContextMenu_SoundRename,
            this.ContextMenu_SoundProperties,
            this.toolStripSeparator6,
            this.ContextMenuSound_TextColor});
            this.ContextMenu_Sound.Name = "ContextMenu_Sound";
            this.ContextMenu_Sound.Size = new System.Drawing.Size(154, 170);
            this.ContextMenu_Sound.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Sound_Opening);
            // 
            // MenuItem_AddToFolder
            // 
            this.MenuItem_AddToFolder.Name = "MenuItem_AddToFolder";
            this.MenuItem_AddToFolder.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_AddToFolder.Text = "Add to folder";
            this.MenuItem_AddToFolder.Click += new System.EventHandler(this.MenuItem_AddToFolder_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItem_AddSample
            // 
            this.MenuItem_AddSample.Name = "MenuItem_AddSample";
            this.MenuItem_AddSample.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_AddSample.Text = "Add sample";
            this.MenuItem_AddSample.Click += new System.EventHandler(this.MenuItem_AddSample_Click);
            // 
            // MenuItem_RemoveSound
            // 
            this.MenuItem_RemoveSound.Name = "MenuItem_RemoveSound";
            this.MenuItem_RemoveSound.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_RemoveSound.Text = "Remove sound";
            this.MenuItem_RemoveSound.Click += new System.EventHandler(this.MenuItem_RemoveSound_Click);
            // 
            // ContextMenu_SoundRename
            // 
            this.ContextMenu_SoundRename.Name = "ContextMenu_SoundRename";
            this.ContextMenu_SoundRename.Size = new System.Drawing.Size(180, 22);
            this.ContextMenu_SoundRename.Text = "Rename";
            this.ContextMenu_SoundRename.Click += new System.EventHandler(this.ContextMenu_SoundRename_Click);
            // 
            // ContextMenu_SoundProperties
            // 
            this.ContextMenu_SoundProperties.Name = "ContextMenu_SoundProperties";
            this.ContextMenu_SoundProperties.Size = new System.Drawing.Size(180, 22);
            this.ContextMenu_SoundProperties.Text = "Properties";
            this.ContextMenu_SoundProperties.Click += new System.EventHandler(this.ContextMenu_SoundProperties_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
            // 
            // ContextMenuSound_TextColor
            // 
            this.ContextMenuSound_TextColor.Name = "ContextMenuSound_TextColor";
            this.ContextMenuSound_TextColor.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuSound_TextColor.Text = "Text Colour...";
            this.ContextMenuSound_TextColor.Click += new System.EventHandler(this.ContextMenuSound_TextColor_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File,
            this.MenuItem_Edit,
            this.MenuItem_Help});
            this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1191, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File_Open,
            this.toolStripSeparator3,
            this.MenuItem_File_Save,
            this.MenuItem_File_SaveAs,
            this.toolStripSeparator2,
            this.MenuItemFile_Export,
            this.toolStripSeparator1,
            this.MenuItemFile_ReadYml,
            this.MenuItemFile_ReadSound,
            this.toolStripSeparator9,
            this.MenuItem_File_Exit});
            this.MenuItem_File.MergeIndex = 0;
            this.MenuItem_File.Name = "MenuItem_File";
            this.MenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.MenuItem_File.Text = "File";
            // 
            // MenuItem_File_Open
            // 
            this.MenuItem_File_Open.Name = "MenuItem_File_Open";
            this.MenuItem_File_Open.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_Open.Text = "Open";
            this.MenuItem_File_Open.Click += new System.EventHandler(this.MenuItem_File_Open_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(204, 6);
            // 
            // MenuItem_File_Save
            // 
            this.MenuItem_File_Save.Name = "MenuItem_File_Save";
            this.MenuItem_File_Save.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_Save.Text = "Save";
            this.MenuItem_File_Save.Click += new System.EventHandler(this.MenuItem_File_Save_Click);
            // 
            // MenuItem_File_SaveAs
            // 
            this.MenuItem_File_SaveAs.Name = "MenuItem_File_SaveAs";
            this.MenuItem_File_SaveAs.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_SaveAs.Text = "Save As...";
            this.MenuItem_File_SaveAs.Click += new System.EventHandler(this.MenuItem_File_SaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // MenuItemFile_Export
            // 
            this.MenuItemFile_Export.Name = "MenuItemFile_Export";
            this.MenuItemFile_Export.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_Export.Text = "Export";
            this.MenuItemFile_Export.Click += new System.EventHandler(this.MenuItemFile_Export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // MenuItemFile_ReadYml
            // 
            this.MenuItemFile_ReadYml.Name = "MenuItemFile_ReadYml";
            this.MenuItemFile_ReadYml.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_ReadYml.Text = "Import Sounds List (.yml)";
            this.MenuItemFile_ReadYml.Click += new System.EventHandler(this.MenuItemFile_ReadYml_Click);
            // 
            // MenuItemFile_ReadSound
            // 
            this.MenuItemFile_ReadSound.Name = "MenuItemFile_ReadSound";
            this.MenuItemFile_ReadSound.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_ReadSound.Text = "Read Sound (.yml)";
            this.MenuItemFile_ReadSound.Click += new System.EventHandler(this.MenuItemFile_ReadSound_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(204, 6);
            // 
            // MenuItem_File_Exit
            // 
            this.MenuItem_File_Exit.Name = "MenuItem_File_Exit";
            this.MenuItem_File_Exit.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_Exit.Text = "Exit";
            this.MenuItem_File_Exit.Click += new System.EventHandler(this.MenuItem_File_Exit_Click);
            // 
            // MenuItem_Edit
            // 
            this.MenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Edit_FileProps});
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
            // MenuItem_Help
            // 
            this.MenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Help_About,
            this.MenuItem_Help_Online});
            this.MenuItem_Help.Name = "MenuItem_Help";
            this.MenuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.MenuItem_Help.Text = "Help";
            // 
            // MenuItem_Help_About
            // 
            this.MenuItem_Help_About.Name = "MenuItem_Help_About";
            this.MenuItem_Help_About.Size = new System.Drawing.Size(177, 22);
            this.MenuItem_Help_About.Text = "About EuroSound...";
            this.MenuItem_Help_About.Click += new System.EventHandler(this.MenuItem_Help_About_Click);
            // 
            // MenuItem_Help_Online
            // 
            this.MenuItem_Help_Online.Name = "MenuItem_Help_Online";
            this.MenuItem_Help_Online.Size = new System.Drawing.Size(177, 22);
            this.MenuItem_Help_Online.Text = "Online Help...";
            // 
            // Statusbar
            // 
            this.Statusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.CurrentFileLabel});
            this.Statusbar.Location = new System.Drawing.Point(0, 873);
            this.Statusbar.Name = "Statusbar";
            this.Statusbar.Size = new System.Drawing.Size(1191, 22);
            this.Statusbar.TabIndex = 4;
            this.Statusbar.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel.Text = "Ready";
            // 
            // CurrentFileLabel
            // 
            this.CurrentFileLabel.Name = "CurrentFileLabel";
            this.CurrentFileLabel.Size = new System.Drawing.Size(1137, 17);
            this.CurrentFileLabel.Spring = true;
            this.CurrentFileLabel.Text = "Current file";
            this.CurrentFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ContextMenu_Sample
            // 
            this.ContextMenu_Sample.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_RemoveSample,
            this.MenuItem_RenameSample,
            this.MenuItem_SampleProperties,
            this.toolStripSeparator4,
            this.ContextMenuSample_TextColor});
            this.ContextMenu_Sample.Name = "ContextMenu_Sample";
            this.ContextMenu_Sample.Size = new System.Drawing.Size(160, 98);
            // 
            // MenuItem_RemoveSample
            // 
            this.MenuItem_RemoveSample.Name = "MenuItem_RemoveSample";
            this.MenuItem_RemoveSample.Size = new System.Drawing.Size(159, 22);
            this.MenuItem_RemoveSample.Text = "Remove Sample";
            this.MenuItem_RemoveSample.Click += new System.EventHandler(this.MenuItem_RemoveSample_Click);
            // 
            // MenuItem_RenameSample
            // 
            this.MenuItem_RenameSample.Name = "MenuItem_RenameSample";
            this.MenuItem_RenameSample.Size = new System.Drawing.Size(159, 22);
            this.MenuItem_RenameSample.Text = "Rename";
            this.MenuItem_RenameSample.Click += new System.EventHandler(this.MenuItem_RenameSample_Click);
            // 
            // MenuItem_SampleProperties
            // 
            this.MenuItem_SampleProperties.Name = "MenuItem_SampleProperties";
            this.MenuItem_SampleProperties.Size = new System.Drawing.Size(159, 22);
            this.MenuItem_SampleProperties.Text = "Properties";
            this.MenuItem_SampleProperties.Click += new System.EventHandler(this.MenuItem_SampleProperties_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(156, 6);
            // 
            // ContextMenuSample_TextColor
            // 
            this.ContextMenuSample_TextColor.Name = "ContextMenuSample_TextColor";
            this.ContextMenuSample_TextColor.Size = new System.Drawing.Size(159, 22);
            this.ContextMenuSample_TextColor.Text = "Text Colour...";
            this.ContextMenuSample_TextColor.Click += new System.EventHandler(this.ContextMenuSample_TextColor_Click);
            // 
            // ColorDialog_TextColor
            // 
            this.ColorDialog_TextColor.FullOpen = true;
            // 
            // Button_UpdateList_WavData
            // 
            this.Button_UpdateList_WavData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_WavData.Location = new System.Drawing.Point(424, 290);
            this.Button_UpdateList_WavData.Name = "Button_UpdateList_WavData";
            this.Button_UpdateList_WavData.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_WavData.TabIndex = 1;
            this.Button_UpdateList_WavData.Text = "Update List";
            this.Button_UpdateList_WavData.UseVisualStyleBackColor = true;
            this.Button_UpdateList_WavData.Click += new System.EventHandler(this.Button_UpdateList_WavData_Click);
            // 
            // ListView_WavHeaderData
            // 
            this.ListView_WavHeaderData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_WavHeaderData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_ParentSound,
            this.Col_Frequency,
            this.Col_Channels,
            this.Col_Bits,
            this.Col_Data,
            this.Col_Encoding,
            this.Col_Duration});
            this.ListView_WavHeaderData.FullRowSelect = true;
            this.ListView_WavHeaderData.GridLines = true;
            this.ListView_WavHeaderData.HideSelection = false;
            this.ListView_WavHeaderData.Location = new System.Drawing.Point(2, 6);
            this.ListView_WavHeaderData.Name = "ListView_WavHeaderData";
            this.ListView_WavHeaderData.Size = new System.Drawing.Size(501, 278);
            this.ListView_WavHeaderData.TabIndex = 0;
            this.ListView_WavHeaderData.UseCompatibleStateImageBehavior = false;
            this.ListView_WavHeaderData.View = System.Windows.Forms.View.Details;
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            // 
            // Col_ParentSound
            // 
            this.Col_ParentSound.Text = "Asociated To";
            this.Col_ParentSound.Width = 120;
            // 
            // Col_Frequency
            // 
            this.Col_Frequency.Text = "Frequency";
            this.Col_Frequency.Width = 66;
            // 
            // Col_Channels
            // 
            this.Col_Channels.Text = "Channels";
            this.Col_Channels.Width = 57;
            // 
            // Col_Bits
            // 
            this.Col_Bits.Text = "Bits";
            this.Col_Bits.Width = 42;
            // 
            // Col_Data
            // 
            this.Col_Data.Text = "Data Length";
            this.Col_Data.Width = 99;
            // 
            // Col_Encoding
            // 
            this.Col_Encoding.Text = "Encoding";
            // 
            // Col_Duration
            // 
            this.Col_Duration.Text = "Duration";
            this.Col_Duration.Width = 70;
            // 
            // GroupBox_Hashcodes
            // 
            this.GroupBox_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Hashcodes.Controls.Add(this.Button_UpdateList_Hashcodes);
            this.GroupBox_Hashcodes.Controls.Add(this.ListView_Hashcodes);
            this.GroupBox_Hashcodes.Location = new System.Drawing.Point(672, 376);
            this.GroupBox_Hashcodes.Name = "GroupBox_Hashcodes";
            this.GroupBox_Hashcodes.Size = new System.Drawing.Size(513, 375);
            this.GroupBox_Hashcodes.TabIndex = 3;
            this.GroupBox_Hashcodes.TabStop = false;
            this.GroupBox_Hashcodes.Text = "Hashcodes";
            // 
            // Button_UpdateList_Hashcodes
            // 
            this.Button_UpdateList_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_Hashcodes.Location = new System.Drawing.Point(432, 346);
            this.Button_UpdateList_Hashcodes.Name = "Button_UpdateList_Hashcodes";
            this.Button_UpdateList_Hashcodes.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_Hashcodes.TabIndex = 1;
            this.Button_UpdateList_Hashcodes.Text = "Update List";
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
            this.ListView_Hashcodes.Size = new System.Drawing.Size(501, 321);
            this.ListView_Hashcodes.TabIndex = 0;
            this.ListView_Hashcodes.UseCompatibleStateImageBehavior = false;
            this.ListView_Hashcodes.View = System.Windows.Forms.View.Details;
            // 
            // Col_Hashcode_OK
            // 
            this.Col_Hashcode_OK.Text = "OK";
            this.Col_Hashcode_OK.Width = 45;
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
            // TabControlDataViewer
            // 
            this.TabControlDataViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControlDataViewer.Controls.Add(this.TabPage_WavHeaderData);
            this.TabControlDataViewer.Controls.Add(this.TabPage_StreamData);
            this.TabControlDataViewer.Location = new System.Drawing.Point(672, 25);
            this.TabControlDataViewer.Name = "TabControlDataViewer";
            this.TabControlDataViewer.SelectedIndex = 0;
            this.TabControlDataViewer.Size = new System.Drawing.Size(513, 345);
            this.TabControlDataViewer.TabIndex = 2;
            // 
            // TabPage_StreamData
            // 
            this.TabPage_StreamData.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_StreamData.Controls.Add(this.ListView_StreamData);
            this.TabPage_StreamData.Location = new System.Drawing.Point(4, 22);
            this.TabPage_StreamData.Name = "TabPage_StreamData";
            this.TabPage_StreamData.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_StreamData.Size = new System.Drawing.Size(505, 319);
            this.TabPage_StreamData.TabIndex = 0;
            this.TabPage_StreamData.Text = "Stream Data";
            // 
            // ListView_StreamData
            // 
            this.ListView_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_StreamData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_StreamName,
            this.Col_AsociatedTo,
            this.Col_FileRef,
            this.Col_LocatedIn});
            this.ListView_StreamData.FullRowSelect = true;
            this.ListView_StreamData.GridLines = true;
            this.ListView_StreamData.HideSelection = false;
            this.ListView_StreamData.Location = new System.Drawing.Point(2, 6);
            this.ListView_StreamData.Name = "ListView_StreamData";
            this.ListView_StreamData.Size = new System.Drawing.Size(503, 278);
            this.ListView_StreamData.TabIndex = 1;
            this.ListView_StreamData.UseCompatibleStateImageBehavior = false;
            this.ListView_StreamData.View = System.Windows.Forms.View.Details;
            // 
            // Col_StreamName
            // 
            this.Col_StreamName.Text = "Name";
            // 
            // Col_AsociatedTo
            // 
            this.Col_AsociatedTo.Text = "Asociated To";
            this.Col_AsociatedTo.Width = 91;
            // 
            // Col_FileRef
            // 
            this.Col_FileRef.Text = "File Ref";
            // 
            // Col_LocatedIn
            // 
            this.Col_LocatedIn.Text = "Located In";
            this.Col_LocatedIn.Width = 84;
            // 
            // TabPage_WavHeaderData
            // 
            this.TabPage_WavHeaderData.BackColor = System.Drawing.SystemColors.Control;
            this.TabPage_WavHeaderData.Controls.Add(this.Button_UpdateList_WavData);
            this.TabPage_WavHeaderData.Controls.Add(this.ListView_WavHeaderData);
            this.TabPage_WavHeaderData.Location = new System.Drawing.Point(4, 22);
            this.TabPage_WavHeaderData.Name = "TabPage_WavHeaderData";
            this.TabPage_WavHeaderData.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_WavHeaderData.Size = new System.Drawing.Size(505, 319);
            this.TabPage_WavHeaderData.TabIndex = 1;
            this.TabPage_WavHeaderData.Text = "Wav Header Data";
            // 
            // MenuItem_MoveSound
            // 
            this.MenuItem_MoveSound.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_MoveUp,
            this.MenuItem_MoveDown});
            this.MenuItem_MoveSound.Name = "MenuItem_MoveSound";
            this.MenuItem_MoveSound.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_MoveSound.Text = "Move";
            // 
            // MenuItem_MoveUp
            // 
            this.MenuItem_MoveUp.Name = "MenuItem_MoveUp";
            this.MenuItem_MoveUp.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_MoveUp.Text = "Move Up";
            this.MenuItem_MoveUp.Click += new System.EventHandler(this.MenuItem_MoveUp_Click);
            // 
            // MenuItem_MoveDown
            // 
            this.MenuItem_MoveDown.Name = "MenuItem_MoveDown";
            this.MenuItem_MoveDown.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_MoveDown.Text = "Move Down";
            this.MenuItem_MoveDown.Click += new System.EventHandler(this.MenuItem_MoveDown_Click);
            // 
            // Frm_Soundbanks_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 895);
            this.Controls.Add(this.TabControlDataViewer);
            this.Controls.Add(this.GroupBox_Hashcodes);
            this.Controls.Add(this.Statusbar);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.TreeView_File);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Frm_Soundbanks_Main";
            this.Text = "EuroSound Soundbank Edior";
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.ContextMenu_Folders.ResumeLayout(false);
            this.ContextMenu_Sound.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.Statusbar.ResumeLayout(false);
            this.Statusbar.PerformLayout();
            this.ContextMenu_Sample.ResumeLayout(false);
            this.GroupBox_Hashcodes.ResumeLayout(false);
            this.TabControlDataViewer.ResumeLayout(false);
            this.TabPage_StreamData.ResumeLayout(false);
            this.TabPage_WavHeaderData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView TreeView_File;
        private System.Windows.Forms.ImageList ImageList_TreeNode;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Folders;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_AddSound;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Sound;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_AddSample;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_RemoveSound;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_SoundProperties;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_SoundRename;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Exit;
        private System.Windows.Forms.StatusStrip Statusbar;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_FileProps;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Sample;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_RemoveSample;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SampleProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_RenameSample;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Help_About;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_NewFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_TextColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSound_TextColor;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSample_TextColor;
        private System.Windows.Forms.ColorDialog ColorDialog_TextColor;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Help_Online;
        public System.Windows.Forms.ToolStripStatusLabel CurrentFileLabel;
        private System.Windows.Forms.ListView ListView_WavHeaderData;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Frequency;
        private System.Windows.Forms.ColumnHeader Col_Channels;
        private System.Windows.Forms.ColumnHeader Col_Data;
        private System.Windows.Forms.ColumnHeader Col_Bits;
        private System.Windows.Forms.Button Button_UpdateList_WavData;
        private System.Windows.Forms.ColumnHeader Col_Encoding;
        private System.Windows.Forms.GroupBox GroupBox_Hashcodes;
        private System.Windows.Forms.Button Button_UpdateList_Hashcodes;
        private System.Windows.Forms.ListView ListView_Hashcodes;
        private System.Windows.Forms.ColumnHeader Col_Hashcode_OK;
        private System.Windows.Forms.ColumnHeader Col_Hashcode;
        private System.Windows.Forms.ColumnHeader Col_Hashcode_Label;
        private System.Windows.Forms.ColumnHeader Col_UsedIn;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader Col_Duration;
        private System.Windows.Forms.TabControl TabControlDataViewer;
        private System.Windows.Forms.TabPage TabPage_StreamData;
        private System.Windows.Forms.TabPage TabPage_WavHeaderData;
        private System.Windows.Forms.ColumnHeader Col_ParentSound;
        private System.Windows.Forms.ListView ListView_StreamData;
        private System.Windows.Forms.ColumnHeader Col_StreamName;
        private System.Windows.Forms.ColumnHeader Col_AsociatedTo;
        private System.Windows.Forms.ColumnHeader Col_FileRef;
        private System.Windows.Forms.ColumnHeader Col_LocatedIn;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadYml;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Folder_Options;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Folder_Expand;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Folder_Collapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Folder_Delete;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadSound;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_AddToFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_MoveSound;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_MoveUp;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_MoveDown;
    }
}