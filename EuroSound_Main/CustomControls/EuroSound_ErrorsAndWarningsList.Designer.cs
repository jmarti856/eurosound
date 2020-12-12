
namespace EuroSound_Application
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
            this.ListView_Reports = new System.Windows.Forms.ListView();
            this.Col_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Error = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Copy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListView_Reports
            // 
            this.ListView_Reports.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Reports.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Level,
            this.Col_Error});
            this.ListView_Reports.GridLines = true;
            this.ListView_Reports.HideSelection = false;
            this.ListView_Reports.Location = new System.Drawing.Point(12, 12);
            this.ListView_Reports.Name = "ListView_Reports";
            this.ListView_Reports.Size = new System.Drawing.Size(590, 360);
            this.ListView_Reports.TabIndex = 0;
            this.ListView_Reports.UseCompatibleStateImageBehavior = false;
            this.ListView_Reports.View = System.Windows.Forms.View.Details;
            // 
            // Col_Level
            // 
            this.Col_Level.Text = "Level";
            this.Col_Level.Width = 40;
            // 
            // Col_Error
            // 
            this.Col_Error.Text = "Error";
            this.Col_Error.Width = 529;
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_OK.Location = new System.Drawing.Point(306, 378);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Copy
            // 
            this.Button_Copy.Location = new System.Drawing.Point(225, 378);
            this.Button_Copy.Name = "Button_Copy";
            this.Button_Copy.Size = new System.Drawing.Size(75, 23);
            this.Button_Copy.TabIndex = 1;
            this.Button_Copy.Text = "Copy";
            this.Button_Copy.UseVisualStyleBackColor = true;
            this.Button_Copy.Click += new System.EventHandler(this.Button_Copy_Click);
            // 
            // EuroSound_ErrorsAndWarningsList
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 413);
            this.Controls.Add(this.Button_Copy);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.ListView_Reports);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EuroSound_ErrorsAndWarningsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EuroSound_ImportResultsList";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.EuroSound_ImportResultsList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListView_Reports;
        private System.Windows.Forms.ColumnHeader Col_Level;
        private System.Windows.Forms.ColumnHeader Col_Error;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Copy;
    }
}