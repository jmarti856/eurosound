
namespace EuroSound_Application.Musics
{
    partial class Frm_Musics_Properties
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Musics_Properties));
            this.Textbox_MD5_Hash = new System.Windows.Forms.TextBox();
            this.Button_SaveAudio = new System.Windows.Forms.Button();
            this.Label_MD5_Hash = new System.Windows.Forms.Label();
            this.Textbox_IMA_ADPCM = new System.Windows.Forms.TextBox();
            this.Label_Data = new System.Windows.Forms.Label();
            this.Button_ReplaceAudio = new System.Windows.Forms.Button();
            this.GroupBox_Audio_Properties = new System.Windows.Forms.GroupBox();
            this.Textbox_RealSize = new System.Windows.Forms.TextBox();
            this.Label_RealSize = new System.Windows.Forms.Label();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Button_Play = new System.Windows.Forms.Button();
            this.ContextMenu_SaveAudio = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuAudioSave = new System.Windows.Forms.ToolStripMenuItem();
            this.Textbox_Encoding = new System.Windows.Forms.TextBox();
            this.Label_Encoding = new System.Windows.Forms.Label();
            this.Textbox_Bits = new System.Windows.Forms.TextBox();
            this.Label_Bits = new System.Windows.Forms.Label();
            this.Textbox_Channels = new System.Windows.Forms.TextBox();
            this.Textbox_Frequency = new System.Windows.Forms.TextBox();
            this.Label_Channels = new System.Windows.Forms.Label();
            this.Label_Frequency = new System.Windows.Forms.Label();
            this.CheckBox_OutputThisMusic = new System.Windows.Forms.CheckBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Numeric_BaseVolume = new System.Windows.Forms.NumericUpDown();
            this.Button_MarkersEditor = new System.Windows.Forms.Button();
            this.Label_BaseVolume = new System.Windows.Forms.Label();
            this.GroupBox_Properties = new System.Windows.Forms.GroupBox();
            this.WaveViewer = new EuroSound_Application.CustomControls.WavesViewerForm.EuroSound_WaveViewer();
            this.Groupbox_LoadedData = new System.Windows.Forms.GroupBox();
            this.GroupBox_Audio_Properties.SuspendLayout();
            this.ContextMenu_SaveAudio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).BeginInit();
            this.GroupBox_Properties.SuspendLayout();
            this.Groupbox_LoadedData.SuspendLayout();
            this.SuspendLayout();
            // 
            // Textbox_MD5_Hash
            // 
            this.Textbox_MD5_Hash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MD5_Hash.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_MD5_Hash.Location = new System.Drawing.Point(47, 45);
            this.Textbox_MD5_Hash.Name = "Textbox_MD5_Hash";
            this.Textbox_MD5_Hash.ReadOnly = true;
            this.Textbox_MD5_Hash.Size = new System.Drawing.Size(571, 20);
            this.Textbox_MD5_Hash.TabIndex = 5;
            // 
            // Button_SaveAudio
            // 
            this.Button_SaveAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SaveAudio.Location = new System.Drawing.Point(577, 19);
            this.Button_SaveAudio.Name = "Button_SaveAudio";
            this.Button_SaveAudio.Size = new System.Drawing.Size(41, 20);
            this.Button_SaveAudio.TabIndex = 3;
            this.Button_SaveAudio.Text = "Save";
            this.Button_SaveAudio.UseVisualStyleBackColor = true;
            this.Button_SaveAudio.Click += new System.EventHandler(this.Button_SaveAudio_Click);
            // 
            // Label_MD5_Hash
            // 
            this.Label_MD5_Hash.AutoSize = true;
            this.Label_MD5_Hash.Location = new System.Drawing.Point(6, 48);
            this.Label_MD5_Hash.Name = "Label_MD5_Hash";
            this.Label_MD5_Hash.Size = new System.Drawing.Size(35, 13);
            this.Label_MD5_Hash.TabIndex = 4;
            this.Label_MD5_Hash.Text = "Hash:";
            // 
            // Textbox_IMA_ADPCM
            // 
            this.Textbox_IMA_ADPCM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_IMA_ADPCM.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_IMA_ADPCM.Location = new System.Drawing.Point(47, 19);
            this.Textbox_IMA_ADPCM.Name = "Textbox_IMA_ADPCM";
            this.Textbox_IMA_ADPCM.ReadOnly = true;
            this.Textbox_IMA_ADPCM.Size = new System.Drawing.Size(490, 20);
            this.Textbox_IMA_ADPCM.TabIndex = 1;
            // 
            // Label_Data
            // 
            this.Label_Data.AutoSize = true;
            this.Label_Data.Location = new System.Drawing.Point(9, 22);
            this.Label_Data.Name = "Label_Data";
            this.Label_Data.Size = new System.Drawing.Size(32, 13);
            this.Label_Data.TabIndex = 0;
            this.Label_Data.Text = "Path:";
            // 
            // Button_ReplaceAudio
            // 
            this.Button_ReplaceAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ReplaceAudio.Location = new System.Drawing.Point(543, 19);
            this.Button_ReplaceAudio.Name = "Button_ReplaceAudio";
            this.Button_ReplaceAudio.Size = new System.Drawing.Size(24, 20);
            this.Button_ReplaceAudio.TabIndex = 2;
            this.Button_ReplaceAudio.Text = "...";
            this.Button_ReplaceAudio.UseVisualStyleBackColor = true;
            this.Button_ReplaceAudio.Click += new System.EventHandler(this.Button_ReplaceAudio_Click);
            // 
            // GroupBox_Audio_Properties
            // 
            this.GroupBox_Audio_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Audio_Properties.Controls.Add(this.Textbox_RealSize);
            this.GroupBox_Audio_Properties.Controls.Add(this.Label_RealSize);
            this.GroupBox_Audio_Properties.Controls.Add(this.Button_Stop);
            this.GroupBox_Audio_Properties.Controls.Add(this.Button_Play);
            this.GroupBox_Audio_Properties.Controls.Add(this.WaveViewer);
            this.GroupBox_Audio_Properties.Controls.Add(this.Textbox_Encoding);
            this.GroupBox_Audio_Properties.Controls.Add(this.Label_Encoding);
            this.GroupBox_Audio_Properties.Controls.Add(this.Textbox_Bits);
            this.GroupBox_Audio_Properties.Controls.Add(this.Label_Bits);
            this.GroupBox_Audio_Properties.Controls.Add(this.Textbox_Channels);
            this.GroupBox_Audio_Properties.Controls.Add(this.Textbox_Frequency);
            this.GroupBox_Audio_Properties.Controls.Add(this.Label_Channels);
            this.GroupBox_Audio_Properties.Controls.Add(this.Label_Frequency);
            this.GroupBox_Audio_Properties.Location = new System.Drawing.Point(12, 153);
            this.GroupBox_Audio_Properties.Name = "GroupBox_Audio_Properties";
            this.GroupBox_Audio_Properties.Size = new System.Drawing.Size(624, 425);
            this.GroupBox_Audio_Properties.TabIndex = 3;
            this.GroupBox_Audio_Properties.TabStop = false;
            this.GroupBox_Audio_Properties.Text = "Sound Properties:";
            // 
            // Textbox_RealSize
            // 
            this.Textbox_RealSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_RealSize.Location = new System.Drawing.Point(66, 45);
            this.Textbox_RealSize.Name = "Textbox_RealSize";
            this.Textbox_RealSize.ReadOnly = true;
            this.Textbox_RealSize.Size = new System.Drawing.Size(87, 20);
            this.Textbox_RealSize.TabIndex = 11;
            // 
            // Label_RealSize
            // 
            this.Label_RealSize.AutoSize = true;
            this.Label_RealSize.Location = new System.Drawing.Point(6, 48);
            this.Label_RealSize.Name = "Label_RealSize";
            this.Label_RealSize.Size = new System.Drawing.Size(55, 13);
            this.Label_RealSize.TabIndex = 10;
            this.Label_RealSize.Text = "Real Size:";
            // 
            // Button_Stop
            // 
            this.Button_Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Stop.Location = new System.Drawing.Point(543, 43);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(75, 23);
            this.Button_Stop.TabIndex = 14;
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Button_Play
            // 
            this.Button_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Play.Location = new System.Drawing.Point(462, 43);
            this.Button_Play.Name = "Button_Play";
            this.Button_Play.Size = new System.Drawing.Size(75, 23);
            this.Button_Play.TabIndex = 13;
            this.Button_Play.Text = "Play";
            this.Button_Play.UseVisualStyleBackColor = true;
            this.Button_Play.Click += new System.EventHandler(this.Button_Play_Click);
            // 
            // ContextMenu_SaveAudio
            // 
            this.ContextMenu_SaveAudio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuAudioSave});
            this.ContextMenu_SaveAudio.Name = "ContextMenu_SaveAudio";
            this.ContextMenu_SaveAudio.Size = new System.Drawing.Size(132, 26);
            // 
            // ContextMenuAudioSave
            // 
            this.ContextMenuAudioSave.Name = "ContextMenuAudioSave";
            this.ContextMenuAudioSave.Size = new System.Drawing.Size(131, 22);
            this.ContextMenuAudioSave.Text = "Save audio";
            this.ContextMenuAudioSave.Click += new System.EventHandler(this.ContextMenuAudioSave_Click);
            // 
            // Textbox_Encoding
            // 
            this.Textbox_Encoding.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Encoding.Location = new System.Drawing.Point(403, 18);
            this.Textbox_Encoding.Name = "Textbox_Encoding";
            this.Textbox_Encoding.ReadOnly = true;
            this.Textbox_Encoding.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Encoding.TabIndex = 5;
            // 
            // Label_Encoding
            // 
            this.Label_Encoding.AutoSize = true;
            this.Label_Encoding.Location = new System.Drawing.Point(342, 21);
            this.Label_Encoding.Name = "Label_Encoding";
            this.Label_Encoding.Size = new System.Drawing.Size(55, 13);
            this.Label_Encoding.TabIndex = 4;
            this.Label_Encoding.Text = "Encoding:";
            // 
            // Textbox_Bits
            // 
            this.Textbox_Bits.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Bits.Location = new System.Drawing.Point(527, 19);
            this.Textbox_Bits.Name = "Textbox_Bits";
            this.Textbox_Bits.ReadOnly = true;
            this.Textbox_Bits.Size = new System.Drawing.Size(91, 20);
            this.Textbox_Bits.TabIndex = 9;
            // 
            // Label_Bits
            // 
            this.Label_Bits.AutoSize = true;
            this.Label_Bits.Location = new System.Drawing.Point(496, 22);
            this.Label_Bits.Name = "Label_Bits";
            this.Label_Bits.Size = new System.Drawing.Size(27, 13);
            this.Label_Bits.TabIndex = 8;
            this.Label_Bits.Text = "Bits:";
            // 
            // Textbox_Channels
            // 
            this.Textbox_Channels.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Channels.Location = new System.Drawing.Point(66, 19);
            this.Textbox_Channels.Name = "Textbox_Channels";
            this.Textbox_Channels.ReadOnly = true;
            this.Textbox_Channels.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Channels.TabIndex = 7;
            // 
            // Textbox_Frequency
            // 
            this.Textbox_Frequency.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Frequency.Location = new System.Drawing.Point(238, 19);
            this.Textbox_Frequency.Name = "Textbox_Frequency";
            this.Textbox_Frequency.ReadOnly = true;
            this.Textbox_Frequency.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Frequency.TabIndex = 3;
            // 
            // Label_Channels
            // 
            this.Label_Channels.AutoSize = true;
            this.Label_Channels.Location = new System.Drawing.Point(6, 21);
            this.Label_Channels.Name = "Label_Channels";
            this.Label_Channels.Size = new System.Drawing.Size(54, 13);
            this.Label_Channels.TabIndex = 6;
            this.Label_Channels.Text = "Channels:";
            // 
            // Label_Frequency
            // 
            this.Label_Frequency.AutoSize = true;
            this.Label_Frequency.Location = new System.Drawing.Point(172, 21);
            this.Label_Frequency.Name = "Label_Frequency";
            this.Label_Frequency.Size = new System.Drawing.Size(60, 13);
            this.Label_Frequency.TabIndex = 2;
            this.Label_Frequency.Text = "Frequency:";
            // 
            // CheckBox_OutputThisMusic
            // 
            this.CheckBox_OutputThisMusic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_OutputThisMusic.AutoSize = true;
            this.CheckBox_OutputThisMusic.Checked = true;
            this.CheckBox_OutputThisMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_OutputThisMusic.Location = new System.Drawing.Point(12, 588);
            this.CheckBox_OutputThisMusic.Name = "CheckBox_OutputThisMusic";
            this.CheckBox_OutputThisMusic.Size = new System.Drawing.Size(107, 17);
            this.CheckBox_OutputThisMusic.TabIndex = 5;
            this.CheckBox_OutputThisMusic.Text = "Output this music";
            this.CheckBox_OutputThisMusic.UseVisualStyleBackColor = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(561, 584);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(480, 584);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 6;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Numeric_BaseVolume
            // 
            this.Numeric_BaseVolume.DecimalPlaces = 2;
            this.Numeric_BaseVolume.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.Numeric_BaseVolume.Location = new System.Drawing.Point(84, 19);
            this.Numeric_BaseVolume.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Numeric_BaseVolume.Name = "Numeric_BaseVolume";
            this.Numeric_BaseVolume.Size = new System.Drawing.Size(87, 20);
            this.Numeric_BaseVolume.TabIndex = 1;
            // 
            // Button_MarkersEditor
            // 
            this.Button_MarkersEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MarkersEditor.Location = new System.Drawing.Point(520, 16);
            this.Button_MarkersEditor.Name = "Button_MarkersEditor";
            this.Button_MarkersEditor.Size = new System.Drawing.Size(98, 23);
            this.Button_MarkersEditor.TabIndex = 12;
            this.Button_MarkersEditor.Text = "Markers Editor";
            this.Button_MarkersEditor.UseVisualStyleBackColor = true;
            this.Button_MarkersEditor.Click += new System.EventHandler(this.Button_MarkersEditor_Click);
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(6, 21);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 0;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // GroupBox_Properties
            // 
            this.GroupBox_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Properties.Controls.Add(this.Label_BaseVolume);
            this.GroupBox_Properties.Controls.Add(this.Numeric_BaseVolume);
            this.GroupBox_Properties.Controls.Add(this.Button_MarkersEditor);
            this.GroupBox_Properties.Location = new System.Drawing.Point(12, 96);
            this.GroupBox_Properties.Name = "GroupBox_Properties";
            this.GroupBox_Properties.Size = new System.Drawing.Size(624, 51);
            this.GroupBox_Properties.TabIndex = 8;
            this.GroupBox_Properties.TabStop = false;
            this.GroupBox_Properties.Text = "Properties";
            // 
            // WaveViewer
            // 
            this.WaveViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WaveViewer.AutoScroll = true;
            this.WaveViewer.BackColor = System.Drawing.Color.Gray;
            this.WaveViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.WaveViewer.ContextMenuStrip = this.ContextMenu_SaveAudio;
            this.WaveViewer.CurrentWaveImage = null;
            this.WaveViewer.Location = new System.Drawing.Point(6, 72);
            this.WaveViewer.Name = "WaveViewer";
            this.WaveViewer.PenWidth = 1F;
            this.WaveViewer.SamplesPerPixel = 128;
            this.WaveViewer.Size = new System.Drawing.Size(612, 347);
            this.WaveViewer.StartPosition = ((long)(0));
            this.WaveViewer.TabIndex = 15;
            this.WaveViewer.WaveStream = null;
            // 
            // Groupbox_LoadedData
            // 
            this.Groupbox_LoadedData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_LoadedData.Controls.Add(this.Textbox_IMA_ADPCM);
            this.Groupbox_LoadedData.Controls.Add(this.Textbox_MD5_Hash);
            this.Groupbox_LoadedData.Controls.Add(this.Button_ReplaceAudio);
            this.Groupbox_LoadedData.Controls.Add(this.Button_SaveAudio);
            this.Groupbox_LoadedData.Controls.Add(this.Label_Data);
            this.Groupbox_LoadedData.Controls.Add(this.Label_MD5_Hash);
            this.Groupbox_LoadedData.Location = new System.Drawing.Point(12, 12);
            this.Groupbox_LoadedData.Name = "Groupbox_LoadedData";
            this.Groupbox_LoadedData.Size = new System.Drawing.Size(624, 78);
            this.Groupbox_LoadedData.TabIndex = 9;
            this.Groupbox_LoadedData.TabStop = false;
            this.Groupbox_LoadedData.Text = "Loaded Data";
            // 
            // Frm_Musics_Properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 619);
            this.Controls.Add(this.Groupbox_LoadedData);
            this.Controls.Add(this.GroupBox_Properties);
            this.Controls.Add(this.GroupBox_Audio_Properties);
            this.Controls.Add(this.CheckBox_OutputThisMusic);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Musics_Properties";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_Musics_Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Musics_Properties_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Musics_Properties_Load);
            this.GroupBox_Audio_Properties.ResumeLayout(false);
            this.GroupBox_Audio_Properties.PerformLayout();
            this.ContextMenu_SaveAudio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).EndInit();
            this.GroupBox_Properties.ResumeLayout(false);
            this.GroupBox_Properties.PerformLayout();
            this.Groupbox_LoadedData.ResumeLayout(false);
            this.Groupbox_LoadedData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox CheckBox_OutputThisMusic;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_SaveAudio;
        private System.Windows.Forms.TextBox Textbox_MD5_Hash;
        private System.Windows.Forms.Label Label_MD5_Hash;
        private System.Windows.Forms.Label Label_Data;
        private System.Windows.Forms.Button Button_ReplaceAudio;
        private System.Windows.Forms.TextBox Textbox_IMA_ADPCM;
        private System.Windows.Forms.GroupBox GroupBox_Audio_Properties;
        private System.Windows.Forms.TextBox Textbox_RealSize;
        private System.Windows.Forms.Label Label_RealSize;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Button Button_Play;
        private CustomControls.WavesViewerForm.EuroSound_WaveViewer WaveViewer;
        private System.Windows.Forms.TextBox Textbox_Encoding;
        private System.Windows.Forms.NumericUpDown Numeric_BaseVolume;
        private System.Windows.Forms.Label Label_Encoding;
        private System.Windows.Forms.Button Button_MarkersEditor;
        private System.Windows.Forms.TextBox Textbox_Bits;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.Label Label_Bits;
        private System.Windows.Forms.TextBox Textbox_Channels;
        private System.Windows.Forms.TextBox Textbox_Frequency;
        private System.Windows.Forms.Label Label_Channels;
        private System.Windows.Forms.Label Label_Frequency;
        private System.Windows.Forms.GroupBox GroupBox_Properties;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_SaveAudio;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudioSave;
        private System.Windows.Forms.GroupBox Groupbox_LoadedData;
    }
}