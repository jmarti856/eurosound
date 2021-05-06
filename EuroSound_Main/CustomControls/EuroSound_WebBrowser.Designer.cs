
namespace EuroSound_Application.CustomControls.WebBrowser
{
    partial class EuroSound_WebBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EuroSound_WebBrowser));
            this.WebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // WebBrowser
            // 
            this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser.Name = "WebBrowser";
            this.WebBrowser.ScriptErrorsSuppressed = true;
            this.WebBrowser.Size = new System.Drawing.Size(967, 577);
            this.WebBrowser.TabIndex = 0;
            // 
            // EuroSound_WebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 577);
            this.Controls.Add(this.WebBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EuroSound_WebBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "WebBrowser";
            this.Text = "Web Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EuroSound_WebBrowser_FormClosing);
            this.Load += new System.EventHandler(this.EuroSound_WebBrowser_Load);
            this.Shown += new System.EventHandler(this.EuroSound_WebBrowser_Shown);
            this.SizeChanged += new System.EventHandler(this.EuroSound_WebBrowser_SizeChanged);
            this.Enter += new System.EventHandler(this.EuroSound_WebBrowser_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser WebBrowser;
    }
}