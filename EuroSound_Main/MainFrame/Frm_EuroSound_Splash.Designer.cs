
namespace EuroSound_Application.SplashForm
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
            this.PanelBase = new System.Windows.Forms.Panel();
            this.Label_EuroSoundVersion = new System.Windows.Forms.Label();
            this.PanelStatusBar = new System.Windows.Forms.Panel();
            this.Label_Status = new System.Windows.Forms.Label();
            this.PictureBox_SplashImage = new System.Windows.Forms.PictureBox();
            this.PanelBase.SuspendLayout();
            this.PanelStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_SplashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelBase
            // 
            this.PanelBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBase.Controls.Add(this.Label_EuroSoundVersion);
            this.PanelBase.Controls.Add(this.PanelStatusBar);
            this.PanelBase.Controls.Add(this.PictureBox_SplashImage);
            this.PanelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBase.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PanelBase.Location = new System.Drawing.Point(0, 0);
            this.PanelBase.Name = "PanelBase";
            this.PanelBase.Size = new System.Drawing.Size(627, 405);
            this.PanelBase.TabIndex = 1;
            // 
            // Label_EuroSoundVersion
            // 
            this.Label_EuroSoundVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_EuroSoundVersion.AutoSize = true;
            this.Label_EuroSoundVersion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Label_EuroSoundVersion.Location = new System.Drawing.Point(515, 382);
            this.Label_EuroSoundVersion.Name = "Label_EuroSoundVersion";
            this.Label_EuroSoundVersion.Size = new System.Drawing.Size(107, 13);
            this.Label_EuroSoundVersion.TabIndex = 5;
            this.Label_EuroSoundVersion.Text = "EuroSound Version 1";
            // 
            // PanelStatusBar
            // 
            this.PanelStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelStatusBar.Controls.Add(this.Label_Status);
            this.PanelStatusBar.Location = new System.Drawing.Point(2, 377);
            this.PanelStatusBar.Name = "PanelStatusBar";
            this.PanelStatusBar.Size = new System.Drawing.Size(507, 25);
            this.PanelStatusBar.TabIndex = 4;
            // 
            // Label_Status
            // 
            this.Label_Status.AutoSize = true;
            this.Label_Status.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Label_Status.Location = new System.Drawing.Point(3, 4);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(66, 13);
            this.Label_Status.TabIndex = 1;
            this.Label_Status.Text = "Label Status";
            // 
            // PictureBox_SplashImage
            // 
            this.PictureBox_SplashImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureBox_SplashImage.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_SplashImage.Image")));
            this.PictureBox_SplashImage.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_SplashImage.Name = "PictureBox_SplashImage";
            this.PictureBox_SplashImage.Size = new System.Drawing.Size(625, 375);
            this.PictureBox_SplashImage.TabIndex = 3;
            this.PictureBox_SplashImage.TabStop = false;
            // 
            // Frm_EuroSound_Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 405);
            this.Controls.Add(this.PanelBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(627, 405);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(627, 405);
            this.Name = "Frm_EuroSound_Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Splash";
            this.Text = "Frm_Splash";
            this.Shown += new System.EventHandler(this.Frm_EuroSound_Splash_Shown);
            this.PanelBase.ResumeLayout(false);
            this.PanelBase.PerformLayout();
            this.PanelStatusBar.ResumeLayout(false);
            this.PanelStatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_SplashImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelBase;
        private System.Windows.Forms.Label Label_EuroSoundVersion;
        private System.Windows.Forms.Panel PanelStatusBar;
        private System.Windows.Forms.Label Label_Status;
        private System.Windows.Forms.PictureBox PictureBox_SplashImage;
    }
}