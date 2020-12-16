
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EuroSound_Main));
            this.EuroSound_Main_StatusBar = new System.Windows.Forms.StatusStrip();
            this.EuroSound_StatusBar_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.EuroSound_StatusBar_FileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuStrip_MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_OpenESF = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_Separator = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView_Preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView_StatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_Cascade = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_TileH = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_TileV = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_Arrange = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWindow_Separator = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTools_SFXDataGen = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemHelp_OnlineHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.EuroSound_Main_StatusBar.SuspendLayout();
            this.MenuStrip_MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // EuroSound_Main_StatusBar
            // 
            this.EuroSound_Main_StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EuroSound_StatusBar_Status,
            this.EuroSound_StatusBar_FileName});
            this.EuroSound_Main_StatusBar.Location = new System.Drawing.Point(0, 834);
            this.EuroSound_Main_StatusBar.Name = "EuroSound_Main_StatusBar";
            this.EuroSound_Main_StatusBar.Size = new System.Drawing.Size(1421, 22);
            this.EuroSound_Main_StatusBar.TabIndex = 1;
            this.EuroSound_Main_StatusBar.Text = "statusStrip1";
            // 
            // EuroSound_StatusBar_Status
            // 
            this.EuroSound_StatusBar_Status.Name = "EuroSound_StatusBar_Status";
            this.EuroSound_StatusBar_Status.Size = new System.Drawing.Size(73, 17);
            this.EuroSound_StatusBar_Status.Text = "Starting App";
            // 
            // EuroSound_StatusBar_FileName
            // 
            this.EuroSound_StatusBar_FileName.Name = "EuroSound_StatusBar_FileName";
            this.EuroSound_StatusBar_FileName.Size = new System.Drawing.Size(1333, 17);
            this.EuroSound_StatusBar_FileName.Spring = true;
            this.EuroSound_StatusBar_FileName.Text = "Unnamed";
            this.EuroSound_StatusBar_FileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MenuStrip_MainMenu
            // 
            this.MenuStrip_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File,
            this.MainMenu_View,
            this.MainMenu_Window,
            this.MainMenu_Tools,
            this.MainMenu_Help});
            this.MenuStrip_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_MainMenu.MdiWindowListItem = this.MainMenu_Window;
            this.MenuStrip_MainMenu.Name = "MenuStrip_MainMenu";
            this.MenuStrip_MainMenu.Size = new System.Drawing.Size(1421, 24);
            this.MenuStrip_MainMenu.TabIndex = 3;
            this.MenuStrip_MainMenu.Text = "menuStrip1";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_New,
            this.MenuItemFile_OpenESF,
            this.MenuItemFile_Separator,
            this.MenuItemFile_Exit});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MenuItemFile_New
            // 
            this.MenuItemFile_New.MergeIndex = 0;
            this.MenuItemFile_New.Name = "MenuItemFile_New";
            this.MenuItemFile_New.Size = new System.Drawing.Size(103, 22);
            this.MenuItemFile_New.Text = "New";
            this.MenuItemFile_New.Click += new System.EventHandler(this.MenuItemFile_New_Click);
            // 
            // MenuItemFile_OpenESF
            // 
            this.MenuItemFile_OpenESF.MergeIndex = 1;
            this.MenuItemFile_OpenESF.Name = "MenuItemFile_OpenESF";
            this.MenuItemFile_OpenESF.Size = new System.Drawing.Size(103, 22);
            this.MenuItemFile_OpenESF.Text = "Open";
            this.MenuItemFile_OpenESF.Click += new System.EventHandler(this.MenuItemFile_OpenESF_Click);
            // 
            // MenuItemFile_Separator
            // 
            this.MenuItemFile_Separator.MergeIndex = 9;
            this.MenuItemFile_Separator.Name = "MenuItemFile_Separator";
            this.MenuItemFile_Separator.Size = new System.Drawing.Size(100, 6);
            // 
            // MenuItemFile_Exit
            // 
            this.MenuItemFile_Exit.MergeIndex = 10;
            this.MenuItemFile_Exit.Name = "MenuItemFile_Exit";
            this.MenuItemFile_Exit.Size = new System.Drawing.Size(103, 22);
            this.MenuItemFile_Exit.Text = "Exit";
            this.MenuItemFile_Exit.Click += new System.EventHandler(this.MenuItemFile_Exit_Click);
            // 
            // MainMenu_View
            // 
            this.MainMenu_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemView_Preferences,
            this.MenuItemView_StatusBar});
            this.MainMenu_View.Name = "MainMenu_View";
            this.MainMenu_View.Size = new System.Drawing.Size(44, 20);
            this.MainMenu_View.Text = "View";
            // 
            // MenuItemView_Preferences
            // 
            this.MenuItemView_Preferences.Name = "MenuItemView_Preferences";
            this.MenuItemView_Preferences.Size = new System.Drawing.Size(135, 22);
            this.MenuItemView_Preferences.Text = "Preferences";
            this.MenuItemView_Preferences.Click += new System.EventHandler(this.MenuItemView_Preferences_Click);
            // 
            // MenuItemView_StatusBar
            // 
            this.MenuItemView_StatusBar.Checked = true;
            this.MenuItemView_StatusBar.CheckOnClick = true;
            this.MenuItemView_StatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemView_StatusBar.Name = "MenuItemView_StatusBar";
            this.MenuItemView_StatusBar.Size = new System.Drawing.Size(135, 22);
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
            this.MainMenuTools_SFXDataGen});
            this.MainMenu_Tools.Name = "MainMenu_Tools";
            this.MainMenu_Tools.Size = new System.Drawing.Size(46, 20);
            this.MainMenu_Tools.Text = "Tools";
            // 
            // MainMenuTools_SFXDataGen
            // 
            this.MainMenuTools_SFXDataGen.Name = "MainMenuTools_SFXDataGen";
            this.MainMenuTools_SFXDataGen.Size = new System.Drawing.Size(180, 22);
            this.MainMenuTools_SFXDataGen.Text = "SFX Data Binary File";
            this.MainMenuTools_SFXDataGen.Click += new System.EventHandler(this.MainMenuTools_SFXDataGen_Click);
            // 
            // MainMenu_Help
            // 
            this.MainMenu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemHelp_About,
            this.MenuItemHelp_OnlineHelp});
            this.MainMenu_Help.Name = "MainMenu_Help";
            this.MainMenu_Help.Size = new System.Drawing.Size(44, 20);
            this.MainMenu_Help.Text = "Help";
            // 
            // MenuItemHelp_About
            // 
            this.MenuItemHelp_About.Name = "MenuItemHelp_About";
            this.MenuItemHelp_About.Size = new System.Drawing.Size(177, 22);
            this.MenuItemHelp_About.Text = "About EuroSound...";
            this.MenuItemHelp_About.Click += new System.EventHandler(this.MenuItemHelp_About_Click);
            // 
            // MenuItemHelp_OnlineHelp
            // 
            this.MenuItemHelp_OnlineHelp.Name = "MenuItemHelp_OnlineHelp";
            this.MenuItemHelp_OnlineHelp.Size = new System.Drawing.Size(177, 22);
            this.MenuItemHelp_OnlineHelp.Text = "Online Help...";
            // 
            // Frm_EuroSound_Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 856);
            this.Controls.Add(this.EuroSound_Main_StatusBar);
            this.Controls.Add(this.MenuStrip_MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuStrip_MainMenu;
            this.Name = "Frm_EuroSound_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EuroSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_EuroSound_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_EuroSound_Main_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Frm_EuroSound_Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Frm_EuroSound_Main_DragEnter);
            this.EuroSound_Main_StatusBar.ResumeLayout(false);
            this.EuroSound_Main_StatusBar.PerformLayout();
            this.MenuStrip_MainMenu.ResumeLayout(false);
            this.MenuStrip_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal System.Windows.Forms.ToolStripStatusLabel EuroSound_StatusBar_Status;
        protected internal System.Windows.Forms.StatusStrip EuroSound_Main_StatusBar;
        protected internal System.Windows.Forms.ToolStripStatusLabel EuroSound_StatusBar_FileName;
        private System.Windows.Forms.MenuStrip MenuStrip_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_View;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Window;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_OpenESF;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView_StatusBar;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_New;
        private System.Windows.Forms.ToolStripSeparator MenuItemFile_Separator;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_Cascade;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_TileH;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_Arrange;
        private System.Windows.Forms.ToolStripSeparator MenuItemWindow_Separator;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_About;
        private System.Windows.Forms.ToolStripMenuItem MenuItemHelp_OnlineHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWindow_TileV;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView_Preferences;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Tools;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTools_SFXDataGen;
    }
}