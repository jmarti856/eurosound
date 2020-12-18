
namespace EuroSound_Application
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
            this.BackgroundWorker_LoadData = new System.ComponentModel.BackgroundWorker();
            this.FolderSavePath = new System.Windows.Forms.FolderBrowserDialog();
            this.Textbox_SelectedHashcode = new System.Windows.Forms.TextBox();
            this.Button_Reload = new System.Windows.Forms.Button();
            this.ListView_HashTableData = new ListViewExtendedMethods.ListView_ColumnSortingClick();
            this.col_hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_innerradius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_outerradius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_altertness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_looping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_tracking3d = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_samplestreamed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label_hashcode
            // 
            this.label_hashcode.AutoSize = true;
            this.label_hashcode.Location = new System.Drawing.Point(9, 15);
            this.label_hashcode.Name = "label_hashcode";
            this.label_hashcode.Size = new System.Drawing.Size(59, 13);
            this.label_hashcode.TabIndex = 1;
            this.label_hashcode.Text = "Hashcode:";
            // 
            // button_generateFile
            // 
            this.button_generateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generateFile.Location = new System.Drawing.Point(473, 553);
            this.button_generateFile.Name = "button_generateFile";
            this.button_generateFile.Size = new System.Drawing.Size(128, 23);
            this.button_generateFile.TabIndex = 6;
            this.button_generateFile.Text = "Generate SFX_Data.bin";
            this.button_generateFile.UseVisualStyleBackColor = true;
            this.button_generateFile.Click += new System.EventHandler(this.Button_generateFile_Click);
            // 
            // BackgroundWorker_LoadData
            // 
            this.BackgroundWorker_LoadData.WorkerReportsProgress = true;
            this.BackgroundWorker_LoadData.WorkerSupportsCancellation = true;
            this.BackgroundWorker_LoadData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_LoadData_DoWork);
            this.BackgroundWorker_LoadData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_LoadData_RunWorkerCompleted);
            // 
            // Textbox_SelectedHashcode
            // 
            this.Textbox_SelectedHashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_SelectedHashcode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_SelectedHashcode.Location = new System.Drawing.Point(74, 12);
            this.Textbox_SelectedHashcode.Name = "Textbox_SelectedHashcode";
            this.Textbox_SelectedHashcode.ReadOnly = true;
            this.Textbox_SelectedHashcode.Size = new System.Drawing.Size(637, 20);
            this.Textbox_SelectedHashcode.TabIndex = 8;
            // 
            // Button_Reload
            // 
            this.Button_Reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Reload.Location = new System.Drawing.Point(607, 553);
            this.Button_Reload.Name = "Button_Reload";
            this.Button_Reload.Size = new System.Drawing.Size(104, 23);
            this.Button_Reload.TabIndex = 9;
            this.Button_Reload.Text = "Reload HashTable";
            this.Button_Reload.UseVisualStyleBackColor = true;
            this.Button_Reload.Click += new System.EventHandler(this.Button_Reload_Click);
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
            this.ListView_HashTableData.Location = new System.Drawing.Point(12, 38);
            this.ListView_HashTableData.Name = "ListView_HashTableData";
            this.ListView_HashTableData.Size = new System.Drawing.Size(699, 509);
            this.ListView_HashTableData.TabIndex = 0;
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
            // Frm_SFX_DataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 588);
            this.Controls.Add(this.Button_Reload);
            this.Controls.Add(this.Textbox_SelectedHashcode);
            this.Controls.Add(this.button_generateFile);
            this.Controls.Add(this.label_hashcode);
            this.Controls.Add(this.ListView_HashTableData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_SFX_DataGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate SFX Data Binary File";
            this.Load += new System.EventHandler(this.Frm_FilelistBinGenerator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewExtendedMethods.ListView_ColumnSortingClick ListView_HashTableData;
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
        private System.ComponentModel.BackgroundWorker BackgroundWorker_LoadData;
        private System.Windows.Forms.FolderBrowserDialog FolderSavePath;
        private System.Windows.Forms.TextBox Textbox_SelectedHashcode;
        private System.Windows.Forms.Button Button_Reload;
    }
}