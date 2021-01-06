
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_StreamFile
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
            this.Groupbox_ExternalFile = new System.Windows.Forms.GroupBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.Textbox_ExternalFilePath = new System.Windows.Forms.TextBox();
            this.Label_ExternalFilePath = new System.Windows.Forms.Label();
            this.Panel_Title.SuspendLayout();
            this.Groupbox_ExternalFile.SuspendLayout();
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
            this.Panel_Title.TabIndex = 1;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(182, 4);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(127, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "External Stream File";
            // 
            // Groupbox_ExternalFile
            // 
            this.Groupbox_ExternalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_ExternalFile.Controls.Add(this.Button_Search);
            this.Groupbox_ExternalFile.Controls.Add(this.Textbox_ExternalFilePath);
            this.Groupbox_ExternalFile.Controls.Add(this.Label_ExternalFilePath);
            this.Groupbox_ExternalFile.Location = new System.Drawing.Point(12, 126);
            this.Groupbox_ExternalFile.Name = "Groupbox_ExternalFile";
            this.Groupbox_ExternalFile.Size = new System.Drawing.Size(467, 92);
            this.Groupbox_ExternalFile.TabIndex = 2;
            this.Groupbox_ExternalFile.TabStop = false;
            this.Groupbox_ExternalFile.Text = "Stream File:";
            // 
            // Button_Search
            // 
            this.Button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Search.Location = new System.Drawing.Point(437, 37);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(24, 20);
            this.Button_Search.TabIndex = 2;
            this.Button_Search.Text = "...";
            this.Button_Search.UseVisualStyleBackColor = true;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // Textbox_ExternalFilePath
            // 
            this.Textbox_ExternalFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_ExternalFilePath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_ExternalFilePath.Location = new System.Drawing.Point(44, 37);
            this.Textbox_ExternalFilePath.Name = "Textbox_ExternalFilePath";
            this.Textbox_ExternalFilePath.ReadOnly = true;
            this.Textbox_ExternalFilePath.Size = new System.Drawing.Size(387, 20);
            this.Textbox_ExternalFilePath.TabIndex = 1;
            // 
            // Label_ExternalFilePath
            // 
            this.Label_ExternalFilePath.AutoSize = true;
            this.Label_ExternalFilePath.Location = new System.Drawing.Point(6, 40);
            this.Label_ExternalFilePath.Name = "Label_ExternalFilePath";
            this.Label_ExternalFilePath.Size = new System.Drawing.Size(32, 13);
            this.Label_ExternalFilePath.TabIndex = 0;
            this.Label_ExternalFilePath.Text = "Path:";
            // 
            // Frm_StreamFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
            this.Controls.Add(this.Groupbox_ExternalFile);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_StreamFile";
            this.Text = "Frm_StreamFile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_StreamFile_FormClosing);
            this.Load += new System.EventHandler(this.Frm_StreamFile_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.Groupbox_ExternalFile.ResumeLayout(false);
            this.Groupbox_ExternalFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.GroupBox Groupbox_ExternalFile;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.TextBox Textbox_ExternalFilePath;
        private System.Windows.Forms.Label Label_ExternalFilePath;
    }
}