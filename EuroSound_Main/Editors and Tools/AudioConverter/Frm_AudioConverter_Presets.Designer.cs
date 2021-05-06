
namespace EuroSound_Application.AudioConverter
{
    partial class Frm_AudioConverter_Presets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AudioConverter_Presets));
            this.Groupbox_AvailablePresets = new System.Windows.Forms.GroupBox();
            this.ListBox_Presets = new System.Windows.Forms.ListBox();
            this.Label_Frequency = new System.Windows.Forms.Label();
            this.Textbox_Frequency = new System.Windows.Forms.TextBox();
            this.Label_Bits = new System.Windows.Forms.Label();
            this.Textbox_Bits = new System.Windows.Forms.TextBox();
            this.Label_Channels = new System.Windows.Forms.Label();
            this.Textbox_Channels = new System.Windows.Forms.TextBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Groupbox_Values = new System.Windows.Forms.GroupBox();
            this.GroupBox_Desc = new System.Windows.Forms.GroupBox();
            this.Textbox_Desc = new System.Windows.Forms.TextBox();
            this.Groupbox_AvailablePresets.SuspendLayout();
            this.Groupbox_Values.SuspendLayout();
            this.GroupBox_Desc.SuspendLayout();
            this.SuspendLayout();
            // 
            // Groupbox_AvailablePresets
            // 
            this.Groupbox_AvailablePresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Groupbox_AvailablePresets.Controls.Add(this.ListBox_Presets);
            this.Groupbox_AvailablePresets.Location = new System.Drawing.Point(12, 12);
            this.Groupbox_AvailablePresets.Name = "Groupbox_AvailablePresets";
            this.Groupbox_AvailablePresets.Size = new System.Drawing.Size(224, 435);
            this.Groupbox_AvailablePresets.TabIndex = 0;
            this.Groupbox_AvailablePresets.TabStop = false;
            this.Groupbox_AvailablePresets.Text = "Presets:";
            // 
            // ListBox_Presets
            // 
            this.ListBox_Presets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBox_Presets.FormattingEnabled = true;
            this.ListBox_Presets.Location = new System.Drawing.Point(3, 16);
            this.ListBox_Presets.Name = "ListBox_Presets";
            this.ListBox_Presets.ScrollAlwaysVisible = true;
            this.ListBox_Presets.Size = new System.Drawing.Size(218, 416);
            this.ListBox_Presets.TabIndex = 0;
            this.ListBox_Presets.SelectedIndexChanged += new System.EventHandler(this.ListBox_Presets_SelectedIndexChanged);
            // 
            // Label_Frequency
            // 
            this.Label_Frequency.AutoSize = true;
            this.Label_Frequency.Location = new System.Drawing.Point(6, 22);
            this.Label_Frequency.Name = "Label_Frequency";
            this.Label_Frequency.Size = new System.Drawing.Size(60, 13);
            this.Label_Frequency.TabIndex = 1;
            this.Label_Frequency.Text = "Frequency:";
            // 
            // Textbox_Frequency
            // 
            this.Textbox_Frequency.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Frequency.Location = new System.Drawing.Point(72, 19);
            this.Textbox_Frequency.Name = "Textbox_Frequency";
            this.Textbox_Frequency.ReadOnly = true;
            this.Textbox_Frequency.Size = new System.Drawing.Size(100, 20);
            this.Textbox_Frequency.TabIndex = 2;
            // 
            // Label_Bits
            // 
            this.Label_Bits.AutoSize = true;
            this.Label_Bits.Location = new System.Drawing.Point(178, 22);
            this.Label_Bits.Name = "Label_Bits";
            this.Label_Bits.Size = new System.Drawing.Size(27, 13);
            this.Label_Bits.TabIndex = 3;
            this.Label_Bits.Text = "Bits:";
            // 
            // Textbox_Bits
            // 
            this.Textbox_Bits.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Bits.Location = new System.Drawing.Point(211, 19);
            this.Textbox_Bits.Name = "Textbox_Bits";
            this.Textbox_Bits.ReadOnly = true;
            this.Textbox_Bits.Size = new System.Drawing.Size(88, 20);
            this.Textbox_Bits.TabIndex = 4;
            // 
            // Label_Channels
            // 
            this.Label_Channels.AutoSize = true;
            this.Label_Channels.Location = new System.Drawing.Point(12, 48);
            this.Label_Channels.Name = "Label_Channels";
            this.Label_Channels.Size = new System.Drawing.Size(54, 13);
            this.Label_Channels.TabIndex = 5;
            this.Label_Channels.Text = "Channels:";
            // 
            // Textbox_Channels
            // 
            this.Textbox_Channels.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Channels.Location = new System.Drawing.Point(72, 45);
            this.Textbox_Channels.Name = "Textbox_Channels";
            this.Textbox_Channels.ReadOnly = true;
            this.Textbox_Channels.Size = new System.Drawing.Size(100, 20);
            this.Textbox_Channels.TabIndex = 6;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(409, 424);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 3;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(490, 424);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 4;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Groupbox_Values
            // 
            this.Groupbox_Values.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Groupbox_Values.Controls.Add(this.Label_Frequency);
            this.Groupbox_Values.Controls.Add(this.Textbox_Frequency);
            this.Groupbox_Values.Controls.Add(this.Label_Bits);
            this.Groupbox_Values.Controls.Add(this.Textbox_Channels);
            this.Groupbox_Values.Controls.Add(this.Label_Channels);
            this.Groupbox_Values.Controls.Add(this.Textbox_Bits);
            this.Groupbox_Values.Location = new System.Drawing.Point(242, 12);
            this.Groupbox_Values.Name = "Groupbox_Values";
            this.Groupbox_Values.Size = new System.Drawing.Size(323, 94);
            this.Groupbox_Values.TabIndex = 1;
            this.Groupbox_Values.TabStop = false;
            this.Groupbox_Values.Text = "Values:";
            // 
            // GroupBox_Desc
            // 
            this.GroupBox_Desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Desc.Controls.Add(this.Textbox_Desc);
            this.GroupBox_Desc.Location = new System.Drawing.Point(242, 112);
            this.GroupBox_Desc.Name = "GroupBox_Desc";
            this.GroupBox_Desc.Size = new System.Drawing.Size(323, 306);
            this.GroupBox_Desc.TabIndex = 2;
            this.GroupBox_Desc.TabStop = false;
            this.GroupBox_Desc.Text = "Description:";
            // 
            // Textbox_Desc
            // 
            this.Textbox_Desc.BackColor = System.Drawing.SystemColors.Window;
            this.Textbox_Desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Textbox_Desc.Location = new System.Drawing.Point(3, 16);
            this.Textbox_Desc.Multiline = true;
            this.Textbox_Desc.Name = "Textbox_Desc";
            this.Textbox_Desc.ReadOnly = true;
            this.Textbox_Desc.Size = new System.Drawing.Size(317, 287);
            this.Textbox_Desc.TabIndex = 0;
            // 
            // Frm_AudioConverter_Presets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(577, 459);
            this.Controls.Add(this.GroupBox_Desc);
            this.Controls.Add(this.Groupbox_Values);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Groupbox_AvailablePresets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AudioConverter_Presets";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Presets";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_AudioConverter_Presets_FormClosing);
            this.Shown += new System.EventHandler(this.Frm_AudioConverter_Presets_Shown);
            this.Groupbox_AvailablePresets.ResumeLayout(false);
            this.Groupbox_Values.ResumeLayout(false);
            this.Groupbox_Values.PerformLayout();
            this.GroupBox_Desc.ResumeLayout(false);
            this.GroupBox_Desc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Groupbox_AvailablePresets;
        private System.Windows.Forms.ListBox ListBox_Presets;
        private System.Windows.Forms.Label Label_Frequency;
        private System.Windows.Forms.TextBox Textbox_Frequency;
        private System.Windows.Forms.Label Label_Bits;
        private System.Windows.Forms.TextBox Textbox_Bits;
        private System.Windows.Forms.Label Label_Channels;
        private System.Windows.Forms.TextBox Textbox_Channels;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox Groupbox_Values;
        private System.Windows.Forms.GroupBox GroupBox_Desc;
        private System.Windows.Forms.TextBox Textbox_Desc;
    }
}