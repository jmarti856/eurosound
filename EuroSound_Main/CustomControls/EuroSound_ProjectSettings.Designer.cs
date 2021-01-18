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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Textbox_Musics_Path = new System.Windows.Forms.TextBox();
            this.Label_MusicEvents_Path = new System.Windows.Forms.Label();
            this.Label_SectionMusic = new System.Windows.Forms.Label();
            this.Textbox_SFXData_Path = new System.Windows.Forms.TextBox();
            this.Label_SFXData_Path = new System.Windows.Forms.Label();
            this.Label_MusicDefines = new System.Windows.Forms.Label();
            this.Textbox_Sounds_Path = new System.Windows.Forms.TextBox();
            this.Label_sfx_path = new System.Windows.Forms.Label();
            this.Label_HT_Sound = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Combobox_TypeOfData = new System.Windows.Forms.ComboBox();
            this.GroupBox_HashTable.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.Textbox_FileName.Location = new System.Drawing.Point(72, 12);
            this.Textbox_FileName.MaxLength = 300;
            this.Textbox_FileName.Name = "Textbox_FileName";
            this.Textbox_FileName.Size = new System.Drawing.Size(391, 20);
            this.Textbox_FileName.TabIndex = 1;
            // 
            // GroupBox_HashTable
            // 
            this.GroupBox_HashTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_HashTable.Controls.Add(this.Combobox_FileHashcode);
            this.GroupBox_HashTable.Controls.Add(this.Label_HashTableEntry);
            this.GroupBox_HashTable.Location = new System.Drawing.Point(12, 272);
            this.GroupBox_HashTable.Name = "GroupBox_HashTable";
            this.GroupBox_HashTable.Size = new System.Drawing.Size(451, 61);
            this.GroupBox_HashTable.TabIndex = 5;
            this.GroupBox_HashTable.TabStop = false;
            this.GroupBox_HashTable.Text = "An entry for this ESF file can be placed in this hash table";
            // 
            // Combobox_FileHashcode
            // 
            this.Combobox_FileHashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_FileHashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_FileHashcode.FormattingEnabled = true;
            this.Combobox_FileHashcode.Location = new System.Drawing.Point(99, 19);
            this.Combobox_FileHashcode.Name = "Combobox_FileHashcode";
            this.Combobox_FileHashcode.Size = new System.Drawing.Size(346, 21);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Textbox_Musics_Path);
            this.groupBox1.Controls.Add(this.Label_MusicEvents_Path);
            this.groupBox1.Controls.Add(this.Label_SectionMusic);
            this.groupBox1.Controls.Add(this.Textbox_SFXData_Path);
            this.groupBox1.Controls.Add(this.Label_SFXData_Path);
            this.groupBox1.Controls.Add(this.Label_MusicDefines);
            this.groupBox1.Controls.Add(this.Textbox_Sounds_Path);
            this.groupBox1.Controls.Add(this.Label_sfx_path);
            this.groupBox1.Controls.Add(this.Label_HT_Sound);
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 195);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hash Table Configuration";
            // 
            // Textbox_Musics_Path
            // 
            this.Textbox_Musics_Path.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Musics_Path.Location = new System.Drawing.Point(44, 151);
            this.Textbox_Musics_Path.Name = "Textbox_Musics_Path";
            this.Textbox_Musics_Path.ReadOnly = true;
            this.Textbox_Musics_Path.Size = new System.Drawing.Size(401, 20);
            this.Textbox_Musics_Path.TabIndex = 9;
            // 
            // Label_MusicEvents_Path
            // 
            this.Label_MusicEvents_Path.AutoSize = true;
            this.Label_MusicEvents_Path.Location = new System.Drawing.Point(6, 154);
            this.Label_MusicEvents_Path.Name = "Label_MusicEvents_Path";
            this.Label_MusicEvents_Path.Size = new System.Drawing.Size(32, 13);
            this.Label_MusicEvents_Path.TabIndex = 8;
            this.Label_MusicEvents_Path.Text = "Path:";
            // 
            // Label_SectionMusic
            // 
            this.Label_SectionMusic.AutoSize = true;
            this.Label_SectionMusic.Location = new System.Drawing.Point(6, 135);
            this.Label_SectionMusic.Name = "Label_SectionMusic";
            this.Label_SectionMusic.Size = new System.Drawing.Size(98, 13);
            this.Label_SectionMusic.TabIndex = 7;
            this.Label_SectionMusic.Text = "Section: HT_Music";
            // 
            // Textbox_SFXData_Path
            // 
            this.Textbox_SFXData_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SFXData_Path.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_SFXData_Path.Location = new System.Drawing.Point(44, 96);
            this.Textbox_SFXData_Path.Name = "Textbox_SFXData_Path";
            this.Textbox_SFXData_Path.ReadOnly = true;
            this.Textbox_SFXData_Path.Size = new System.Drawing.Size(401, 20);
            this.Textbox_SFXData_Path.TabIndex = 6;
            // 
            // Label_SFXData_Path
            // 
            this.Label_SFXData_Path.AutoSize = true;
            this.Label_SFXData_Path.Location = new System.Drawing.Point(6, 99);
            this.Label_SFXData_Path.Name = "Label_SFXData_Path";
            this.Label_SFXData_Path.Size = new System.Drawing.Size(32, 13);
            this.Label_SFXData_Path.TabIndex = 5;
            this.Label_SFXData_Path.Text = "Path:";
            // 
            // Label_MusicDefines
            // 
            this.Label_MusicDefines.AutoSize = true;
            this.Label_MusicDefines.Location = new System.Drawing.Point(6, 80);
            this.Label_MusicDefines.Name = "Label_MusicDefines";
            this.Label_MusicDefines.Size = new System.Drawing.Size(119, 13);
            this.Label_MusicDefines.TabIndex = 4;
            this.Label_MusicDefines.Text = "Section: HT_SFX_Data";
            // 
            // Textbox_Sounds_Path
            // 
            this.Textbox_Sounds_Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_Sounds_Path.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Sounds_Path.Location = new System.Drawing.Point(44, 42);
            this.Textbox_Sounds_Path.Name = "Textbox_Sounds_Path";
            this.Textbox_Sounds_Path.ReadOnly = true;
            this.Textbox_Sounds_Path.Size = new System.Drawing.Size(401, 20);
            this.Textbox_Sounds_Path.TabIndex = 2;
            // 
            // Label_sfx_path
            // 
            this.Label_sfx_path.AutoSize = true;
            this.Label_sfx_path.Location = new System.Drawing.Point(6, 45);
            this.Label_sfx_path.Name = "Label_sfx_path";
            this.Label_sfx_path.Size = new System.Drawing.Size(32, 13);
            this.Label_sfx_path.TabIndex = 1;
            this.Label_sfx_path.Text = "Path:";
            // 
            // Label_HT_Sound
            // 
            this.Label_HT_Sound.AutoSize = true;
            this.Label_HT_Sound.Location = new System.Drawing.Point(6, 26);
            this.Label_HT_Sound.Name = "Label_HT_Sound";
            this.Label_HT_Sound.Size = new System.Drawing.Size(101, 13);
            this.Label_HT_Sound.TabIndex = 0;
            this.Label_HT_Sound.Text = "Section: HT_Sound";
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(307, 339);
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
            this.Button_Cancel.Location = new System.Drawing.Point(388, 339);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stored Data Type:";
            // 
            // Combobox_TypeOfData
            // 
            this.Combobox_TypeOfData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_TypeOfData.Enabled = false;
            this.Combobox_TypeOfData.FormattingEnabled = true;
            this.Combobox_TypeOfData.Items.AddRange(new object[] {
            "Soundbanks",
            "Streamed sounds",
            "Music tracks"});
            this.Combobox_TypeOfData.Location = new System.Drawing.Point(112, 38);
            this.Combobox_TypeOfData.Name = "Combobox_TypeOfData";
            this.Combobox_TypeOfData.Size = new System.Drawing.Size(351, 21);
            this.Combobox_TypeOfData.TabIndex = 3;
            // 
            // Frm_FileProperties
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(475, 379);
            this.Controls.Add(this.Combobox_TypeOfData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_FileName;
        private System.Windows.Forms.TextBox Textbox_FileName;
        private System.Windows.Forms.GroupBox GroupBox_HashTable;
        private System.Windows.Forms.ComboBox Combobox_FileHashcode;
        private System.Windows.Forms.Label Label_HashTableEntry;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Textbox_SFXData_Path;
        private System.Windows.Forms.Label Label_SFXData_Path;
        private System.Windows.Forms.Label Label_MusicDefines;
        private System.Windows.Forms.TextBox Textbox_Sounds_Path;
        private System.Windows.Forms.Label Label_sfx_path;
        private System.Windows.Forms.Label Label_HT_Sound;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Combobox_TypeOfData;
        private System.Windows.Forms.TextBox Textbox_Musics_Path;
        private System.Windows.Forms.Label Label_MusicEvents_Path;
        private System.Windows.Forms.Label Label_SectionMusic;
    }
}