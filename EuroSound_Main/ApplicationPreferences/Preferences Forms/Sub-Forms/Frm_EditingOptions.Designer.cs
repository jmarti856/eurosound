
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_EditingOptions
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
            this.GroupBox_EditingOptions = new System.Windows.Forms.GroupBox();
            this.Checkbox_SortNodes = new System.Windows.Forms.CheckBox();
            this.Checkbox_UseExtendedColorPicker = new System.Windows.Forms.CheckBox();
            this.Panel_Title = new System.Windows.Forms.Panel();
            this.Label_Title = new System.Windows.Forms.Label();
            this.GroupBox_EditingOptions.SuspendLayout();
            this.Panel_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_EditingOptions
            // 
            this.GroupBox_EditingOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_EditingOptions.Controls.Add(this.Checkbox_SortNodes);
            this.GroupBox_EditingOptions.Controls.Add(this.Checkbox_UseExtendedColorPicker);
            this.GroupBox_EditingOptions.Location = new System.Drawing.Point(12, 130);
            this.GroupBox_EditingOptions.Name = "GroupBox_EditingOptions";
            this.GroupBox_EditingOptions.Size = new System.Drawing.Size(489, 95);
            this.GroupBox_EditingOptions.TabIndex = 7;
            this.GroupBox_EditingOptions.TabStop = false;
            this.GroupBox_EditingOptions.Text = "Options:";
            // 
            // Checkbox_SortNodes
            // 
            this.Checkbox_SortNodes.AutoSize = true;
            this.Checkbox_SortNodes.Location = new System.Drawing.Point(17, 42);
            this.Checkbox_SortNodes.Name = "Checkbox_SortNodes";
            this.Checkbox_SortNodes.Size = new System.Drawing.Size(273, 17);
            this.Checkbox_SortNodes.TabIndex = 1;
            this.Checkbox_SortNodes.Text = "Automatically sort tree view when adding new nodes";
            this.Checkbox_SortNodes.UseVisualStyleBackColor = true;
            // 
            // Checkbox_UseExtendedColorPicker
            // 
            this.Checkbox_UseExtendedColorPicker.AutoSize = true;
            this.Checkbox_UseExtendedColorPicker.Location = new System.Drawing.Point(17, 19);
            this.Checkbox_UseExtendedColorPicker.Name = "Checkbox_UseExtendedColorPicker";
            this.Checkbox_UseExtendedColorPicker.Size = new System.Drawing.Size(150, 17);
            this.Checkbox_UseExtendedColorPicker.TabIndex = 0;
            this.Checkbox_UseExtendedColorPicker.Text = "Use extended color-picker";
            this.Checkbox_UseExtendedColorPicker.UseVisualStyleBackColor = true;
            // 
            // Panel_Title
            // 
            this.Panel_Title.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Panel_Title.Controls.Add(this.Label_Title);
            this.Panel_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Title.Location = new System.Drawing.Point(0, 0);
            this.Panel_Title.Name = "Panel_Title";
            this.Panel_Title.Size = new System.Drawing.Size(513, 23);
            this.Panel_Title.TabIndex = 6;
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(232, 5);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(49, 16);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Editing";
            // 
            // Frm_EditingOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 395);
            this.Controls.Add(this.GroupBox_EditingOptions);
            this.Controls.Add(this.Panel_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_EditingOptions";
            this.Text = "Frm_EditingOptions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_EditingOptions_FormClosing);
            this.Load += new System.EventHandler(this.Frm_EditingOptions_Load);
            this.GroupBox_EditingOptions.ResumeLayout(false);
            this.GroupBox_EditingOptions.PerformLayout();
            this.Panel_Title.ResumeLayout(false);
            this.Panel_Title.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_EditingOptions;
        private System.Windows.Forms.CheckBox Checkbox_UseExtendedColorPicker;
        private System.Windows.Forms.Panel Panel_Title;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.CheckBox Checkbox_SortNodes;
    }
}