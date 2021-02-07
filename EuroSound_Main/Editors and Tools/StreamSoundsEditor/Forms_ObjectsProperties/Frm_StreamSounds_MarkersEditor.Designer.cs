
namespace EuroSound_Application.StreamSounds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StreamSounds_MarkersEditor));
            this.GroupBox_MarkerData = new System.Windows.Forms.GroupBox();
            this.Button_Clear = new System.Windows.Forms.Button();
            this.Button_AddMarker = new System.Windows.Forms.Button();
            this.Numeric_MarkerLoopStart = new System.Windows.Forms.NumericUpDown();
            this.Label_LoopStart = new System.Windows.Forms.Label();
            this.ComboBox_MarkerType = new System.Windows.Forms.ComboBox();
            this.Label_Type = new System.Windows.Forms.Label();
            this.Numeric_Position = new System.Windows.Forms.NumericUpDown();
            this.Label_Position = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_StartMarkers = new System.Windows.Forms.Label();
            this.ListView_Markers = new System.Windows.Forms.ListView();
            this.Col_MarkerPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_IsInstant = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_StateA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_StateB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_StartMarkerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label_MarkerData = new System.Windows.Forms.Label();
            this.ListView_MarkerData = new System.Windows.Forms.ListView();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Position = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MusicMarkerType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Flags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Extra = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_LoopStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_MarkerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_LoopMarkerCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox_MarkerData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerLoopStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_Position)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox_MarkerData
            // 
            this.GroupBox_MarkerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_MarkerData.Controls.Add(this.Button_Clear);
            this.GroupBox_MarkerData.Controls.Add(this.Button_AddMarker);
            this.GroupBox_MarkerData.Controls.Add(this.Numeric_MarkerLoopStart);
            this.GroupBox_MarkerData.Controls.Add(this.Label_LoopStart);
            this.GroupBox_MarkerData.Controls.Add(this.ComboBox_MarkerType);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Type);
            this.GroupBox_MarkerData.Controls.Add(this.Numeric_Position);
            this.GroupBox_MarkerData.Controls.Add(this.Label_Position);
            this.GroupBox_MarkerData.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_MarkerData.Name = "GroupBox_MarkerData";
            this.GroupBox_MarkerData.Size = new System.Drawing.Size(529, 92);
            this.GroupBox_MarkerData.TabIndex = 1;
            this.GroupBox_MarkerData.TabStop = false;
            this.GroupBox_MarkerData.Text = "Marker Data:";
            // 
            // Button_Clear
            // 
            this.Button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Clear.Location = new System.Drawing.Point(380, 53);
            this.Button_Clear.Name = "Button_Clear";
            this.Button_Clear.Size = new System.Drawing.Size(78, 21);
            this.Button_Clear.TabIndex = 12;
            this.Button_Clear.Text = "Clear All";
            this.Button_Clear.UseVisualStyleBackColor = true;
            this.Button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // Button_AddMarker
            // 
            this.Button_AddMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_AddMarker.Location = new System.Drawing.Point(464, 53);
            this.Button_AddMarker.Name = "Button_AddMarker";
            this.Button_AddMarker.Size = new System.Drawing.Size(59, 21);
            this.Button_AddMarker.TabIndex = 11;
            this.Button_AddMarker.Text = "Add";
            this.Button_AddMarker.UseVisualStyleBackColor = true;
            this.Button_AddMarker.Click += new System.EventHandler(this.Button_AddMarker_Click);
            // 
            // Numeric_MarkerLoopStart
            // 
            this.Numeric_MarkerLoopStart.Location = new System.Drawing.Point(411, 26);
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
            this.Label_LoopStart.Location = new System.Drawing.Point(346, 29);
            this.Label_LoopStart.Name = "Label_LoopStart";
            this.Label_LoopStart.Size = new System.Drawing.Size(59, 13);
            this.Label_LoopStart.TabIndex = 8;
            this.Label_LoopStart.Text = "Loop Start:";
            // 
            // ComboBox_MarkerType
            // 
            this.ComboBox_MarkerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_MarkerType.FormattingEnabled = true;
            this.ComboBox_MarkerType.Location = new System.Drawing.Point(46, 25);
            this.ComboBox_MarkerType.Name = "ComboBox_MarkerType";
            this.ComboBox_MarkerType.Size = new System.Drawing.Size(115, 21);
            this.ComboBox_MarkerType.TabIndex = 3;
            // 
            // Label_Type
            // 
            this.Label_Type.AutoSize = true;
            this.Label_Type.Location = new System.Drawing.Point(6, 29);
            this.Label_Type.Name = "Label_Type";
            this.Label_Type.Size = new System.Drawing.Size(34, 13);
            this.Label_Type.TabIndex = 2;
            this.Label_Type.Text = "Type:";
            // 
            // Numeric_Position
            // 
            this.Numeric_Position.Location = new System.Drawing.Point(220, 26);
            this.Numeric_Position.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.Numeric_Position.Name = "Numeric_Position";
            this.Numeric_Position.Size = new System.Drawing.Size(120, 20);
            this.Numeric_Position.TabIndex = 1;
            // 
            // Label_Position
            // 
            this.Label_Position.AutoSize = true;
            this.Label_Position.Location = new System.Drawing.Point(167, 29);
            this.Label_Position.Name = "Label_Position";
            this.Label_Position.Size = new System.Drawing.Size(47, 13);
            this.Label_Position.TabIndex = 0;
            this.Label_Position.Text = "Position:";
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(385, 512);
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
            this.Button_Cancel.Location = new System.Drawing.Point(466, 512);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Label_StartMarkers
            // 
            this.Label_StartMarkers.AutoSize = true;
            this.Label_StartMarkers.Location = new System.Drawing.Point(9, 107);
            this.Label_StartMarkers.Name = "Label_StartMarkers";
            this.Label_StartMarkers.Size = new System.Drawing.Size(73, 13);
            this.Label_StartMarkers.TabIndex = 4;
            this.Label_StartMarkers.Text = "Start Markers:";
            // 
            // ListView_Markers
            // 
            this.ListView_Markers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Markers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_MarkerPos,
            this.Col_IsInstant,
            this.Col_StateA,
            this.Col_StateB,
            this.Col_StartMarkerCount});
            this.ListView_Markers.FullRowSelect = true;
            this.ListView_Markers.GridLines = true;
            this.ListView_Markers.HideSelection = false;
            this.ListView_Markers.Location = new System.Drawing.Point(12, 123);
            this.ListView_Markers.Name = "ListView_Markers";
            this.ListView_Markers.Size = new System.Drawing.Size(529, 145);
            this.ListView_Markers.TabIndex = 8;
            this.ListView_Markers.UseCompatibleStateImageBehavior = false;
            this.ListView_Markers.View = System.Windows.Forms.View.Details;
            // 
            // Col_MarkerPos
            // 
            this.Col_MarkerPos.Text = "Marker Pos";
            this.Col_MarkerPos.Width = 115;
            // 
            // Col_IsInstant
            // 
            this.Col_IsInstant.Text = "Is Instant";
            // 
            // Col_StateA
            // 
            this.Col_StateA.Text = "State A";
            this.Col_StateA.Width = 80;
            // 
            // Col_StateB
            // 
            this.Col_StateB.Text = "State B";
            this.Col_StateB.Width = 79;
            // 
            // Col_StartMarkerCount
            // 
            this.Col_StartMarkerCount.Text = "Marker Count";
            this.Col_StartMarkerCount.Width = 118;
            // 
            // Label_MarkerData
            // 
            this.Label_MarkerData.AutoSize = true;
            this.Label_MarkerData.Location = new System.Drawing.Point(12, 271);
            this.Label_MarkerData.Name = "Label_MarkerData";
            this.Label_MarkerData.Size = new System.Drawing.Size(48, 13);
            this.Label_MarkerData.TabIndex = 9;
            this.Label_MarkerData.Text = "Markers:";
            // 
            // ListView_MarkerData
            // 
            this.ListView_MarkerData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_MarkerData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_Position,
            this.Col_MusicMarkerType,
            this.Col_Flags,
            this.Col_Extra,
            this.Col_LoopStart,
            this.Col_MarkerCount,
            this.Col_LoopMarkerCount});
            this.ListView_MarkerData.FullRowSelect = true;
            this.ListView_MarkerData.GridLines = true;
            this.ListView_MarkerData.HideSelection = false;
            this.ListView_MarkerData.Location = new System.Drawing.Point(12, 287);
            this.ListView_MarkerData.Name = "ListView_MarkerData";
            this.ListView_MarkerData.Size = new System.Drawing.Size(529, 219);
            this.ListView_MarkerData.TabIndex = 10;
            this.ListView_MarkerData.UseCompatibleStateImageBehavior = false;
            this.ListView_MarkerData.View = System.Windows.Forms.View.Details;
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            // 
            // Col_Position
            // 
            this.Col_Position.Text = "Position";
            this.Col_Position.Width = 56;
            // 
            // Col_MusicMarkerType
            // 
            this.Col_MusicMarkerType.Text = "Type";
            this.Col_MusicMarkerType.Width = 51;
            // 
            // Col_Flags
            // 
            this.Col_Flags.Text = "Flags";
            this.Col_Flags.Width = 50;
            // 
            // Col_Extra
            // 
            this.Col_Extra.Text = "Extra";
            // 
            // Col_LoopStart
            // 
            this.Col_LoopStart.Text = "Loop Start";
            this.Col_LoopStart.Width = 69;
            // 
            // Col_MarkerCount
            // 
            this.Col_MarkerCount.Text = "Count";
            this.Col_MarkerCount.Width = 54;
            // 
            // Col_LoopMarkerCount
            // 
            this.Col_LoopMarkerCount.Text = "Loop Marker Count";
            this.Col_LoopMarkerCount.Width = 108;
            // 
            // Frm_StreamSounds_MarkersEditor
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(553, 547);
            this.Controls.Add(this.ListView_MarkerData);
            this.Controls.Add(this.Label_MarkerData);
            this.Controls.Add(this.ListView_Markers);
            this.Controls.Add(this.Label_StartMarkers);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.GroupBox_MarkerData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StreamSounds_MarkersEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_StreamSounds_MarkersEditor";
            this.Load += new System.EventHandler(this.Frm_StreamSounds_MarkersEditor_Load);
            this.GroupBox_MarkerData.ResumeLayout(false);
            this.GroupBox_MarkerData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MarkerLoopStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_Position)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GroupBox_MarkerData;
        private System.Windows.Forms.NumericUpDown Numeric_MarkerLoopStart;
        private System.Windows.Forms.Label Label_LoopStart;
        private System.Windows.Forms.ComboBox ComboBox_MarkerType;
        private System.Windows.Forms.Label Label_Type;
        private System.Windows.Forms.NumericUpDown Numeric_Position;
        private System.Windows.Forms.Label Label_Position;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_StartMarkers;
        private System.Windows.Forms.Button Button_AddMarker;
        private System.Windows.Forms.ListView ListView_Markers;
        private System.Windows.Forms.Button Button_Clear;
        private System.Windows.Forms.ColumnHeader Col_MarkerPos;
        private System.Windows.Forms.ColumnHeader Col_StateA;
        private System.Windows.Forms.ColumnHeader Col_StateB;
        private System.Windows.Forms.Label Label_MarkerData;
        private System.Windows.Forms.ListView ListView_MarkerData;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Position;
        private System.Windows.Forms.ColumnHeader Col_MusicMarkerType;
        private System.Windows.Forms.ColumnHeader Col_Flags;
        private System.Windows.Forms.ColumnHeader Col_LoopStart;
        private System.Windows.Forms.ColumnHeader Col_MarkerCount;
        private System.Windows.Forms.ColumnHeader Col_LoopMarkerCount;
        private System.Windows.Forms.ColumnHeader Col_StartMarkerCount;
        private System.Windows.Forms.ColumnHeader Col_IsInstant;
        private System.Windows.Forms.ColumnHeader Col_Extra;
    }
}