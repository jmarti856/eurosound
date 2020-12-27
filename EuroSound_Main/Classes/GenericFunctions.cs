using CustomStatusBar;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal static class GenericFunctions
    {
        internal static ResourceManager ResourcesManager;
        internal static StatusBarToolTips ParentFormStatusBar;

        internal static string TruncateLongString(string str, int maxLenght)
        {
            if (str.Length > maxLenght)
            {
                str = str.Substring(0, maxLenght) + "...";
            }

            return str;
        }

        internal static void SetCurrentFileLabel(string text)
        {
            if (ParentFormStatusBar.Visible && ParentFormStatusBar != null)
            {
                ParentFormStatusBar.Invoke((MethodInvoker)delegate
                {
                    ParentFormStatusBar.Panels["File"].Text = text;
                });
            }
            text = null;
        }

        internal static string CalculateMD5(string filename)
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

        internal static bool FileIsModified(string StoredMD5Hash, string FileToCheck)
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

        internal static int GetColorFromColorPicker()
        {
            int SelectedColor = -1;

            WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
            using (ColorDialog ColorDiag = new ColorDialog())
            {
                ColorDiag.AllowFullOpen = true;
                ColorDiag.FullOpen = true;
                ColorDiag.CustomColors = WRegistryFunctions.SetCustomColors();
                if (ColorDiag.ShowDialog() == DialogResult.OK)
                {
                    SelectedColor = ColorDiag.Color.ToArgb();
                    WRegistryFunctions.SaveCustomColors(ColorDiag.CustomColors);
                }
            }

            return SelectedColor;
        }

        internal static Form GetFormByName(string FormName, string tag)
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

        //ChangeLabel Value Text
        internal static string OpenFileBrowser(string BrowserFilter, int SelectedIndexFilter)
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

        internal static string OpenInputBox(string Text, string Title)
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

        internal static string SaveFileBrowser(string Filter, int SelectedIndexFilter, bool RestoreDirectory, string Name)
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

        internal static void ShowErrorsAndWarningsList(List<string> ListToPrint, string FormTitle)
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


        internal static uint GetSoundID(ProjectFile FileProperties)
        {
            uint index;

            index = (FileProperties.SoundID += 1);

            return index;
        }

        internal static uint GetStreamedSoundID(ProjectFile FileProperties)
        {
            uint index;

            index = (FileProperties.StreamedSoundID += 1);

            return index;
        }
        internal static int CountNumberOfSamples(Dictionary<uint, EXSound> SoundsList)
        {
            int Counter = 0;
            foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Value.Samples)
                {
                    if (!Sample.IsStreamed)
                    {
                        Counter++;
                    }
                }
            }
            return Counter;
        }
    }
}