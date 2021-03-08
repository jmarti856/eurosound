
namespace RecentFilesProject
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuStrip_MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemFile_OpenESF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemFile_RecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip_MainMenu
            // 
            this.MenuStrip_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File});
            this.MenuStrip_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_MainMenu.Name = "MenuStrip_MainMenu";
            this.MenuStrip_MainMenu.Size = new System.Drawing.Size(846, 24);
            this.MenuStrip_MainMenu.TabIndex = 0;
            this.MenuStrip_MainMenu.Text = "menuStrip1";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile_OpenESF,
            this.toolStripSeparator1,
            this.MenuItemFile_RecentFiles,
            this.toolStripSeparator2});
            this.MainMenu_File.Name = "MainMenu_File";
            this.MainMenu_File.Size = new System.Drawing.Size(37, 20);
            this.MainMenu_File.Text = "File";
            // 
            // MenuItemFile_OpenESF
            // 
            this.MenuItemFile_OpenESF.Name = "MenuItemFile_OpenESF";
            this.MenuItemFile_OpenESF.Size = new System.Drawing.Size(180, 22);
            this.MenuItemFile_OpenESF.Text = "Open File";
            this.MenuItemFile_OpenESF.Click += new System.EventHandler(this.MenuItemFile_OpenESF_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItemFile_RecentFiles
            // 
            this.MenuItemFile_RecentFiles.Name = "MenuItemFile_RecentFiles";
            this.MenuItemFile_RecentFiles.Size = new System.Drawing.Size(180, 22);
            this.MenuItemFile_RecentFiles.Text = "Recent Files";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 520);
            this.Controls.Add(this.MenuStrip_MainMenu);
            this.MainMenuStrip = this.MenuStrip_MainMenu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MenuStrip_MainMenu.ResumeLayout(false);
            this.MenuStrip_MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_OpenESF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile_RecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

