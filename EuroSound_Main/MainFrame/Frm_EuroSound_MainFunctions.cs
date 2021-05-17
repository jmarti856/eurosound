using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Octokit;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Main
    {
        private void OpenFormsWithFileToLoad(string FileToLoad)
        {
            int typeOfFileToLoad;

            if (!string.IsNullOrEmpty(FileToLoad))
            {
                if (!FileIsAlreadyOpened(FileToLoad))
                {
                    //Check File Type
                    typeOfFileToLoad = TypeOfEuroSoundFile(FileToLoad);

                    //Open form
                    if (typeOfFileToLoad == (int)GenericFunctions.ESoundFileType.SoundBanks)
                    {
                        //Add file to recent list
                        RecentFilesMenu.AddFile(FileToLoad);

                        //Save Active Document
                        WindowsRegistryFunctions.SaveActiveDocument(FileToLoad);

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
                    else if (typeOfFileToLoad == (int)GenericFunctions.ESoundFileType.StreamSounds)
                    {
                        //Add file to recent list
                        RecentFilesMenu.AddFile(FileToLoad);

                        //Save Active Document
                        WindowsRegistryFunctions.SaveActiveDocument(FileToLoad);

                        //Open Form
                        Frm_StreamSounds_Main StreamSoundsForm = new Frm_StreamSounds_Main(string.Empty, FileToLoad, RecentFilesMenu)
                        {
                            Owner = this,
                            MdiParent = this,
                            Tag = FormID.ToString()
                        };
                        StreamSoundsForm.Show();
                        FormID++;
                    }
                    else if (typeOfFileToLoad == (int)GenericFunctions.ESoundFileType.MusicBanks)
                    {
                        //Add file to recent list
                        RecentFilesMenu.AddFile(FileToLoad);

                        //Save Active Document
                        WindowsRegistryFunctions.SaveActiveDocument(FileToLoad);

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
        }

        private void OpenEmptyForms(string ProjectName, int TypeOfdata)
        {
            if (TypeOfdata == (int)GenericFunctions.ESoundFileType.SoundBanks)
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
            else if (TypeOfdata == (int)GenericFunctions.ESoundFileType.StreamSounds)
            {
                Frm_StreamSounds_Main SoundBanksForms = new Frm_StreamSounds_Main(ProjectName, string.Empty, RecentFilesMenu)
                {
                    Owner = this,
                    MdiParent = this,
                    Tag = FormID.ToString()
                };
                SoundBanksForms.Show();
                FormID++;
            }
            else if (TypeOfdata == (int)GenericFunctions.ESoundFileType.MusicBanks)
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
            int fileType = -1;

            if (File.Exists(FileToLoad))
            {
                EuroSoundFiles ESFFiles = new EuroSoundFiles();
                using (BinaryReader BReader = new BinaryReader(File.Open(FileToLoad, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    if (ESFFiles.FileIsCorrect(BReader))
                    {
                        fileType = BReader.ReadSByte();
                    }

                    BReader.Close();
                }
            }

            return fileType;
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
            bool filesRemoved = false;
            string temporalFolderPath = Path.Combine(new string[] { Path.GetTempPath(), "EuroSound" });

            //Delete Temp Files from session if exists
            if (Directory.Exists(temporalFolderPath))
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_RemovingTempFiles"));

                //Get temporal files
                DirectoryInfo temporalDirectoryInfo = new DirectoryInfo(temporalFolderPath);
                foreach (FileInfo fileToDelete in temporalDirectoryInfo.GetFiles())
                {
                    filesRemoved = true;
                    fileToDelete.Delete();
                }
                foreach (DirectoryInfo directoryToDelete in temporalDirectoryInfo.GetDirectories())
                {
                    filesRemoved = true;
                    directoryToDelete.Delete(true);
                }

                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }

            return filesRemoved;
        }

        private bool FileIsAlreadyOpened(string FilePathToCheck)
        {
            bool fileIsAlreadyLoaded = false;
            foreach (Form formToCheck in Application.OpenForms)
            {
                if (formToCheck.GetType() == typeof(Frm_Soundbanks_Main))
                {
                    if (((Frm_Soundbanks_Main)formToCheck).CurrentFilePath.Equals(FilePathToCheck))
                    {
                        fileIsAlreadyLoaded = true;
                        break;
                    }
                }
                else if (formToCheck.GetType() == typeof(Frm_StreamSounds_Main))
                {
                    if (((Frm_StreamSounds_Main)formToCheck).CurrentFilePath.Equals(FilePathToCheck))
                    {
                        fileIsAlreadyLoaded = true;
                        break;
                    }
                }
                else if (formToCheck.GetType() == typeof(Frm_Musics_Main))
                {
                    if (((Frm_Musics_Main)formToCheck).CurrentFilePath.Equals(FilePathToCheck))
                    {
                        fileIsAlreadyLoaded = true;
                        break;
                    }
                }
            }
            return fileIsAlreadyLoaded;
        }

        private void CheckForUpdates()
        {
            if (GlobalPreferences.ShowUpdatesAlerts)
            {
                if (GenericFunctions.CheckForInternetConnection())
                {
                    CheckUpdates = new Thread(async () =>
                    {
                        GitHubClient github = new GitHubClient(new ProductHeaderValue("EuroSound-Editor"));
                        IReadOnlyList<Release> ESReleases = await github.Repository.Release.GetAll("jmarti856", "eurosound");
                        if (ESReleases.Count > 0)
                        {
                            string currentRelease = GenericFunctions.GetEuroSoundVersion();
                            string latestRelease = ESReleases[0].TagName;
                            if (!currentRelease.Equals(latestRelease))
                            {
                                DialogResult updateQuestion = MessageBox.Show(string.Join("", "It seems that you don't have the latest version of EuroSound.\nYou have the release: ", currentRelease, " and the latest release is: ", latestRelease, ".\n\nWould you like to go to the repository page?"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (updateQuestion == DialogResult.Yes)
                                {
                                    Process.Start(string.Join("", "https://github.com/jmarti856/eurosound/releases/tag/", latestRelease));
                                }
                            }
                        }
                    })
                    {
                        IsBackground = true
                    };
                    CheckUpdates.Start();
                }
            }
        }
    }
}
