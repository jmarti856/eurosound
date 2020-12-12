
namespace CustomControls
{
    partial class EuroSound_NewFileProject
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
            this.GroupBox_Properties = new System.Windows.Forms.GroupBox();
            this.Textbox_ProjectName = new System.Windows.Forms.TextBox();
            this.Label_ProjectName = new System.Windows.Forms.Label();
            this.Label_Combobox = new System.Windows.Forms.Label();
            this.Combobox_TypeOfData = new System.Windows.Forms.ComboBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.GroupBox_Properties.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_Properties
            // 
            this.GroupBox_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Properties.Controls.Add(this.Textbox_ProjectName);
            this.GroupBox_Properties.Controls.Add(this.Label_ProjectName);
            this.GroupBox_Properties.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_Properties.Name = "GroupBox_Properties";
            this.GroupBox_Properties.Size = new System.Drawing.Size(316, 71);
            this.GroupBox_Properties.TabIndex = 0;
            this.GroupBox_Properties.TabStop = false;
            this.GroupBox_Properties.Text = "Project Properties";
            // 
            // Textbox_ProjectName
            // 
            this.Textbox_ProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_ProjectName.Location = new System.Drawing.Point(50, 29);
            this.Textbox_ProjectName.Name = "Textbox_ProjectName";
            this.Textbox_ProjectName.Size = new System.Drawing.Size(260, 20);
            this.Textbox_ProjectName.TabIndex = 1;
            this.Textbox_ProjectName.Text = "Unnamed";
            // 
            // Label_ProjectName
            // 
            this.Label_ProjectName.AutoSize = true;
            this.Label_ProjectName.Location = new System.Drawing.Point(6, 29);
            this.Label_ProjectName.Name = "Label_ProjectName";
            this.Label_ProjectName.Size = new System.Drawing.Size(38, 13);
            this.Label_ProjectName.TabIndex = 0;
            this.Label_ProjectName.Text = "Name:";
            // 
            // Label_Combobox
            // 
            this.Label_Combobox.AutoSize = true;
            this.Label_Combobox.Location = new System.Drawing.Point(9, 92);
            this.Label_Combobox.Name = "Label_Combobox";
            this.Label_Combobox.Size = new System.Drawing.Size(106, 13);
            this.Label_Combobox.TabIndex = 1;
            this.Label_Combobox.Text = "Type of Stored Data:";
            // 
            // Combobox_TypeOfData
            // 
            this.Combobox_TypeOfData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_TypeOfData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_TypeOfData.FormattingEnabled = true;
            this.Combobox_TypeOfData.Items.AddRange(new object[] {
            "Soundbanks",
            "Streamed sounds",
            "Music tracks"});
            this.Combobox_TypeOfData.Location = new System.Drawing.Point(121, 89);
            this.Combobox_TypeOfData.Name = "Combobox_TypeOfData";
            this.Combobox_TypeOfData.Size = new System.Drawing.Size(207, 21);
            this.Combobox_TypeOfData.TabIndex = 2;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(172, 127);
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
            this.Button_Cancel.Location = new System.Drawing.Point(253, 127);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 4;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // EuroSound_NewFileProject
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(340, 162);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Combobox_TypeOfData);
            this.Controls.Add(this.Label_Combobox);
            this.Controls.Add(this.GroupBox_Properties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_NewFileProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound_NewFileProject";
            this.Load += new System.EventHandler(this.EuroSound_NewFileProject_Load);
            this.GroupBox_Properties.ResumeLayout(false);
            this.GroupBox_Properties.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_Properties;
        private System.Windows.Forms.TextBox Textbox_ProjectName;
        private System.Windows.Forms.Label Label_ProjectName;
        private System.Windows.Forms.Label Label_Combobox;
        private System.Windows.Forms.ComboBox Combobox_TypeOfData;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
    }
}