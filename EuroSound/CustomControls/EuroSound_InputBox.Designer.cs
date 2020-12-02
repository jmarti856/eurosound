namespace EuroSound
{
    partial class EuroSound_InputBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EuroSound_InputBox));
            this.Label_Text = new System.Windows.Forms.Label();
            this.TextBox_InputText = new System.Windows.Forms.TextBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_Text
            // 
            this.Label_Text.AutoSize = true;
            this.Label_Text.Location = new System.Drawing.Point(12, 9);
            this.Label_Text.MaximumSize = new System.Drawing.Size(461, 39);
            this.Label_Text.Name = "Label_Text";
            this.Label_Text.Size = new System.Drawing.Size(461, 39);
            this.Label_Text.TabIndex = 0;
            this.Label_Text.Text = resources.GetString("Label_Text.Text");
            // 
            // TextBox_InputText
            // 
            this.TextBox_InputText.Location = new System.Drawing.Point(15, 51);
            this.TextBox_InputText.Name = "TextBox_InputText";
            this.TextBox_InputText.Size = new System.Drawing.Size(458, 20);
            this.TextBox_InputText.TabIndex = 1;
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(317, 77);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(75, 23);
            this.Button_Ok.TabIndex = 2;
            this.Button_Ok.Text = "OK";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(398, 77);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 3;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // EuroSound_InputBox
            // 
            this.AcceptButton = this.Button_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(485, 110);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.TextBox_InputText);
            this.Controls.Add(this.Label_Text);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound_InputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Text;
        private System.Windows.Forms.TextBox TextBox_InputText;
        private System.Windows.Forms.Button Button_Ok;
        private System.Windows.Forms.Button Button_Cancel;
    }
}