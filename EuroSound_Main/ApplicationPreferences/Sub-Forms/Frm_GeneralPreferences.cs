using EuroSound_Application.ApplicationPreferences;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_General : Form
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public Frm_General()
        {
            InitializeComponent();
        }

        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            Textbox_SFX_OutputPath.Text = GlobalPreferences.SFXOutputPath;
            Textbox_MusicOutputPath.Text = GlobalPreferences.MusicOutputPath;
            Button_WavesColorControl.BackColor = Color.FromArgb(GlobalPreferences.ColorWavesControl);
            Button_WavesBackColor.BackColor = Color.FromArgb(GlobalPreferences.BackColorWavesControl);
        }

        private void Button_ChooseSFX_OutputPath_Click(object sender, EventArgs e)
        {
            if (FolderBrowser_OutputPath.ShowDialog() == DialogResult.OK)
            {
                Textbox_SFX_OutputPath.Text = FolderBrowser_OutputPath.SelectedPath;
            }
        }

        private void Button_MusicOutputPath_Click(object sender, EventArgs e)
        {
            if (FolderBrowser_OutputPath.ShowDialog() == DialogResult.OK)
            {
                Textbox_MusicOutputPath.Text = FolderBrowser_OutputPath.SelectedPath;
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
            Form OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            ((Frm_MainPreferences)OpenForm).SFXOutputPathTEMPORAL = Textbox_SFX_OutputPath.Text;
            ((Frm_MainPreferences)OpenForm).MusicOutputPathTEMPORAL = Textbox_MusicOutputPath.Text;
            ((Frm_MainPreferences)OpenForm).ColorWavesControlTEMPORAL = Button_WavesColorControl.BackColor.ToArgb();
            ((Frm_MainPreferences)OpenForm).BackColorWavesControlTEMPORAL = Button_WavesBackColor.BackColor.ToArgb();
        }

        private void ButtonRegister_FileTypes_Click(object sender, EventArgs e)
        {
            try
            {
                SetAssociation(".esf", "EuroSound.exe", Application.ExecutablePath);
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("EuroSoundFile-TypesRegisterdOK"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("EuroSoundFile-TypesRegisterdError"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SetAssociation(string Extension, string KeyName, string OpenWith)
        {
            using (RegistryKey FileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + Extension, RegistryKeyPermissionCheck.ReadWriteSubTree))
            using (RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\" + KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
            using (RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + Extension, RegistryKeyPermissionCheck.Default))
            {
                FileReg.CreateSubKey("shell\\open\\command", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
                AppReg.CreateSubKey("shell\\open\\command", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
                AppAssoc.CreateSubKey("OpenWithList", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("a", KeyName, RegistryValueKind.String);
                AppAssoc.CreateSubKey("OpenWithList", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("b", "a", RegistryValueKind.String);
                AppAssoc.CreateSubKey("OpenWithList", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("MRUList", "ab", RegistryValueKind.String);
                AppAssoc.CreateSubKey("OpenWithProgids", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(Extension, new byte[0], RegistryValueKind.None);
                AppAssoc.CreateSubKey("UserChoice", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue("Progid", "Applications\\" + KeyName, RegistryValueKind.String);

                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
