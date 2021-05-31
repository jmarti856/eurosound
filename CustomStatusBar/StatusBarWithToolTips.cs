using System.Windows.Forms;

namespace CustomStatusBar
{
    public partial class StatusBarToolTips : StatusBar
    {
        private bool ToolTipMode;
        private string ProgramStatus;

        public StatusBarToolTips()
        {
            InitializeComponent();
        }

        public void ToolTipModeStatus(bool Action)
        {
            ToolTipMode = Action;
            if (ToolTipMode)
            {
                if (!(Disposing || IsDisposed))
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            StatusPanel.Text = "";
                        });
                    }
                    else
                    {
                        StatusPanel.Text = "";
                    }
                }
            }
            else
            {
                ShowProgramStatus();
            }
        }

        public void ShowToolTipText(string TextToShow)
        {
            if (ToolTipMode)
            {
                if (!(Disposing || IsDisposed))
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            StatusPanel.Text = TextToShow;
                        });
                    }
                    else
                    {
                        StatusPanel.Text = TextToShow;
                    }
                }
            }
        }

        public void ShowProgramStatus(string TextToShow)
        {
            ProgramStatus = TextToShow;
            if (!ToolTipMode)
            {
                if (!(Disposing || IsDisposed))
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            StatusPanel.Text = ProgramStatus;
                        });
                    }
                    else
                    {
                        if (StatusPanel != null)
                        {
                            StatusPanel.Text = ProgramStatus;
                        }
                    }
                }
            }
        }

        public void ShowProgramStatus()
        {
            if (!ToolTipMode)
            {
                if (!(Disposing || IsDisposed))
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            StatusPanel.Text = ProgramStatus;
                        });
                    }
                    else
                    {
                        StatusPanel.Text = ProgramStatus;
                    }
                }
            }
        }
    }
}
