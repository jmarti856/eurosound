using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_TreeViewPrefs : Form
    {
        public Frm_TreeViewPrefs()
        {
            InitializeComponent();
        }

        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            Textbox_SelectedFont.Text = GlobalPreferences.SelectedFont;
            Numeric_TreeViewIndent.Value = GlobalPreferences.TreeViewIndent;
            CheckBox_ShowLines.Checked = GlobalPreferences.ShowLines;
            Checkbox_ShowRootLines.Checked = GlobalPreferences.ShowRootLines;
        }

        private void Textbox_SelectedFont_Click(object sender, EventArgs e)
        {
            FontConverter cvt = new FontConverter();
            FontDialogTreeView.Font = cvt.ConvertFromString(Textbox_SelectedFont.Text) as Font;

            if (FontDialogTreeView.ShowDialog() != DialogResult.Cancel)
            {
                Textbox_SelectedFont.Text = cvt.ConvertToString(FontDialogTreeView.Font);
            }
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalPreferences.SelectedFontTEMPORAL = Textbox_SelectedFont.Text;
            GlobalPreferences.ShowLinesTEMPORAL = CheckBox_ShowLines.Checked;
            GlobalPreferences.ShowRootLinesTEMPORAL = Checkbox_ShowRootLines.Checked;
            GlobalPreferences.TreeViewIndentTEMPORAL = int.Parse(Numeric_TreeViewIndent.Value.ToString());
        }
    }
}
