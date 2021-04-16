using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_General : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_General()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Button_WavesColorControl.BackColor = Color.FromArgb(((Frm_MainPreferences)OpenForm).ColorWavesControlTEMPORAL);
            Button_WavesBackColor.BackColor = Color.FromArgb(((Frm_MainPreferences)OpenForm).BackColorWavesControlTEMPORAL);
            CheckBox_IgnoreLookTree.Checked = ((Frm_MainPreferences)OpenForm).TV_IgnoreStlyesFromESFTEMPORAL;
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).ColorWavesControlTEMPORAL = Button_WavesColorControl.BackColor.ToArgb();
            ((Frm_MainPreferences)OpenForm).BackColorWavesControlTEMPORAL = Button_WavesBackColor.BackColor.ToArgb();
            ((Frm_MainPreferences)OpenForm).TV_IgnoreStlyesFromESFTEMPORAL = CheckBox_IgnoreLookTree.Checked;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_WavesColorControl_Click(object sender, EventArgs e)
        {
            int SelectedColor = GenericFunctions.GetColorFromColorPicker(Button_WavesColorControl.BackColor);
            if (SelectedColor != -1)
            {
                Button_WavesColorControl.BackColor = Color.FromArgb(SelectedColor);
            }
        }

        private void Button_WavesBackColor_Click(object sender, EventArgs e)
        {
            int SelectedColor = GenericFunctions.GetColorFromColorPicker(Button_WavesBackColor.BackColor);
            if (SelectedColor != -1)
            {
                Button_WavesBackColor.BackColor = Color.FromArgb(SelectedColor);
            }
        }
    }
}
