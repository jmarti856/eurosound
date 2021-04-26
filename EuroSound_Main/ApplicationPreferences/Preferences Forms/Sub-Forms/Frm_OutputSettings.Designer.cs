
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_OutputSettings
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
            this.Label_Output = new System.Windows.Forms.Label();
            this.Checkbox_PlaySoundOutput = new System.Windows.Forms.CheckBox();
            this.Textbox_SoundPath = new System.Windows.Forms.TextBox();
            this.Button_BrowseSound = new System.Windows.Forms.Button();
            this.Button_PlaySound = new System.Windows.Forms.Button();
            this.Panel_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Output);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(513, 23);
            this.Panel_Title.TabIndex = 2;
            // 
            // Label_Output
            // 
            this.Label_Output.AutoSize = true;
            this.Label_Output.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Output.Location = new System.Drawing.Point(222, 5);
            this.Label_Output.Name = "Label_Output";
            this.Label_Output.Size = new System.Drawing.Size(46, 16);
            this.Label_Output.TabIndex = 1;
            this.Label_Output.Text = "Output";
            // 
            // Checkbox_PlaySoundOutput
            // 
            this.Checkbox_PlaySoundOutput.AutoSize = true;
            this.Checkbox_PlaySoundOutput.Location = new System.Drawing.Point(25, 132);
            this.Checkbox_PlaySoundOutput.Name = "Checkbox_PlaySoundOutput";
            this.Checkbox_PlaySoundOutput.Size = new System.Drawing.Size(208, 17);
            this.Checkbox_PlaySoundOutput.TabIndex = 3;
            this.Checkbox_PlaySoundOutput.Text = "Play Sound once Output is Completed:";
            this.Checkbox_PlaySoundOutput.UseVisualStyleBackColor = true;
            this.Checkbox_PlaySoundOutput.CheckedChanged += new System.EventHandler(this.Checkbox_PlaySoundOutput_CheckedChanged);
            // 
            // Textbox_SoundPath
            // 
            this.Textbox_SoundPath.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_SoundPath.Location = new System.Drawing.Point(25, 155);
            this.Textbox_SoundPath.Name = "Textbox_SoundPath";
            this.Textbox_SoundPath.ReadOnly = true;
            this.Textbox_SoundPath.Size = new System.Drawing.Size(305, 20);
            this.Textbox_SoundPath.TabIndex = 4;
            // 
            // Button_BrowseSound
            // 
            this.Button_BrowseSound.Location = new System.Drawing.Point(336, 153);
            this.Button_BrowseSound.Name = "Button_BrowseSound";
            this.Button_BrowseSound.Size = new System.Drawing.Size(75, 23);
            this.Button_BrowseSound.TabIndex = 5;
            this.Button_BrowseSound.Text = "Browse...";
            this.Button_BrowseSound.UseVisualStyleBackColor = true;
            this.Button_BrowseSound.Click += new System.EventHandler(this.Button_BrowseSound_Click);
            // 
            // Button_PlaySound
            // 
            this.Button_PlaySound.Location = new System.Drawing.Point(417, 153);
            this.Button_PlaySound.Name = "Button_PlaySound";
            this.Button_PlaySound.Size = new System.Drawing.Size(75, 23);
            this.Button_PlaySound.TabIndex = 6;
            this.Button_PlaySound.Text = "Play";
            this.Button_PlaySound.UseVisualStyleBackColor = true;
            this.Button_PlaySound.Click += new System.EventHandler(this.Button_PlaySound_Click);
            // 
            // Frm_OutputSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 395);
            this.Controls.Add(this.Button_PlaySound);
            this.Controls.Add(this.Button_BrowseSound);
            this.Controls.Add(this.Textbox_SoundPath);
            this.Controls.Add(this.Checkbox_PlaySoundOutput);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_OutputSettings";
            this.Text = "Frm_OutputSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_OutputSettings_FormClosing);
            this.Load += new System.EventHandler(this.Frm_OutputSettings_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Output;
        private System.Windows.Forms.CheckBox Checkbox_PlaySoundOutput;
        private System.Windows.Forms.TextBox Textbox_SoundPath;
        private System.Windows.Forms.Button Button_BrowseSound;
        private System.Windows.Forms.Button Button_PlaySound;
    }
}