using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
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
        private EuroSoundFiles SerializeInfo = new EuroSoundFiles();
        private Thread UpdateList, UpdateWavList, UpdateStreamDataList;
        private SoundBanksYMLReader LibYamlReader = new SoundBanksYMLReader();
        private AudioFunctions AudioFunctionsLibrary = new AudioFunctions();
        private readonly Regex sWhitespace = new Regex(@"\s+");
        private string FileToLoadArg, ProjectName;
        private string LoadedFile = string.Empty;

        public Frm_Soundbanks_Main(string NewProjectName, string FileToLoad)
        {
            InitializeComponent();

            FileToLoadArg = FileToLoad;
            ProjectName = NewProjectName;

            /*Menu Item: File*/
            MenuItem_File_Save.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_Save")); };
            MenuItem_File_SaveAs.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_SaveAs")); };
            MenuItem_File_Export.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_File_Export")); };
            MenuItemFile_ReadSound.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ReadSound")); };
            MenuItemFile_ReadYml.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItemFile_ReadYml")); };

            MenuItem_File_Save.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_SaveAs.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_File_Export.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_ReadSound.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItemFile_ReadYml.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            /*Menu Item: Edit*/
            MenuItem_Edit.DropDownOpened += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = true; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };
            MenuItem_Edit.DropDownClosed += (se, ev) => { GlobalPreferences.StatusBar_ToolTipMode = false; GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode); };

            MenuItem_Edit_Search.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankSearch")); };
            MenuItem_Edit_FileProps.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("MenuItem_Edit_FileProps")); };

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);
            MenuItem_Edit_Search.MouseLeave += (se, ev) => GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(GlobalPreferences.StatusBar_ToolTipMode);

            /*Buttons*/
            Button_GenerateList.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonGenerateSoundsList")); };
            Button_GenerateList.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_Hashcodes.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankCheckHashcodes")); };
            Button_UpdateList_Hashcodes.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_UpdateList_WavData.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ButtonSoundsBankCheckAudios")); };
            Button_UpdateList_WavData.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            /*Context Menu Folders*/
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
            ContextMenu_Folders.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            /*ContextMenu_Sound*/
            ContextMenuSound_AddSample.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_AddSample")); };
            ContextMenuSound_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Remove")); };
            ContextMenuSound_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Rename")); };
            ContextMenuSound_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSound_Properties")); };
            ContextMenuSound_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Sound.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            /*Context Menu Samples*/
            ContextMenuSample_Remove.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSample_Remove")); };
            ContextMenuSample_Rename.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSample_Rename")); };
            ContextMenuSample_Properties.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuSample_Properties")); };
            ContextMenuSample_TextColor.MouseHover += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("ContextMenuFolder_TextColor")); };
            ContextMenu_Sample.Closing += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_Soundbanks_Main_Load(object sender, EventArgs e)
        {
            /*Check Hashcodes are not null*/
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Keys.Count == 0)
            {
                /*Load Data*/
                Thread LoadHashcodeData = new Thread(() => Hashcodes.LoadSoundDataFile())
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

            /*Load esf file if nedded*/
            if (string.IsNullOrEmpty(FileToLoadArg))
            {
                ProjectInfo.FileName = ProjectName;
            }
            else
            {
                /*Update Status Bar*/
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

                LoadedFile = FileToLoadArg;
                SerializeInfo.LoadSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, FileToLoadArg, ProjectInfo);
                TreeView_File.CollapseAll();
            }
        }

        private void Frm_Soundbanks_Main_Shown(object sender, EventArgs e)
        {
            /*Update from title*/
            Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
            MdiParent.Text = "EuroSound - " + Text;

            /*Set Program status*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            /*Update File name label*/
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName);

            /*Apply User Preferences*/
            FontConverter cvt = new FontConverter();
            TreeView_File.Indent = GlobalPreferences.TreeViewIndent;
            TreeView_File.Font = cvt.ConvertFromString(GlobalPreferences.SelectedFont) as Font;
            TreeView_File.ShowLines = GlobalPreferences.ShowLines;
            TreeView_File.ShowRootLines = GlobalPreferences.ShowRootLines;
        }

        private void Frm_Soundbanks_Main_Enter(object sender, EventArgs e)
        {
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
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

            //Ask user to save if file is modified
            if (ProjectInfo.FileHasBeenModified)
            {
                DialogResult dialogResult = MessageBox.Show("Save changes to " + ProjectInfo.FileName + "?", "EuroSound", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;
                    LoadedFile = SaveDocument(LoadedFile, TreeView_File, SoundsList, AudioDataDict, ProjectInfo);
                    ProjectInfo.FileHasBeenModified = false;
                    e.Cancel = false;
                    GenericFunctions.SetCurrentFileLabel("");
                    MdiParent.Text = "EuroSound";
                }
                else if (dialogResult == DialogResult.No)
                {
                    GlobalPreferences.CancelApplicationClose = false;
                    e.Cancel = false;
                    GenericFunctions.SetCurrentFileLabel("");
                    MdiParent.Text = "EuroSound";
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    GlobalPreferences.CancelApplicationClose = true;
                    e.Cancel = true;
                }
            }
            else
            {
                GenericFunctions.SetCurrentFileLabel("");
                MdiParent.Text = "EuroSound";
            }
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Button_GenerateList_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            string SavePath = GenericFunctions.SaveFileBrowser("YML Files (*.yml)|*.yml", 1, true, ProjectName);
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

        //*===============================================================================================
        //* LIST VIEWS DATA
        //*===============================================================================================
        private void Button_UpdateList_Hashcodes_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            /*Check New data*/
            UpdateHashcodesValidList();
        }

        /*-----------------------------[STREAM DATA]-----------------------------*/
        private void Button_UpdateList_StreamData_Click(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false);

            /*Check Stream Data*/
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
            /*Check Audio properties*/
            UpdateWavDataList();
        }

        private void ListView_WavHeaderData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_WavHeaderData.SelectedItems.Count > 0 )
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

        //*===============================================================================================
        //* MAIN MENU FILE
        //*===============================================================================================
        private void MenuItem_File_Save_Click(object sender, EventArgs e)
        {
            LoadedFile = SaveDocument(LoadedFile, TreeView_File, SoundsList, AudioDataDict, ProjectInfo);
            ProjectInfo.FileHasBeenModified = false;

            Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }
        }

        private void MenuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            /*Save file in different location*/
            LoadedFile = OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectInfo);

            /*Update text*/
            Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }

            /*Update var*/
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItemFile_Export_Click(object sender, EventArgs e)
        {
            if (ProjectInfo.Hashcode != 0x00000000)
            {
                string FileName = "HC" + ProjectInfo.Hashcode.ToString("X8").Substring(2);
                Frm_BuildSFXFile BuildFile = new Frm_BuildSFXFile(ProjectInfo, FileName)
                {
                    Tag = Tag,
                    Owner = Owner,
                    ShowInTaskbar = false
                };
                BuildFile.ShowDialog();
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_BuildSFX_NoHashcode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemFile_ReadSound_Click(object sender, EventArgs e)
        {
            string SoundName;
            uint SoundHashcode;

            string FilePath = GenericFunctions.OpenFileBrowser("YML Files (*.yml)|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                SoundName = new DirectoryInfo(Path.GetDirectoryName(FilePath)).Name;
                SoundHashcode = Hashcodes.GetHashcodeByLabel(Hashcodes.SFX_Defines, SoundName);
                LibYamlReader.ReadYmlFile(SoundsList, AudioDataDict, TreeView_File, FilePath, SoundName, SoundHashcode, true, ProjectInfo);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItemFile_ReadYml_Click(object sender, EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("YML Files (*.yml)|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                /*--Ask user for a fully reimport--*/
                DialogResult ReimportQuestion = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ReimportQuestion == DialogResult.Yes)
                {
                    //Clear Data
                    ProjectInfo.ClearSoundBankStoredData(SoundsList, AudioDataDict, TreeView_File);
                }

                /*--Load New data--*/
                Thread LoadYamlFile = new Thread(() => LibYamlReader.LoadDataFromSwyterUnpacker(SoundsList, AudioDataDict, TreeView_File, FilePath, ProjectInfo))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        //*===============================================================================================
        //* TREE VIEW EVENTS
        //*===============================================================================================
        private void TreeView_File_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string LabelText;

            /*Check that we have selected a node, and we have not selected the root folder*/
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                /*Get text label*/
                LabelText = e.Label.Trim();

                /*Check we are not renaming with an empty string*/
                if (string.IsNullOrEmpty(LabelText))
                {
                    /*Cancel edit*/
                    e.CancelEdit = true;
                }
                else
                {
                    /*Check that not exists an item with the same name*/
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
            else
            {
                /*Cancel edit*/
                e.CancelEdit = true;
            }
        }

        //---------------------------------------------[Change Nodes Images]---------------------------------------------
        private void TreeView_File_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            /*Change node images depending of the type*/
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
            /*Change node images depending of the type*/
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

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = TreeView_File.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = TreeView_File.GetNodeAt(targetPoint);
            TreeNode FindTargetNode = TreeNodeFunctions.FindRootNode(targetNode);
            if (FindTargetNode != null)
            {
                DestSection = FindTargetNode.Text;
                DestNodeType = targetNode.Tag.ToString();

                // Retrieve the node that was dragged.
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                SourceSection = TreeNodeFunctions.FindRootNode(draggedNode).Text;

                // Confirm that the node at the drop location is not
                // the dragged node and that target node isn't null
                // (for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && targetNode != null)
                {
                    /*
                    Confirm we are not outside the node section and that the destination place is a folder or the root
                    node section
                    */
                    if (SourceSection.Equals(DestSection) && (DestNodeType.Equals("Folder") || DestNodeType.Equals("Root")))
                    {
                        // Remove the node from its current
                        // location and add it to the node at the drop location.
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                        targetNode.Expand();
                        TreeView_File.SelectedNode = draggedNode;
                        ProjectInfo.FileHasBeenModified = true;
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

            // See if we need to scroll up or down
            if ((p.Y + scrollRegion) > TreeView_File.Height)
            {
                // Call the API to scroll down
                SendMessage(TreeView_File.Handle, 277, 1, 0);
            }
            else if (p.Y < scrollRegion)
            {
                // Call thje API to scroll up
                SendMessage(TreeView_File.Handle, 277, 0, 0);
            }

            TreeNode node = TreeView_File.GetNodeAt(p.X, p.Y);
            TreeView_File.SelectedNode = node;
        }

        private void TreeView_File_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void MenuItem_Edit_Search_Click(object sender, EventArgs e)
        {
            EuroSound_SearchItem Search = new EuroSound_SearchItem(Name)
            {
                Owner = this,
                ShowInTaskbar = false,
                Tag = Tag
            };
            Search.Show();
        }

        private void TreeView_File_MouseClick(object sender, MouseEventArgs e)
        {
            /*Open context menu depending of the selected node*/
            if (e.Button == MouseButtons.Right)
            {
                /*Select node*/
                TreeNode SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);
                TreeView_File.SelectedNode = SelectedNode;

                /*Check the selected node*/
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
            /*Select node*/
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
        //* HOT KEYS
        //*===============================================================================================
        private void TreeView_File_KeyDown(object sender, KeyEventArgs e)
        {
            TreeNode SelectedNode = TreeView_File.SelectedNode;

            /*Rename selected Node*/
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_File, SelectedNode);
                ProjectInfo.FileHasBeenModified = true;
            }
            /*Delete selected Node*/
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