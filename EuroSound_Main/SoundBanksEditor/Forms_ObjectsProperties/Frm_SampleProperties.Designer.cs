namespace EuroSound_Application
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
            this.GroupBoxMedia = new System.Windows.Forms.GroupBox();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Button_PlayAudio = new System.Windows.Forms.Button();
            this.Label_AudioSelected = new System.Windows.Forms.Label();
            this.Combobox_SelectedAudio = new System.Windows.Forms.ComboBox();
            this.Checkbox_IsStreamedSound = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Combobox_Hashcode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Label_SubSFX = new System.Windows.Forms.Label();
            this.groupbox_properties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomPitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randompan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomvolumeoffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pitchoffset)).BeginInit();
            this.GroupBoxMedia.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupbox_properties.Size = new System.Drawing.Size(450, 108);
            this.groupbox_properties.TabIndex = 0;
            this.groupbox_properties.TabStop = false;
            this.groupbox_properties.Text = "Properties:";
            // 
            // Numeric_BaseVolume
            // 
            this.Numeric_BaseVolume.Location = new System.Drawing.Point(94, 45);
            this.Numeric_BaseVolume.Name = "Numeric_BaseVolume";
            this.Numeric_BaseVolume.Size = new System.Drawing.Size(94, 20);
            this.Numeric_BaseVolume.TabIndex = 8;
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(16, 47);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 7;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // numeric_randomPitchOffset
            // 
            this.numeric_randomPitchOffset.Location = new System.Drawing.Point(342, 19);
            this.numeric_randomPitchOffset.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_randomPitchOffset.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numeric_randomPitchOffset.Name = "numeric_randomPitchOffset";
            this.numeric_randomPitchOffset.Size = new System.Drawing.Size(94, 20);
            this.numeric_randomPitchOffset.TabIndex = 6;
            // 
            // Label_RandomPitchOffset
            // 
            this.Label_RandomPitchOffset.AutoSize = true;
            this.Label_RandomPitchOffset.Location = new System.Drawing.Point(228, 21);
            this.Label_RandomPitchOffset.Name = "Label_RandomPitchOffset";
            this.Label_RandomPitchOffset.Size = new System.Drawing.Size(108, 13);
            this.Label_RandomPitchOffset.TabIndex = 5;
            this.Label_RandomPitchOffset.Text = "Random Pitch Offset:";
            // 
            // numeric_randompan
            // 
            this.numeric_randompan.Location = new System.Drawing.Point(342, 71);
            this.numeric_randompan.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_randompan.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numeric_randompan.Name = "numeric_randompan";
            this.numeric_randompan.Size = new System.Drawing.Size(94, 20);
            this.numeric_randompan.TabIndex = 14;
            // 
            // label_randompan
            // 
            this.label_randompan.AutoSize = true;
            this.label_randompan.Location = new System.Drawing.Point(264, 71);
            this.label_randompan.Name = "label_randompan";
            this.label_randompan.Size = new System.Drawing.Size(72, 13);
            this.label_randompan.TabIndex = 13;
            this.label_randompan.Text = "Random Pan:";
            // 
            // numeric_pan
            // 
            this.numeric_pan.Location = new System.Drawing.Point(94, 71);
            this.numeric_pan.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numeric_pan.Name = "numeric_pan";
            this.numeric_pan.Size = new System.Drawing.Size(94, 20);
            this.numeric_pan.TabIndex = 12;
            // 
            // label_pan
            // 
            this.label_pan.AutoSize = true;
            this.label_pan.Location = new System.Drawing.Point(59, 73);
            this.label_pan.Name = "label_pan";
            this.label_pan.Size = new System.Drawing.Size(29, 13);
            this.label_pan.TabIndex = 11;
            this.label_pan.Text = "Pan:";
            // 
            // numeric_randomvolumeoffset
            // 
            this.numeric_randomvolumeoffset.Location = new System.Drawing.Point(342, 45);
            this.numeric_randomvolumeoffset.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_randomvolumeoffset.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numeric_randomvolumeoffset.Name = "numeric_randomvolumeoffset";
            this.numeric_randomvolumeoffset.Size = new System.Drawing.Size(94, 20);
            this.numeric_randomvolumeoffset.TabIndex = 10;
            // 
            // label_randomvolumeoffset
            // 
            this.label_randomvolumeoffset.AutoSize = true;
            this.label_randomvolumeoffset.Location = new System.Drawing.Point(217, 47);
            this.label_randomvolumeoffset.Name = "label_randomvolumeoffset";
            this.label_randomvolumeoffset.Size = new System.Drawing.Size(119, 13);
            this.label_randomvolumeoffset.TabIndex = 9;
            this.label_randomvolumeoffset.Text = "Random Volume Offset:";
            // 
            // numeric_pitchoffset
            // 
            this.numeric_pitchoffset.Location = new System.Drawing.Point(94, 19);
            this.numeric_pitchoffset.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numeric_pitchoffset.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numeric_pitchoffset.Name = "numeric_pitchoffset";
            this.numeric_pitchoffset.Size = new System.Drawing.Size(94, 20);
            this.numeric_pitchoffset.TabIndex = 4;
            // 
            // label_pitchoffset
            // 
            this.label_pitchoffset.AutoSize = true;
            this.label_pitchoffset.Location = new System.Drawing.Point(23, 21);
            this.label_pitchoffset.Name = "label_pitchoffset";
            this.label_pitchoffset.Size = new System.Drawing.Size(65, 13);
            this.label_pitchoffset.TabIndex = 3;
            this.label_pitchoffset.Text = "Pitch Offset:";
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(306, 348);
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
            this.button_cancel.Location = new System.Drawing.Point(387, 348);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // GroupBoxMedia
            // 
            this.GroupBoxMedia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxMedia.Controls.Add(this.Button_Stop);
            this.GroupBoxMedia.Controls.Add(this.Button_PlayAudio);
            this.GroupBoxMedia.Controls.Add(this.Label_AudioSelected);
            this.GroupBoxMedia.Controls.Add(this.Combobox_SelectedAudio);
            this.GroupBoxMedia.Controls.Add(this.Checkbox_IsStreamedSound);
            this.GroupBoxMedia.Location = new System.Drawing.Point(12, 126);
            this.GroupBoxMedia.Name = "GroupBoxMedia";
            this.GroupBoxMedia.Size = new System.Drawing.Size(450, 109);
            this.GroupBoxMedia.TabIndex = 3;
            this.GroupBoxMedia.TabStop = false;
            this.GroupBoxMedia.Text = "Media";
            // 
            // Button_Stop
            // 
            this.Button_Stop.Location = new System.Drawing.Point(369, 80);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(75, 23);
            this.Button_Stop.TabIndex = 10;
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Button_PlayAudio
            // 
            this.Button_PlayAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PlayAudio.Location = new System.Drawing.Point(288, 80);
            this.Button_PlayAudio.Name = "Button_PlayAudio";
            this.Button_PlayAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_PlayAudio.TabIndex = 9;
            this.Button_PlayAudio.Text = "Play";
            this.Button_PlayAudio.UseVisualStyleBackColor = true;
            this.Button_PlayAudio.Click += new System.EventHandler(this.Button_PlayAudio_Click);
            // 
            // Label_AudioSelected
            // 
            this.Label_AudioSelected.AutoSize = true;
            this.Label_AudioSelected.Location = new System.Drawing.Point(6, 45);
            this.Label_AudioSelected.Name = "Label_AudioSelected";
            this.Label_AudioSelected.Size = new System.Drawing.Size(37, 13);
            this.Label_AudioSelected.TabIndex = 8;
            this.Label_AudioSelected.Text = "Audio:";
            // 
            // Combobox_SelectedAudio
            // 
            this.Combobox_SelectedAudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_SelectedAudio.FormattingEnabled = true;
            this.Combobox_SelectedAudio.Location = new System.Drawing.Point(49, 42);
            this.Combobox_SelectedAudio.Name = "Combobox_SelectedAudio";
            this.Combobox_SelectedAudio.Size = new System.Drawing.Size(395, 21);
            this.Combobox_SelectedAudio.TabIndex = 7;
            // 
            // Checkbox_IsStreamedSound
            // 
            this.Checkbox_IsStreamedSound.AutoSize = true;
            this.Checkbox_IsStreamedSound.Enabled = false;
            this.Checkbox_IsStreamedSound.Location = new System.Drawing.Point(6, 19);
            this.Checkbox_IsStreamedSound.Name = "Checkbox_IsStreamedSound";
            this.Checkbox_IsStreamedSound.Size = new System.Drawing.Size(279, 17);
            this.Checkbox_IsStreamedSound.TabIndex = 6;
            this.Checkbox_IsStreamedSound.Text = "This sound is streamed and is stored in an external file";
            this.Checkbox_IsStreamedSound.UseVisualStyleBackColor = true;
            this.Checkbox_IsStreamedSound.CheckedChanged += new System.EventHandler(this.Checkbox_IsStreamedSound_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Combobox_Hashcode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Label_SubSFX);
            this.groupBox2.Location = new System.Drawing.Point(12, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hashcode";
            // 
            // Combobox_Hashcode
            // 
            this.Combobox_Hashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_Hashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_Hashcode.FormattingEnabled = true;
            this.Combobox_Hashcode.Location = new System.Drawing.Point(71, 52);
            this.Combobox_Hashcode.Name = "Combobox_Hashcode";
            this.Combobox_Hashcode.Size = new System.Drawing.Size(373, 21);
            this.Combobox_Hashcode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hashcode:";
            // 
            // Label_SubSFX
            // 
            this.Label_SubSFX.AutoSize = true;
            this.Label_SubSFX.Location = new System.Drawing.Point(6, 16);
            this.Label_SubSFX.MaximumSize = new System.Drawing.Size(451, 0);
            this.Label_SubSFX.Name = "Label_SubSFX";
            this.Label_SubSFX.Size = new System.Drawing.Size(402, 26);
            this.Label_SubSFX.TabIndex = 0;
            this.Label_SubSFX.Text = "If the flag \"hasSubSfx\" is checked, the file reference of the first \"sample\" entr" +
    "y gets interpreted as a SFX hashcode and played as a 3D sound at the current pos" +
    "ition.";
            // 
            // Frm_SampleProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 383);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GroupBoxMedia);
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
            this.GroupBoxMedia.ResumeLayout(false);
            this.GroupBoxMedia.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.GroupBox GroupBoxMedia;
        private System.Windows.Forms.NumericUpDown Numeric_BaseVolume;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.NumericUpDown numeric_randomPitchOffset;
        private System.Windows.Forms.Label Label_RandomPitchOffset;
        private System.Windows.Forms.CheckBox Checkbox_IsStreamedSound;
        private System.Windows.Forms.Label Label_AudioSelected;
        private System.Windows.Forms.ComboBox Combobox_SelectedAudio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Label_SubSFX;
        private System.Windows.Forms.ComboBox Combobox_Hashcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Button_PlayAudio;
        private System.Windows.Forms.Button Button_Stop;
    }
}