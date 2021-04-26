
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_SoxPrefs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SoxPrefs));
            this.Panel_Title = new System.Windows.Forms.Panel();
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_Description = new System.Windows.Forms.Label();
            this.DownloadLinkSox = new System.Windows.Forms.LinkLabel();
            this.Label_Download = new System.Windows.Forms.Label();
            this.GroupboxSoxPath = new System.Windows.Forms.GroupBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.Textbox_SoXPath = new System.Windows.Forms.TextBox();
            this.LabelSoXPath = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Panel_Title.SuspendLayout();
            this.GroupboxSoxPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.Label_Title.Location = new System.Drawing.Point(172, 4);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(147, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "SoX (Sound eXchange)";
            // 
            // Label_Description
            // 
            this.Label_Description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description.AutoSize = true;
            this.Label_Description.Location = new System.Drawing.Point(12, 42);
            this.Label_Description.MaximumSize = new System.Drawing.Size(450, 0);
            this.Label_Description.Name = "Label_Description";
            this.Label_Description.Size = new System.Drawing.Size(449, 39);
            this.Label_Description.TabIndex = 3;
            this.Label_Description.Text = resources.GetString("Label_Description.Text");
            // 
            // DownloadLinkSox
            // 
            this.DownloadLinkSox.AutoSize = true;
            this.DownloadLinkSox.Location = new System.Drawing.Point(263, 68);
            this.DownloadLinkSox.Name = "DownloadLinkSox";
            this.DownloadLinkSox.Size = new System.Drawing.Size(136, 13);
            this.DownloadLinkSox.TabIndex = 4;
            this.DownloadLinkSox.TabStop = true;
            this.DownloadLinkSox.Text = "http://sox.sourceforge.net/";
            this.DownloadLinkSox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DownloadLinkSox_LinkClicked);
            // 
            // Label_Download
            // 
            this.Label_Download.AutoSize = true;
            this.Label_Download.Location = new System.Drawing.Point(12, 90);
            this.Label_Download.Name = "Label_Download";
            this.Label_Download.Size = new System.Drawing.Size(0, 13);
            this.Label_Download.TabIndex = 5;
            // 
            // GroupboxSoxPath
            // 
            this.GroupboxSoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxSoxPath.Controls.Add(this.Button_Search);
            this.GroupboxSoxPath.Controls.Add(this.Textbox_SoXPath);
            this.GroupboxSoxPath.Controls.Add(this.LabelSoXPath);
            this.GroupboxSoxPath.Location = new System.Drawing.Point(12, 106);
            this.GroupboxSoxPath.Name = "GroupboxSoxPath";
            this.GroupboxSoxPath.Size = new System.Drawing.Size(489, 79);
            this.GroupboxSoxPath.TabIndex = 6;
            this.GroupboxSoxPath.TabStop = false;
            this.GroupboxSoxPath.Text = "SoX Path:";
            // 
            // Button_Search
            // 
            this.Button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Search.Location = new System.Drawing.Point(458, 29);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(25, 23);
            this.Button_Search.TabIndex = 2;
            this.Button_Search.Text = "...";
            this.Button_Search.UseVisualStyleBackColor = true;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // Textbox_SoXPath
            // 
            this.Textbox_SoXPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SoXPath.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_SoXPath.Location = new System.Drawing.Point(44, 31);
            this.Textbox_SoXPath.Name = "Textbox_SoXPath";
            this.Textbox_SoXPath.ReadOnly = true;
            this.Textbox_SoXPath.Size = new System.Drawing.Size(408, 20);
            this.Textbox_SoXPath.TabIndex = 1;
            // 
            // LabelSoXPath
            // 
            this.LabelSoXPath.AutoSize = true;
            this.LabelSoXPath.Location = new System.Drawing.Point(6, 34);
            this.LabelSoXPath.Name = "LabelSoXPath";
            this.LabelSoXPath.Size = new System.Drawing.Size(32, 13);
            this.LabelSoXPath.TabIndex = 0;
            this.LabelSoXPath.Text = "Path:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(115, 209);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Frm_SoxPrefs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 395);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.GroupboxSoxPath);
            this.Controls.Add(this.DownloadLinkSox);
            this.Controls.Add(this.Label_Download);
            this.Controls.Add(this.Label_Description);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_SoxPrefs";
            this.Text = "Frm_SoxPrefs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SoxPrefs_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SoxPrefs_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.GroupboxSoxPath.ResumeLayout(false);
            this.GroupboxSoxPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Description;
        private System.Windows.Forms.LinkLabel DownloadLinkSox;
        private System.Windows.Forms.Label Label_Download;
        private System.Windows.Forms.GroupBox GroupboxSoxPath;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.TextBox Textbox_SoXPath;
        private System.Windows.Forms.Label LabelSoXPath;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}