namespace EuroSound_Application.CustomControls.ProjectSettings
{
    partial class Frm_FileProperties
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
            this.Label_FileName = new System.Windows.Forms.Label();
            this.Textbox_FileName = new System.Windows.Forms.TextBox();
            this.GroupBox_HashTable = new System.Windows.Forms.GroupBox();
            this.Combobox_FileHashcode = new System.Windows.Forms.ComboBox();
            this.Label_HashTableEntry = new System.Windows.Forms.Label();
            this.GroupBox_HashTableConfig = new System.Windows.Forms.GroupBox();
            this.Label_Hashcodes = new System.Windows.Forms.Label();
            this.Textbox_Musics_Path = new System.Windows.Forms.TextBox();
            this.Label_MusicEvents_Path = new System.Windows.Forms.Label();
            this.Label_SectionMusic = new System.Windows.Forms.Label();
            this.Textbox_SFXData_Path = new System.Windows.Forms.TextBox();
            this.Label_SFXData_Path = new System.Windows.Forms.Label();
            this.Label_MusicDefines = new System.Windows.Forms.Label();
            this.Textbox_Sounds_Path = new System.Windows.Forms.TextBox();
            this.Label_SFX_Path = new System.Windows.Forms.Label();
            this.Label_HT_Sound = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_DataType = new System.Windows.Forms.Label();
            this.Combobox_TypeOfData = new System.Windows.Forms.ComboBox();
            this.GroupBox_HashTable.SuspendLayout();
            this.GroupBox_HashTableConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_FileName
            // 
            this.Label_FileName.AutoSize = true;
            this.Label_FileName.Location = new System.Drawing.Point(12, 15);
            this.Label_FileName.Name = "Label_FileName";
            this.Label_FileName.Size = new System.Drawing.Size(57, 13);
            this.Label_FileName.TabIndex = 0;
            this.Label_FileName.Text = "File Name:";
            // 
            // Textbox_FileName
            // 
            this.Textbox_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_FileName.Location = new System.Drawing.Point(75, 12);
            this.Textbox_FileName.MaxLength = 300;
            this.Textbox_FileName.Name = "Textbox_FileName";
            this.Textbox_FileName.Size = new System.Drawing.Size(401, 20);
            this.Textbox_FileName.TabIndex = 1;
            // 
            // GroupBox_HashTable
            // 
            this.GroupBox_HashTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_HashTable.Controls.Add(this.Combobox_FileHashcode);
            this.GroupBox_HashTable.Controls.Add(this.Label_HashTableEntry);
            this.GroupBox_HashTable.Location = new System.Drawing.Point(12, 305);
            this.GroupBox_HashTable.Name = "GroupBox_HashTable";
            this.GroupBox_HashTable.Size = new System.Drawing.Size(464, 61);
            this.GroupBox_HashTable.TabIndex = 5;
            this.GroupBox_HashTable.TabStop = false;
            this.GroupBox_HashTable.Text = "An entry for this ESF file can be placed in this hash table:";
            // 
            // Combobox_FileHashcode
            // 
            this.Combobox_FileHashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_FileHashcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.Combobox_FileHashcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Combobox_FileHashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_FileHashcode.FormattingEnabled = true;
            this.Combobox_FileHashcode.Location = new System.Drawing.Point(99, 19);
            this.Combobox_FileHashcode.Name = "Combobox_FileHashcode";
            this.Combobox_FileHashcode.Size = new System.Drawing.Size(359, 21);
            this.Combobox_FileHashcode.TabIndex = 1;
            this.Combobox_FileHashcode.Click += new System.EventHandler(this.Combobox_FileHashcode_Click);
            // 
            // Label_HashTableEntry
            // 
            this.Label_HashTableEntry.AutoSize = true;
            this.Label_HashTableEntry.Location = new System.Drawing.Point(6, 22);
            this.Label_HashTableEntry.Name = "Label_HashTableEntry";
            this.Label_HashTableEntry.Size = new System.Drawing.Size(87, 13);
            this.Label_HashTableEntry.TabIndex = 0;
            this.Label_HashTableEntry.Text = "Hash table entry:";
            // 
            // GroupBox_HashTableConfig
            // 
            this.GroupBox_HashTableConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_Hashcodes);
            this.GroupBox_HashTableConfig.Controls.Add(this.Textbox_Musics_Path);
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_MusicEvents_Path);
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_SectionMusic);
            this.GroupBox_HashTableConfig.Controls.Add(this.Textbox_SFXData_Path);
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_SFXData_Path);
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_MusicDefines);
            this.GroupBox_HashTableConfig.Controls.Add(this.Textbox_Sounds_Path);
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_SFX_Path);
            this.GroupBox_HashTableConfig.Controls.Add(this.Label_HT_Sound);
            this.GroupBox_HashTableConfig.Location = new System.Drawing.Point(12, 71);
            this.GroupBox_HashTableConfig.Name = "GroupBox_HashTableConfig";
            this.GroupBox_HashTableConfig.Size = new System.Drawing.Size(464, 228);
            this.GroupBox_HashTableConfig.TabIndex = 4;
            this.GroupBox_HashTableConfig.TabStop = false;
            this.GroupBox_HashTableConfig.Text = "Hash Table Configuration:";
            // 
            // Label_Hashcodes
            // 
            this.Label_Hashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Hashcodes.Location = new System.Drawing.Point(6, 16);
            this.Label_Hashcodes.MaximumSize = new System.Drawing.Size(500, 0);
            this.Label_Hashcodes.MinimumSize = new System.Drawing.Size(452, 26);
            this.Label_Hashcodes.Name = "Label_Hashcodes";
            this.Label_Hashcodes.Size = new System.Drawing.Size(452, 26);
            this.Label_Hashcodes.TabIndex = 8;
            this.Label_Hashcodes.Text = "A project can use a shared header file to record all item identities, with unique" +
    " identifier values being assigned by Eurosound to each item.";
            // 
            // Textbox_Musics_Path
            // 
            this.Textbox_Musics_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_Musics_Path.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Musics_Path.Location = new System.Drawing.Point(44, 179);
            this.Textbox_Musics_Path.Name = "Textbox_Musics_Path";
            this.Textbox_Musics_Path.ReadOnly = true;
            this.Textbox_Musics_Path.Size = new System.Drawing.Size(414, 20);
            this.Textbox_Musics_Path.TabIndex = 9;
            // 
            // Label_MusicEvents_Path
            // 
            this.Label_MusicEvents_Path.AutoSize = true;
            this.Label_MusicEvents_Path.Location = new System.Drawing.Point(6, 182);
            this.Label_MusicEvents_Path.Name = "Label_MusicEvents_Path";
            this.Label_MusicEvents_Path.Size = new System.Drawing.Size(32, 13);
            this.Label_MusicEvents_Path.TabIndex = 8;
            this.Label_MusicEvents_Path.Text = "Path:";
            // 
            // Label_SectionMusic
            // 
            this.Label_SectionMusic.AutoSize = true;
            this.Label_SectionMusic.Location = new System.Drawing.Point(6, 163);
            this.Label_SectionMusic.Name = "Label_SectionMusic";
            this.Label_SectionMusic.Size = new System.Drawing.Size(98, 13);
            this.Label_SectionMusic.TabIndex = 7;
            this.Label_SectionMusic.Text = "Section: HT_Music";
            // 
            // Textbox_SFXData_Path
            // 
            this.Textbox_SFXData_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SFXData_Path.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_SFXData_Path.Location = new System.Drawing.Point(44, 124);
            this.Textbox_SFXData_Path.Name = "Textbox_SFXData_Path";
            this.Textbox_SFXData_Path.ReadOnly = true;
            this.Textbox_SFXData_Path.Size = new System.Drawing.Size(414, 20);
            this.Textbox_SFXData_Path.TabIndex = 6;
            // 
            // Label_SFXData_Path
            // 
            this.Label_SFXData_Path.AutoSize = true;
            this.Label_SFXData_Path.Location = new System.Drawing.Point(6, 127);
            this.Label_SFXData_Path.Name = "Label_SFXData_Path";
            this.Label_SFXData_Path.Size = new System.Drawing.Size(32, 13);
            this.Label_SFXData_Path.TabIndex = 5;
            this.Label_SFXData_Path.Text = "Path:";
            // 
            // Label_MusicDefines
            // 
            this.Label_MusicDefines.AutoSize = true;
            this.Label_MusicDefines.Location = new System.Drawing.Point(6, 108);
            this.Label_MusicDefines.Name = "Label_MusicDefines";
            this.Label_MusicDefines.Size = new System.Drawing.Size(119, 13);
            this.Label_MusicDefines.TabIndex = 4;
            this.Label_MusicDefines.Text = "Section: HT_SFX_Data";
            // 
            // Textbox_Sounds_Path
            // 
            this.Textbox_Sounds_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_Sounds_Path.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Sounds_Path.Location = new System.Drawing.Point(44, 70);
            this.Textbox_Sounds_Path.Name = "Textbox_Sounds_Path";
            this.Textbox_Sounds_Path.ReadOnly = true;
            this.Textbox_Sounds_Path.Size = new System.Drawing.Size(414, 20);
            this.Textbox_Sounds_Path.TabIndex = 2;
            // 
            // Label_SFX_Path
            // 
            this.Label_SFX_Path.AutoSize = true;
            this.Label_SFX_Path.Location = new System.Drawing.Point(6, 73);
            this.Label_SFX_Path.Name = "Label_SFX_Path";
            this.Label_SFX_Path.Size = new System.Drawing.Size(32, 13);
            this.Label_SFX_Path.TabIndex = 1;
            this.Label_SFX_Path.Text = "Path:";
            // 
            // Label_HT_Sound
            // 
            this.Label_HT_Sound.AutoSize = true;
            this.Label_HT_Sound.Location = new System.Drawing.Point(6, 54);
            this.Label_HT_Sound.Name = "Label_HT_Sound";
            this.Label_HT_Sound.Size = new System.Drawing.Size(101, 13);
            this.Label_HT_Sound.TabIndex = 0;
            this.Label_HT_Sound.Text = "Section: HT_Sound";
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(320, 372);
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
            this.Button_Cancel.Location = new System.Drawing.Point(401, 372);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Label_DataType
            // 
            this.Label_DataType.AutoSize = true;
            this.Label_DataType.Location = new System.Drawing.Point(12, 41);
            this.Label_DataType.Name = "Label_DataType";
            this.Label_DataType.Size = new System.Drawing.Size(94, 13);
            this.Label_DataType.TabIndex = 2;
            this.Label_DataType.Text = "Stored Data Type:";
            // 
            // Combobox_TypeOfData
            // 
            this.Combobox_TypeOfData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_TypeOfData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_TypeOfData.Enabled = false;
            this.Combobox_TypeOfData.FormattingEnabled = true;
            this.Combobox_TypeOfData.Items.AddRange(new object[] {
            "Soundbanks",
            "Streamed sounds",
            "Music tracks"});
            this.Combobox_TypeOfData.Location = new System.Drawing.Point(112, 38);
            this.Combobox_TypeOfData.Name = "Combobox_TypeOfData";
            this.Combobox_TypeOfData.Size = new System.Drawing.Size(363, 21);
            this.Combobox_TypeOfData.TabIndex = 3;
            // 
            // Frm_FileProperties
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(488, 407);
            this.Controls.Add(this.Combobox_TypeOfData);
            this.Controls.Add(this.Label_DataType);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.GroupBox_HashTableConfig);
            this.Controls.Add(this.GroupBox_HashTable);
            this.Controls.Add(this.Textbox_FileName);
            this.Controls.Add(this.Label_FileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_FileProperties";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Options";
            this.Load += new System.EventHandler(this.Frm_FileProperties_Load);
            this.Shown += new System.EventHandler(this.Frm_FileProperties_Shown);
            this.GroupBox_HashTable.ResumeLayout(false);
            this.GroupBox_HashTable.PerformLayout();
            this.GroupBox_HashTableConfig.ResumeLayout(false);
            this.GroupBox_HashTableConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_FileName;
        private System.Windows.Forms.TextBox Textbox_FileName;
        private System.Windows.Forms.GroupBox GroupBox_HashTable;
        private System.Windows.Forms.ComboBox Combobox_FileHashcode;
        private System.Windows.Forms.Label Label_HashTableEntry;
        private System.Windows.Forms.GroupBox GroupBox_HashTableConfig;
        private System.Windows.Forms.TextBox Textbox_SFXData_Path;
        private System.Windows.Forms.Label Label_SFXData_Path;
        private System.Windows.Forms.Label Label_MusicDefines;
        private System.Windows.Forms.TextBox Textbox_Sounds_Path;
        private System.Windows.Forms.Label Label_SFX_Path;
        private System.Windows.Forms.Label Label_HT_Sound;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_DataType;
        private System.Windows.Forms.ComboBox Combobox_TypeOfData;
        private System.Windows.Forms.TextBox Textbox_Musics_Path;
        private System.Windows.Forms.Label Label_MusicEvents_Path;
        private System.Windows.Forms.Label Label_SectionMusic;
        private System.Windows.Forms.Label Label_Hashcodes;
    }
}