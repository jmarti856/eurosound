
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_TreeViewPrefs
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
            this.Label_PropsExplanation = new System.Windows.Forms.Label();
            this.Label_Indent = new System.Windows.Forms.Label();
            this.Numeric_TreeViewIndent = new System.Windows.Forms.NumericUpDown();
            this.CheckBox_ShowLines = new System.Windows.Forms.CheckBox();
            this.Checkbox_ShowRootLines = new System.Windows.Forms.CheckBox();
            this.FontDialogTreeView = new System.Windows.Forms.FontDialog();
            this.Label_Font = new System.Windows.Forms.Label();
            this.Textbox_SelectedFont = new System.Windows.Forms.TextBox();
            this.GroupBox_Lines = new System.Windows.Forms.GroupBox();
            this.Label_ItemHeight = new System.Windows.Forms.Label();
            this.Numeric_ItemHeight = new System.Windows.Forms.NumericUpDown();
            this.Panel_Title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_TreeViewIndent)).BeginInit();
            this.GroupBox_Lines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_ItemHeight)).BeginInit();
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
            this.Panel_Title.TabIndex = 1;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(170, 4);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(150, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Tree View Configuration";
            // 
            // Label_PropsExplanation
            // 
            this.Label_PropsExplanation.AutoSize = true;
            this.Label_PropsExplanation.Location = new System.Drawing.Point(9, 45);
            this.Label_PropsExplanation.Name = "Label_PropsExplanation";
            this.Label_PropsExplanation.Size = new System.Drawing.Size(345, 13);
            this.Label_PropsExplanation.TabIndex = 2;
            this.Label_PropsExplanation.Text = "The following properties will take effect when the program gets restarted";
            // 
            // Label_Indent
            // 
            this.Label_Indent.AutoSize = true;
            this.Label_Indent.Location = new System.Drawing.Point(34, 108);
            this.Label_Indent.Name = "Label_Indent";
            this.Label_Indent.Size = new System.Drawing.Size(40, 13);
            this.Label_Indent.TabIndex = 5;
            this.Label_Indent.Text = "Indent:";
            // 
            // Numeric_TreeViewIndent
            // 
            this.Numeric_TreeViewIndent.Location = new System.Drawing.Point(80, 106);
            this.Numeric_TreeViewIndent.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.Numeric_TreeViewIndent.Name = "Numeric_TreeViewIndent";
            this.Numeric_TreeViewIndent.Size = new System.Drawing.Size(95, 20);
            this.Numeric_TreeViewIndent.TabIndex = 6;
            // 
            // CheckBox_ShowLines
            // 
            this.CheckBox_ShowLines.AutoSize = true;
            this.CheckBox_ShowLines.Location = new System.Drawing.Point(6, 19);
            this.CheckBox_ShowLines.Name = "CheckBox_ShowLines";
            this.CheckBox_ShowLines.Size = new System.Drawing.Size(81, 17);
            this.CheckBox_ShowLines.TabIndex = 0;
            this.CheckBox_ShowLines.Text = "Show Lines";
            this.CheckBox_ShowLines.UseVisualStyleBackColor = true;
            // 
            // Checkbox_ShowRootLines
            // 
            this.Checkbox_ShowRootLines.AutoSize = true;
            this.Checkbox_ShowRootLines.Location = new System.Drawing.Point(6, 42);
            this.Checkbox_ShowRootLines.Name = "Checkbox_ShowRootLines";
            this.Checkbox_ShowRootLines.Size = new System.Drawing.Size(107, 17);
            this.Checkbox_ShowRootLines.TabIndex = 1;
            this.Checkbox_ShowRootLines.Text = "Show Root Lines";
            this.Checkbox_ShowRootLines.UseVisualStyleBackColor = true;
            // 
            // Label_Font
            // 
            this.Label_Font.AutoSize = true;
            this.Label_Font.Location = new System.Drawing.Point(43, 135);
            this.Label_Font.Name = "Label_Font";
            this.Label_Font.Size = new System.Drawing.Size(31, 13);
            this.Label_Font.TabIndex = 7;
            this.Label_Font.Text = "Font:";
            // 
            // Textbox_SelectedFont
            // 
            this.Textbox_SelectedFont.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Textbox_SelectedFont.Location = new System.Drawing.Point(80, 132);
            this.Textbox_SelectedFont.Name = "Textbox_SelectedFont";
            this.Textbox_SelectedFont.ReadOnly = true;
            this.Textbox_SelectedFont.Size = new System.Drawing.Size(236, 20);
            this.Textbox_SelectedFont.TabIndex = 8;
            this.Textbox_SelectedFont.Click += new System.EventHandler(this.Textbox_SelectedFont_Click);
            // 
            // GroupBox_Lines
            // 
            this.GroupBox_Lines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Lines.Controls.Add(this.CheckBox_ShowLines);
            this.GroupBox_Lines.Controls.Add(this.Checkbox_ShowRootLines);
            this.GroupBox_Lines.Location = new System.Drawing.Point(12, 158);
            this.GroupBox_Lines.Name = "GroupBox_Lines";
            this.GroupBox_Lines.Size = new System.Drawing.Size(467, 115);
            this.GroupBox_Lines.TabIndex = 9;
            this.GroupBox_Lines.TabStop = false;
            this.GroupBox_Lines.Text = "Lines:";
            // 
            // Label_ItemHeight
            // 
            this.Label_ItemHeight.AutoSize = true;
            this.Label_ItemHeight.Location = new System.Drawing.Point(12, 82);
            this.Label_ItemHeight.Name = "Label_ItemHeight";
            this.Label_ItemHeight.Size = new System.Drawing.Size(62, 13);
            this.Label_ItemHeight.TabIndex = 3;
            this.Label_ItemHeight.Text = "Item height:";
            // 
            // Numeric_ItemHeight
            // 
            this.Numeric_ItemHeight.Location = new System.Drawing.Point(80, 80);
            this.Numeric_ItemHeight.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.Numeric_ItemHeight.Name = "Numeric_ItemHeight";
            this.Numeric_ItemHeight.Size = new System.Drawing.Size(95, 20);
            this.Numeric_ItemHeight.TabIndex = 4;
            // 
            // Frm_TreeViewPrefs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 339);
            this.Controls.Add(this.Numeric_ItemHeight);
            this.Controls.Add(this.Label_ItemHeight);
            this.Controls.Add(this.GroupBox_Lines);
            this.Controls.Add(this.Textbox_SelectedFont);
            this.Controls.Add(this.Label_Font);
            this.Controls.Add(this.Numeric_TreeViewIndent);
            this.Controls.Add(this.Label_Indent);
            this.Controls.Add(this.Label_PropsExplanation);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_TreeViewPrefs";
            this.Text = "Frm_TreeViewPrefs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TreeViewPrefs_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TreeViewPrefs_Load);
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_TreeViewIndent)).EndInit();
            this.GroupBox_Lines.ResumeLayout(false);
            this.GroupBox_Lines.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Numeric_ItemHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_PropsExplanation;
        private System.Windows.Forms.Label Label_Indent;
        private System.Windows.Forms.NumericUpDown Numeric_TreeViewIndent;
        private System.Windows.Forms.CheckBox CheckBox_ShowLines;
        private System.Windows.Forms.CheckBox Checkbox_ShowRootLines;
        private System.Windows.Forms.FontDialog FontDialogTreeView;
        private System.Windows.Forms.Label Label_Font;
        private System.Windows.Forms.TextBox Textbox_SelectedFont;
        private System.Windows.Forms.GroupBox GroupBox_Lines;
        private System.Windows.Forms.Label Label_ItemHeight;
        private System.Windows.Forms.NumericUpDown Numeric_ItemHeight;
    }
}