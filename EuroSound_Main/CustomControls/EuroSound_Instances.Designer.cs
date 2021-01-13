
namespace EuroSound_Application.CustomControls.ProgramInstancesForm
{
    partial class EuroSound_Instances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EuroSound_Instances));
            this.Label_Warning = new System.Windows.Forms.Label();
            this.Button_NoStart = new System.Windows.Forms.Button();
            this.Button_ShowOtherInstance = new System.Windows.Forms.Button();
            this.Button_StartAnyway = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_Warning
            // 
            this.Label_Warning.AutoSize = true;
            this.Label_Warning.Location = new System.Drawing.Point(25, 21);
            this.Label_Warning.Name = "Label_Warning";
            this.Label_Warning.Size = new System.Drawing.Size(245, 65);
            this.Label_Warning.TabIndex = 0;
            this.Label_Warning.Text = "Warning!\r\nYou are already running an instance of EuroSound\r\n\r\n\r\nWhat do you want " +
    "to do?";
            this.Label_Warning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_NoStart
            // 
            this.Button_NoStart.Location = new System.Drawing.Point(28, 111);
            this.Button_NoStart.Name = "Button_NoStart";
            this.Button_NoStart.Size = new System.Drawing.Size(242, 23);
            this.Button_NoStart.TabIndex = 1;
            this.Button_NoStart.Text = "Don\'t start another instance of EuroSound";
            this.Button_NoStart.UseVisualStyleBackColor = true;
            this.Button_NoStart.Click += new System.EventHandler(this.Button_NoStart_Click);
            // 
            // Button_ShowOtherInstance
            // 
            this.Button_ShowOtherInstance.Location = new System.Drawing.Point(28, 140);
            this.Button_ShowOtherInstance.Name = "Button_ShowOtherInstance";
            this.Button_ShowOtherInstance.Size = new System.Drawing.Size(242, 23);
            this.Button_ShowOtherInstance.TabIndex = 2;
            this.Button_ShowOtherInstance.Text = "Show the other instance EuroSound";
            this.Button_ShowOtherInstance.UseVisualStyleBackColor = true;
            this.Button_ShowOtherInstance.Click += new System.EventHandler(this.Button_ShowOtherInstance_Click);
            // 
            // Button_StartAnyway
            // 
            this.Button_StartAnyway.Location = new System.Drawing.Point(28, 169);
            this.Button_StartAnyway.Name = "Button_StartAnyway";
            this.Button_StartAnyway.Size = new System.Drawing.Size(242, 23);
            this.Button_StartAnyway.TabIndex = 3;
            this.Button_StartAnyway.Text = "Start another instance of EuroSound anyway";
            this.Button_StartAnyway.UseVisualStyleBackColor = true;
            this.Button_StartAnyway.Click += new System.EventHandler(this.Button_StartAnyway_Click);
            // 
            // EuroSound_Instances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 221);
            this.Controls.Add(this.Button_StartAnyway);
            this.Controls.Add(this.Button_ShowOtherInstance);
            this.Controls.Add(this.Button_NoStart);
            this.Controls.Add(this.Label_Warning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_Instances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EuroSound Warning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Warning;
        private System.Windows.Forms.Button Button_NoStart;
        private System.Windows.Forms.Button Button_ShowOtherInstance;
        private System.Windows.Forms.Button Button_StartAnyway;
    }
}