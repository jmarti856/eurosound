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
using EuroSound_Application.StreamSounds.YMLReader;
using EuroSound_Application.TreeViewLibraryFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_Main : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        internal Dictionary<uint, EXAppTarget> OutputTargets = new Dictionary<uint, EXAppTarget>();
        public Dictionary<uint, EXSoundStream> StreamSoundsList = new Dictionary<uint, EXSoundStream>();
        public ProjectFile ProjectInfo = new ProjectFile();
        internal string CurrentFilePath = string.Empty;
        private string ProjectName;
        private bool FormMustBeClosed = false;
        private bool CanExpandChildNodes = true;
        private EuroSoundFiles EuroSoundFilesFunctions = new EuroSoundFiles();
        private StreamSoundsYMLReader LibYamlReader = new StreamSoundsYMLReader();
        private Thread UpdateWavList, LoadStreamFile;
        private MostRecentFilesMenu RecentFilesMenu;
        private System.Timers.Timer TimerBackups;

        // The undo and redo history lists.
        private Stack<object> UndoListSounds = new Stack<object>();
        private Stack<TreeNode> UndoListNodes = new Stack<TreeNode>();

        public Frm_StreamSounds_Main(string Name, string FilePath, MostRecentFilesMenu RecentFiles)
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
            MenuItemFile_ReadSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ReadSound")); };
            MenuItemFile_ReadYml.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ReadYml")); };
            MenuItemFile_ReadESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("MenuItemFile_ImportESIF")); };

            MenuItem_File_Close.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Save.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_SaveAs.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Export.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_ReadSound.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
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

            //Buttons
            Button_UpdateList_WavData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonSoundsBankCheckAudios")); };
            Button_UpdateList_WavData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_ExportInterchangeFile.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("Button_ExportInterchangeFile")); };
            Button_ExportInterchangeFile.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //Context Menu Folders
            ContextMenuFolder_ExpandAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_ExpandAll")); };
            ContextMenuFolder_CollapseAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_CollapseAll")); };
            ContextMenuFolder_AddSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_AddSound")); };
            ContextMenuFolder_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_Rename")); };
            ContextMenuFolder_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Folders.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //Context Menu Sounds
            ContextMenuSounds_Delete.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_Remove")); };
            ContextMenuSounds_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_Rename")); };
            ContextMenuSounds_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_Properties")); };
            ContextMenuSounds_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuSounds_ExportESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenuSounds_MoveDown.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuStreamSound_MoveDown")); };
            ContextMenuSounds_MoveUp.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.resourcesManager.GetString("ContextMenuStreamSound_MoveUp")); };
            ContextMenu_Sounds.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSoundsEditorMain_Load(object sender, EventArgs e)
        {
            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Type of data that creates this form
            ProjectInfo.TypeOfData = (int)Enumerations.ESoundFileType.StreamSounds;

            //Check Hashcodes are not null
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Count == 0)
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESFFile"));

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

            ProjectInfo.Hashcode = GlobalPreferences.StreamFileHashCode;

            //Load Last State
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                if (Convert.ToBoolean(WindowStateConfig.GetValue("SSView_IsIconic", 0)))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (Convert.ToBoolean(WindowStateConfig.GetValue("SSView_IsMaximized", 0)))
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("SSView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("SSView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("SSView_Width", 974));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("SSView_Height", 680));
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
                EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_StreamData, StreamSoundsList, null, OutputTargets, Path.Combine(GlobalPreferences.MakeBackupsDirectory, string.Join("", "ES_BackUp", GlobalPreferences.MakeBackupsIndex, ".ESF")), ProjectInfo);
                GlobalPreferences.MakeBackupsIndex++;
            }

            //Set Program status
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
        }


        private void Frm_StreamSoundsEditorMain_Shown(object sender, EventArgs e)
        {
            if (FormMustBeClosed)
            {
                Close();
            }
            else
            {
                //Show HashCode Group
                Textbox_GroupHashCode.Text = string.Join("", "0x", GlobalPreferences.SfxPrefix.ToString("X8"));

                //Apply Splitter Distance
                using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
                {
                    SplitContainerStreamSoundsForm.SplitterDistance = Convert.ToInt32(WindowStateConfig.GetValue("SSView_SplitterDistance", 370));
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
                        LoadStreamFile = new Thread(() =>
                        {
                            //Disable Button
                            Button_UpdateList_WavData.Invoke((MethodInvoker)delegate
                            {
                                Button_UpdateList_WavData.Enabled = false;
                            });

                            //Disable Button
                            Button_ExportInterchangeFile.Invoke((MethodInvoker)delegate
                            {
                                Button_ExportInterchangeFile.Enabled = false;
                            });

                            //Check that the profile name matches with the current one
                            string ProfileName = EuroSoundFilesFunctions.LoadEuroSoundFile(TreeView_StreamData, StreamSoundsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
                            if (!ProfileName.Equals(GlobalPreferences.SelectedProfileName))
                            {
                                FormMustBeClosed = true;
                            }

                            //Update File name label
                            UpdateStatusBarLabels();

                            //Enable Button
                            if (!(Button_UpdateList_WavData.Disposing || Button_UpdateList_WavData.IsDisposed))
                            {
                                Button_UpdateList_WavData.Invoke((MethodInvoker)delegate
                                {
                                    Button_UpdateList_WavData.Enabled = true;
                                });
                            }

                            //Enable Button
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
                        LoadStreamFile.Start();
                    }
                    else
                    {
                        //Check that the profile name matches with the current one
                        string ProfileName = EuroSoundFilesFunctions.LoadEuroSoundFile(TreeView_StreamData, StreamSoundsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
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
                TreeView_StreamData.Indent = GlobalPreferences.TV_Indent;
                TreeView_StreamData.ItemHeight = GlobalPreferences.TV_ItemHeight;
                TreeView_StreamData.Font = new FontConverter().ConvertFromString(GlobalPreferences.TV_SelectedFont) as Font;
                TreeView_StreamData.ShowLines = GlobalPreferences.TV_ShowLines;
                TreeView_StreamData.ShowRootLines = GlobalPreferences.TV_ShowRootLines;
            }
        }

        private void Frm_StreamSoundsEditorMain_Enter(object sender, System.EventArgs e)
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

        private void Frm_StreamSoundsEditorMain_SizeChanged(object sender, System.EventArgs e)
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

        private void Frm_StreamSoundsEditorMain_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                            CurrentFilePath = OpenSaveAsDialog(TreeView_StreamData, StreamSoundsList, ProjectInfo);
                        }
                        //Save Data
                        else
                        {
                            EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_StreamData, StreamSoundsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
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
            WindowsRegistryFunctions.SaveWindowState("SSView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized, SplitContainerStreamSoundsForm.SplitterDistance, 0);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            //Clear Last File Label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_LastFile");
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================

        private void Button_ExportInterchangeFile_Click(object sender, EventArgs e)
        {
            string ExportPath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, ProjectInfo.FileName);
            if (!string.IsNullOrEmpty(ExportPath))
            {
                ESIF_Exporter ESIF_Exp = new ESIF_Exporter();
                ESIF_Exp.ExportProjectStream(ExportPath, ProjectInfo, StreamSoundsList, TreeView_StreamData);
            }
        }

        private void Button_UpdateList_WavData_Click(object sender, System.EventArgs e)
        {
            //Check Audio properties
            UpdateWavDataList();
        }

        private void ListView_WavHeaderData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_WavHeaderData.SelectedItems.Count > 0)
            {
                TreeNode[] SelectedNode = TreeView_StreamData.Nodes.Find(ListView_WavHeaderData.SelectedItems[0].Tag.ToString(), true);
                if (SelectedNode.Length > 0)
                {
                    OpenSoundPropertiesForm(SelectedNode[0]);
                }
            }
        }

        //*===============================================================================================
        //* Tree View Controls
        //*===============================================================================================
        private void TreeView_StreamData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ToolsCommonFunctions.TreeViewNodeRename(TreeView_StreamData, ProjectInfo, e, OutputTargets);
        }

        private void TreeView_StreamData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (CanExpandChildNodes)
            {
                //Change node images depending of the type
                if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Folder || e.Node.Level == 0)
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 0, 0);
                }
                else if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Sound)
                {
                    if (EXStreamSoundsFunctions.SoundWillBeOutputed(StreamSoundsList, e.Node.Name))
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

        private void TreeView_StreamData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (CanExpandChildNodes)
            {
                //Change node images depending of the type
                if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Folder || e.Node.Level == 0)
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 1, 1);
                }
                else if (Convert.ToByte(e.Node.Tag) == (byte)Enumerations.TreeNodeType.Sound)
                {
                    if (EXStreamSoundsFunctions.SoundWillBeOutputed(StreamSoundsList, e.Node.Name))
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

        private void TreeView_StreamData_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode selectedNode = TreeView_StreamData.SelectedNode;

            //Rename selected Node
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, selectedNode);
            }
            //Delete selected Node
            if (e.KeyCode == Keys.Delete)
            {
                //Check that is not a root node
                if (selectedNode.Level > 0)
                {
                    if (Convert.ToByte(selectedNode.Tag) == (byte)Enumerations.TreeNodeType.Sound)
                    {
                        ToolsCommonFunctions.RemoveEngineXObject("Remove sound:", 0, TreeView_StreamData, selectedNode, StreamSoundsList, null, ProjectInfo, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo, Tag.ToString());
                    }
                    else if (Convert.ToByte(selectedNode.Tag) == (byte)Enumerations.TreeNodeType.Target)
                    {
                        ToolsCommonFunctions.RemoveTargetSelectedNode(selectedNode, OutputTargets, TreeView_StreamData, ProjectInfo);
                    }
                }
            }
        }

        private void TreeView_StreamData_MouseClick(object sender, MouseEventArgs e)
        {
            //Open context menu depending of the selected node
            if (e.Button == MouseButtons.Right)
            {
                //Select node
                TreeNode SelectedTreeViewNode = TreeView_StreamData.GetNodeAt(e.X, e.Y);
                TreeView_StreamData.SelectedNode = SelectedTreeViewNode;

                //Open contextual menu
                if (Convert.ToByte(SelectedTreeViewNode.Tag) == (byte)Enumerations.TreeNodeType.Folder || SelectedTreeViewNode.Level == 0)
                {
                    ContextMenu_Folders.Show(Cursor.Position);
                    if (TreeNodeFunctions.FindRootNode(SelectedTreeViewNode).Name.Equals("Sounds"))
                    {
                        ContextMenuFolder_AddTarget.Visible = false;
                        ContextMenuFolder_AddSound.Visible = true;
                    }
                    else if (TreeNodeFunctions.FindRootNode(SelectedTreeViewNode).Name.Equals("AppTargets"))
                    {
                        ContextMenuFolder_AddTarget.Visible = true;
                        ContextMenuFolder_AddSound.Visible = false;
                    }
                }
                else if (Convert.ToByte(SelectedTreeViewNode.Tag) == (byte)Enumerations.TreeNodeType.Sound)
                {
                    ContextMenu_Sounds.Show(Cursor.Position);
                }
                else if (Convert.ToByte(SelectedTreeViewNode.Tag) == (byte)Enumerations.TreeNodeType.Target)
                {
                    ContextMenu_Targets.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_StreamData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Get node
            TreeNode SelectedTreeViewNode = TreeView_StreamData.SelectedNode;

            //Check clicked node
            if (SelectedTreeViewNode != null)
            {
                //Open Properties
                switch (Convert.ToByte(SelectedTreeViewNode.Tag))
                {
                    case (byte)Enumerations.TreeNodeType.Sound:
                        OpenSoundPropertiesForm(SelectedTreeViewNode);
                        break;
                    case (byte)Enumerations.TreeNodeType.Target:
                        OpenTargetProperties(SelectedTreeViewNode);
                        break;
                }
            }
        }

        private void TreeView_StreamData_MouseDown(object sender, MouseEventArgs e)
        {
            //Expand child nodes only if is a folder
            if (TreeView_StreamData.SelectedNode != null)
            {
                CanExpandChildNodes = true;

                //Double click
                if (e.Clicks > 1)
                {
                    if (!(Convert.ToByte(TreeView_StreamData.SelectedNode.Tag) == (byte)Enumerations.TreeNodeType.Folder || TreeView_StreamData.SelectedNode.Level == 0))
                    {
                        CanExpandChildNodes = false;
                    }
                }
            }
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*===============================================================================================
        private void MenuItem_File_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItem_File_Save_Click(object sender, System.EventArgs e)
        {
            //Check if we have a path for this file
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                CurrentFilePath = OpenSaveAsDialog(TreeView_StreamData, StreamSoundsList, ProjectInfo);
            }
            //Save Data
            else
            {
                EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_StreamData, StreamSoundsList, null, OutputTargets, CurrentFilePath, ProjectInfo);
            }
            ProjectInfo.FileHasBeenModified = false;

            Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void MenuItem_File_SaveAs_Click(object sender, System.EventArgs e)
        {
            CurrentFilePath = OpenSaveAsDialog(TreeView_StreamData, StreamSoundsList, ProjectInfo);

            //Update text
            Text = GenericFunctions.UpdateProjectFormText(CurrentFilePath, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }

            //Update var
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItem_File_Export_Click(object sender, System.EventArgs e)
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

        private void MenuItemFile_ReadESIF_Click(object sender, EventArgs e)
        {
            string FilePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Interchange File (*.ESIF)|*.esif", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                ESIF_StreamBankFile EuroSoundPropertiesFileLoader = new ESIF_StreamBankFile();
                IList<string> ImportResults = EuroSoundPropertiesFileLoader.LoadStreamSoundBank_File(FilePath, ProjectInfo, StreamSoundsList, TreeView_StreamData);
                if (ImportResults.Count > 0)
                {
                    GenericFunctions.ShowErrorsAndWarningsList(ImportResults, "Import Results", this);
                }
            }
        }

        private void MenuItemFile_ReadYml_Click(object sender, EventArgs e)
        {
            string FilePath = BrowsersAndDialogs.FileBrowserDialog("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                //--Ask user for a fully reimport--
                DialogResult ReimportQuestion = MessageBox.Show(GenericFunctions.resourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ReimportQuestion == DialogResult.Yes)
                {
                    //Clear Data
                    ProjectInfo.ClearStreamSoundStoredData(StreamSoundsList, TreeView_StreamData);
                }

                //--Load New data--
                Thread LoadYamlFile = new Thread(() => LibYamlReader.LoadDataFromSwyterUnpacker(StreamSoundsList, TreeView_StreamData, FilePath, ProjectInfo))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();
            }
        }

        private void MenuItemFile_ReadSound_Click(object sender, EventArgs e)
        {
            string FilePath = BrowsersAndDialogs.FileBrowserDialog("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                string SoundName = Path.GetFileNameWithoutExtension(FilePath);
                LibYamlReader.ReadYmlFile(StreamSoundsList, TreeView_StreamData, FilePath, SoundName, ProjectInfo);
            }
        }

        //*===============================================================================================
        //* MAIN MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, System.EventArgs e)
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

        private void MenuItem_Edit_Undo_Click(object sender, System.EventArgs e)
        {
            //Restore the first serialization from the undo list.
            if (UndoListSounds.Count > 0)
            {
                if (UndoListSounds.Peek().GetType() == typeof(KeyValuePair<uint, EXSoundStream>))
                {
                    //Get Objects
                    TreeNode NodeToAdd = UndoListNodes.Pop();
                    KeyValuePair<uint, EXSoundStream> ItemToRestore = (KeyValuePair<uint, EXSoundStream>)UndoListSounds.Pop();

                    //Check that object does not exists
                    bool NodeToAddExists = TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, NodeToAdd.Text);
                    if (StreamSoundsList.ContainsKey(ItemToRestore.Key) || NodeToAddExists == true)
                    {
                        MessageBox.Show(string.Format("The object \"{0}\" could not be recovered because another item with the same name exists", NodeToAdd.Text), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //Get Node
                        StreamSoundsList.Add(ItemToRestore.Key, ItemToRestore.Value);

                        //AddNode
                        TreeView_StreamData.Nodes[0].Nodes.Insert(NodeToAdd.Index, NodeToAdd);
                    }

                    //Enable or disable button
                    ToolsCommonFunctions.EnableUndo(MenuItem_Edit_Undo, UndoListNodes, ProjectInfo.TypeOfData);
                }
            }
        }

        private void MenuItem_Edit_Search_Click(object sender, System.EventArgs e)
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
