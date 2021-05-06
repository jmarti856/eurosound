using EuroSound_Application.ApplicationRegistryFunctions;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.WebBrowser
{
    public partial class EuroSound_WebBrowser : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private string BrowserLink = string.Empty;
        private bool FormIsOpened;

        public EuroSound_WebBrowser(string LinkToOpen, string Title)
        {
            InitializeComponent();
            WebBrowser.StatusTextChanged += (se, ev) => UpdateStatusBar();

            Text = Title;
            BrowserLink = LinkToOpen;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void EuroSound_WebBrowser_Load(object sender, EventArgs e)
        {
            //Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Update Var
            FormIsOpened = true;

            //Load Last State
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                if (Convert.ToBoolean(WindowStateConfig.GetValue("WBrowser_IsIconic", 0)))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (Convert.ToBoolean(WindowStateConfig.GetValue("WBrowser_IsMaximized", 0)))
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("WBrowser_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("WBrowser_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("WBrowser_Width", 983));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("WBrowser_Height", 616));
                WindowStateConfig.Close();
            }

            //Open URL
            WebBrowser.Url = new Uri(BrowserLink);
        }

        private void EuroSound_WebBrowser_Shown(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void EuroSound_WebBrowser_Enter(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void EuroSound_WebBrowser_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void EuroSound_WebBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save Window position
            WindowsRegistryFunctions.SaveWindowState("WBrowser", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized, 0);

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            //Update Title Bar
            MdiParent.Text = "EuroSound";

            //Update Var
            FormIsOpened = false;
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void UpdateStatusBar()
        {
            if (FormIsOpened)
            {
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(WebBrowser.StatusText);
            }
            else
            {
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }
    }
}
