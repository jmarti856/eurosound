using CustomStatusBar;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.SoundBanksEditor;
using NAudio.Wave;
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
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] buffer = md5.ComputeHash(File.ReadAllBytes(filename));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        sb.Append(buffer[i].ToString("x2"));
                    }
                    MD5hash = sb.ToString();
                    //Clear and dispose
                    sb.Clear();
                }
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
                    if (FormsToCheck[i].Tag != null)
                    {
                        if (FormsToCheck[i].Tag.Equals(tag))
                        {
                            Results = FormsToCheck[i];
                            break;
                        }
                    }
                }
            }

            return Results;
        }

        //ChangeLabel Value Text
        internal static string OpenFileBrowser(string BrowserFilter, int SelectedIndexFilter)
        {
            string FilePath = string.Empty;

            using (OpenFileDialog FileBrowser = new OpenFileDialog())
            {
                FileBrowser.Filter = BrowserFilter + "|All files(*.*)|*.*";
                FileBrowser.FilterIndex = SelectedIndexFilter;

                if (FileBrowser.ShowDialog() == DialogResult.OK)
                {
                    FilePath = FileBrowser.FileName;
                }
            }

            return FilePath;
        }

        internal static string OpenInputBox(string Text, string Title)
        {
            string SampleName = string.Empty;

            /*Ask user for a name*/
            using (EuroSound_InputBox dlg = new EuroSound_InputBox(Text, Title))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SampleName = dlg.Result;
                }
            }

            return SampleName;
        }

        internal static string SaveFileBrowser(string Filter, int SelectedIndexFilter, bool RestoreDirectory, string Name)
        {
            string SelectedPath = string.Empty;

            using (SaveFileDialog SaveFile = new SaveFileDialog())
            {
                SaveFile.Filter = Filter + "|All files(*.*)|*.*";
                SaveFile.FilterIndex = SelectedIndexFilter;
                SaveFile.RestoreDirectory = RestoreDirectory;
                if (!string.IsNullOrEmpty(Name))
                {
                    SaveFile.FileName = Name;
                }

                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    SelectedPath = SaveFile.FileName;
                }
            }

            return SelectedPath;
        }

        internal static string OpenFolderBrowser()
        {
            string SelectedPath = string.Empty;

            using (FolderBrowserDialog OpenFolder = new FolderBrowserDialog())
            {
                if (OpenFolder.ShowDialog() == DialogResult.OK)
                {
                    SelectedPath = OpenFolder.SelectedPath;
                }
            }

            return SelectedPath;
        }

        internal static void ShowErrorsAndWarningsList(List<string> ListToPrint, string FormTitle)
        {
            //Show Import results
            using (EuroSound_ErrorsAndWarningsList ImportResults = new EuroSound_ErrorsAndWarningsList(ListToPrint))
            {
                ImportResults.Text = FormTitle;
                ImportResults.ShowInTaskbar = false;
                ImportResults.TopMost = true;

                ImportResults.ShowDialog();
            }
        }

        internal static uint GetNewObjectID(ProjectFile FileProperties)
        {
            uint index;

            index = (FileProperties.SoundID += 1);

            return index;
        }

        internal static int CountNumberOfSamples(Dictionary<uint, EXSound> SoundsList)
        {
            int Counter = 0;
            foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
            {
                foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                {
                    if (!Sample.Value.IsStreamed)
                    {
                        Counter++;
                    }
                }
            }
            return Counter;
        }

        internal static string UpdateProjectFormText(string LoadedFile, string ProjectName)
        {
            string Text;

            if (string.IsNullOrEmpty(LoadedFile))
            {
                Text = ProjectName;
            }
            else
            {
                Text = Path.GetFileName(LoadedFile) + " - " + Path.GetDirectoryName(LoadedFile);
            }

            return Text;
        }

        internal static bool AudioIsValid(string FilePath, int NumChannels, int frequency)
        {
            bool AudioIsCorrect = false;
            using (WaveFileReader AudioReader = new WaveFileReader(FilePath))
            {
                int Rate, Bits, Channels;
                string Encoding;

                Rate = AudioReader.WaveFormat.SampleRate;
                Bits = AudioReader.WaveFormat.BitsPerSample;
                Encoding = AudioReader.WaveFormat.Encoding.ToString();
                Channels = AudioReader.WaveFormat.Channels;

                if (Encoding.Equals("Pcm") && Bits == 16 && Rate == frequency && Channels == NumChannels)
                {
                    AudioIsCorrect = true;
                }

                AudioReader.Close();
            }

            return AudioIsCorrect;
        }

        internal static void CreateTemporalFolder()
        {
            //Create folder in %temp%
            if (!Directory.Exists(Path.GetTempPath() + "EuroSound"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "EuroSound");
            }
        }

        internal static bool ClearTemporalFiles()
        {
            bool FilesRemoved = false;

            //Delete Temp Files from session if exists
            if (Directory.Exists(Path.GetTempPath() + @"EuroSound\"))
            {
                /*Update Status Bar*/
                ParentFormStatusBar.ShowProgramStatus(ResourcesManager.GetString("StatusBar_RemovingTempFiles"));

                //Get temporal files
                DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + @"EuroSound\");
                foreach (FileInfo file in di.GetFiles())
                {
                    FilesRemoved = true;
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    FilesRemoved = true;
                    dir.Delete(true);
                }

                //Update Status Bar
                ParentFormStatusBar.ShowProgramStatus(ResourcesManager.GetString("StatusBar_Status_Ready"));
            }

            return FilesRemoved;
        }
    }
}