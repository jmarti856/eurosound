
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_System
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
            this.Groupbox_Shell = new System.Windows.Forms.GroupBox();
            this.ButtonRegister_FileTypes = new System.Windows.Forms.Button();
            this.Label_Expl = new System.Windows.Forms.Label();
            this.Groupbox_UseSystemTray = new System.Windows.Forms.GroupBox();
            this.CheckBox_UseSystemTray = new System.Windows.Forms.CheckBox();
            this.Panel_Title.SuspendLayout();
            this.Groupbox_Shell.SuspendLayout();
            this.Groupbox_UseSystemTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(491, 23);
            this.Panel_Title.TabIndex = 2;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(219, 4);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(53, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "System";
            // 
            // Groupbox_Shell
            // 
            this.Groupbox_Shell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Shell.Controls.Add(this.ButtonRegister_FileTypes);
            this.Groupbox_Shell.Controls.Add(this.Label_Expl);
            this.Groupbox_Shell.Location = new System.Drawing.Point(12, 88);
            this.Groupbox_Shell.Name = "Groupbox_Shell";
            this.Groupbox_Shell.Size = new System.Drawing.Size(467, 92);
            this.Groupbox_Shell.TabIndex = 3;
            this.Groupbox_Shell.TabStop = false;
            this.Groupbox_Shell.Text = "Shell:";
            // 
            // ButtonRegister_FileTypes
            // 
            this.ButtonRegister_FileTypes.Location = new System.Drawing.Point(133, 63);
            this.ButtonRegister_FileTypes.Name = "ButtonRegister_FileTypes";
            this.ButtonRegister_FileTypes.Size = new System.Drawing.Size(207, 23);
            this.ButtonRegister_FileTypes.TabIndex = 1;
            this.ButtonRegister_FileTypes.Text = "Register Shell File-Types";
            this.ButtonRegister_FileTypes.UseVisualStyleBackColor = true;
            this.ButtonRegister_FileTypes.Click += new System.EventHandler(this.ButtonRegister_FileTypes_Click);
            // 
            // Label_Expl
            // 
            this.Label_Expl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Expl.AutoSize = true;
            this.Label_Expl.Location = new System.Drawing.Point(6, 16);
            this.Label_Expl.Name = "Label_Expl";
            this.Label_Expl.Size = new System.Drawing.Size(452, 26);
            this.Label_Expl.TabIndex = 0;
            this.Label_Expl.Text = "ESFs can be loaded into a running EuroSound by double-clicking on them. If this d" +
    "oesn\'t work,\r\nclick on the button below to register the files that EuroSound use" +
    "s with the system.";
            // 
            // Groupbox_UseSystemTray
            // 
            this.Groupbox_UseSystemTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_UseSystemTray.Controls.Add(this.CheckBox_UseSystemTray);
            this.Groupbox_UseSystemTray.Location = new System.Drawing.Point(12, 186);
            this.Groupbox_UseSystemTray.Name = "Groupbox_UseSystemTray";
            this.Groupbox_UseSystemTray.Size = new System.Drawing.Size(467, 74);
            this.Groupbox_UseSystemTray.TabIndex = 4;
            this.Groupbox_UseSystemTray.TabStop = false;
            this.Groupbox_UseSystemTray.Text = "System-Tray:";
            // 
            // CheckBox_UseSystemTray
            // 
            this.CheckBox_UseSystemTray.AutoSize = true;
            this.CheckBox_UseSystemTray.Location = new System.Drawing.Point(92, 33);
            this.CheckBox_UseSystemTray.Name = "CheckBox_UseSystemTray";
            this.CheckBox_UseSystemTray.Size = new System.Drawing.Size(288, 17);
            this.CheckBox_UseSystemTray.TabIndex = 0;
            this.CheckBox_UseSystemTray.Text = "Minimise Eurosound to System-Tray instead of TaskBar.";
            this.CheckBox_UseSystemTray.UseVisualStyleBackColor = true;
            // 
            // Frm_System
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
            this.Controls.Add(this.Groupbox_UseSystemTray);
            this.Controls.Add(this.Groupbox_Shell);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_System";
            this.Text = "Frm_System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_System_FormClosing);
            this.Load += new System.EventHandler(this.Frm_System_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.Groupbox_Shell.ResumeLayout(false);
            this.Groupbox_Shell.PerformLayout();
            this.Groupbox_UseSystemTray.ResumeLayout(false);
            this.Groupbox_UseSystemTray.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.GroupBox Groupbox_Shell;
        private System.Windows.Forms.Button ButtonRegister_FileTypes;
        private System.Windows.Forms.Label Label_Expl;
        private System.Windows.Forms.GroupBox Groupbox_UseSystemTray;
        private System.Windows.Forms.CheckBox CheckBox_UseSystemTray;
    }
}