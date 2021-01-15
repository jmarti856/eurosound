
using EuroSound_Application.CustomControls.ListViewColumnSorting;

namespace EuroSound_Application.AudioConverter
{
    partial class Frm_AudioConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AudioConverter));
            this.Groupbox_OutputDirectory = new System.Windows.Forms.GroupBox();
            this.Button_SearchOutputFolder = new System.Windows.Forms.Button();
            this.Textbox_OutputFolder = new System.Windows.Forms.TextBox();
            this.Label_OutputPath = new System.Windows.Forms.Label();
            this.AudioConverter_MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_ImportFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_ImportFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEdit_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEdit_SelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEdit_InvertSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.convertPreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Groupbox_Files = new System.Windows.Forms.GroupBox();
            this.Button_ClearList = new System.Windows.Forms.Button();
            this.Textbox_ItemsCount = new System.Windows.Forms.TextBox();
            this.Label_ItemsCount = new System.Windows.Forms.Label();
            this.ListView_ItemsToConvert = new EuroSound_Application.CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_FilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_FileExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_FileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_ListItemsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_InvertSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.Groupbox_ConvertOptions = new System.Windows.Forms.GroupBox();
            this.RadioButton_Stereo = new System.Windows.Forms.RadioButton();
            this.RadioButton_Mono = new System.Windows.Forms.RadioButton();
            this.Label_Channels = new System.Windows.Forms.Label();
            this.ComboBox_Bits = new System.Windows.Forms.ComboBox();
            this.Label_Bits = new System.Windows.Forms.Label();
            this.Combobox_Rate = new System.Windows.Forms.ComboBox();
            this.Label_Rate = new System.Windows.Forms.Label();
            this.Button_Start = new System.Windows.Forms.Button();
            this.ProgressBar_Status = new System.Windows.Forms.ProgressBar();
            this.Label_Status = new System.Windows.Forms.Label();
            this.Background_ConvertAudios = new System.ComponentModel.BackgroundWorker();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Groupbox_OutputDirectory.SuspendLayout();
            this.AudioConverter_MainMenu.SuspendLayout();
            this.Groupbox_Files.SuspendLayout();
            this.ContextMenu_ListItemsOptions.SuspendLayout();
            this.Groupbox_ConvertOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // Groupbox_OutputDirectory
            // 
            this.Groupbox_OutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_OutputDirectory.Controls.Add(this.Button_SearchOutputFolder);
            this.Groupbox_OutputDirectory.Controls.Add(this.Textbox_OutputFolder);
            this.Groupbox_OutputDirectory.Controls.Add(this.Label_OutputPath);
            this.Groupbox_OutputDirectory.Location = new System.Drawing.Point(12, 584);
            this.Groupbox_OutputDirectory.Name = "Groupbox_OutputDirectory";
            this.Groupbox_OutputDirectory.Size = new System.Drawing.Size(479, 71);
            this.Groupbox_OutputDirectory.TabIndex = 1;
            this.Groupbox_OutputDirectory.TabStop = false;
            this.Groupbox_OutputDirectory.Text = "Output Directory";
            // 
            // Button_SearchOutputFolder
            // 
            this.Button_SearchOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SearchOutputFolder.Location = new System.Drawing.Point(449, 19);
            this.Button_SearchOutputFolder.Name = "Button_SearchOutputFolder";
            this.Button_SearchOutputFolder.Size = new System.Drawing.Size(24, 20);
            this.Button_SearchOutputFolder.TabIndex = 2;
            this.Button_SearchOutputFolder.Text = "...";
            this.Button_SearchOutputFolder.UseVisualStyleBackColor = true;
            this.Button_SearchOutputFolder.Click += new System.EventHandler(this.Button_SearchOutputFolder_Click);
            // 
            // Textbox_OutputFolder
            // 
            this.Textbox_OutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_OutputFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_OutputFolder.Location = new System.Drawing.Point(44, 19);
            this.Textbox_OutputFolder.Name = "Textbox_OutputFolder";
            this.Textbox_OutputFolder.ReadOnly = true;
            this.Textbox_OutputFolder.Size = new System.Drawing.Size(399, 20);
            this.Textbox_OutputFolder.TabIndex = 1;
            // 
            // Label_OutputPath
            // 
            this.Label_OutputPath.AutoSize = true;
            this.Label_OutputPath.Location = new System.Drawing.Point(6, 22);
            this.Label_OutputPath.Name = "Label_OutputPath";
            this.Label_OutputPath.Size = new System.Drawing.Size(32, 13);
            this.Label_OutputPath.TabIndex = 0;
            this.Label_OutputPath.Text = "Path:";
            // 
            // AudioConverter_MainMenu
            // 
            this.AudioConverter_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File,
            this.MainMenu_Edit,
            this.MainMenu_Load});
            this.AudioConverter_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.AudioConverter_MainMenu.Name = "AudioConverter_MainMenu";
            this.AudioConverter_MainMenu.Size = new System.Drawing.Size(887, 24);
            this.AudioConverter_MainMenu.TabIndex = 0;
            this.AudioConverter_MainMenu.Text = "Main Menu";
            this.AudioConverter_MainMenu.Visible = false;
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_Separator1,
            this.MenuItemFile_ImportFolders,
            this.MenuItemFile_ImportFiles});
            this.MainMenu_File.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.MainMenu_File.MergeIndex = 0;
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MenuItemFile_Separator1
            // 
            this.MenuItemFile_Separator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_Separator1.MergeIndex = 2;
            this.MenuItemFile_Separator1.Name = "MenuItemFile_Separator1";
            this.MenuItemFile_Separator1.Size = new System.Drawing.Size(142, 6);
            // 
            // MenuItemFile_ImportFolders
            // 
            this.MenuItemFile_ImportFolders.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_ImportFolders.MergeIndex = 3;
            this.MenuItemFile_ImportFolders.Name = "MenuItemFile_ImportFolders";
            this.MenuItemFile_ImportFolders.Size = new System.Drawing.Size(145, 22);
            this.MenuItemFile_ImportFolders.Text = "Load Folder...";
            this.MenuItemFile_ImportFolders.Click += new System.EventHandler(this.MenuItemFile_ImportFolders_Click);
            // 
            // MenuItemFile_ImportFiles
            // 
            this.MenuItemFile_ImportFiles.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MenuItemFile_ImportFiles.MergeIndex = 4;
            this.MenuItemFile_ImportFiles.Name = "MenuItemFile_ImportFiles";
            this.MenuItemFile_ImportFiles.Size = new System.Drawing.Size(145, 22);
            this.MenuItemFile_ImportFiles.Text = "Import Files...";
            this.MenuItemFile_ImportFiles.Click += new System.EventHandler(this.MenuItemFile_ImportFiles_Click);
            // 
            // MainMenu_Edit
            // 
            this.MainMenu_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemEdit_SelectAll,
            this.MenuItemEdit_SelectNone,
            this.MenuItemEdit_InvertSelection});
            this.MainMenu_Edit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MainMenu_Edit.MergeIndex = 1;
            this.MainMenu_Edit.Name = "MainMenu_Edit";
            this.MainMenu_Edit.Size = new System.Drawing.Size(39, 20);
            this.MainMenu_Edit.Text = "Edit";
            // 
            // MenuItemEdit_SelectAll
            // 
            this.MenuItemEdit_SelectAll.Name = "MenuItemEdit_SelectAll";
            this.MenuItemEdit_SelectAll.Size = new System.Drawing.Size(155, 22);
            this.MenuItemEdit_SelectAll.Text = "Select All";
            this.MenuItemEdit_SelectAll.Click += new System.EventHandler(this.MenuItemEdit_SelectAll_Click);
            // 
            // MenuItemEdit_SelectNone
            // 
            this.MenuItemEdit_SelectNone.Name = "MenuItemEdit_SelectNone";
            this.MenuItemEdit_SelectNone.Size = new System.Drawing.Size(155, 22);
            this.MenuItemEdit_SelectNone.Text = "Select None";
            this.MenuItemEdit_SelectNone.Click += new System.EventHandler(this.MenuItemEdit_SelectNone_Click);
            // 
            // MenuItemEdit_InvertSelection
            // 
            this.MenuItemEdit_InvertSelection.Name = "MenuItemEdit_InvertSelection";
            this.MenuItemEdit_InvertSelection.Size = new System.Drawing.Size(155, 22);
            this.MenuItemEdit_InvertSelection.Text = "Invert Selection";
            this.MenuItemEdit_InvertSelection.Click += new System.EventHandler(this.MenuItemEdit_InvertSelection_Click);
            // 
            // MainMenu_Load
            // 
            this.MainMenu_Load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertPreferencesToolStripMenuItem});
            this.MainMenu_Load.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MainMenu_Load.MergeIndex = 3;
            this.MainMenu_Load.Name = "MainMenu_Load";
            this.MainMenu_Load.Size = new System.Drawing.Size(45, 20);
            this.MainMenu_Load.Text = "Load";
            // 
            // convertPreferencesToolStripMenuItem
            // 
            this.convertPreferencesToolStripMenuItem.Name = "convertPreferencesToolStripMenuItem";
            this.convertPreferencesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertPreferencesToolStripMenuItem.Text = "Convert Preferences";
            // 
            // Groupbox_Files
            // 
            this.Groupbox_Files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Files.Controls.Add(this.Button_ClearList);
            this.Groupbox_Files.Controls.Add(this.Textbox_ItemsCount);
            this.Groupbox_Files.Controls.Add(this.Label_ItemsCount);
            this.Groupbox_Files.Controls.Add(this.ListView_ItemsToConvert);
            this.Groupbox_Files.Location = new System.Drawing.Point(12, 12);
            this.Groupbox_Files.Name = "Groupbox_Files";
            this.Groupbox_Files.Size = new System.Drawing.Size(863, 566);
            this.Groupbox_Files.TabIndex = 0;
            this.Groupbox_Files.TabStop = false;
            this.Groupbox_Files.Text = "Files";
            // 
            // Button_ClearList
            // 
            this.Button_ClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ClearList.Location = new System.Drawing.Point(782, 537);
            this.Button_ClearList.Name = "Button_ClearList";
            this.Button_ClearList.Size = new System.Drawing.Size(75, 23);
            this.Button_ClearList.TabIndex = 3;
            this.Button_ClearList.Text = "Clear List";
            this.Button_ClearList.UseVisualStyleBackColor = true;
            this.Button_ClearList.Click += new System.EventHandler(this.Button_ClearList_Click);
            // 
            // Textbox_ItemsCount
            // 
            this.Textbox_ItemsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Textbox_ItemsCount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_ItemsCount.Location = new System.Drawing.Point(78, 540);
            this.Textbox_ItemsCount.Name = "Textbox_ItemsCount";
            this.Textbox_ItemsCount.ReadOnly = true;
            this.Textbox_ItemsCount.Size = new System.Drawing.Size(100, 20);
            this.Textbox_ItemsCount.TabIndex = 2;
            // 
            // Label_ItemsCount
            // 
            this.Label_ItemsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_ItemsCount.AutoSize = true;
            this.Label_ItemsCount.ForeColor = System.Drawing.Color.Green;
            this.Label_ItemsCount.Location = new System.Drawing.Point(6, 543);
            this.Label_ItemsCount.Name = "Label_ItemsCount";
            this.Label_ItemsCount.Size = new System.Drawing.Size(66, 13);
            this.Label_ItemsCount.TabIndex = 1;
            this.Label_ItemsCount.Text = "Items Count:";
            // 
            // ListView_ItemsToConvert
            // 
            this.ListView_ItemsToConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_ItemsToConvert.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_FilePath,
            this.Col_FileExtension,
            this.Col_FileSize});
            this.ListView_ItemsToConvert.ContextMenuStrip = this.ContextMenu_ListItemsOptions;
            this.ListView_ItemsToConvert.FullRowSelect = true;
            this.ListView_ItemsToConvert.GridLines = true;
            this.ListView_ItemsToConvert.HideSelection = false;
            this.ListView_ItemsToConvert.Location = new System.Drawing.Point(3, 16);
            this.ListView_ItemsToConvert.Name = "ListView_ItemsToConvert";
            this.ListView_ItemsToConvert.Size = new System.Drawing.Size(857, 515);
            this.ListView_ItemsToConvert.TabIndex = 0;
            this.ListView_ItemsToConvert.UseCompatibleStateImageBehavior = false;
            this.ListView_ItemsToConvert.View = System.Windows.Forms.View.Details;
            this.ListView_ItemsToConvert.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_ItemsToConvert_MouseDoubleClick);
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            this.Col_Name.Width = 196;
            // 
            // Col_FilePath
            // 
            this.Col_FilePath.Text = "Path";
            this.Col_FilePath.Width = 482;
            // 
            // Col_FileExtension
            // 
            this.Col_FileExtension.Text = "Extension";
            // 
            // Col_FileSize
            // 
            this.Col_FileSize.Text = "Size";
            this.Col_FileSize.Width = 107;
            // 
            // ContextMenu_ListItemsOptions
            // 
            this.ContextMenu_ListItemsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Open,
            this.MenuItem_OpenFolder,
            this.MenuItem_Separator1,
            this.MenuItem_SelectAll,
            this.MenuItem_SelectNone,
            this.MenuItem_InvertSelection,
            this.MenuItem_Separator2,
            this.MenuItem_Delete});
            this.ContextMenu_ListItemsOptions.Name = "ContextMenu_ListItemsOptions";
            this.ContextMenu_ListItemsOptions.Size = new System.Drawing.Size(163, 148);
            // 
            // MenuItem_Open
            // 
            this.MenuItem_Open.Name = "MenuItem_Open";
            this.MenuItem_Open.Size = new System.Drawing.Size(162, 22);
            this.MenuItem_Open.Text = "Open";
            this.MenuItem_Open.Click += new System.EventHandler(this.MenuItem_Open_Click);
            // 
            // MenuItem_OpenFolder
            // 
            this.MenuItem_OpenFolder.Name = "MenuItem_OpenFolder";
            this.MenuItem_OpenFolder.Size = new System.Drawing.Size(162, 22);
            this.MenuItem_OpenFolder.Text = "Open Folder";
            this.MenuItem_OpenFolder.Click += new System.EventHandler(this.MenuItem_OpenFolder_Click);
            // 
            // MenuItem_Separator1
            // 
            this.MenuItem_Separator1.Name = "MenuItem_Separator1";
            this.MenuItem_Separator1.Size = new System.Drawing.Size(159, 6);
            // 
            // MenuItem_SelectAll
            // 
            this.MenuItem_SelectAll.Name = "MenuItem_SelectAll";
            this.MenuItem_SelectAll.Size = new System.Drawing.Size(162, 22);
            this.MenuItem_SelectAll.Text = "Select All";
            this.MenuItem_SelectAll.Click += new System.EventHandler(this.MenuItem_SelectAll_Click);
            // 
            // MenuItem_SelectNone
            // 
            this.MenuItem_SelectNone.Name = "MenuItem_SelectNone";
            this.MenuItem_SelectNone.Size = new System.Drawing.Size(162, 22);
            this.MenuItem_SelectNone.Text = "Select None";
            this.MenuItem_SelectNone.Click += new System.EventHandler(this.MenuItem_SelectNone_Click);
            // 
            // MenuItem_InvertSelection
            // 
            this.MenuItem_InvertSelection.Name = "MenuItem_InvertSelection";
            this.MenuItem_InvertSelection.Size = new System.Drawing.Size(162, 22);
            this.MenuItem_InvertSelection.Text = "Invert Selection";
            this.MenuItem_InvertSelection.Click += new System.EventHandler(this.MenuItem_InvertSelection_Click);
            // 
            // MenuItem_Separator2
            // 
            this.MenuItem_Separator2.Name = "MenuItem_Separator2";
            this.MenuItem_Separator2.Size = new System.Drawing.Size(159, 6);
            // 
            // MenuItem_Delete
            // 
            this.MenuItem_Delete.Name = "MenuItem_Delete";
            this.MenuItem_Delete.Size = new System.Drawing.Size(162, 22);
            this.MenuItem_Delete.Text = "Delete (from list)";
            this.MenuItem_Delete.Click += new System.EventHandler(this.MenuItem_Delete_Click);
            // 
            // Groupbox_ConvertOptions
            // 
            this.Groupbox_ConvertOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_ConvertOptions.Controls.Add(this.RadioButton_Stereo);
            this.Groupbox_ConvertOptions.Controls.Add(this.RadioButton_Mono);
            this.Groupbox_ConvertOptions.Controls.Add(this.Label_Channels);
            this.Groupbox_ConvertOptions.Controls.Add(this.ComboBox_Bits);
            this.Groupbox_ConvertOptions.Controls.Add(this.Label_Bits);
            this.Groupbox_ConvertOptions.Controls.Add(this.Combobox_Rate);
            this.Groupbox_ConvertOptions.Controls.Add(this.Label_Rate);
            this.Groupbox_ConvertOptions.Location = new System.Drawing.Point(497, 584);
            this.Groupbox_ConvertOptions.Name = "Groupbox_ConvertOptions";
            this.Groupbox_ConvertOptions.Size = new System.Drawing.Size(378, 71);
            this.Groupbox_ConvertOptions.TabIndex = 2;
            this.Groupbox_ConvertOptions.TabStop = false;
            this.Groupbox_ConvertOptions.Text = "Convert Options";
            // 
            // RadioButton_Stereo
            // 
            this.RadioButton_Stereo.AutoSize = true;
            this.RadioButton_Stereo.Location = new System.Drawing.Point(152, 46);
            this.RadioButton_Stereo.Name = "RadioButton_Stereo";
            this.RadioButton_Stereo.Size = new System.Drawing.Size(56, 17);
            this.RadioButton_Stereo.TabIndex = 6;
            this.RadioButton_Stereo.Text = "Stereo";
            this.RadioButton_Stereo.UseVisualStyleBackColor = true;
            // 
            // RadioButton_Mono
            // 
            this.RadioButton_Mono.AutoSize = true;
            this.RadioButton_Mono.Checked = true;
            this.RadioButton_Mono.Location = new System.Drawing.Point(94, 46);
            this.RadioButton_Mono.Name = "RadioButton_Mono";
            this.RadioButton_Mono.Size = new System.Drawing.Size(52, 17);
            this.RadioButton_Mono.TabIndex = 5;
            this.RadioButton_Mono.TabStop = true;
            this.RadioButton_Mono.Text = "Mono";
            this.RadioButton_Mono.UseVisualStyleBackColor = true;
            // 
            // Label_Channels
            // 
            this.Label_Channels.AutoSize = true;
            this.Label_Channels.Location = new System.Drawing.Point(34, 48);
            this.Label_Channels.Name = "Label_Channels";
            this.Label_Channels.Size = new System.Drawing.Size(54, 13);
            this.Label_Channels.TabIndex = 4;
            this.Label_Channels.Text = "Channels:";
            // 
            // ComboBox_Bits
            // 
            this.ComboBox_Bits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Bits.FormattingEnabled = true;
            this.ComboBox_Bits.Items.AddRange(new object[] {
            "16",
            "24",
            "32"});
            this.ComboBox_Bits.Location = new System.Drawing.Point(271, 19);
            this.ComboBox_Bits.Name = "ComboBox_Bits";
            this.ComboBox_Bits.Size = new System.Drawing.Size(101, 21);
            this.ComboBox_Bits.TabIndex = 3;
            // 
            // Label_Bits
            // 
            this.Label_Bits.AutoSize = true;
            this.Label_Bits.Location = new System.Drawing.Point(238, 22);
            this.Label_Bits.Name = "Label_Bits";
            this.Label_Bits.Size = new System.Drawing.Size(27, 13);
            this.Label_Bits.TabIndex = 2;
            this.Label_Bits.Text = "Bits:";
            // 
            // Combobox_Rate
            // 
            this.Combobox_Rate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_Rate.FormattingEnabled = true;
            this.Combobox_Rate.Items.AddRange(new object[] {
            "16000",
            "22050",
            "32000",
            "44100"});
            this.Combobox_Rate.Location = new System.Drawing.Point(94, 19);
            this.Combobox_Rate.Name = "Combobox_Rate";
            this.Combobox_Rate.Size = new System.Drawing.Size(138, 21);
            this.Combobox_Rate.TabIndex = 1;
            // 
            // Label_Rate
            // 
            this.Label_Rate.AutoSize = true;
            this.Label_Rate.Location = new System.Drawing.Point(6, 22);
            this.Label_Rate.Name = "Label_Rate";
            this.Label_Rate.Size = new System.Drawing.Size(82, 13);
            this.Label_Rate.TabIndex = 0;
            this.Label_Rate.Text = "Frequency (Hz):";
            // 
            // Button_Start
            // 
            this.Button_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Start.Location = new System.Drawing.Point(12, 661);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(75, 39);
            this.Button_Start.TabIndex = 3;
            this.Button_Start.Text = "Start";
            this.Button_Start.UseVisualStyleBackColor = true;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // ProgressBar_Status
            // 
            this.ProgressBar_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Status.Location = new System.Drawing.Point(177, 677);
            this.ProgressBar_Status.Name = "ProgressBar_Status";
            this.ProgressBar_Status.Size = new System.Drawing.Size(698, 23);
            this.ProgressBar_Status.TabIndex = 5;
            // 
            // Label_Status
            // 
            this.Label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_Status.AutoSize = true;
            this.Label_Status.Location = new System.Drawing.Point(174, 661);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(40, 13);
            this.Label_Status.TabIndex = 4;
            this.Label_Status.Text = "Status:";
            // 
            // Background_ConvertAudios
            // 
            this.Background_ConvertAudios.WorkerSupportsCancellation = true;
            this.Background_ConvertAudios.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Background_ConvertAudios_DoWorkAsync);
            this.Background_ConvertAudios.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Background_ConvertAudios_RunWorkerCompleted);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_Cancel.Location = new System.Drawing.Point(93, 661);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 39);
            this.Button_Cancel.TabIndex = 6;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Frm_AudioConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 712);
            this.Controls.Add(this.AudioConverter_MainMenu);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Label_Status);
            this.Controls.Add(this.ProgressBar_Status);
            this.Controls.Add(this.Button_Start);
            this.Controls.Add(this.Groupbox_ConvertOptions);
            this.Controls.Add(this.Groupbox_Files);
            this.Controls.Add(this.Groupbox_OutputDirectory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.AudioConverter_MainMenu;
            this.Name = "Frm_AudioConverter";
            this.ShowInTaskbar = false;
            this.Text = "Audio Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AudioConverter_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AudioConverter_Load);
            this.Groupbox_OutputDirectory.ResumeLayout(false);
            this.Groupbox_OutputDirectory.PerformLayout();
            this.AudioConverter_MainMenu.ResumeLayout(false);
            this.AudioConverter_MainMenu.PerformLayout();
            this.Groupbox_Files.ResumeLayout(false);
            this.Groupbox_Files.PerformLayout();
            this.ContextMenu_ListItemsOptions.ResumeLayout(false);
            this.Groupbox_ConvertOptions.ResumeLayout(false);
            this.Groupbox_ConvertOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Groupbox_OutputDirectory;
        private System.Windows.Forms.Label Label_OutputPath;
        private System.Windows.Forms.Button Button_SearchOutputFolder;
        private System.Windows.Forms.TextBox Textbox_OutputFolder;
        private System.Windows.Forms.MenuStrip AudioConverter_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ImportFolders;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_ImportFiles;
        private System.Windows.Forms.GroupBox Groupbox_Files;
        private System.Windows.Forms.GroupBox Groupbox_ConvertOptions;
        private System.Windows.Forms.RadioButton RadioButton_Stereo;
        private System.Windows.Forms.RadioButton RadioButton_Mono;
        private System.Windows.Forms.Label Label_Channels;
        private System.Windows.Forms.ComboBox ComboBox_Bits;
        private System.Windows.Forms.Label Label_Bits;
        private System.Windows.Forms.ComboBox Combobox_Rate;
        private System.Windows.Forms.Label Label_Rate;
        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.ProgressBar ProgressBar_Status;
        private System.Windows.Forms.Label Label_Status;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Load;
        private System.Windows.Forms.ToolStripMenuItem convertPreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit_SelectNone;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit_InvertSelection;
        private System.Windows.Forms.ToolStripSeparator MenuItemFile_Separator1;
        private ListView_ColumnSortingClick ListView_ItemsToConvert;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_FilePath;
        private System.Windows.Forms.ColumnHeader Col_FileExtension;
        private System.Windows.Forms.ColumnHeader Col_FileSize;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_ListItemsOptions;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Open;
        private System.Windows.Forms.ToolStripSeparator MenuItem_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Delete;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_OpenFolder;
        private System.Windows.Forms.ToolStripSeparator MenuItem_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SelectNone;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_InvertSelection;
        private System.ComponentModel.BackgroundWorker Background_ConvertAudios;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_ClearList;
        private System.Windows.Forms.TextBox Textbox_ItemsCount;
        private System.Windows.Forms.Label Label_ItemsCount;
    }
}