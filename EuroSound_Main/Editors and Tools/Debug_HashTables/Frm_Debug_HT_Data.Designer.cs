
namespace EuroSound_Application.Debug_HashTables.HT_Data
{
    partial class Frm_Debug_HT_Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Debug_HT_Data));
            this.DataGridView_HT_Content = new System.Windows.Forms.DataGridView();
            this.Col_HashCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_HashC_Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_DurationInSeconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Looping = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GroupBox_Options = new System.Windows.Forms.GroupBox();
            this.Button_DeleteItems = new System.Windows.Forms.Button();
            this.Button_Add = new System.Windows.Forms.Button();
            this.ComboBox_Looping = new System.Windows.Forms.ComboBox();
            this.Label_Looping = new System.Windows.Forms.Label();
            this.Numeric_MusicDuration = new System.Windows.Forms.NumericUpDown();
            this.Label_Duration = new System.Windows.Forms.Label();
            this.Combobox_HashCode = new System.Windows.Forms.ComboBox();
            this.Label_Hashcode = new System.Windows.Forms.Label();
            this.Button_Generate = new System.Windows.Forms.Button();
            this.Button_Close = new System.Windows.Forms.Button();
            this.GroupBox_LoadHashTable = new System.Windows.Forms.GroupBox();
            this.Button_HashTablePath = new System.Windows.Forms.Button();
            this.Textbox_FilePath = new System.Windows.Forms.TextBox();
            this.Label_FilePath = new System.Windows.Forms.Label();
            this.Label_UserInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_HT_Content)).BeginInit();
            this.GroupBox_Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MusicDuration)).BeginInit();
            this.GroupBox_LoadHashTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView_HT_Content
            // 
            this.DataGridView_HT_Content.AllowUserToAddRows = false;
            this.DataGridView_HT_Content.AllowUserToResizeColumns = false;
            this.DataGridView_HT_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_HT_Content.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_HT_Content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_HT_Content.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_HashCode,
            this.Col_HashC_Label,
            this.Col_DurationInSeconds,
            this.Col_Looping});
            this.DataGridView_HT_Content.Location = new System.Drawing.Point(12, 170);
            this.DataGridView_HT_Content.Name = "DataGridView_HT_Content";
            this.DataGridView_HT_Content.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridView_HT_Content.Size = new System.Drawing.Size(638, 378);
            this.DataGridView_HT_Content.TabIndex = 0;
            // 
            // Col_HashCode
            // 
            this.Col_HashCode.HeaderText = "HashCode";
            this.Col_HashCode.Name = "Col_HashCode";
            this.Col_HashCode.ReadOnly = true;
            this.Col_HashCode.Visible = false;
            // 
            // Col_HashC_Label
            // 
            this.Col_HashC_Label.HeaderText = "HashCode Label";
            this.Col_HashC_Label.Name = "Col_HashC_Label";
            this.Col_HashC_Label.ReadOnly = true;
            // 
            // Col_DurationInSeconds
            // 
            this.Col_DurationInSeconds.HeaderText = "Duration (sec)";
            this.Col_DurationInSeconds.Name = "Col_DurationInSeconds";
            // 
            // Col_Looping
            // 
            this.Col_Looping.HeaderText = "Looping";
            this.Col_Looping.Items.AddRange(new object[] {
            "False",
            "True"});
            this.Col_Looping.Name = "Col_Looping";
            // 
            // GroupBox_Options
            // 
            this.GroupBox_Options.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Options.Controls.Add(this.Button_DeleteItems);
            this.GroupBox_Options.Controls.Add(this.Button_Add);
            this.GroupBox_Options.Controls.Add(this.ComboBox_Looping);
            this.GroupBox_Options.Controls.Add(this.Label_Looping);
            this.GroupBox_Options.Controls.Add(this.Numeric_MusicDuration);
            this.GroupBox_Options.Controls.Add(this.Label_Duration);
            this.GroupBox_Options.Controls.Add(this.Combobox_HashCode);
            this.GroupBox_Options.Controls.Add(this.Label_Hashcode);
            this.GroupBox_Options.Location = new System.Drawing.Point(12, 86);
            this.GroupBox_Options.Name = "GroupBox_Options";
            this.GroupBox_Options.Size = new System.Drawing.Size(638, 78);
            this.GroupBox_Options.TabIndex = 1;
            this.GroupBox_Options.TabStop = false;
            this.GroupBox_Options.Text = "Options:";
            // 
            // Button_DeleteItems
            // 
            this.Button_DeleteItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_DeleteItems.Location = new System.Drawing.Point(557, 49);
            this.Button_DeleteItems.Name = "Button_DeleteItems";
            this.Button_DeleteItems.Size = new System.Drawing.Size(75, 23);
            this.Button_DeleteItems.TabIndex = 8;
            this.Button_DeleteItems.Text = "Delete";
            this.Button_DeleteItems.UseVisualStyleBackColor = true;
            this.Button_DeleteItems.Click += new System.EventHandler(this.Button_DeleteItems_Click);
            // 
            // Button_Add
            // 
            this.Button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Add.Location = new System.Drawing.Point(476, 49);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(75, 23);
            this.Button_Add.TabIndex = 7;
            this.Button_Add.Text = "Add";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // ComboBox_Looping
            // 
            this.ComboBox_Looping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Looping.FormattingEnabled = true;
            this.ComboBox_Looping.Items.AddRange(new object[] {
            "False",
            "True"});
            this.ComboBox_Looping.Location = new System.Drawing.Point(492, 19);
            this.ComboBox_Looping.Name = "ComboBox_Looping";
            this.ComboBox_Looping.Size = new System.Drawing.Size(137, 21);
            this.ComboBox_Looping.TabIndex = 6;
            // 
            // Label_Looping
            // 
            this.Label_Looping.AutoSize = true;
            this.Label_Looping.Location = new System.Drawing.Point(438, 22);
            this.Label_Looping.Name = "Label_Looping";
            this.Label_Looping.Size = new System.Drawing.Size(48, 13);
            this.Label_Looping.TabIndex = 5;
            this.Label_Looping.Text = "Looping:";
            // 
            // Numeric_MusicDuration
            // 
            this.Numeric_MusicDuration.DecimalPlaces = 5;
            this.Numeric_MusicDuration.Location = new System.Drawing.Point(312, 20);
            this.Numeric_MusicDuration.Maximum = new decimal(new int[] {
            1674494636,
            1802994560,
            1844674297,
            0});
            this.Numeric_MusicDuration.Name = "Numeric_MusicDuration";
            this.Numeric_MusicDuration.Size = new System.Drawing.Size(120, 20);
            this.Numeric_MusicDuration.TabIndex = 4;
            // 
            // Label_Duration
            // 
            this.Label_Duration.AutoSize = true;
            this.Label_Duration.Location = new System.Drawing.Point(219, 22);
            this.Label_Duration.Name = "Label_Duration";
            this.Label_Duration.Size = new System.Drawing.Size(87, 13);
            this.Label_Duration.TabIndex = 3;
            this.Label_Duration.Text = "Duration (in sec):";
            // 
            // Combobox_HashCode
            // 
            this.Combobox_HashCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.Combobox_HashCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Combobox_HashCode.FormattingEnabled = true;
            this.Combobox_HashCode.Location = new System.Drawing.Point(72, 19);
            this.Combobox_HashCode.Name = "Combobox_HashCode";
            this.Combobox_HashCode.Size = new System.Drawing.Size(141, 21);
            this.Combobox_HashCode.TabIndex = 2;
            // 
            // Label_Hashcode
            // 
            this.Label_Hashcode.AutoSize = true;
            this.Label_Hashcode.Location = new System.Drawing.Point(6, 22);
            this.Label_Hashcode.Name = "Label_Hashcode";
            this.Label_Hashcode.Size = new System.Drawing.Size(60, 13);
            this.Label_Hashcode.TabIndex = 0;
            this.Label_Hashcode.Text = "HashCode:";
            // 
            // Button_Generate
            // 
            this.Button_Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Generate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Generate.Location = new System.Drawing.Point(494, 557);
            this.Button_Generate.Name = "Button_Generate";
            this.Button_Generate.Size = new System.Drawing.Size(75, 23);
            this.Button_Generate.TabIndex = 2;
            this.Button_Generate.Text = "Generate";
            this.Button_Generate.UseVisualStyleBackColor = true;
            this.Button_Generate.Click += new System.EventHandler(this.Button_Generate_Click);
            // 
            // Button_Close
            // 
            this.Button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Close.Location = new System.Drawing.Point(575, 557);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(75, 23);
            this.Button_Close.TabIndex = 3;
            this.Button_Close.Text = "Close";
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // GroupBox_LoadHashTable
            // 
            this.GroupBox_LoadHashTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_LoadHashTable.Controls.Add(this.Button_HashTablePath);
            this.GroupBox_LoadHashTable.Controls.Add(this.Textbox_FilePath);
            this.GroupBox_LoadHashTable.Controls.Add(this.Label_FilePath);
            this.GroupBox_LoadHashTable.Controls.Add(this.Label_UserInfo);
            this.GroupBox_LoadHashTable.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_LoadHashTable.Name = "GroupBox_LoadHashTable";
            this.GroupBox_LoadHashTable.Size = new System.Drawing.Size(638, 68);
            this.GroupBox_LoadHashTable.TabIndex = 4;
            this.GroupBox_LoadHashTable.TabStop = false;
            this.GroupBox_LoadHashTable.Text = "Load Existing HashTable:";
            // 
            // Button_HashTablePath
            // 
            this.Button_HashTablePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_HashTablePath.Location = new System.Drawing.Point(608, 32);
            this.Button_HashTablePath.Name = "Button_HashTablePath";
            this.Button_HashTablePath.Size = new System.Drawing.Size(24, 20);
            this.Button_HashTablePath.TabIndex = 3;
            this.Button_HashTablePath.Text = "...";
            this.Button_HashTablePath.UseVisualStyleBackColor = true;
            this.Button_HashTablePath.Click += new System.EventHandler(this.Button_HashTablePath_Click);
            // 
            // Textbox_FilePath
            // 
            this.Textbox_FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_FilePath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_FilePath.Location = new System.Drawing.Point(62, 32);
            this.Textbox_FilePath.Name = "Textbox_FilePath";
            this.Textbox_FilePath.ReadOnly = true;
            this.Textbox_FilePath.Size = new System.Drawing.Size(540, 20);
            this.Textbox_FilePath.TabIndex = 2;
            // 
            // Label_FilePath
            // 
            this.Label_FilePath.AutoSize = true;
            this.Label_FilePath.Location = new System.Drawing.Point(6, 35);
            this.Label_FilePath.Name = "Label_FilePath";
            this.Label_FilePath.Size = new System.Drawing.Size(50, 13);
            this.Label_FilePath.TabIndex = 1;
            this.Label_FilePath.Text = "File path:";
            // 
            // Label_UserInfo
            // 
            this.Label_UserInfo.AutoSize = true;
            this.Label_UserInfo.Location = new System.Drawing.Point(6, 16);
            this.Label_UserInfo.Name = "Label_UserInfo";
            this.Label_UserInfo.Size = new System.Drawing.Size(442, 13);
            this.Label_UserInfo.TabIndex = 0;
            this.Label_UserInfo.Text = "If you previously have a HashTable generated with the MFX Data, you can read it f" +
    "rom here.";
            // 
            // Frm_Debug_HT_Data
            // 
            this.AcceptButton = this.Button_Generate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Close;
            this.ClientSize = new System.Drawing.Size(662, 592);
            this.Controls.Add(this.GroupBox_LoadHashTable);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.Button_Generate);
            this.Controls.Add(this.GroupBox_Options);
            this.Controls.Add(this.DataGridView_HT_Content);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_Debug_HT_Data";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "MusicData";
            this.Text = "Music Data Table Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Debug_HT_Data_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Debug_HT_Data_Load);
            this.Shown += new System.EventHandler(this.Frm_Debug_HT_Data_Shown);
            this.SizeChanged += new System.EventHandler(this.Frm_Debug_HT_Data_SizeChanged);
            this.Enter += new System.EventHandler(this.Frm_Debug_HT_Data_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_HT_Content)).EndInit();
            this.GroupBox_Options.ResumeLayout(false);
            this.GroupBox_Options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_MusicDuration)).EndInit();
            this.GroupBox_LoadHashTable.ResumeLayout(false);
            this.GroupBox_LoadHashTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_HT_Content;
        private System.Windows.Forms.GroupBox GroupBox_Options;
        private System.Windows.Forms.Button Button_Generate;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Label Label_Hashcode;
        private System.Windows.Forms.ComboBox Combobox_HashCode;
        private System.Windows.Forms.NumericUpDown Numeric_MusicDuration;
        private System.Windows.Forms.Label Label_Duration;
        private System.Windows.Forms.Button Button_DeleteItems;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.ComboBox ComboBox_Looping;
        private System.Windows.Forms.Label Label_Looping;
        private System.Windows.Forms.GroupBox GroupBox_LoadHashTable;
        private System.Windows.Forms.Label Label_UserInfo;
        private System.Windows.Forms.TextBox Textbox_FilePath;
        private System.Windows.Forms.Label Label_FilePath;
        private System.Windows.Forms.Button Button_HashTablePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_HashCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_HashC_Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_DurationInSeconds;
        private System.Windows.Forms.DataGridViewComboBoxColumn Col_Looping;
    }
}