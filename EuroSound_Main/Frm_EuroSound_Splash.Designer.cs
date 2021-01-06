
namespace EuroSound_Application
{
    partial class Frm_EuroSound_Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EuroSound_Splash));
            this.PanelStatusBar = new System.Windows.Forms.Panel();
            this.Label_Status = new System.Windows.Forms.Label();
            this.Label_EuroSoundVersion = new System.Windows.Forms.Label();
            this.PictureBox_SplashImage = new System.Windows.Forms.PictureBox();
            this.PanelStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_SplashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelStatusBar
            // 
            this.PanelStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelStatusBar.Controls.Add(this.Label_Status);
            this.PanelStatusBar.Location = new System.Drawing.Point(2, 379);
            this.PanelStatusBar.Name = "PanelStatusBar";
            this.PanelStatusBar.Size = new System.Drawing.Size(502, 25);
            this.PanelStatusBar.TabIndex = 0;
            // 
            // Label_Status
            // 
            this.Label_Status.AutoSize = true;
            this.Label_Status.Location = new System.Drawing.Point(4, 5);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(66, 13);
            this.Label_Status.TabIndex = 1;
            this.Label_Status.Text = "Label Status";
            // 
            // Label_EuroSoundVersion
            // 
            this.Label_EuroSoundVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_EuroSoundVersion.AutoSize = true;
            this.Label_EuroSoundVersion.Location = new System.Drawing.Point(512, 383);
            this.Label_EuroSoundVersion.Name = "Label_EuroSoundVersion";
            this.Label_EuroSoundVersion.Size = new System.Drawing.Size(107, 13);
            this.Label_EuroSoundVersion.TabIndex = 1;
            this.Label_EuroSoundVersion.Text = "EuroSound Version 1";
            // 
            // PictureBox_SplashImage
            // 
            this.PictureBox_SplashImage.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_SplashImage.Image")));
            this.PictureBox_SplashImage.Location = new System.Drawing.Point(1, 1);
            this.PictureBox_SplashImage.Name = "PictureBox_SplashImage";
            this.PictureBox_SplashImage.Size = new System.Drawing.Size(625, 375);
            this.PictureBox_SplashImage.TabIndex = 2;
            this.PictureBox_SplashImage.TabStop = false;
            // 
            // Frm_EuroSound_Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 405);
            this.Controls.Add(this.PictureBox_SplashImage);
            this.Controls.Add(this.Label_EuroSoundVersion);
            this.Controls.Add(this.PanelStatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(627, 405);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(627, 405);
            this.Name = "Frm_EuroSound_Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Splash";
            this.Shown += new System.EventHandler(this.Frm_EuroSound_Splash_Shown);
            this.PanelStatusBar.ResumeLayout(false);
            this.PanelStatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_SplashImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelStatusBar;
        private System.Windows.Forms.Label Label_Status;
        private System.Windows.Forms.Label Label_EuroSoundVersion;
        private System.Windows.Forms.PictureBox PictureBox_SplashImage;
    }
}