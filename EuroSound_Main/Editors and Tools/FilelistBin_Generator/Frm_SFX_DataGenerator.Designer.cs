
using EuroSound_Application.CustomControls.ListViewColumnSorting;

namespace EuroSound_Application.SFXData
{
    partial class Frm_SFX_DataGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SFX_DataGenerator));
            this.label_hashcode = new System.Windows.Forms.Label();
            this.button_generateFile = new System.Windows.Forms.Button();
            this.Textbox_SelectedHashcode = new System.Windows.Forms.TextBox();
            this.Button_Reload = new System.Windows.Forms.Button();
            this.Label_Hashcodes = new System.Windows.Forms.Label();
            this.Combobox_LabelHashcodes = new System.Windows.Forms.ComboBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.Label_TotalItems = new System.Windows.Forms.Label();
            this.Textbox_TotalItems = new System.Windows.Forms.TextBox();
            this.ListView_HashTableData = new EuroSound_Application.CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick();
            this.col_hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_innerradius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_outerradius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_altertness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_looping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_tracking3d = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_samplestreamed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.Textbox_OutputDirectory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_hashcode
            // 
            this.label_hashcode.AutoSize = true;
            this.label_hashcode.Location = new System.Drawing.Point(12, 15);
            this.label_hashcode.Name = "label_hashcode";
            this.label_hashcode.Size = new System.Drawing.Size(59, 13);
            this.label_hashcode.TabIndex = 0;
            this.label_hashcode.Text = "Hashcode:";
            // 
            // button_generateFile
            // 
            this.button_generateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generateFile.Location = new System.Drawing.Point(542, 553);
            this.button_generateFile.Name = "button_generateFile";
            this.button_generateFile.Size = new System.Drawing.Size(75, 23);
            this.button_generateFile.TabIndex = 8;
            this.button_generateFile.Text = "Generate";
            this.button_generateFile.UseVisualStyleBackColor = true;
            this.button_generateFile.Click += new System.EventHandler(this.Button_generateFile_Click);
            // 
            // Textbox_SelectedHashcode
            // 
            this.Textbox_SelectedHashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SelectedHashcode.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_SelectedHashcode.Location = new System.Drawing.Point(77, 12);
            this.Textbox_SelectedHashcode.Name = "Textbox_SelectedHashcode";
            this.Textbox_SelectedHashcode.ReadOnly = true;
            this.Textbox_SelectedHashcode.Size = new System.Drawing.Size(650, 20);
            this.Textbox_SelectedHashcode.TabIndex = 1;
            // 
            // Button_Reload
            // 
            this.Button_Reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Reload.Location = new System.Drawing.Point(623, 553);
            this.Button_Reload.Name = "Button_Reload";
            this.Button_Reload.Size = new System.Drawing.Size(104, 23);
            this.Button_Reload.TabIndex = 9;
            this.Button_Reload.Text = "Reload Hashtable";
            this.Button_Reload.UseVisualStyleBackColor = true;
            this.Button_Reload.Click += new System.EventHandler(this.Button_Reload_Click);
            // 
            // Label_Hashcodes
            // 
            this.Label_Hashcodes.AutoSize = true;
            this.Label_Hashcodes.Location = new System.Drawing.Point(30, 41);
            this.Label_Hashcodes.Name = "Label_Hashcodes";
            this.Label_Hashcodes.Size = new System.Drawing.Size(41, 13);
            this.Label_Hashcodes.TabIndex = 2;
            this.Label_Hashcodes.Text = "Labels:";
            // 
            // Combobox_LabelHashcodes
            // 
            this.Combobox_LabelHashcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_LabelHashcodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.Combobox_LabelHashcodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Combobox_LabelHashcodes.FormattingEnabled = true;
            this.Combobox_LabelHashcodes.Location = new System.Drawing.Point(77, 38);
            this.Combobox_LabelHashcodes.Name = "Combobox_LabelHashcodes";
            this.Combobox_LabelHashcodes.Size = new System.Drawing.Size(569, 21);
            this.Combobox_LabelHashcodes.TabIndex = 3;
            this.Combobox_LabelHashcodes.Click += new System.EventHandler(this.Combobox_LabelHashcodes_Click);
            // 
            // Button_Search
            // 
            this.Button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Search.Location = new System.Drawing.Point(652, 36);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(75, 23);
            this.Button_Search.TabIndex = 4;
            this.Button_Search.Text = "Search";
            this.Button_Search.UseVisualStyleBackColor = true;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // Label_TotalItems
            // 
            this.Label_TotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_TotalItems.AutoSize = true;
            this.Label_TotalItems.ForeColor = System.Drawing.Color.Green;
            this.Label_TotalItems.Location = new System.Drawing.Point(9, 558);
            this.Label_TotalItems.Name = "Label_TotalItems";
            this.Label_TotalItems.Size = new System.Drawing.Size(62, 13);
            this.Label_TotalItems.TabIndex = 6;
            this.Label_TotalItems.Text = "Total Items:";
            // 
            // Textbox_TotalItems
            // 
            this.Textbox_TotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Textbox_TotalItems.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_TotalItems.Location = new System.Drawing.Point(77, 555);
            this.Textbox_TotalItems.Name = "Textbox_TotalItems";
            this.Textbox_TotalItems.ReadOnly = true;
            this.Textbox_TotalItems.Size = new System.Drawing.Size(100, 20);
            this.Textbox_TotalItems.TabIndex = 7;
            // 
            // ListView_HashTableData
            // 
            this.ListView_HashTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_HashTableData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_hashcode,
            this.col_innerradius,
            this.col_outerradius,
            this.col_altertness,
            this.col_duration,
            this.col_looping,
            this.col_tracking3d,
            this.col_samplestreamed});
            this.ListView_HashTableData.FullRowSelect = true;
            this.ListView_HashTableData.GridLines = true;
            this.ListView_HashTableData.HideSelection = false;
            this.ListView_HashTableData.Location = new System.Drawing.Point(12, 71);
            this.ListView_HashTableData.Name = "ListView_HashTableData";
            this.ListView_HashTableData.Size = new System.Drawing.Size(715, 476);
            this.ListView_HashTableData.TabIndex = 5;
            this.ListView_HashTableData.UseCompatibleStateImageBehavior = false;
            this.ListView_HashTableData.View = System.Windows.Forms.View.Details;
            this.ListView_HashTableData.SelectedIndexChanged += new System.EventHandler(this.ListView_HashTableData_SelectedIndexChanged);
            // 
            // col_hashcode
            // 
            this.col_hashcode.Text = "HashCode";
            this.col_hashcode.Width = 75;
            // 
            // col_innerradius
            // 
            this.col_innerradius.Text = "Inner Radius";
            this.col_innerradius.Width = 87;
            // 
            // col_outerradius
            // 
            this.col_outerradius.Text = "Outer Radius";
            this.col_outerradius.Width = 80;
            // 
            // col_altertness
            // 
            this.col_altertness.Text = "Altertness";
            this.col_altertness.Width = 70;
            // 
            // col_duration
            // 
            this.col_duration.Text = "Duration";
            this.col_duration.Width = 126;
            // 
            // col_looping
            // 
            this.col_looping.Text = "Looping";
            // 
            // col_tracking3d
            // 
            this.col_tracking3d.Text = "Tracking 3D";
            this.col_tracking3d.Width = 83;
            // 
            // col_samplestreamed
            // 
            this.col_samplestreamed.Text = "Sample Streamed";
            this.col_samplestreamed.Width = 109;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 558);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Output Directory:";
            // 
            // Textbox_OutputDirectory
            // 
            this.Textbox_OutputDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_OutputDirectory.Location = new System.Drawing.Point(276, 555);
            this.Textbox_OutputDirectory.Name = "Textbox_OutputDirectory";
            this.Textbox_OutputDirectory.ReadOnly = true;
            this.Textbox_OutputDirectory.Size = new System.Drawing.Size(222, 20);
            this.Textbox_OutputDirectory.TabIndex = 11;
            // 
            // Frm_SFX_DataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 588);
            this.Controls.Add(this.Textbox_OutputDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Textbox_TotalItems);
            this.Controls.Add(this.Label_TotalItems);
            this.Controls.Add(this.Button_Search);
            this.Controls.Add(this.Combobox_LabelHashcodes);
            this.Controls.Add(this.Label_Hashcodes);
            this.Controls.Add(this.Button_Reload);
            this.Controls.Add(this.Textbox_SelectedHashcode);
            this.Controls.Add(this.button_generateFile);
            this.Controls.Add(this.label_hashcode);
            this.Controls.Add(this.ListView_HashTableData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_SFX_DataGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "SFXData";
            this.Text = "SFX Data Table";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SFX_DataGenerator_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SFX_DataGenerator_Load);
            this.Shown += new System.EventHandler(this.Frm_SFX_DataGenerator_Shown);
            this.SizeChanged += new System.EventHandler(this.Frm_SFX_DataGenerator_SizeChanged);
            this.Enter += new System.EventHandler(this.Frm_SFX_DataGenerator_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView_ColumnSortingClick ListView_HashTableData;
        private System.Windows.Forms.ColumnHeader col_hashcode;
        private System.Windows.Forms.ColumnHeader col_innerradius;
        private System.Windows.Forms.ColumnHeader col_outerradius;
        private System.Windows.Forms.ColumnHeader col_altertness;
        private System.Windows.Forms.ColumnHeader col_duration;
        private System.Windows.Forms.ColumnHeader col_looping;
        private System.Windows.Forms.ColumnHeader col_tracking3d;
        private System.Windows.Forms.ColumnHeader col_samplestreamed;
        private System.Windows.Forms.Label label_hashcode;
        private System.Windows.Forms.Button button_generateFile;
        private System.Windows.Forms.TextBox Textbox_SelectedHashcode;
        private System.Windows.Forms.Button Button_Reload;
        private System.Windows.Forms.Label Label_Hashcodes;
        private System.Windows.Forms.ComboBox Combobox_LabelHashcodes;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.Label Label_TotalItems;
        private System.Windows.Forms.TextBox Textbox_TotalItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Textbox_OutputDirectory;
    }
}