﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound
{
    public partial class Frm_Main : Form
    {
        bool MenuStripOpened;

        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        List<EXSound> SoundsList;
        string LoadedFile = string.Empty;
        string FileToLoadArg;

        public Frm_Main(string FilePath)
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
            Hashcodes.ReadHashcodes(EXFile.HT_SoundsPath);

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
                SaveData.LoadDataFromEuroSoundFile(TreeView_File, SoundsList, ListView_WavHeaderData, CurrentFileLabel, FileToLoadArg);
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

                    /*Show warning*/
                    MessageBox.Show("The new name can't be empty", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Debug.WriteLine("ERROR -- The new name can't be empty");
                }
                else
                {
                    /*Check that not exists an item with the same name*/
                    foreach (TreeNode tn in e.Node.Parent.Nodes)
                    {
                        if (tn.Text == e.Label)
                        {
                            MessageBox.Show("Sorry, cannot rename this item, an item with this name already exists", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Debug.WriteLine("ERROR -- Trying to rename an object with a name that already exists.");
                            e.CancelEdit = true;
                            return;
                        }
                    }

                    /*Rename Sound item*/
                    if (e.Node.Tag.Equals("Sound"))
                    {
                        for (int i = 0; i < SoundsList.Count; i++)
                        {
                            if (SoundsList[i].Name.Equals(e.Node.Name))
                            {
                                SoundsList[i].Name = EXFunctions.RemoveWhiteSpaces(e.Label);
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
                                ParentSound.Samples[i].Name = EXFunctions.RemoveWhiteSpaces(e.Label);
                                ParentSound.Samples[i].DisplayName = e.Label;
                                break;
                            }
                        }
                    }

                    /*Update tree node props*/
                    e.Node.Name = EXFunctions.RemoveWhiteSpaces(e.Label);
                    e.Node.Text = e.Label;
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
            TreeNodeFunctions.AddNewFolder(Name, TreeView_File);
        }

        private void MenuItem_AddSound_Click(object sender, System.EventArgs e)
        {
            string Name = OpenInputBox("Enter a name for new sound.", "New Sound");
            TreeNodeFunctions.AddNewSound(Name, TreeView_File, SoundsList);
        }

        private void MenuItem_AddSample_Click(object sender, System.EventArgs e)
        {
            string Name = OpenInputBox("Enter a new for new sample.", "New Sample");
            TreeNodeFunctions.AddNewSample(Name, TreeView_File, SoundsList);
        }

        private void MenuItem_RemoveSound_Click(object sender, System.EventArgs e)
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sound: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode);
            }
        }

        private void MenuItem_RemoveSample_Click(object sender, System.EventArgs e)
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sample: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXSound ParentSound = TreeNodeFunctions.GetSelectedSound(TreeView_File.SelectedNode.Parent.Name, SoundsList);
                EXFunctions.RemoveSampleFromSound(ParentSound, TreeView_File.SelectedNode.Name);
                TreeView_File.SelectedNode.Remove();
            }
        }

        private void MenuItem_RemoveFolder_Click(object sender, System.EventArgs e)
        {
            /*Check we are not trying to delete the root folder*/
            if (!(TreeView_File.SelectedNode == null || TreeView_File.SelectedNode.Name.Equals("Sounds")))
            {
                /*Show warning*/
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Folder: " + TreeView_File.SelectedNode.Text, "Warning", false);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    /*Remove child nodes sounds and samples*/
                    IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
                    foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_File, TreeView_File.SelectedNode, ChildNodesCollection))
                    {
                        EXFunctions.RemoveSound(ChildNode.Name, SoundsList);
                    }
                    TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode);
                }
            }
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
            string Name;

            /*Create a new sound*/
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.F1)
            {
                Name = OpenInputBox("Enter a name for new sound.", "New Sound");
                TreeNodeFunctions.AddNewSound(Name, TreeView_File, SoundsList);
            }

            /*Createa a new Sample*/
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.F2)
            {
                Name = OpenInputBox("Enter a new for new sample.", "New Sample");
                TreeNodeFunctions.AddNewSample(Name, TreeView_File, SoundsList);
            }
        }

        //*===============================================================================================
        //* MAIN MENU FILE
        //*===============================================================================================
        private void MenuItem_File_Open_Click(object sender, System.EventArgs e)
        {
            string FilePath = Browsers.OpenFileBrowser("EuroSoundFile|*.ESF", 0);
            if (!string.IsNullOrEmpty(FilePath))
            {
                LoadedFile = FilePath;
                SaveData.LoadDataFromEuroSoundFile(TreeView_File, SoundsList, ListView_WavHeaderData, CurrentFileLabel, FilePath);
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
            OpenSaveAsDialog();
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
                    if (Sample.Audio.IsEmpty() == false)
                    {
                        if (Sample.Audio.IsEmpty() == false)
                        {
                            ListViewItem AudioData = new ListViewItem(new[] { Sample.DisplayName, Sample.Audio.Frequency.ToString(), Sample.Audio.Channels.ToString(), Sample.Audio.Bits.ToString(), Sample.Audio.PCMdata.Length.ToString(), Sample.Audio.Encoding, Sample.Audio.Duration.ToString() });
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

        private void MenuItemFile_Export_Click(object sender, System.EventArgs e)
        {
            string SavePath = Browsers.SaveFileBrowser("SFX Files (*.SFX)|*.SFX", 1, true);
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    EXBuildSFX.ExportContentToSFX(SoundsList, SavePath);
                }
            }
        }
    }
}