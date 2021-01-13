using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_General : Form
    {
        private Form OpenForm;

        public Frm_General()
        {
            InitializeComponent();
        }

        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            Textbox_SFX_OutputPath.Text = ((Frm_MainPreferences)OpenForm).SFXOutputPathTEMPORAL;
            Textbox_MusicOutputPath.Text = ((Frm_MainPreferences)OpenForm).MusicOutputPathTEMPORAL;
            Button_WavesColorControl.BackColor = Color.FromArgb(((Frm_MainPreferences)OpenForm).ColorWavesControlTEMPORAL);
            Button_WavesBackColor.BackColor = Color.FromArgb(((Frm_MainPreferences)OpenForm).BackColorWavesControlTEMPORAL);
        }

        private void Button_ChooseSFX_OutputPath_Click(object sender, EventArgs e)
        {
            string SelectedPath = GenericFunctions.OpenFolderBrowser();
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                Textbox_SFX_OutputPath.Text = SelectedPath;
            }
        }

        private void Button_MusicOutputPath_Click(object sender, EventArgs e)
        {
            string SelectedPath = GenericFunctions.OpenFolderBrowser();
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                Textbox_MusicOutputPath.Text = SelectedPath;
            }
        }

        private void Button_WavesColorControl_Click(object sender, EventArgs e)
        {
            int SelectedColor = GenericFunctions.GetColorFromColorPicker();
            if (SelectedColor != -1)
            {
                Button_WavesColorControl.BackColor = Color.FromArgb(SelectedColor);
            }
        }

        private void Button_WavesBackColor_Click(object sender, EventArgs e)
        {
            int SelectedColor = GenericFunctions.GetColorFromColorPicker();
            if (SelectedColor != -1)
            {
                Button_WavesBackColor.BackColor = Color.FromArgb(SelectedColor);
            }
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).SFXOutputPathTEMPORAL = Textbox_SFX_OutputPath.Text;
            ((Frm_MainPreferences)OpenForm).MusicOutputPathTEMPORAL = Textbox_MusicOutputPath.Text;
            ((Frm_MainPreferences)OpenForm).ColorWavesControlTEMPORAL = Button_WavesColorControl.BackColor.ToArgb();
            ((Frm_MainPreferences)OpenForm).BackColorWavesControlTEMPORAL = Button_WavesBackColor.BackColor.ToArgb();
        }
    }
}
