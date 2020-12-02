namespace EuroSound
{
    partial class Frm_SampleProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SampleProperties));
            this.groupbox_properties = new System.Windows.Forms.GroupBox();
            this.Numeric_BaseVolume = new System.Windows.Forms.NumericUpDown();
            this.Label_BaseVolume = new System.Windows.Forms.Label();
            this.numeric_randomPitchOffset = new System.Windows.Forms.NumericUpDown();
            this.Label_RandomPitchOffset = new System.Windows.Forms.Label();
            this.numeric_randompan = new System.Windows.Forms.NumericUpDown();
            this.label_randompan = new System.Windows.Forms.Label();
            this.numeric_pan = new System.Windows.Forms.NumericUpDown();
            this.label_pan = new System.Windows.Forms.Label();
            this.numeric_randomvolumeoffset = new System.Windows.Forms.NumericUpDown();
            this.label_randomvolumeoffset = new System.Windows.Forms.Label();
            this.numeric_pitchoffset = new System.Windows.Forms.NumericUpDown();
            this.label_pitchoffset = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_StopAudio = new System.Windows.Forms.Button();
            this.Button_PlayAudio = new System.Windows.Forms.Button();
            this.button_RemoveAudio = new System.Windows.Forms.Button();
            this.Button_LoadAudio = new System.Windows.Forms.Button();
            this.Textbox_MediaName = new System.Windows.Forms.TextBox();
            this.Label_MediaAudio = new System.Windows.Forms.Label();
            this.groupbox_properties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomPitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randompan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomvolumeoffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pitchoffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupbox_properties
            // 
            this.groupbox_properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbox_properties.Controls.Add(this.Numeric_BaseVolume);
            this.groupbox_properties.Controls.Add(this.Label_BaseVolume);
            this.groupbox_properties.Controls.Add(this.numeric_randomPitchOffset);
            this.groupbox_properties.Controls.Add(this.Label_RandomPitchOffset);
            this.groupbox_properties.Controls.Add(this.numeric_randompan);
            this.groupbox_properties.Controls.Add(this.label_randompan);
            this.groupbox_properties.Controls.Add(this.numeric_pan);
            this.groupbox_properties.Controls.Add(this.label_pan);
            this.groupbox_properties.Controls.Add(this.numeric_randomvolumeoffset);
            this.groupbox_properties.Controls.Add(this.label_randomvolumeoffset);
            this.groupbox_properties.Controls.Add(this.numeric_pitchoffset);
            this.groupbox_properties.Controls.Add(this.label_pitchoffset);
            this.groupbox_properties.Location = new System.Drawing.Point(12, 12);
            this.groupbox_properties.Name = "groupbox_properties";
            this.groupbox_properties.Size = new System.Drawing.Size(399, 182);
            this.groupbox_properties.TabIndex = 0;
            this.groupbox_properties.TabStop = false;
            this.groupbox_properties.Text = "Properties";
            // 
            // Numeric_BaseVolume
            // 
            this.Numeric_BaseVolume.Location = new System.Drawing.Point(126, 71);
            this.Numeric_BaseVolume.Name = "Numeric_BaseVolume";
            this.Numeric_BaseVolume.Size = new System.Drawing.Size(120, 20);
            this.Numeric_BaseVolume.TabIndex = 8;
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(48, 73);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 7;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // numeric_randomPitchOffset
            // 
            this.numeric_randomPitchOffset.Location = new System.Drawing.Point(126, 45);
            this.numeric_randomPitchOffset.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_randomPitchOffset.Name = "numeric_randomPitchOffset";
            this.numeric_randomPitchOffset.Size = new System.Drawing.Size(120, 20);
            this.numeric_randomPitchOffset.TabIndex = 6;
            // 
            // Label_RandomPitchOffset
            // 
            this.Label_RandomPitchOffset.AutoSize = true;
            this.Label_RandomPitchOffset.Location = new System.Drawing.Point(12, 52);
            this.Label_RandomPitchOffset.Name = "Label_RandomPitchOffset";
            this.Label_RandomPitchOffset.Size = new System.Drawing.Size(108, 13);
            this.Label_RandomPitchOffset.TabIndex = 5;
            this.Label_RandomPitchOffset.Text = "Random Pitch Offset:";
            // 
            // numeric_randompan
            // 
            this.numeric_randompan.Location = new System.Drawing.Point(126, 149);
            this.numeric_randompan.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_randompan.Name = "numeric_randompan";
            this.numeric_randompan.Size = new System.Drawing.Size(120, 20);
            this.numeric_randompan.TabIndex = 14;
            // 
            // label_randompan
            // 
            this.label_randompan.AutoSize = true;
            this.label_randompan.Location = new System.Drawing.Point(48, 151);
            this.label_randompan.Name = "label_randompan";
            this.label_randompan.Size = new System.Drawing.Size(72, 13);
            this.label_randompan.TabIndex = 13;
            this.label_randompan.Text = "Random Pan:";
            // 
            // numeric_pan
            // 
            this.numeric_pan.Location = new System.Drawing.Point(126, 123);
            this.numeric_pan.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_pan.Name = "numeric_pan";
            this.numeric_pan.Size = new System.Drawing.Size(120, 20);
            this.numeric_pan.TabIndex = 12;
            // 
            // label_pan
            // 
            this.label_pan.AutoSize = true;
            this.label_pan.Location = new System.Drawing.Point(91, 125);
            this.label_pan.Name = "label_pan";
            this.label_pan.Size = new System.Drawing.Size(29, 13);
            this.label_pan.TabIndex = 11;
            this.label_pan.Text = "Pan:";
            // 
            // numeric_randomvolumeoffset
            // 
            this.numeric_randomvolumeoffset.Location = new System.Drawing.Point(126, 97);
            this.numeric_randomvolumeoffset.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_randomvolumeoffset.Name = "numeric_randomvolumeoffset";
            this.numeric_randomvolumeoffset.Size = new System.Drawing.Size(120, 20);
            this.numeric_randomvolumeoffset.TabIndex = 10;
            // 
            // label_randomvolumeoffset
            // 
            this.label_randomvolumeoffset.AutoSize = true;
            this.label_randomvolumeoffset.Location = new System.Drawing.Point(1, 99);
            this.label_randomvolumeoffset.Name = "label_randomvolumeoffset";
            this.label_randomvolumeoffset.Size = new System.Drawing.Size(119, 13);
            this.label_randomvolumeoffset.TabIndex = 9;
            this.label_randomvolumeoffset.Text = "Random Volume Offset:";
            // 
            // numeric_pitchoffset
            // 
            this.numeric_pitchoffset.Location = new System.Drawing.Point(126, 19);
            this.numeric_pitchoffset.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_pitchoffset.Name = "numeric_pitchoffset";
            this.numeric_pitchoffset.Size = new System.Drawing.Size(120, 20);
            this.numeric_pitchoffset.TabIndex = 4;
            // 
            // label_pitchoffset
            // 
            this.label_pitchoffset.AutoSize = true;
            this.label_pitchoffset.Location = new System.Drawing.Point(55, 21);
            this.label_pitchoffset.Name = "label_pitchoffset";
            this.label_pitchoffset.Size = new System.Drawing.Size(65, 13);
            this.label_pitchoffset.TabIndex = 3;
            this.label_pitchoffset.Text = "Pitch Offset:";
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(258, 292);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.Location = new System.Drawing.Point(339, 292);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Button_StopAudio);
            this.groupBox1.Controls.Add(this.Button_PlayAudio);
            this.groupBox1.Controls.Add(this.button_RemoveAudio);
            this.groupBox1.Controls.Add(this.Button_LoadAudio);
            this.groupBox1.Controls.Add(this.Textbox_MediaName);
            this.groupBox1.Controls.Add(this.Label_MediaAudio);
            this.groupBox1.Location = new System.Drawing.Point(12, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 86);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Media";
            // 
            // Button_StopAudio
            // 
            this.Button_StopAudio.Location = new System.Drawing.Point(207, 48);
            this.Button_StopAudio.Name = "Button_StopAudio";
            this.Button_StopAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_StopAudio.TabIndex = 4;
            this.Button_StopAudio.Text = "Stop Audio";
            this.Button_StopAudio.UseVisualStyleBackColor = true;
            this.Button_StopAudio.Click += new System.EventHandler(this.Button_StopAudio_Click);
            // 
            // Button_PlayAudio
            // 
            this.Button_PlayAudio.Location = new System.Drawing.Point(126, 48);
            this.Button_PlayAudio.Name = "Button_PlayAudio";
            this.Button_PlayAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_PlayAudio.TabIndex = 3;
            this.Button_PlayAudio.Text = "Play Audio";
            this.Button_PlayAudio.UseVisualStyleBackColor = true;
            this.Button_PlayAudio.Click += new System.EventHandler(this.Button_PlayAudio_Click);
            // 
            // button_RemoveAudio
            // 
            this.button_RemoveAudio.Location = new System.Drawing.Point(288, 48);
            this.button_RemoveAudio.Name = "button_RemoveAudio";
            this.button_RemoveAudio.Size = new System.Drawing.Size(105, 23);
            this.button_RemoveAudio.TabIndex = 5;
            this.button_RemoveAudio.Text = "Remove Audio";
            this.button_RemoveAudio.UseVisualStyleBackColor = true;
            this.button_RemoveAudio.Click += new System.EventHandler(this.Button_RemoveAudio_Click);
            // 
            // Button_LoadAudio
            // 
            this.Button_LoadAudio.Location = new System.Drawing.Point(369, 22);
            this.Button_LoadAudio.Name = "Button_LoadAudio";
            this.Button_LoadAudio.Size = new System.Drawing.Size(24, 20);
            this.Button_LoadAudio.TabIndex = 2;
            this.Button_LoadAudio.Text = "...";
            this.Button_LoadAudio.UseVisualStyleBackColor = true;
            this.Button_LoadAudio.Click += new System.EventHandler(this.Button_LoadAudio_Click);
            // 
            // Textbox_MediaName
            // 
            this.Textbox_MediaName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_MediaName.Location = new System.Drawing.Point(49, 22);
            this.Textbox_MediaName.Name = "Textbox_MediaName";
            this.Textbox_MediaName.ReadOnly = true;
            this.Textbox_MediaName.Size = new System.Drawing.Size(314, 20);
            this.Textbox_MediaName.TabIndex = 1;
            this.Textbox_MediaName.Text = "<NO AUDIO>";
            // 
            // Label_MediaAudio
            // 
            this.Label_MediaAudio.AutoSize = true;
            this.Label_MediaAudio.Location = new System.Drawing.Point(6, 25);
            this.Label_MediaAudio.Name = "Label_MediaAudio";
            this.Label_MediaAudio.Size = new System.Drawing.Size(37, 13);
            this.Label_MediaAudio.TabIndex = 0;
            this.Label_MediaAudio.Text = "Audio:";
            // 
            // Frm_SampleProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 327);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.groupbox_properties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SampleProperties";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_SampleProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SampleProperties_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SampleProperties_Load);
            this.groupbox_properties.ResumeLayout(false);
            this.groupbox_properties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomPitchOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randompan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomvolumeoffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pitchoffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupbox_properties;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.NumericUpDown numeric_pitchoffset;
        private System.Windows.Forms.Label label_pitchoffset;
        private System.Windows.Forms.NumericUpDown numeric_randompan;
        private System.Windows.Forms.Label label_randompan;
        private System.Windows.Forms.NumericUpDown numeric_pan;
        private System.Windows.Forms.Label label_pan;
        private System.Windows.Forms.NumericUpDown numeric_randomvolumeoffset;
        private System.Windows.Forms.Label label_randomvolumeoffset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_RemoveAudio;
        private System.Windows.Forms.Button Button_LoadAudio;
        private System.Windows.Forms.TextBox Textbox_MediaName;
        private System.Windows.Forms.Label Label_MediaAudio;
        private System.Windows.Forms.Button Button_PlayAudio;
        private System.Windows.Forms.Button Button_StopAudio;
        private System.Windows.Forms.NumericUpDown Numeric_BaseVolume;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.NumericUpDown numeric_randomPitchOffset;
        private System.Windows.Forms.Label Label_RandomPitchOffset;
    }
}