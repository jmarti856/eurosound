using SoundBanks_Editor;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FunctionsLibrary
{
    public static class GenericFunctions
    {
        private static ToolStripLabel ProgramStatusLabel;
        public static ToolStripLabel FileNameLabel;
        private static StatusStrip StatusBar;
        public static string CurrentStatus;

        //ChangeLabel Value Text
        public static void GetProgramStatusLabel(ToolStripLabel ControlLabel, ToolStripLabel FileName, StatusStrip StatusBarControl)
        {
            ProgramStatusLabel = ControlLabel;
            FileNameLabel = FileName;
            StatusBar = StatusBarControl;
        }

        public static string OpenFileBrowser(string BrowserFilter, int SelectedIndexFilter)
        {
            string FilePath = string.Empty;

            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Filter = BrowserFilter,
                FilterIndex = SelectedIndexFilter
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                FilePath = FileBrowser.FileName;
            }
            FileBrowser.Dispose();

            return FilePath;
        }

        public static string SaveFileBrowser(string Filter, int SelectedIndexFilter, bool RestoreDirectory, string Name)
        {
            string SelectedPath = string.Empty;

            SaveFileDialog SaveFile = new SaveFileDialog
            {
                Filter = Filter,
                FilterIndex = SelectedIndexFilter,
                RestoreDirectory = RestoreDirectory,
                FileName = Name
            };

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                SelectedPath = SaveFile.FileName;
            }

            return SelectedPath;
        }

        public static string CalculateMD5(string filename)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] buffer = md5.ComputeHash(File.ReadAllBytes(filename));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static bool FileIsModified(string StoredMD5Hash, string FileToCheck)
        {
            string hash;
            bool Modified = true;

            hash = CalculateMD5(FileToCheck);

            if (hash.Equals(StoredMD5Hash))
            {
                Modified = false;
            }

            return Modified;
        }

        public static string OpenInputBox(string Text, string Title)
        {
            string SampleName = string.Empty;

            /*Ask user for a name*/
            EuroSound_InputBox dlg = new EuroSound_InputBox(Text, Title);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SampleName = dlg.Result;
            }

            return SampleName;
        }

        public static void SetCurrentFileLabel(string text)
        {
            FileNameLabel.Text = text;
        }

        public static void SetProgramStateShowToStatusBar(string NewStatus)
        {
            if (!NewStatus.Equals("CurrentStatus"))
            {
                CurrentStatus = NewStatus;
            }
            if (StatusBar.Visible)
            {
                ProgramStatusLabel.Text = CurrentStatus;
            }
        }

        public static void StatusBarTutorialModeShowText(string TextToDisplay)
        {
            if (StatusBar.Visible)
            {
                ProgramStatusLabel.Text = TextToDisplay;
            }
        }

        public static void StatusBarTutorialMode(bool MenuStripOpened)
        {
            if (StatusBar.Visible)
            {
                if (MenuStripOpened)
                {
                    ProgramStatusLabel.Text = "";
                }
                else
                {
                    ProgramStatusLabel.Text = CurrentStatus;
                }
            }
        }

        public static void ShowErrorsAndWarningsList(List<string> ListToPrint, string FormTitle)
        {
            //Show Import results
            EuroSound_ErrorsAndWarningsList ImportResults = new EuroSound_ErrorsAndWarningsList(ListToPrint)
            {
                Text = FormTitle,
                ShowInTaskbar = false,
                TopMost = true
            };
            ImportResults.ShowDialog();
            ImportResults.Dispose();
        }
    }
}