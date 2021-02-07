
namespace EuroSound_Application.SoundBanksEditor
{
    partial class Frm_Sounbanks_DebugOutput
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Hashcodes Table");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("SFX Elements");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Sample Info");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Sample Data");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Header Info");
            this.Btn_SelectAllOptions = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.CheckListBox_DebugElements = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // Btn_SelectAllOptions
            // 
            this.Btn_SelectAllOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_SelectAllOptions.Location = new System.Drawing.Point(243, 12);
            this.Btn_SelectAllOptions.Name = "Btn_SelectAllOptions";
            this.Btn_SelectAllOptions.Size = new System.Drawing.Size(75, 23);
            this.Btn_SelectAllOptions.TabIndex = 1;
            this.Btn_SelectAllOptions.Text = "Select All";
            this.Btn_SelectAllOptions.UseVisualStyleBackColor = true;
            this.Btn_SelectAllOptions.Click += new System.EventHandler(this.Btn_SelectAllOptions_Click);
            // 
            // Btn_OK
            // 
            this.Btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_OK.Location = new System.Drawing.Point(243, 193);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 2;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Cancel.Location = new System.Drawing.Point(243, 222);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // CheckListBox_DebugElements
            // 
            this.CheckListBox_DebugElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckListBox_DebugElements.CheckBoxes = true;
            this.CheckListBox_DebugElements.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.CheckListBox_DebugElements.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            this.CheckListBox_DebugElements.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.CheckListBox_DebugElements.Location = new System.Drawing.Point(12, 12);
            this.CheckListBox_DebugElements.Name = "CheckListBox_DebugElements";
            this.CheckListBox_DebugElements.Size = new System.Drawing.Size(219, 233);
            this.CheckListBox_DebugElements.TabIndex = 0;
            this.CheckListBox_DebugElements.UseCompatibleStateImageBehavior = false;
            this.CheckListBox_DebugElements.View = System.Windows.Forms.View.List;
            // 
            // Frm_Sounbanks_DebugOutput
            // 
            this.AcceptButton = this.Btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Btn_Cancel;
            this.ClientSize = new System.Drawing.Size(330, 257);
            this.Controls.Add(this.CheckListBox_DebugElements);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.Btn_SelectAllOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Sounbanks_DebugOutput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Debug Types";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_SelectAllOptions;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.ListView CheckListBox_DebugElements;
    }
}