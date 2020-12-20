using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public static class GenericFunctions
    {
        public static string CurrentStatus;
        public static ToolStripLabel FileNameLabel;
        public static ResourceManager ResourcesManager;
        private static ToolStripLabel ProgramStatusLabel;
        private static StatusStrip StatusBar;

        public static string CalculateMD5(string filename)
        {
            string MD5hash = string.Empty;

            if (File.Exists(filename))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] buffer = md5.ComputeHash(File.ReadAllBytes(filename));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("x2"));
                }
                MD5hash = sb.ToString();

                //Clear and dispose
                md5.Clear();
                md5.Dispose();

                //Clear and dispose
                sb.Clear();
            }

            return MD5hash;
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

        public static Color GetColorFromColorPicker()
        {
            Color PickedColor = Color.Black;
            ColorDialog ColorDiag = new ColorDialog() { AllowFullOpen = true, FullOpen = true };
            ColorDiag.CustomColors = WindowsRegistryFunctions.SetCustomColors();
            if (ColorDiag.ShowDialog() == DialogResult.OK)
            {
                PickedColor = ColorDiag.Color;
                WindowsRegistryFunctions.SaveCustomColors(ColorDiag.CustomColors);
            }
            ColorDiag.Dispose();

            return PickedColor;
        }

        public static Form GetFormByName(string FormName, string tag)
        {
            Form Results = null;

            FormCollection FormsToCheck = Application.OpenForms;
            for (int i = 0; i < FormsToCheck.Count; i++)
            {
                if (FormsToCheck[i].Name.Equals(FormName))
                {
                    if (FormsToCheck[i].Tag.Equals(tag))
                    {
                        Results = FormsToCheck[i];
                        break;
                    }
                }
            }

            return Results;
        }

        public static void GetStatusBarControls(StatusStrip v_StatusBar, ToolStripLabel v_ProgramStatusLabel, ToolStripLabel v_FileNameLabel)
        {
            ProgramStatusLabel = v_ProgramStatusLabel;
            FileNameLabel = v_FileNameLabel;
            StatusBar = v_StatusBar;
        }

        //ChangeLabel Value Text
        public static string OpenFileBrowser(string BrowserFilter, int SelectedIndexFilter)
        {
            string FilePath = string.Empty;

            OpenFileDialog FileBrowser = new OpenFileDialog
            {
                Filter = BrowserFilter + "|All files(*.*)|*.*",
                FilterIndex = SelectedIndexFilter
            };

            if (FileBrowser.ShowDialog() == DialogResult.OK)
            {
                FilePath = FileBrowser.FileName;
            }
            FileBrowser.Dispose();

            return FilePath;
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
            dlg.Dispose();

            return SampleName;
        }

        public static string SaveFileBrowser(string Filter, int SelectedIndexFilter, bool RestoreDirectory, string Name)
        {
            string SelectedPath = string.Empty;

            SaveFileDialog SaveFile = new SaveFileDialog
            {
                Filter = Filter + "|All files(*.*)|*.*",
                FilterIndex = SelectedIndexFilter,
                RestoreDirectory = RestoreDirectory,
                FileName = Name
            };

            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                SelectedPath = SaveFile.FileName;
            }
            SaveFile.Dispose();

            return SelectedPath;
        }
        public static void SetCurrentFileLabel(string text)
        {
            if (StatusBar.Visible && StatusBar != null)
            {
                StatusBar.Invoke((MethodInvoker)delegate
                {
                    FileNameLabel.Text = text;
                });
            }
            text = null;
        }

        public static void SetProgramStateShowToStatusBar(string NewStatus)
        {
            if (!NewStatus.Equals("CurrentStatus"))
            {
                CurrentStatus = NewStatus;
            }
            if (StatusBar.Visible)
            {
                StatusBarSetText(CurrentStatus);
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

        public static void StatusBarTutorialMode(bool MenuStripOpened)
        {
            if (StatusBar.Visible)
            {
                if (MenuStripOpened)
                {
                    StatusBarSetText("");
                }
                else
                {
                    StatusBarSetText(CurrentStatus);
                }
            }
        }

        public static void StatusBarTutorialModeShowText(string TextToDisplay)
        {
            if (StatusBar.Visible)
            {
                StatusBarSetText(TextToDisplay);
            }
        }

        private static void StatusBarSetText(string TextToDisplay)
        {
            if (StatusBar.Visible && StatusBar != null)
            {
                StatusBar.Invoke((MethodInvoker)delegate
                {
                    ProgramStatusLabel.Text = TextToDisplay;
                });
            }
            TextToDisplay = null;
        }
    }
}