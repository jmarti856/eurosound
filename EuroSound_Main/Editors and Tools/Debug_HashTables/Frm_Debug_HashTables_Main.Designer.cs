
namespace EuroSound_Application.Debug_HashTables
{
    partial class Frm_Debug_HashTables_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Debug_HashTables_Main));
            this.Groupbox_Console = new System.Windows.Forms.GroupBox();
            this.Textbox_Console = new System.Windows.Forms.TextBox();
            this.Button_HT_Sound = new System.Windows.Forms.Button();
            this.Button_MFX_ValidList = new System.Windows.Forms.Button();
            this.Groupbox_MFX = new System.Windows.Forms.GroupBox();
            this.Button_MFX_Data = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Label_OutputFolder = new System.Windows.Forms.Label();
            this.Textbox_OutputFolder = new System.Windows.Forms.TextBox();
            this.Button_SearchOutputFolder = new System.Windows.Forms.Button();
            this.Groupbox_Console.SuspendLayout();
            this.Groupbox_MFX.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Groupbox_Console
            // 
            this.Groupbox_Console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Console.Controls.Add(this.Textbox_Console);
            this.Groupbox_Console.Location = new System.Drawing.Point(12, 12);
            this.Groupbox_Console.Name = "Groupbox_Console";
            this.Groupbox_Console.Size = new System.Drawing.Size(312, 497);
            this.Groupbox_Console.TabIndex = 0;
            this.Groupbox_Console.TabStop = false;
            this.Groupbox_Console.Text = "Console:";
            // 
            // Textbox_Console
            // 
            this.Textbox_Console.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_Console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Textbox_Console.Location = new System.Drawing.Point(3, 16);
            this.Textbox_Console.Multiline = true;
            this.Textbox_Console.Name = "Textbox_Console";
            this.Textbox_Console.ReadOnly = true;
            this.Textbox_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Textbox_Console.Size = new System.Drawing.Size(306, 478);
            this.Textbox_Console.TabIndex = 0;
            // 
            // Button_HT_Sound
            // 
            this.Button_HT_Sound.Location = new System.Drawing.Point(6, 19);
            this.Button_HT_Sound.Name = "Button_HT_Sound";
            this.Button_HT_Sound.Size = new System.Drawing.Size(95, 43);
            this.Button_HT_Sound.TabIndex = 1;
            this.Button_HT_Sound.Text = "HT_Sound";
            this.Button_HT_Sound.UseVisualStyleBackColor = true;
            this.Button_HT_Sound.Click += new System.EventHandler(this.Button_HT_Sound_Click);
            // 
            // Button_MFX_ValidList
            // 
            this.Button_MFX_ValidList.Location = new System.Drawing.Point(6, 19);
            this.Button_MFX_ValidList.Name = "Button_MFX_ValidList";
            this.Button_MFX_ValidList.Size = new System.Drawing.Size(95, 43);
            this.Button_MFX_ValidList.TabIndex = 2;
            this.Button_MFX_ValidList.Text = "HT_ValidList";
            this.Button_MFX_ValidList.UseVisualStyleBackColor = true;
            this.Button_MFX_ValidList.Click += new System.EventHandler(this.Button_MFX_ValidList_Click);
            // 
            // Groupbox_MFX
            // 
            this.Groupbox_MFX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_MFX.Controls.Add(this.Button_MFX_Data);
            this.Groupbox_MFX.Controls.Add(this.Button_MFX_ValidList);
            this.Groupbox_MFX.Location = new System.Drawing.Point(330, 12);
            this.Groupbox_MFX.Name = "Groupbox_MFX";
            this.Groupbox_MFX.Size = new System.Drawing.Size(320, 100);
            this.Groupbox_MFX.TabIndex = 3;
            this.Groupbox_MFX.TabStop = false;
            this.Groupbox_MFX.Text = "MFX Hash Tables:";
            // 
            // Button_MFX_Data
            // 
            this.Button_MFX_Data.Location = new System.Drawing.Point(107, 19);
            this.Button_MFX_Data.Name = "Button_MFX_Data";
            this.Button_MFX_Data.Size = new System.Drawing.Size(95, 43);
            this.Button_MFX_Data.TabIndex = 3;
            this.Button_MFX_Data.Text = "HT_Data";
            this.Button_MFX_Data.UseVisualStyleBackColor = true;
            this.Button_MFX_Data.Click += new System.EventHandler(this.Button_MFX_Data_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Button_HT_Sound);
            this.groupBox1.Location = new System.Drawing.Point(330, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SFX Hash Tables:";
            // 
            // Label_OutputFolder
            // 
            this.Label_OutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_OutputFolder.AutoSize = true;
            this.Label_OutputFolder.Location = new System.Drawing.Point(330, 229);
            this.Label_OutputFolder.Name = "Label_OutputFolder";
            this.Label_OutputFolder.Size = new System.Drawing.Size(71, 13);
            this.Label_OutputFolder.TabIndex = 5;
            this.Label_OutputFolder.Text = "Output folder:";
            // 
            // Textbox_OutputFolder
            // 
            this.Textbox_OutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_OutputFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_OutputFolder.Location = new System.Drawing.Point(407, 226);
            this.Textbox_OutputFolder.Name = "Textbox_OutputFolder";
            this.Textbox_OutputFolder.ReadOnly = true;
            this.Textbox_OutputFolder.Size = new System.Drawing.Size(211, 20);
            this.Textbox_OutputFolder.TabIndex = 6;
            // 
            // Button_SearchOutputFolder
            // 
            this.Button_SearchOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SearchOutputFolder.Location = new System.Drawing.Point(624, 226);
            this.Button_SearchOutputFolder.Name = "Button_SearchOutputFolder";
            this.Button_SearchOutputFolder.Size = new System.Drawing.Size(26, 20);
            this.Button_SearchOutputFolder.TabIndex = 7;
            this.Button_SearchOutputFolder.Text = "...";
            this.Button_SearchOutputFolder.UseVisualStyleBackColor = true;
            this.Button_SearchOutputFolder.Click += new System.EventHandler(this.Button_SearchOutputFolder_Click);
            // 
            // Frm_Debug_HashTables_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 521);
            this.Controls.Add(this.Button_SearchOutputFolder);
            this.Controls.Add(this.Textbox_OutputFolder);
            this.Controls.Add(this.Label_OutputFolder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Groupbox_MFX);
            this.Controls.Add(this.Groupbox_Console);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_Debug_HashTables_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "HT_Debugger";
            this.Text = "HashTables Debugger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Debug_HashTables_Main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Debug_HashTables_Main_Load);
            this.Shown += new System.EventHandler(this.Frm_Debug_HashTables_Main_Shown);
            this.SizeChanged += new System.EventHandler(this.Frm_Debug_HashTables_Main_SizeChanged);
            this.Enter += new System.EventHandler(this.Frm_Debug_HashTables_Main_Enter);
            this.Groupbox_Console.ResumeLayout(false);
            this.Groupbox_Console.PerformLayout();
            this.Groupbox_MFX.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Groupbox_Console;
        private System.Windows.Forms.TextBox Textbox_Console;
        private System.Windows.Forms.Button Button_HT_Sound;
        private System.Windows.Forms.Button Button_MFX_ValidList;
        private System.Windows.Forms.GroupBox Groupbox_MFX;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_MFX_Data;
        private System.Windows.Forms.Label Label_OutputFolder;
        private System.Windows.Forms.TextBox Textbox_OutputFolder;
        private System.Windows.Forms.Button Button_SearchOutputFolder;
    }
}