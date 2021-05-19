using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.DebugTypes;
using EuroSound_Application.CustomControls.ProjectSettings;
using EuroSound_Application.CustomControls.SearcherForm;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.SoundBanksEditor.BuildSFX;
using EuroSound_Application.SoundBanksEditor.YMLReader;
using EuroSound_Application.TreeViewLibraryFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_Soundbanks_Main : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        internal Dictionary<string, EXAudio> AudioDataDict = new Dictionary<string, EXAudio>();
        internal Dictionary<uint, EXSound> SoundsList = new Dictionary<uint, EXSound>();
        internal ProjectFile ProjectInfo = new ProjectFile();
        internal string CurrentFilePath = string.Empty;
        private string ProjectName;
        private bool FormMustBeClosed = false;
        private bool CanExpandChildNodes = true;
        private EuroSoundFiles EuroSoundFilesFunctions = new EuroSoundFiles();
        private Thread UpdateList, UpdateWavList, UpdateStreamDataList, LoadYamlFile, LoadSoundBankFile;
        private SoundBanksYMLReader LibYamlReader = new SoundBanksYMLReader();
        private MostRecentFilesMenu RecentFilesMenu;
        private System.Timers.Timer TimerBackups;

        //The undo and redo history lists.
        private Stack<object> UndoListSounds = new Stack<object>();
        private Stack<KeyValuePair<string, TreeNode>> UndoListNodes = new Stack<KeyValuePair<string, TreeNode>>();

        public Frm_Soundbanks_Main(string NewProjectName, string FileToLoad, MostRecentFilesMenu RecentFiles)
        {
            InitializeComponent();

            CurrentFilePath = FileToLoad;
            ProjectName = NewProjectName;
            RecentFilesMenu = RecentFiles;

            //Menu Item: File
            MenuItem_File_Close.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_Close")); };
            MenuItem_File_Save.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_File_Save")); };
            MenuItem_File_SaveAs.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_File_SaveAs")); };
            MenuItem_File_Export.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_File_Export")); };
            MenuItem_File_ImportYML_Single.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ReadSound")); };
            MenuItem_File_ImportYML_List.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ReadYml")); };
            MenuItem_File_ImportESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ImportESIF")); };

            MenuItem_File_Close.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Save.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_SaveAs.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Export.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_ImportYML_Single.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_ImportYML_List.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_ImportESIF.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: Edit
            MenuItem_Edit.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MenuItem_Edit.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItem_Edit_Search.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankSearch")); };
            MenuItem_Edit_Undo.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemEdit_UndoSoundbanks")); };
            MenuItem_Edit_FileProps.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_Edit_FileProps")); };

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Undo.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Search.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: View
            MenuItemView_CollapseTree.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemView_CollapseTree")); };

            MenuItemView_CollapseTree.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Buttons
            Button_GenerateList.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonGenerateSoundsList")); };
            Button_GenerateList.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_Hashcodes.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankCheckHashcodes")); };
            Button_UpdateList_Hashcodes.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_WavData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankCheckAudios")); };
            Button_UpdateList_WavData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_StreamData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankCheckStreamData")); };
            Button_UpdateList_StreamData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopHashcodeUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopHashcodeUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_Stop_WavUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonStopListUpdate")); };
            Button_Stop_WavUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopStreamData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopStreamData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_ExportInterchangeFile.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("Button_ExportInterchangeFile")); };
            Button_ExportInterchangeFile.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Folders
            ContextMenuFolders_Folder.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(""); };
            ContextMenuFolders_New.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolders_New")); };
            ContextMenuFolder_ExpandAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_ExpandAll")); };
            ContextMenuFolder_CollapseAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_CollapseAll")); };
            ContextMenuFolder_Delete.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Delete")); };
            ContextMenuFolder_Sort.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Sort")); };
            ContextMenuFolder_Move.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Move")); };
            ContextMenuFolder_AddSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_AddSound")); };
            ContextMenuFolder_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Rename")); };
            ContextMenuFolder_AddAudio.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_AddAudio")); };
            ContextMenuFolder_Purge.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Purge")); };
            ContextMenuFolder_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuFolder_ExportSounds.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenuFolder_ImportESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ImportESIF")); };
            ContextMenu_Folders.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Sound
            ContextMenuSound_AddSample.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_AddSample")); };
            ContextMenuSound_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_Remove")); };
            ContextMenuSound_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_Rename")); };
            ContextMenuSound_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_Properties")); };
            ContextMenuSound_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuSound_ExportSingle.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenu_Sound.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Samples
            ContextMenuSample_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSample_Remove")); };
            ContextMenuSample_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSample_Rename")); };
            ContextMenuSample_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSample_Properties")); };
            ContextMenuSample_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Sample.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Audio
            ContextMenuAudio_Usage.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuAudio_Usage")); };
            ContextMenuAudio_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuAudio_OpenProperties")); };
            ContextMenuAudio_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuAudio_Delete")); };
            ContextMenuAudio_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuAudio_Rename")); };
            ContextMenuAudio_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Audio.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_Soundbanks_Main_Load(object sender, EventArgs e)
        {
            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Type of data that creates this form
            ProjectInfo.TypeOfData = (int)GenericFunctions.ESoundFileType.SoundBanks;

            //Check Hashcodes are not null
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Count == 0)
            {
                //Load Data
                Thread LoadHashcodeData = new Thread(() => Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath))
                {
                    IsBackground = true
                };
                Thread LoadHashcodes = new Thread(() => Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath))
                {
                    IsBackground = true
                };
                LoadHashcodes.Start();
                LoadHashcodes.Join();
                LoadHashcodeData.Start();
            }

            //Load Last State
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                if (Convert.ToBoolean(WindowStateConfig.GetValue("SBView_IsIconic", 0)))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (Convert.ToBoolean(WindowStateConfig.GetValue("SBView_IsMaximized", 0)))
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("SBView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("SBView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("SBView_Width", 997));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("SBView_Height", 779));
                WindowStateConfig.Close();
            }

            //Backups
            if (GlobalPreferences.MakeBackups)
            {
                TimerBackups = new System.Timers.Timer(TimeSpan.FromMinutes(GlobalPreferences.MakeBackupsInterval).TotalMilliseconds)
                {
                    AutoReset = true
                };
                TimerBackups.Elapsed += new ElapsedEventHandler(MakeBackup);
                TimerBackups.Start();
            }
        }

        private void MakeBackup(object sender, ElapsedEventArgs e)
        {
            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_SavingBackUp"));

            //Check index
            if (GlobalPreferences.MakeBackupsIndex > (GlobalPreferences.MakeBackupsMaxNumber - 1))
            {
                GlobalPreferences.MakeBackupsIndex = 0;
            }

            //Save File
            if (Directory.Exists(GlobalPreferences.MakeBackupsDirectory))
            {
                EuroSoundFilesFunctions.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, Path.Combine(GlobalPreferences.MakeBackupsDirectory, string.Join("", "ES_BackUp", GlobalPreferences.MakeBackupsIndex, ".ESF")), ProjectInfo);
                GlobalPreferences.MakeBackupsIndex++;
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_Soundbanks_Main_Shown(object sender, EventArgs e)
        {
            if (FormMustBeClosed)
            {
                Close();
            }
            else
            {
                //Apply Splitter Distance
                using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
                {
                    SplitContainer_SoundbanksForm.SplitterDistance = Convert.ToInt32(WindowStateConfig.GetValue("SBView_SplitterDistance", 456));
                    WindowStateConfig.Close();
                }

                //Load ESF file if nedded
                if (string.IsNullOrEmpty(CurrentFilePath))
                {
                    ProjectInfo.FileName = ProjectName;

                    //Update File name label
                    UpdateStatusBarLabels();

                    //Set Program status
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
                }
                else
                {
                    if (GlobalPreferences.UseThreadingWhenLoad)
                    {
                        LoadSoundBankFile = new Thread(() =>
                        {
                            //Disable Button
                            Button_UpdateList_WavData.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_WavData.Enabled = false;
                            });

                            //Disable Button
                            Button_Stop_WavUpdate.Invoke((MethodInvoker)delegate
                            {
                                Button_Stop_WavUpdate.Enabled = false;
                            });

                            //Disable Button
                            Button_UpdateList_Hashcodes.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_Hashcodes.Enabled = false;
                            });

                            //Disable Button
                            Button_StopHashcodeUpdate.Invoke((MethodInvoker)delegate
                            {
                                Button_StopHashcodeUpdate.Enabled = false;
                            });

                            //Disable Button
                            Button_UpdateList_StreamData.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_StreamData.Enabled = false;
                            });

                            //Disable Button
                            Button_StopStreamData.Invoke((MethodInvoker)delegate
                            {
                                Button_StopStreamData.Enabled = false;
                            });

                            //Disable Button
                            Button_GenerateList.Invoke((MethodInvoker)delegate
                            {
                                Button_GenerateList.Enabled = false;
                            });

                            //Disable Button
                            Button_ExportInterchangeFile.Invoke((MethodInvoker)delegate
                            {
                                Button_ExportInterchangeFile.Enabled = false;
                            });

                            //Check that the profile name matches with the current one
                            string ProfileName = EuroSoundFilesFunctions.LoadEuroSoundFile(TreeView_File, SoundsList, AudioDataDict, CurrentFilePath, ProjectInfo);
                            if (!ProfileName.Equals(GlobalPreferences.SelectedProfileName))
                            {
                                FormMustBeClosed = true;
                            }

                            //Update File name label
                            UpdateStatusBarLabels();

                            //Enable Button
                            Button_UpdateList_WavData.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_WavData.Enabled = true;
                            });

                            //Enable Button
                            Button_Stop_WavUpdate.Invoke((MethodInvoker)delegate
                            {
                                Button_Stop_WavUpdate.Enabled = true;
                            });

                            //Enable Button
                            Button_UpdateList_Hashcodes.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_Hashcodes.Enabled = true;
                            });

                            //Enable Button
                            Button_StopHashcodeUpdate.Invoke((MethodInvoker)delegate
                            {
                                Button_StopHashcodeUpdate.Enabled = true;
                            });

                            //Enable Button
                            Button_UpdateList_StreamData.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_StreamData.Enabled = true;
                            });

                            //Enable Button
                            Button_StopStreamData.Invoke((MethodInvoker)delegate
                            {
                                Button_StopStreamData.Enabled = true;
                            });

                            //Enable Button
                            Button_GenerateList.Invoke((MethodInvoker)delegate
                            {
                                Button_GenerateList.Enabled = true;
                            });

                            //Enable Button
                            Button_ExportInterchangeFile.Invoke((MethodInvoker)delegate
                            {
                                Button_ExportInterchangeFile.Enabled = true;
                            });

                            //Set Program status
                            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
                        })
                        {
                            IsBackground = true
                        };
                        LoadSoundBankFile.Start();
                    }
                    else
                    {
                        //Check that the profile name matches with the current one
                        string ProfileName = EuroSoundFilesFunctions.LoadEuroSoundFile(TreeView_File, SoundsList, AudioDataDict, CurrentFilePath, ProjectInfo);
                        if (!ProfileName.Equals(GlobalPreferences.SelectedProfileName))
                        {
                            FormMustBeClosed = true;
                        }

                        //Update File name label
                        UpdateStatusBarLabels();

                        //Set Program status
                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
                    }
                }

                //Update from title
                Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
                if (WindowState != FormWindowState.Maximized)
                {
                    MdiParent.Text = "EuroSound - " + Text;
                }

                //Apply User Preferences
                TreeView_File.Indent = GlobalPreferences.TV_Indent;
                TreeView_File.ItemHeight = GlobalPreferences.TV_ItemHeight;
                TreeView_File.Font = new FontConverter().ConvertFromString(GlobalPreferences.TV_SelectedFont) as Font;
                TreeView_File.ShowLines = GlobalPreferences.TV_ShowLines;
                TreeView_File.ShowRootLines = GlobalPreferences.TV_ShowRootLines;
            }
        }

        private void Frm_Soundbanks_Main_Enter(object sender, EventArgs e)
        {
            //Update File name label
            UpdateStatusBarLabels();

            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void Frm_Soundbanks_Main_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void Frm_Soundbanks_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_StoppingThreads"));

            //Stop Threads
            if (UpdateList != null)
            {
                UpdateList.Abort();
            }

            if (UpdateWavList != null)
            {
                UpdateWavList.Abort();
            }

            if (UpdateStreamDataList != null)
            {
                UpdateStreamDataList.Abort();
            }

            if (LoadYamlFile != null)
            {
                LoadYamlFile.Abort();
            }

            if (LoadSoundBankFile != null)
            {
                LoadSoundBankFile.Abort();
            }

            //Stop timer
            if (TimerBackups != null)
            {
                TimerBackups.Stop();
                TimerBackups.Close();
                TimerBackups.Dispose();
            }

            //Clear stack lists
            UndoListSounds.Clear();
            UndoListNodes.Clear();

            //Check closing reason
            if (e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.UserClosing)
            {
                //Ask user to save if file is modified
                if (ProjectInfo.FileHasBeenModified)
                {
                    DialogResult dialogResult = MessageBox.Show("Save changes to " + ProjectInfo.FileName + "?", "EuroSound", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //Check if we have a path for this file
                        if (string.IsNullOrEmpty(CurrentFilePath))
                        {
                            CurrentFilePath = OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectInfo);
                        }
                        //Save Data
                        else
                        {
                            EuroSoundFilesFunctions.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, CurrentFilePath, ProjectInfo);
                        }
                        ProjectInfo.FileHasBeenModified = false;
                        MdiParent.Text = "EuroSound";
                        Close();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ProjectInfo.FileHasBeenModified = false;
                        MdiParent.Text = "EuroSound";
                        Close();
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    MdiParent.Text = "EuroSound";
                }

                ClearStatusBarLabels();
            }

            WindowsRegistryFunctions.SaveWindowState("SBView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized, SplitContainer_SoundbanksForm.SplitterDistance);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            //Clear Last File Label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_LastFile");
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Button_GenerateList_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            string savePath = BrowsersAndDialogs.SaveFileBrowser("YML Files (*.yml)|*.yml", 1, true, ProjectName);
            if (!string.IsNullOrEmpty(savePath))
            {
                StreamWriter fileWriter = new StreamWriter(savePath);
                fileWriter.WriteLine("#ftype:1");
                fileWriter.WriteLine("# swy: EngineX sound bank exported from " + Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, ProjectInfo.Hashcode) + " / " + ProjectInfo.Hashcode);
                foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                {
                    fileWriter.WriteLine("- " + Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, Convert.ToUInt32(Sound.Value.Hashcode)));
                }
                fileWriter.Close();
                fileWriter.Dispose();
            }
        }

        private void Button_ExportInterchangeFile_Click(object sender, EventArgs e)
        {
            string exportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, ProjectInfo.FileName);
            if (!string.IsNullOrEmpty(exportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportProject(exportPath, true, ProjectInfo, SoundsList, AudioDataDict, TreeView_File);
            }
        }

        //*===============================================================================================
        //* LIST VIEWS DATA
        //*===============================================================================================
        private void Button_UpdateList_Hashcodes_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            //Check New data
            UpdateHashcodesValidList();
        }

        private void ListView_Hashcodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_Hashcodes.SelectedItems.Count > 0)
            {
                TreeNode[] selectedNode = TreeView_File.Nodes.Find(ListView_Hashcodes.SelectedItems[0].Tag.ToString(), true);
                if (selectedNode.Length > 0)
                {
                    OpenSoundProperties(selectedNode[0]);
                }
            }
        }

        /*-----------------------------[STREAM DATA]-----------------------------*/
        private void Button_UpdateList_StreamData_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            //Check Stream Data
            UpdateStreamedDataList();
        }

        private void ListView_StreamData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_StreamData.SelectedItems.Count > 0)
            {
                TreeNode[] SelectedNode = TreeView_File.Nodes.Find(ListView_StreamData.SelectedItems[0].Tag.ToString(), true);
                if (SelectedNode.Length > 0)
                {
                    OpenSampleProperties(SelectedNode[0]);
                }
            }
        }

        /*-----------------------------[WAV DATA]-----------------------------*/
        private void Button_UpdateList_WavData_Click(object sender, EventArgs e)
        {
            //Check Audio properties
            UpdateWavDataList();
        }

        private void ListView_WavHeaderData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_WavHeaderData.SelectedItems.Count > 0)
            {
                TreeNode[] selectedNode = TreeView_File.Nodes.Find(ListView_WavHeaderData.SelectedItems[0].Tag.ToString(), true);
                if (selectedNode.Length > 0)
                {
                    OpenAudioProperties(selectedNode[0]);
                }
            }
        }

        //*===============================================================================================
        //* MAIN MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;

            Frm_FileProperties filePropsForm = new Frm_FileProperties(ProjectInfo)
            {
                Owner = this,
                ShowInTaskbar = false,
                Tag = Tag
            };
            filePropsForm.ShowDialog();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void MenuItem_Edit_Undo_Click(object sender, EventArgs e)
        {
            //Restore the first serialization from the undo list.
            if (UndoListSounds.Count > 0)
            {
                //Get Node
                KeyValuePair<string, TreeNode> itemWithTreeNode = UndoListNodes.Pop();
                TreeNode NodeToAdd = itemWithTreeNode.Value;

                if (UndoListSounds.Peek().GetType() == typeof(KeyValuePair<uint, EXSound>))
                {
                    //Get Object
                    KeyValuePair<uint, EXSound> ItemToRestore = (KeyValuePair<uint, EXSound>)UndoListSounds.Pop();

                    //Check that object does not exists
                    bool NodeToAddExists = TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, NodeToAdd.Text);
                    if (SoundsList.ContainsKey(ItemToRestore.Key) || NodeToAddExists == true)
                    {
                        MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because another item with the same name exists", NodeToAdd.Text), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //Get Parent
                        TreeNode[] ParentNode = TreeView_File.Nodes.Find(itemWithTreeNode.Key, true);
                        if (ParentNode.Length > 0)
                        {
                            //Add node
                            ParentNode[0].Nodes.Insert(NodeToAdd.Index, NodeToAdd);

                            //Get Node
                            SoundsList.Add(ItemToRestore.Key, ItemToRestore.Value);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because their parent \"{1}\" does not exists", NodeToAdd.Text, itemWithTreeNode.Key), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else if (UndoListSounds.Peek().GetType() == typeof(KeyValuePair<string, EXAudio>))
                {
                    //Get Object
                    KeyValuePair<string, EXAudio> ItemToRestore = (KeyValuePair<string, EXAudio>)UndoListSounds.Pop();

                    //Check that object does not exists
                    bool NodeToAddExists = TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, NodeToAdd.Text);
                    if (AudioDataDict.ContainsKey(ItemToRestore.Key) || NodeToAddExists == true)
                    {
                        MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because another item with the same name exists", NodeToAdd.Text), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //Get Parent
                        TreeNode[] ParentNode = TreeView_File.Nodes.Find(itemWithTreeNode.Key, true);
                        if (ParentNode.Length > 0)
                        {
                            //Add node
                            ParentNode[0].Nodes.Insert(NodeToAdd.Index, NodeToAdd);

                            //Get Node
                            AudioDataDict.Add(ItemToRestore.Key, ItemToRestore.Value);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because their parent \"{1}\" does not exists", NodeToAdd.Text, itemWithTreeNode.Key), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                //Enable or disable button
                EnableUndo();
            }
        }

        private void MenuItem_Edit_Search_Click(object sender, EventArgs e)
        {
            EuroSound_SearchItem searchForm = new EuroSound_SearchItem(Name)
            {
                Owner = this,
                Tag = Tag
            };
            searchForm.Show();
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*==============================================================================================
        private void MenuItem_File_ImportESIF_Click(object sender, EventArgs e)
        {
            string filePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Interchange File (*.ESIF)|*.esif", 0, true);
            if (!string.IsNullOrEmpty(filePath))
            {
                ESIF_Loader euroSoundPropsFileLoader = new ESIF_Loader();
                IList<string> importResultsList = euroSoundPropsFileLoader.LoadSFX_File(filePath, ProjectInfo, SoundsList, AudioDataDict, TreeView_File);
                if (importResultsList.Count > 0)
                {
                    GenericFunctions.ShowErrorsAndWarningsList(importResultsList, "Import Results", this);
                }

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItem_File_ImportYML_List_Click(object sender, EventArgs e)
        {
            string filePath = BrowsersAndDialogs.FileBrowserDialog("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(filePath))
            {
                //Ask user for a fully reimport
                DialogResult reimportQuestion = MessageBox.Show(GenericFunctions.resourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (reimportQuestion == DialogResult.Yes)
                {
                    //Clear Data
                    ProjectInfo.ClearSoundBankStoredData(SoundsList, AudioDataDict, TreeView_File);

                    //Clear stack lists
                    UndoListSounds.Clear();
                    UndoListNodes.Clear();
                }

                //Update file Hashcode
                DialogResult updateHashcodeQuestion = MessageBox.Show("Do you want to use the hashcode of the loaded file?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (updateHashcodeQuestion == DialogResult.Yes)
                {
                    ProjectInfo.Hashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SB_Defines, Path.GetFileNameWithoutExtension(filePath));
                    GenericFunctions.SetCurrentFileLabel(Path.GetFileNameWithoutExtension(filePath), "SBPanel_Hashcode");
                }

                //Load New data
                LoadYamlFile = new Thread(() => LibYamlReader.LoadDataFromSwyterUnpacker(SoundsList, AudioDataDict, TreeView_File, filePath, ProjectInfo))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItem_File_ImportYML_Single_Click(object sender, EventArgs e)
        {
            string filePath = BrowsersAndDialogs.FileBrowserDialog("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(filePath))
            {
                string SoundName = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name;
                uint SoundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, SoundName);
                LibYamlReader.ReadYmlFile(SoundsList, AudioDataDict, TreeView_File, filePath, SoundName, SoundHashcode, true, ProjectInfo);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItem_File_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItem_File_Save_Click(object sender, EventArgs e)
        {
            //Check if we have a path for this file
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                CurrentFilePath = OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectInfo);
            }
            //Save Data
            else
            {
                EuroSoundFilesFunctions.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, CurrentFilePath, ProjectInfo);
            }
            ProjectInfo.FileHasBeenModified = false;

            Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void MenuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            //Save file in different location
            CurrentFilePath = OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectInfo);

            //Update text
            Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }

            //Update var
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItemFile_Export_Click(object sender, EventArgs e)
        {
            if (ProjectInfo.Hashcode != 0x00000000)
            {
                string fileName = "HC" + ProjectInfo.Hashcode.ToString("X8").Substring(2);

                //---[Output with debug options
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (EuroSound_DebugTypes DebugOpts = new EuroSound_DebugTypes(new string[] { "SFX Elements", "Sample info elements", "Sample Data" }))
                    {
                        DebugOpts.Owner = Owner;
                        if (DebugOpts.ShowDialog() == DialogResult.OK)
                        {
                            BuildSfxForm(fileName, DebugOpts.CheckedOptions);
                        }
                    }
                }
                else
                {
                    BuildSfxForm(fileName, 0);
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("Error_BuildSFX_NoHashcode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildSfxForm(string FileName, int DebugFlags)
        {
            Frm_BuildSFXFile buildSFXFileForm = new Frm_BuildSFXFile(ProjectInfo, FileName, DebugFlags)
            {
                Tag = Tag,
                Owner = Owner,
                ShowInTaskbar = false
            };
            buildSFXFileForm.ShowDialog();
        }

        //*===============================================================================================
        //* TREE VIEW EVENTS
        //*===============================================================================================
        private void TreeView_File_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //Check that we have selected a node, and we have not selected the root folder
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                //Check label is not null, sometimes can crash without this check
                if (e.Label != null)
                {
                    //Get text label
                    string labelText = e.Label.Trim();

                    //Check we are not renaming with an empty string
                    if (string.IsNullOrEmpty(labelText))
                    {
                        //Cancel edit
                        e.CancelEdit = true;
                    }
                    else
                    {
                        //Check that not exists an item with the same name
                        if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, labelText))
                        {
                            MessageBox.Show(GenericFunctions.resourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.CancelEdit = true;
                        }
                        else
                        {
                            e.Node.Text = labelText;
                        }
                    }
                }
            }
            else
            {
                //Cancel edit
                e.CancelEdit = true;
            }
        }

        //---------------------------------------------[Change Nodes Images]---------------------------------------------
        private void TreeView_File_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (CanExpandChildNodes)
            {
                //Change node images depending of the type
                if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 0, 0);
                }
                else if (e.Node.Tag.Equals("Sound"))
                {
                    if (EXSoundbanksFunctions.SoundWillBeOutputed(SoundsList, uint.Parse(e.Node.Name)))
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 2, 2);
                    }
                    else
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 5, 5);
                    }
                }
                else if (e.Node.Tag.Equals("Sample"))
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 4, 4);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TreeView_File_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (CanExpandChildNodes)
            {
                //Change node images depending of the type
                if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 1, 1);
                }
                else if (e.Node.Tag.Equals("Sound"))
                {
                    if (EXSoundbanksFunctions.SoundWillBeOutputed(SoundsList, uint.Parse(e.Node.Name)))
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 3, 3);
                    }
                    else
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 6, 6);
                    }
                }
                else if (e.Node.Tag.Equals("Sample"))
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 4, 4);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TreeView_File_DragDrop(object sender, DragEventArgs e)
        {
            //Retrieve the client coordinates of the drop location.
            Point targetPoint = TreeView_File.PointToClient(new Point(e.X, e.Y));

            //Retrieve the node at the drop location.
            TreeNode targetNode = TreeView_File.GetNodeAt(targetPoint);
            TreeNode findTargetNode = TreeNodeFunctions.FindRootNode(targetNode);

            TreeNode parentNode = targetNode;

            if (findTargetNode != null)
            {
                string destSection = findTargetNode.Text;
                string destNodeType = targetNode.Tag.ToString();

                //Retrieve the node that was dragged
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                string sourceSection = TreeNodeFunctions.FindRootNode(draggedNode).Text;

                //Confirm that the node at the drop location is not
                //the dragged node and that target node isn't null
                //(for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && draggedNode != null && targetNode != null)
                {
                    bool canDrop = true;
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !Object.ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }

                    if (canDrop)
                    {
                        /*
                        Confirm we are not outside the node section and that the destination place is a folder or the root
                        node section
                        */
                        if (sourceSection.Equals(destSection) && (destNodeType.Equals("Folder") || destNodeType.Equals("Root")))
                        {
                            //Remove the node from its current
                            //location and add it to the node at the drop location.
                            draggedNode.Remove();
                            targetNode.Nodes.Add(draggedNode);
                            targetNode.Expand();
                            TreeView_File.SelectedNode = draggedNode;
                            ProjectInfo.FileHasBeenModified = true;
                        }
                    }
                }
            }
        }

        private void TreeView_File_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (draggedNode != null)
            {
                Point targetPoint = TreeView_File.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = TreeView_File.GetNodeAt(targetPoint);

                if (targetNode != null)
                {
                    //Type of nodes that are allowed to be re-ubicated
                    if (draggedNode.Tag.Equals("Folder") || draggedNode.Tag.Equals("Sound") || draggedNode.Tag.Equals("Audio"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    TreeView_File.SelectedNode = targetNode;
                }
            }
        }

        private void TreeView_File_DragOver(object sender, DragEventArgs e)
        {
            const float scrollRegion = 20;

            Point p = TreeView_File.PointToClient(new Point(e.X, e.Y));

            //See if we need to scroll up or down
            if ((p.Y + scrollRegion) > TreeView_File.Height)
            {
                //Call the API to scroll down
                SendMessage(TreeView_File.Handle, 277, 1, 0);
            }
            else if (p.Y < scrollRegion)
            {
                //Call thje API to scroll up
                SendMessage(TreeView_File.Handle, 277, 0, 0);
            }

            TreeNode node = TreeView_File.GetNodeAt(p.X, p.Y);
            TreeView_File.SelectedNode = node;
        }

        private void TreeView_File_MouseDown(object sender, MouseEventArgs e)
        {
            //Expand child nodes only if is a folder
            if (TreeView_File.SelectedNode != null)
            {
                CanExpandChildNodes = true;

                //Double click
                if (e.Clicks > 1)
                {
                    if (!(TreeView_File.SelectedNode.Tag.Equals("Folder") || TreeView_File.SelectedNode.Tag.Equals("Root")))
                    {
                        CanExpandChildNodes = false;
                    }
                }
            }
        }

        private void TreeView_File_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeView_File_MouseClick(object sender, MouseEventArgs e)
        {
            //Open context menu depending of the selected node
            if (e.Button == MouseButtons.Right)
            {
                //Select node
                TreeNode selectedTreeViewNode = TreeView_File.GetNodeAt(e.X, e.Y);
                TreeView_File.SelectedNode = selectedTreeViewNode;

                //Check node is not null
                if (selectedTreeViewNode != null)
                {
                    //Check the selected node
                    if (selectedTreeViewNode.Tag.Equals("Folder") || selectedTreeViewNode.Tag.Equals("Root"))
                    {
                        ContextMenu_Folders.Show(Cursor.Position);
                        if (TreeNodeFunctions.FindRootNode(selectedTreeViewNode).Name.Equals("AudioData"))
                        {
                            ContextMenuFolder_AddSound.Visible = false;
                            ContextMenuFolder_AddAudio.Visible = true;
                        }
                        else
                        {
                            ContextMenuFolder_AddSound.Visible = true;
                            ContextMenuFolder_AddAudio.Visible = false;
                        }
                    }
                    else if (selectedTreeViewNode.Tag.Equals("Sound"))
                    {
                        ContextMenu_Sound.Show(Cursor.Position);
                    }
                    else if (selectedTreeViewNode.Tag.Equals("Sample"))
                    {
                        ContextMenu_Sample.Show(Cursor.Position);
                    }
                    else if (selectedTreeViewNode.Tag.Equals("Audio"))
                    {
                        ContextMenu_Audio.Show(Cursor.Position);
                    }
                }
            }
        }

        private void TreeView_File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Get node
            TreeNode selectedTreeViewNode = TreeView_File.SelectedNode;

            //Check clicked node
            if (selectedTreeViewNode != null)
            {
                //Open Properties
                switch (selectedTreeViewNode.Tag)
                {
                    case "Sample":
                        OpenSelectedNodeSampleProperties(selectedTreeViewNode);
                        break;
                    case "Audio":
                        OpenAudioProperties(selectedTreeViewNode);
                        break;
                    case "Sound":
                        OpenSoundProperties(selectedTreeViewNode);
                        break;
                }
            }
        }

        //*===============================================================================================
        //* MenuItem VIEW
        //*===============================================================================================
        private void MenuItemView_CollapseTree_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;
            TreeView_File.CollapseAll();
        }

        //*===============================================================================================
        //* HOT KEYS
        //*===============================================================================================
        private void TreeView_File_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode selectedNode = TreeView_File.SelectedNode;

            //Rename selected Node
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_File, selectedNode);
                ProjectInfo.FileHasBeenModified = true;
            }
            //Delete selected Node
            if (e.KeyCode == Keys.Delete)
            {
                if (selectedNode.Tag.Equals("Sound"))
                {
                    RemoveSoundSelectedNode(selectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else if (selectedNode.Tag.Equals("Sample"))
                {
                    RemoveSampleSelectedNode(selectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else if (selectedNode.Tag.Equals("Audio"))
                {
                    RemoveAudioAndWarningDependencies(selectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    RemoveFolderSelectedNode(selectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }
    }
}