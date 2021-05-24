using System;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_EditingOptions : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_EditingOptions()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_EditingOptions_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Checkbox_SortNodes.Checked = ((Frm_MainPreferences)OpenForm).AutomaticallySortNodesTEMPORAL;
            Checkbox_UseExtendedColorPicker.Checked = ((Frm_MainPreferences)OpenForm).UseExtendedColorPickerTEMPORAL;
        }

        private void Frm_EditingOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).AutomaticallySortNodesTEMPORAL = Checkbox_SortNodes.Checked;
            ((Frm_MainPreferences)OpenForm).UseExtendedColorPickerTEMPORAL = Checkbox_UseExtendedColorPicker.Checked;
        }
    }
}
