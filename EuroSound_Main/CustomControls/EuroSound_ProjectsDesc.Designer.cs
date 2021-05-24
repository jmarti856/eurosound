
namespace EuroSound_Application.CustomControls.ProjectSettings
{
    partial class EuroSound_ProjectsDesc
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
            this.RichTextbox_Desc = new RicherTextBox.RicherTextBox();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(735, 496);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 1;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(816, 496);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // RichTextbox_Desc
            // 
            this.RichTextbox_Desc.AlignCenterVisible = true;
            this.RichTextbox_Desc.AlignLeftVisible = true;
            this.RichTextbox_Desc.AlignRightVisible = true;
            this.RichTextbox_Desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextbox_Desc.BoldVisible = true;
            this.RichTextbox_Desc.BulletsVisible = true;
            this.RichTextbox_Desc.ChooseFontVisible = true;
            this.RichTextbox_Desc.FindReplaceVisible = true;
            this.RichTextbox_Desc.FontColorVisible = true;
            this.RichTextbox_Desc.FontFamilyVisible = true;
            this.RichTextbox_Desc.FontSizeVisible = true;
            this.RichTextbox_Desc.GroupAlignmentVisible = true;
            this.RichTextbox_Desc.GroupBoldUnderlineItalicVisible = true;
            this.RichTextbox_Desc.GroupFontColorVisible = true;
            this.RichTextbox_Desc.GroupFontNameAndSizeVisible = true;
            this.RichTextbox_Desc.GroupIndentationAndBulletsVisible = true;
            this.RichTextbox_Desc.GroupInsertVisible = true;
            this.RichTextbox_Desc.GroupSaveAndLoadVisible = true;
            this.RichTextbox_Desc.GroupZoomVisible = true;
            this.RichTextbox_Desc.INDENT = 10;
            this.RichTextbox_Desc.IndentVisible = true;
            this.RichTextbox_Desc.InsertPictureVisible = true;
            this.RichTextbox_Desc.ItalicVisible = true;
            this.RichTextbox_Desc.LoadVisible = true;
            this.RichTextbox_Desc.Location = new System.Drawing.Point(12, 12);
            this.RichTextbox_Desc.Name = "RichTextbox_Desc";
            this.RichTextbox_Desc.OutdentVisible = true;
            this.RichTextbox_Desc.Rtf = "{\\rtf1\\ansi\\deff0\\nouicompat{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft Sans Serif;}" +
    "}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\lang3082 ric" +
    "herTextBox1\\par\r\n}\r\n";
            this.RichTextbox_Desc.SaveVisible = true;
            this.RichTextbox_Desc.SeparatorAlignVisible = true;
            this.RichTextbox_Desc.SeparatorBoldUnderlineItalicVisible = true;
            this.RichTextbox_Desc.SeparatorFontColorVisible = true;
            this.RichTextbox_Desc.SeparatorFontVisible = true;
            this.RichTextbox_Desc.SeparatorIndentAndBulletsVisible = true;
            this.RichTextbox_Desc.SeparatorInsertVisible = true;
            this.RichTextbox_Desc.SeparatorSaveLoadVisible = true;
            this.RichTextbox_Desc.Size = new System.Drawing.Size(879, 478);
            this.RichTextbox_Desc.TabIndex = 3;
            this.RichTextbox_Desc.ToolStripVisible = true;
            this.RichTextbox_Desc.UnderlineVisible = true;
            this.RichTextbox_Desc.WordWrapVisible = true;
            this.RichTextbox_Desc.ZoomFactorTextVisible = true;
            this.RichTextbox_Desc.ZoomInVisible = true;
            this.RichTextbox_Desc.ZoomOutVisible = true;
            // 
            // EuroSound_ProjectsDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(903, 531);
            this.Controls.Add(this.RichTextbox_Desc);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_ProjectsDesc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Description";
            this.Load += new System.EventHandler(this.EuroSound_ProjectsDesc_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private RicherTextBox.RicherTextBox RichTextbox_Desc;
    }
}