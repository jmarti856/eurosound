using EuroSound_Application.Clases;
using EuroSound_Application.FunctionsListView;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.SearcherForm
{
    public partial class EuroSound_SearchItem : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private string FormName;
        private Form FormToSearch;
        private List<TreeNode> Results;
        private Type FormType;
        private ListViewFunctions LVFunctions = new ListViewFunctions();

        public EuroSound_SearchItem(string CurrentFormName)
        {
            InitializeComponent();
            FormName = CurrentFormName;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void EuroSound_SearchItem_Load(object sender, EventArgs e)
        {
            //Get type of the form on we have to search
            FormToSearch = GenericFunctions.GetFormByName(FormName, Tag.ToString());
            FormType = FormToSearch.GetType();

            //InsertItems To Combobox
            Combobox_LookIn.DisplayMember = "Text";
            Combobox_LookIn.ValueMember = "Value";

            Combobox_LookIn.Items.Add(new { Text = "All", Value = 9 });
            if (FormType == typeof(Frm_Soundbanks_Main))
            {
                Combobox_LookIn.Items.Add(new { Text = "Audio Data", Value = 0 });
                Combobox_LookIn.Items.Add(new { Text = "Sounds", Value = 1 });
                Combobox_LookIn.Items.Add(new { Text = "Stream Sounds", Value = 2 });
            }
            else if (FormType == typeof(Frm_StreamSounds_Main))
            {
                Combobox_LookIn.Items.Add(new { Text = "Sounds", Value = 1 });
            }
            else if (FormType == typeof(Frm_Musics_Main))
            {
                Combobox_LookIn.Items.Add(new { Text = "Musics", Value = 1 });
            }

            //Show Data
            Combobox_LookIn.SelectedIndex = 0;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Search_Click(object sender, EventArgs e)
        {
            if (!BgWorker_Searches.IsBusy)
            {
                //Clear stored data from previous searchs
                if (Results != null)
                {
                    Results.Clear();
                }
                ListViewResults.Items.Clear();

                //Reset Status Bar
                ListViewResults.BeginInvoke((MethodInvoker)delegate
                {
                    Label_Results.Text = string.Join(" ", new string[] { ListViewResults.Items.Count.ToString(), "Items" });
                });

                //Start Search
                BgWorker_Searches.RunWorkerAsync();
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            BgWorker_Searches.CancelAsync();
        }

        private void Button_NewSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private void ListViewResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedItem();
        }

        //*===============================================================================================
        //* Menu Items
        //*===============================================================================================
        //-----File-----
        private void MenuItemFile_New_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private void MenuItemFile_SaveAs_Click(object sender, EventArgs e)
        {
            string PathToSave = BrowsersAndDialogs.SaveFileBrowser("Text File (*.txt)|*.txt", 0, true, "");
            if (!string.IsNullOrEmpty(PathToSave))
            {
                using (StreamWriter sw = File.CreateText(PathToSave))
                {
                    if (Results != null)
                    {
                        foreach (TreeNode Node in Results)
                        {
                            sw.WriteLine(Node.Text);
                        }
                    }
                    sw.Close();
                }
            }
        }

        private void MenuItemFile_Close_Click(object sender, EventArgs e)
        {
            BgWorker_Searches.CancelAsync();
            Close();
        }

        //-----Edit-----
        private void MenuItemEdit_SelectAll_Click(object sender, EventArgs e)
        {
            LVFunctions.SelectAllItems(ListViewResults);
        }

        private void MenuItemEdit_SelectNone_Click(object sender, EventArgs e)
        {
            LVFunctions.SelectNone(ListViewResults);
        }

        private void MenuItemEdit_InvertSelection_Click(object sender, EventArgs e)
        {
            LVFunctions.InvertSelection(ListViewResults);
        }

        //-----Menu Object-----
        private void MenuItemObject_Edit_Click(object sender, EventArgs e)
        {
            OpenSelectedItem();
        }

        private void MenuItemObject_TextColor_Click(object sender, EventArgs e)
        {
            if (ListViewResults.SelectedItems.Count > 0)
            {
                if (FormToSearch != null)
                {
                    //Get the node we have to modify
                    TreeNode NodeToChange = Results[(ListViewResults.SelectedItems[0].Index)];

                    //Apply color
                    Color NewColor = Color.FromArgb(BrowsersAndDialogs.ColorPickerDialog(NodeToChange.ForeColor));
                    NodeToChange.ForeColor = NewColor;

                    //Update cell color from the list view item
                    ListViewResults.SelectedItems[0].SubItems[1].BackColor = NewColor;
                    ListViewResults.SelectedItems[0].UseItemStyleForSubItems = false;
                }
            }
        }

        //*===============================================================================================
        //* BACKGROUND WORKER
        //*===============================================================================================
        private void BgWorker_Searches_DoWork(object sender, DoWorkEventArgs e)
        {
            int ComboSelectedIndex = 0;

            //Get Selected Index
            Combobox_LookIn.Invoke((MethodInvoker)delegate
            {
                ComboSelectedIndex = (int)(Combobox_LookIn.SelectedItem as dynamic).Value;
            });

            //Soundbanks main
            if (FormType == typeof(Frm_Soundbanks_Main))
            {
                //Search all
                if (ComboSelectedIndex == 9)
                {
                    Results = GetNodeCollection(((Frm_Soundbanks_Main)FormToSearch).TreeView_File.Nodes, ((Frm_Soundbanks_Main)FormToSearch).TreeView_File, Textbox_TextToSearch.Text.Trim(), RadioButton_MatchCase.Checked, e);
                }
                //Search Specific
                else
                {
                    Results = GetNodeCollection(((Frm_Soundbanks_Main)FormToSearch).TreeView_File.Nodes[ComboSelectedIndex].Nodes, ((Frm_Soundbanks_Main)FormToSearch).TreeView_File, Textbox_TextToSearch.Text.Trim(), RadioButton_MatchCase.Checked, e);
                }
            }
            //Stream Sounds
            else if (FormType == typeof(Frm_StreamSounds_Main))
            {
                Results = GetNodeCollection(((Frm_StreamSounds_Main)FormToSearch).TreeView_StreamData.Nodes, ((Frm_StreamSounds_Main)FormToSearch).TreeView_StreamData, Textbox_TextToSearch.Text.Trim(), RadioButton_MatchCase.Checked, e);
            }
            //Musics
            else if (FormType == typeof(Frm_Musics_Main))
            {
                Results = GetNodeCollection(((Frm_Musics_Main)FormToSearch).TreeView_MusicData.Nodes, ((Frm_Musics_Main)FormToSearch).TreeView_MusicData, Textbox_TextToSearch.Text.Trim(), RadioButton_MatchCase.Checked, e);
            }

            //Print list
            if (Results.Count > 0)
            {
                PrintList(Results, e);
            }
        }

        private void BgWorker_Searches_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Label_Results.Text = "Cancelled";
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void OpenSelectedItem()
        {
            if (ListViewResults.SelectedItems.Count > 0)
            {
                if (FormToSearch != null)
                {
                    TreeNode NodeToOpen = Results[(ListViewResults.SelectedItems[0].Index)];
                    string ObjectType = NodeToOpen.Tag.ToString();

                    if (FormType == typeof(Frm_Soundbanks_Main))
                    {
                        if (ObjectType.Equals("Sound"))
                        {
                            ((Frm_Soundbanks_Main)FormToSearch).OpenSoundProperties(NodeToOpen);
                        }
                        else if (ObjectType.Equals("Sample"))
                        {
                            ((Frm_Soundbanks_Main)FormToSearch).OpenSampleProperties(NodeToOpen);
                        }
                        else if (ObjectType.Equals("Audio"))
                        {
                            ((Frm_Soundbanks_Main)FormToSearch).OpenAudioProperties(NodeToOpen);
                        }
                    }
                    else if (FormType == typeof(Frm_StreamSounds_Main))
                    {
                        if (ObjectType.Equals("Sound"))
                        {
                            ((Frm_StreamSounds_Main)FormToSearch).OpenSoundPropertiesForm(NodeToOpen);
                        }
                    }
                    else if (FormType == typeof(Frm_Musics_Main))
                    {
                        if (ObjectType.Equals("Music"))
                        {
                            ((Frm_Musics_Main)FormToSearch).OpenMusicPropertiesForm(NodeToOpen);
                        }
                    }
                }
            }
        }

        private void PrintList(List<TreeNode> ListToPrint, DoWorkEventArgs e)
        {
            foreach (TreeNode NodeToSearch in ListToPrint)
            {
                if (BgWorker_Searches.CancellationPending)
                {
                    e.Cancel = true;
                    Label_Results.Text = "Cancelling";
                    break;
                }
                else
                {
                    ListViewItem ItemToAdd = new ListViewItem(new[] { NodeToSearch.Text, "", NodeToSearch.Tag.ToString(), NodeToSearch.Parent.Text });
                    ItemToAdd.SubItems[1].BackColor = NodeToSearch.ForeColor;
                    ItemToAdd.UseItemStyleForSubItems = false;

                    try
                    {
                        ListViewResults.BeginInvoke((MethodInvoker)delegate
                        {
                            ListViewResults.Items.Add(ItemToAdd);
                        });

                        //Results Count
                        ListViewResults.BeginInvoke((MethodInvoker)delegate
                        {
                            Label_Results.Text = string.Join(" ", new string[] { ListViewResults.Items.Count.ToString(), "Items" });
                        });
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        private List<TreeNode> GetNodeCollection(TreeNodeCollection Collection, TreeView ControlToSearch, string SearchFor, bool MatchOnly, DoWorkEventArgs e)
        {
            List<TreeNode> Matches = new List<TreeNode>();

            SearchNodeRecursiveByText(Collection, SearchFor.ToLower(), ControlToSearch, MatchOnly, Matches, e);

            return Matches;
        }

        internal void SearchNodeRecursiveByText(IEnumerable NodeParent, string searchFor, TreeView TreeViewControl, bool MatchOnly, List<TreeNode> Matches, DoWorkEventArgs e)
        {
            foreach (TreeNode ChildNode in NodeParent)
            {
                if (BgWorker_Searches.CancellationPending)
                {
                    e.Cancel = true;
                    Label_Results.Text = "Cancelling";
                    break;
                }
                else
                {
                    //Search inside folders
                    if (ChildNode.Level == 0 || Convert.ToByte(ChildNode.Tag) == (byte)Enumerations.TreeNodeType.Folder)
                    {
                        SearchNodeRecursiveByText(ChildNode.Nodes, searchFor, TreeViewControl, MatchOnly, Matches, e);
                    }
                    else
                    {
                        //Search type
                        if (MatchOnly)
                        {
                            if (ChildNode.Text.ToLower().Contains(searchFor))
                            {
                                Matches.Add(ChildNode);
                            }
                        }
                        else
                        {
                            if (ChildNode.Text.Equals(searchFor, StringComparison.OrdinalIgnoreCase))
                            {
                                Matches.Add(ChildNode);
                            }
                        }

                        if (ChildNode.Nodes.Count > 0)
                        {
                            SearchNodeRecursiveByText(ChildNode.Nodes, searchFor, TreeViewControl, MatchOnly, Matches, e);
                        }
                    }
                }
            }
        }

        private void ClearSearch()
        {
            if (ListViewResults.Items.Count > 0)
            {
                if (!BgWorker_Searches.IsBusy)
                {
                    DialogResult AskToClearSearch = MessageBox.Show(GenericFunctions.resourcesManager.GetString("SearcherInfoClearButton"), "EuroSound", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (AskToClearSearch == DialogResult.OK)
                    {
                        //Clear stored data from previous searchs
                        if (Results != null)
                        {
                            Results.Clear();
                        }
                        ListViewResults.Items.Clear();
                        Textbox_TextToSearch.Clear();
                        Label_Results.Text = "0 Items";
                    }
                }
            }
        }
    }
}