
using EuroSound_Application.CustomControls.WavesViewerForm;

namespace EuroSound_Application.SoundBanksEditor
{
    partial class Frm_AudioProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AudioProperties));
            this.numeric_psi = new System.Windows.Forms.NumericUpDown();
            this.Label_PSI = new System.Windows.Forms.Label();
            this.Button_StopAudio = new System.Windows.Forms.Button();
            this.numeric_loopOffset = new System.Windows.Forms.NumericUpDown();
            this.Label_Flags = new System.Windows.Forms.Label();
            this.Label_LoopOffset = new System.Windows.Forms.Label();
            this.Button_PlayAudio = new System.Windows.Forms.Button();
            this.Button_ReplaceAudio = new System.Windows.Forms.Button();
            this.Textbox_MediaName = new System.Windows.Forms.TextBox();
            this.Label_MediaAudio = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Textbox_MD5Hash = new System.Windows.Forms.TextBox();
            this.Button_SaveAudio = new System.Windows.Forms.Button();
            this.Label_MD5Hash = new System.Windows.Forms.Label();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Groupbox_AudioProperties = new System.Windows.Forms.GroupBox();
            this.Button_TestLoopOffset = new System.Windows.Forms.Button();
            this.Textbox_Flags = new System.Windows.Forms.TextBox();
            this.euroSound_WaveViewer1 = new EuroSound_Application.CustomControls.WavesViewerForm.EuroSound_WaveViewer();
            this.ContextMenu_SaveAudio = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuAudioSave = new System.Windows.Forms.ToolStripMenuItem();
            this.Textbox_Encoding = new System.Windows.Forms.TextBox();
            this.Label_Frequency = new System.Windows.Forms.Label();
            this.Textbox_Frequency = new System.Windows.Forms.TextBox();
            this.Label_Encoding = new System.Windows.Forms.Label();
            this.Textbox_Duration = new System.Windows.Forms.TextBox();
            this.Label_Duration = new System.Windows.Forms.Label();
            this.Textbox_Bits = new System.Windows.Forms.TextBox();
            this.Label_Bits = new System.Windows.Forms.Label();
            this.Textbox_Channels = new System.Windows.Forms.TextBox();
            this.Label_Channels = new System.Windows.Forms.Label();
            this.Textbox_RealSize = new System.Windows.Forms.TextBox();
            this.Label_RealSize = new System.Windows.Forms.Label();
            this.Textbox_DataSize = new System.Windows.Forms.TextBox();
            this.Label_DataSize = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_psi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_loopOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Groupbox_AudioProperties.SuspendLayout();
            this.ContextMenu_SaveAudio.SuspendLayout();
            this.SuspendLayout();
            // 
            // numeric_psi
            // 
            this.numeric_psi.Location = new System.Drawing.Point(68, 100);
            this.numeric_psi.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numeric_psi.Name = "numeric_psi";
            this.numeric_psi.Size = new System.Drawing.Size(87, 20);
            this.numeric_psi.TabIndex = 7;
            // 
            // Label_PSI
            // 
            this.Label_PSI.AutoSize = true;
            this.Label_PSI.Location = new System.Drawing.Point(35, 102);
            this.Label_PSI.Name = "Label_PSI";
            this.Label_PSI.Size = new System.Drawing.Size(27, 13);
            this.Label_PSI.TabIndex = 6;
            this.Label_PSI.Text = "PSI:";
            // 
            // Button_StopAudio
            // 
            this.Button_StopAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StopAudio.Location = new System.Drawing.Point(433, 102);
            this.Button_StopAudio.Name = "Button_StopAudio";
            this.Button_StopAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_StopAudio.TabIndex = 22;
            this.Button_StopAudio.Text = "Stop Audio";
            this.Button_StopAudio.UseVisualStyleBackColor = true;
            this.Button_StopAudio.Click += new System.EventHandler(this.Button_StopAudio_Click);
            // 
            // numeric_loopOffset
            // 
            this.numeric_loopOffset.Location = new System.Drawing.Point(421, 74);
            this.numeric_loopOffset.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numeric_loopOffset.Name = "numeric_loopOffset";
            this.numeric_loopOffset.Size = new System.Drawing.Size(87, 20);
            this.numeric_loopOffset.TabIndex = 19;
            // 
            // Label_Flags
            // 
            this.Label_Flags.AutoSize = true;
            this.Label_Flags.Location = new System.Drawing.Point(201, 76);
            this.Label_Flags.Name = "Label_Flags";
            this.Label_Flags.Size = new System.Drawing.Size(35, 13);
            this.Label_Flags.TabIndex = 12;
            this.Label_Flags.Text = "Flags:";
            // 
            // Label_LoopOffset
            // 
            this.Label_LoopOffset.AutoSize = true;
            this.Label_LoopOffset.Location = new System.Drawing.Point(350, 77);
            this.Label_LoopOffset.Name = "Label_LoopOffset";
            this.Label_LoopOffset.Size = new System.Drawing.Size(65, 13);
            this.Label_LoopOffset.TabIndex = 18;
            this.Label_LoopOffset.Text = "Loop Offset:";
            // 
            // Button_PlayAudio
            // 
            this.Button_PlayAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PlayAudio.Location = new System.Drawing.Point(352, 102);
            this.Button_PlayAudio.Name = "Button_PlayAudio";
            this.Button_PlayAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_PlayAudio.TabIndex = 21;
            this.Button_PlayAudio.Text = "Play Audio";
            this.Button_PlayAudio.UseVisualStyleBackColor = true;
            this.Button_PlayAudio.Click += new System.EventHandler(this.Button_PlayAudio_Click);
            // 
            // Button_ReplaceAudio
            // 
            this.Button_ReplaceAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ReplaceAudio.Location = new System.Drawing.Point(437, 19);
            this.Button_ReplaceAudio.Name = "Button_ReplaceAudio";
            this.Button_ReplaceAudio.Size = new System.Drawing.Size(24, 20);
            this.Button_ReplaceAudio.TabIndex = 2;
            this.Button_ReplaceAudio.Text = "...";
            this.Button_ReplaceAudio.UseVisualStyleBackColor = true;
            this.Button_ReplaceAudio.Click += new System.EventHandler(this.Button_ReplaceAudio_Click);
            // 
            // Textbox_MediaName
            // 
            this.Textbox_MediaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MediaName.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_MediaName.Location = new System.Drawing.Point(50, 19);
            this.Textbox_MediaName.Name = "Textbox_MediaName";
            this.Textbox_MediaName.ReadOnly = true;
            this.Textbox_MediaName.Size = new System.Drawing.Size(381, 20);
            this.Textbox_MediaName.TabIndex = 1;
            this.Textbox_MediaName.Text = "<NO DATA>";
            // 
            // Label_MediaAudio
            // 
            this.Label_MediaAudio.AutoSize = true;
            this.Label_MediaAudio.Location = new System.Drawing.Point(6, 22);
            this.Label_MediaAudio.Name = "Label_MediaAudio";
            this.Label_MediaAudio.Size = new System.Drawing.Size(38, 13);
            this.Label_MediaAudio.TabIndex = 0;
            this.Label_MediaAudio.Text = "Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Textbox_MD5Hash);
            this.groupBox1.Controls.Add(this.Button_SaveAudio);
            this.groupBox1.Controls.Add(this.Label_MD5Hash);
            this.groupBox1.Controls.Add(this.Textbox_MediaName);
            this.groupBox1.Controls.Add(this.Label_MediaAudio);
            this.groupBox1.Controls.Add(this.Button_ReplaceAudio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loaded Data:";
            // 
            // Textbox_MD5Hash
            // 
            this.Textbox_MD5Hash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MD5Hash.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_MD5Hash.Location = new System.Drawing.Point(50, 45);
            this.Textbox_MD5Hash.Name = "Textbox_MD5Hash";
            this.Textbox_MD5Hash.ReadOnly = true;
            this.Textbox_MD5Hash.Size = new System.Drawing.Size(458, 20);
            this.Textbox_MD5Hash.TabIndex = 5;
            // 
            // Button_SaveAudio
            // 
            this.Button_SaveAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SaveAudio.Location = new System.Drawing.Point(467, 19);
            this.Button_SaveAudio.Name = "Button_SaveAudio";
            this.Button_SaveAudio.Size = new System.Drawing.Size(41, 20);
            this.Button_SaveAudio.TabIndex = 3;
            this.Button_SaveAudio.Text = "Save";
            this.Button_SaveAudio.UseVisualStyleBackColor = true;
            this.Button_SaveAudio.Click += new System.EventHandler(this.Button_SaveAudio_Click);
            // 
            // Label_MD5Hash
            // 
            this.Label_MD5Hash.AutoSize = true;
            this.Label_MD5Hash.Location = new System.Drawing.Point(9, 48);
            this.Label_MD5Hash.Name = "Label_MD5Hash";
            this.Label_MD5Hash.Size = new System.Drawing.Size(35, 13);
            this.Label_MD5Hash.TabIndex = 4;
            this.Label_MD5Hash.Text = "Hash:";
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(451, 452);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 3;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Groupbox_AudioProperties
            // 
            this.Groupbox_AudioProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_AudioProperties.Controls.Add(this.Button_TestLoopOffset);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Flags);
            this.Groupbox_AudioProperties.Controls.Add(this.euroSound_WaveViewer1);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Encoding);
            this.Groupbox_AudioProperties.Controls.Add(this.numeric_psi);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_PSI);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Frequency);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Frequency);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Encoding);
            this.Groupbox_AudioProperties.Controls.Add(this.Button_StopAudio);
            this.Groupbox_AudioProperties.Controls.Add(this.Button_PlayAudio);
            this.Groupbox_AudioProperties.Controls.Add(this.numeric_loopOffset);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Duration);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Duration);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Flags);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Bits);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_LoopOffset);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Bits);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Channels);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Channels);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_RealSize);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_RealSize);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_DataSize);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_DataSize);
            this.Groupbox_AudioProperties.Location = new System.Drawing.Point(12, 97);
            this.Groupbox_AudioProperties.Name = "Groupbox_AudioProperties";
            this.Groupbox_AudioProperties.Size = new System.Drawing.Size(514, 349);
            this.Groupbox_AudioProperties.TabIndex = 1;
            this.Groupbox_AudioProperties.TabStop = false;
            this.Groupbox_AudioProperties.Text = "Audio Properties:";
            // 
            // Button_TestLoopOffset
            // 
            this.Button_TestLoopOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_TestLoopOffset.Location = new System.Drawing.Point(242, 102);
            this.Button_TestLoopOffset.Name = "Button_TestLoopOffset";
            this.Button_TestLoopOffset.Size = new System.Drawing.Size(104, 23);
            this.Button_TestLoopOffset.TabIndex = 20;
            this.Button_TestLoopOffset.Text = "Check Loop Offset";
            this.Button_TestLoopOffset.UseVisualStyleBackColor = true;
            this.Button_TestLoopOffset.Click += new System.EventHandler(this.Button_TestLoopOffset_Click);
            // 
            // Textbox_Flags
            // 
            this.Textbox_Flags.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Flags.Location = new System.Drawing.Point(242, 74);
            this.Textbox_Flags.Name = "Textbox_Flags";
            this.Textbox_Flags.ReadOnly = true;
            this.Textbox_Flags.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Flags.TabIndex = 13;
            this.Textbox_Flags.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Textbox_Flags_MouseClick);
            // 
            // euroSound_WaveViewer1
            // 
            this.euroSound_WaveViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.euroSound_WaveViewer1.AutoScroll = true;
            this.euroSound_WaveViewer1.BackColor = System.Drawing.Color.Gray;
            this.euroSound_WaveViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.euroSound_WaveViewer1.ContextMenuStrip = this.ContextMenu_SaveAudio;
            this.euroSound_WaveViewer1.CurrentWaveImage = null;
            this.euroSound_WaveViewer1.Location = new System.Drawing.Point(6, 131);
            this.euroSound_WaveViewer1.Name = "euroSound_WaveViewer1";
            this.euroSound_WaveViewer1.PenWidth = 1F;
            this.euroSound_WaveViewer1.SamplesPerPixel = 128;
            this.euroSound_WaveViewer1.Size = new System.Drawing.Size(502, 212);
            this.euroSound_WaveViewer1.StartPosition = ((long)(0));
            this.euroSound_WaveViewer1.TabIndex = 23;
            this.euroSound_WaveViewer1.WaveStream = null;
            this.euroSound_WaveViewer1.OnLineDrawEvent += new EuroSound_Application.CustomControls.WavesViewerForm.EuroSound_WaveViewer.OnLineDrawHandler(this.EuroSound_WaveViewer1_OnLineDrawEvent);
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
            this.Textbox_Encoding.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Encoding.Location = new System.Drawing.Point(68, 74);
            this.Textbox_Encoding.Name = "Textbox_Encoding";
            this.Textbox_Encoding.ReadOnly = true;
            this.Textbox_Encoding.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Encoding.TabIndex = 5;
            // 
            // Label_Frequency
            // 
            this.Label_Frequency.AutoSize = true;
            this.Label_Frequency.Location = new System.Drawing.Point(355, 25);
            this.Label_Frequency.Name = "Label_Frequency";
            this.Label_Frequency.Size = new System.Drawing.Size(60, 13);
            this.Label_Frequency.TabIndex = 14;
            this.Label_Frequency.Text = "Frequency:";
            // 
            // Textbox_Frequency
            // 
            this.Textbox_Frequency.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Frequency.Location = new System.Drawing.Point(421, 22);
            this.Textbox_Frequency.Name = "Textbox_Frequency";
            this.Textbox_Frequency.ReadOnly = true;
            this.Textbox_Frequency.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Frequency.TabIndex = 15;
            // 
            // Label_Encoding
            // 
            this.Label_Encoding.AutoSize = true;
            this.Label_Encoding.Location = new System.Drawing.Point(7, 76);
            this.Label_Encoding.Name = "Label_Encoding";
            this.Label_Encoding.Size = new System.Drawing.Size(55, 13);
            this.Label_Encoding.TabIndex = 4;
            this.Label_Encoding.Text = "Encoding:";
            // 
            // Textbox_Duration
            // 
            this.Textbox_Duration.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Duration.Location = new System.Drawing.Point(421, 48);
            this.Textbox_Duration.Name = "Textbox_Duration";
            this.Textbox_Duration.ReadOnly = true;
            this.Textbox_Duration.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Duration.TabIndex = 17;
            // 
            // Label_Duration
            // 
            this.Label_Duration.AutoSize = true;
            this.Label_Duration.Location = new System.Drawing.Point(365, 51);
            this.Label_Duration.Name = "Label_Duration";
            this.Label_Duration.Size = new System.Drawing.Size(50, 13);
            this.Label_Duration.TabIndex = 16;
            this.Label_Duration.Text = "Duration:";
            // 
            // Textbox_Bits
            // 
            this.Textbox_Bits.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Bits.Location = new System.Drawing.Point(242, 48);
            this.Textbox_Bits.Name = "Textbox_Bits";
            this.Textbox_Bits.ReadOnly = true;
            this.Textbox_Bits.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Bits.TabIndex = 11;
            // 
            // Label_Bits
            // 
            this.Label_Bits.AutoSize = true;
            this.Label_Bits.Location = new System.Drawing.Point(209, 51);
            this.Label_Bits.Name = "Label_Bits";
            this.Label_Bits.Size = new System.Drawing.Size(27, 13);
            this.Label_Bits.TabIndex = 10;
            this.Label_Bits.Text = "Bits:";
            // 
            // Textbox_Channels
            // 
            this.Textbox_Channels.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Channels.Location = new System.Drawing.Point(68, 48);
            this.Textbox_Channels.Name = "Textbox_Channels";
            this.Textbox_Channels.ReadOnly = true;
            this.Textbox_Channels.Size = new System.Drawing.Size(87, 20);
            this.Textbox_Channels.TabIndex = 3;
            // 
            // Label_Channels
            // 
            this.Label_Channels.AutoSize = true;
            this.Label_Channels.Location = new System.Drawing.Point(8, 51);
            this.Label_Channels.Name = "Label_Channels";
            this.Label_Channels.Size = new System.Drawing.Size(54, 13);
            this.Label_Channels.TabIndex = 2;
            this.Label_Channels.Text = "Channels:";
            // 
            // Textbox_RealSize
            // 
            this.Textbox_RealSize.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_RealSize.Location = new System.Drawing.Point(242, 22);
            this.Textbox_RealSize.Name = "Textbox_RealSize";
            this.Textbox_RealSize.ReadOnly = true;
            this.Textbox_RealSize.Size = new System.Drawing.Size(87, 20);
            this.Textbox_RealSize.TabIndex = 9;
            // 
            // Label_RealSize
            // 
            this.Label_RealSize.AutoSize = true;
            this.Label_RealSize.Location = new System.Drawing.Point(181, 25);
            this.Label_RealSize.Name = "Label_RealSize";
            this.Label_RealSize.Size = new System.Drawing.Size(55, 13);
            this.Label_RealSize.TabIndex = 8;
            this.Label_RealSize.Text = "Real Size:";
            // 
            // Textbox_DataSize
            // 
            this.Textbox_DataSize.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_DataSize.Location = new System.Drawing.Point(68, 22);
            this.Textbox_DataSize.Name = "Textbox_DataSize";
            this.Textbox_DataSize.ReadOnly = true;
            this.Textbox_DataSize.Size = new System.Drawing.Size(87, 20);
            this.Textbox_DataSize.TabIndex = 1;
            // 
            // Label_DataSize
            // 
            this.Label_DataSize.AutoSize = true;
            this.Label_DataSize.Location = new System.Drawing.Point(6, 25);
            this.Label_DataSize.Name = "Label_DataSize";
            this.Label_DataSize.Size = new System.Drawing.Size(56, 13);
            this.Label_DataSize.TabIndex = 0;
            this.Label_DataSize.Text = "Data Size:";
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(370, 452);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Frm_AudioProperties
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(538, 487);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Groupbox_AudioProperties);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AudioProperties";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_AudioProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AudioProperties_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AudioProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numeric_psi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_loopOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Groupbox_AudioProperties.ResumeLayout(false);
            this.Groupbox_AudioProperties.PerformLayout();
            this.ContextMenu_SaveAudio.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numeric_psi;
        private System.Windows.Forms.Label Label_PSI;
        private System.Windows.Forms.Button Button_StopAudio;
        private System.Windows.Forms.NumericUpDown numeric_loopOffset;
        private System.Windows.Forms.Label Label_Flags;
        private System.Windows.Forms.Label Label_LoopOffset;
        private System.Windows.Forms.Button Button_PlayAudio;
        private System.Windows.Forms.Button Button_ReplaceAudio;
        private System.Windows.Forms.TextBox Textbox_MediaName;
        private System.Windows.Forms.Label Label_MediaAudio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox Groupbox_AudioProperties;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.TextBox Textbox_Duration;
        private System.Windows.Forms.Label Label_Duration;
        private System.Windows.Forms.TextBox Textbox_Bits;
        private System.Windows.Forms.Label Label_Bits;
        private System.Windows.Forms.TextBox Textbox_Channels;
        private System.Windows.Forms.Label Label_Channels;
        private System.Windows.Forms.TextBox Textbox_RealSize;
        private System.Windows.Forms.Label Label_RealSize;
        private System.Windows.Forms.TextBox Textbox_Frequency;
        private System.Windows.Forms.Label Label_Frequency;
        private System.Windows.Forms.TextBox Textbox_DataSize;
        private System.Windows.Forms.Label Label_DataSize;
        private System.Windows.Forms.Label Label_MD5Hash;
        private System.Windows.Forms.TextBox Textbox_MD5Hash;
        private System.Windows.Forms.TextBox Textbox_Encoding;
        private System.Windows.Forms.Label Label_Encoding;
        private EuroSound_WaveViewer euroSound_WaveViewer1;
        private System.Windows.Forms.TextBox Textbox_Flags;
        private System.Windows.Forms.Button Button_TestLoopOffset;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_SaveAudio;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAudioSave;
        private System.Windows.Forms.Button Button_SaveAudio;
    }
}