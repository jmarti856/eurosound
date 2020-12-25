
namespace EuroSound_Application
{
    partial class Frm_StreamSounds_MarkersEditor
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
            this.TreeView_Markers = new System.Windows.Forms.TreeView();
            this.GroupBox_MarkerData = new System.Windows.Forms.GroupBox();
            this.Numeric_MarkerLoopStart = new System.Windows.Forms.NumericUpDown();
            this.Label_LoopStart = new System.Windows.Forms.Label();
            this.Textbox_Extra = new System.Windows.Forms.TextBox();
            this.Label_Extra = new System.Windows.Forms.Label();
            this.Textbox_Flags = new System.Windows.Forms.TextBox();
            this.Label_Flags = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Label_Type = new System.Windows.Forms.Label();
            this.Numeric_MarkerPosition = new System.Windows.Forms.NumericUpDown();
            this.Label_MarkerPosition = new System.Windows.Forms.Label();
            this.GroupBox_StartData = new System.Windows.Forms.GroupBox();
            this.Textbox_InstantBuffer = new System.Windows.Forms.TextBox();
            this.Textbox_IsInstant = new System.Windows.Forms.TextBox();
            this.Label_InstantBuffer = new System.Windows.Forms.Label();
            this.Label_IsInstant = new System.Windows.Forms.Label();
            this.GroupBox_MarkerData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerLoopStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerPosition)).BeginInit();
            this.GroupBox_StartData.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView_Markers
            // 
            this.TreeView_Markers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_Markers.Location = new System.Drawing.Point(144, 262);
            this.TreeView_Markers.Name = "TreeView_Markers";
            this.TreeView_Markers.Size = new System.Drawing.Size(512, 176);
            this.TreeView_Markers.TabIndex = 7;
            // 
            // GroupBox_MarkerData
            // 
            this.GroupBox_MarkerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_MarkerData.Controls.Add(this.Numeric_MarkerLoopStart);
            this.GroupBox_MarkerData.Controls.Add(this.Label_LoopStart);
            this.GroupBox_MarkerData.Controls.Add(this.Textbox_Extra);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Extra);
            this.GroupBox_MarkerData.Controls.Add(this.Textbox_Flags);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Flags);
            this.GroupBox_MarkerData.Controls.Add(this.comboBox1);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Type);
            this.GroupBox_MarkerData.Controls.Add(this.Numeric_MarkerPosition);
            this.GroupBox_MarkerData.Controls.Add(this.Label_MarkerPosition);
            this.GroupBox_MarkerData.Location = new System.Drawing.Point(144, 77);
            this.GroupBox_MarkerData.Name = "GroupBox_MarkerData";
            this.GroupBox_MarkerData.Size = new System.Drawing.Size(512, 92);
            this.GroupBox_MarkerData.TabIndex = 6;
            this.GroupBox_MarkerData.TabStop = false;
            this.GroupBox_MarkerData.Text = "Marker Data";
            // 
            // Numeric_MarkerLoopStart
            // 
            this.Numeric_MarkerLoopStart.Location = new System.Drawing.Point(397, 26);
            this.Numeric_MarkerLoopStart.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Numeric_MarkerLoopStart.Name = "Numeric_MarkerLoopStart";
            this.Numeric_MarkerLoopStart.Size = new System.Drawing.Size(108, 20);
            this.Numeric_MarkerLoopStart.TabIndex = 10;
            // 
            // Label_LoopStart
            // 
            this.Label_LoopStart.AutoSize = true;
            this.Label_LoopStart.Location = new System.Drawing.Point(332, 29);
            this.Label_LoopStart.Name = "Label_LoopStart";
            this.Label_LoopStart.Size = new System.Drawing.Size(59, 13);
            this.Label_LoopStart.TabIndex = 8;
            this.Label_LoopStart.Text = "Loop Start:";
            // 
            // Textbox_Extra
            // 
            this.Textbox_Extra.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Extra.Location = new System.Drawing.Point(287, 53);
            this.Textbox_Extra.Name = "Textbox_Extra";
            this.Textbox_Extra.ReadOnly = true;
            this.Textbox_Extra.Size = new System.Drawing.Size(110, 20);
            this.Textbox_Extra.TabIndex = 7;
            this.Textbox_Extra.Text = "0";
            // 
            // Label_Extra
            // 
            this.Label_Extra.AutoSize = true;
            this.Label_Extra.Location = new System.Drawing.Point(246, 56);
            this.Label_Extra.Name = "Label_Extra";
            this.Label_Extra.Size = new System.Drawing.Size(34, 13);
            this.Label_Extra.TabIndex = 6;
            this.Label_Extra.Text = "Extra:";
            // 
            // Textbox_Flags
            // 
            this.Textbox_Flags.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Flags.Location = new System.Drawing.Point(226, 26);
            this.Textbox_Flags.Name = "Textbox_Flags";
            this.Textbox_Flags.ReadOnly = true;
            this.Textbox_Flags.Size = new System.Drawing.Size(100, 20);
            this.Textbox_Flags.TabIndex = 5;
            this.Textbox_Flags.Text = "2";
            // 
            // Label_Flags
            // 
            this.Label_Flags.AutoSize = true;
            this.Label_Flags.Location = new System.Drawing.Point(185, 29);
            this.Label_Flags.Name = "Label_Flags";
            this.Label_Flags.Size = new System.Drawing.Size(35, 13);
            this.Label_Flags.TabIndex = 4;
            this.Label_Flags.Text = "Flags:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(59, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // Label_Type
            // 
            this.Label_Type.AutoSize = true;
            this.Label_Type.Location = new System.Drawing.Point(19, 56);
            this.Label_Type.Name = "Label_Type";
            this.Label_Type.Size = new System.Drawing.Size(34, 13);
            this.Label_Type.TabIndex = 2;
            this.Label_Type.Text = "Type:";
            // 
            // Numeric_MarkerPosition
            // 
            this.Numeric_MarkerPosition.Location = new System.Drawing.Point(59, 27);
            this.Numeric_MarkerPosition.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Numeric_MarkerPosition.Name = "Numeric_MarkerPosition";
            this.Numeric_MarkerPosition.Size = new System.Drawing.Size(120, 20);
            this.Numeric_MarkerPosition.TabIndex = 1;
            // 
            // Label_MarkerPosition
            // 
            this.Label_MarkerPosition.AutoSize = true;
            this.Label_MarkerPosition.Location = new System.Drawing.Point(6, 29);
            this.Label_MarkerPosition.Name = "Label_MarkerPosition";
            this.Label_MarkerPosition.Size = new System.Drawing.Size(47, 13);
            this.Label_MarkerPosition.TabIndex = 0;
            this.Label_MarkerPosition.Text = "Position:";
            // 
            // GroupBox_StartData
            // 
            this.GroupBox_StartData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_StartData.Controls.Add(this.Textbox_InstantBuffer);
            this.GroupBox_StartData.Controls.Add(this.Textbox_IsInstant);
            this.GroupBox_StartData.Controls.Add(this.Label_InstantBuffer);
            this.GroupBox_StartData.Controls.Add(this.Label_IsInstant);
            this.GroupBox_StartData.Location = new System.Drawing.Point(144, 13);
            this.GroupBox_StartData.Name = "GroupBox_StartData";
            this.GroupBox_StartData.Size = new System.Drawing.Size(512, 58);
            this.GroupBox_StartData.TabIndex = 5;
            this.GroupBox_StartData.TabStop = false;
            this.GroupBox_StartData.Text = "Marker Start Data";
            // 
            // Textbox_InstantBuffer
            // 
            this.Textbox_InstantBuffer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_InstantBuffer.Location = new System.Drawing.Point(264, 19);
            this.Textbox_InstantBuffer.Name = "Textbox_InstantBuffer";
            this.Textbox_InstantBuffer.Size = new System.Drawing.Size(123, 20);
            this.Textbox_InstantBuffer.TabIndex = 3;
            this.Textbox_InstantBuffer.Text = "0";
            // 
            // Textbox_IsInstant
            // 
            this.Textbox_IsInstant.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_IsInstant.Location = new System.Drawing.Point(65, 19);
            this.Textbox_IsInstant.Name = "Textbox_IsInstant";
            this.Textbox_IsInstant.Size = new System.Drawing.Size(114, 20);
            this.Textbox_IsInstant.TabIndex = 2;
            this.Textbox_IsInstant.Text = "0";
            // 
            // Label_InstantBuffer
            // 
            this.Label_InstantBuffer.AutoSize = true;
            this.Label_InstantBuffer.Location = new System.Drawing.Point(185, 22);
            this.Label_InstantBuffer.Name = "Label_InstantBuffer";
            this.Label_InstantBuffer.Size = new System.Drawing.Size(73, 13);
            this.Label_InstantBuffer.TabIndex = 1;
            this.Label_InstantBuffer.Text = "Instant Buffer:";
            // 
            // Label_IsInstant
            // 
            this.Label_IsInstant.AutoSize = true;
            this.Label_IsInstant.Location = new System.Drawing.Point(6, 22);
            this.Label_IsInstant.Name = "Label_IsInstant";
            this.Label_IsInstant.Size = new System.Drawing.Size(53, 13);
            this.Label_IsInstant.TabIndex = 0;
            this.Label_IsInstant.Text = "Is Instant:";
            // 
            // Frm_StreamSounds_MarkersEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TreeView_Markers);
            this.Controls.Add(this.GroupBox_MarkerData);
            this.Controls.Add(this.GroupBox_StartData);
            this.Name = "Frm_StreamSounds_MarkersEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_StreamSounds_MarkersEditor";
            this.GroupBox_MarkerData.ResumeLayout(false);
            this.GroupBox_MarkerData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerLoopStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerPosition)).EndInit();
            this.GroupBox_StartData.ResumeLayout(false);
            this.GroupBox_StartData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TreeView_Markers;
        private System.Windows.Forms.GroupBox GroupBox_MarkerData;
        private System.Windows.Forms.NumericUpDown Numeric_MarkerLoopStart;
        private System.Windows.Forms.Label Label_LoopStart;
        private System.Windows.Forms.TextBox Textbox_Extra;
        private System.Windows.Forms.Label Label_Extra;
        private System.Windows.Forms.TextBox Textbox_Flags;
        private System.Windows.Forms.Label Label_Flags;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label Label_Type;
        private System.Windows.Forms.NumericUpDown Numeric_MarkerPosition;
        private System.Windows.Forms.Label Label_MarkerPosition;
        private System.Windows.Forms.GroupBox GroupBox_StartData;
        private System.Windows.Forms.TextBox Textbox_InstantBuffer;
        private System.Windows.Forms.TextBox Textbox_IsInstant;
        private System.Windows.Forms.Label Label_InstantBuffer;
        private System.Windows.Forms.Label Label_IsInstant;
    }
}