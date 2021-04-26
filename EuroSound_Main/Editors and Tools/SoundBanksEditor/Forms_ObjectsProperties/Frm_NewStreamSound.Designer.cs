
namespace EuroSound_Application.SoundBanksEditor
{
    partial class Frm_NewStreamSound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_NewStreamSound));
            this.Label_ExternalFile = new System.Windows.Forms.Label();
            this.Textbox_ExternalFile = new System.Windows.Forms.TextBox();
            this.ListBox_StreamSounds = new System.Windows.Forms.ListBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_Text = new System.Windows.Forms.Label();
            this.Button_RefreshList = new System.Windows.Forms.Button();
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
            this.groupbox_properties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomPitchOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randompan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomvolumeoffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pitchoffset)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_ExternalFile
            // 
            this.Label_ExternalFile.AutoSize = true;
            this.Label_ExternalFile.Location = new System.Drawing.Point(12, 15);
            this.Label_ExternalFile.Name = "Label_ExternalFile";
            this.Label_ExternalFile.Size = new System.Drawing.Size(67, 13);
            this.Label_ExternalFile.TabIndex = 0;
            this.Label_ExternalFile.Text = "External File:";
            // 
            // Textbox_ExternalFile
            // 
            this.Textbox_ExternalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_ExternalFile.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_ExternalFile.Location = new System.Drawing.Point(85, 12);
            this.Textbox_ExternalFile.Name = "Textbox_ExternalFile";
            this.Textbox_ExternalFile.ReadOnly = true;
            this.Textbox_ExternalFile.Size = new System.Drawing.Size(383, 20);
            this.Textbox_ExternalFile.TabIndex = 1;
            this.Textbox_ExternalFile.WordWrap = false;
            // 
            // ListBox_StreamSounds
            // 
            this.ListBox_StreamSounds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox_StreamSounds.FormattingEnabled = true;
            this.ListBox_StreamSounds.Location = new System.Drawing.Point(12, 168);
            this.ListBox_StreamSounds.Name = "ListBox_StreamSounds";
            this.ListBox_StreamSounds.ScrollAlwaysVisible = true;
            this.ListBox_StreamSounds.Size = new System.Drawing.Size(456, 316);
            this.ListBox_StreamSounds.TabIndex = 4;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(312, 487);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 6;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(393, 487);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Label_Text
            // 
            this.Label_Text.AutoSize = true;
            this.Label_Text.Location = new System.Drawing.Point(12, 152);
            this.Label_Text.Name = "Label_Text";
            this.Label_Text.Size = new System.Drawing.Size(115, 13);
            this.Label_Text.TabIndex = 3;
            this.Label_Text.Text = "Select a stream sound:";
            // 
            // Button_RefreshList
            // 
            this.Button_RefreshList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_RefreshList.Location = new System.Drawing.Point(12, 487);
            this.Button_RefreshList.Name = "Button_RefreshList";
            this.Button_RefreshList.Size = new System.Drawing.Size(75, 23);
            this.Button_RefreshList.TabIndex = 5;
            this.Button_RefreshList.Text = "Refresh";
            this.Button_RefreshList.UseVisualStyleBackColor = true;
            this.Button_RefreshList.Click += new System.EventHandler(this.Button_RefreshList_Click);
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
            this.groupbox_properties.Location = new System.Drawing.Point(12, 38);
            this.groupbox_properties.Name = "groupbox_properties";
            this.groupbox_properties.Size = new System.Drawing.Size(456, 108);
            this.groupbox_properties.TabIndex = 2;
            this.groupbox_properties.TabStop = false;
            this.groupbox_properties.Text = "Properties:";
            // 
            // Numeric_BaseVolume
            // 
            this.Numeric_BaseVolume.DecimalPlaces = 2;
            this.Numeric_BaseVolume.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.Numeric_BaseVolume.Location = new System.Drawing.Point(94, 45);
            this.Numeric_BaseVolume.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Numeric_BaseVolume.Name = "Numeric_BaseVolume";
            this.Numeric_BaseVolume.Size = new System.Drawing.Size(94, 20);
            this.Numeric_BaseVolume.TabIndex = 3;
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(16, 47);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 2;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // numeric_randomPitchOffset
            // 
            this.numeric_randomPitchOffset.Location = new System.Drawing.Point(342, 19);
            this.numeric_randomPitchOffset.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numeric_randomPitchOffset.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.numeric_randomPitchOffset.Name = "numeric_randomPitchOffset";
            this.numeric_randomPitchOffset.Size = new System.Drawing.Size(94, 20);
            this.numeric_randomPitchOffset.TabIndex = 7;
            // 
            // Label_RandomPitchOffset
            // 
            this.Label_RandomPitchOffset.AutoSize = true;
            this.Label_RandomPitchOffset.Location = new System.Drawing.Point(228, 21);
            this.Label_RandomPitchOffset.Name = "Label_RandomPitchOffset";
            this.Label_RandomPitchOffset.Size = new System.Drawing.Size(108, 13);
            this.Label_RandomPitchOffset.TabIndex = 6;
            this.Label_RandomPitchOffset.Text = "Random Pitch Offset:";
            // 
            // numeric_randompan
            // 
            this.numeric_randompan.DecimalPlaces = 2;
            this.numeric_randompan.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numeric_randompan.Location = new System.Drawing.Point(342, 71);
            this.numeric_randompan.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric_randompan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numeric_randompan.Name = "numeric_randompan";
            this.numeric_randompan.Size = new System.Drawing.Size(94, 20);
            this.numeric_randompan.TabIndex = 11;
            // 
            // label_randompan
            // 
            this.label_randompan.AutoSize = true;
            this.label_randompan.Location = new System.Drawing.Point(264, 71);
            this.label_randompan.Name = "label_randompan";
            this.label_randompan.Size = new System.Drawing.Size(72, 13);
            this.label_randompan.TabIndex = 10;
            this.label_randompan.Text = "Random Pan:";
            // 
            // numeric_pan
            // 
            this.numeric_pan.DecimalPlaces = 2;
            this.numeric_pan.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numeric_pan.Location = new System.Drawing.Point(94, 71);
            this.numeric_pan.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric_pan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numeric_pan.Name = "numeric_pan";
            this.numeric_pan.Size = new System.Drawing.Size(94, 20);
            this.numeric_pan.TabIndex = 5;
            // 
            // label_pan
            // 
            this.label_pan.AutoSize = true;
            this.label_pan.Location = new System.Drawing.Point(59, 73);
            this.label_pan.Name = "label_pan";
            this.label_pan.Size = new System.Drawing.Size(29, 13);
            this.label_pan.TabIndex = 4;
            this.label_pan.Text = "Pan:";
            // 
            // numeric_randomvolumeoffset
            // 
            this.numeric_randomvolumeoffset.DecimalPlaces = 2;
            this.numeric_randomvolumeoffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numeric_randomvolumeoffset.Location = new System.Drawing.Point(342, 45);
            this.numeric_randomvolumeoffset.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric_randomvolumeoffset.Name = "numeric_randomvolumeoffset";
            this.numeric_randomvolumeoffset.Size = new System.Drawing.Size(94, 20);
            this.numeric_randomvolumeoffset.TabIndex = 9;
            // 
            // label_randomvolumeoffset
            // 
            this.label_randomvolumeoffset.AutoSize = true;
            this.label_randomvolumeoffset.Location = new System.Drawing.Point(217, 47);
            this.label_randomvolumeoffset.Name = "label_randomvolumeoffset";
            this.label_randomvolumeoffset.Size = new System.Drawing.Size(119, 13);
            this.label_randomvolumeoffset.TabIndex = 8;
            this.label_randomvolumeoffset.Text = "Random Volume Offset:";
            // 
            // numeric_pitchoffset
            // 
            this.numeric_pitchoffset.Location = new System.Drawing.Point(94, 19);
            this.numeric_pitchoffset.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numeric_pitchoffset.Minimum = new decimal(new int[] {
            22000,
            0,
            0,
            -2147483648});
            this.numeric_pitchoffset.Name = "numeric_pitchoffset";
            this.numeric_pitchoffset.Size = new System.Drawing.Size(94, 20);
            this.numeric_pitchoffset.TabIndex = 1;
            // 
            // label_pitchoffset
            // 
            this.label_pitchoffset.AutoSize = true;
            this.label_pitchoffset.Location = new System.Drawing.Point(23, 21);
            this.label_pitchoffset.Name = "label_pitchoffset";
            this.label_pitchoffset.Size = new System.Drawing.Size(65, 13);
            this.label_pitchoffset.TabIndex = 0;
            this.label_pitchoffset.Text = "Pitch Offset:";
            // 
            // Frm_NewStreamSound
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(480, 522);
            this.Controls.Add(this.groupbox_properties);
            this.Controls.Add(this.Button_RefreshList);
            this.Controls.Add(this.Label_Text);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.ListBox_StreamSounds);
            this.Controls.Add(this.Textbox_ExternalFile);
            this.Controls.Add(this.Label_ExternalFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_NewStreamSound";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_NewStreamSound";
            this.Load += new System.EventHandler(this.Frm_NewStreamSound_Load);
            this.groupbox_properties.ResumeLayout(false);
            this.groupbox_properties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomPitchOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randompan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_randomvolumeoffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_pitchoffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ExternalFile;
        private System.Windows.Forms.TextBox Textbox_ExternalFile;
        private System.Windows.Forms.ListBox ListBox_StreamSounds;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_Text;
        private System.Windows.Forms.Button Button_RefreshList;
        private System.Windows.Forms.GroupBox groupbox_properties;
        private System.Windows.Forms.NumericUpDown Numeric_BaseVolume;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.NumericUpDown numeric_randomPitchOffset;
        private System.Windows.Forms.Label Label_RandomPitchOffset;
        private System.Windows.Forms.NumericUpDown numeric_randompan;
        private System.Windows.Forms.Label label_randompan;
        private System.Windows.Forms.NumericUpDown numeric_pan;
        private System.Windows.Forms.Label label_pan;
        private System.Windows.Forms.NumericUpDown numeric_randomvolumeoffset;
        private System.Windows.Forms.Label label_randomvolumeoffset;
        private System.Windows.Forms.NumericUpDown numeric_pitchoffset;
        private System.Windows.Forms.Label label_pitchoffset;
    }
}