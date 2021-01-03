
namespace EuroSound_Application.StreamSounds
{
    partial class Frm_StreamSounds_Properties
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
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.GroupBox_Properties = new System.Windows.Forms.GroupBox();
            this.Numeric_BaseVolume = new System.Windows.Forms.NumericUpDown();
            this.Button_MarkersEditor = new System.Windows.Forms.Button();
            this.Label_BaseVolume = new System.Windows.Forms.Label();
            this.CheckBox_OutputThisSound = new System.Windows.Forms.CheckBox();
            this.GroupBox_IMA_Data = new System.Windows.Forms.GroupBox();
            this.Textbox_MD5_Hash = new System.Windows.Forms.TextBox();
            this.Label_MD5_Hash = new System.Windows.Forms.Label();
            this.Label_Data = new System.Windows.Forms.Label();
            this.Button_SearchIMA = new System.Windows.Forms.Button();
            this.Textbox_IMA_ADPCM = new System.Windows.Forms.TextBox();
            this.GroupBox_Properties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).BeginInit();
            this.GroupBox_IMA_Data.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(168, 218);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 4;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(249, 218);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 5;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // GroupBox_Properties
            // 
            this.GroupBox_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Properties.Controls.Add(this.Numeric_BaseVolume);
            this.GroupBox_Properties.Controls.Add(this.Button_MarkersEditor);
            this.GroupBox_Properties.Controls.Add(this.Label_BaseVolume);
            this.GroupBox_Properties.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_Properties.Name = "GroupBox_Properties";
            this.GroupBox_Properties.Size = new System.Drawing.Size(312, 79);
            this.GroupBox_Properties.TabIndex = 0;
            this.GroupBox_Properties.TabStop = false;
            this.GroupBox_Properties.Text = "Sound Properties";
            // 
            // Numeric_BaseVolume
            // 
            this.Numeric_BaseVolume.Location = new System.Drawing.Point(110, 19);
            this.Numeric_BaseVolume.Name = "Numeric_BaseVolume";
            this.Numeric_BaseVolume.Size = new System.Drawing.Size(120, 20);
            this.Numeric_BaseVolume.TabIndex = 3;
            // 
            // Button_MarkersEditor
            // 
            this.Button_MarkersEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MarkersEditor.Location = new System.Drawing.Point(194, 50);
            this.Button_MarkersEditor.Name = "Button_MarkersEditor";
            this.Button_MarkersEditor.Size = new System.Drawing.Size(112, 23);
            this.Button_MarkersEditor.TabIndex = 1;
            this.Button_MarkersEditor.Text = "Open Markers Editor";
            this.Button_MarkersEditor.UseVisualStyleBackColor = true;
            this.Button_MarkersEditor.Click += new System.EventHandler(this.Button_MarkersEditor_Click);
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(32, 21);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 2;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // CheckBox_OutputThisSound
            // 
            this.CheckBox_OutputThisSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_OutputThisSound.AutoSize = true;
            this.CheckBox_OutputThisSound.Checked = true;
            this.CheckBox_OutputThisSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_OutputThisSound.Location = new System.Drawing.Point(12, 224);
            this.CheckBox_OutputThisSound.Name = "CheckBox_OutputThisSound";
            this.CheckBox_OutputThisSound.Size = new System.Drawing.Size(109, 17);
            this.CheckBox_OutputThisSound.TabIndex = 3;
            this.CheckBox_OutputThisSound.Text = "Output this sound";
            this.CheckBox_OutputThisSound.UseVisualStyleBackColor = true;
            // 
            // GroupBox_IMA_Data
            // 
            this.GroupBox_IMA_Data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_IMA_Data.Controls.Add(this.Textbox_MD5_Hash);
            this.GroupBox_IMA_Data.Controls.Add(this.Label_MD5_Hash);
            this.GroupBox_IMA_Data.Controls.Add(this.Label_Data);
            this.GroupBox_IMA_Data.Controls.Add(this.Button_SearchIMA);
            this.GroupBox_IMA_Data.Controls.Add(this.Textbox_IMA_ADPCM);
            this.GroupBox_IMA_Data.Location = new System.Drawing.Point(12, 97);
            this.GroupBox_IMA_Data.Name = "GroupBox_IMA_Data";
            this.GroupBox_IMA_Data.Size = new System.Drawing.Size(312, 100);
            this.GroupBox_IMA_Data.TabIndex = 6;
            this.GroupBox_IMA_Data.TabStop = false;
            this.GroupBox_IMA_Data.Text = "IMA ADPCM";
            // 
            // Textbox_MD5_Hash
            // 
            this.Textbox_MD5_Hash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_MD5_Hash.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_MD5_Hash.Location = new System.Drawing.Point(44, 45);
            this.Textbox_MD5_Hash.Name = "Textbox_MD5_Hash";
            this.Textbox_MD5_Hash.ReadOnly = true;
            this.Textbox_MD5_Hash.Size = new System.Drawing.Size(262, 20);
            this.Textbox_MD5_Hash.TabIndex = 11;
            // 
            // Label_MD5_Hash
            // 
            this.Label_MD5_Hash.AutoSize = true;
            this.Label_MD5_Hash.Location = new System.Drawing.Point(6, 48);
            this.Label_MD5_Hash.Name = "Label_MD5_Hash";
            this.Label_MD5_Hash.Size = new System.Drawing.Size(35, 13);
            this.Label_MD5_Hash.TabIndex = 10;
            this.Label_MD5_Hash.Text = "Hash:";
            // 
            // Label_Data
            // 
            this.Label_Data.AutoSize = true;
            this.Label_Data.Location = new System.Drawing.Point(6, 23);
            this.Label_Data.Name = "Label_Data";
            this.Label_Data.Size = new System.Drawing.Size(32, 13);
            this.Label_Data.TabIndex = 9;
            this.Label_Data.Text = "Path:";
            // 
            // Button_SearchIMA
            // 
            this.Button_SearchIMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SearchIMA.Location = new System.Drawing.Point(282, 19);
            this.Button_SearchIMA.Name = "Button_SearchIMA";
            this.Button_SearchIMA.Size = new System.Drawing.Size(24, 20);
            this.Button_SearchIMA.TabIndex = 8;
            this.Button_SearchIMA.Text = "...";
            this.Button_SearchIMA.UseVisualStyleBackColor = true;
            this.Button_SearchIMA.Click += new System.EventHandler(this.Button_SearchIMA_Click);
            // 
            // Textbox_IMA_ADPCM
            // 
            this.Textbox_IMA_ADPCM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_IMA_ADPCM.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_IMA_ADPCM.Location = new System.Drawing.Point(44, 19);
            this.Textbox_IMA_ADPCM.Name = "Textbox_IMA_ADPCM";
            this.Textbox_IMA_ADPCM.ReadOnly = true;
            this.Textbox_IMA_ADPCM.Size = new System.Drawing.Size(233, 20);
            this.Textbox_IMA_ADPCM.TabIndex = 7;
            // 
            // Frm_StreamSounds_Properties
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(336, 253);
            this.Controls.Add(this.GroupBox_IMA_Data);
            this.Controls.Add(this.CheckBox_OutputThisSound);
            this.Controls.Add(this.GroupBox_Properties);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StreamSounds_Properties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_StreamSounds_MarkersEditor";
            this.Load += new System.EventHandler(this.Frm_StreamSounds_Properties_Load);
            this.GroupBox_Properties.ResumeLayout(false);
            this.GroupBox_Properties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).EndInit();
            this.GroupBox_IMA_Data.ResumeLayout(false);
            this.GroupBox_IMA_Data.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox GroupBox_Properties;
        private System.Windows.Forms.NumericUpDown Numeric_BaseVolume;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.Button Button_MarkersEditor;
        private System.Windows.Forms.CheckBox CheckBox_OutputThisSound;
        private System.Windows.Forms.GroupBox GroupBox_IMA_Data;
        private System.Windows.Forms.Label Label_Data;
        private System.Windows.Forms.Button Button_SearchIMA;
        private System.Windows.Forms.TextBox Textbox_IMA_ADPCM;
        private System.Windows.Forms.TextBox Textbox_MD5_Hash;
        private System.Windows.Forms.Label Label_MD5_Hash;
    }
}