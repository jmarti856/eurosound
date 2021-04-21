
namespace EuroSound_Application.Musics
{
    partial class Frm_Musics_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Musics_Main));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Musics", 0, 0);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_File_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_File_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ReadESIF = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ReadYml = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ReadMusic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_FileProps = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Edit_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Edit_Search = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageList_TreeNode = new System.Windows.Forms.ImageList(this.components);
            this.ContextMenu_Musics = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuMusics_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMusics_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMusics_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMusics_ExportESIF = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMusics_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuMusics_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Folders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuFolder_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_NewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_CollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_SortItems = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_AddSound = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.SplitContainerMusicsForm = new System.Windows.Forms.SplitContainer();
            this.TreeView_MusicData = new System.Windows.Forms.TreeView();
            this.GroupBox_StreamData = new System.Windows.Forms.GroupBox();
            this.Textbox_DataCount = new System.Windows.Forms.TextBox();
            this.Label_ItemsCountWav = new System.Windows.Forms.Label();
            this.Button_StopUpdate = new System.Windows.Forms.Button();
            this.ListView_WavHeaderData = new EuroSound_Application.CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Channels = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Bits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_DataLenght = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Encoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkersCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_StartMarkerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_UpdateProperties = new System.Windows.Forms.Button();
            this.GroupBox_MusicHashcodes = new System.Windows.Forms.GroupBox();
            this.Rtbx_Jump_Music_Codes = new System.Windows.Forms.RichTextBox();
            this.ContextMenu_RichTextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RichTextBox_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.RichTextBox_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RichTextBox_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.RichTextBox_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Button_Generate_Hashcodes = new System.Windows.Forms.Button();
            this.Button_UpdateIMAData = new System.Windows.Forms.Button();
            this.Button_ExportInterchangeFile = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.ContextMenu_Musics.SuspendLayout();
            this.ContextMenu_Folders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMusicsForm)).BeginInit();
            this.SplitContainerMusicsForm.Panel1.SuspendLayout();
            this.SplitContainerMusicsForm.Panel2.SuspendLayout();
            this.SplitContainerMusicsForm.SuspendLayout();
            this.GroupBox_StreamData.SuspendLayout();
            this.GroupBox_MusicHashcodes.SuspendLayout();
            this.ContextMenu_RichTextBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File,
            this.MenuItem_Edit});
            this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(908, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "MainMenu";
            this.MainMenu.Visible = false;
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File_Close,
            this.MenuItem_File_Save,
            this.MenuItem_File_SaveAs,
            this.MenuItem_File_Separator1,
            this.MenuItem_File_Export,
            this.MenuItem_File_Separator2,
            this.MenuItemFile_Import});
            this.MenuItem_File.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.MenuItem_File.MergeIndex = 0;
            this.MenuItem_File.Name = "MenuItem_File";
            this.MenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.MenuItem_File.Text = "File";
            // 
            // MenuItem_File_Close
            // 
            this.MenuItem_File_Close.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Close.MergeIndex = 2;
            this.MenuItem_File_Close.Name = "MenuItem_File_Close";
            this.MenuItem_File_Close.Size = new System.Drawing.Size(209, 22);
            this.MenuItem_File_Close.Text = "Close";
            this.MenuItem_File_Close.Click += new System.EventHandler(this.MenuItem_File_Close_Click);
            // 
            // MenuItem_File_Save
            // 
            this.MenuItem_File_Save.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Save.MergeIndex = 3;
            this.MenuItem_File_Save.Name = "MenuItem_File_Save";
            this.MenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuItem_File_Save.Size = new System.Drawing.Size(209, 22);
            this.MenuItem_File_Save.Text = "Save";
            this.MenuItem_File_Save.Click += new System.EventHandler(this.MenuItem_File_Save_Click);
            // 
            // MenuItem_File_SaveAs
            // 
            this.MenuItem_File_SaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_SaveAs.MergeIndex = 4;
            this.MenuItem_File_SaveAs.Name = "MenuItem_File_SaveAs";
            this.MenuItem_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MenuItem_File_SaveAs.Size = new System.Drawing.Size(209, 22);
            this.MenuItem_File_SaveAs.Text = "Save As...";
            this.MenuItem_File_SaveAs.Click += new System.EventHandler(this.MenuItem_File_SaveAs_Click);
            // 
            // MenuItem_File_Separator1
            // 
            this.MenuItem_File_Separator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Separator1.MergeIndex = 5;
            this.MenuItem_File_Separator1.Name = "MenuItem_File_Separator1";
            this.MenuItem_File_Separator1.Size = new System.Drawing.Size(206, 6);
            // 
            // MenuItem_File_Export
            // 
            this.MenuItem_File_Export.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Export.MergeIndex = 6;
            this.MenuItem_File_Export.Name = "MenuItem_File_Export";
            this.MenuItem_File_Export.Size = new System.Drawing.Size(209, 22);
            this.MenuItem_File_Export.Text = "Export";
            this.MenuItem_File_Export.Click += new System.EventHandler(this.MenuItem_File_Export_Click);
            // 
            // MenuItem_File_Separator2
            // 
            this.MenuItem_File_Separator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Separator2.MergeIndex = 7;
            this.MenuItem_File_Separator2.Name = "MenuItem_File_Separator2";
            this.MenuItem_File_Separator2.Size = new System.Drawing.Size(206, 6);
            // 
            // MenuItemFile_Import
            // 
            this.MenuItemFile_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_ReadESIF,
            this.MenuItemFile_ReadYml,
            this.MenuItemFile_ReadMusic});
            this.MenuItemFile_Import.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_Import.MergeIndex = 8;
            this.MenuItemFile_Import.Name = "MenuItemFile_Import";
            this.MenuItemFile_Import.Size = new System.Drawing.Size(209, 22);
            this.MenuItemFile_Import.Text = "Import...";
            // 
            // MenuItemFile_ReadESIF
            // 
            this.MenuItemFile_ReadESIF.Name = "MenuItemFile_ReadESIF";
            this.MenuItemFile_ReadESIF.Size = new System.Drawing.Size(205, 22);
            this.MenuItemFile_ReadESIF.Text = "Import ESIF";
            this.MenuItemFile_ReadESIF.Click += new System.EventHandler(this.MenuItemFile_ReadESIF_Click);
            // 
            // MenuItemFile_ReadYml
            // 
            this.MenuItemFile_ReadYml.Name = "MenuItemFile_ReadYml";
            this.MenuItemFile_ReadYml.Size = new System.Drawing.Size(205, 22);
            this.MenuItemFile_ReadYml.Text = "Import Musics List (.yml)";
            // 
            // MenuItemFile_ReadMusic
            // 
            this.MenuItemFile_ReadMusic.Name = "MenuItemFile_ReadMusic";
            this.MenuItemFile_ReadMusic.Size = new System.Drawing.Size(205, 22);
            this.MenuItemFile_ReadMusic.Text = "Read Music (.yml)";
            // 
            // MenuItem_Edit
            // 
            this.MenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Edit_FileProps,
            this.MenuItem_Edit_Separator1,
            this.MenuItem_Edit_Undo,
            this.MenuItem_Edit_Separator2,
            this.MenuItem_Edit_Search});
            this.MenuItem_Edit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_Edit.MergeIndex = 1;
            this.MenuItem_Edit.Name = "MenuItem_Edit";
            this.MenuItem_Edit.Size = new System.Drawing.Size(39, 20);
            this.MenuItem_Edit.Text = "Edit";
            // 
            // MenuItem_Edit_FileProps
            // 
            this.MenuItem_Edit_FileProps.Name = "MenuItem_Edit_FileProps";
            this.MenuItem_Edit_FileProps.Size = new System.Drawing.Size(158, 22);
            this.MenuItem_Edit_FileProps.Text = "File Properties";
            this.MenuItem_Edit_FileProps.Click += new System.EventHandler(this.MenuItem_Edit_FileProps_Click);
            // 
            // MenuItem_Edit_Separator1
            // 
            this.MenuItem_Edit_Separator1.Name = "MenuItem_Edit_Separator1";
            this.MenuItem_Edit_Separator1.Size = new System.Drawing.Size(155, 6);
            // 
            // MenuItem_Edit_Undo
            // 
            this.MenuItem_Edit_Undo.Enabled = false;
            this.MenuItem_Edit_Undo.Name = "MenuItem_Edit_Undo";
            this.MenuItem_Edit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuItem_Edit_Undo.Size = new System.Drawing.Size(158, 22);
            this.MenuItem_Edit_Undo.Text = "Undo";
            this.MenuItem_Edit_Undo.Click += new System.EventHandler(this.MenuItem_Edit_Undo_Click);
            // 
            // MenuItem_Edit_Separator2
            // 
            this.MenuItem_Edit_Separator2.Name = "MenuItem_Edit_Separator2";
            this.MenuItem_Edit_Separator2.Size = new System.Drawing.Size(155, 6);
            // 
            // MenuItem_Edit_Search
            // 
            this.MenuItem_Edit_Search.Name = "MenuItem_Edit_Search";
            this.MenuItem_Edit_Search.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MenuItem_Edit_Search.Size = new System.Drawing.Size(158, 22);
            this.MenuItem_Edit_Search.Text = "Search...";
            this.MenuItem_Edit_Search.Click += new System.EventHandler(this.MenuItem_Edit_Search_Click);
            // 
            // ImageList_TreeNode
            // 
            this.ImageList_TreeNode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_TreeNode.ImageStream")));
            this.ImageList_TreeNode.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_TreeNode.Images.SetKeyName(0, "directory_closed_cool-4.png");
            this.ImageList_TreeNode.Images.SetKeyName(1, "directory_open_cool-4.png");
            this.ImageList_TreeNode.Images.SetKeyName(2, "cd_audio_cd-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(3, "cd_audio_cd-2.png");
            this.ImageList_TreeNode.Images.SetKeyName(4, "audio_compression-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(5, "cd_audio_cd-1 - no_output.png");
            this.ImageList_TreeNode.Images.SetKeyName(6, "cd_audio_cd-2 - no_output.png");
            this.ImageList_TreeNode.Images.SetKeyName(7, "amplify.png");
            this.ImageList_TreeNode.Images.SetKeyName(8, "mmsys_118.ico");
            this.ImageList_TreeNode.Images.SetKeyName(9, "directory_closed-1.png");
            this.ImageList_TreeNode.Images.SetKeyName(10, "directory_open_cool-1.png");
            // 
            // ContextMenu_Musics
            // 
            this.ContextMenu_Musics.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuMusics_Rename,
            this.ContextMenuMusics_Delete,
            this.ContextMenuMusics_Properties,
            this.ContextMenuMusics_ExportESIF,
            this.ContextMenuMusics_Separator2,
            this.ContextMenuMusics_TextColor});
            this.ContextMenu_Musics.Name = "ContextMenu_Sounds";
            this.ContextMenu_Musics.Size = new System.Drawing.Size(137, 120);
            this.ContextMenu_Musics.Text = "ContextMenu_StreamSounds";
            // 
            // ContextMenuMusics_Rename
            // 
            this.ContextMenuMusics_Rename.Name = "ContextMenuMusics_Rename";
            this.ContextMenuMusics_Rename.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuMusics_Rename.Text = "Rename";
            this.ContextMenuMusics_Rename.Click += new System.EventHandler(this.ContextMenuMusics_Rename_Click);
            // 
            // ContextMenuMusics_Delete
            // 
            this.ContextMenuMusics_Delete.Name = "ContextMenuMusics_Delete";
            this.ContextMenuMusics_Delete.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuMusics_Delete.Text = "Delete";
            this.ContextMenuMusics_Delete.Click += new System.EventHandler(this.ContextMenuMusics_Delete_Click);
            // 
            // ContextMenuMusics_Properties
            // 
            this.ContextMenuMusics_Properties.Name = "ContextMenuMusics_Properties";
            this.ContextMenuMusics_Properties.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuMusics_Properties.Text = "Properties...";
            this.ContextMenuMusics_Properties.Click += new System.EventHandler(this.ContextMenuMusics_Properties_Click);
            // 
            // ContextMenuMusics_ExportESIF
            // 
            this.ContextMenuMusics_ExportESIF.Name = "ContextMenuMusics_ExportESIF";
            this.ContextMenuMusics_ExportESIF.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuMusics_ExportESIF.Text = "Export";
            this.ContextMenuMusics_ExportESIF.Click += new System.EventHandler(this.ContextMenuMusics_ExportESIF_Click);
            // 
            // ContextMenuMusics_Separator2
            // 
            this.ContextMenuMusics_Separator2.Name = "ContextMenuMusics_Separator2";
            this.ContextMenuMusics_Separator2.Size = new System.Drawing.Size(133, 6);
            // 
            // ContextMenuMusics_TextColor
            // 
            this.ContextMenuMusics_TextColor.Name = "ContextMenuMusics_TextColor";
            this.ContextMenuMusics_TextColor.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuMusics_TextColor.Text = "Text Color...";
            this.ContextMenuMusics_TextColor.Click += new System.EventHandler(this.ContextMenuMusics_TextColor_Click);
            // 
            // ContextMenu_Folders
            // 
            this.ContextMenu_Folders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuFolder_Folder,
            this.ContextMenuFolder_Separator4,
            this.ContextMenuFolder_AddSound,
            this.ContextMenuFolder_Rename,
            this.ContextMenuFolder_Separator3,
            this.ContextMenuFolder_TextColor});
            this.ContextMenu_Folders.Name = "contextMenuStrip1";
            this.ContextMenu_Folders.Size = new System.Drawing.Size(137, 104);
            this.ContextMenu_Folders.Text = "ContextMenu Folders";
            // 
            // ContextMenuFolder_Folder
            // 
            this.ContextMenuFolder_Folder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuFolder_NewFolder,
            this.ContextMenuFolder_Separator1,
            this.ContextMenuFolder_CollapseAll,
            this.ContextMenuFolder_ExpandAll,
            this.ContextMenuFolder_Separator2,
            this.ContextMenuFolder_Delete,
            this.ContextMenuFolder_Separator5,
            this.ContextMenuFolder_SortItems});
            this.ContextMenuFolder_Folder.Name = "ContextMenuFolder_Folder";
            this.ContextMenuFolder_Folder.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_Folder.Text = "Folder";
            // 
            // ContextMenuFolder_NewFolder
            // 
            this.ContextMenuFolder_NewFolder.Name = "ContextMenuFolder_NewFolder";
            this.ContextMenuFolder_NewFolder.Size = new System.Drawing.Size(158, 22);
            this.ContextMenuFolder_NewFolder.Text = "New";
            this.ContextMenuFolder_NewFolder.Click += new System.EventHandler(this.ContextMenuFolder_NewFolder_Click);
            // 
            // ContextMenuFolder_Separator1
            // 
            this.ContextMenuFolder_Separator1.Name = "ContextMenuFolder_Separator1";
            this.ContextMenuFolder_Separator1.Size = new System.Drawing.Size(155, 6);
            // 
            // ContextMenuFolder_CollapseAll
            // 
            this.ContextMenuFolder_CollapseAll.Name = "ContextMenuFolder_CollapseAll";
            this.ContextMenuFolder_CollapseAll.Size = new System.Drawing.Size(158, 22);
            this.ContextMenuFolder_CollapseAll.Text = "Collapse All";
            this.ContextMenuFolder_CollapseAll.Click += new System.EventHandler(this.ContextMenuFolder_CollapseAll_Click);
            // 
            // ContextMenuFolder_ExpandAll
            // 
            this.ContextMenuFolder_ExpandAll.Name = "ContextMenuFolder_ExpandAll";
            this.ContextMenuFolder_ExpandAll.Size = new System.Drawing.Size(158, 22);
            this.ContextMenuFolder_ExpandAll.Text = "Expand All";
            this.ContextMenuFolder_ExpandAll.Click += new System.EventHandler(this.ContextMenuFolder_ExpandAll_Click);
            // 
            // ContextMenuFolder_Separator2
            // 
            this.ContextMenuFolder_Separator2.Name = "ContextMenuFolder_Separator2";
            this.ContextMenuFolder_Separator2.Size = new System.Drawing.Size(155, 6);
            // 
            // ContextMenuFolder_Delete
            // 
            this.ContextMenuFolder_Delete.Name = "ContextMenuFolder_Delete";
            this.ContextMenuFolder_Delete.Size = new System.Drawing.Size(158, 22);
            this.ContextMenuFolder_Delete.Text = "Delete";
            this.ContextMenuFolder_Delete.Click += new System.EventHandler(this.ContextMenuFolder_Delete_Click);
            // 
            // ContextMenuFolder_Separator5
            // 
            this.ContextMenuFolder_Separator5.Name = "ContextMenuFolder_Separator5";
            this.ContextMenuFolder_Separator5.Size = new System.Drawing.Size(155, 6);
            // 
            // ContextMenuFolder_SortItems
            // 
            this.ContextMenuFolder_SortItems.Name = "ContextMenuFolder_SortItems";
            this.ContextMenuFolder_SortItems.Size = new System.Drawing.Size(158, 22);
            this.ContextMenuFolder_SortItems.Text = "Sort Child Items";
            this.ContextMenuFolder_SortItems.Click += new System.EventHandler(this.ContextMenuFolder_SortItems_Click);
            // 
            // ContextMenuFolder_Separator4
            // 
            this.ContextMenuFolder_Separator4.Name = "ContextMenuFolder_Separator4";
            this.ContextMenuFolder_Separator4.Size = new System.Drawing.Size(133, 6);
            // 
            // ContextMenuFolder_AddSound
            // 
            this.ContextMenuFolder_AddSound.Name = "ContextMenuFolder_AddSound";
            this.ContextMenuFolder_AddSound.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_AddSound.Text = "New";
            this.ContextMenuFolder_AddSound.Click += new System.EventHandler(this.ContextMenuFolder_AddSound_Click);
            // 
            // ContextMenuFolder_Rename
            // 
            this.ContextMenuFolder_Rename.Name = "ContextMenuFolder_Rename";
            this.ContextMenuFolder_Rename.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_Rename.Text = "Rename...";
            this.ContextMenuFolder_Rename.Click += new System.EventHandler(this.ContextMenuFolder_Rename_Click);
            // 
            // ContextMenuFolder_Separator3
            // 
            this.ContextMenuFolder_Separator3.Name = "ContextMenuFolder_Separator3";
            this.ContextMenuFolder_Separator3.Size = new System.Drawing.Size(133, 6);
            // 
            // ContextMenuFolder_TextColor
            // 
            this.ContextMenuFolder_TextColor.Name = "ContextMenuFolder_TextColor";
            this.ContextMenuFolder_TextColor.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_TextColor.Text = "Text Color...";
            this.ContextMenuFolder_TextColor.Click += new System.EventHandler(this.ContextMenuFolder_TextColor_Click);
            // 
            // SplitContainerMusicsForm
            // 
            this.SplitContainerMusicsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerMusicsForm.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerMusicsForm.Name = "SplitContainerMusicsForm";
            // 
            // SplitContainerMusicsForm.Panel1
            // 
            this.SplitContainerMusicsForm.Panel1.Controls.Add(this.TreeView_MusicData);
            // 
            // SplitContainerMusicsForm.Panel2
            // 
            this.SplitContainerMusicsForm.Panel2.Controls.Add(this.GroupBox_StreamData);
            this.SplitContainerMusicsForm.Panel2.Controls.Add(this.GroupBox_MusicHashcodes);
            this.SplitContainerMusicsForm.Panel2.Controls.Add(this.Button_UpdateIMAData);
            this.SplitContainerMusicsForm.Panel2.Controls.Add(this.Button_ExportInterchangeFile);
            this.SplitContainerMusicsForm.Size = new System.Drawing.Size(908, 591);
            this.SplitContainerMusicsForm.SplitterDistance = 456;
            this.SplitContainerMusicsForm.TabIndex = 0;
            // 
            // TreeView_MusicData
            // 
            this.TreeView_MusicData.AllowDrop = true;
            this.TreeView_MusicData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_MusicData.ImageIndex = 0;
            this.TreeView_MusicData.ImageList = this.ImageList_TreeNode;
            this.TreeView_MusicData.LabelEdit = true;
            this.TreeView_MusicData.Location = new System.Drawing.Point(0, 0);
            this.TreeView_MusicData.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TreeView_MusicData.Name = "TreeView_MusicData";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Musics";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Tag = "Root";
            treeNode1.Text = "Musics";
            this.TreeView_MusicData.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.TreeView_MusicData.SelectedImageIndex = 0;
            this.TreeView_MusicData.Size = new System.Drawing.Size(456, 591);
            this.TreeView_MusicData.TabIndex = 0;
            this.TreeView_MusicData.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_MusicData_AfterLabelEdit);
            this.TreeView_MusicData.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_MusicData_BeforeCollapse);
            this.TreeView_MusicData.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_MusicData_BeforeExpand);
            this.TreeView_MusicData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_MusicData_ItemDrag);
            this.TreeView_MusicData.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_MusicData_DragDrop);
            this.TreeView_MusicData.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_MusicData_DragEnter);
            this.TreeView_MusicData.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView_MusicData_DragOver);
            this.TreeView_MusicData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_MusicData_KeyDown);
            this.TreeView_MusicData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_MusicData_MouseClick);
            this.TreeView_MusicData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_MusicData_MouseDoubleClick);
            // 
            // GroupBox_StreamData
            // 
            this.GroupBox_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_StreamData.Controls.Add(this.Textbox_DataCount);
            this.GroupBox_StreamData.Controls.Add(this.Label_ItemsCountWav);
            this.GroupBox_StreamData.Controls.Add(this.Button_StopUpdate);
            this.GroupBox_StreamData.Controls.Add(this.ListView_WavHeaderData);
            this.GroupBox_StreamData.Controls.Add(this.Button_UpdateProperties);
            this.GroupBox_StreamData.Location = new System.Drawing.Point(3, 283);
            this.GroupBox_StreamData.Name = "GroupBox_StreamData";
            this.GroupBox_StreamData.Size = new System.Drawing.Size(442, 267);
            this.GroupBox_StreamData.TabIndex = 2;
            this.GroupBox_StreamData.TabStop = false;
            this.GroupBox_StreamData.Text = "Music Data:";
            // 
            // Textbox_DataCount
            // 
            this.Textbox_DataCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Textbox_DataCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_DataCount.Location = new System.Drawing.Point(74, 240);
            this.Textbox_DataCount.Name = "Textbox_DataCount";
            this.Textbox_DataCount.ReadOnly = true;
            this.Textbox_DataCount.Size = new System.Drawing.Size(100, 20);
            this.Textbox_DataCount.TabIndex = 4;
            this.Textbox_DataCount.Text = "0";
            // 
            // Label_ItemsCountWav
            // 
            this.Label_ItemsCountWav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_ItemsCountWav.AutoSize = true;
            this.Label_ItemsCountWav.ForeColor = System.Drawing.Color.ForestGreen;
            this.Label_ItemsCountWav.Location = new System.Drawing.Point(6, 243);
            this.Label_ItemsCountWav.Name = "Label_ItemsCountWav";
            this.Label_ItemsCountWav.Size = new System.Drawing.Size(62, 13);
            this.Label_ItemsCountWav.TabIndex = 3;
            this.Label_ItemsCountWav.Text = "Files Count:";
            // 
            // Button_StopUpdate
            // 
            this.Button_StopUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StopUpdate.Location = new System.Drawing.Point(361, 238);
            this.Button_StopUpdate.Name = "Button_StopUpdate";
            this.Button_StopUpdate.Size = new System.Drawing.Size(75, 23);
            this.Button_StopUpdate.TabIndex = 2;
            this.Button_StopUpdate.Text = "Stop/Clear";
            this.Button_StopUpdate.UseVisualStyleBackColor = true;
            this.Button_StopUpdate.Click += new System.EventHandler(this.Button_StopUpdate_Click);
            // 
            // ListView_WavHeaderData
            // 
            this.ListView_WavHeaderData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_WavHeaderData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_Frequency,
            this.Col_Channels,
            this.Col_Bits,
            this.Col_DataLenght,
            this.Col_Encoding,
            this.Col_Duration,
            this.Col_MarkersCount,
            this.Col_StartMarkerCount});
            this.ListView_WavHeaderData.FullRowSelect = true;
            this.ListView_WavHeaderData.GridLines = true;
            this.ListView_WavHeaderData.HideSelection = false;
            this.ListView_WavHeaderData.Location = new System.Drawing.Point(6, 19);
            this.ListView_WavHeaderData.Name = "ListView_WavHeaderData";
            this.ListView_WavHeaderData.Size = new System.Drawing.Size(430, 213);
            this.ListView_WavHeaderData.TabIndex = 0;
            this.ListView_WavHeaderData.UseCompatibleStateImageBehavior = false;
            this.ListView_WavHeaderData.View = System.Windows.Forms.View.Details;
            this.ListView_WavHeaderData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_WavHeaderData_MouseDoubleClick);
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            this.Col_Name.Width = 153;
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
            this.Col_Bits.Width = 38;
            // 
            // Col_DataLenght
            // 
            this.Col_DataLenght.Text = "Data Length";
            this.Col_DataLenght.Width = 74;
            // 
            // Col_Encoding
            // 
            this.Col_Encoding.Text = "Encoding";
            // 
            // Col_Duration
            // 
            this.Col_Duration.Text = "Duration";
            // 
            // Col_MarkersCount
            // 
            this.Col_MarkersCount.Text = "Marker Count";
            this.Col_MarkersCount.Width = 90;
            // 
            // Col_StartMarkerCount
            // 
            this.Col_StartMarkerCount.Text = "Start Marker Count";
            this.Col_StartMarkerCount.Width = 100;
            // 
            // Button_UpdateProperties
            // 
            this.Button_UpdateProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateProperties.Location = new System.Drawing.Point(280, 238);
            this.Button_UpdateProperties.Name = "Button_UpdateProperties";
            this.Button_UpdateProperties.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateProperties.TabIndex = 1;
            this.Button_UpdateProperties.Text = "Update";
            this.Button_UpdateProperties.UseVisualStyleBackColor = true;
            this.Button_UpdateProperties.Click += new System.EventHandler(this.Button_UpdateProperties_Click);
            // 
            // GroupBox_MusicHashcodes
            // 
            this.GroupBox_MusicHashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_MusicHashcodes.Controls.Add(this.Rtbx_Jump_Music_Codes);
            this.GroupBox_MusicHashcodes.Controls.Add(this.Button_Generate_Hashcodes);
            this.GroupBox_MusicHashcodes.Location = new System.Drawing.Point(3, 12);
            this.GroupBox_MusicHashcodes.Name = "GroupBox_MusicHashcodes";
            this.GroupBox_MusicHashcodes.Size = new System.Drawing.Size(442, 265);
            this.GroupBox_MusicHashcodes.TabIndex = 1;
            this.GroupBox_MusicHashcodes.TabStop = false;
            this.GroupBox_MusicHashcodes.Text = "Hashcodes:";
            // 
            // Rtbx_Jump_Music_Codes
            // 
            this.Rtbx_Jump_Music_Codes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Rtbx_Jump_Music_Codes.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Rtbx_Jump_Music_Codes.ContextMenuStrip = this.ContextMenu_RichTextBox;
            this.Rtbx_Jump_Music_Codes.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rtbx_Jump_Music_Codes.Location = new System.Drawing.Point(6, 19);
            this.Rtbx_Jump_Music_Codes.Name = "Rtbx_Jump_Music_Codes";
            this.Rtbx_Jump_Music_Codes.ReadOnly = true;
            this.Rtbx_Jump_Music_Codes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Rtbx_Jump_Music_Codes.Size = new System.Drawing.Size(427, 211);
            this.Rtbx_Jump_Music_Codes.TabIndex = 1;
            this.Rtbx_Jump_Music_Codes.Text = "";
            // 
            // ContextMenu_RichTextBox
            // 
            this.ContextMenu_RichTextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RichTextBox_Copy,
            this.RichTextBox_Separator1,
            this.RichTextBox_SelectAll,
            this.RichTextBox_Separator2});
            this.ContextMenu_RichTextBox.Name = "ContextMenu_RichTextBox";
            this.ContextMenu_RichTextBox.Size = new System.Drawing.Size(181, 82);
            // 
            // RichTextBox_Copy
            // 
            this.RichTextBox_Copy.Name = "RichTextBox_Copy";
            this.RichTextBox_Copy.Size = new System.Drawing.Size(180, 22);
            this.RichTextBox_Copy.Text = "Copy";
            this.RichTextBox_Copy.Click += new System.EventHandler(this.RichTextBox_Copy_Click);
            // 
            // RichTextBox_Separator1
            // 
            this.RichTextBox_Separator1.Name = "RichTextBox_Separator1";
            this.RichTextBox_Separator1.Size = new System.Drawing.Size(177, 6);
            // 
            // RichTextBox_SelectAll
            // 
            this.RichTextBox_SelectAll.Name = "RichTextBox_SelectAll";
            this.RichTextBox_SelectAll.Size = new System.Drawing.Size(180, 22);
            this.RichTextBox_SelectAll.Text = "Select All";
            this.RichTextBox_SelectAll.Click += new System.EventHandler(this.RichTextBox_SelectAll_Click);
            // 
            // RichTextBox_Separator2
            // 
            this.RichTextBox_Separator2.Name = "RichTextBox_Separator2";
            this.RichTextBox_Separator2.Size = new System.Drawing.Size(177, 6);
            // 
            // Button_Generate_Hashcodes
            // 
            this.Button_Generate_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Generate_Hashcodes.Location = new System.Drawing.Point(361, 236);
            this.Button_Generate_Hashcodes.Name = "Button_Generate_Hashcodes";
            this.Button_Generate_Hashcodes.Size = new System.Drawing.Size(75, 23);
            this.Button_Generate_Hashcodes.TabIndex = 2;
            this.Button_Generate_Hashcodes.Text = "Calculate";
            this.Button_Generate_Hashcodes.UseVisualStyleBackColor = true;
            this.Button_Generate_Hashcodes.Click += new System.EventHandler(this.Button_Generate_Hashcodes_Click);
            // 
            // Button_UpdateIMAData
            // 
            this.Button_UpdateIMAData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateIMAData.Location = new System.Drawing.Point(316, 556);
            this.Button_UpdateIMAData.Name = "Button_UpdateIMAData";
            this.Button_UpdateIMAData.Size = new System.Drawing.Size(120, 23);
            this.Button_UpdateIMAData.TabIndex = 4;
            this.Button_UpdateIMAData.Text = "Update ADPCM Data";
            this.Button_UpdateIMAData.UseVisualStyleBackColor = true;
            this.Button_UpdateIMAData.Click += new System.EventHandler(this.Button_UpdateIMAData_Click);
            // 
            // Button_ExportInterchangeFile
            // 
            this.Button_ExportInterchangeFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ExportInterchangeFile.Location = new System.Drawing.Point(184, 556);
            this.Button_ExportInterchangeFile.Name = "Button_ExportInterchangeFile";
            this.Button_ExportInterchangeFile.Size = new System.Drawing.Size(126, 23);
            this.Button_ExportInterchangeFile.TabIndex = 3;
            this.Button_ExportInterchangeFile.Text = "Export Interchange File";
            this.Button_ExportInterchangeFile.UseVisualStyleBackColor = true;
            this.Button_ExportInterchangeFile.Click += new System.EventHandler(this.Button_ExportInterchangeFile_Click);
            // 
            // Frm_Musics_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 591);
            this.Controls.Add(this.SplitContainerMusicsForm);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Frm_Musics_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound Musics Edior";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Musics_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Musics_Main_Load);
            this.Shown += new System.EventHandler(this.Frm_Musics_Main_Shown);
            this.SizeChanged += new System.EventHandler(this.Frm_Musics_Main_SizeChanged);
            this.Enter += new System.EventHandler(this.Frm_Musics_Main_Enter);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ContextMenu_Musics.ResumeLayout(false);
            this.ContextMenu_Folders.ResumeLayout(false);
            this.SplitContainerMusicsForm.Panel1.ResumeLayout(false);
            this.SplitContainerMusicsForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMusicsForm)).EndInit();
            this.SplitContainerMusicsForm.ResumeLayout(false);
            this.GroupBox_StreamData.ResumeLayout(false);
            this.GroupBox_StreamData.PerformLayout();
            this.GroupBox_MusicHashcodes.ResumeLayout(false);
            this.ContextMenu_RichTextBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Close;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator MenuItem_File_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Export;
        private System.Windows.Forms.ToolStripSeparator MenuItem_File_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Import;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadESIF;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadYml;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadMusic;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_FileProps;
        private System.Windows.Forms.ToolStripSeparator MenuItem_Edit_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_Undo;
        private System.Windows.Forms.ToolStripSeparator MenuItem_Edit_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_Search;
        private System.Windows.Forms.ImageList ImageList_TreeNode;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Musics;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMusics_Rename;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMusics_Delete;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMusics_Properties;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMusics_ExportESIF;
        private System.Windows.Forms.ToolStripSeparator ContextMenuMusics_Separator2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMusics_TextColor;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Folders;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Folder;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_CollapseAll;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_ExpandAll;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator4;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_AddSound;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Rename;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator3;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_TextColor;
        private System.Windows.Forms.SplitContainer SplitContainerMusicsForm;
        protected internal System.Windows.Forms.TreeView TreeView_MusicData;
        private System.Windows.Forms.GroupBox GroupBox_MusicHashcodes;
        private System.Windows.Forms.Button Button_Generate_Hashcodes;
        private System.Windows.Forms.Button Button_UpdateIMAData;
        private System.Windows.Forms.Button Button_ExportInterchangeFile;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_NewFolder;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator1;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Delete;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator5;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_SortItems;
        private System.Windows.Forms.GroupBox GroupBox_StreamData;
        private System.Windows.Forms.Button Button_StopUpdate;
        private CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick ListView_WavHeaderData;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Frequency;
        private System.Windows.Forms.ColumnHeader Col_Channels;
        private System.Windows.Forms.ColumnHeader Col_Bits;
        private System.Windows.Forms.ColumnHeader Col_DataLenght;
        private System.Windows.Forms.ColumnHeader Col_Encoding;
        private System.Windows.Forms.ColumnHeader Col_Duration;
        private System.Windows.Forms.ColumnHeader Col_MarkersCount;
        private System.Windows.Forms.ColumnHeader Col_StartMarkerCount;
        private System.Windows.Forms.Button Button_UpdateProperties;
        private System.Windows.Forms.TextBox Textbox_DataCount;
        private System.Windows.Forms.Label Label_ItemsCountWav;
        private System.Windows.Forms.RichTextBox Rtbx_Jump_Music_Codes;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_RichTextBox;
        private System.Windows.Forms.ToolStripMenuItem RichTextBox_Copy;
        private System.Windows.Forms.ToolStripSeparator RichTextBox_Separator1;
        private System.Windows.Forms.ToolStripMenuItem RichTextBox_SelectAll;
        private System.Windows.Forms.ToolStripSeparator RichTextBox_Separator2;
    }
}