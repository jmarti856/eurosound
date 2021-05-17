using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_System : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public Frm_System()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_System_Load(object sender, EventArgs e)
        {
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            CheckBox_UseSystemTray.Checked = ((Frm_MainPreferences)OpenForm).UseSystemTrayTEMPORAL;
        }

        private void Frm_System_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).UseSystemTrayTEMPORAL = CheckBox_UseSystemTray.Checked;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void ButtonRegister_FileTypes_Click(object sender, EventArgs e)
        {
            try
            {
                SetAssociation(".esf", "EuroSound.exe", Application.ExecutablePath);
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("EuroSoundFile-TypesRegisterdOK"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("EuroSoundFile-TypesRegisterdError"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
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
