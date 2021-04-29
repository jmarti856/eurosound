
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_AutoBackUps
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
            this.Panel_Title = new System.Windows.Forms.Panel();
            this.Label_Title = new System.Windows.Forms.Label();
            this.GroupBox_Backups = new System.Windows.Forms.GroupBox();
            this.Numeric_MaxBackups = new System.Windows.Forms.NumericUpDown();
            this.Label_MaxBackups = new System.Windows.Forms.Label();
            this.Label_Minutes = new System.Windows.Forms.Label();
            this.Numeric_BackupFrequency = new System.Windows.Forms.NumericUpDown();
            this.Label_Frequency = new System.Windows.Forms.Label();
            this.Button_BrowseFolder = new System.Windows.Forms.Button();
            this.Textbox_BackupFolderPath = new System.Windows.Forms.TextBox();
            this.Label_BackupFolder = new System.Windows.Forms.Label();
            this.CheckBox_AutomaticBackups = new System.Windows.Forms.CheckBox();
            this.Panel_Title.SuspendLayout();
            this.GroupBox_Backups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MaxBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BackupFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(513, 23);
            this.Panel_Title.TabIndex = 2;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(214, 5);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(85, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Auto-Backup";
            // 
            // GroupBox_Backups
            // 
            this.GroupBox_Backups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Backups.Controls.Add(this.Numeric_MaxBackups);
            this.GroupBox_Backups.Controls.Add(this.Label_MaxBackups);
            this.GroupBox_Backups.Controls.Add(this.Label_Minutes);
            this.GroupBox_Backups.Controls.Add(this.Numeric_BackupFrequency);
            this.GroupBox_Backups.Controls.Add(this.Label_Frequency);
            this.GroupBox_Backups.Controls.Add(this.Button_BrowseFolder);
            this.GroupBox_Backups.Controls.Add(this.Textbox_BackupFolderPath);
            this.GroupBox_Backups.Controls.Add(this.Label_BackupFolder);
            this.GroupBox_Backups.Controls.Add(this.CheckBox_AutomaticBackups);
            this.GroupBox_Backups.Location = new System.Drawing.Point(12, 114);
            this.GroupBox_Backups.Name = "GroupBox_Backups";
            this.GroupBox_Backups.Size = new System.Drawing.Size(489, 152);
            this.GroupBox_Backups.TabIndex = 3;
            this.GroupBox_Backups.TabStop = false;
            // 
            // Numeric_MaxBackups
            // 
            this.Numeric_MaxBackups.Location = new System.Drawing.Point(345, 53);
            this.Numeric_MaxBackups.Name = "Numeric_MaxBackups";
            this.Numeric_MaxBackups.Size = new System.Drawing.Size(57, 20);
            this.Numeric_MaxBackups.TabIndex = 12;
            // 
            // Label_MaxBackups
            // 
            this.Label_MaxBackups.AutoSize = true;
            this.Label_MaxBackups.Location = new System.Drawing.Point(259, 55);
            this.Label_MaxBackups.Name = "Label_MaxBackups";
            this.Label_MaxBackups.Size = new System.Drawing.Size(80, 13);
            this.Label_MaxBackups.TabIndex = 11;
            this.Label_MaxBackups.Text = "Max. Back-Ups";
            // 
            // Label_Minutes
            // 
            this.Label_Minutes.AutoSize = true;
            this.Label_Minutes.Location = new System.Drawing.Point(181, 55);
            this.Label_Minutes.Name = "Label_Minutes";
            this.Label_Minutes.Size = new System.Drawing.Size(44, 13);
            this.Label_Minutes.TabIndex = 10;
            this.Label_Minutes.Text = "Minutes";
            // 
            // Numeric_BackupFrequency
            // 
            this.Numeric_BackupFrequency.Location = new System.Drawing.Point(91, 53);
            this.Numeric_BackupFrequency.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.Numeric_BackupFrequency.Name = "Numeric_BackupFrequency";
            this.Numeric_BackupFrequency.Size = new System.Drawing.Size(84, 20);
            this.Numeric_BackupFrequency.TabIndex = 9;
            // 
            // Label_Frequency
            // 
            this.Label_Frequency.AutoSize = true;
            this.Label_Frequency.Location = new System.Drawing.Point(25, 55);
            this.Label_Frequency.Name = "Label_Frequency";
            this.Label_Frequency.Size = new System.Drawing.Size(60, 13);
            this.Label_Frequency.TabIndex = 8;
            this.Label_Frequency.Text = "Frequency:";
            // 
            // Button_BrowseFolder
            // 
            this.Button_BrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_BrowseFolder.Location = new System.Drawing.Point(408, 25);
            this.Button_BrowseFolder.Name = "Button_BrowseFolder";
            this.Button_BrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.Button_BrowseFolder.TabIndex = 7;
            this.Button_BrowseFolder.Text = "Browse...";
            this.Button_BrowseFolder.UseVisualStyleBackColor = true;
            this.Button_BrowseFolder.Click += new System.EventHandler(this.Button_BrowseFolder_Click);
            // 
            // Textbox_BackupFolderPath
            // 
            this.Textbox_BackupFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_BackupFolderPath.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_BackupFolderPath.Location = new System.Drawing.Point(91, 27);
            this.Textbox_BackupFolderPath.Name = "Textbox_BackupFolderPath";
            this.Textbox_BackupFolderPath.ReadOnly = true;
            this.Textbox_BackupFolderPath.Size = new System.Drawing.Size(311, 20);
            this.Textbox_BackupFolderPath.TabIndex = 6;
            // 
            // Label_BackupFolder
            // 
            this.Label_BackupFolder.AutoSize = true;
            this.Label_BackupFolder.Location = new System.Drawing.Point(6, 30);
            this.Label_BackupFolder.Name = "Label_BackupFolder";
            this.Label_BackupFolder.Size = new System.Drawing.Size(79, 13);
            this.Label_BackupFolder.TabIndex = 5;
            this.Label_BackupFolder.Text = "Backup Folder:";
            // 
            // CheckBox_AutomaticBackups
            // 
            this.CheckBox_AutomaticBackups.AutoSize = true;
            this.CheckBox_AutomaticBackups.Location = new System.Drawing.Point(8, 0);
            this.CheckBox_AutomaticBackups.Name = "CheckBox_AutomaticBackups";
            this.CheckBox_AutomaticBackups.Size = new System.Drawing.Size(153, 17);
            this.CheckBox_AutomaticBackups.TabIndex = 4;
            this.CheckBox_AutomaticBackups.Text = "Make Automatic Back-Ups";
            this.CheckBox_AutomaticBackups.UseVisualStyleBackColor = true;
            // 
            // Frm_AutoBackUps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 395);
            this.Controls.Add(this.GroupBox_Backups);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_AutoBackUps";
            this.Text = "Frm_AutoBackUps";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AutoBackUps_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AutoBackUps_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.GroupBox_Backups.ResumeLayout(false);
            this.GroupBox_Backups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MaxBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BackupFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.GroupBox GroupBox_Backups;
        private System.Windows.Forms.Label Label_Minutes;
        private System.Windows.Forms.NumericUpDown Numeric_BackupFrequency;
        private System.Windows.Forms.Label Label_Frequency;
        private System.Windows.Forms.Button Button_BrowseFolder;
        private System.Windows.Forms.TextBox Textbox_BackupFolderPath;
        private System.Windows.Forms.Label Label_BackupFolder;
        private System.Windows.Forms.CheckBox CheckBox_AutomaticBackups;
        private System.Windows.Forms.NumericUpDown Numeric_MaxBackups;
        private System.Windows.Forms.Label Label_MaxBackups;
    }
}