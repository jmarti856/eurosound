using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.CustomControls.InputBoxForm;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.Clases
{
    internal static class BrowsersAndDialogs
    {
        internal static string FileBrowserDialog(string BrowserFilter, int SelectedIndexFilter, bool ForceSpecifiedFilter)
        {
            string FilePath = string.Empty;

            using (OpenFileDialog FileBrowser = new OpenFileDialog())
            {
                if (ForceSpecifiedFilter)
                {
                    FileBrowser.Filter = BrowserFilter;
                }
                else
                {
                    FileBrowser.Filter = BrowserFilter + "|All files(*.*)|*.*";
                }
                FileBrowser.FilterIndex = SelectedIndexFilter;

                if (FileBrowser.ShowDialog() == DialogResult.OK)
                {
                    FilePath = FileBrowser.FileName;
                }
            }

            return FilePath;
        }

        internal static string InputBoxDialog(string Text, string Title)
        {
            string SampleName = string.Empty;

            //Ask user for a name
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
                OpenFolder.SelectedPath = WindowsRegistryFunctions.LoadFolderBrowserLastPath();
                if (OpenFolder.ShowDialog() == DialogResult.OK)
                {
                    SelectedPath = OpenFolder.SelectedPath;
                    WindowsRegistryFunctions.SaveFolderBrowserLastPath(SelectedPath);
                }
            }

            return SelectedPath;
        }

        internal static int ColorPickerDialog(Color SelectedUserColor)
        {
            int SelectedColor = -1;

            using (ColorDialog ColorDiag = new ColorDialog())
            {
                ColorDiag.Color = SelectedUserColor;
                ColorDiag.AllowFullOpen = true;
                ColorDiag.FullOpen = true;
                ColorDiag.CustomColors = WindowsRegistryFunctions.LoadCustomColors();
                if (ColorDiag.ShowDialog() == DialogResult.OK)
                {
                    SelectedColor = ColorDiag.Color.ToArgb();
                    WindowsRegistryFunctions.SaveCustomColors(ColorDiag.CustomColors);
                }
            }

            //Ask user to reset color
            if (SelectedColor == -1)
            {
                if (!SelectedUserColor.ToArgb().Equals(SystemColors.WindowText.ToArgb()))
                {
                    DialogResult QuestionAnswer = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("ColorDialogRemoveColor"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (QuestionAnswer == DialogResult.Yes)
                    {
                        SelectedColor = SystemColors.WindowText.ToArgb();
                    }
                }
            }

            return SelectedColor;
        }
    }
}
