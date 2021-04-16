
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_Profiles
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
            this.FontDialogTreeView = new System.Windows.Forms.FontDialog();
            this.Label_AvailableProfiles = new System.Windows.Forms.Label();
            this.RichTextbox_ProfilesInfo = new System.Windows.Forms.RichTextBox();
            this.Label_ProfileInfo = new System.Windows.Forms.Label();
            this.ListView_Profiles = new System.Windows.Forms.ListView();
            this.Panel_Title.SuspendLayout();
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
            this.Label_Title.Location = new System.Drawing.Point(222, 5);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(46, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Profile";
            // 
            // Label_AvailableProfiles
            // 
            this.Label_AvailableProfiles.AutoSize = true;
            this.Label_AvailableProfiles.Location = new System.Drawing.Point(12, 35);
            this.Label_AvailableProfiles.Name = "Label_AvailableProfiles";
            this.Label_AvailableProfiles.Size = new System.Drawing.Size(90, 13);
            this.Label_AvailableProfiles.TabIndex = 0;
            this.Label_AvailableProfiles.Text = "Available Profiles:";
            // 
            // RichTextbox_ProfilesInfo
            // 
            this.RichTextbox_ProfilesInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextbox_ProfilesInfo.Location = new System.Drawing.Point(184, 51);
            this.RichTextbox_ProfilesInfo.Name = "RichTextbox_ProfilesInfo";
            this.RichTextbox_ProfilesInfo.ReadOnly = true;
            this.RichTextbox_ProfilesInfo.Size = new System.Drawing.Size(295, 276);
            this.RichTextbox_ProfilesInfo.TabIndex = 3;
            this.RichTextbox_ProfilesInfo.Text = "";
            // 
            // Label_ProfileInfo
            // 
            this.Label_ProfileInfo.AutoSize = true;
            this.Label_ProfileInfo.Location = new System.Drawing.Point(181, 35);
            this.Label_ProfileInfo.Name = "Label_ProfileInfo";
            this.Label_ProfileInfo.Size = new System.Drawing.Size(28, 13);
            this.Label_ProfileInfo.TabIndex = 2;
            this.Label_ProfileInfo.Text = "Info:";
            // 
            // ListView_Profiles
            // 
            this.ListView_Profiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListView_Profiles.HideSelection = false;
            this.ListView_Profiles.Location = new System.Drawing.Point(12, 51);
            this.ListView_Profiles.MultiSelect = false;
            this.ListView_Profiles.Name = "ListView_Profiles";
            this.ListView_Profiles.Size = new System.Drawing.Size(166, 276);
            this.ListView_Profiles.TabIndex = 1;
            this.ListView_Profiles.UseCompatibleStateImageBehavior = false;
            this.ListView_Profiles.View = System.Windows.Forms.View.List;
            this.ListView_Profiles.SelectedIndexChanged += new System.EventHandler(this.ListView_Profiles_SelectedIndexChanged);
            this.ListView_Profiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListView_Profiles_MouseUp);
            // 
            // Frm_Profiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
            this.Controls.Add(this.ListView_Profiles);
            this.Controls.Add(this.Label_ProfileInfo);
            this.Controls.Add(this.RichTextbox_ProfilesInfo);
            this.Controls.Add(this.Label_AvailableProfiles);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Profiles";
            this.Text = "Frm_TreeViewPrefs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TreeViewPrefs_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TreeViewPrefs_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.FontDialog FontDialogTreeView;
        private System.Windows.Forms.Label Label_AvailableProfiles;
        private System.Windows.Forms.RichTextBox RichTextbox_ProfilesInfo;
        private System.Windows.Forms.Label Label_ProfileInfo;
        private System.Windows.Forms.ListView ListView_Profiles;
    }
}