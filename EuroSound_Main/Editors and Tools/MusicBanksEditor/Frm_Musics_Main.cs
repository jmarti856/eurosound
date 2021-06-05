using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls;
using EuroSound_Application.CustomControls.DebugTypes;
using EuroSound_Application.CustomControls.ProjectSettings;
using EuroSound_Application.CustomControls.SearcherForm;
using EuroSound_Application.Editors_and_Tools;
using EuroSound_Application.Editors_and_Tools.ApplicationTargets;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_Musics_Main : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        internal Dictionary<uint, EXAppTarget> OutputTargets = new Dictionary<uint, EXAppTarget>();
        public Dictionary<uint, EXMusic> MusicsList = new Dictionary<uint, EXMusic>();
        public ProjectFile ProjectInfo = new ProjectFile();
        internal string CurrentFilePath = string.Empty;
        private string ProjectName;
        private bool FormMustBeClosed = false;
        private bool CanExpandChildNodes = true;
        private EuroSoundFiles EuroSoundFilesFunctions = new EuroSoundFiles();
        private MostRecentFilesMenu RecentFilesMenu;
        private Thread UpdateWavList, LoadMusicFile;
        private System.Timers.Timer TimerBackups;

        // The undo and redo history lists.
        private Stack<object> UndoListMusics = new Stack<object>();
        private Stack<TreeNode> UndoListNodes = new Stack<TreeNode>();

        public Frm_Musics_Main(string Name, string FilePath, MostRecentFilesMenu RecentFiles)
        {
            InitializeComponent();
            CurrentFilePath = FilePath;
            ProjectName = Name;
            RecentFilesMenu = RecentFiles;

            //Menu Item: File
            MenuItem_File_Close.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_Close")); };
            MenuItem_File_Save.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_File_Save")); };
            MenuItem_File_SaveAs.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_File_SaveAs")); };
            MenuItem_File_Export.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_File_Export")); };
            MenuItemFile_ReadMusic.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ReadSound")); };
            MenuItemFile_ReadYml.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ReadYml")); };
            MenuItemFile_ReadESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ImportESIF")); };

            MenuItem_File_Close.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Save.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_SaveAs.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Export.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_ReadMusic.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_ReadYml.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_ReadESIF.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Menu Item: Edit
            MenuItem_Edit.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MenuItem_Edit.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItem_Edit_FileProps.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItem_Edit_FileProps")); };
            MenuItem_Edit_Undo.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemEdit_Undo")); };
            MenuItem_Edit_Search.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankSearch")); };

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Undo.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Search.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Context Menu Folders
            ContextMenuFolder_NewFolder.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolders_New")); };
            ContextMenuFolder_Delete.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Delete")); };
            ContextMenuFolder_SortItems.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Sort")); };
            ContextMenuFolder_ExpandAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_ExpandAll")); };
            ContextMenuFolder_CollapseAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_CollapseAll")); };
            ContextMenuFolder_AddMusic.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_AddSound")); };
            ContextMenuFolder_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Rename")); };
            ContextMenuFolder_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Folders.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //Context Menu Musics
            ContextMenuMusics_Delete.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuMusic_Remove")); };
            ContextMenuMusics_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuMusic_Rename")); };
            ContextMenuMusics_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuMusic_Properties")); };
            ContextMenuMusics_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuMusics_ExportESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenu_Musics.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //Buttons
            Button_UpdateProperties.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankCheckAudios")); };
            Button_UpdateProperties.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_Generate_Hashcodes.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonCalculateJumpCodes")); };
            Button_Generate_Hashcodes.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_ExportInterchangeFile.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("Button_ExportInterchangeFile")); };
            Button_ExportInterchangeFile.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_Musics_Main_Load(object sender, EventArgs e)
        {
            //Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Type of data that creates this form
            ProjectInfo.TypeOfData = (int)Enumerations.ESoundFileType.MusicBanks;

            //Check Hashcodes are not null
            if (Hashcodes.MFX_Defines.Keys.Count == 0)
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESFFile"));

                //Load Data
                Thread LoadHashcodes = new Thread(() => Hashcodes.LoadMusicHashcodes(GlobalPreferences.HT_MusicPath))
                {
                    IsBackground = true
                };
                LoadHashcodes.Start();
            }

            //Load Last State
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                if (Convert.ToBoolean(WindowStateConfig.GetValue("MView_IsIconic", 0)))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (Convert.ToBoolean(WindowStateConfig.GetValue("MView_IsMaximized", 0)))
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("MView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("MView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("MView_Width", 974));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("MView_Height", 680));
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
                EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_MusicData, MusicsList, null, OutputTargets, Path.Combine(GlobalPreferences.MakeBackupsDirectory, string.Join("", "ES_BackUp", GlobalPreferences.MakeBackupsIndex, ".ESF")), ProjectInfo);
                GlobalPreferences.MakeBackupsIndex++;
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Frm_Musics_Main_Shown(object sender, EventArgs e)
        {
            if (FormMustBeClosed)
            {
                Close();
            }
            else
            {
                //Show HashCode Group
                Textbox_GroupHashCode.Text = string.Join("", "0x", GlobalPreferences.MusicPrefix.ToString("X8"));

                //Apply Splitter Distance
                using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
                {
                    SplitContainerMusicsForm.SplitterDistance = Convert.ToInt32(WindowStateConfig.GetValue("MView_SplitterDistance", 370));
                    SplitContainer_LateralPanels.SplitterDistance = Convert.ToInt32(WindowStateConfig.GetValue("MView_SplitterInfoDistance", 260));
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
                        LoadMusicFile = new Thread(() =>
                        {
                            //Disable button
                            Button_Generate_Hashcodes.Invoke((MethodInvoker)delegate
                            {
                                Button_Generate_Hashcodes.Enabled = false;
                            });

                            //Disable button
                            Button_UpdateProperties.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateProperties.Enabled = false;
                            });

                            //Disable button
                            Button_StopUpdate.Invoke((MethodInvoker)delegate
                            {
                                Button_StopUpdate.Enabled = false;
                            });

                            //Disable button
                            Button_ExportInterchangeFile.Invoke((MethodInvoker)delegate
                            {
                                Button_ExportInterchangeFile.Enabled = false;
                            });

                            //Check that the profile name matches with the current one
                            string ProfileName = EuroSoundFilesFunctions.LoadEuroSoundFile(TreeView_MusicData, MusicsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
                            if (!ProfileName.Equals(GlobalPreferences.SelectedProfileName))
                            {
                                FormMustBeClosed = true;
                            }

                            //Update File name label
                            UpdateStatusBarLabels();

                            //Disable button
                            if (!(Button_Generate_Hashcodes.Disposing || Button_Generate_Hashcodes.IsDisposed))
                            {
                                Button_Generate_Hashcodes.Invoke((MethodInvoker)delegate
                                {
                                    Button_Generate_Hashcodes.Enabled = true;
                                });
                            }

                            //Disable button
                            if (!(Button_UpdateProperties.Disposing || Button_UpdateProperties.IsDisposed))
                            {
                                Button_UpdateProperties.Invoke((MethodInvoker)delegate
                                {
                                    Button_UpdateProperties.Enabled = true;
                                });
                            }

                            //Disable button
                            if (!(Button_StopUpdate.Disposing || Button_StopUpdate.IsDisposed))
                            {
                                Button_StopUpdate.Invoke((MethodInvoker)delegate
                                {
                                    Button_StopUpdate.Enabled = true;
                                });
                            }

                            //Disable button
                            if (!(Button_ExportInterchangeFile.Disposing || Button_ExportInterchangeFile.IsDisposed))
                            {
                                Button_ExportInterchangeFile.Invoke((MethodInvoker)delegate
                                {
                                    Button_ExportInterchangeFile.Enabled = true;
                                });
                            }

                            //Set Program status
                            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
                        })
                        {
                            IsBackground = true
                        };
                        LoadMusicFile.Start();
                    }
                    else
                    {
                        //Check that the profile name matches with the current one
                        string ProfileName = EuroSoundFilesFunctions.LoadEuroSoundFile(TreeView_MusicData, MusicsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
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
                FontConverter cvt = new FontConverter();
                TreeView_MusicData.Indent = GlobalPreferences.TV_Indent;
                TreeView_MusicData.ItemHeight = GlobalPreferences.TV_ItemHeight;
                TreeView_MusicData.Font = cvt.ConvertFromString(GlobalPreferences.TV_SelectedFont) as Font;
                TreeView_MusicData.ShowLines = GlobalPreferences.TV_ShowLines;
                TreeView_MusicData.ShowRootLines = GlobalPreferences.TV_ShowRootLines;
            }
        }

        private void Frm_Musics_Main_Enter(object sender, EventArgs e)
        {
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

        private void Frm_Musics_Main_SizeChanged(object sender, EventArgs e)
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

        private void Frm_Musics_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_StoppingThreads"));

            //Stop timer
            if (TimerBackups != null)
            {
                TimerBackups.Stop();
                TimerBackups.Close();
                TimerBackups.Dispose();
            }

            //Clear stack lists
            UndoListMusics.Clear();
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
                            CurrentFilePath = OpenSaveAsDialog(TreeView_MusicData, MusicsList, ProjectInfo);
                        }
                        //Save Data
                        else
                        {
                            EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_MusicData, MusicsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
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

            WindowsRegistryFunctions.SaveWindowState("MView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized, SplitContainerMusicsForm.SplitterDistance, SplitContainer_LateralPanels.SplitterDistance);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            //Clear Last File Label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_LastFile");
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void UpdateStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName, "SBPanel_File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.MFX_Defines, ProjectInfo.Hashcode), "SBPanel_Hashcode");
        }

        private void ClearStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_Hashcode");
        }

        //*===============================================================================================
        //* MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, EventArgs e)
        {
            GlobalPreferences.StatusBar_ToolTipMode = false;

            Frm_FileProperties Props = new Frm_FileProperties(ProjectInfo, OutputTargets)
            {
                Owner = this,
                ShowInTaskbar = false,
                Tag = Tag
            };
            Props.ShowDialog();
        }

        //*===============================================================================================
        //* MENU FILE
        //*===============================================================================================
        private void MenuItem_File_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItem_File_Save_Click(object sender, EventArgs e)
        {
            //Check if we have a path for this file
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                CurrentFilePath = OpenSaveAsDialog(TreeView_MusicData, MusicsList, ProjectInfo);
            }
            //Save Data
            else
            {
                EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_MusicData, MusicsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
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
            CurrentFilePath = OpenSaveAsDialog(TreeView_MusicData, MusicsList, ProjectInfo);

            //Update text
            Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }

            //Update var
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItem_File_Export_Click(object sender, EventArgs e)
        {
            int debugOptions = 0;
            //Debug options form
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                using (EuroSound_DebugTypes DebugOpts = new EuroSound_DebugTypes(new string[] { "File start 1", "File start 2" }))
                {
                    DebugOpts.Owner = Owner;
                    if (DebugOpts.ShowDialog() == DialogResult.OK)
                    {
                        debugOptions = DebugOpts.CheckedOptions;
                    }
                }
            }
            //Target Selector
            using (EuroSound_OutputTargetSelector targetsSelector = new EuroSound_OutputTargetSelector(OutputTargets) { Owner = this })
            {
                targetsSelector.ShowDialog();
                if (targetsSelector.DialogResult == DialogResult.OK)
                {
                    //Build form file
                    using (Frm_OutputTargetFileBuilder buildSFX = new Frm_OutputTargetFileBuilder(ProjectInfo, targetsSelector.SelectedTarget, OutputTargets, debugOptions, Tag.ToString()) { Owner = this })
                    {
                        buildSFX.ShowDialog();
                    }
                }
            }
        }

        //*===============================================================================================
        //* Tree View Controls
        //*===============================================================================================
        private void TreeView_MusicData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (CanExpandChildNodes)
            {
                //Change node images depending of the type
                if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Folder || e.Node.Level == 0)
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 0, 0);
                }
                else if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Music)
                {
                    if (EXMusicsFunctions.MusicWillBeOutputed(MusicsList, e.Node.Name))
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 2, 2);
                    }
                    else
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 5, 5);
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TreeView_MusicData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (CanExpandChildNodes)
            {
                //Change node images depending of the type
                if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Folder || e.Node.Level == 0)
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 1, 1);
                }
                else if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Music)
                {
                    if (EXMusicsFunctions.MusicWillBeOutputed(MusicsList, e.Node.Name))
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 3, 3);
                    }
                    else
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 6, 6);
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void TreeView_MusicData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ToolsCommonFunctions.TreeViewNodeRename(TreeView_MusicData, ProjectInfo, e);
        }

        private void TreeView_MusicData_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode selectedNode = TreeView_MusicData.SelectedNode;

            //Rename selected Node
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_MusicData, selectedNode);
            }
            //Delete selected Node
            if (e.KeyCode == Keys.Delete)
            {
                //Check that is not a root node
                if (selectedNode.Level > 0)
                {
                    if (Convert.ToByte(selectedNode.Tag) == (byte)Enumerations.TreeNodeType.Music)
                    {
                        ToolsCommonFunctions.RemoveEngineXObject("Remove music:", (int)Enumerations.EXObjectType.EXMusic, TreeView_MusicData, null, selectedNode, MusicsList, ProjectInfo, UndoListMusics, UndoListNodes, MenuItem_Edit_Undo, Tag.ToString());
                    }
                    if (Convert.ToByte(selectedNode.Tag) == (byte)Enumerations.TreeNodeType.Target)
                    {
                        ToolsCommonFunctions.RemoveTargetSelectedNode(selectedNode, OutputTargets, TreeView_MusicData, ProjectInfo);
                    }
                    else
                    {
                        ToolsCommonFunctions.RemoveEngineXObject("Remove folder:", (int)Enumerations.EXObjectType.EXMusicFolder, TreeView_MusicData, null, TreeView_MusicData.SelectedNode, MusicsList, ProjectInfo, UndoListMusics, UndoListNodes, MenuItem_Edit_Undo, Tag.ToString());
                    }
                }
            }
        }

        private void TreeView_MusicData_MouseClick(object sender, MouseEventArgs e)
        {
            //Open context menu depending of the selected node
            if (e.Button == MouseButtons.Right)
            {
                //Select node
                TreeNode SelectedNode = TreeView_MusicData.GetNodeAt(e.X, e.Y);
                TreeView_MusicData.SelectedNode = SelectedNode;

                //Open contextual menu
                if (Convert.ToByte(SelectedNode.Tag) == (byte)Enumerations.TreeNodeType.Folder || SelectedNode.Level == 0)
                {
                    ContextMenu_Folders.Show(Cursor.Position);
                    if (TreeNodeFunctions.FindRootNode(SelectedNode).Name.Equals("Musics"))
                    {
                        ContextMenuFolder_AddMusic.Visible = true;
                        ContextMenuFolder_AddTarget.Visible = false;
                    }
                    else if (TreeNodeFunctions.FindRootNode(SelectedNode).Name.Equals("AppTargets"))
                    {
                        ContextMenuFolder_AddTarget.Visible = true;
                        ContextMenuFolder_AddMusic.Visible = false;
                    }
                }
                else if (Convert.ToByte(SelectedNode.Tag) == (byte)Enumerations.TreeNodeType.Music)
                {
                    ContextMenu_Musics.Show(Cursor.Position);
                }
                else if (Convert.ToByte(SelectedNode.Tag) == (byte)Enumerations.TreeNodeType.Target)
                {
                    ContextMenu_Targets.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_MusicData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Get node
            TreeNode selectedTreeViewNode = TreeView_MusicData.SelectedNode;

            if (selectedTreeViewNode != null)
            {
                //Open Properties
                switch (Convert.ToByte(selectedTreeViewNode.Tag))
                {
                    case (byte)Enumerations.TreeNodeType.Music:
                        OpenMusicPropertiesForm(selectedTreeViewNode);
                        break;
                    case (byte)Enumerations.TreeNodeType.Target:
                        OpenTargetProperties(selectedTreeViewNode);
                        break;
                }
            }
        }

        private void TreeView_MusicData_MouseDown(object sender, MouseEventArgs e)
        {
            //Expand child nodes only if is a folder
            if (TreeView_MusicData.SelectedNode != null)
            {
                CanExpandChildNodes = true;

                //Double click
                if (e.Clicks > 1)
                {
                    if (!(Convert.ToByte(TreeView_MusicData.SelectedNode.Tag) == (byte)Enumerations.TreeNodeType.Folder || TreeView_MusicData.SelectedNode.Level == 0))
                    {
                        CanExpandChildNodes = false;
                    }
                }
            }
        }

        private void MenuItem_Edit_Undo_Click(object sender, EventArgs e)
        {
            //Restore the first serialization from the undo list.
            if (UndoListMusics.Count > 0)
            {
                if (UndoListMusics.Peek().GetType() == typeof(KeyValuePair<uint, EXMusic>))
                {
                    //Get Objects
                    TreeNode NodeToAdd = UndoListNodes.Pop();
                    KeyValuePair<uint, EXMusic> ItemToRestore = (KeyValuePair<uint, EXMusic>)UndoListMusics.Pop();

                    //Check that object does not exists
                    bool NodeToAddExists = TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_MusicData, NodeToAdd.Text);
                    if (MusicsList.ContainsKey(ItemToRestore.Key) || NodeToAddExists == true)
                    {
                        MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because another item with the same name exists", NodeToAdd.Text), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //Get Node
                        MusicsList.Add(ItemToRestore.Key, ItemToRestore.Value);

                        //AddNode
                        TreeView_MusicData.Nodes[0].Nodes.Insert(NodeToAdd.Index, NodeToAdd);
                    }

                    //Enable or disable button
                    ToolsCommonFunctions.EnableUndo(MenuItem_Edit_Undo, UndoListNodes, ProjectInfo.TypeOfData);

                }
            }
        }

        private void TreeView_MusicData_DragDrop(object sender, DragEventArgs e)
        {
            ToolsCommonFunctions.NodesDragginDrop(ProjectInfo, TreeView_MusicData, e);
        }

        private void TreeView_MusicData_DragEnter(object sender, DragEventArgs e)
        {
            ToolsCommonFunctions.NodesDraggin_Enter(ProjectInfo, TreeView_MusicData, e);
        }

        private void TreeView_MusicData_DragOver(object sender, DragEventArgs e)
        {
            ToolsCommonFunctions.NodesDragginOver(TreeView_MusicData, e);
        }

        private void TreeView_MusicData_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void ListView_WavHeaderData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_WavHeaderData.SelectedItems.Count > 0)
            {
                TreeNode[] SelectedNode = TreeView_MusicData.Nodes.Find(ListView_WavHeaderData.SelectedItems[0].Tag.ToString(), true);
                if (SelectedNode.Length > 0)
                {
                    OpenMusicPropertiesForm(SelectedNode[0]);
                }
            }
        }

        //*===============================================================================================
        //* Buttons Events
        //*===============================================================================================
        private void Button_UpdateProperties_Click(object sender, EventArgs e)
        {
            UpdateWavDataList();
        }

        private void Button_StopUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateWavList != null)
            {
                UpdateWavList.Abort();
                ListView_WavHeaderData.Items.Clear();
                ListView_WavHeaderData.Enabled = true;
                Textbox_DataCount.Text = "0";
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void Button_ExportInterchangeFile_Click(object sender, EventArgs e)
        {
            string ExportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, ProjectInfo.FileName);
            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportProjectMusic(ExportPath, ProjectInfo, MusicsList, TreeView_MusicData);
            }
        }
        private void Button_Generate_Hashcodes_Click(object sender, EventArgs e)
        {
            //Clear textbox
            Rtbx_Jump_Music_Codes.Clear();

            foreach (EXMusic Music in MusicsList.Values)
            {
                if (Music.OutputThisSound)
                {
                    //Generate comment
                    string Comment = "// Music Jump Codes For Level " + Hashcodes.GetHashcodeLabel(Hashcodes.MFX_Defines, ProjectInfo.Hashcode) + "\n";
                    GenericFunctions.AppendTextToRichTextBox(Comment, Color.Green, Rtbx_Jump_Music_Codes);

                    //Get Music Name
                    string MusicHashcodeLabel = Hashcodes.GetHashcodeLabel(Hashcodes.MFX_Defines, ProjectInfo.Hashcode);
                    string MusicName;

                    if (MusicHashcodeLabel.StartsWith("MFX_"))
                    {
                        MusicName = MusicHashcodeLabel.Substring(4);
                    }
                    else
                    {
                        MusicName = MusicHashcodeLabel;
                    }

                    //Search Goto Loop Start Postition
                    uint LoopPos = 0;
                    for (int j = 0; j < Music.Markers.Count; j++)
                    {
                        if (Music.Markers[j].MusicMakerType == (int)Enumerations.EXMarkerType.Goto)
                        {
                            LoopPos = Music.Markers[j].LoopStart;
                        }
                    }

                    //Print Hashcodes
                    int StartMarkersCount = 1;
                    for (int i = 0; i < Music.Markers.Count; i++)
                    {
                        string JumpHashcodeLabel = string.Empty;
                        if (Music.Markers[i].MusicMakerType == (int)Enumerations.EXMarkerType.Goto)
                        {
                            JumpHashcodeLabel = string.Join("", "JMP_GOTO_", MusicName, "_LOOP");
                        }
                        if (Music.Markers[i].MusicMakerType == (int)Enumerations.EXMarkerType.Start && Music.Markers[i].Position != LoopPos)
                        {
                            if (i == 0)
                            {
                                JumpHashcodeLabel = string.Join("", "JMP_", MusicName, "_START");
                            }
                            else
                            {
                                JumpHashcodeLabel = string.Join("", "JMP_", MusicName, "_Start" + StartMarkersCount);
                                StartMarkersCount++;
                            }
                        }
                        if (Music.Markers[i].MusicMakerType == (int)Enumerations.EXMarkerType.Start && Music.Markers[i].Position == LoopPos)
                        {
                            JumpHashcodeLabel = string.Join("", "JMP_", MusicName, "_LOOP");
                        }

                        //Calculate Jump HashCode
                        uint JumpHashcode = Convert.ToUInt32((int)GlobalPreferences.MusicPrefix | ((i & 0xFF) << 8) | (((int)ProjectInfo.Hashcode & 0xFF << 0)));
                        if (!string.IsNullOrEmpty(JumpHashcodeLabel))
                        {
                            GenericFunctions.AppendTextToRichTextBox("#define " + JumpHashcodeLabel + " 0x" + JumpHashcode.ToString("X8") + "\n", Color.Brown, Rtbx_Jump_Music_Codes);
                        }
                    }
                }
            }
        }

        private void ContextMenuFolder_AddTarget_Click(object sender, EventArgs e)
        {
            EXAppTarget outTarget = new EXAppTarget
            {
                BinaryName = EXAppTarget_Functions.GetBinaryName(ProjectInfo, GlobalPreferences.SelectedProfileName)
            };

            using (Frm_ApplicationTarget newOutTarget = new Frm_ApplicationTarget(outTarget) { Owner = this })
            {
                newOutTarget.ShowDialog();

                if (newOutTarget.DialogResult == DialogResult.OK)
                {
                    uint SoundID = GenericFunctions.GetNewObjectID(ProjectInfo);
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_MusicData.SelectedNode.Name, SoundID.ToString(), outTarget.Name, 10, 10, (byte)Enumerations.TreeNodeType.Target, true, true, false, SystemColors.WindowText, TreeView_MusicData);

                    //Add Target
                    OutputTargets.Add(SoundID, outTarget);
                }
            }
        }

        //*===============================================================================================
        //* MENUS
        //*===============================================================================================
        private void MenuItemFile_ReadESIF_Click(object sender, EventArgs e)
        {
            string FilePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Interchange File (*.ESIF)|*.esif", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                ESIF_MusicBankFile EuroSoundPropertiesFileLoader = new ESIF_MusicBankFile();
                IList<string> ImportResults = EuroSoundPropertiesFileLoader.LoadMusicBank_File(FilePath, ProjectInfo, MusicsList, TreeView_MusicData);
                if (ImportResults.Count > 0)
                {
                    GenericFunctions.ShowErrorsAndWarningsList(ImportResults, "Import Results", this);
                }
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
    }
}
