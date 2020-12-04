using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public partial class Frm_Soundbanks_Main : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        List<EXSound> SoundsList;
        bool MenuStripOpened;
        string LoadedFile = string.Empty;
        string FileToLoadArg;
        Thread LoadYamlFile;

        /*Initialize sound params array to zeros*/
        int[] SndParams = new int[12];
        int[] SampleParams = new int[7];

        public Frm_Soundbanks_Main(string FilePath)
        {
            InitializeComponent();

            FileToLoadArg = FilePath;

            /*Menu Item: File*/
            MenuItem_File.DropDownOpened += (se, ev) => { StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MenuItem_File.DropDownClosed += (se, ev) => { SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItem_File_Open.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Open an existing document");
            MenuItem_File_Save.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Save the active document");
            MenuItem_File_SaveAs.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Save the active document with a new name");
            MenuItemFile_Export.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Export content to SFX file");
            MenuItem_File_Exit.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Quit the application; prompts to save documents");

            MenuItem_File_Open.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);
            MenuItem_File_Save.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);
            MenuItem_File_SaveAs.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);
            MenuItemFile_Export.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);
            MenuItem_File_Exit.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);

            /*Menu Item: Edit*/
            MenuItem_Edit.DropDownOpened += (se, ev) => { StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MenuItem_Edit.DropDownClosed += (se, ev) => { SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItem_Edit_FileProps.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Set project options");

            MenuItem_Edit_FileProps.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);

            /*Menu Item: Help*/
            MenuItem_Help.DropDownOpened += (se, ev) => { StatusBarTutorialModeShowText(""); MenuStripOpened = true; };
            MenuItem_Help.DropDownClosed += (se, ev) => { SetProgramStateShowToStatusBar("CurrentStatus"); MenuStripOpened = false; };

            MenuItem_Help_About.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Display program information, version number and copyright");
            MenuItem_Help_Online.MouseHover += (se, ev) => StatusBarTutorialModeShowText("Show online help");

            MenuItem_Help_About.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);
            MenuItem_Help_Online.MouseLeave += (se, ev) => StatusBarTutorialMode(MenuStripOpened);
            MenuItem_Help_Online.Click += (se, ev) => Process.Start("https://sphinxandthecursedmummy.fandom.com/wiki/SFX");
        }

        //*===============================================================================================
        //* Change Nodes Images
        //*===============================================================================================
        private void TreeView_File_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            /*Change node images depending of the type*/
            if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 1, 1);
            }
            else if (e.Node.Tag.Equals("Sound"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 3, 3);
            }
            else if (e.Node.Tag.Equals("Sample"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 4, 4);
            }
        }

        private void TreeView_File_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            /*Change node images depending of the type*/
            if (e.Node.Tag.Equals("Folder") || e.Node.Tag.Equals("Root"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 0, 0);
            }
            else if (e.Node.Tag.Equals("Sound"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 2, 2);
            }
            else if (e.Node.Tag.Equals("Sample"))
            {
                TreeNodeFunctions.TreeNodeSetNodeImage(e.Node, 4, 4);
            }
        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_Main_Load(object sender, System.EventArgs e)
        {
            /*Load Hashcodes*/
            Hashcodes.LoadSoundHashcodes();
            Hashcodes.LoadSoundDataFile();

            /*Initialize sounds list*/
            SoundsList = new List<EXSound>();

            /*Expand Sounds node*/
            TreeView_File.Nodes["Sounds"].Expand();

            /*Set Program status*/
            SetProgramStateShowToStatusBar("Ready");

            CurrentFileLabel.Text = EXFile.FileName;

            /*Load file in argument 0*/
            if (!string.IsNullOrEmpty(FileToLoadArg))
            {
                SaveData.LoadDataFromEuroSoundFile(TreeView_File, SoundsList, CurrentFileLabel, FileToLoadArg);
            }
        }

        //*===============================================================================================
        //* Tree View Events
        //*===============================================================================================
        private void TreeView_File_MouseClick(object sender, MouseEventArgs e)
        {
            /*Select node*/
            TreeView_File.SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);

            /*Open context menu depending of the selected node*/
            if (e.Button == MouseButtons.Right)
            {
                if (TreeView_File.SelectedNode.Tag.Equals("Folder") || TreeView_File.SelectedNode.Tag.Equals("Root"))
                {
                    ContextMenu_Folders.Show(Cursor.Position);
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Sound"))
                {
                    ContextMenu_Sound.Show(Cursor.Position);
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Sample"))
                {
                    ContextMenu_Sample.Show(Cursor.Position);
                }
            }
        }

        private void TreeView_File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*Select node*/
            TreeView_File.SelectedNode = TreeView_File.GetNodeAt(e.X, e.Y);

            if (TreeView_File.SelectedNode.Tag.Equals("Sample"))
            {
                OpenItemProperties();
            }
        }

        private void TreeView_File_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            /*Check that we have selected a node, and we have not selected the root folder*/
            if (e.Node.Parent != null && !e.Node.Tag.Equals("Root"))
            {
                /*Check we are not renaming with an empty string*/
                if (string.IsNullOrEmpty(e.Label))
                {
                    /*Cancel edit*/
                    e.CancelEdit = true;
                    Debug.WriteLine("ERROR -- The new name can't be empty");
                }
                else
                {
                    /*Check that not exists an item with the same name*/
                    if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, e.Label))
                    {
                        MessageBox.Show("Sorry, cannot rename this item, an item with this name already exists", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine("ERROR -- Trying to rename an object with a name that already exists.");
                        e.CancelEdit = true;
                    }
                    else
                    {
                        /*Rename Sound item*/
                        if (e.Node.Tag.Equals("Sound"))
                        {
                            for (int i = 0; i < SoundsList.Count; i++)
                            {
                                if (SoundsList[i].Name.Equals(e.Node.Name))
                                {
                                    SoundsList[i].Name = EXObjectsFunctions.RemoveWhiteSpaces(e.Label);
                                    SoundsList[i].DisplayName = e.Label;
                                    break;
                                }
                            }
                        }
                        /*Rename sound sample*/
                        else if (e.Node.Tag.Equals("Sample"))
                        {
                            EXSound ParentSound = TreeNodeFunctions.GetSelectedSound(e.Node.Parent.Name, SoundsList);
                            for (int i = 0; i < ParentSound.Samples.Count; i++)
                            {
                                if (ParentSound.Samples[i].Name.Equals(e.Node.Name))
                                {
                                    ParentSound.Samples[i].Name = EXObjectsFunctions.RemoveWhiteSpaces(e.Label);
                                    ParentSound.Samples[i].DisplayName = e.Label;
                                    break;
                                }
                            }
                        }

                        /*Update tree node props*/
                        e.Node.Name = EXObjectsFunctions.RemoveWhiteSpaces(e.Label);
                        e.Node.Text = e.Label;
                    }
                }
            }
            else
            {
                /*Cancel edit*/
                e.CancelEdit = true;
                Debug.WriteLine("ERROR -- Trying to rename a root folder.");
            }
        }

        //*===============================================================================================
        //* Context Menu - Items
        //*===============================================================================================
        private void NewFolderToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            string Name = OpenInputBox("Enter a name for new folder.", "New Folder");
            if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, Name))
            {
                MessageBox.Show("Sorry, cannot add a folder with this name, an item with this name already exists", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, Name, 1, 1, "Folder", Color.Black, TreeView_File);
                }
                else
                {
                    MessageBox.Show("The name can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Debug.WriteLine("WARNING -- Trying to add a folder withouth name.");
                }
            }

        }

        private void MenuItem_AddSound_Click(object sender, System.EventArgs e)
        {
            string Name = OpenInputBox("Enter a name for new sound.", "New Sound");
            if (TreeNodeFunctions.CheckIfNodeExists(TreeView_File, Name))
            {
                MessageBox.Show("Sorry, cannot add a sound with this name, an item with this name already exists", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, Name, 2, 2, "Sound", Color.Black, TreeView_File);
                    EXObjectsFunctions.AddNewSound(Name, Name, "0x1A000001", SndParams, SoundsList);
                }
                else
                {
                    MessageBox.Show("The name can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Debug.WriteLine("WARNING -- Trying to add a sound withouth name.");
                }
            }
        }

        private void MenuItem_AddSample_Click(object sender, System.EventArgs e)
        {
            string Name = OpenInputBox("Enter a name for new a new sample.", "New Sample");
            if (!string.IsNullOrEmpty(Name))
            {
                if (TreeNodeFunctions.FindRootNode(TreeView_File.SelectedNode).Name.Equals("StreamedSounds"))
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, Name, 4, 4, "Sample", Color.Black, TreeView_File);
                    EXObjectsFunctions.AddSampleToSound(TreeNodeFunctions.GetSelectedSound(TreeView_File.SelectedNode.Name, SoundsList), Name, SampleParams, true);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeAddNewNode(TreeView_File.SelectedNode.Name, Name, 4, 4, "Sample", Color.Black, TreeView_File);
                    EXObjectsFunctions.AddSampleToSound(TreeNodeFunctions.GetSelectedSound(TreeView_File.SelectedNode.Name, SoundsList), Name, SampleParams, false);
                }
            }
            else
            {
                MessageBox.Show("The name can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.WriteLine("WARNING -- Trying to add a sample withouth name.");
            }
        }

        private void MenuItem_RemoveSound_Click(object sender, System.EventArgs e)
        {
            RemoveSoundSelectedNode();
        }

        private void MenuItem_RemoveSample_Click(object sender, System.EventArgs e)
        {
            RemoveSampleSelectedNode();
        }

        private void MenuItem_Folder_Delete_Click(object sender, System.EventArgs e)
        {
            RemoveFolderSelectedNode();
        }

        private void MenuItem_Folder_Expand_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.Expand();
        }

        private void MenuItem_Folder_Collapse_Click(object sender, System.EventArgs e)
        {
            TreeView_File.SelectedNode.Collapse();
        }

        private void ContextMenu_SoundProperties_Click(object sender, System.EventArgs e)
        {
            OpenItemProperties();
        }

        private void MenuItem_SampleProperties_Click(object sender, System.EventArgs e)
        {
            OpenItemProperties();
        }

        private void ContextMenu_SoundRename_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
        }

        private void MenuItem_RenameSample_Click(object sender, System.EventArgs e)
        {
            TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
        }

        private void ContextMenuFolders_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
            }
        }

        private void ContextMenuSample_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
            }
        }

        private void ContextMenuSound_TextColor_Click(object sender, System.EventArgs e)
        {
            if (ColorDialog_TextColor.ShowDialog() == DialogResult.OK)
            {
                TreeView_File.SelectedNode.ForeColor = ColorDialog_TextColor.Color;
            }
        }

        //*===============================================================================================
        //* HOT KEYS
        //*===============================================================================================
        private void TreeView_File_KeyDown(object sender, KeyEventArgs e)
        {
            /*Rename selected Node*/
            if (e.KeyCode == Keys.F2)
            {
                TreeNodeFunctions.EditNodeLabel(TreeView_File, TreeView_File.SelectedNode);
            }
            /*Delete selected Node*/
            if (e.KeyCode == Keys.Delete)
            {
                if (TreeView_File.SelectedNode.Tag.Equals("Sound"))
                {
                    RemoveSoundSelectedNode();
                }
                else if(TreeView_File.SelectedNode.Equals("Sample"))
                {
                    RemoveSampleSelectedNode();
                }
                else
                {
                    RemoveFolderSelectedNode();
                }
            }
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*===============================================================================================
        private void MenuItem_File_Open_Click(object sender, System.EventArgs e)
        {
            string FilePath = Generic.OpenFileBrowser("EuroSoundFile|*.ESF", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                //Clear Data
                TreeView_File.Nodes[0].Nodes.Clear();
                TreeView_File.Nodes[1].Nodes.Clear();
                SoundsList.Clear();
                ListView_WavHeaderData.Items.Clear();

                /*Load New File*/
                LoadedFile = FilePath;
                SaveData.LoadDataFromEuroSoundFile(TreeView_File, SoundsList, CurrentFileLabel, FilePath);
            }
        }

        private void MenuItem_File_Save_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(LoadedFile))
            {
                SaveData.SaveDataToEuroSoundFile(TreeView_File, SoundsList, LoadedFile);
            }
            else
            {
                OpenSaveAsDialog();
            }
        }

        private void MenuItem_File_SaveAs_Click(object sender, System.EventArgs e)
        {
            LoadedFile = OpenSaveAsDialog();
        }

        private void MenuItemFile_Export_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(EXFile.Hashcode))
            {
                string FileName = "HC" + EXFile.Hashcode.Substring(4);
                string SavePath = Generic.SaveFileBrowser("SFX Files (*.SFX)|*.SFX", 1, true, FileName);
                if (!string.IsNullOrEmpty(SavePath))
                {
                    if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                    {
                        EXBuildSFX.ExportContentToSFX(SoundsList, SavePath);
                    }
                }
            }
            else
            {
                MessageBox.Show("Can't build the soundbank file without a hashcode. Please set the hashcode for this file in the file preferences.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemFile_ReadYml_Click(object sender, System.EventArgs e)
        {
            string FilePath = Generic.OpenFileBrowser("YML Files|*.yml", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                //Clear Data
                TreeView_File.Nodes[0].Nodes.Clear();
                TreeView_File.Nodes[1].Nodes.Clear();
                SoundsList.Clear();
                ListView_WavHeaderData.Items.Clear();

                /*Load New data*/
                LoadYamlFile = new Thread(() => YamlReader.LoadDataFromSwyterUnpacker(SoundsList, TreeView_File, FilePath))
                {
                    IsBackground = true
                };
                LoadYamlFile.Start();
            }
        }

        private void MenuItem_File_Exit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        //*===============================================================================================
        //* MAIN MENU EDIT
        //*===============================================================================================
        private void MenuItem_Edit_FileProps_Click(object sender, System.EventArgs e)
        {
            Frm_FileProperties Props = new Frm_FileProperties()
            {
                Owner = this,
                ShowInTaskbar = false
            };
            Props.ShowDialog();
        }

        //*===============================================================================================
        //* MAIN MENU HELP
        //*===============================================================================================
        private void MenuItem_Help_About_Click(object sender, System.EventArgs e)
        {
            Frm_AboutEuroSound About = new Frm_AboutEuroSound()
            {
                Owner = this,
                ShowInTaskbar = false
            };
            About.ShowDialog();
        }

        //*===============================================================================================
        //* WAV HEADER DATA
        //*===============================================================================================
        private void Button_UpdateList_WavData_Click(object sender, System.EventArgs e)
        {
            ListView_WavHeaderData.Items.Clear();
            foreach (EXSound Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Samples)
                {
                    if (Sample.IsStreamed == false)
                    {
                        if (Sample.Audio.IsEmpty() == false)
                        {
                            ListViewItem AudioData = new ListViewItem(new[] { Sample.DisplayName, Sound.Name, Sample.Audio.Frequency.ToString(), Sample.Audio.Channels.ToString(), Sample.Audio.Bits.ToString(), Sample.Audio.PCMdata.Length.ToString(), Sample.Audio.Encoding, Sample.Audio.Duration.ToString() });
                            ListView_WavHeaderData.Items.Add(AudioData);
                        }
                    }
                }
            }
        }

        private void Button_UpdateList_Hashcodes_Click(object sender, System.EventArgs e)
        {
            /*Clear List*/
            ListView_Hashcodes.Items.Clear();

            /*Level Hashcode*/
            ListViewItem LevelHashcode = new ListViewItem(new[] { "", EXFile.Hashcode, "<Label Not Found>", "File Properties" });
            if (Hashcodes.SB_Defines.ContainsKey(EXFile.Hashcode))
            {
                LevelHashcode.SubItems[0].BackColor = Color.Green;
                LevelHashcode.SubItems[2].Text = Hashcodes.SB_Defines[EXFile.Hashcode];
            }
            else
            {
                LevelHashcode.SubItems[0].BackColor = Color.Red;
            }
            LevelHashcode.UseItemStyleForSubItems = false;
            ListView_Hashcodes.Items.Add(LevelHashcode);

            /*Sounds Hashcodes*/
            foreach (EXSound Sound in SoundsList)
            {
                ListViewItem Hashcode = new ListViewItem(new[] { "", Sound.Hashcode, "<Label Not Found>", Sound.DisplayName });
                if (Hashcodes.SFX_Defines.ContainsKey(Sound.Hashcode))
                {
                    Hashcode.SubItems[0].BackColor = Color.Green;
                    Hashcode.SubItems[2].Text = Hashcodes.SFX_Defines[Sound.Hashcode];
                }
                else
                {
                    Hashcode.SubItems[0].BackColor = Color.Red;
                }
                Hashcode.UseItemStyleForSubItems = false;
                ListView_Hashcodes.Items.Add(Hashcode);
            }
        }
    }
}