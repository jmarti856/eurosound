using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.AudioFunctionsLibrary;
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
using System.Text.RegularExpressions;
using System.Threading;
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
        private readonly Regex sWhitespace = new Regex(@"\s+");
        private string ProjectName;
        private bool FormMustBeClosed = false;
        private WindowsRegistryFunctions WRegFunctions = new WindowsRegistryFunctions();
        private EuroSoundFiles EuroSoundFilesFunctions = new EuroSoundFiles();
        private Thread UpdateList, UpdateWavList, UpdateStreamDataList, LoadYamlFile;
        private SoundBanksYMLReader LibYamlReader = new SoundBanksYMLReader();
        private AudioFunctions AudioFunctionsLibrary = new AudioFunctions();
        private MostRecentFilesMenu RecentFilesMenu;

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
            MenuItem_File_Close.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_Close")); };
            MenuItem_File_Save.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_Save")); };
            MenuItem_File_SaveAs.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_SaveAs")); };
            MenuItem_File_Export.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_Export")); };
            MenuItem_File_ImportYML_Single.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ReadSound")); };
            MenuItem_File_ImportYML_List.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ReadYml")); };
            MenuItem_File_ImportESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ImportESIF")); };

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

            MenuItem_Edit_Search.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankSearch")); };
            MenuItem_Edit_Undo.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemEdit_UndoSoundbanks")); };
            MenuItem_Edit_FileProps.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_Edit_FileProps")); };

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Undo.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Search.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: View
            MenuItemView_CollapseTree.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemView_CollapseTree")); };

            MenuItemView_CollapseTree.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Buttons
            Button_GenerateList.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonGenerateSoundsList")); };
            Button_GenerateList.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_Hashcodes.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankCheckHashcodes")); };
            Button_UpdateList_Hashcodes.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_WavData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankCheckAudios")); };
            Button_UpdateList_WavData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_StreamData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankCheckStreamData")); };
            Button_UpdateList_StreamData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopHashcodeUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopHashcodeUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_Stop_WavUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonStopListUpdate")); };
            Button_Stop_WavUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopStreamData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopStreamData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_ExportInterchangeFile.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("Button_ExportInterchangeFile")); };
            Button_ExportInterchangeFile.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Folders
            ContextMenuFolders_Folder.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(""); };
            ContextMenuFolders_New.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolders_New")); };
            ContextMenuFolder_ExpandAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_ExpandAll")); };
            ContextMenuFolder_CollapseAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_CollapseAll")); };
            ContextMenuFolder_Delete.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_Delete")); };
            ContextMenuFolder_Sort.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_Sort")); };
            ContextMenuFolder_Move.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_Move")); };
            ContextMenuFolder_AddSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_AddSound")); };
            ContextMenuFolder_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_Rename")); };
            ContextMenuFolder_AddAudio.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_AddAudio")); };
            ContextMenuFolder_Purge.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_Purge")); };
            ContextMenuFolder_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuFolder_ExportSounds.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenu_Folders.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Sound
            ContextMenuSound_AddSample.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_AddSample")); };
            ContextMenuSound_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Remove")); };
            ContextMenuSound_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Rename")); };
            ContextMenuSound_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Properties")); };
            ContextMenuSound_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuSound_ExportSingle.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenu_Sound.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Samples
            ContextMenuSample_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSample_Remove")); };
            ContextMenuSample_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSample_Rename")); };
            ContextMenuSample_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSample_Properties")); };
            ContextMenuSample_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Sample.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //ContextMenu_Audio
            ContextMenuAudio_Usage.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuAudio_Usage")); };
            ContextMenuAudio_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuAudio_OpenProperties")); };
            ContextMenuAudio_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuAudio_Delete")); };
            ContextMenuAudio_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuAudio_Rename")); };
            ContextMenuAudio_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Audio.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_Soundbanks_Main_Load(object sender, EventArgs e)
        {
            string ProfileName;

            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Load Preferences
            using (RegistryKey WindowStateConfig = WRegFunctions.ReturnRegistryKey("WindowState"))
            {
                bool IsIconic = Convert.ToBoolean(WindowStateConfig.GetValue("SBView_IsIconic", 0));
                bool IsMaximized = Convert.ToBoolean(WindowStateConfig.GetValue("SBView_IsMaximized", 0));

                if (IsIconic)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (IsMaximized)
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

            //Check Hashcodes are not null
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Keys.Count == 0)
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

            //Load ESF file if nedded
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                ProjectInfo.FileName = ProjectName;
            }
            else
            {
                ProfileName = EuroSoundFilesFunctions.LoadSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, CurrentFilePath, ProjectInfo);

                //Check that the profile name matches with the current one
                if (!ProfileName.Equals(GlobalPreferences.SelectedProfileName))
                {
                    FormMustBeClosed = true;
                }
            }
        }

        private void Frm_Soundbanks_Main_Shown(object sender, EventArgs e)
        {
            if (FormMustBeClosed)
            {
                Close();
            }
            else
            {
                //Update from title
                Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
                if (WindowState != FormWindowState.Maximized)
                {
                    MdiParent.Text = "EuroSound - " + Text;
                }

                //Set Program status
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

                //Update File name label
                UpdateStatusBarLabels();

                //Apply User Preferences
                FontConverter cvt = new FontConverter();
                TreeView_File.Indent = GlobalPreferences.TV_Indent;
                TreeView_File.ItemHeight = GlobalPreferences.TV_ItemHeight;
                TreeView_File.Font = cvt.ConvertFromString(GlobalPreferences.TV_SelectedFont) as Font;
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
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_StoppingThreads"));

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

            WRegFunctions.SaveWindowState("SBView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Button_GenerateList_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            string SavePath = BrowsersAndDialogs.SaveFileBrowser("YML Files (*.yml)|*.yml", 1, true, ProjectName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                StreamWriter file = new StreamWriter(SavePath);
                file.WriteLine("#ftype:1");
                file.WriteLine("# swy: EngineX sound bank exported from " + Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, ProjectInfo.Hashcode) + " / " + ProjectInfo.Hashcode);
                foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                {
                    file.WriteLine("- " + Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, Convert.ToUInt32(Sound.Value.Hashcode)));
                }
                file.Close();
                file.Dispose();
            }
        }

        private void Button_ExportInterchangeFile_Click(object sender, EventArgs e)
        {
            string ExportPath;

            ExportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, ProjectInfo.FileName);
            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportProject(ExportPath, true, ProjectInfo, SoundsList, AudioDataDict, TreeView_File);
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
                TreeNode[] SelectedNode = TreeView_File.Nodes.Find(ListView_Hashcodes.SelectedItems[0].Tag.ToString(), true);
                if (SelectedNode.Length > 0)
                {
                    OpenSoundProperties(SelectedNode[0]);
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
                TreeNode[] SelectedNode = TreeView_File.Nodes.Find(ListView_WavHeaderData.SelectedItems[0].Tag.ToString(), true);
                if (SelectedNode.Length > 0)
                {
                    OpenAudioProperties(SelectedNode[0]);
                }
            }
        }

        //*===============================================================================================
        //* MAIN MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;

            Frm_FileProperties Props = new Frm_FileProperties(ProjectInfo)
            {
                Owner = this,
                ShowInTaskbar = false,
                Tag = Tag
            };
            Props.ShowDialog();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void MenuItem_Edit_Undo_Click(object sender, EventArgs e)
        {
            //Restore the first serialization from the undo list.
            if (UndoListSounds.Count > 0)
            {
                //Get Node
                KeyValuePair<string, TreeNode> ItemWithTreeNode = UndoListNodes.Pop();
                TreeNode NodeToAdd = ItemWithTreeNode.Value;

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
                        TreeNode[] ParentNode = TreeView_File.Nodes.Find(ItemWithTreeNode.Key, true);
                        if (ParentNode.Length > 0)
                        {
                            //Add node
                            ParentNode[0].Nodes.Insert(NodeToAdd.Index, NodeToAdd);

                            //Get Node
                            SoundsList.Add(ItemToRestore.Key, ItemToRestore.Value);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because their parent \"{1}\" does not exists", NodeToAdd.Text, ItemWithTreeNode.Key), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        TreeNode[] ParentNode = TreeView_File.Nodes.Find(ItemWithTreeNode.Key, true);
                        if (ParentNode.Length > 0)
                        {
                            //Add node
                            ParentNode[0].Nodes.Insert(NodeToAdd.Index, NodeToAdd);

                            //Get Node
                            AudioDataDict.Add(ItemToRestore.Key, ItemToRestore.Value);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because their parent \"{1}\" does not exists", NodeToAdd.Text, ItemWithTreeNode.Key), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                //Enable or disable button
                EnableUndo();
            }
        }

        private void MenuItem_Edit_Search_Click(object sender, EventArgs e)
        {
            EuroSound_SearchItem Search = new EuroSound_SearchItem(Name)
            {
                Owner = this,
                Tag = Tag
            };
            Search.Show();
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*==============================================================================================
        private void MenuItem_File_ImportESIF_Click(object sender, EventArgs e)
        {
            string FilePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Interchange File (*.ESIF)|*.esif", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                ESIF_Loader EuroSoundPropertiesFileLoader = new ESIF_Loader();
                List<string> ImportResults = EuroSoundPropertiesFileLoader.LoadSFX_File(FilePath, ProjectInfo, SoundsList, AudioDataDict, TreeView_File);
                if (ImportResults.Count > 0)
                {
                    GenericFunctions.ShowErrorsAndWarningsList(ImportResults, "Import Results", this);
                }

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItem_File_ImportYML_List_Click(object sender, EventArgs e)
        {
            string FilePath = BrowsersAndDialogs.FileBrowserDialog("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                //Ask user for a fully reimport
                DialogResult ReimportQuestion = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ReimportQuestion == DialogResult.Yes)
                {
                    //Clear Data
                    ProjectInfo.ClearSoundBankStoredData(SoundsList, AudioDataDict, TreeView_File);

                    //Clear stack lists
                    UndoListSounds.Clear();
                    UndoListNodes.Clear();
                }

                //Update file Hashcode
                DialogResult QuestionAnswer = MessageBox.Show("Do you want to use the hashcode of the loaded file?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (QuestionAnswer == DialogResult.Yes)
                {
                    ProjectInfo.Hashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SB_Defines, Path.GetFileNameWithoutExtension(FilePath));
                    GenericFunctions.SetCurrentFileLabel(Path.GetFileNameWithoutExtension(FilePath), "Hashcode");
                }

                //Load New data
                LoadYamlFile = new Thread(() => LibYamlReader.LoadDataFromSwyterUnpacker(SoundsList, AudioDataDict, TreeView_File, FilePath, ProjectInfo))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItem_File_ImportYML_Single_Click(object sender, EventArgs e)
        {
            string SoundName;
            uint SoundHashcode;

            string FilePath = BrowsersAndDialogs.FileBrowserDialog("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                SoundName = new DirectoryInfo(Path.GetDirectoryName(FilePath)).Name;
                SoundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, SoundName);
                LibYamlReader.ReadYmlFile(SoundsList, AudioDataDict, TreeView_File, FilePath, SoundName, SoundHashcode, true, ProjectInfo);
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
                string FileName = "HC" + ProjectInfo.Hashcode.ToString("X8").Substring(2);

                //---[Output with debug options
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (EuroSound_DebugTypes DebugOpts = new EuroSound_DebugTypes(new string[] { "SFX Elements", "Sample info elements", "Sample Data" }))
                    {
                        DebugOpts.Owner = this.Owner;
                        if (DebugOpts.ShowDialog() == DialogResult.OK)
                        {
                            BuildSfxForm(FileName, DebugOpts.CheckedOptions);
                        }
                    }
                }
                else
                {

                    BuildSfxForm(FileName, 0);
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_BuildSFX_NoHashcode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildSfxForm(string FileName, int DebugFlags)
        {
            Frm_BuildSFXFile BuildFile = new Frm_BuildSFXFile(ProjectInfo, FileName, DebugFlags)
            {
                Tag = Tag,
                Owner = Owner,
                ShowInTaskbar = false
            };
            BuildFile.ShowDialog();
        }

        //*===============================================================================================
        //* TREE VIEW EVENTS
        //*===============================================================================================
        private void TreeView_File_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string LabelText;

            //Check that we have selected a node, and we have not selected the root folder
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                //Check label is not null, sometimes can crash without this check
                if (e.Label != null)
                {
                    //Get text label
                    LabelText = e.Label.Trim();

                    //Check we are not renaming with an empty string
                    if (string.IsNullOrEmpty(LabelText))
                    {
                        //Cancel edit
                        e.CancelEdit = true;
                    }
                    else
                    {
                        //Check that not exists an item with the same name
                        if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_File, LabelText))
                        {
                            MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.CancelEdit = true;
                        }
                        else
                        {
                            if (e.Node.Tag.Equals("Folder"))
                            {
                                e.Node.Name = sWhitespace.Replace(LabelText, "_");
                            }
                            e.Node.Text = LabelText;
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

        private void TreeView_File_BeforeExpand(object sender, TreeViewCancelEventArgs e)
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

        private void TreeView_File_DragDrop(object sender, DragEventArgs e)
        {
            string DestSection, SourceSection, DestNodeType;

            //Retrieve the client coordinates of the drop location.
            Point targetPoint = TreeView_File.PointToClient(new Point(e.X, e.Y));

            //Retrieve the node at the drop location.
            TreeNode targetNode = TreeView_File.GetNodeAt(targetPoint);
            TreeNode FindTargetNode = TreeNodeFunctions.FindRootNode(targetNode);

            TreeNode parentNode = targetNode;

            if (FindTargetNode != null)
            {
                DestSection = FindTargetNode.Text;
                DestNodeType = targetNode.Tag.ToString();

                //Retrieve the node that was dragged
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                SourceSection = TreeNodeFunctions.FindRootNode(draggedNode).Text;

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
                        if (SourceSection.Equals(DestSection) && (DestNodeType.Equals("Folder") || DestNodeType.Equals("Root")))
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
                TreeNode SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);
                TreeView_File.SelectedNode = SelectedNode;

                //Check the selected node
                if (SelectedNode.Tag.Equals("Folder") || SelectedNode.Tag.Equals("Root"))
                {
                    if (SelectedNode != null)
                    {
                        ContextMenu_Folders.Show(Cursor.Position);
                        if (TreeNodeFunctions.FindRootNode(SelectedNode).Name.Equals("AudioData"))
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
                }
                else if (SelectedNode.Tag.Equals("Sound"))
                {
                    ContextMenu_Sound.Show(Cursor.Position);
                }
                else if (SelectedNode.Tag.Equals("Sample"))
                {
                    ContextMenu_Sample.Show(Cursor.Position);
                }
                else if (SelectedNode.Tag.Equals("Audio"))
                {
                    ContextMenu_Audio.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Select node
            TreeNode SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);
            TreeView_File.SelectedNode = SelectedNode;

            if (SelectedNode.Tag.Equals("Sample"))
            {
                OpenSelectedNodeSampleProperties(SelectedNode);
            }
            if (SelectedNode.Tag.Equals("Audio"))
            {
                OpenAudioProperties(TreeView_File.SelectedNode);
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
            TreeNode SelectedNode = TreeView_File.SelectedNode;

            //Rename selected Node
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_File, SelectedNode);
                ProjectInfo.FileHasBeenModified = true;
            }
            //Delete selected Node
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedNode.Tag.Equals("Sound"))
                {
                    RemoveSoundSelectedNode(SelectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else if (SelectedNode.Tag.Equals("Sample"))
                {
                    RemoveSampleSelectedNode(SelectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else if (SelectedNode.Tag.Equals("Audio"))
                {
                    RemoveAudioAndWarningDependencies(SelectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
                else
                {
                    RemoveFolderSelectedNode(SelectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }
    }
}