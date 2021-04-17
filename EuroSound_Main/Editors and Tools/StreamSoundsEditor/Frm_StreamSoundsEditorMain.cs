using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.DebugTypes;
using EuroSound_Application.CustomControls.ProjectSettings;
using EuroSound_Application.CustomControls.SearcherForm;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundInterchangeFile;
using EuroSound_Application.StreamSounds.BuildSFX;
using EuroSound_Application.StreamSounds.YMLReader;
using EuroSound_Application.TreeViewLibraryFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSoundsEditorMain : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private WindowsRegistryFunctions WRegFunctions = new WindowsRegistryFunctions();
        public Dictionary<uint, EXSoundStream> StreamSoundsList = new Dictionary<uint, EXSoundStream>();
        private EuroSoundFiles SerializeInfo = new EuroSoundFiles();
        public ProjectFile ProjectInfo = new ProjectFile();
        private StreamSoundsYMLReader LibYamlReader = new StreamSoundsYMLReader();
        private AudioFunctions AudioLibrary = new AudioFunctions();
        private Thread UpdateImaData, UpdateWavList;
        private MostRecentFilesMenu RecentFilesMenu;
        private string FileToLoadArg, ProjectName;
        private string LoadedFile = string.Empty;
        private bool FormMustBeClosed = false;

        // The undo and redo history lists.
        private Stack<object> UndoListSounds = new Stack<object>();
        private Stack<TreeNode> UndoListNodes = new Stack<TreeNode>();

        public Frm_StreamSoundsEditorMain(string Name, string FilePath, MostRecentFilesMenu RecentFiles)
        {
            InitializeComponent();

            FileToLoadArg = FilePath;
            ProjectName = Name;
            RecentFilesMenu = RecentFiles;

            //Menu Item: File
            MenuItem_File_Close.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_Close")); };
            MenuItem_File_Save.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_Save")); };
            MenuItem_File_SaveAs.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_SaveAs")); };
            MenuItem_File_Export.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_Export")); };
            MenuItemFile_ReadSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ReadSound")); };
            MenuItemFile_ReadYml.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ReadYml")); };
            MenuItemFile_ReadESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ImportESIF")); };

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

            MenuItem_Edit_FileProps.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_Edit_FileProps")); };
            MenuItem_Edit_Undo.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemEdit_Undo")); };
            MenuItem_Edit_Search.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankSearch")); };

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Undo.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Search.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            //Buttons
            Button_UpdateList_WavData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankCheckAudios")); };
            Button_UpdateList_WavData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_StopUpdate.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonStopListUpdate")); };
            Button_StopUpdate.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateIMAData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonUpdateIMAData")); };
            Button_UpdateIMAData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_ExportInterchangeFile.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("Button_ExportInterchangeFile")); };
            Button_ExportInterchangeFile.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //Context Menu Folders
            ContextMenuFolder_ExpandAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_ExpandAll")); };
            ContextMenuFolder_CollapseAll.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_CollapseAll")); };
            ContextMenuFolder_AddSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_AddSound")); };
            ContextMenuFolder_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_Rename")); };
            ContextMenuFolder_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Folders.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            //Context Menu Sounds
            ContextMenuSounds_Delete.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Remove")); };
            ContextMenuSounds_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Rename")); };
            ContextMenuSounds_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Properties")); };
            ContextMenuSounds_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenuSounds_ExportESIF.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_ExportESIF")); };
            ContextMenu_Sounds.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSoundsEditorMain_Load(object sender, EventArgs e)
        {
            string ProfileName;

            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Load Preferences
            using (RegistryKey WindowStateConfig = WRegFunctions.ReturnRegistryKey("WindowState"))
            {
                bool IsIconic = Convert.ToBoolean(WindowStateConfig.GetValue("SSView_IsIconic", 0));
                bool IsMaximized = Convert.ToBoolean(WindowStateConfig.GetValue("SSView_IsMaximized", 0));
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
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("SSView_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("SSView_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("SSView_Width", 997));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("SSView_Height", 779));

                WindowStateConfig.Close();
            }

            ProjectInfo.TypeOfData = 1;

            //Check Hashcodes are not null
            //Load Hashcodes
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Keys.Count == 0)
            {
                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

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

            //Load file in argument 0
            if (string.IsNullOrEmpty(FileToLoadArg))
            {
                ProjectInfo.FileName = ProjectName;
            }
            else
            {
                LoadedFile = FileToLoadArg;
                ProfileName = SerializeInfo.LoadStreamSoundsDocument(TreeView_StreamData, StreamSoundsList, FileToLoadArg, ProjectInfo, GenericFunctions.ResourcesManager);

                //Check that the profile name matches with the current one
                if (ProfileName.Equals(GlobalPreferences.SelectedProfileName))
                {
                    TreeView_StreamData.ExpandAll();
                }
                else
                {
                    FormMustBeClosed = true;
                }
            }

            ProjectInfo.Hashcode = 65535;
        }

        private void Frm_StreamSoundsEditorMain_Shown(object sender, EventArgs e)
        {
            if (FormMustBeClosed)
            {
                Close();
            }
            else
            {
                //Update from title
                Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
                if (WindowState != FormWindowState.Maximized)
                {
                    MdiParent.Text = "EuroSound - " + Text;
                }

                //Set Program status
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

                UpdateStatusBarLabels();

                //Apply User Preferences
                FontConverter cvt = new FontConverter();
                TreeView_StreamData.Indent = GlobalPreferences.TV_Indent;
                TreeView_StreamData.ItemHeight = GlobalPreferences.TV_ItemHeight;
                TreeView_StreamData.Font = cvt.ConvertFromString(GlobalPreferences.TV_SelectedFont) as Font;
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
            //Stop thread if active
            if (UpdateImaData != null)
            {
                UpdateImaData.Abort();
            }

            //Stop thread if active
            if (UpdateWavList != null)
            {
                UpdateWavList.Abort();
            }

            if (e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.UserClosing)
            {
                //Ask user to save if file is modified
                if (ProjectInfo.FileHasBeenModified)
                {
                    DialogResult dialogResult = MessageBox.Show("Save changes to " + ProjectInfo.FileName + "?", "EuroSound", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        LoadedFile = SaveDocument(LoadedFile, TreeView_StreamData, StreamSoundsList, ProjectInfo);
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

            WRegFunctions.SaveWindowState("SSView", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_UpdateIMAData_Click(object sender, System.EventArgs e)
        {
            //Update File Status
            ProjectInfo.FileHasBeenModified = true;

            //Create folder in %temp%
            GenericFunctions.CreateTemporalFolder();

            UpdateIMAData();
        }

        private void Button_ExportInterchangeFile_Click(object sender, EventArgs e)
        {
            string ExportPath;

            ExportPath = GenericFunctions.SaveFileBrowser("EuroSound Interchange File (*.esif)|*.ESIF", 0, true, ProjectInfo.FileName);
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
                        if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, LabelText))
                        {
                            MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.CancelEdit = true;
                        }
                        else
                        {
                            //Update tree node props
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

        private void TreeView_StreamData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //Change node images depending of the type
            if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 0, 0);
            }
            else if (e.Node.Tag.Equals("Sound"))
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

        private void TreeView_StreamData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //Change node images depending of the type
            if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 1, 1);
            }
            else if (e.Node.Tag.Equals("Sound"))
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

        private void TreeView_StreamData_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode SelectedNode = TreeView_StreamData.SelectedNode;

            //Rename selected Node
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, SelectedNode);
                ProjectInfo.FileHasBeenModified = true;
            }
            //Delete selected Node
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedNode.Tag.Equals("Sound"))
                {
                    RemoveStreamSoundSelectedNode(TreeView_StreamData.SelectedNode);
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void TreeView_StreamData_MouseClick(object sender, MouseEventArgs e)
        {
            //Select node
            TreeNode SelectedNode = TreeView_StreamData.GetNodeAt(e.X, e.Y);
            TreeView_StreamData.SelectedNode = SelectedNode;

            //Open context menu depending of the selected node
            if (e.Button == MouseButtons.Right)
            {
                if (SelectedNode.Tag.Equals("Folder") || SelectedNode.Tag.Equals("Root"))
                {
                    ContextMenu_Folders.Show(Cursor.Position);
                }
                else if (SelectedNode.Tag.Equals("Sound"))
                {
                    ContextMenu_Sounds.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_StreamData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TreeView_StreamData.SelectedNode != null)
            {
                if (TreeView_StreamData.SelectedNode.Tag.Equals("Sound"))
                {
                    OpenSoundPropertiesForm(TreeView_StreamData.SelectedNode);
                    ProjectInfo.FileHasBeenModified = true;
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
            LoadedFile = SaveDocument(LoadedFile, TreeView_StreamData, StreamSoundsList, ProjectInfo);
            ProjectInfo.FileHasBeenModified = false;

            Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void MenuItem_File_SaveAs_Click(object sender, System.EventArgs e)
        {
            LoadedFile = OpenSaveAsDialog(TreeView_StreamData, StreamSoundsList, ProjectInfo);

            //Update text
            Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }

            //Update var
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItem_File_Export_Click(object sender, System.EventArgs e)
        {
            if (ProjectInfo.Hashcode != 0x00000000)
            {
                string FileName = "HC00FFFF";

                //---[Output with debug options
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    using (EuroSound_DebugTypes DebugOpts = new EuroSound_DebugTypes(new string[] { "File start 1", "File start 2" }))
                    {
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
            Frm_BuildSFXStreamFile BuildFile = new Frm_BuildSFXStreamFile(ProjectInfo, FileName, DebugFlags)
            {
                Tag = Tag,
                Owner = Owner
            };
            BuildFile.ShowDialog();
        }

        private void MenuItemFile_ReadESIF_Click(object sender, EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("EuroSound Interchange File (*.ESIF)|*.esif", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                ESIF_Loader EuroSoundPropertiesFileLoader = new ESIF_Loader();
                List<string> ImportResults = EuroSoundPropertiesFileLoader.LoadStreamSoundBank_File(FilePath, ProjectInfo, StreamSoundsList, TreeView_StreamData);
                if (ImportResults.Count > 0)
                {
                    GenericFunctions.ShowErrorsAndWarningsList(ImportResults, "Import Results", this);
                }

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItemFile_ReadYml_Click(object sender, EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                //--Ask user for a fully reimport--
                DialogResult ReimportQuestion = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItemFile_ReadSound_Click(object sender, EventArgs e)
        {
            string SoundName;

            string FilePath = GenericFunctions.OpenFileBrowser("YML Files (*.yml)|*.yml", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                SoundName = Path.GetFileNameWithoutExtension(FilePath);
                LibYamlReader.ReadYmlFile(StreamSoundsList, TreeView_StreamData, FilePath, SoundName, ProjectInfo);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        //*===============================================================================================
        //* MAIN MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, System.EventArgs e)
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
                    EnableUndo();
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
