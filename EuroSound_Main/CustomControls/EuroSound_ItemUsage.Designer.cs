
namespace EuroSound_Application.CustomControls.ObjectInstancesForm
{
    partial class EuroSound_ItemUsage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EuroSound_ItemUsage));
            this.ListView_ItemUsage = new System.Windows.Forms.ListView();
            this.Col_Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Usage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList_ListView = new System.Windows.Forms.ImageList(this.components);
            this.Button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListView_ItemUsage
            // 
            this.ListView_ItemUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_ItemUsage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Item,
            this.Col_Usage});
            this.ListView_ItemUsage.FullRowSelect = true;
            this.ListView_ItemUsage.GridLines = true;
            this.ListView_ItemUsage.HideSelection = false;
            this.ListView_ItemUsage.Location = new System.Drawing.Point(12, 12);
            this.ListView_ItemUsage.Name = "ListView_ItemUsage";
            this.ListView_ItemUsage.Size = new System.Drawing.Size(409, 314);
            this.ListView_ItemUsage.SmallImageList = this.ImageList_ListView;
            this.ListView_ItemUsage.TabIndex = 0;
            this.ListView_ItemUsage.UseCompatibleStateImageBehavior = false;
            this.ListView_ItemUsage.View = System.Windows.Forms.View.Details;
            this.ListView_ItemUsage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_ItemUsage_MouseDoubleClick);
            // 
            // Col_Item
            // 
            this.Col_Item.Text = "Item";
            this.Col_Item.Width = 79;
            // 
            // Col_Usage
            // 
            this.Col_Usage.Text = "Usage";
            this.Col_Usage.Width = 305;
            // 
            // ImageList_ListView
            // 
            this.ImageList_ListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_ListView.ImageStream")));
            this.ImageList_ListView.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_ListView.Images.SetKeyName(0, "exclamation.png");
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.Location = new System.Drawing.Point(346, 332);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 1;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // EuroSound_ItemUsage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 367);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.ListView_ItemUsage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_ItemUsage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound_ItemUsage";
            this.Shown += new System.EventHandler(this.EuroSound_ItemUsage_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListView_ItemUsage;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.ColumnHeader Col_Item;
        private System.Windows.Forms.ColumnHeader Col_Usage;
        private System.Windows.Forms.ImageList ImageList_ListView;
    }
}