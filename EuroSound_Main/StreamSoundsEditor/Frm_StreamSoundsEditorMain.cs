using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
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
        public Dictionary<uint, EXSoundStream> StreamSoundsList = new Dictionary<uint, EXSoundStream>();
        private EuroSoundFiles SerializeInfo = new EuroSoundFiles();
        public ProjectFile ProjectInfo = new ProjectFile();
        private StreamSoundsYMLReader LibYamlReader = new StreamSoundsYMLReader();
        private AudioFunctions AudioLibrary = new AudioFunctions();
        private Thread UpdateImaData;
        private string FileToLoadArg, ProjectName;
        private string LoadedFile = string.Empty;

        public Frm_StreamSoundsEditorMain(string Name, string FilePath)
        {
            InitializeComponent();

            FileToLoadArg = FilePath;
            ProjectName = Name;
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSoundsEditorMain_Load(object sender, System.EventArgs e)
        {
            ProjectInfo.TypeOfData = 1;

            /*Check Hashcodes are not null*/
            /*Load Hashcodes*/
            if (Hashcodes.SFX_Defines.Keys.Count == 0 || Hashcodes.SFX_Data.Keys.Count == 0)
            {
                /*Update Status Bar*/
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

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

            /*Load file in argument 0*/
            if (string.IsNullOrEmpty(FileToLoadArg))
            {
                ProjectInfo.FileName = ProjectName;
            }
            else
            {
                /*Update Status Bar*/
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

                LoadedFile = FileToLoadArg;
                SerializeInfo.LoadStreamSoundsDocument(TreeView_StreamData, StreamSoundsList, FileToLoadArg, ProjectInfo, GenericFunctions.ResourcesManager);
                TreeView_StreamData.ExpandAll();
            }

            ProjectInfo.Hashcode = 65535;
        }

        private void Frm_StreamSoundsEditorMain_Shown(object sender, System.EventArgs e)
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
            TreeView_StreamData.Indent = GlobalPreferences.TreeViewIndent;
            TreeView_StreamData.Font = cvt.ConvertFromString(GlobalPreferences.SelectedFont) as Font;
            TreeView_StreamData.ShowLines = GlobalPreferences.ShowLines;
            TreeView_StreamData.ShowRootLines = GlobalPreferences.ShowRootLines;
        }

        private void Frm_StreamSoundsEditorMain_Enter(object sender, System.EventArgs e)
        {
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
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
            if (e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.UserClosing)
            {
                //Stop thread if active
                if (UpdateImaData != null)
                {
                    UpdateImaData.Abort();
                }

                //Ask user to save if file is modified
                if (ProjectInfo.FileHasBeenModified)
                {
                    DialogResult dialogResult = MessageBox.Show("Save changes to " + ProjectInfo.FileName + "?", "EuroSound", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        LoadedFile = SaveDocument(LoadedFile, TreeView_StreamData, StreamSoundsList, ProjectInfo);
                        ProjectInfo.FileHasBeenModified = false;
                        GenericFunctions.SetCurrentFileLabel("");
                        MdiParent.Text = "EuroSound";
                        Close();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ProjectInfo.FileHasBeenModified = false;
                        GenericFunctions.SetCurrentFileLabel("");
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
                    GenericFunctions.SetCurrentFileLabel("");
                    MdiParent.Text = "EuroSound";
                }
            }
        }

        //*===============================================================================================
        //* Tree View Controls
        //*===============================================================================================
        private void TreeView_StreamData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            uint SoundID;

            /*Check that we have selected a node, and we have not selected the root folder*/
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                /*Check we are not renaming with an empty string*/
                if (string.IsNullOrEmpty(e.Label))
                {
                    /*Cancel edit*/
                    e.CancelEdit = true;
                }
                else
                {
                    /*Check that not exists an item with the same name*/
                    if (TreeNodeFunctions.CheckIfNodeExistsByText(TreeView_StreamData, e.Label))
                    {
                        MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Error_Rename_AlreadyExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                    }
                    else
                    {
                        /*Rename Sound item*/
                        if (e.Node.Tag.Equals("Sound"))
                        {
                            SoundID = uint.Parse(e.Node.Name);
                            if (StreamSoundsList.ContainsKey(SoundID))
                            {
                                StreamSoundsList[SoundID].DisplayName = e.Label;
                            }
                        }
                        else
                        {
                            /*Update tree node props*/
                            e.Node.Text = e.Label;
                        }
                    }
                }
            }
            else
            {
                /*Cancel edit*/
                e.CancelEdit = true;
            }
        }

        private void TreeView_StreamData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            /*Change node images depending of the type*/
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
            /*Change node images depending of the type*/
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

            /*Rename selected Node*/
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_StreamData, SelectedNode);
                ProjectInfo.FileHasBeenModified = true;
            }
            /*Delete selected Node*/
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedNode.Tag.Equals("Sound"))
                {
                    RemoveStreamSoundSelectedNode();
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void TreeView_StreamData_MouseClick(object sender, MouseEventArgs e)
        {
            /*Select node*/
            TreeNode SelectedNode = TreeView_StreamData.GetNodeAt(e.X, e.Y);
            TreeView_StreamData.SelectedNode = SelectedNode;

            /*Open context menu depending of the selected node*/
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
            if (TreeView_StreamData.SelectedNode.Tag.Equals("Sound"))
            {
                OpenSoundPropertiesForm();
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*===============================================================================================
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

            /*Update text*/
            Text = GenericFunctions.UpdateProjectFormText(LoadedFile, ProjectInfo.FileName);
            if (!(WindowState == FormWindowState.Maximized))
            {
                MdiParent.Text = "EuroSound - " + Text;
            }

            /*Update var*/
            ProjectInfo.FileHasBeenModified = false;
        }

        private void MenuItem_File_Export_Click(object sender, System.EventArgs e)
        {
            if (ProjectInfo.Hashcode != 0x00000000)
            {
                string FileName = "HC00FFFF";
                Frm_BuildSFXStreamFile BuildFile = new Frm_BuildSFXStreamFile(ProjectInfo, FileName)
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

        private void MenuItemFile_ReadSound_Click(object sender, System.EventArgs e)
        {
            string SoundName;

            string FilePath = GenericFunctions.OpenFileBrowser("YML Files (*.yml)|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                SoundName = Path.GetFileNameWithoutExtension(FilePath);
                LibYamlReader.ReadYmlFile(StreamSoundsList, TreeView_StreamData, FilePath, SoundName, ProjectInfo);
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void MenuItemFile_ReadYml_Click(object sender, System.EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("YML Files (*.yml)|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                /*--Ask user for a fully reimport--*/
                DialogResult ReimportQuestion = MessageBox.Show(GenericFunctions.ResourcesManager.GetString("MenuItem_File_LoadListCleanData"), "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ReimportQuestion == DialogResult.Yes)
                {
                    //Clear Data
                    ProjectInfo.ClearStreamSoundStoredData(StreamSoundsList, TreeView_StreamData);
                }

                /*--Load New data--*/
                Thread LoadYamlFile = new Thread(() => LibYamlReader.LoadDataFromSwyterUnpacker(StreamSoundsList, TreeView_StreamData, FilePath, ProjectInfo))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();

                ProjectInfo.FileHasBeenModified = true;
            }
        }

        private void Button_UpdateIMAData_Click(object sender, System.EventArgs e)
        {
            string ImaPath;

            //Update File Status
            ProjectInfo.FileHasBeenModified = true;

            //Create folder in %temp%
            GenericFunctions.CreateTemporalFolder();

            UpdateImaData = new Thread(() =>
            {
                try
                {
                    foreach (KeyValuePair<uint, EXSoundStream> SoundToUpdate in StreamSoundsList)
                    {
                        string AudioPath = Path.GetTempPath() + @"EuroSound\" + SoundToUpdate.Key + ".wav";
                        AudioLibrary.CreateWavFile((int)SoundToUpdate.Value.Frequency, (int)SoundToUpdate.Value.Bits, (int)SoundToUpdate.Value.Channels, SoundToUpdate.Value.PCM_Data, AudioPath);

                        /*Get IMA ADPCM Data*/
                        ImaPath = AudioLibrary.ConvertWavToIMAADPCM(AudioPath, Path.GetFileNameWithoutExtension(AudioPath));
                        if (!string.IsNullOrEmpty(ImaPath))
                        {
                            SoundToUpdate.Value.IMA_ADPCM_DATA = File.ReadAllBytes(ImaPath);
                        }

                        /*Update Status Bar*/
                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Format("Checking: {0}.wav", SoundToUpdate.Key));
                    }

                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("StreamSoundsUpdatedSuccess"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    /*Update Status Bar*/
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("StreamSoundsUpdatedError"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                /*Update Status Bar*/
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            })
            {
                IsBackground = true
            };
            UpdateImaData.Start();
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
    }
}
