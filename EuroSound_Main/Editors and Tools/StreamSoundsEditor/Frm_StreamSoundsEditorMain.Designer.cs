﻿
using EuroSound_Application.CustomControls.ListViewColumnSorting;

namespace EuroSound_Application.StreamSounds
{
    partial class Frm_StreamSoundsEditorMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StreamSoundsEditorMain));
            this.TreeView_StreamData = new System.Windows.Forms.TreeView();
            this.ImageList_TreeNode = new System.Windows.Forms.ImageList(this.components);
            this.ContextMenu_Folders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuFolder_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_CollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_AddSound = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_Separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuFolder_TextColor = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MenuItemFile_ReadSound = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_FileProps = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Edit_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Edit_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Edit_Search = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBox_StreamData = new System.Windows.Forms.GroupBox();
            this.Button_StopUpdate = new System.Windows.Forms.Button();
            this.Button_UpdateList_WavData = new System.Windows.Forms.Button();
            this.ContextMenu_Sounds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuSounds_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_ExportESIF = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSounds_MoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_MoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSounds_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.Button_UpdateIMAData = new System.Windows.Forms.Button();
            this.Button_ExportInterchangeFile = new System.Windows.Forms.Button();
            this.SplitContainerStreamSoundsForm = new System.Windows.Forms.SplitContainer();
            this.ListView_WavHeaderData = new EuroSound_Application.CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Channels = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Bits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_DataLenght = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Encoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkersCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_StartMarkerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_Folders.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.GroupBox_StreamData.SuspendLayout();
            this.ContextMenu_Sounds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerStreamSoundsForm)).BeginInit();
            this.SplitContainerStreamSoundsForm.Panel1.SuspendLayout();
            this.SplitContainerStreamSoundsForm.Panel2.SuspendLayout();
            this.SplitContainerStreamSoundsForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView_StreamData
            // 
            this.TreeView_StreamData.AllowDrop = true;
            this.TreeView_StreamData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_StreamData.ImageIndex = 0;
            this.TreeView_StreamData.ImageList = this.ImageList_TreeNode;
            this.TreeView_StreamData.LabelEdit = true;
            this.TreeView_StreamData.Location = new System.Drawing.Point(0, 0);
            this.TreeView_StreamData.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TreeView_StreamData.Name = "TreeView_StreamData";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "Sounds";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Tag = "Root";
            treeNode1.Text = "Sounds";
            this.TreeView_StreamData.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.TreeView_StreamData.SelectedImageIndex = 0;
            this.TreeView_StreamData.Size = new System.Drawing.Size(456, 567);
            this.TreeView_StreamData.TabIndex = 1;
            this.TreeView_StreamData.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_StreamData_AfterLabelEdit);
            this.TreeView_StreamData.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_StreamData_BeforeCollapse);
            this.TreeView_StreamData.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_StreamData_BeforeExpand);
            this.TreeView_StreamData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_StreamData_KeyDown);
            this.TreeView_StreamData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_StreamData_MouseClick);
            this.TreeView_StreamData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_StreamData_MouseDoubleClick);
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
            this.ContextMenuFolder_CollapseAll,
            this.ContextMenuFolder_ExpandAll});
            this.ContextMenuFolder_Folder.Name = "ContextMenuFolder_Folder";
            this.ContextMenuFolder_Folder.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_Folder.Text = "Folder";
            // 
            // ContextMenuFolder_CollapseAll
            // 
            this.ContextMenuFolder_CollapseAll.Name = "ContextMenuFolder_CollapseAll";
            this.ContextMenuFolder_CollapseAll.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_CollapseAll.Text = "Collapse All";
            this.ContextMenuFolder_CollapseAll.Click += new System.EventHandler(this.ContextMenuFolder_CollapseAll_Click);
            // 
            // ContextMenuFolder_ExpandAll
            // 
            this.ContextMenuFolder_ExpandAll.Name = "ContextMenuFolder_ExpandAll";
            this.ContextMenuFolder_ExpandAll.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_ExpandAll.Text = "Expand All";
            this.ContextMenuFolder_ExpandAll.Click += new System.EventHandler(this.ContextMenuFolder_ExpandAll_Click);
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
            this.ContextMenuFolder_AddSound.Text = "Add Sound";
            this.ContextMenuFolder_AddSound.Click += new System.EventHandler(this.ContextMenuMain_AddSound_Click);
            // 
            // ContextMenuFolder_Rename
            // 
            this.ContextMenuFolder_Rename.Name = "ContextMenuFolder_Rename";
            this.ContextMenuFolder_Rename.Size = new System.Drawing.Size(136, 22);
            this.ContextMenuFolder_Rename.Text = "Rename...";
            this.ContextMenuFolder_Rename.Click += new System.EventHandler(this.ContextMenuMain_Rename_Click);
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
            this.ContextMenuFolder_TextColor.Click += new System.EventHandler(this.ContextMenuMain_TextColor_Click);
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
            this.MainMenu.TabIndex = 0;
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
            this.MenuItem_File_Close.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_File_Close.Text = "Close";
            this.MenuItem_File_Close.Click += new System.EventHandler(this.MenuItem_File_Close_Click);
            // 
            // MenuItem_File_Save
            // 
            this.MenuItem_File_Save.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Save.MergeIndex = 3;
            this.MenuItem_File_Save.Name = "MenuItem_File_Save";
            this.MenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuItem_File_Save.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_File_Save.Text = "Save";
            this.MenuItem_File_Save.Click += new System.EventHandler(this.MenuItem_File_Save_Click);
            // 
            // MenuItem_File_SaveAs
            // 
            this.MenuItem_File_SaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_SaveAs.MergeIndex = 4;
            this.MenuItem_File_SaveAs.Name = "MenuItem_File_SaveAs";
            this.MenuItem_File_SaveAs.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_File_SaveAs.Text = "Save As...";
            this.MenuItem_File_SaveAs.Click += new System.EventHandler(this.MenuItem_File_SaveAs_Click);
            // 
            // MenuItem_File_Separator1
            // 
            this.MenuItem_File_Separator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Separator1.MergeIndex = 5;
            this.MenuItem_File_Separator1.Name = "MenuItem_File_Separator1";
            this.MenuItem_File_Separator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItem_File_Export
            // 
            this.MenuItem_File_Export.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Export.MergeIndex = 6;
            this.MenuItem_File_Export.Name = "MenuItem_File_Export";
            this.MenuItem_File_Export.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_File_Export.Text = "Export";
            this.MenuItem_File_Export.Click += new System.EventHandler(this.MenuItem_File_Export_Click);
            // 
            // MenuItem_File_Separator2
            // 
            this.MenuItem_File_Separator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_Separator2.MergeIndex = 7;
            this.MenuItem_File_Separator2.Name = "MenuItem_File_Separator2";
            this.MenuItem_File_Separator2.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItemFile_Import
            // 
            this.MenuItemFile_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_ReadESIF,
            this.MenuItemFile_ReadYml,
            this.MenuItemFile_ReadSound});
            this.MenuItemFile_Import.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_Import.MergeIndex = 8;
            this.MenuItemFile_Import.Name = "MenuItemFile_Import";
            this.MenuItemFile_Import.Size = new System.Drawing.Size(180, 22);
            this.MenuItemFile_Import.Text = "Import...";
            // 
            // MenuItemFile_ReadESIF
            // 
            this.MenuItemFile_ReadESIF.Name = "MenuItemFile_ReadESIF";
            this.MenuItemFile_ReadESIF.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_ReadESIF.Text = "Import ESIF";
            this.MenuItemFile_ReadESIF.Click += new System.EventHandler(this.MenuItemFile_ReadESIF_Click);
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
            this.MenuItem_Edit_FileProps.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_Edit_FileProps.Text = "File Properties";
            this.MenuItem_Edit_FileProps.Click += new System.EventHandler(this.MenuItem_Edit_FileProps_Click);
            // 
            // MenuItem_Edit_Separator1
            // 
            this.MenuItem_Edit_Separator1.Name = "MenuItem_Edit_Separator1";
            this.MenuItem_Edit_Separator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItem_Edit_Undo
            // 
            this.MenuItem_Edit_Undo.Enabled = false;
            this.MenuItem_Edit_Undo.Name = "MenuItem_Edit_Undo";
            this.MenuItem_Edit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuItem_Edit_Undo.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_Edit_Undo.Text = "Undo";
            this.MenuItem_Edit_Undo.Click += new System.EventHandler(this.MenuItem_Edit_Undo_Click);
            // 
            // MenuItem_Edit_Separator2
            // 
            this.MenuItem_Edit_Separator2.Name = "MenuItem_Edit_Separator2";
            this.MenuItem_Edit_Separator2.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItem_Edit_Search
            // 
            this.MenuItem_Edit_Search.Name = "MenuItem_Edit_Search";
            this.MenuItem_Edit_Search.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MenuItem_Edit_Search.Size = new System.Drawing.Size(180, 22);
            this.MenuItem_Edit_Search.Text = "Search...";
            this.MenuItem_Edit_Search.Click += new System.EventHandler(this.MenuItem_Edit_Search_Click);
            // 
            // GroupBox_StreamData
            // 
            this.GroupBox_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_StreamData.Controls.Add(this.Button_StopUpdate);
            this.GroupBox_StreamData.Controls.Add(this.ListView_WavHeaderData);
            this.GroupBox_StreamData.Controls.Add(this.Button_UpdateList_WavData);
            this.GroupBox_StreamData.Location = new System.Drawing.Point(3, 12);
            this.GroupBox_StreamData.Name = "GroupBox_StreamData";
            this.GroupBox_StreamData.Size = new System.Drawing.Size(442, 512);
            this.GroupBox_StreamData.TabIndex = 2;
            this.GroupBox_StreamData.TabStop = false;
            this.GroupBox_StreamData.Text = "Stream Data:";
            // 
            // Button_StopUpdate
            // 
            this.Button_StopUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StopUpdate.Location = new System.Drawing.Point(361, 483);
            this.Button_StopUpdate.Name = "Button_StopUpdate";
            this.Button_StopUpdate.Size = new System.Drawing.Size(75, 23);
            this.Button_StopUpdate.TabIndex = 2;
            this.Button_StopUpdate.Text = "Stop/Clear";
            this.Button_StopUpdate.UseVisualStyleBackColor = true;
            this.Button_StopUpdate.Click += new System.EventHandler(this.Button_StopUpdate_Click);
            // 
            // Button_UpdateList_WavData
            // 
            this.Button_UpdateList_WavData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_WavData.Location = new System.Drawing.Point(280, 483);
            this.Button_UpdateList_WavData.Name = "Button_UpdateList_WavData";
            this.Button_UpdateList_WavData.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_WavData.TabIndex = 1;
            this.Button_UpdateList_WavData.Text = "Update";
            this.Button_UpdateList_WavData.UseVisualStyleBackColor = true;
            this.Button_UpdateList_WavData.Click += new System.EventHandler(this.Button_UpdateList_WavData_Click);
            // 
            // ContextMenu_Sounds
            // 
            this.ContextMenu_Sounds.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuSounds_Rename,
            this.ContextMenuSounds_Delete,
            this.ContextMenuSounds_Properties,
            this.ContextMenuSounds_ExportESIF,
            this.ContextMenuSounds_Separator1,
            this.ContextMenuSounds_MoveUp,
            this.ContextMenuSounds_MoveDown,
            this.ContextMenuSounds_Separator2,
            this.ContextMenuSounds_TextColor});
            this.ContextMenu_Sounds.Name = "ContextMenu_Sounds";
            this.ContextMenu_Sounds.Size = new System.Drawing.Size(139, 170);
            this.ContextMenu_Sounds.Text = "ContextMenu_StreamSounds";
            // 
            // ContextMenuSounds_Rename
            // 
            this.ContextMenuSounds_Rename.Name = "ContextMenuSounds_Rename";
            this.ContextMenuSounds_Rename.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_Rename.Text = "Rename";
            this.ContextMenuSounds_Rename.Click += new System.EventHandler(this.ContextMenuSounds_Rename_Click);
            // 
            // ContextMenuSounds_Delete
            // 
            this.ContextMenuSounds_Delete.Name = "ContextMenuSounds_Delete";
            this.ContextMenuSounds_Delete.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_Delete.Text = "Delete";
            this.ContextMenuSounds_Delete.Click += new System.EventHandler(this.ContextMenuSounds_Delete_Click);
            // 
            // ContextMenuSounds_Properties
            // 
            this.ContextMenuSounds_Properties.Name = "ContextMenuSounds_Properties";
            this.ContextMenuSounds_Properties.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_Properties.Text = "Properties...";
            this.ContextMenuSounds_Properties.Click += new System.EventHandler(this.ContextMenuSounds_Properties_Click);
            // 
            // ContextMenuSounds_ExportESIF
            // 
            this.ContextMenuSounds_ExportESIF.Name = "ContextMenuSounds_ExportESIF";
            this.ContextMenuSounds_ExportESIF.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_ExportESIF.Text = "Export";
            this.ContextMenuSounds_ExportESIF.Click += new System.EventHandler(this.ContextMenuSounds_ExportESIF_Click);
            // 
            // ContextMenuSounds_Separator1
            // 
            this.ContextMenuSounds_Separator1.Name = "ContextMenuSounds_Separator1";
            this.ContextMenuSounds_Separator1.Size = new System.Drawing.Size(135, 6);
            // 
            // ContextMenuSounds_MoveUp
            // 
            this.ContextMenuSounds_MoveUp.Name = "ContextMenuSounds_MoveUp";
            this.ContextMenuSounds_MoveUp.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_MoveUp.Text = "Move Up";
            this.ContextMenuSounds_MoveUp.Click += new System.EventHandler(this.ContextMenuSounds_MoveUp_Click);
            // 
            // ContextMenuSounds_MoveDown
            // 
            this.ContextMenuSounds_MoveDown.Name = "ContextMenuSounds_MoveDown";
            this.ContextMenuSounds_MoveDown.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_MoveDown.Text = "Move Down";
            this.ContextMenuSounds_MoveDown.Click += new System.EventHandler(this.ContextMenuSounds_MoveDown_Click);
            // 
            // ContextMenuSounds_Separator2
            // 
            this.ContextMenuSounds_Separator2.Name = "ContextMenuSounds_Separator2";
            this.ContextMenuSounds_Separator2.Size = new System.Drawing.Size(135, 6);
            // 
            // ContextMenuSounds_TextColor
            // 
            this.ContextMenuSounds_TextColor.Name = "ContextMenuSounds_TextColor";
            this.ContextMenuSounds_TextColor.Size = new System.Drawing.Size(138, 22);
            this.ContextMenuSounds_TextColor.Text = "Text Color...";
            this.ContextMenuSounds_TextColor.Click += new System.EventHandler(this.ContextMenuSounds_TextColor_Click);
            // 
            // Button_UpdateIMAData
            // 
            this.Button_UpdateIMAData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateIMAData.Location = new System.Drawing.Point(316, 532);
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
            this.Button_ExportInterchangeFile.Location = new System.Drawing.Point(184, 532);
            this.Button_ExportInterchangeFile.Name = "Button_ExportInterchangeFile";
            this.Button_ExportInterchangeFile.Size = new System.Drawing.Size(126, 23);
            this.Button_ExportInterchangeFile.TabIndex = 3;
            this.Button_ExportInterchangeFile.Text = "Export Interchange File";
            this.Button_ExportInterchangeFile.UseVisualStyleBackColor = true;
            this.Button_ExportInterchangeFile.Click += new System.EventHandler(this.Button_ExportInterchangeFile_Click);
            // 
            // SplitContainerStreamSoundsForm
            // 
            this.SplitContainerStreamSoundsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerStreamSoundsForm.Location = new System.Drawing.Point(0, 24);
            this.SplitContainerStreamSoundsForm.Name = "SplitContainerStreamSoundsForm";
            // 
            // SplitContainerStreamSoundsForm.Panel1
            // 
            this.SplitContainerStreamSoundsForm.Panel1.Controls.Add(this.TreeView_StreamData);
            // 
            // SplitContainerStreamSoundsForm.Panel2
            // 
            this.SplitContainerStreamSoundsForm.Panel2.Controls.Add(this.GroupBox_StreamData);
            this.SplitContainerStreamSoundsForm.Panel2.Controls.Add(this.Button_UpdateIMAData);
            this.SplitContainerStreamSoundsForm.Panel2.Controls.Add(this.Button_ExportInterchangeFile);
            this.SplitContainerStreamSoundsForm.Size = new System.Drawing.Size(908, 567);
            this.SplitContainerStreamSoundsForm.SplitterDistance = 456;
            this.SplitContainerStreamSoundsForm.TabIndex = 5;
            // 
            // ListView_WavHeaderData
            // 
            this.ListView_WavHeaderData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_WavHeaderData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_Index,
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
            this.ListView_WavHeaderData.Size = new System.Drawing.Size(430, 458);
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
            // Col_Index
            // 
            this.Col_Index.Text = "Index";
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
            // Frm_StreamSoundsEditorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 591);
            this.Controls.Add(this.SplitContainerStreamSoundsForm);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Frm_StreamSoundsEditorMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound StreamSounds Edior";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_StreamSoundsEditorMain_FormClosing);
            this.Load += new System.EventHandler(this.Frm_StreamSoundsEditorMain_Load);
            this.Shown += new System.EventHandler(this.Frm_StreamSoundsEditorMain_Shown);
            this.SizeChanged += new System.EventHandler(this.Frm_StreamSoundsEditorMain_SizeChanged);
            this.Enter += new System.EventHandler(this.Frm_StreamSoundsEditorMain_Enter);
            this.ContextMenu_Folders.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.GroupBox_StreamData.ResumeLayout(false);
            this.ContextMenu_Sounds.ResumeLayout(false);
            this.SplitContainerStreamSoundsForm.Panel1.ResumeLayout(false);
            this.SplitContainerStreamSoundsForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerStreamSoundsForm)).EndInit();
            this.SplitContainerStreamSoundsForm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.TreeView TreeView_StreamData;
        private System.Windows.Forms.ImageList ImageList_TreeNode;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator MenuItem_File_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Export;
        private System.Windows.Forms.ToolStripSeparator MenuItem_File_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_FileProps;
        private System.Windows.Forms.GroupBox GroupBox_StreamData;
        private ListView_ColumnSortingClick ListView_WavHeaderData;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Frequency;
        private System.Windows.Forms.ColumnHeader Col_Channels;
        private System.Windows.Forms.ColumnHeader Col_Bits;
        private System.Windows.Forms.ColumnHeader Col_DataLenght;
        private System.Windows.Forms.ColumnHeader Col_Encoding;
        private System.Windows.Forms.ColumnHeader Col_Duration;
        private System.Windows.Forms.Button Button_UpdateList_WavData;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Folders;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_AddSound;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Rename;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator3;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_TextColor;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_Folder;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_CollapseAll;
        private System.Windows.Forms.ToolStripSeparator ContextMenuFolder_Separator4;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_ExpandAll;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Sounds;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_Rename;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_Properties;
        private System.Windows.Forms.ToolStripSeparator ContextMenuSounds_Separator1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_TextColor;
        private System.Windows.Forms.ToolStripSeparator ContextMenuSounds_Separator2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_MoveUp;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_MoveDown;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_Delete;
        private System.Windows.Forms.Button Button_UpdateIMAData;
        private System.Windows.Forms.ColumnHeader Col_Index;
        private System.Windows.Forms.ColumnHeader Col_MarkersCount;
        private System.Windows.Forms.ColumnHeader Col_StartMarkerCount;
        private System.Windows.Forms.ToolStripSeparator MenuItem_Edit_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_Search;
        private System.Windows.Forms.ToolStripSeparator MenuItem_Edit_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_Undo;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Close;
        private System.Windows.Forms.Button Button_StopUpdate;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Import;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadESIF;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadYml;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadSound;
        private System.Windows.Forms.Button Button_ExportInterchangeFile;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_ExportESIF;
        private System.Windows.Forms.SplitContainer SplitContainerStreamSoundsForm;
    }
}