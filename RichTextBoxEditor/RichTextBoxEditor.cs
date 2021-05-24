using System;
using System.Drawing;
using System.Windows.Forms;

namespace RichTextBoxEditor
{
    public partial class RichTextBoxEditor : UserControl
    {
        public RichTextBoxEditor()
        {
            InitializeComponent();
        }

        private bool _ShowSpellChecker = false;

        public bool ShowSpellChecker
        {
            get
            {
                return _ShowSpellChecker;
            }
            set
            {
                _ShowSpellChecker = value;
                SpellcheckToolStripButton.Visible = _ShowSpellChecker;
            }
        }


        private void FontToolStripButton_Click(object sender, EventArgs e)
        {
            if (FontDlg.ShowDialog() == DialogResult.OK)
            {
                RichTextbox_Content.SelectionFont = FontDlg.Font;

                //Styles
                if (FontDlg.Font.Bold)
                {
                    BoldToolStripButton.Checked = true;
                }

                if (FontDlg.Font.Underline)
                {
                    UnderlineToolStripButton.Checked = true;
                }

                if (FontDlg.Font.Italic)
                {
                    ItalicToolStripButton.Checked = true;
                }
            }
        }

        private void FontColorToolStripButton_Click(object sender, EventArgs e)
        {
            if (ColorDlg.ShowDialog() == DialogResult.OK)
            {
                RichTextbox_Content.SelectionColor = ColorDlg.Color;
            }
        }

        private void BoldToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            //FontStyle BoldStyle = RichTextbox_Content.SelectionFont.Style;

            //if (!string.IsNullOrEmpty(RichTextbox_Content.SelectedText))
            //{
            //    if (RichTextbox_Content.SelectionFont.Bold)
            //    {
            //        BoldStyle &= ~FontStyle.Bold;
            //    }
            //    else
            //    {
            //        BoldStyle |= FontStyle.Bold;
            //    }

            //    RichTextbox_Content.SelectionFont = new Font(RichTextbox_Content.SelectionFont, BoldStyle);

            //    BoldToolStripButton.Checked = RichTextbox_Content.SelectionFont.Bold;
            //}
        }

        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {
            Font new1, old1;
            old1 = RichTextbox_Content.SelectionFont;
            if (old1.Bold)
                new1 = new Font(old1, old1.Style & ~FontStyle.Bold);
            else
                new1 = new Font(old1, old1.Style | FontStyle.Bold);

            RichTextbox_Content.SelectionFont = new1;
            RichTextbox_Content.Focus();
        }

        private void ItalicToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle ItalicStyle = RichTextbox_Content.SelectionFont.Style;

            if (!string.IsNullOrEmpty(RichTextbox_Content.SelectedText))
            {
                if (RichTextbox_Content.SelectionFont.Italic)
                {
                    ItalicStyle &= ~FontStyle.Italic;
                }
                else
                {
                    ItalicStyle |= FontStyle.Italic;
                }

                RichTextbox_Content.SelectionFont = new Font(RichTextbox_Content.SelectionFont, ItalicStyle);

                BoldToolStripButton.Checked = RichTextbox_Content.SelectionFont.Italic;
            }
        }

        private void UnderlineToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle UnderlineStyle = RichTextbox_Content.SelectionFont.Style;

            if (!string.IsNullOrEmpty(RichTextbox_Content.SelectedText))
            {
                if (RichTextbox_Content.SelectionFont.Underline)
                {
                    UnderlineStyle &= ~FontStyle.Underline;
                }
                else
                {
                    UnderlineStyle |= FontStyle.Underline;
                }

                RichTextbox_Content.SelectionFont = new Font(RichTextbox_Content.SelectionFont, UnderlineStyle);

                BoldToolStripButton.Checked = RichTextbox_Content.SelectionFont.Underline;
            }
        }

        private void LeftToolStripButton_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void CenterToolStripButton_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void RightToolStripButton_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void BulletsToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (BulletsToolStripButton.Checked)
            {
                RichTextbox_Content.SelectionBullet = true;
            }
            else
            {
                RichTextbox_Content.SelectionBullet = false;
            }
        }

        private void SpellcheckToolStripButton_Click(object sender, EventArgs e)
        {
            NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary
            {
                DictionaryFile = Application.StartupPath + "\\Dictionaries\\en-US.dic"
            };
            oDict.Initialize();
            SpellChecker.Dictionary = oDict;
            SpellChecker.Text = RichTextbox_Content.Text;
            SpellChecker.SpellCheck();
        }

        private void SpellChecker_ReplacedWord(object sender, NetSpell.SpellChecker.ReplaceWordEventArgs e)
        {
            //save existing selecting
            int start = RichTextbox_Content.SelectionStart;
            int length = RichTextbox_Content.SelectionLength;

            //select word for this event
            RichTextbox_Content.Select(e.TextIndex, e.Word.Length);

            //replace word
            RichTextbox_Content.SelectedText = e.ReplacementWord;

            if ((start + length) > RichTextbox_Content.Text.Length)
            {
                length = 0;
            }

            //restore selection
            RichTextbox_Content.Select(start, length);
        }

        private void SpellChecker_DeletedWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
        {
            //save existing selecting
            int start = RichTextbox_Content.SelectionStart;
            int length = RichTextbox_Content.SelectionLength;

            //select word for this event
            RichTextbox_Content.Select(e.TextIndex, e.Word.Length);

            //replace word
            RichTextbox_Content.SelectedText = "";

            if ((start + length) > RichTextbox_Content.Text.Length)
            {
                length = 0;
            }

            //restore selection
            RichTextbox_Content.Select(start, length);
        }

        private void SpellChecker_EndOfText(object sender, EventArgs e)
        {
            Console.WriteLine("EndOfText");
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void Paste()
        {
            DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Rtf);
            if (RichTextbox_Content.CanPaste(myFormat))
            {
                RichTextbox_Content.Paste(myFormat);
            }
            else
            {
                RichTextbox_Content.Paste();
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.SelectedText = string.Empty;
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.SelectAll();
            RichTextbox_Content.Focus();
        }

        private void RightToLeftToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rightToLeftToolStripMenuItem.Checked)
            {
                RichTextbox_Content.RightToLeft = RightToLeft.Yes;
            }
            else
            {
                RichTextbox_Content.RightToLeft = RightToLeft.No;
            }
        }

        private void CutToolStripButton_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.Cut();
        }

        private void CopyToolStripButton_Click(object sender, EventArgs e)
        {
            RichTextbox_Content.Copy();
        }

        private void PasteToolStripButton_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void RichTextbox_Content_SelectionChanged(object sender, EventArgs e)
        {
            if (RichTextbox_Content.SelectionFont != null)
            {
                BoldToolStripButton.Checked = RichTextbox_Content.SelectionFont.Bold;
                UnderlineToolStripButton.Checked = RichTextbox_Content.SelectionFont.Underline;
                ItalicToolStripButton.Checked = RichTextbox_Content.SelectionFont.Italic;
            }
            LeftToolStripButton.Checked = RichTextbox_Content.SelectionAlignment == HorizontalAlignment.Left;
            CenterToolStripButton.Checked = RichTextbox_Content.SelectionAlignment == HorizontalAlignment.Center;
            RightToolStripButton.Checked = RichTextbox_Content.SelectionAlignment == HorizontalAlignment.Right;
            BulletsToolStripButton.Checked = RichTextbox_Content.SelectionBullet;
        }

        private void UnderlineToolStripButton_Click(object sender, EventArgs e)
        {

        }
    }
}
