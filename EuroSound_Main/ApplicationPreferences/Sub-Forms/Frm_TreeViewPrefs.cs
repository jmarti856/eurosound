using EuroSound_Application.ApplicationPreferences;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
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
            Form OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            ((Frm_MainPreferences)OpenForm).SelectedFontTEMPORAL = Textbox_SelectedFont.Text;
            ((Frm_MainPreferences)OpenForm).ShowLinesTEMPORAL = CheckBox_ShowLines.Checked;
            ((Frm_MainPreferences)OpenForm).ShowRootLinesTEMPORAL = Checkbox_ShowRootLines.Checked;
            ((Frm_MainPreferences)OpenForm).TreeViewIndentTEMPORAL = int.Parse(Numeric_TreeViewIndent.Value.ToString());
        }
    }
}