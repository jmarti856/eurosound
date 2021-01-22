
using EuroSound_Application.CustomControls.ListViewColumnSorting;

namespace EuroSound_Application.CustomControls.SearcherForm
{
    partial class EuroSound_SearchItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EuroSound_SearchItem));
            this.MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.MenuItem_File = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_New = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_SaveAs = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_Separator1 = new System.Windows.Forms.MenuItem();
            this.MenuItemFile_Close = new System.Windows.Forms.MenuItem();
            this.MenuItem_Edit = new System.Windows.Forms.MenuItem();
            this.MenuItemEdit_SelectAll = new System.Windows.Forms.MenuItem();
            this.MenuItemEdit_SelectNone = new System.Windows.Forms.MenuItem();
            this.MenuItemEdit_InvertSelection = new System.Windows.Forms.MenuItem();
            this.MenuItem_Object = new System.Windows.Forms.MenuItem();
            this.MenuItemObject_Edit = new System.Windows.Forms.MenuItem();
            this.MenuItemObject_Separator1 = new System.Windows.Forms.MenuItem();
            this.MenuItemObject_TextColor = new System.Windows.Forms.MenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.Label_Results = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_LookIn = new System.Windows.Forms.Label();
            this.Combobox_LookIn = new System.Windows.Forms.ComboBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.Label_SearchFor = new System.Windows.Forms.Label();
            this.Textbox_TextToSearch = new System.Windows.Forms.TextBox();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.RadioButton_MatchCase = new System.Windows.Forms.RadioButton();
            this.RadioButton_WholeWord = new System.Windows.Forms.RadioButton();
            this.Button_NewSearch = new System.Windows.Forms.Button();
            this.BgWorker_Searches = new System.ComponentModel.BackgroundWorker();
            this.ListViewResults = new EuroSound_Application.CustomControls.ListViewColumnSorting.ListView_ColumnSortingClick();
            this.Col_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Col_Folder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem_File,
            this.MenuItem_Edit,
            this.MenuItem_Object});
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.Index = 0;
            this.MenuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemFile_New,
            this.MenuItemFile_SaveAs,
            this.MenuItemFile_Separator1,
            this.MenuItemFile_Close});
            this.MenuItem_File.Text = "File";
            // 
            // MenuItemFile_New
            // 
            this.MenuItemFile_New.Index = 0;
            this.MenuItemFile_New.Text = "New...";
            this.MenuItemFile_New.Click += new System.EventHandler(this.MenuItemFile_New_Click);
            // 
            // MenuItemFile_SaveAs
            // 
            this.MenuItemFile_SaveAs.Index = 1;
            this.MenuItemFile_SaveAs.Text = "Save As...";
            this.MenuItemFile_SaveAs.Click += new System.EventHandler(this.MenuItemFile_SaveAs_Click);
            // 
            // MenuItemFile_Separator1
            // 
            this.MenuItemFile_Separator1.Index = 2;
            this.MenuItemFile_Separator1.Text = "-";
            // 
            // MenuItemFile_Close
            // 
            this.MenuItemFile_Close.Index = 3;
            this.MenuItemFile_Close.Text = "Close";
            this.MenuItemFile_Close.Click += new System.EventHandler(this.MenuItemFile_Close_Click);
            // 
            // MenuItem_Edit
            // 
            this.MenuItem_Edit.Index = 1;
            this.MenuItem_Edit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemEdit_SelectAll,
            this.MenuItemEdit_SelectNone,
            this.MenuItemEdit_InvertSelection});
            this.MenuItem_Edit.Text = "Edit";
            // 
            // MenuItemEdit_SelectAll
            // 
            this.MenuItemEdit_SelectAll.Index = 0;
            this.MenuItemEdit_SelectAll.Text = "Select All";
            this.MenuItemEdit_SelectAll.Click += new System.EventHandler(this.MenuItemEdit_SelectAll_Click);
            // 
            // MenuItemEdit_SelectNone
            // 
            this.MenuItemEdit_SelectNone.Index = 1;
            this.MenuItemEdit_SelectNone.Text = "Select None";
            this.MenuItemEdit_SelectNone.Click += new System.EventHandler(this.MenuItemEdit_SelectNone_Click);
            // 
            // MenuItemEdit_InvertSelection
            // 
            this.MenuItemEdit_InvertSelection.Index = 2;
            this.MenuItemEdit_InvertSelection.Text = "Invert Selection";
            this.MenuItemEdit_InvertSelection.Click += new System.EventHandler(this.MenuItemEdit_InvertSelection_Click);
            // 
            // MenuItem_Object
            // 
            this.MenuItem_Object.Index = 2;
            this.MenuItem_Object.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemObject_Edit,
            this.MenuItemObject_Separator1,
            this.MenuItemObject_TextColor});
            this.MenuItem_Object.Text = "Object";
            // 
            // MenuItemObject_Edit
            // 
            this.MenuItemObject_Edit.Index = 0;
            this.MenuItemObject_Edit.Text = "Edit";
            this.MenuItemObject_Edit.Click += new System.EventHandler(this.MenuItemObject_Edit_Click);
            // 
            // MenuItemObject_Separator1
            // 
            this.MenuItemObject_Separator1.Index = 1;
            this.MenuItemObject_Separator1.Text = "-";
            // 
            // MenuItemObject_TextColor
            // 
            this.MenuItemObject_TextColor.Index = 2;
            this.MenuItemObject_TextColor.Text = "Text Color...";
            this.MenuItemObject_TextColor.Click += new System.EventHandler(this.MenuItemObject_TextColor_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_Results});
            this.StatusBar.Location = new System.Drawing.Point(0, 511);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(617, 22);
            this.StatusBar.TabIndex = 0;
            this.StatusBar.Text = "statusStrip1";
            // 
            // Label_Results
            // 
            this.Label_Results.Name = "Label_Results";
            this.Label_Results.Size = new System.Drawing.Size(45, 17);
            this.Label_Results.Text = "0 Items";
            // 
            // Label_LookIn
            // 
            this.Label_LookIn.AutoSize = true;
            this.Label_LookIn.Location = new System.Drawing.Point(28, 15);
            this.Label_LookIn.Name = "Label_LookIn";
            this.Label_LookIn.Size = new System.Drawing.Size(46, 13);
            this.Label_LookIn.TabIndex = 1;
            this.Label_LookIn.Text = "Look In:";
            // 
            // Combobox_LookIn
            // 
            this.Combobox_LookIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox_LookIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox_LookIn.FormattingEnabled = true;
            this.Combobox_LookIn.Location = new System.Drawing.Point(80, 12);
            this.Combobox_LookIn.Name = "Combobox_LookIn";
            this.Combobox_LookIn.Size = new System.Drawing.Size(444, 21);
            this.Combobox_LookIn.TabIndex = 2;
            // 
            // Button_Search
            // 
            this.Button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Search.Location = new System.Drawing.Point(530, 10);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(75, 23);
            this.Button_Search.TabIndex = 3;
            this.Button_Search.Text = "Search...";
            this.Button_Search.UseVisualStyleBackColor = true;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // Label_SearchFor
            // 
            this.Label_SearchFor.AutoSize = true;
            this.Label_SearchFor.Location = new System.Drawing.Point(12, 42);
            this.Label_SearchFor.Name = "Label_SearchFor";
            this.Label_SearchFor.Size = new System.Drawing.Size(62, 13);
            this.Label_SearchFor.TabIndex = 4;
            this.Label_SearchFor.Text = "Search For:";
            // 
            // Textbox_TextToSearch
            // 
            this.Textbox_TextToSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Textbox_TextToSearch.Location = new System.Drawing.Point(80, 39);
            this.Textbox_TextToSearch.Name = "Textbox_TextToSearch";
            this.Textbox_TextToSearch.Size = new System.Drawing.Size(444, 20);
            this.Textbox_TextToSearch.TabIndex = 5;
            // 
            // Button_Stop
            // 
            this.Button_Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Stop.Location = new System.Drawing.Point(530, 37);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(75, 23);
            this.Button_Stop.TabIndex = 6;
            this.Button_Stop.Text = "Stop";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // RadioButton_MatchCase
            // 
            this.RadioButton_MatchCase.AutoSize = true;
            this.RadioButton_MatchCase.Checked = true;
            this.RadioButton_MatchCase.Location = new System.Drawing.Point(80, 72);
            this.RadioButton_MatchCase.Name = "RadioButton_MatchCase";
            this.RadioButton_MatchCase.Size = new System.Drawing.Size(82, 17);
            this.RadioButton_MatchCase.TabIndex = 8;
            this.RadioButton_MatchCase.TabStop = true;
            this.RadioButton_MatchCase.Text = "Match Case";
            this.RadioButton_MatchCase.UseVisualStyleBackColor = true;
            // 
            // RadioButton_WholeWord
            // 
            this.RadioButton_WholeWord.AutoSize = true;
            this.RadioButton_WholeWord.Location = new System.Drawing.Point(168, 72);
            this.RadioButton_WholeWord.Name = "RadioButton_WholeWord";
            this.RadioButton_WholeWord.Size = new System.Drawing.Size(85, 17);
            this.RadioButton_WholeWord.TabIndex = 9;
            this.RadioButton_WholeWord.Text = "Whole Word";
            this.RadioButton_WholeWord.UseVisualStyleBackColor = true;
            // 
            // Button_NewSearch
            // 
            this.Button_NewSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_NewSearch.Location = new System.Drawing.Point(530, 66);
            this.Button_NewSearch.Name = "Button_NewSearch";
            this.Button_NewSearch.Size = new System.Drawing.Size(75, 23);
            this.Button_NewSearch.TabIndex = 10;
            this.Button_NewSearch.Text = "New Search";
            this.Button_NewSearch.UseVisualStyleBackColor = true;
            this.Button_NewSearch.Click += new System.EventHandler(this.Button_NewSearch_Click);
            // 
            // BgWorker_Searches
            // 
            this.BgWorker_Searches.WorkerSupportsCancellation = true;
            this.BgWorker_Searches.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_Searches_DoWork);
            this.BgWorker_Searches.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_Searches_RunWorkerCompleted);
            // 
            // ListViewResults
            // 
            this.ListViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Col_Name,
            this.Col_Color,
            this.Col_Type,
            this.Col_Folder});
            this.ListViewResults.FullRowSelect = true;
            this.ListViewResults.GridLines = true;
            this.ListViewResults.HideSelection = false;
            this.ListViewResults.Location = new System.Drawing.Point(0, 95);
            this.ListViewResults.Name = "ListViewResults";
            this.ListViewResults.Size = new System.Drawing.Size(617, 415);
            this.ListViewResults.TabIndex = 11;
            this.ListViewResults.UseCompatibleStateImageBehavior = false;
            this.ListViewResults.View = System.Windows.Forms.View.Details;
            this.ListViewResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewResults_MouseDoubleClick);
            // 
            // Col_Name
            // 
            this.Col_Name.Text = "Name";
            this.Col_Name.Width = 197;
            // 
            // Col_Color
            // 
            this.Col_Color.Text = "Color";
            this.Col_Color.Width = 53;
            // 
            // Col_Type
            // 
            this.Col_Type.Text = "Type";
            this.Col_Type.Width = 95;
            // 
            // Col_Folder
            // 
            this.Col_Folder.Text = "Parent Node";
            this.Col_Folder.Width = 259;
            // 
            // EuroSound_SearchItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 533);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.ListViewResults);
            this.Controls.Add(this.Button_NewSearch);
            this.Controls.Add(this.RadioButton_WholeWord);
            this.Controls.Add(this.RadioButton_MatchCase);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.Textbox_TextToSearch);
            this.Controls.Add(this.Label_SearchFor);
            this.Controls.Add(this.Button_Search);
            this.Controls.Add(this.Combobox_LookIn);
            this.Controls.Add(this.Label_LookIn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.MainMenu;
            this.Name = "EuroSound_SearchItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.EuroSound_SearchItem_Load);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem MenuItem_File;
        private System.Windows.Forms.MenuItem MenuItemFile_New;
        private System.Windows.Forms.MenuItem MenuItemFile_SaveAs;
        private System.Windows.Forms.MenuItem MenuItemFile_Separator1;
        private System.Windows.Forms.MenuItem MenuItemFile_Close;
        private System.Windows.Forms.MenuItem MenuItem_Edit;
        private System.Windows.Forms.MenuItem MenuItemEdit_SelectAll;
        private System.Windows.Forms.MenuItem MenuItemEdit_SelectNone;
        private System.Windows.Forms.MenuItem MenuItemEdit_InvertSelection;
        private System.Windows.Forms.MenuItem MenuItem_Object;
        private System.Windows.Forms.MenuItem MenuItemObject_Edit;
        private System.Windows.Forms.MenuItem MenuItemObject_Separator1;
        private System.Windows.Forms.MenuItem MenuItemObject_TextColor;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel Label_Results;
        private System.Windows.Forms.Label Label_LookIn;
        private System.Windows.Forms.ComboBox Combobox_LookIn;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.Label Label_SearchFor;
        private System.Windows.Forms.TextBox Textbox_TextToSearch;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.RadioButton RadioButton_MatchCase;
        private System.Windows.Forms.RadioButton RadioButton_WholeWord;
        private System.Windows.Forms.Button Button_NewSearch;
        private ListView_ColumnSortingClick ListViewResults;
        private System.Windows.Forms.ColumnHeader Col_Name;
        private System.Windows.Forms.ColumnHeader Col_Color;
        private System.Windows.Forms.ColumnHeader Col_Type;
        private System.Windows.Forms.ColumnHeader Col_Folder;
        private System.ComponentModel.BackgroundWorker BgWorker_Searches;
    }
}