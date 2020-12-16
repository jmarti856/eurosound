using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Application
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
            Textbox_OutputSelectedPath.Text = GlobalPreferences.SFXOutputPath;
        }

        private void Button_Choose_Click(object sender, EventArgs e)
        {
            if (FolderBrowser_OutputPath.ShowDialog() == DialogResult.OK)
            {
                Textbox_OutputSelectedPath.Text = FolderBrowser_OutputPath.SelectedPath;
            }
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalPreferences.SFXOutputPathTEMPORAL = Textbox_OutputSelectedPath.Text;
        }

        private void ButtonRegister_FileTypes_Click(object sender, EventArgs e)
        {
            try
            {
                SetAssociation(".esf", "EuroSound.exe", Application.ExecutablePath, "EuroSound File");
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("EuroSoundFile-TypesRegisterdOK"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("EuroSoundFile-TypesRegisterdError"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void SetAssociation(string Extension, string KeyName, string OpenWith, string FileDescription)
        {
            RegistryKey FileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + Extension);
            RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\" + KeyName);
            RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + Extension);

            FileReg.CreateSubKey("shell\\open\\command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
            AppReg.CreateSubKey("shell\\open\\command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
            AppAssoc.CreateSubKey("UserChoice").SetValue("Progid", KeyName, RegistryValueKind.String);

            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
