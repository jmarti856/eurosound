namespace RichTextBoxEditor
{
    partial class RichTextBoxEditor
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextBoxEditor));
            this.MenuOptions = new System.Windows.Forms.ToolStrip();
            this.FontToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FontColorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.BoldToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ItalicToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.UnderlineToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.LeftToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CenterToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RightToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BulletsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SpellcheckToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RichTextbox_Content = new System.Windows.Forms.RichTextBox();
            this.ContextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.rightToLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontDlg = new System.Windows.Forms.FontDialog();
            this.ColorDlg = new System.Windows.Forms.ColorDialog();
            this.SpellChecker = new NetSpell.SpellChecker.Spelling(this.components);
            this.MenuOptions.SuspendLayout();
            this.ContextMenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuOptions
            // 
            this.MenuOptions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MenuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontToolStripButton,
            this.FontColorToolStripButton,
            this.BoldToolStripButton,
            this.ItalicToolStripButton,
            this.UnderlineToolStripButton,
            this.ToolStripSeparator4,
            this.LeftToolStripButton,
            this.CenterToolStripButton,
            this.RightToolStripButton,
            this.ToolStripSeparator3,
            this.BulletsToolStripButton,
            this.SpellcheckToolStripButton,
            this.ToolStripSeparator2,
            this.CutToolStripButton,
            this.CopyToolStripButton,
            this.PasteToolStripButton});
            this.MenuOptions.Location = new System.Drawing.Point(0, 0);
            this.MenuOptions.Name = "MenuOptions";
            this.MenuOptions.Size = new System.Drawing.Size(403, 25);
            this.MenuOptions.TabIndex = 0;
            this.MenuOptions.Text = "ToolStrip1";
            // 
            // FontToolStripButton
            // 
            this.FontToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FontToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("FontToolStripButton.Image")));
            this.FontToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FontToolStripButton.MergeIndex = 0;
            this.FontToolStripButton.Name = "FontToolStripButton";
            this.FontToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FontToolStripButton.Text = "Font";
            this.FontToolStripButton.Click += new System.EventHandler(this.FontToolStripButton_Click);
            // 
            // FontColorToolStripButton
            // 
            this.FontColorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FontColorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("FontColorToolStripButton.Image")));
            this.FontColorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FontColorToolStripButton.MergeIndex = 1;
            this.FontColorToolStripButton.Name = "FontColorToolStripButton";
            this.FontColorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FontColorToolStripButton.Text = "Font Color";
            this.FontColorToolStripButton.Click += new System.EventHandler(this.FontColorToolStripButton_Click);
            // 
            // BoldToolStripButton
            // 
            this.BoldToolStripButton.CheckOnClick = true;
            this.BoldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BoldToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("BoldToolStripButton.Image")));
            this.BoldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BoldToolStripButton.MergeIndex = 2;
            this.BoldToolStripButton.Name = "BoldToolStripButton";
            this.BoldToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.BoldToolStripButton.Text = "Bold";
            this.BoldToolStripButton.CheckedChanged += new System.EventHandler(this.BoldToolStripButton_CheckedChanged);
            this.BoldToolStripButton.Click += new System.EventHandler(this.BoldToolStripButton_Click);
            // 
            // ItalicToolStripButton
            // 
            this.ItalicToolStripButton.CheckOnClick = true;
            this.ItalicToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ItalicToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ItalicToolStripButton.Image")));
            this.ItalicToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ItalicToolStripButton.MergeIndex = 3;
            this.ItalicToolStripButton.Name = "ItalicToolStripButton";
            this.ItalicToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ItalicToolStripButton.Text = "Italic";
            this.ItalicToolStripButton.CheckedChanged += new System.EventHandler(this.ItalicToolStripButton_CheckedChanged);
            // 
            // UnderlineToolStripButton
            // 
            this.UnderlineToolStripButton.CheckOnClick = true;
            this.UnderlineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnderlineToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("UnderlineToolStripButton.Image")));
            this.UnderlineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnderlineToolStripButton.MergeIndex = 4;
            this.UnderlineToolStripButton.Name = "UnderlineToolStripButton";
            this.UnderlineToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.UnderlineToolStripButton.Text = "Underline";
            this.UnderlineToolStripButton.CheckedChanged += new System.EventHandler(this.UnderlineToolStripButton_CheckedChanged);
            this.UnderlineToolStripButton.Click += new System.EventHandler(this.UnderlineToolStripButton_Click);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.MergeIndex = 5;
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // LeftToolStripButton
            // 
            this.LeftToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LeftToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("LeftToolStripButton.Image")));
            this.LeftToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LeftToolStripButton.MergeIndex = 6;
            this.LeftToolStripButton.Name = "LeftToolStripButton";
            this.LeftToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.LeftToolStripButton.Text = "Left";
            this.LeftToolStripButton.Click += new System.EventHandler(this.LeftToolStripButton_Click);
            // 
            // CenterToolStripButton
            // 
            this.CenterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CenterToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CenterToolStripButton.Image")));
            this.CenterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CenterToolStripButton.MergeIndex = 7;
            this.CenterToolStripButton.Name = "CenterToolStripButton";
            this.CenterToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CenterToolStripButton.Text = "Center";
            this.CenterToolStripButton.Click += new System.EventHandler(this.CenterToolStripButton_Click);
            // 
            // RightToolStripButton
            // 
            this.RightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RightToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RightToolStripButton.Image")));
            this.RightToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RightToolStripButton.MergeIndex = 8;
            this.RightToolStripButton.Name = "RightToolStripButton";
            this.RightToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.RightToolStripButton.Text = "Right";
            this.RightToolStripButton.Click += new System.EventHandler(this.RightToolStripButton_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.MergeIndex = 9;
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // BulletsToolStripButton
            // 
            this.BulletsToolStripButton.CheckOnClick = true;
            this.BulletsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BulletsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("BulletsToolStripButton.Image")));
            this.BulletsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BulletsToolStripButton.MergeIndex = 10;
            this.BulletsToolStripButton.Name = "BulletsToolStripButton";
            this.BulletsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.BulletsToolStripButton.Text = "Bullets";
            this.BulletsToolStripButton.CheckedChanged += new System.EventHandler(this.BulletsToolStripButton_CheckedChanged);
            // 
            // SpellcheckToolStripButton
            // 
            this.SpellcheckToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SpellcheckToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SpellcheckToolStripButton.Image")));
            this.SpellcheckToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SpellcheckToolStripButton.MergeIndex = 11;
            this.SpellcheckToolStripButton.Name = "SpellcheckToolStripButton";
            this.SpellcheckToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SpellcheckToolStripButton.Text = "Spell Check";
            this.SpellcheckToolStripButton.Click += new System.EventHandler(this.SpellcheckToolStripButton_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.MergeIndex = 12;
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // CutToolStripButton
            // 
            this.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CutToolStripButton.Image")));
            this.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutToolStripButton.MergeIndex = 13;
            this.CutToolStripButton.Name = "CutToolStripButton";
            this.CutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CutToolStripButton.Text = "Cut";
            this.CutToolStripButton.Click += new System.EventHandler(this.CutToolStripButton_Click);
            // 
            // CopyToolStripButton
            // 
            this.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripButton.Image")));
            this.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripButton.MergeIndex = 14;
            this.CopyToolStripButton.Name = "CopyToolStripButton";
            this.CopyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CopyToolStripButton.Text = "Copy";
            this.CopyToolStripButton.Click += new System.EventHandler(this.CopyToolStripButton_Click);
            // 
            // PasteToolStripButton
            // 
            this.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripButton.Image")));
            this.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripButton.MergeIndex = 15;
            this.PasteToolStripButton.Name = "PasteToolStripButton";
            this.PasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PasteToolStripButton.Text = "Paste";
            this.PasteToolStripButton.Click += new System.EventHandler(this.PasteToolStripButton_Click);
            // 
            // RichTextbox_Content
            // 
            this.RichTextbox_Content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextbox_Content.ContextMenuStrip = this.ContextMenuMain;
            this.RichTextbox_Content.Location = new System.Drawing.Point(0, 28);
            this.RichTextbox_Content.Name = "RichTextbox_Content";
            this.RichTextbox_Content.Size = new System.Drawing.Size(403, 194);
            this.RichTextbox_Content.TabIndex = 1;
            this.RichTextbox_Content.Text = "";
            this.RichTextbox_Content.SelectionChanged += new System.EventHandler(this.RichTextbox_Content_SelectionChanged);
            // 
            // ContextMenuMain
            // 
            this.ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator5,
            this.rightToLeftToolStripMenuItem});
            this.ContextMenuMain.Name = "ContextMenuMain";
            this.ContextMenuMain.Size = new System.Drawing.Size(137, 148);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(133, 6);
            // 
            // rightToLeftToolStripMenuItem
            // 
            this.rightToLeftToolStripMenuItem.CheckOnClick = true;
            this.rightToLeftToolStripMenuItem.Name = "rightToLeftToolStripMenuItem";
            this.rightToLeftToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.rightToLeftToolStripMenuItem.Text = "Right to left";
            this.rightToLeftToolStripMenuItem.CheckedChanged += new System.EventHandler(this.RightToLeftToolStripMenuItem_CheckedChanged);
            // 
            // SpellChecker
            // 
            this.SpellChecker.Dictionary = null;
            this.SpellChecker.DeletedWord += new NetSpell.SpellChecker.Spelling.DeletedWordEventHandler(this.SpellChecker_DeletedWord);
            this.SpellChecker.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
            this.SpellChecker.ReplacedWord += new NetSpell.SpellChecker.Spelling.ReplacedWordEventHandler(this.SpellChecker_ReplacedWord);
            // 
            // RichTextBoxEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RichTextbox_Content);
            this.Controls.Add(this.MenuOptions);
            this.Name = "RichTextBoxEditor";
            this.Size = new System.Drawing.Size(403, 222);
            this.MenuOptions.ResumeLayout(false);
            this.MenuOptions.PerformLayout();
            this.ContextMenuMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.ToolStripButton FontToolStripButton;
        internal System.Windows.Forms.ToolStripButton FontColorToolStripButton;
        internal System.Windows.Forms.ToolStripButton BoldToolStripButton;
        internal System.Windows.Forms.ToolStripButton UnderlineToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton LeftToolStripButton;
        internal System.Windows.Forms.ToolStripButton CenterToolStripButton;
        internal System.Windows.Forms.ToolStripButton RightToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton BulletsToolStripButton;
        internal System.Windows.Forms.ToolStripButton SpellcheckToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton CutToolStripButton;
        internal System.Windows.Forms.ToolStripButton CopyToolStripButton;
        internal System.Windows.Forms.ToolStripButton PasteToolStripButton;
        private System.Windows.Forms.FontDialog FontDlg;
        private System.Windows.Forms.ColorDialog ColorDlg;
        private NetSpell.SpellChecker.Spelling SpellChecker;
        internal System.Windows.Forms.ToolStripButton ItalicToolStripButton;
        private System.Windows.Forms.ContextMenuStrip ContextMenuMain;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem rightToLeftToolStripMenuItem;
        public System.Windows.Forms.RichTextBox RichTextbox_Content;
        public System.Windows.Forms.ToolStrip MenuOptions;
    }
}
