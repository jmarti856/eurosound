
namespace EuroSound_Application
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
            this.Combobox_Hashcode = new System.Windows.Forms.ComboBox();
            this.Label_Hashcode = new System.Windows.Forms.Label();
            this.Numeric_BaseVolume = new System.Windows.Forms.NumericUpDown();
            this.Label_BaseVolume = new System.Windows.Forms.Label();
            this.Button_MarkersEditor = new System.Windows.Forms.Button();
            this.CheckBox_OutputThisSound = new System.Windows.Forms.CheckBox();
            this.Button_AudioData = new System.Windows.Forms.Button();
            this.GroupBox_Properties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_BaseVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(172, 162);
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
            this.Button_Cancel.Location = new System.Drawing.Point(253, 162);
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
            this.GroupBox_Properties.Controls.Add(this.Combobox_Hashcode);
            this.GroupBox_Properties.Controls.Add(this.Label_Hashcode);
            this.GroupBox_Properties.Controls.Add(this.Numeric_BaseVolume);
            this.GroupBox_Properties.Controls.Add(this.Label_BaseVolume);
            this.GroupBox_Properties.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_Properties.Name = "GroupBox_Properties";
            this.GroupBox_Properties.Size = new System.Drawing.Size(316, 87);
            this.GroupBox_Properties.TabIndex = 0;
            this.GroupBox_Properties.TabStop = false;
            this.GroupBox_Properties.Text = "Sound Properties";
            // 
            // Combobox_Hashcode
            // 
            this.Combobox_Hashcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_Hashcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_Hashcode.FormattingEnabled = true;
            this.Combobox_Hashcode.Location = new System.Drawing.Point(84, 19);
            this.Combobox_Hashcode.Name = "Combobox_Hashcode";
            this.Combobox_Hashcode.Size = new System.Drawing.Size(226, 21);
            this.Combobox_Hashcode.TabIndex = 1;
            // 
            // Label_Hashcode
            // 
            this.Label_Hashcode.AutoSize = true;
            this.Label_Hashcode.Location = new System.Drawing.Point(19, 22);
            this.Label_Hashcode.Name = "Label_Hashcode";
            this.Label_Hashcode.Size = new System.Drawing.Size(59, 13);
            this.Label_Hashcode.TabIndex = 0;
            this.Label_Hashcode.Text = "Hashcode:";
            // 
            // Numeric_BaseVolume
            // 
            this.Numeric_BaseVolume.Location = new System.Drawing.Point(84, 46);
            this.Numeric_BaseVolume.Name = "Numeric_BaseVolume";
            this.Numeric_BaseVolume.Size = new System.Drawing.Size(104, 20);
            this.Numeric_BaseVolume.TabIndex = 3;
            // 
            // Label_BaseVolume
            // 
            this.Label_BaseVolume.AutoSize = true;
            this.Label_BaseVolume.Location = new System.Drawing.Point(6, 48);
            this.Label_BaseVolume.Name = "Label_BaseVolume";
            this.Label_BaseVolume.Size = new System.Drawing.Size(72, 13);
            this.Label_BaseVolume.TabIndex = 2;
            this.Label_BaseVolume.Text = "Base Volume:";
            // 
            // Button_MarkersEditor
            // 
            this.Button_MarkersEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_MarkersEditor.Location = new System.Drawing.Point(12, 105);
            this.Button_MarkersEditor.Name = "Button_MarkersEditor";
            this.Button_MarkersEditor.Size = new System.Drawing.Size(146, 23);
            this.Button_MarkersEditor.TabIndex = 1;
            this.Button_MarkersEditor.Text = "Open Markers Editor";
            this.Button_MarkersEditor.UseVisualStyleBackColor = true;
            // 
            // CheckBox_OutputThisSound
            // 
            this.CheckBox_OutputThisSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBox_OutputThisSound.AutoSize = true;
            this.CheckBox_OutputThisSound.Checked = true;
            this.CheckBox_OutputThisSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_OutputThisSound.Location = new System.Drawing.Point(12, 168);
            this.CheckBox_OutputThisSound.Name = "CheckBox_OutputThisSound";
            this.CheckBox_OutputThisSound.Size = new System.Drawing.Size(109, 17);
            this.CheckBox_OutputThisSound.TabIndex = 3;
            this.CheckBox_OutputThisSound.Text = "Output this sound";
            this.CheckBox_OutputThisSound.UseVisualStyleBackColor = true;
            // 
            // Button_AudioData
            // 
            this.Button_AudioData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_AudioData.Location = new System.Drawing.Point(164, 105);
            this.Button_AudioData.Name = "Button_AudioData";
            this.Button_AudioData.Size = new System.Drawing.Size(164, 23);
            this.Button_AudioData.TabIndex = 2;
            this.Button_AudioData.Text = "Audio Data";
            this.Button_AudioData.UseVisualStyleBackColor = true;
            // 
            // Frm_StreamSounds_Properties
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(340, 197);
            this.Controls.Add(this.Button_AudioData);
            this.Controls.Add(this.Button_MarkersEditor);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.GroupBox GroupBox_Properties;
        private System.Windows.Forms.NumericUpDown Numeric_BaseVolume;
        private System.Windows.Forms.Label Label_BaseVolume;
        private System.Windows.Forms.ComboBox Combobox_Hashcode;
        private System.Windows.Forms.Label Label_Hashcode;
        private System.Windows.Forms.Button Button_MarkersEditor;
        private System.Windows.Forms.CheckBox CheckBox_OutputThisSound;
        private System.Windows.Forms.Button Button_AudioData;
    }
}