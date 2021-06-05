
namespace EuroSound_Application
{
    partial class Frm_EuroSound_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EuroSound_Main));
            this.MenuStrip_MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_OpenESF = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Updates = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_RecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEdit_GlobalOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView_StatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_Cascade = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_TileH = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_TileV = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_Arrange = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_Separator = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_DebugHashtables = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_AudioConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_SFXDataGen = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuTools_BackupSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_RestoreSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuTools_ClearTempFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_OnlineHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemHelp_ReleaseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_CurrentReleaseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemHelp_Documentation = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusBar = new CustomStatusBar.StatusBarToolTips();
            this.SBPanel_LastFile = new System.Windows.Forms.StatusBarPanel();
            this.SBPanel_Hashcode = new System.Windows.Forms.StatusBarPanel();
            this.SBPanel_File = new System.Windows.Forms.StatusBarPanel();
            this.EuroSoundTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.EuroSoundTrayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EuroSoundTrayIcon_Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.EuroSoundTrayIcon_Separator = new System.Windows.Forms.ToolStripSeparator();
            this.EuroSoundTrayIcon_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SBPanel_LastFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SBPanel_Hashcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SBPanel_File)).BeginInit();
            this.EuroSoundTrayIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip_MainMenu
            // 
            this.MenuStrip_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File,
            this.MainMenu_Edit,
            this.MainMenu_View,
            this.MainMenu_Window,
            this.MainMenu_Tools,
            this.MainMenu_Help});
            this.MenuStrip_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_MainMenu.MdiWindowListItem = this.MainMenu_Window;
            this.MenuStrip_MainMenu.Name = "MenuStrip_MainMenu";
            this.MenuStrip_MainMenu.Size = new System.Drawing.Size(1103, 24);
            this.MenuStrip_MainMenu.TabIndex = 0;
            this.MenuStrip_MainMenu.Text = "MainMenu";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_New,
            this.MenuItemFile_OpenESF,
            this.MenuItemFile_Separator1,
            this.MenuItemFile_Updates,
            this.MenuItemFile_Separator2,
            this.MenuItemFile_RecentFiles,
            this.MenuItemFile_Separator3,
            this.MenuItemFile_Exit});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MenuItemFile_New
            // 
            this.MenuItemFile_New.MergeIndex = 0;
            this.MenuItemFile_New.Name = "MenuItemFile_New";
            this.MenuItemFile_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuItemFile_New.Size = new System.Drawing.Size(196, 22);
            this.MenuItemFile_New.Text = "New";
            this.MenuItemFile_New.Click += new System.EventHandler(this.MenuItemFile_New_Click);
            // 
            // MenuItemFile_OpenESF
            // 
            this.MenuItemFile_OpenESF.MergeIndex = 1;
            this.MenuItemFile_OpenESF.Name = "MenuItemFile_OpenESF";
            this.MenuItemFile_OpenESF.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuItemFile_OpenESF.Size = new System.Drawing.Size(196, 22);
            this.MenuItemFile_OpenESF.Text = "Open";
            this.MenuItemFile_OpenESF.Click += new System.EventHandler(this.MenuItemFile_OpenESF_Click);
            // 
            // MenuItemFile_Separator1
            // 
            this.MenuItemFile_Separator1.MergeIndex = 10;
            this.MenuItemFile_Separator1.Name = "MenuItemFile_Separator1";
            this.MenuItemFile_Separator1.Size = new System.Drawing.Size(193, 6);
            // 
            // MenuItemFile_Updates
            // 
            this.MenuItemFile_Updates.Checked = true;
            this.MenuItemFile_Updates.CheckOnClick = true;
            this.MenuItemFile_Updates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemFile_Updates.MergeIndex = 11;
            this.MenuItemFile_Updates.Name = "MenuItemFile_Updates";
            this.MenuItemFile_Updates.Size = new System.Drawing.Size(196, 22);
            this.MenuItemFile_Updates.Text = "Enable Version Updates";
            this.MenuItemFile_Updates.CheckedChanged += new System.EventHandler(this.MenuItemFile_Updates_CheckedChanged);
            // 
            // MenuItemFile_Separator2
            // 
            this.MenuItemFile_Separator2.MergeIndex = 12;
            this.MenuItemFile_Separator2.Name = "MenuItemFile_Separator2";
            this.MenuItemFile_Separator2.Size = new System.Drawing.Size(193, 6);
            // 
            // MenuItemFile_RecentFiles
            // 
            this.MenuItemFile_RecentFiles.MergeIndex = 12;
            this.MenuItemFile_RecentFiles.Name = "MenuItemFile_RecentFiles";
            this.MenuItemFile_RecentFiles.Size = new System.Drawing.Size(196, 22);
            this.MenuItemFile_RecentFiles.Text = "Recent Files";
            // 
            // MenuItemFile_Separator3
            // 
            this.MenuItemFile_Separator3.MergeIndex = 19;
            this.MenuItemFile_Separator3.Name = "MenuItemFile_Separator3";
            this.MenuItemFile_Separator3.Size = new System.Drawing.Size(193, 6);
            // 
            // MenuItemFile_Exit
            // 
            this.MenuItemFile_Exit.MergeIndex = 20;
            this.MenuItemFile_Exit.Name = "MenuItemFile_Exit";
            this.MenuItemFile_Exit.Size = new System.Drawing.Size(196, 22);
            this.MenuItemFile_Exit.Text = "Exit";
            this.MenuItemFile_Exit.Click += new System.EventHandler(this.MenuItemFile_Exit_Click);
            // 
            // MainMenu_Edit
            // 
            this.MainMenu_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemEdit_GlobalOptions});
            this.MainMenu_Edit.Name = "MainMenu_Edit";
            this.MainMenu_Edit.Size = new System.Drawing.Size(39, 20);
            this.MainMenu_Edit.Text = "Edit";
            // 
            // MenuItemEdit_GlobalOptions
            // 
            this.MenuItemEdit_GlobalOptions.Name = "MenuItemEdit_GlobalOptions";
            this.MenuItemEdit_GlobalOptions.Size = new System.Drawing.Size(180, 22);
            this.MenuItemEdit_GlobalOptions.Text = "Global Options";
            this.MenuItemEdit_GlobalOptions.Click += new System.EventHandler(this.MenuItemEdit_GlobalOptions_Click);
            // 
            // MainMenu_View
            // 
            this.MainMenu_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemView_StatusBar});
            this.MainMenu_View.Name = "MainMenu_View";
            this.MainMenu_View.Size = new System.Drawing.Size(44, 20);
            this.MainMenu_View.Text = "View";
            // 
            // MenuItemView_StatusBar
            // 
            this.MenuItemView_StatusBar.Checked = true;
            this.MenuItemView_StatusBar.CheckOnClick = true;
            this.MenuItemView_StatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemView_StatusBar.MergeIndex = 1;
            this.MenuItemView_StatusBar.Name = "MenuItemView_StatusBar";
            this.MenuItemView_StatusBar.Size = new System.Drawing.Size(126, 22);
            this.MenuItemView_StatusBar.Text = "Status Bar";
            this.MenuItemView_StatusBar.CheckStateChanged += new System.EventHandler(this.MenuItemView_StatusBar_CheckStateChanged);
            // 
            // MainMenu_Window
            // 
            this.MainMenu_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemWindow_Cascade,
            this.MenuItemWindow_TileH,
            this.MenuItemWindow_TileV,
            this.MenuItemWindow_Arrange,
            this.MenuItemWindow_Separator});
            this.MainMenu_Window.Name = "MainMenu_Window";
            this.MainMenu_Window.Size = new System.Drawing.Size(63, 20);
            this.MainMenu_Window.Text = "Window";
            // 
            // MenuItemWindow_Cascade
            // 
            this.MenuItemWindow_Cascade.Name = "MenuItemWindow_Cascade";
            this.MenuItemWindow_Cascade.Size = new System.Drawing.Size(150, 22);
            this.MenuItemWindow_Cascade.Text = "Cascade";
            // 
            // MenuItemWindow_TileH
            // 
            this.MenuItemWindow_TileH.Name = "MenuItemWindow_TileH";
            this.MenuItemWindow_TileH.Size = new System.Drawing.Size(150, 22);
            this.MenuItemWindow_TileH.Text = "Tile Horizontal";
            // 
            // MenuItemWindow_TileV
            // 
            this.MenuItemWindow_TileV.Name = "MenuItemWindow_TileV";
            this.MenuItemWindow_TileV.Size = new System.Drawing.Size(150, 22);
            this.MenuItemWindow_TileV.Text = "Tile Vertical";
            // 
            // MenuItemWindow_Arrange
            // 
            this.MenuItemWindow_Arrange.Name = "MenuItemWindow_Arrange";
            this.MenuItemWindow_Arrange.Size = new System.Drawing.Size(150, 22);
            this.MenuItemWindow_Arrange.Text = "Arrange Icons";
            // 
            // MenuItemWindow_Separator
            // 
            this.MenuItemWindow_Separator.Name = "MenuItemWindow_Separator";
            this.MenuItemWindow_Separator.Size = new System.Drawing.Size(147, 6);
            // 
            // MainMenu_Tools
            // 
            this.MainMenu_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuTools_DebugHashtables,
            this.MainMenuTools_AudioConverter,
            this.MainMenuTools_SFXDataGen,
            this.MainMenuTools_Separator1,
            this.MainMenuTools_BackupSettings,
            this.MainMenuTools_RestoreSettings,
            this.MainMenuTools_Separator2,
            this.MainMenuTools_ClearTempFiles});
            this.MainMenu_Tools.Name = "MainMenu_Tools";
            this.MainMenu_Tools.Size = new System.Drawing.Size(46, 20);
            this.MainMenu_Tools.Text = "Tools";
            // 
            // MainMenuTools_DebugHashtables
            // 
            this.MainMenuTools_DebugHashtables.Name = "MainMenuTools_DebugHashtables";
            this.MainMenuTools_DebugHashtables.Size = new System.Drawing.Size(201, 22);
            this.MainMenuTools_DebugHashtables.Text = "Debug HashTables";
            this.MainMenuTools_DebugHashtables.Click += new System.EventHandler(this.MainMenuTools_DebugHashtables_Click);
            // 
            // MainMenuTools_AudioConverter
            // 
            this.MainMenuTools_AudioConverter.Name = "MainMenuTools_AudioConverter";
            this.MainMenuTools_AudioConverter.Size = new System.Drawing.Size(201, 22);
            this.MainMenuTools_AudioConverter.Text = "Audio Converter";
            this.MainMenuTools_AudioConverter.Click += new System.EventHandler(this.MainMenuTools_AudioConverter_Click);
            // 
            // MainMenuTools_SFXDataGen
            // 
            this.MainMenuTools_SFXDataGen.Name = "MainMenuTools_SFXDataGen";
            this.MainMenuTools_SFXDataGen.Size = new System.Drawing.Size(201, 22);
            this.MainMenuTools_SFXDataGen.Text = "SFX Data Table";
            this.MainMenuTools_SFXDataGen.Click += new System.EventHandler(this.MainMenuTools_SFXDataGen_Click);
            // 
            // MainMenuTools_Separator1
            // 
            this.MainMenuTools_Separator1.Name = "MainMenuTools_Separator1";
            this.MainMenuTools_Separator1.Size = new System.Drawing.Size(198, 6);
            // 
            // MainMenuTools_BackupSettings
            // 
            this.MainMenuTools_BackupSettings.Name = "MainMenuTools_BackupSettings";
            this.MainMenuTools_BackupSettings.Size = new System.Drawing.Size(201, 22);
            this.MainMenuTools_BackupSettings.Text = "Backup Settings...";
            this.MainMenuTools_BackupSettings.Click += new System.EventHandler(this.MainMenuTools_BackupSettings_Click);
            // 
            // MainMenuTools_RestoreSettings
            // 
            this.MainMenuTools_RestoreSettings.Name = "MainMenuTools_RestoreSettings";
            this.MainMenuTools_RestoreSettings.Size = new System.Drawing.Size(201, 22);
            this.MainMenuTools_RestoreSettings.Text = "Restore Settings...";
            this.MainMenuTools_RestoreSettings.Click += new System.EventHandler(this.MainMenuTools_RestoreSettings_Click);
            // 
            // MainMenuTools_Separator2
            // 
            this.MainMenuTools_Separator2.Name = "MainMenuTools_Separator2";
            this.MainMenuTools_Separator2.Size = new System.Drawing.Size(198, 6);
            // 
            // MainMenuTools_ClearTempFiles
            // 
            this.MainMenuTools_ClearTempFiles.Name = "MainMenuTools_ClearTempFiles";
            this.MainMenuTools_ClearTempFiles.Size = new System.Drawing.Size(201, 22);
            this.MainMenuTools_ClearTempFiles.Text = "Delete Temporary Files...";
            this.MainMenuTools_ClearTempFiles.Click += new System.EventHandler(this.MainMenuTools_ClearTempFiles_Click);
            // 
            // MainMenu_Help
            // 
            this.MainMenu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemHelp_About,
            this.MenuItemHelp_OnlineHelp,
            this.MenuItemHelp_Separator1,
            this.MenuItemHelp_ReleaseInfo,
            this.MenuItemHelp_CurrentReleaseInfo,
            this.MenuItemHelp_Separator2,
            this.MenuItemHelp_Documentation});
            this.MainMenu_Help.Name = "MainMenu_Help";
            this.MainMenu_Help.Size = new System.Drawing.Size(44, 20);
            this.MainMenu_Help.Text = "Help";
            // 
            // MenuItemHelp_About
            // 
            this.MenuItemHelp_About.Name = "MenuItemHelp_About";
            this.MenuItemHelp_About.Size = new System.Drawing.Size(189, 22);
            this.MenuItemHelp_About.Text = "About EuroSound...";
            this.MenuItemHelp_About.Click += new System.EventHandler(this.MenuItemHelp_About_Click);
            // 
            // MenuItemHelp_OnlineHelp
            // 
            this.MenuItemHelp_OnlineHelp.Name = "MenuItemHelp_OnlineHelp";
            this.MenuItemHelp_OnlineHelp.Size = new System.Drawing.Size(189, 22);
            this.MenuItemHelp_OnlineHelp.Text = "Online Help...";
            // 
            // MenuItemHelp_Separator1
            // 
            this.MenuItemHelp_Separator1.Name = "MenuItemHelp_Separator1";
            this.MenuItemHelp_Separator1.Size = new System.Drawing.Size(186, 6);
            // 
            // MenuItemHelp_ReleaseInfo
            // 
            this.MenuItemHelp_ReleaseInfo.Name = "MenuItemHelp_ReleaseInfo";
            this.MenuItemHelp_ReleaseInfo.Size = new System.Drawing.Size(189, 22);
            this.MenuItemHelp_ReleaseInfo.Text = "Release Info...";
            // 
            // MenuItemHelp_CurrentReleaseInfo
            // 
            this.MenuItemHelp_CurrentReleaseInfo.Name = "MenuItemHelp_CurrentReleaseInfo";
            this.MenuItemHelp_CurrentReleaseInfo.Size = new System.Drawing.Size(189, 22);
            this.MenuItemHelp_CurrentReleaseInfo.Text = "Current Release Info...";
            // 
            // MenuItemHelp_Separator2
            // 
            this.MenuItemHelp_Separator2.Name = "MenuItemHelp_Separator2";
            this.MenuItemHelp_Separator2.Size = new System.Drawing.Size(186, 6);
            // 
            // MenuItemHelp_Documentation
            // 
            this.MenuItemHelp_Documentation.Name = "MenuItemHelp_Documentation";
            this.MenuItemHelp_Documentation.Size = new System.Drawing.Size(189, 22);
            this.MenuItemHelp_Documentation.Text = "Documentation";
            this.MenuItemHelp_Documentation.Click += new System.EventHandler(this.MenuItemHelp_Documentation_Click);
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.Location = new System.Drawing.Point(0, 556);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.SBPanel_LastFile,
            this.SBPanel_Hashcode,
            this.SBPanel_File});
            this.MainStatusBar.ShowPanels = true;
            this.MainStatusBar.Size = new System.Drawing.Size(1103, 22);
            this.MainStatusBar.TabIndex = 1;
            this.MainStatusBar.Text = "EuroSound_StatusBar";
            // 
            // SBPanel_LastFile
            // 
            this.SBPanel_LastFile.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.SBPanel_LastFile.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.SBPanel_LastFile.MinWidth = 40;
            this.SBPanel_LastFile.Name = "SBPanel_LastFile";
            this.SBPanel_LastFile.Width = 40;
            // 
            // SBPanel_Hashcode
            // 
            this.SBPanel_Hashcode.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.SBPanel_Hashcode.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.SBPanel_Hashcode.MinWidth = 40;
            this.SBPanel_Hashcode.Name = "SBPanel_Hashcode";
            this.SBPanel_Hashcode.Width = 40;
            // 
            // SBPanel_File
            // 
            this.SBPanel_File.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.SBPanel_File.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.SBPanel_File.MinWidth = 40;
            this.SBPanel_File.Name = "SBPanel_File";
            this.SBPanel_File.Width = 40;
            // 
            // EuroSoundTrayIcon
            // 
            this.EuroSoundTrayIcon.BalloonTipText = "Double click to restore";
            this.EuroSoundTrayIcon.BalloonTipTitle = "EuroSound";
            this.EuroSoundTrayIcon.ContextMenuStrip = this.EuroSoundTrayIconMenu;
            this.EuroSoundTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("EuroSoundTrayIcon.Icon")));
            this.EuroSoundTrayIcon.Text = "EuroSound";
            this.EuroSoundTrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EuroSoundTrayIcon_MouseClick);
            // 
            // EuroSoundTrayIconMenu
            // 
            this.EuroSoundTrayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EuroSoundTrayIcon_Restore,
            this.EuroSoundTrayIcon_Separator,
            this.EuroSoundTrayIcon_Close});
            this.EuroSoundTrayIconMenu.Name = "EuroSoundTrayIconMenu";
            this.EuroSoundTrayIconMenu.Size = new System.Drawing.Size(135, 54);
            // 
            // EuroSoundTrayIcon_Restore
            // 
            this.EuroSoundTrayIcon_Restore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EuroSoundTrayIcon_Restore.Name = "EuroSoundTrayIcon_Restore";
            this.EuroSoundTrayIcon_Restore.Size = new System.Drawing.Size(134, 22);
            this.EuroSoundTrayIcon_Restore.Text = "EuroSound";
            this.EuroSoundTrayIcon_Restore.Click += new System.EventHandler(this.EuroSoundTrayIcon_Restore_Click);
            // 
            // EuroSoundTrayIcon_Separator
            // 
            this.EuroSoundTrayIcon_Separator.Name = "EuroSoundTrayIcon_Separator";
            this.EuroSoundTrayIcon_Separator.Size = new System.Drawing.Size(131, 6);
            // 
            // EuroSoundTrayIcon_Close
            // 
            this.EuroSoundTrayIcon_Close.Name = "EuroSoundTrayIcon_Close";
            this.EuroSoundTrayIcon_Close.Size = new System.Drawing.Size(134, 22);
            this.EuroSoundTrayIcon_Close.Text = "Close";
            this.EuroSoundTrayIcon_Close.Click += new System.EventHandler(this.EuroSoundTrayIcon_Close_Click);
            // 
            // Frm_EuroSound_Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 578);
            this.Controls.Add(this.MainStatusBar);
            this.Controls.Add(this.MenuStrip_MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuStrip_MainMenu;
            this.Name = "Frm_EuroSound_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Main";
            this.Text = "EuroSound";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_EuroSound_Main_FormClosed);
            this.Load += new System.EventHandler(this.Frm_EuroSound_Main_Load);
            this.Shown += new System.EventHandler(this.Frm_EuroSound_Main_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Frm_EuroSound_Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Frm_EuroSound_Main_DragEnter);
            this.Resize += new System.EventHandler(this.Frm_EuroSound_Main_Resize);
            this.MenuStrip_MainMenu.ResumeLayout(false);
            this.MenuStrip_MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SBPanel_LastFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SBPanel_Hashcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SBPanel_File)).EndInit();
            this.EuroSoundTrayIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MenuStrip_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_View;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_OpenESF;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView_StatusBar;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_New;
        private System.Windows.Forms.ToolStripSeparator MenuItemFile_Separator3;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_Cascade;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_TileH;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_Arrange;
        private System.Windows.Forms.ToolStripSeparator MenuItemWindow_Separator;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_About;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_OnlineHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_TileV;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Tools;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_SFXDataGen;
        private CustomStatusBar.StatusBarToolTips MainStatusBar;
        private System.Windows.Forms.StatusBarPanel SBPanel_File;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Window;
        private System.Windows.Forms.ToolStripSeparator MainMenuTools_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_BackupSettings;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_RestoreSettings;
        private System.Windows.Forms.ToolStripSeparator MainMenuTools_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_ClearTempFiles;
        private System.Windows.Forms.NotifyIcon EuroSoundTrayIcon;
        private System.Windows.Forms.ContextMenuStrip EuroSoundTrayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem EuroSoundTrayIcon_Restore;
        private System.Windows.Forms.ToolStripSeparator EuroSoundTrayIcon_Separator;
        private System.Windows.Forms.ToolStripMenuItem EuroSoundTrayIcon_Close;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_AudioConverter;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_RecentFiles;
        private System.Windows.Forms.ToolStripSeparator MenuItemFile_Separator1;
        private System.Windows.Forms.StatusBarPanel SBPanel_Hashcode;
        private System.Windows.Forms.ToolStripSeparator MenuItemHelp_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_ReleaseInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_CurrentReleaseInfo;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_DebugHashtables;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Updates;
        private System.Windows.Forms.ToolStripSeparator MenuItemFile_Separator2;
        private System.Windows.Forms.StatusBarPanel SBPanel_LastFile;
        private System.Windows.Forms.ToolStripSeparator MenuItemHelp_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_Documentation;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit_GlobalOptions;
    }
}