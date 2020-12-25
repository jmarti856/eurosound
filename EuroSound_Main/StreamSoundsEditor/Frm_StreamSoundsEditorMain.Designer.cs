
namespace EuroSound_Application
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
            this.ContextMenuMain_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_CollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuFolder_ExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuMain_AddSound = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMain_DeleteSound = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMain_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuMain_TextColor = new System.Windows.Forms.ToolStripMenuItem();
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
            this.GroupBox_StreamData = new System.Windows.Forms.GroupBox();
            this.ListView_WavHeaderData = new ListViewExtendedMethods.ListView_ColumnSortingClick();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Frequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Channels = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Bits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Encoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_UpdateList_WavData = new System.Windows.Forms.Button();
            this.ContextMenu_Sounds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuSounds_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuSounds_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuSounds_TextColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Folders.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.GroupBox_StreamData.SuspendLayout();
            this.ContextMenu_Sounds.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView_StreamData
            // 
            this.TreeView_StreamData.AllowDrop = true;
            this.TreeView_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.TreeView_StreamData.Size = new System.Drawing.Size(494, 740);
            this.TreeView_StreamData.TabIndex = 1;
            this.TreeView_StreamData.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_StreamData_AfterLabelEdit);
            this.TreeView_StreamData.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_StreamData_BeforeCollapse);
            this.TreeView_StreamData.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView_StreamData_BeforeExpand);
            this.TreeView_StreamData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_StreamData_ItemDrag);
            this.TreeView_StreamData.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_StreamData_DragDrop);
            this.TreeView_StreamData.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView_StreamData_DragEnter);
            this.TreeView_StreamData.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeView_StreamData_DragOver);
            this.TreeView_StreamData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_StreamData_KeyDown);
            this.TreeView_StreamData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_StreamData_MouseClick);
            this.TreeView_StreamData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_StreamData_MouseDoubleClick);
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
            this.ContextMenuMain_Folder,
            this.toolStripSeparator4,
            this.ContextMenuMain_AddSound,
            this.ContextMenuMain_DeleteSound,
            this.ContextMenuMain_Rename,
            this.toolStripSeparator3,
            this.ContextMenuMain_TextColor});
            this.ContextMenu_Folders.Name = "contextMenuStrip1";
            this.ContextMenu_Folders.Size = new System.Drawing.Size(145, 126);
            this.ContextMenu_Folders.Text = "ContextMenu Folders";
            // 
            // ContextMenuMain_Folder
            // 
            this.ContextMenuMain_Folder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuFolder_CollapseAll,
            this.ContextMenuFolder_ExpandAll});
            this.ContextMenuMain_Folder.Name = "ContextMenuMain_Folder";
            this.ContextMenuMain_Folder.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuMain_Folder.Text = "Folder";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // ContextMenuMain_AddSound
            // 
            this.ContextMenuMain_AddSound.Name = "ContextMenuMain_AddSound";
            this.ContextMenuMain_AddSound.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuMain_AddSound.Text = "Add Sound";
            this.ContextMenuMain_AddSound.Click += new System.EventHandler(this.ContextMenuMain_AddSound_Click);
            // 
            // ContextMenuMain_DeleteSound
            // 
            this.ContextMenuMain_DeleteSound.Name = "ContextMenuMain_DeleteSound";
            this.ContextMenuMain_DeleteSound.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuMain_DeleteSound.Text = "Delete Sound";
            this.ContextMenuMain_DeleteSound.Click += new System.EventHandler(this.ContextMenuMain_DeleteSound_Click);
            // 
            // ContextMenuMain_Rename
            // 
            this.ContextMenuMain_Rename.Name = "ContextMenuMain_Rename";
            this.ContextMenuMain_Rename.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuMain_Rename.Text = "Rename...";
            this.ContextMenuMain_Rename.Click += new System.EventHandler(this.ContextMenuMain_Rename_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // ContextMenuMain_TextColor
            // 
            this.ContextMenuMain_TextColor.Name = "ContextMenuMain_TextColor";
            this.ContextMenuMain_TextColor.Size = new System.Drawing.Size(144, 22);
            this.ContextMenuMain_TextColor.Text = "Text Color...";
            this.ContextMenuMain_TextColor.Click += new System.EventHandler(this.ContextMenuMain_TextColor_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File,
            this.MenuItem_Edit});
            this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(981, 24);
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
            // 
            // MenuItem_File_SaveAs
            // 
            this.MenuItem_File_SaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItem_File_SaveAs.MergeIndex = 3;
            this.MenuItem_File_SaveAs.Name = "MenuItem_File_SaveAs";
            this.MenuItem_File_SaveAs.Size = new System.Drawing.Size(207, 22);
            this.MenuItem_File_SaveAs.Text = "Save As...";
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
            // 
            // MenuItemFile_ReadSound
            // 
            this.MenuItemFile_ReadSound.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_ReadSound.MergeIndex = 8;
            this.MenuItemFile_ReadSound.Name = "MenuItemFile_ReadSound";
            this.MenuItemFile_ReadSound.Size = new System.Drawing.Size(207, 22);
            this.MenuItemFile_ReadSound.Text = "Read Sound (.yml)";
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
            // 
            // GroupBox_StreamData
            // 
            this.GroupBox_StreamData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_StreamData.Controls.Add(this.ListView_WavHeaderData);
            this.GroupBox_StreamData.Controls.Add(this.Button_UpdateList_WavData);
            this.GroupBox_StreamData.Location = new System.Drawing.Point(500, 12);
            this.GroupBox_StreamData.Name = "GroupBox_StreamData";
            this.GroupBox_StreamData.Size = new System.Drawing.Size(469, 316);
            this.GroupBox_StreamData.TabIndex = 2;
            this.GroupBox_StreamData.TabStop = false;
            this.GroupBox_StreamData.Text = "Stream Data";
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
            this.Col_Data,
            this.Col_Encoding,
            this.Col_Duration});
            this.ListView_WavHeaderData.FullRowSelect = true;
            this.ListView_WavHeaderData.GridLines = true;
            this.ListView_WavHeaderData.HideSelection = false;
            this.ListView_WavHeaderData.Location = new System.Drawing.Point(6, 19);
            this.ListView_WavHeaderData.Name = "ListView_WavHeaderData";
            this.ListView_WavHeaderData.Size = new System.Drawing.Size(457, 262);
            this.ListView_WavHeaderData.TabIndex = 0;
            this.ListView_WavHeaderData.UseCompatibleStateImageBehavior = false;
            this.ListView_WavHeaderData.View = System.Windows.Forms.View.Details;
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            this.Col_Name.Width = 166;
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
            // Button_UpdateList_WavData
            // 
            this.Button_UpdateList_WavData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_UpdateList_WavData.Location = new System.Drawing.Point(388, 287);
            this.Button_UpdateList_WavData.Name = "Button_UpdateList_WavData";
            this.Button_UpdateList_WavData.Size = new System.Drawing.Size(75, 23);
            this.Button_UpdateList_WavData.TabIndex = 1;
            this.Button_UpdateList_WavData.Text = "Update";
            this.Button_UpdateList_WavData.UseVisualStyleBackColor = true;
            // 
            // ContextMenu_Sounds
            // 
            this.ContextMenu_Sounds.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuSounds_Rename,
            this.ContextMenuSounds_Properties,
            this.toolStripSeparator5,
            this.ContextMenuSounds_TextColor});
            this.ContextMenu_Sounds.Name = "ContextMenu_Sounds";
            this.ContextMenu_Sounds.Size = new System.Drawing.Size(181, 98);
            this.ContextMenu_Sounds.Text = "ContextMenu_StreamSounds";
            // 
            // ContextMenuSounds_Rename
            // 
            this.ContextMenuSounds_Rename.Name = "ContextMenuSounds_Rename";
            this.ContextMenuSounds_Rename.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuSounds_Rename.Text = "Rename";
            // 
            // ContextMenuSounds_Properties
            // 
            this.ContextMenuSounds_Properties.Name = "ContextMenuSounds_Properties";
            this.ContextMenuSounds_Properties.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuSounds_Properties.Text = "Properties...";
            this.ContextMenuSounds_Properties.Click += new System.EventHandler(this.ContextMenuSounds_Properties_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // ContextMenuSounds_TextColor
            // 
            this.ContextMenuSounds_TextColor.Name = "ContextMenuSounds_TextColor";
            this.ContextMenuSounds_TextColor.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuSounds_TextColor.Text = "Text Color...";
            // 
            // Frm_StreamSoundsEditorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 740);
            this.Controls.Add(this.GroupBox_StreamData);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.TreeView_StreamData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Frm_StreamSoundsEditorMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_StreamSoundsEditorMain";
            this.Load += new System.EventHandler(this.Frm_StreamSoundsEditorMain_Load);
            this.Shown += new System.EventHandler(this.Frm_StreamSoundsEditorMain_Shown);
            this.ContextMenu_Folders.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.GroupBox_StreamData.ResumeLayout(false);
            this.ContextMenu_Sounds.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadYml;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ReadSound;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit_FileProps;
        private System.Windows.Forms.GroupBox GroupBox_StreamData;
        private ListViewExtendedMethods.ListView_ColumnSortingClick ListView_WavHeaderData;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Frequency;
        private System.Windows.Forms.ColumnHeader Col_Channels;
        private System.Windows.Forms.ColumnHeader Col_Bits;
        private System.Windows.Forms.ColumnHeader Col_Data;
        private System.Windows.Forms.ColumnHeader Col_Encoding;
        private System.Windows.Forms.ColumnHeader Col_Duration;
        private System.Windows.Forms.Button Button_UpdateList_WavData;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Folders;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMain_AddSound;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMain_DeleteSound;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMain_Rename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMain_TextColor;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuMain_Folder;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_CollapseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuFolder_ExpandAll;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_Sounds;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_Rename;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_Properties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSounds_TextColor;
    }
}