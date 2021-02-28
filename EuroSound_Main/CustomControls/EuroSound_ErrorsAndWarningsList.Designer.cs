
using EuroSound_Application.CustomControls.ListViewColumnSorting;

namespace EuroSound_Application.CustomControls.WarningsList
{
    partial class EuroSound_ErrorsAndWarningsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EuroSound_ErrorsAndWarningsList));
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Copy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ListViewImages = new System.Windows.Forms.ImageList(this.components);
            this.Button_Print = new System.Windows.Forms.Button();
            this.PrintDialog_Document = new System.Windows.Forms.PrintDialog();
            this.PrintDocument_DocumentToPrint = new System.Drawing.Printing.PrintDocument();
            this.ListView_Reports = new EuroSound_Application.CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick();
            this.Col_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Error = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_OK.Location = new System.Drawing.Point(368, 409);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 3;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Copy
            // 
            this.Button_Copy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Copy.Location = new System.Drawing.Point(206, 409);
            this.Button_Copy.Name = "Button_Copy";
            this.Button_Copy.Size = new System.Drawing.Size(75, 23);
            this.Button_Copy.TabIndex = 1;
            this.Button_Copy.Text = "Copy";
            this.Button_Copy.UseVisualStyleBackColor = true;
            this.Button_Copy.Click += new System.EventHandler(this.Button_Copy_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 409);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Click on Column Headers to Sort";
            // 
            // ListViewImages
            // 
            this.ListViewImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImages.ImageStream")));
            this.ListViewImages.TransparentColor = System.Drawing.Color.Transparent;
            this.ListViewImages.Images.SetKeyName(0, "Message_Error.png");
            this.ListViewImages.Images.SetKeyName(1, "Message_Warning.png");
            this.ListViewImages.Images.SetKeyName(2, "Message_Info.png");
            // 
            // Button_Print
            // 
            this.Button_Print.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Print.Location = new System.Drawing.Point(287, 409);
            this.Button_Print.Name = "Button_Print";
            this.Button_Print.Size = new System.Drawing.Size(75, 23);
            this.Button_Print.TabIndex = 2;
            this.Button_Print.Text = "Print";
            this.Button_Print.UseVisualStyleBackColor = true;
            this.Button_Print.Click += new System.EventHandler(this.Button_Print_Click);
            // 
            // PrintDialog_Document
            // 
            this.PrintDialog_Document.AllowSelection = true;
            this.PrintDialog_Document.PrintToFile = true;
            this.PrintDialog_Document.UseEXDialog = true;
            // 
            // PrintDocument_DocumentToPrint
            // 
            this.PrintDocument_DocumentToPrint.OriginAtMargins = true;
            this.PrintDocument_DocumentToPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_DocumentToPrint_PrintPage);
            // 
            // ListView_Reports
            // 
            this.ListView_Reports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Reports.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Level,
            this.Col_Error});
            this.ListView_Reports.FullRowSelect = true;
            this.ListView_Reports.HideSelection = false;
            this.ListView_Reports.Location = new System.Drawing.Point(12, 12);
            this.ListView_Reports.MultiSelect = false;
            this.ListView_Reports.Name = "ListView_Reports";
            this.ListView_Reports.Size = new System.Drawing.Size(627, 391);
            this.ListView_Reports.SmallImageList = this.ListViewImages;
            this.ListView_Reports.TabIndex = 0;
            this.ListView_Reports.UseCompatibleStateImageBehavior = false;
            this.ListView_Reports.View = System.Windows.Forms.View.Details;
            // 
            // Col_Level
            // 
            this.Col_Level.Text = "Level";
            this.Col_Level.Width = 70;
            // 
            // Col_Error
            // 
            this.Col_Error.Text = "Error";
            this.Col_Error.Width = 620;
            // 
            // EuroSound_ErrorsAndWarningsList
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 444);
            this.Controls.Add(this.Button_Print);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListView_Reports);
            this.Controls.Add(this.Button_Copy);
            this.Controls.Add(this.Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_ErrorsAndWarningsList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound_ImportResultsList";
            this.Load += new System.EventHandler(this.EuroSound_ImportResultsList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Copy;
        private ListView_ColumnSortingClick ListView_Reports;
        private System.Windows.Forms.ColumnHeader Col_Level;
        private System.Windows.Forms.ColumnHeader Col_Error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList ListViewImages;
        private System.Windows.Forms.Button Button_Print;
        private System.Windows.Forms.PrintDialog PrintDialog_Document;
        private System.Drawing.Printing.PrintDocument PrintDocument_DocumentToPrint;
    }
}