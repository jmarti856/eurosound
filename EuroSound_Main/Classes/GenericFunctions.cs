﻿using CustomStatusBar;
using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsList;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.TreeViewLibraryFunctions;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal static class GenericFunctions
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        internal static ResourceManager ResourcesManager;
        internal static StatusBarToolTips ParentFormStatusBar;
        internal static Dictionary<string, string> AvailableProfiles = new Dictionary<string, string>();
        internal enum ESoundMarkers : uint
        {
            Start = 10,
            End = 9,
            Goto = 7,
            Loop = 6,
            Pause = 5,
            Jump = 0
        }

        internal static string TruncateLongString(string str, int maxLenght)
        {
            if (str.Length > maxLenght)
            {
                str = string.Join("", str.Substring(0, maxLenght), "...");
            }

            return str;
        }

        internal static void SetCurrentFileLabel(string TextToShow, string strPanelName)
        {
            if (ParentFormStatusBar.Visible && ParentFormStatusBar != null)
            {
                ParentFormStatusBar.Invoke((MethodInvoker)delegate
                {
                    ParentFormStatusBar.Panels[strPanelName].Text = TextToShow;
                });
            }
            TextToShow = null;
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

        internal static void ShowErrorsAndWarningsList(IEnumerable<string> ListToPrint, string FormTitle, Form OwnerForm)
        {
            //Show Import results
            using (EuroSound_ErrorsAndWarningsList ImportResults = new EuroSound_ErrorsAndWarningsList(ListToPrint))
            {
                ImportResults.Text = FormTitle;
                ImportResults.Owner = OwnerForm;
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
                Text = string.Join(" - ", new string[] { Path.GetFileName(LoadedFile), Path.GetDirectoryName(LoadedFile) });
            }

            return Text;
        }

        internal static bool AudioIsValid(string FilePath, int NumChannels, int Frequency)
        {
            bool AudioIsCorrect = false;

            try
            {
                using (WaveFileReader AudioReader = new WaveFileReader(FilePath))
                {
                    int Rate, Bits, Channels;
                    string Encoding;

                    Rate = AudioReader.WaveFormat.SampleRate;
                    Bits = AudioReader.WaveFormat.BitsPerSample;
                    Encoding = AudioReader.WaveFormat.Encoding.ToString();
                    Channels = AudioReader.WaveFormat.Channels;

                    if (Encoding.Equals("Pcm") && Bits == 16 && Rate == Frequency && Channels == NumChannels)
                    {
                        AudioIsCorrect = true;
                    }

                    AudioReader.Close();
                }
            }
            catch (FormatException)
            {

            }

            return AudioIsCorrect;
        }

        internal static void CreateTemporalFolder()
        {
            string TemporalFolderPath = Path.Combine(new string[] { Path.GetTempPath(), "EuroSound" });

            //Create folder in %temp%
            if (!Directory.Exists(TemporalFolderPath))
            {
                Directory.CreateDirectory(TemporalFolderPath);
            }
        }

        internal static void AddItemToListView(ListViewItem ItemToAdd, ListView ListToAddItem)
        {
            try
            {
                ListToAddItem.Invoke((MethodInvoker)delegate
                {
                    if (!ListToAddItem.IsDisposed)
                    {
                        ListToAddItem.Items.Add(ItemToAdd);
                    }
                });
            }
            catch (ObjectDisposedException)
            {
                // Ignore.  Control is disposed cannot update the UI.
            }
        }

        internal static float StringFloatToDouble(string Number)
        {
            float FinalNumber;
            string num;

            try
            {
                num = Number.Replace("f", string.Empty).Trim();
                FinalNumber = float.Parse(num, CultureInfo.GetCultureInfo("en-US"));
            }
            catch (FormatException)
            {
                FinalNumber = 0.0f;
            }

            return FinalNumber;
        }

        internal static string GetNextAvailableName(string BaseName, TreeView TreeViewControl)
        {
            string FinalName = BaseName;

            int Loops = 0;
            //Check there's no item with the same name
            while (TreeNodeFunctions.CheckIfNodeExistsByText(TreeViewControl, FinalName))
            {
                Loops++;
                FinalName = BaseName + Loops;
            }

            return FinalName;
        }

        internal static string GetEuroSoundVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        internal static void ExecuteCMDCommand(string Command)
        {
            using (Process ProcessExecuteCMD = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = Command
                };
                ProcessExecuteCMD.StartInfo = startInfo;
                ProcessExecuteCMD.Start();
            }
        }

        internal static void BuildSphinxFilelist()
        {
            if (File.Exists(GlobalPreferences.MkFileListPath))
            {
                ExecuteCMDCommand(@"/C " + GlobalPreferences.MkFileListPath + " " + GlobalPreferences.SelectedProfileName + " PC");
            }
            if (File.Exists(GlobalPreferences.MkFileList2Path))
            {
                ExecuteCMDCommand(@"/C " + GlobalPreferences.MkFileList2Path + " " + GlobalPreferences.SelectedProfileName + " PC");
            }
        }

        internal static void SaveAudio(AudioFunctions AudioLibrary, string FileName, int Frequency, int Bits, int Channels, byte[] PCM_Data)
        {
            string SavePath;

            SavePath = BrowsersAndDialogs.SaveFileBrowser("WAV Files (*.wav)|*.wav", 0, true, FileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                AudioLibrary.CreateWavFile(Frequency, Bits, Channels, PCM_Data, SavePath);
            }
        }

        internal static void AppendTextToRichTextBox(string TextToAppend, Color TextColor, RichTextBox RichTextBoxControl)
        {
            RichTextBoxControl.SelectionStart = RichTextBoxControl.TextLength;
            RichTextBoxControl.SelectionLength = 0;
            RichTextBoxControl.SelectionColor = TextColor;
            RichTextBoxControl.AppendText(TextToAppend);
            RichTextBoxControl.SelectionColor = RichTextBoxControl.ForeColor;
            RichTextBoxControl.ScrollToCaret();
        }

        internal static void SetLabelText(Label LabelToChange, string TextToShow)
        {
            LabelToChange.Invoke((MethodInvoker)delegate
            {
                LabelToChange.Text = TextToShow;
            });
        }

        //*===============================================================================================
        //* PROGRESS BAR FUNCTIONS
        //*===============================================================================================
        internal static void ProgressBarSetMaximum(ProgressBar BarToChange, int Maximum)
        {
            BarToChange.Invoke((MethodInvoker)delegate
            {
                BarToChange.Maximum = Maximum;
            });
        }

        internal static void ProgressBarValue(ProgressBar BarToChange, int value)
        {
            BarToChange.Invoke((MethodInvoker)delegate
            {
                BarToChange.Value = value;
            });
        }

        internal static void ProgressBarAddValue(ProgressBar BarToChange, int value)
        {
            BarToChange.Invoke((MethodInvoker)delegate
            {
                BarToChange.Value += value;
            });
        }

        //*===============================================================================================
        //* PROFILES FUNCTIONS
        //*===============================================================================================
        internal static int NumberOfChildForms()
        {
            int NumberOfChildForms;

            Form OpenForm = GetFormByName("Frm_EuroSound_Main", "Main");
            NumberOfChildForms = ((Frm_EuroSound_Main)OpenForm).MdiChildren.Length;

            return NumberOfChildForms;
        }

        internal static void CheckProfiles(string ProfileSavedInESF, string ProfileNameSavedInESF)
        {
            ProfilesFunctions ProfilesLoader = new ProfilesFunctions();
            string ProfilePath = string.Empty;
            string ProfileName = string.Empty;

            //There's a file loaded that uses another profile
            if (!ProfileNameSavedInESF.Equals(GlobalPreferences.SelectedProfileName) && NumberOfChildForms() > 1)
            {
                MessageBox.Show("Can't open two files with different profiles. Current profile: " + GlobalPreferences.SelectedProfile + " File profile: " + ProfileSavedInESF, "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //No files loaded but there's a diferent profile
            else if (NumberOfChildForms() == 1 && !ProfileNameSavedInESF.Equals(GlobalPreferences.SelectedProfileName))
            {
                //Get path of the profile file in the ini file.
                foreach (KeyValuePair<string, string> ProfileItem in AvailableProfiles)
                {
                    if (ProfileItem.Key.Equals(ProfileNameSavedInESF))
                    {
                        ProfileName = ProfileItem.Key;
                        ProfilePath = ProfileItem.Value;
                        break;
                    }
                }

                //Apply profile from ini file
                if (File.Exists(ProfilePath))
                {
                    ProfilesLoader.ApplyProfile(ProfilePath, ProfileName, true);
                }
                else
                {
                    //Apply profile saved in the ESF
                    if (File.Exists(ProfileSavedInESF))
                    {
                        ProfilesLoader.ApplyProfile(ProfileSavedInESF, ProfileNameSavedInESF, true);
                    }
                    else
                    {
                        MessageBox.Show("Unable to load file: " + ProfileSavedInESF + "\n" + "(Also tried to search for local copies in the overriden UseESP path from Esound.ini: <<" + ProfilePath + ">>)", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}