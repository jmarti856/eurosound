using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Main
    {
        private void OpenFormsWithFileToLoad(string FileToLoad)
        {
            int TypeOfFileToLoad;

            //Save file to recent files list
            if (string.IsNullOrEmpty(RecentFilesList[0]))
            {
                MainMenu_File.DropDownItems.RemoveAt(3);
            }

            //Add it to list if not exists
            if (!RecentFilesList.Contains(FileToLoad))
            {

                RecentFilesList[NextMergeIndexRecentFiles - 1] = FileToLoad;
                InsertRecentFileToMenu(FileToLoad);
            }

            //Open form
            TypeOfFileToLoad = TypeOfEuroSoundFile(FileToLoad);
            if (TypeOfFileToLoad == 0)
            {
                Frm_Soundbanks_Main SoundBanksForm = new Frm_Soundbanks_Main(string.Empty, FileToLoad)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForm.Show();
                FormID++;
            }
            else if (TypeOfFileToLoad == 1)
            {
                Frm_StreamSoundsEditorMain StreamSoundsForm = new Frm_StreamSoundsEditorMain(string.Empty, FileToLoad)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };

                StreamSoundsForm.Show();
                FormID++;
            }
        }

        private void OpenEmptyForms(string ProjectName, int TypeOfdata)
        {
            /*--[COMBOBOX FILE PROJECT SELECTED VALUES]--
            0 = Soundbanks; 1 = Streamed sounds; 2 = Music tracks*/

            if (TypeOfdata == 0)
            {
                Frm_Soundbanks_Main SoundBanksForms = new Frm_Soundbanks_Main(ProjectName, string.Empty)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
            else if (TypeOfdata == 1)
            {
                Frm_StreamSoundsEditorMain SoundBanksForms = new Frm_StreamSoundsEditorMain(ProjectName, string.Empty)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
        }

        private int TypeOfEuroSoundFile(string FileToLoad)
        {
            int Type = -1;

            /* TYPE VALUES
            Type -1 = bad format
            Type 0  = Soundbank
            Type 1  = Stream Soundbank
            Type 2  = Musics --NOT IMPLEMENTED YET--
            */

            if (System.IO.File.Exists(FileToLoad))
            {
                EuroSoundFiles ESFFiles = new EuroSoundFiles();
                using (BinaryReader BReader = new BinaryReader(System.IO.File.Open(FileToLoad, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    if (ESFFiles.FileIsCorrect(BReader))
                    {
                        Type = BReader.ReadSByte();
                    }

                    BReader.Close();
                }
            }

            return Type;
        }

        private void RestoreApplication()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Maximized;
                EuroSoundTrayIcon.Visible = false;
                ShowInTaskbar = true;
            }
        }

        private bool ClearTemporalFiles()
        {
            bool FilesRemoved = false;
            string TemporalFolderPath = Path.Combine(new string[] { Path.GetTempPath(), "EuroSound" });

            //Delete Temp Files from session if exists
            if (Directory.Exists(TemporalFolderPath))
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_RemovingTempFiles"));

                //Get temporal files
                DirectoryInfo TemporalDirectoryInfo = new DirectoryInfo(TemporalFolderPath);
                foreach (FileInfo FileToDelete in TemporalDirectoryInfo.GetFiles())
                {
                    FilesRemoved = true;
                    FileToDelete.Delete();
                }
                foreach (DirectoryInfo DirectoryToDelete in TemporalDirectoryInfo.GetDirectories())
                {
                    FilesRemoved = true;
                    DirectoryToDelete.Delete(true);
                }

                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            }

            return FilesRemoved;
        }

        private void InsertRecentFileToMenu(string FilePath)
        {
            bool ItemExists = false;
            string NextItemName;

            //Get Name
            NextItemName = "RecentFile" + NextMergeIndexRecentFiles;

            //If Exists Modify
            foreach (object item in MainMenu_File.DropDownItems)
            {
                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem TItem = (ToolStripMenuItem)item;
                    if (TItem.Name.Equals(NextItemName))
                    {
                        TItem.Text = string.Join(" ", NextMergeIndexRecentFiles.ToString(), GetShortPath(FilePath));
                        TItem.Tag = FilePath;
                        ItemExists = true;
                        break;
                    }
                }
            }

            //Create From Scratch
            if (!ItemExists)
            {
                //Add Item
                int RelativeIndex = MainMenu_File.DropDownItems.IndexOf(MenuItemFile_Separator1);

                ToolStripMenuItem RecentFile = new ToolStripMenuItem(string.Join(" ", NextMergeIndexRecentFiles.ToString(), GetShortPath(FilePath)), null, RecentFile_click);
                RecentFile.MouseHover += new EventHandler(RecentFile_MouseHover);
                RecentFile.MouseLeave += new EventHandler(RecentFile_MouseLeave);
                RecentFile.MergeIndex = 11 + (NextMergeIndexRecentFiles);
                RecentFile.Tag = FilePath;
                RecentFile.Name = NextItemName;
                MainMenu_File.DropDownItems.Insert(NextMergeIndexRecentFiles + RelativeIndex, RecentFile);
            }

            NextMergeIndexRecentFiles++;

            //Items Count
            if (NextMergeIndexRecentFiles > 8)
            {
                NextMergeIndexRecentFiles = 1;
            }
        }

        private string GetShortPath(string FullPath)
        {
            string ShortPath;
            string SplittedPath;

            //Get filename
            ShortPath = Path.GetFileName(FullPath);

            //Split path
            string[] Paths = FullPath.Split(new string[] { "\\" }, StringSplitOptions.None);

            //We have more than 3 directories
            if (Paths.Length > 3)
            {
                SplittedPath = string.Join(@"\", Paths[0], Paths[1], "...", Paths[Paths.Length - 1]);
                if (SplittedPath.Length <= 30)
                {
                    ShortPath = SplittedPath;
                }
            }
            //We have 2 directories (Root Folder Filename)
            else if (Paths.Length == 3)
            {
                SplittedPath = string.Join(@"\", Paths[0], Paths[1], Paths[Paths.Length - 1]);
                if (SplittedPath.Length <= 30)
                {
                    ShortPath = SplittedPath;
                }
            }
            //We have root and file name
            else if (Paths.Length < 3)
            {
                SplittedPath = string.Join(@"\", Paths[0], Paths[Paths.Length - 1]);
                if (SplittedPath.Length <= 30)
                {
                    ShortPath = SplittedPath;
                }
            }

            return ShortPath;
        }
    }
}
