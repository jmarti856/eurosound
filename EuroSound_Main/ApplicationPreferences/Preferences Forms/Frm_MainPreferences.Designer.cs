
namespace EuroSound_Application.ApplicationPreferencesForms
{
    partial class Frm_MainPreferences
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Auto-Backup");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Editing");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ESF Tree");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Audio Devices");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("General", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Output");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Profile");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("System");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MainPreferences));
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Panel_SecondaryForms = new System.Windows.Forms.TableLayoutPanel();
            this.TreeViewPreferences = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(552, 413);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 7;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(633, 413);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 6;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Panel_SecondaryForms
            // 
            this.Panel_SecondaryForms.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.Panel_SecondaryForms.ColumnCount = 1;
            this.Panel_SecondaryForms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Panel_SecondaryForms.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.Panel_SecondaryForms.Location = new System.Drawing.Point(195, 12);
            this.Panel_SecondaryForms.Name = "Panel_SecondaryForms";
            this.Panel_SecondaryForms.RowCount = 1;
            this.Panel_SecondaryForms.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Panel_SecondaryForms.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 392F));
            this.Panel_SecondaryForms.Size = new System.Drawing.Size(513, 395);
            this.Panel_SecondaryForms.TabIndex = 5;
            // 
            // TreeViewPreferences
            // 
            this.TreeViewPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewPreferences.Location = new System.Drawing.Point(12, 12);
            this.TreeViewPreferences.Name = "TreeViewPreferences";
            treeNode1.Name = "AutoBackup";
            treeNode1.Text = "Auto-Backup";
            treeNode2.Name = "Editing";
            treeNode2.Text = "Editing";
            treeNode3.Name = "ESFTree";
            treeNode3.Text = "ESF Tree";
            treeNode4.Name = "AudioDevices";
            treeNode4.Text = "Audio Devices";
            treeNode5.Name = "General";
            treeNode5.Text = "General";
            treeNode6.Name = "Output";
            treeNode6.Text = "Output";
            treeNode7.Name = "Profile";
            treeNode7.Text = "Profile";
            treeNode8.Name = "System";
            treeNode8.Text = "System";
            this.TreeViewPreferences.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.TreeViewPreferences.Size = new System.Drawing.Size(177, 424);
            this.TreeViewPreferences.TabIndex = 0;
            this.TreeViewPreferences.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewPreferences_AfterSelect);
            // 
            // Frm_MainPreferences
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(720, 448);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Panel_SecondaryForms);
            this.Controls.Add(this.TreeViewPreferences);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_MainPreferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Prefs";
            this.Text = "Global Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MainPreferences_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MainPreferences_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.TableLayoutPanel Panel_SecondaryForms;
        private System.Windows.Forms.TreeView TreeViewPreferences;
    }
}