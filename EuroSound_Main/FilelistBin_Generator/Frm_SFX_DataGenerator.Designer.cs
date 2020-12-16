
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
            this.combobox_hashcode = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numeric_samplestreamed = new System.Windows.Forms.NumericUpDown();
            this.label_samplestreamed = new System.Windows.Forms.Label();
            this.numeric_tracking3d = new System.Windows.Forms.NumericUpDown();
            this.label_tracking3d = new System.Windows.Forms.Label();
            this.numeric_looping = new System.Windows.Forms.NumericUpDown();
            this.label_looping = new System.Windows.Forms.Label();
            this.numeric_duration = new System.Windows.Forms.NumericUpDown();
            this.label_duration = new System.Windows.Forms.Label();
            this.numeric_altertness = new System.Windows.Forms.NumericUpDown();
            this.label_altertness = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label_outerradius = new System.Windows.Forms.Label();
            this.numeric_innerradius = new System.Windows.Forms.NumericUpDown();
            this.label_innerradius = new System.Windows.Forms.Label();
            this.button_deleteselection = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.button_generateFile = new System.Windows.Forms.Button();
            this.BackgroundWorker_LoadData = new System.ComponentModel.BackgroundWorker();
            this.ProgressBar_Loading = new System.Windows.Forms.ProgressBar();
            this.listView_ColumnSortingClick1 = new ListViewExtendedMethods.ListView_ColumnSortingClick();
            this.col_hashcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_innerradius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_outerradius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_altertness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_looping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_tracking3d = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_samplestreamed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FolderSavePath = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_samplestreamed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_tracking3d)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_looping)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_duration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_altertness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_innerradius)).BeginInit();
            this.SuspendLayout();
            // 
            // label_hashcode
            // 
            this.label_hashcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_hashcode.AutoSize = true;
            this.label_hashcode.Location = new System.Drawing.Point(648, 12);
            this.label_hashcode.Name = "label_hashcode";
            this.label_hashcode.Size = new System.Drawing.Size(59, 13);
            this.label_hashcode.TabIndex = 1;
            this.label_hashcode.Text = "Hashcode:";
            // 
            // combobox_hashcode
            // 
            this.combobox_hashcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combobox_hashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_hashcode.DropDownWidth = 340;
            this.combobox_hashcode.FormattingEnabled = true;
            this.combobox_hashcode.Location = new System.Drawing.Point(648, 28);
            this.combobox_hashcode.Name = "combobox_hashcode";
            this.combobox_hashcode.Size = new System.Drawing.Size(196, 21);
            this.combobox_hashcode.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numeric_samplestreamed);
            this.groupBox1.Controls.Add(this.label_samplestreamed);
            this.groupBox1.Controls.Add(this.numeric_tracking3d);
            this.groupBox1.Controls.Add(this.label_tracking3d);
            this.groupBox1.Controls.Add(this.numeric_looping);
            this.groupBox1.Controls.Add(this.label_looping);
            this.groupBox1.Controls.Add(this.numeric_duration);
            this.groupBox1.Controls.Add(this.label_duration);
            this.groupBox1.Controls.Add(this.numeric_altertness);
            this.groupBox1.Controls.Add(this.label_altertness);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label_outerradius);
            this.groupBox1.Controls.Add(this.numeric_innerradius);
            this.groupBox1.Controls.Add(this.label_innerradius);
            this.groupBox1.Location = new System.Drawing.Point(648, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 297);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Values:";
            // 
            // numeric_samplestreamed
            // 
            this.numeric_samplestreamed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numeric_samplestreamed.Location = new System.Drawing.Point(6, 266);
            this.numeric_samplestreamed.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric_samplestreamed.Name = "numeric_samplestreamed";
            this.numeric_samplestreamed.Size = new System.Drawing.Size(184, 20);
            this.numeric_samplestreamed.TabIndex = 13;
            // 
            // label_samplestreamed
            // 
            this.label_samplestreamed.AutoSize = true;
            this.label_samplestreamed.Location = new System.Drawing.Point(6, 250);
            this.label_samplestreamed.Name = "label_samplestreamed";
            this.label_samplestreamed.Size = new System.Drawing.Size(90, 13);
            this.label_samplestreamed.TabIndex = 12;
            this.label_samplestreamed.Text = "Sample Streamed";
            // 
            // numeric_tracking3d
            // 
            this.numeric_tracking3d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numeric_tracking3d.Location = new System.Drawing.Point(6, 227);
            this.numeric_tracking3d.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeric_tracking3d.Name = "numeric_tracking3d";
            this.numeric_tracking3d.Size = new System.Drawing.Size(184, 20);
            this.numeric_tracking3d.TabIndex = 11;
            // 
            // label_tracking3d
            // 
            this.label_tracking3d.AutoSize = true;
            this.label_tracking3d.Location = new System.Drawing.Point(6, 211);
            this.label_tracking3d.Name = "label_tracking3d";
            this.label_tracking3d.Size = new System.Drawing.Size(66, 13);
            this.label_tracking3d.TabIndex = 10;
            this.label_tracking3d.Text = "Tracking 3D";
            // 
            // numeric_looping
            // 
            this.numeric_looping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numeric_looping.Location = new System.Drawing.Point(6, 188);
            this.numeric_looping.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numeric_looping.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numeric_looping.Name = "numeric_looping";
            this.numeric_looping.Size = new System.Drawing.Size(184, 20);
            this.numeric_looping.TabIndex = 9;
            // 
            // label_looping
            // 
            this.label_looping.AutoSize = true;
            this.label_looping.Location = new System.Drawing.Point(6, 172);
            this.label_looping.Name = "label_looping";
            this.label_looping.Size = new System.Drawing.Size(45, 13);
            this.label_looping.TabIndex = 8;
            this.label_looping.Text = "Looping";
            // 
            // numeric_duration
            // 
            this.numeric_duration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numeric_duration.Location = new System.Drawing.Point(6, 149);
            this.numeric_duration.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numeric_duration.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numeric_duration.Name = "numeric_duration";
            this.numeric_duration.Size = new System.Drawing.Size(184, 20);
            this.numeric_duration.TabIndex = 7;
            // 
            // label_duration
            // 
            this.label_duration.AutoSize = true;
            this.label_duration.Location = new System.Drawing.Point(6, 133);
            this.label_duration.Name = "label_duration";
            this.label_duration.Size = new System.Drawing.Size(47, 13);
            this.label_duration.TabIndex = 6;
            this.label_duration.Text = "Duration";
            // 
            // numeric_altertness
            // 
            this.numeric_altertness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numeric_altertness.Location = new System.Drawing.Point(6, 110);
            this.numeric_altertness.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numeric_altertness.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numeric_altertness.Name = "numeric_altertness";
            this.numeric_altertness.Size = new System.Drawing.Size(184, 20);
            this.numeric_altertness.TabIndex = 5;
            // 
            // label_altertness
            // 
            this.label_altertness.AutoSize = true;
            this.label_altertness.Location = new System.Drawing.Point(6, 94);
            this.label_altertness.Name = "label_altertness";
            this.label_altertness.Size = new System.Drawing.Size(53, 13);
            this.label_altertness.TabIndex = 4;
            this.label_altertness.Text = "Altertness";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(6, 71);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(184, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // label_outerradius
            // 
            this.label_outerradius.AutoSize = true;
            this.label_outerradius.Location = new System.Drawing.Point(6, 55);
            this.label_outerradius.Name = "label_outerradius";
            this.label_outerradius.Size = new System.Drawing.Size(69, 13);
            this.label_outerradius.TabIndex = 2;
            this.label_outerradius.Text = "Outer Radius";
            // 
            // numeric_innerradius
            // 
            this.numeric_innerradius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numeric_innerradius.Location = new System.Drawing.Point(6, 32);
            this.numeric_innerradius.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numeric_innerradius.Minimum = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.numeric_innerradius.Name = "numeric_innerradius";
            this.numeric_innerradius.Size = new System.Drawing.Size(184, 20);
            this.numeric_innerradius.TabIndex = 1;
            // 
            // label_innerradius
            // 
            this.label_innerradius.AutoSize = true;
            this.label_innerradius.Location = new System.Drawing.Point(6, 16);
            this.label_innerradius.Name = "label_innerradius";
            this.label_innerradius.Size = new System.Drawing.Size(67, 13);
            this.label_innerradius.TabIndex = 0;
            this.label_innerradius.Text = "Inner Radius";
            // 
            // button_deleteselection
            // 
            this.button_deleteselection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteselection.Location = new System.Drawing.Point(648, 387);
            this.button_deleteselection.Name = "button_deleteselection";
            this.button_deleteselection.Size = new System.Drawing.Size(196, 23);
            this.button_deleteselection.TabIndex = 4;
            this.button_deleteselection.Text = "Delete Selection";
            this.button_deleteselection.UseVisualStyleBackColor = true;
            this.button_deleteselection.Click += new System.EventHandler(this.Button_deleteselection_Click);
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add.Location = new System.Drawing.Point(648, 358);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(196, 23);
            this.button_add.TabIndex = 5;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.Button_add_Click);
            // 
            // button_generateFile
            // 
            this.button_generateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generateFile.Location = new System.Drawing.Point(648, 416);
            this.button_generateFile.Name = "button_generateFile";
            this.button_generateFile.Size = new System.Drawing.Size(196, 23);
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
            // ProgressBar_Loading
            // 
            this.ProgressBar_Loading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Loading.Location = new System.Drawing.Point(648, 509);
            this.ProgressBar_Loading.Name = "ProgressBar_Loading";
            this.ProgressBar_Loading.Size = new System.Drawing.Size(196, 23);
            this.ProgressBar_Loading.TabIndex = 7;
            // 
            // listView_ColumnSortingClick1
            // 
            this.listView_ColumnSortingClick1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_ColumnSortingClick1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_hashcode,
            this.col_innerradius,
            this.col_outerradius,
            this.col_altertness,
            this.col_duration,
            this.col_looping,
            this.col_tracking3d,
            this.col_samplestreamed});
            this.listView_ColumnSortingClick1.FullRowSelect = true;
            this.listView_ColumnSortingClick1.GridLines = true;
            this.listView_ColumnSortingClick1.HideSelection = false;
            this.listView_ColumnSortingClick1.Location = new System.Drawing.Point(12, 12);
            this.listView_ColumnSortingClick1.Name = "listView_ColumnSortingClick1";
            this.listView_ColumnSortingClick1.Size = new System.Drawing.Size(630, 520);
            this.listView_ColumnSortingClick1.TabIndex = 0;
            this.listView_ColumnSortingClick1.UseCompatibleStateImageBehavior = false;
            this.listView_ColumnSortingClick1.View = System.Windows.Forms.View.Details;
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
            this.col_samplestreamed.Width = 98;
            // 
            // Frm_SFX_DataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 544);
            this.Controls.Add(this.ProgressBar_Loading);
            this.Controls.Add(this.button_generateFile);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.button_deleteselection);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.combobox_hashcode);
            this.Controls.Add(this.label_hashcode);
            this.Controls.Add(this.listView_ColumnSortingClick1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_SFX_DataGenerator";
            this.Text = "Generate SFX Data Binary File";
            this.Load += new System.EventHandler(this.Frm_FilelistBinGenerator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_samplestreamed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_tracking3d)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_looping)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_duration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_altertness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_innerradius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewExtendedMethods.ListView_ColumnSortingClick listView_ColumnSortingClick1;
        private System.Windows.Forms.ColumnHeader col_hashcode;
        private System.Windows.Forms.ColumnHeader col_innerradius;
        private System.Windows.Forms.ColumnHeader col_outerradius;
        private System.Windows.Forms.ColumnHeader col_altertness;
        private System.Windows.Forms.ColumnHeader col_duration;
        private System.Windows.Forms.ColumnHeader col_looping;
        private System.Windows.Forms.ColumnHeader col_tracking3d;
        private System.Windows.Forms.ColumnHeader col_samplestreamed;
        private System.Windows.Forms.Label label_hashcode;
        private System.Windows.Forms.ComboBox combobox_hashcode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numeric_samplestreamed;
        private System.Windows.Forms.Label label_samplestreamed;
        private System.Windows.Forms.NumericUpDown numeric_tracking3d;
        private System.Windows.Forms.Label label_tracking3d;
        private System.Windows.Forms.NumericUpDown numeric_looping;
        private System.Windows.Forms.Label label_looping;
        private System.Windows.Forms.NumericUpDown numeric_duration;
        private System.Windows.Forms.Label label_duration;
        private System.Windows.Forms.NumericUpDown numeric_altertness;
        private System.Windows.Forms.Label label_altertness;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label_outerradius;
        private System.Windows.Forms.NumericUpDown numeric_innerradius;
        private System.Windows.Forms.Label label_innerradius;
        private System.Windows.Forms.Button button_deleteselection;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_generateFile;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_LoadData;
        private System.Windows.Forms.ProgressBar ProgressBar_Loading;
        private System.Windows.Forms.FolderBrowserDialog FolderSavePath;
    }
}