
namespace EuroSound_Application.StreamSoundsEditor
{
    partial class Frm_StreamSounds_Properties
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
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.GroupBox_AudioData = new System.Windows.Forms.GroupBox();
            this.Button_SearchIMA = new System.Windows.Forms.Button();
            this.Textbox_IMA_ADPCM = new System.Windows.Forms.TextBox();
            this.Label_IMA_ADPCM = new System.Windows.Forms.Label();
            this.Button_SearchPCM = new System.Windows.Forms.Button();
            this.Textbox_PCM_Data = new System.Windows.Forms.TextBox();
            this.Label_PCM_Data = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Combobox_Hashcode = new System.Windows.Forms.ComboBox();
            this.Label_Hashcode = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Label_BaseVolume = new System.Windows.Forms.Label();
            this.Groupbox_FileProperties = new System.Windows.Forms.GroupBox();
            this.Textbox_MD5Hash = new System.Windows.Forms.TextBox();
            this.Label_MD5Hash = new System.Windows.Forms.Label();
            this.Groupbox_AudioProperties = new System.Windows.Forms.GroupBox();
            this.euroSound_WaveViewer1 = new EuroSound_Application.EuroSound_WaveViewer();
            this.Textbox_Encoding = new System.Windows.Forms.TextBox();
            this.Label_Encoding = new System.Windows.Forms.Label();
            this.Button_StopAudio = new System.Windows.Forms.Button();
            this.Button_PlayAudio = new System.Windows.Forms.Button();
            this.Textbox_Duration = new System.Windows.Forms.TextBox();
            this.Label_Duration = new System.Windows.Forms.Label();
            this.Textbox_Bits = new System.Windows.Forms.TextBox();
            this.Label_Bits = new System.Windows.Forms.Label();
            this.Textbox_Channels = new System.Windows.Forms.TextBox();
            this.Label_Channels = new System.Windows.Forms.Label();
            this.Textbox_RealSize = new System.Windows.Forms.TextBox();
            this.Label_RealSize = new System.Windows.Forms.Label();
            this.Textbox_Frequency = new System.Windows.Forms.TextBox();
            this.Label_Frequency = new System.Windows.Forms.Label();
            this.Textbox_DataSize = new System.Windows.Forms.TextBox();
            this.Label_DataSize = new System.Windows.Forms.Label();
            this.Button_MarkersEditor = new System.Windows.Forms.Button();
            this.GroupBox_AudioData.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.Groupbox_FileProperties.SuspendLayout();
            this.Groupbox_AudioProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(340, 529);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(421, 529);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 3;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // GroupBox_AudioData
            // 
            this.GroupBox_AudioData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_AudioData.Controls.Add(this.Button_SearchIMA);
            this.GroupBox_AudioData.Controls.Add(this.Textbox_IMA_ADPCM);
            this.GroupBox_AudioData.Controls.Add(this.Label_IMA_ADPCM);
            this.GroupBox_AudioData.Controls.Add(this.Button_SearchPCM);
            this.GroupBox_AudioData.Controls.Add(this.Textbox_PCM_Data);
            this.GroupBox_AudioData.Controls.Add(this.Label_PCM_Data);
            this.GroupBox_AudioData.Location = new System.Drawing.Point(12, 98);
            this.GroupBox_AudioData.Name = "GroupBox_AudioData";
            this.GroupBox_AudioData.Size = new System.Drawing.Size(484, 81);
            this.GroupBox_AudioData.TabIndex = 5;
            this.GroupBox_AudioData.TabStop = false;
            this.GroupBox_AudioData.Text = "Audio Data";
            // 
            // Button_SearchIMA
            // 
            this.Button_SearchIMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SearchIMA.Location = new System.Drawing.Point(453, 45);
            this.Button_SearchIMA.Name = "Button_SearchIMA";
            this.Button_SearchIMA.Size = new System.Drawing.Size(24, 20);
            this.Button_SearchIMA.TabIndex = 5;
            this.Button_SearchIMA.Text = "...";
            this.Button_SearchIMA.UseVisualStyleBackColor = true;
            // 
            // Textbox_IMA_ADPCM
            // 
            this.Textbox_IMA_ADPCM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_IMA_ADPCM.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_IMA_ADPCM.Location = new System.Drawing.Point(108, 45);
            this.Textbox_IMA_ADPCM.Name = "Textbox_IMA_ADPCM";
            this.Textbox_IMA_ADPCM.ReadOnly = true;
            this.Textbox_IMA_ADPCM.Size = new System.Drawing.Size(340, 20);
            this.Textbox_IMA_ADPCM.TabIndex = 4;
            // 
            // Label_IMA_ADPCM
            // 
            this.Label_IMA_ADPCM.AutoSize = true;
            this.Label_IMA_ADPCM.Location = new System.Drawing.Point(6, 48);
            this.Label_IMA_ADPCM.Name = "Label_IMA_ADPCM";
            this.Label_IMA_ADPCM.Size = new System.Drawing.Size(96, 13);
            this.Label_IMA_ADPCM.TabIndex = 3;
            this.Label_IMA_ADPCM.Text = "IMA ADPCM Data:";
            // 
            // Button_SearchPCM
            // 
            this.Button_SearchPCM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SearchPCM.Location = new System.Drawing.Point(453, 19);
            this.Button_SearchPCM.Name = "Button_SearchPCM";
            this.Button_SearchPCM.Size = new System.Drawing.Size(24, 20);
            this.Button_SearchPCM.TabIndex = 2;
            this.Button_SearchPCM.Text = "...";
            this.Button_SearchPCM.UseVisualStyleBackColor = true;
            // 
            // Textbox_PCM_Data
            // 
            this.Textbox_PCM_Data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_PCM_Data.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_PCM_Data.Location = new System.Drawing.Point(108, 19);
            this.Textbox_PCM_Data.Name = "Textbox_PCM_Data";
            this.Textbox_PCM_Data.ReadOnly = true;
            this.Textbox_PCM_Data.Size = new System.Drawing.Size(340, 20);
            this.Textbox_PCM_Data.TabIndex = 1;
            // 
            // Label_PCM_Data
            // 
            this.Label_PCM_Data.AutoSize = true;
            this.Label_PCM_Data.Location = new System.Drawing.Point(43, 23);
            this.Label_PCM_Data.Name = "Label_PCM_Data";
            this.Label_PCM_Data.Size = new System.Drawing.Size(59, 13);
            this.Label_PCM_Data.TabIndex = 0;
            this.Label_PCM_Data.Text = "PCM Data:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Button_MarkersEditor);
            this.groupBox1.Controls.Add(this.Combobox_Hashcode);
            this.groupBox1.Controls.Add(this.Label_Hashcode);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.Label_BaseVolume);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 80);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sound Properties";
            // 
            // Combobox_Hashcode
            // 
            this.Combobox_Hashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_Hashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_Hashcode.FormattingEnabled = true;
            this.Combobox_Hashcode.Location = new System.Drawing.Point(84, 19);
            this.Combobox_Hashcode.Name = "Combobox_Hashcode";
            this.Combobox_Hashcode.Size = new System.Drawing.Size(394, 21);
            this.Combobox_Hashcode.TabIndex = 3;
            // 
            // Label_Hashcode
            // 
            this.Label_Hashcode.AutoSize = true;
            this.Label_Hashcode.Location = new System.Drawing.Point(19, 22);
            this.Label_Hashcode.Name = "Label_Hashcode";
            this.Label_Hashcode.Size = new System.Drawing.Size(59, 13);
            this.Label_Hashcode.TabIndex = 2;
            this.Label_Hashcode.Text = "Hashcode:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(84, 46);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(104, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(6, 48);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 0;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // Groupbox_FileProperties
            // 
            this.Groupbox_FileProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_FileProperties.Controls.Add(this.Textbox_MD5Hash);
            this.Groupbox_FileProperties.Controls.Add(this.Label_MD5Hash);
            this.Groupbox_FileProperties.Location = new System.Drawing.Point(12, 455);
            this.Groupbox_FileProperties.Name = "Groupbox_FileProperties";
            this.Groupbox_FileProperties.Size = new System.Drawing.Size(484, 60);
            this.Groupbox_FileProperties.TabIndex = 39;
            this.Groupbox_FileProperties.TabStop = false;
            this.Groupbox_FileProperties.Text = "File Properties";
            // 
            // Textbox_MD5Hash
            // 
            this.Textbox_MD5Hash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MD5Hash.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_MD5Hash.Location = new System.Drawing.Point(73, 19);
            this.Textbox_MD5Hash.Name = "Textbox_MD5Hash";
            this.Textbox_MD5Hash.ReadOnly = true;
            this.Textbox_MD5Hash.Size = new System.Drawing.Size(405, 20);
            this.Textbox_MD5Hash.TabIndex = 13;
            // 
            // Label_MD5Hash
            // 
            this.Label_MD5Hash.AutoSize = true;
            this.Label_MD5Hash.Location = new System.Drawing.Point(6, 22);
            this.Label_MD5Hash.Name = "Label_MD5Hash";
            this.Label_MD5Hash.Size = new System.Drawing.Size(61, 13);
            this.Label_MD5Hash.TabIndex = 12;
            this.Label_MD5Hash.Text = "MD5 Hash:";
            // 
            // Groupbox_AudioProperties
            // 
            this.Groupbox_AudioProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_AudioProperties.Controls.Add(this.euroSound_WaveViewer1);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Encoding);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Encoding);
            this.Groupbox_AudioProperties.Controls.Add(this.Button_StopAudio);
            this.Groupbox_AudioProperties.Controls.Add(this.Button_PlayAudio);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Duration);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Duration);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Bits);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Bits);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Channels);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Channels);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_RealSize);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_RealSize);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_Frequency);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_Frequency);
            this.Groupbox_AudioProperties.Controls.Add(this.Textbox_DataSize);
            this.Groupbox_AudioProperties.Controls.Add(this.Label_DataSize);
            this.Groupbox_AudioProperties.Location = new System.Drawing.Point(12, 185);
            this.Groupbox_AudioProperties.Name = "Groupbox_AudioProperties";
            this.Groupbox_AudioProperties.Size = new System.Drawing.Size(484, 264);
            this.Groupbox_AudioProperties.TabIndex = 38;
            this.Groupbox_AudioProperties.TabStop = false;
            this.Groupbox_AudioProperties.Text = "Audio Properties:";
            // 
            // euroSound_WaveViewer1
            // 
            this.euroSound_WaveViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.euroSound_WaveViewer1.AutoScroll = true;
            this.euroSound_WaveViewer1.BackColor = System.Drawing.Color.Gray;
            this.euroSound_WaveViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.euroSound_WaveViewer1.CurrentWaveImage = null;
            this.euroSound_WaveViewer1.Location = new System.Drawing.Point(6, 103);
            this.euroSound_WaveViewer1.Name = "euroSound_WaveViewer1";
            this.euroSound_WaveViewer1.PenColor = System.Drawing.Color.Blue;
            this.euroSound_WaveViewer1.PenWidth = 1F;
            this.euroSound_WaveViewer1.SamplesPerPixel = 128;
            this.euroSound_WaveViewer1.Size = new System.Drawing.Size(472, 150);
            this.euroSound_WaveViewer1.StartPosition = ((long)(0));
            this.euroSound_WaveViewer1.TabIndex = 22;
            this.euroSound_WaveViewer1.WaveStream = null;
            // 
            // Textbox_Encoding
            // 
            this.Textbox_Encoding.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Encoding.Location = new System.Drawing.Point(68, 74);
            this.Textbox_Encoding.Name = "Textbox_Encoding";
            this.Textbox_Encoding.ReadOnly = true;
            this.Textbox_Encoding.Size = new System.Drawing.Size(84, 20);
            this.Textbox_Encoding.TabIndex = 19;
            // 
            // Label_Encoding
            // 
            this.Label_Encoding.AutoSize = true;
            this.Label_Encoding.Location = new System.Drawing.Point(7, 77);
            this.Label_Encoding.Name = "Label_Encoding";
            this.Label_Encoding.Size = new System.Drawing.Size(55, 13);
            this.Label_Encoding.TabIndex = 18;
            this.Label_Encoding.Text = "Encoding:";
            // 
            // Button_StopAudio
            // 
            this.Button_StopAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StopAudio.Location = new System.Drawing.Point(403, 74);
            this.Button_StopAudio.Name = "Button_StopAudio";
            this.Button_StopAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_StopAudio.TabIndex = 21;
            this.Button_StopAudio.Text = "Stop Audio";
            this.Button_StopAudio.UseVisualStyleBackColor = true;
            // 
            // Button_PlayAudio
            // 
            this.Button_PlayAudio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PlayAudio.Location = new System.Drawing.Point(322, 74);
            this.Button_PlayAudio.Name = "Button_PlayAudio";
            this.Button_PlayAudio.Size = new System.Drawing.Size(75, 23);
            this.Button_PlayAudio.TabIndex = 20;
            this.Button_PlayAudio.Text = "Play Audio";
            this.Button_PlayAudio.UseVisualStyleBackColor = true;
            // 
            // Textbox_Duration
            // 
            this.Textbox_Duration.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Duration.Location = new System.Drawing.Point(390, 48);
            this.Textbox_Duration.Name = "Textbox_Duration";
            this.Textbox_Duration.ReadOnly = true;
            this.Textbox_Duration.Size = new System.Drawing.Size(84, 20);
            this.Textbox_Duration.TabIndex = 11;
            // 
            // Label_Duration
            // 
            this.Label_Duration.AutoSize = true;
            this.Label_Duration.Location = new System.Drawing.Point(334, 51);
            this.Label_Duration.Name = "Label_Duration";
            this.Label_Duration.Size = new System.Drawing.Size(50, 13);
            this.Label_Duration.TabIndex = 10;
            this.Label_Duration.Text = "Duration:";
            // 
            // Textbox_Bits
            // 
            this.Textbox_Bits.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Bits.Location = new System.Drawing.Point(229, 48);
            this.Textbox_Bits.Name = "Textbox_Bits";
            this.Textbox_Bits.ReadOnly = true;
            this.Textbox_Bits.Size = new System.Drawing.Size(84, 20);
            this.Textbox_Bits.TabIndex = 9;
            // 
            // Label_Bits
            // 
            this.Label_Bits.AutoSize = true;
            this.Label_Bits.Location = new System.Drawing.Point(196, 51);
            this.Label_Bits.Name = "Label_Bits";
            this.Label_Bits.Size = new System.Drawing.Size(27, 13);
            this.Label_Bits.TabIndex = 8;
            this.Label_Bits.Text = "Bits:";
            // 
            // Textbox_Channels
            // 
            this.Textbox_Channels.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Channels.Location = new System.Drawing.Point(68, 48);
            this.Textbox_Channels.Name = "Textbox_Channels";
            this.Textbox_Channels.ReadOnly = true;
            this.Textbox_Channels.Size = new System.Drawing.Size(84, 20);
            this.Textbox_Channels.TabIndex = 7;
            // 
            // Label_Channels
            // 
            this.Label_Channels.AutoSize = true;
            this.Label_Channels.Location = new System.Drawing.Point(8, 51);
            this.Label_Channels.Name = "Label_Channels";
            this.Label_Channels.Size = new System.Drawing.Size(54, 13);
            this.Label_Channels.TabIndex = 6;
            this.Label_Channels.Text = "Channels:";
            // 
            // Textbox_RealSize
            // 
            this.Textbox_RealSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_RealSize.Location = new System.Drawing.Point(390, 22);
            this.Textbox_RealSize.Name = "Textbox_RealSize";
            this.Textbox_RealSize.ReadOnly = true;
            this.Textbox_RealSize.Size = new System.Drawing.Size(84, 20);
            this.Textbox_RealSize.TabIndex = 5;
            // 
            // Label_RealSize
            // 
            this.Label_RealSize.AutoSize = true;
            this.Label_RealSize.Location = new System.Drawing.Point(329, 25);
            this.Label_RealSize.Name = "Label_RealSize";
            this.Label_RealSize.Size = new System.Drawing.Size(55, 13);
            this.Label_RealSize.TabIndex = 4;
            this.Label_RealSize.Text = "Real Size:";
            // 
            // Textbox_Frequency
            // 
            this.Textbox_Frequency.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Frequency.Location = new System.Drawing.Point(229, 22);
            this.Textbox_Frequency.Name = "Textbox_Frequency";
            this.Textbox_Frequency.ReadOnly = true;
            this.Textbox_Frequency.Size = new System.Drawing.Size(84, 20);
            this.Textbox_Frequency.TabIndex = 3;
            // 
            // Label_Frequency
            // 
            this.Label_Frequency.AutoSize = true;
            this.Label_Frequency.Location = new System.Drawing.Point(163, 25);
            this.Label_Frequency.Name = "Label_Frequency";
            this.Label_Frequency.Size = new System.Drawing.Size(60, 13);
            this.Label_Frequency.TabIndex = 2;
            this.Label_Frequency.Text = "Frequency:";
            // 
            // Textbox_DataSize
            // 
            this.Textbox_DataSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_DataSize.Location = new System.Drawing.Point(68, 22);
            this.Textbox_DataSize.Name = "Textbox_DataSize";
            this.Textbox_DataSize.ReadOnly = true;
            this.Textbox_DataSize.Size = new System.Drawing.Size(84, 20);
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
            // Button_MarkersEditor
            // 
            this.Button_MarkersEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MarkersEditor.Location = new System.Drawing.Point(194, 46);
            this.Button_MarkersEditor.Name = "Button_MarkersEditor";
            this.Button_MarkersEditor.Size = new System.Drawing.Size(146, 23);
            this.Button_MarkersEditor.TabIndex = 4;
            this.Button_MarkersEditor.Text = "Open Markers Editor";
            this.Button_MarkersEditor.UseVisualStyleBackColor = true;
            // 
            // Frm_StreamSounds_Properties
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(508, 564);
            this.Controls.Add(this.Groupbox_FileProperties);
            this.Controls.Add(this.Groupbox_AudioProperties);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox_AudioData);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Frm_StreamSounds_Properties";
            this.Text = "Frm_StreamSounds_MarkersEditor";
            this.GroupBox_AudioData.ResumeLayout(false);
            this.GroupBox_AudioData.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.Groupbox_FileProperties.ResumeLayout(false);
            this.Groupbox_FileProperties.PerformLayout();
            this.Groupbox_AudioProperties.ResumeLayout(false);
            this.Groupbox_AudioProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox GroupBox_AudioData;
        private System.Windows.Forms.Button Button_SearchIMA;
        private System.Windows.Forms.TextBox Textbox_IMA_ADPCM;
        private System.Windows.Forms.Label Label_IMA_ADPCM;
        private System.Windows.Forms.Button Button_SearchPCM;
        private System.Windows.Forms.TextBox Textbox_PCM_Data;
        private System.Windows.Forms.Label Label_PCM_Data;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.ComboBox Combobox_Hashcode;
        private System.Windows.Forms.Label Label_Hashcode;
        private System.Windows.Forms.GroupBox Groupbox_FileProperties;
        private System.Windows.Forms.TextBox Textbox_MD5Hash;
        private System.Windows.Forms.Label Label_MD5Hash;
        private System.Windows.Forms.GroupBox Groupbox_AudioProperties;
        private EuroSound_WaveViewer euroSound_WaveViewer1;
        private System.Windows.Forms.TextBox Textbox_Encoding;
        private System.Windows.Forms.Label Label_Encoding;
        private System.Windows.Forms.Button Button_StopAudio;
        private System.Windows.Forms.Button Button_PlayAudio;
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
        private System.Windows.Forms.Button Button_MarkersEditor;
    }
}