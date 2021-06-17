using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_TreeViewPrefs : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_TreeViewPrefs()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Textbox_SelectedFont.Text = ((Frm_MainPreferences)OpenForm).SelectedFontTEMPORAL;
            Numeric_TreeViewIndent.Value = ((Frm_MainPreferences)OpenForm).TreeViewIndentTEMPORAL;
            Numeric_ItemHeight.Value = ((Frm_MainPreferences)OpenForm).TreeViewItemHeightTEMPORAL;
            CheckBox_ShowLines.Checked = ((Frm_MainPreferences)OpenForm).ShowLinesTEMPORAL;
            Checkbox_ShowRootLines.Checked = ((Frm_MainPreferences)OpenForm).ShowRootLinesTEMPORAL;
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).SelectedFontTEMPORAL = Textbox_SelectedFont.Text;
            ((Frm_MainPreferences)OpenForm).ShowLinesTEMPORAL = CheckBox_ShowLines.Checked;
            ((Frm_MainPreferences)OpenForm).ShowRootLinesTEMPORAL = Checkbox_ShowRootLines.Checked;
            ((Frm_MainPreferences)OpenForm).TreeViewIndentTEMPORAL = int.Parse(Numeric_TreeViewIndent.Value.ToString());
            ((Frm_MainPreferences)OpenForm).TreeViewItemHeightTEMPORAL = int.Parse(Numeric_ItemHeight.Value.ToString());
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Textbox_SelectedFont_Click(object sender, EventArgs e)
        {
            FontConverter cvt = new FontConverter();
            FontDialogTreeView.Font = cvt.ConvertFromString(Textbox_SelectedFont.Text) as Font;

            if (FontDialogTreeView.ShowDialog() != DialogResult.Cancel)
            {
                Textbox_SelectedFont.Text = cvt.ConvertToInvariantString(FontDialogTreeView.Font);
            }
        }
    }
}