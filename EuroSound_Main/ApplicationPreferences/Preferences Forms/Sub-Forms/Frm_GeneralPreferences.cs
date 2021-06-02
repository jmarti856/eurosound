using EuroSound_Application.Clases;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_General : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;
        private bool MouseDownButtonColor, MouseDownButtonBackColor;

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
            CheckBox_ReloadLastESF.Checked = ((Frm_MainPreferences)OpenForm).LoadLastLoadedESFTEMPORAL;
            CheckBox_UseThreading.Checked = ((Frm_MainPreferences)OpenForm).UseThreadingWhenLoadTEMPORAL;
            checkBox_VisualStyles.Checked = ((Frm_MainPreferences)OpenForm).EnableAppVisualStylesTEMPORAL;
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).ColorWavesControlTEMPORAL = Button_WavesColorControl.BackColor.ToArgb();
            ((Frm_MainPreferences)OpenForm).BackColorWavesControlTEMPORAL = Button_WavesBackColor.BackColor.ToArgb();
            ((Frm_MainPreferences)OpenForm).TV_IgnoreStlyesFromESFTEMPORAL = CheckBox_IgnoreLookTree.Checked;
            ((Frm_MainPreferences)OpenForm).LoadLastLoadedESFTEMPORAL = CheckBox_ReloadLastESF.Checked;
            ((Frm_MainPreferences)OpenForm).UseThreadingWhenLoadTEMPORAL = CheckBox_UseThreading.Checked;
            ((Frm_MainPreferences)OpenForm).EnableAppVisualStylesTEMPORAL = checkBox_VisualStyles.Checked;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_WavesColorControl_Click(object sender, EventArgs e)
        {
            //Update Boolean
            MouseDownButtonColor = false;

            //Open Color Dialog
            int SelectedColor = BrowsersAndDialogs.ColorPickerDialog(Button_WavesColorControl.BackColor);
            if (SelectedColor != -1)
            {
                Button_WavesColorControl.BackColor = Color.FromArgb(SelectedColor);
            }
        }

        private void Button_WavesColorControl_Paint(object sender, PaintEventArgs e)
        {
            //Draw 3d border
            Rectangle borderRectangle = Button_WavesColorControl.ClientRectangle;
            if (MouseDownButtonColor)
            {
                ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, Border3DStyle.Sunken);
            }
            else
            {
                ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, Border3DStyle.Raised);
            }
        }
        private void Button_WavesColorControl_MouseUp(object sender, MouseEventArgs e)
        {
            //Update Boolean
            MouseDownButtonColor = false;
        }

        private void Button_WavesColorControl_MouseDown(object sender, MouseEventArgs e)
        {
            //Update Boolean
            MouseDownButtonColor = true;
        }

        private void Button_WavesBackColor_Click(object sender, EventArgs e)
        {
            //Update Boolean
            MouseDownButtonBackColor = false;

            //Open Color Dialog
            int SelectedColor = BrowsersAndDialogs.ColorPickerDialog(Button_WavesBackColor.BackColor);
            if (SelectedColor != -1)
            {
                Button_WavesBackColor.BackColor = Color.FromArgb(SelectedColor);
            }
        }

        private void Button_WavesBackColor_Paint(object sender, PaintEventArgs e)
        {
            //Draw 3d border
            Rectangle borderRectangle = Button_WavesBackColor.ClientRectangle;
            if (MouseDownButtonBackColor)
            {
                ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, Border3DStyle.Sunken);
            }
            else
            {
                ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, Border3DStyle.Raised);
            }
        }

        private void Button_WavesBackColor_MouseUp(object sender, MouseEventArgs e)
        {
            //Update Boolean
            MouseDownButtonBackColor = false;
        }

        private void checkBox_VisualStyles_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_VisualStyles.Checked)
            {
                if (Application.VisualStyleState != VisualStyleState.ClientAndNonClientAreasEnabled)
                {
                    Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled;
                }
            }
            else
            {
                if (Application.VisualStyleState != VisualStyleState.NonClientAreaEnabled)
                {
                    Application.VisualStyleState = VisualStyleState.NonClientAreaEnabled;
                }
            }
        }

        private void Button_WavesBackColor_MouseDown(object sender, MouseEventArgs e)
        {
            //Update Boolean
            MouseDownButtonBackColor = true;
        }
    }
}
