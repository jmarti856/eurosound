
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
            this.GroupBox_MarkerData = new System.Windows.Forms.GroupBox();
            this.Button_AddMarker = new System.Windows.Forms.Button();
            this.Numeric_MarkerLoopStart = new System.Windows.Forms.NumericUpDown();
            this.Label_LoopStart = new System.Windows.Forms.Label();
            this.Textbox_Extra = new System.Windows.Forms.TextBox();
            this.Label_Extra = new System.Windows.Forms.Label();
            this.Textbox_Flags = new System.Windows.Forms.TextBox();
            this.Label_Flags = new System.Windows.Forms.Label();
            this.ComboBox_MarkerType = new System.Windows.Forms.ComboBox();
            this.Label_Type = new System.Windows.Forms.Label();
            this.Numeric_MarkerPosition = new System.Windows.Forms.NumericUpDown();
            this.Label_MarkerPosition = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_Markers = new System.Windows.Forms.Label();
            this.ListView_Markers = new System.Windows.Forms.ListView();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_LoopMarker = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_LoopStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Flags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Extra = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkerPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox_MarkerData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerLoopStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox_MarkerData
            // 
            this.GroupBox_MarkerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_MarkerData.Controls.Add(this.Button_AddMarker);
            this.GroupBox_MarkerData.Controls.Add(this.Numeric_MarkerLoopStart);
            this.GroupBox_MarkerData.Controls.Add(this.Label_LoopStart);
            this.GroupBox_MarkerData.Controls.Add(this.Textbox_Extra);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Extra);
            this.GroupBox_MarkerData.Controls.Add(this.Textbox_Flags);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Flags);
            this.GroupBox_MarkerData.Controls.Add(this.ComboBox_MarkerType);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Type);
            this.GroupBox_MarkerData.Controls.Add(this.Numeric_MarkerPosition);
            this.GroupBox_MarkerData.Controls.Add(this.Label_MarkerPosition);
            this.GroupBox_MarkerData.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_MarkerData.Name = "GroupBox_MarkerData";
            this.GroupBox_MarkerData.Size = new System.Drawing.Size(514, 92);
            this.GroupBox_MarkerData.TabIndex = 1;
            this.GroupBox_MarkerData.TabStop = false;
            this.GroupBox_MarkerData.Text = "Marker Data";
            // 
            // Button_AddMarker
            // 
            this.Button_AddMarker.Location = new System.Drawing.Point(403, 53);
            this.Button_AddMarker.Name = "Button_AddMarker";
            this.Button_AddMarker.Size = new System.Drawing.Size(36, 21);
            this.Button_AddMarker.TabIndex = 11;
            this.Button_AddMarker.Text = "Add";
            this.Button_AddMarker.UseVisualStyleBackColor = true;
            this.Button_AddMarker.Click += new System.EventHandler(this.Button_AddMarker_Click);
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
            // ComboBox_MarkerType
            // 
            this.ComboBox_MarkerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_MarkerType.FormattingEnabled = true;
            this.ComboBox_MarkerType.Location = new System.Drawing.Point(59, 53);
            this.ComboBox_MarkerType.Name = "ComboBox_MarkerType";
            this.ComboBox_MarkerType.Size = new System.Drawing.Size(181, 21);
            this.ComboBox_MarkerType.TabIndex = 3;
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
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(370, 469);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 6;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(451, 469);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Label_Markers
            // 
            this.Label_Markers.AutoSize = true;
            this.Label_Markers.Location = new System.Drawing.Point(9, 107);
            this.Label_Markers.Name = "Label_Markers";
            this.Label_Markers.Size = new System.Drawing.Size(48, 13);
            this.Label_Markers.TabIndex = 4;
            this.Label_Markers.Text = "Markers:";
            // 
            // ListView_Markers
            // 
            this.ListView_Markers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_Position,
            this.Col_Type,
            this.Col_Flags,
            this.Col_Extra,
            this.Col_LoopStart,
            this.Col_MarkerCount,
            this.Col_LoopMarker,
            this.Col_MarkerPos});
            this.ListView_Markers.GridLines = true;
            this.ListView_Markers.HideSelection = false;
            this.ListView_Markers.Location = new System.Drawing.Point(12, 123);
            this.ListView_Markers.Name = "ListView_Markers";
            this.ListView_Markers.Size = new System.Drawing.Size(514, 340);
            this.ListView_Markers.TabIndex = 8;
            this.ListView_Markers.UseCompatibleStateImageBehavior = false;
            this.ListView_Markers.View = System.Windows.Forms.View.Details;
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            // 
            // Col_Position
            // 
            this.Col_Position.Text = "Position";
            this.Col_Position.Width = 72;
            // 
            // Col_Type
            // 
            this.Col_Type.Text = "Type";
            this.Col_Type.Width = 66;
            // 
            // Col_MarkerCount
            // 
            this.Col_MarkerCount.Text = "Marker Count";
            this.Col_MarkerCount.Width = 96;
            // 
            // Col_LoopMarker
            // 
            this.Col_LoopMarker.Text = "Loop Marker Count";
            this.Col_LoopMarker.Width = 108;
            // 
            // Col_LoopStart
            // 
            this.Col_LoopStart.Text = "Loop Start";
            this.Col_LoopStart.Width = 87;
            // 
            // Col_Flags
            // 
            this.Col_Flags.Text = "Flags";
            // 
            // Col_Extra
            // 
            this.Col_Extra.Text = "Extra";
            // 
            // Col_MarkerPos
            // 
            this.Col_MarkerPos.Text = "Marker Pos";
            // 
            // Frm_StreamSounds_MarkersEditor
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(538, 504);
            this.Controls.Add(this.ListView_Markers);
            this.Controls.Add(this.Label_Markers);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.GroupBox_MarkerData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StreamSounds_MarkersEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_StreamSounds_MarkersEditor";
            this.Load += new System.EventHandler(this.Frm_StreamSounds_MarkersEditor_Load);
            this.GroupBox_MarkerData.ResumeLayout(false);
            this.GroupBox_MarkerData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerLoopStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GroupBox_MarkerData;
        private System.Windows.Forms.NumericUpDown Numeric_MarkerLoopStart;
        private System.Windows.Forms.Label Label_LoopStart;
        private System.Windows.Forms.TextBox Textbox_Extra;
        private System.Windows.Forms.Label Label_Extra;
        private System.Windows.Forms.TextBox Textbox_Flags;
        private System.Windows.Forms.Label Label_Flags;
        private System.Windows.Forms.ComboBox ComboBox_MarkerType;
        private System.Windows.Forms.Label Label_Type;
        private System.Windows.Forms.NumericUpDown Numeric_MarkerPosition;
        private System.Windows.Forms.Label Label_MarkerPosition;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_Markers;
        private System.Windows.Forms.Button Button_AddMarker;
        private System.Windows.Forms.ListView ListView_Markers;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Position;
        private System.Windows.Forms.ColumnHeader Col_Type;
        private System.Windows.Forms.ColumnHeader Col_MarkerCount;
        private System.Windows.Forms.ColumnHeader Col_LoopMarker;
        private System.Windows.Forms.ColumnHeader Col_LoopStart;
        private System.Windows.Forms.ColumnHeader Col_Flags;
        private System.Windows.Forms.ColumnHeader Col_Extra;
        private System.Windows.Forms.ColumnHeader Col_MarkerPos;
    }
}