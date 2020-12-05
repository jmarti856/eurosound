
namespace EuroSound_SB_Editor
{
    partial class EuroSound_NodesToFolder
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
            this.label_TypeOfSounds = new System.Windows.Forms.Label();
            this.Combobox_SoudsType = new System.Windows.Forms.ComboBox();
            this.ListBox_Items = new System.Windows.Forms.ListBox();
            this.Label_Folder = new System.Windows.Forms.Label();
            this.Combobox_AvailableFolders = new System.Windows.Forms.ComboBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_TypeOfSounds
            // 
            this.label_TypeOfSounds.AutoSize = true;
            this.label_TypeOfSounds.Location = new System.Drawing.Point(12, 15);
            this.label_TypeOfSounds.Name = "label_TypeOfSounds";
            this.label_TypeOfSounds.Size = new System.Drawing.Size(85, 13);
            this.label_TypeOfSounds.TabIndex = 0;
            this.label_TypeOfSounds.Text = "Type of Sounds:";
            // 
            // Combobox_SoudsType
            // 
            this.Combobox_SoudsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_SoudsType.FormattingEnabled = true;
            this.Combobox_SoudsType.Items.AddRange(new object[] {
            "Sounds",
            "StreamedSounds"});
            this.Combobox_SoudsType.Location = new System.Drawing.Point(103, 12);
            this.Combobox_SoudsType.Name = "Combobox_SoudsType";
            this.Combobox_SoudsType.Size = new System.Drawing.Size(252, 21);
            this.Combobox_SoudsType.TabIndex = 1;
            this.Combobox_SoudsType.SelectedIndexChanged += new System.EventHandler(this.Combobox_SoudsType_SelectedIndexChanged);
            // 
            // ListBox_Items
            // 
            this.ListBox_Items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox_Items.FormattingEnabled = true;
            this.ListBox_Items.HorizontalScrollbar = true;
            this.ListBox_Items.Location = new System.Drawing.Point(12, 66);
            this.ListBox_Items.Name = "ListBox_Items";
            this.ListBox_Items.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListBox_Items.Size = new System.Drawing.Size(343, 316);
            this.ListBox_Items.TabIndex = 2;
            // 
            // Label_Folder
            // 
            this.Label_Folder.AutoSize = true;
            this.Label_Folder.Location = new System.Drawing.Point(12, 42);
            this.Label_Folder.Name = "Label_Folder";
            this.Label_Folder.Size = new System.Drawing.Size(85, 13);
            this.Label_Folder.TabIndex = 3;
            this.Label_Folder.Text = "Move To Folder:";
            // 
            // Combobox_AvailableFolders
            // 
            this.Combobox_AvailableFolders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_AvailableFolders.FormattingEnabled = true;
            this.Combobox_AvailableFolders.Location = new System.Drawing.Point(103, 39);
            this.Combobox_AvailableFolders.Name = "Combobox_AvailableFolders";
            this.Combobox_AvailableFolders.Size = new System.Drawing.Size(252, 21);
            this.Combobox_AvailableFolders.TabIndex = 4;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_OK.Location = new System.Drawing.Point(199, 388);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 5;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(280, 388);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 6;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // EuroSound_NodesToFolder
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Button_Cancel;
            this.ClientSize = new System.Drawing.Size(367, 424);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Combobox_AvailableFolders);
            this.Controls.Add(this.Label_Folder);
            this.Controls.Add(this.ListBox_Items);
            this.Controls.Add(this.Combobox_SoudsType);
            this.Controls.Add(this.label_TypeOfSounds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_NodesToFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Sounds to Folder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_TypeOfSounds;
        private System.Windows.Forms.ComboBox Combobox_SoudsType;
        private System.Windows.Forms.ListBox ListBox_Items;
        private System.Windows.Forms.Label Label_Folder;
        private System.Windows.Forms.ComboBox Combobox_AvailableFolders;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
    }
}