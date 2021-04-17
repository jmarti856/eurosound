using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Main
    {
        private void OpenFormsWithFileToLoad(string FileToLoad)
        {
            int TypeOfFileToLoad;

            if (!string.IsNullOrEmpty(FileToLoad))
            {
                //Check File Type
                TypeOfFileToLoad = TypeOfEuroSoundFile(FileToLoad);
                if (TypeOfFileToLoad == 0)
                {
                    //Add file to recent list
                    RecentFilesMenu.AddFile(FileToLoad);

                    //Save Active Document
                    WRegFunctions.SaveActiveDocument(FileToLoad);

                    //Open Form
                    Frm_Soundbanks_Main SoundBanksForm = new Frm_Soundbanks_Main(string.Empty, FileToLoad, RecentFilesMenu)
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
                    //Add file to recent list
                    RecentFilesMenu.AddFile(FileToLoad);

                    //Save Active Document
                    WRegFunctions.SaveActiveDocument(FileToLoad);

                    //Open Form
                    Frm_StreamSoundsEditorMain StreamSoundsForm = new Frm_StreamSoundsEditorMain(string.Empty, FileToLoad, RecentFilesMenu)
                    {
                        Owner = this,
                        MdiParent = this,
                        Tag = FormID.ToString()
                    };
                    StreamSoundsForm.Show();
                    FormID++;
                }
                else if (TypeOfFileToLoad == 2)
                {
                    //Add file to recent list
                    RecentFilesMenu.AddFile(FileToLoad);

                    //Save Active Document
                    WRegFunctions.SaveActiveDocument(FileToLoad);

                    //Open Form
                    Frm_Musics_Main MusicsForm = new Frm_Musics_Main(string.Empty, FileToLoad, RecentFilesMenu)
                    {
                        Owner = this,
                        MdiParent = this,
                        Tag = FormID.ToString()
                    };
                    MusicsForm.Show();
                    FormID++;
                }
                else
                {
                    MessageBox.Show(string.Join(" ", "Loading file:", FileToLoad, "\n\n", "Error:", FileToLoad, "has a bad format"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void OpenEmptyForms(string ProjectName, int TypeOfdata)
        {
            /*--[COMBOBOX FILE PROJECT SELECTED VALUES]--
            0 = Soundbanks; 1 = Streamed sounds; 2 = Music tracks*/

            if (TypeOfdata == 0)
            {
                Frm_Soundbanks_Main SoundBanksForms = new Frm_Soundbanks_Main(ProjectName, string.Empty, RecentFilesMenu)
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
                Frm_StreamSoundsEditorMain SoundBanksForms = new Frm_StreamSoundsEditorMain(ProjectName, string.Empty, RecentFilesMenu)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
            else if (TypeOfdata == 2)
            {
                Frm_Musics_Main MusicsForms = new Frm_Musics_Main(ProjectName, string.Empty, RecentFilesMenu)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                MusicsForms.Show();
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
            Type 2  = Musics
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
    }
}
