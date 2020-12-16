
namespace EuroSound_Application
{
    partial class Frm_HashTablesConfig
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
            this.Panel_Title = new System.Windows.Forms.Panel();
            this.Label_Title = new System.Windows.Forms.Label();
            this.GroupBox_HT_SoundSection = new System.Windows.Forms.GroupBox();
            this.button_HT_Sound = new System.Windows.Forms.Button();
            this.Textbox_HT_Sound = new System.Windows.Forms.TextBox();
            this.label_HT_Sound_Section = new System.Windows.Forms.Label();
            this.Label_Hashcodes = new System.Windows.Forms.Label();
            this.GroupBox_HT_Music = new System.Windows.Forms.GroupBox();
            this.button_HT_SoundData = new System.Windows.Forms.Button();
            this.Textbox_HT_Sound_Data = new System.Windows.Forms.TextBox();
            this.Label_HT_SoundData = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_HT_Music = new System.Windows.Forms.Button();
            this.Textbox_HT_Music = new System.Windows.Forms.TextBox();
            this.Label_HT_Music = new System.Windows.Forms.Label();
            this.Panel_Title.SuspendLayout();
            this.GroupBox_HT_SoundSection.SuspendLayout();
            this.GroupBox_HT_Music.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(491, 23);
            this.Panel_Title.TabIndex = 0;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(159, 4);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(167, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Hash Tables Configuration";
            // 
            // GroupBox_HT_SoundSection
            // 
            this.GroupBox_HT_SoundSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_HT_SoundSection.Controls.Add(this.button_HT_Sound);
            this.GroupBox_HT_SoundSection.Controls.Add(this.Textbox_HT_Sound);
            this.GroupBox_HT_SoundSection.Controls.Add(this.label_HT_Sound_Section);
            this.GroupBox_HT_SoundSection.Location = new System.Drawing.Point(12, 84);
            this.GroupBox_HT_SoundSection.Name = "GroupBox_HT_SoundSection";
            this.GroupBox_HT_SoundSection.Size = new System.Drawing.Size(467, 76);
            this.GroupBox_HT_SoundSection.TabIndex = 3;
            this.GroupBox_HT_SoundSection.TabStop = false;
            this.GroupBox_HT_SoundSection.Text = "HT_Sound";
            // 
            // button_HT_Sound
            // 
            this.button_HT_Sound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_HT_Sound.Location = new System.Drawing.Point(406, 27);
            this.button_HT_Sound.Name = "button_HT_Sound";
            this.button_HT_Sound.Size = new System.Drawing.Size(52, 20);
            this.button_HT_Sound.TabIndex = 2;
            this.button_HT_Sound.Text = "Browse";
            this.button_HT_Sound.UseVisualStyleBackColor = true;
            this.button_HT_Sound.Click += new System.EventHandler(this.Button_HT_Sound_Click);
            // 
            // Textbox_HT_Sound
            // 
            this.Textbox_HT_Sound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_HT_Sound.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_HT_Sound.Location = new System.Drawing.Point(99, 27);
            this.Textbox_HT_Sound.Name = "Textbox_HT_Sound";
            this.Textbox_HT_Sound.ReadOnly = true;
            this.Textbox_HT_Sound.Size = new System.Drawing.Size(301, 20);
            this.Textbox_HT_Sound.TabIndex = 1;
            // 
            // label_HT_Sound_Section
            // 
            this.label_HT_Sound_Section.AutoSize = true;
            this.label_HT_Sound_Section.Location = new System.Drawing.Point(6, 30);
            this.label_HT_Sound_Section.Name = "label_HT_Sound_Section";
            this.label_HT_Sound_Section.Size = new System.Drawing.Size(87, 13);
            this.label_HT_Sound_Section.TabIndex = 0;
            this.label_HT_Sound_Section.Text = "Hash table name";
            // 
            // Label_Hashcodes
            // 
            this.Label_Hashcodes.AutoSize = true;
            this.Label_Hashcodes.Location = new System.Drawing.Point(12, 41);
            this.Label_Hashcodes.MaximumSize = new System.Drawing.Size(500, 0);
            this.Label_Hashcodes.Name = "Label_Hashcodes";
            this.Label_Hashcodes.Size = new System.Drawing.Size(478, 26);
            this.Label_Hashcodes.TabIndex = 2;
            this.Label_Hashcodes.Text = "A project can use a shared header file to record all item identities, with unique" +
    " identifier values being assigned by Eurosound to each item.";
            // 
            // GroupBox_HT_Music
            // 
            this.GroupBox_HT_Music.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_HT_Music.Controls.Add(this.button_HT_SoundData);
            this.GroupBox_HT_Music.Controls.Add(this.Textbox_HT_Sound_Data);
            this.GroupBox_HT_Music.Controls.Add(this.Label_HT_SoundData);
            this.GroupBox_HT_Music.Location = new System.Drawing.Point(12, 166);
            this.GroupBox_HT_Music.Name = "GroupBox_HT_Music";
            this.GroupBox_HT_Music.Size = new System.Drawing.Size(467, 76);
            this.GroupBox_HT_Music.TabIndex = 4;
            this.GroupBox_HT_Music.TabStop = false;
            this.GroupBox_HT_Music.Text = "HT_Sound_Data";
            // 
            // button_HT_SoundData
            // 
            this.button_HT_SoundData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_HT_SoundData.Location = new System.Drawing.Point(406, 27);
            this.button_HT_SoundData.Name = "button_HT_SoundData";
            this.button_HT_SoundData.Size = new System.Drawing.Size(52, 20);
            this.button_HT_SoundData.TabIndex = 2;
            this.button_HT_SoundData.Text = "Browse";
            this.button_HT_SoundData.UseVisualStyleBackColor = true;
            this.button_HT_SoundData.Click += new System.EventHandler(this.Button_HT_SoundData_Click);
            // 
            // Textbox_HT_Sound_Data
            // 
            this.Textbox_HT_Sound_Data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_HT_Sound_Data.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_HT_Sound_Data.Location = new System.Drawing.Point(99, 27);
            this.Textbox_HT_Sound_Data.Name = "Textbox_HT_Sound_Data";
            this.Textbox_HT_Sound_Data.ReadOnly = true;
            this.Textbox_HT_Sound_Data.Size = new System.Drawing.Size(301, 20);
            this.Textbox_HT_Sound_Data.TabIndex = 1;
            // 
            // Label_HT_SoundData
            // 
            this.Label_HT_SoundData.AutoSize = true;
            this.Label_HT_SoundData.Location = new System.Drawing.Point(6, 30);
            this.Label_HT_SoundData.Name = "Label_HT_SoundData";
            this.Label_HT_SoundData.Size = new System.Drawing.Size(87, 13);
            this.Label_HT_SoundData.TabIndex = 0;
            this.Label_HT_SoundData.Text = "Hash table name";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button_HT_Music);
            this.groupBox1.Controls.Add(this.Textbox_HT_Music);
            this.groupBox1.Controls.Add(this.Label_HT_Music);
            this.groupBox1.Location = new System.Drawing.Point(12, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HT_Music";
            // 
            // button_HT_Music
            // 
            this.button_HT_Music.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_HT_Music.Location = new System.Drawing.Point(406, 27);
            this.button_HT_Music.Name = "button_HT_Music";
            this.button_HT_Music.Size = new System.Drawing.Size(52, 20);
            this.button_HT_Music.TabIndex = 2;
            this.button_HT_Music.Text = "Browse";
            this.button_HT_Music.UseVisualStyleBackColor = true;
            this.button_HT_Music.Click += new System.EventHandler(this.Button_HT_Music_Click);
            // 
            // Textbox_HT_Music
            // 
            this.Textbox_HT_Music.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_HT_Music.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_HT_Music.Location = new System.Drawing.Point(99, 27);
            this.Textbox_HT_Music.Name = "Textbox_HT_Music";
            this.Textbox_HT_Music.ReadOnly = true;
            this.Textbox_HT_Music.Size = new System.Drawing.Size(301, 20);
            this.Textbox_HT_Music.TabIndex = 1;
            // 
            // Label_HT_Music
            // 
            this.Label_HT_Music.AutoSize = true;
            this.Label_HT_Music.Location = new System.Drawing.Point(6, 30);
            this.Label_HT_Music.Name = "Label_HT_Music";
            this.Label_HT_Music.Size = new System.Drawing.Size(87, 13);
            this.Label_HT_Music.TabIndex = 0;
            this.Label_HT_Music.Text = "Hash table name";
            // 
            // Frm_HashTablesConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox_HT_Music);
            this.Controls.Add(this.Label_Hashcodes);
            this.Controls.Add(this.GroupBox_HT_SoundSection);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_HashTablesConfig";
            this.Text = "Frm_HashTablesConfig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_HashTablesConfig_FormClosing);
            this.Load += new System.EventHandler(this.Frm_HashTablesConfig_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.GroupBox_HT_SoundSection.ResumeLayout(false);
            this.GroupBox_HT_SoundSection.PerformLayout();
            this.GroupBox_HT_Music.ResumeLayout(false);
            this.GroupBox_HT_Music.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.GroupBox GroupBox_HT_SoundSection;
        private System.Windows.Forms.Button button_HT_Sound;
        private System.Windows.Forms.TextBox Textbox_HT_Sound;
        private System.Windows.Forms.Label label_HT_Sound_Section;
        private System.Windows.Forms.Label Label_Hashcodes;
        private System.Windows.Forms.GroupBox GroupBox_HT_Music;
        private System.Windows.Forms.Button button_HT_SoundData;
        private System.Windows.Forms.TextBox Textbox_HT_Sound_Data;
        private System.Windows.Forms.Label Label_HT_SoundData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_HT_Music;
        private System.Windows.Forms.TextBox Textbox_HT_Music;
        private System.Windows.Forms.Label Label_HT_Music;
    }
}