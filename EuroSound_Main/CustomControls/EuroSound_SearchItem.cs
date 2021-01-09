using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
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
            Combobox_LookIn.Items.Add(new { Text = "All", Value = 9 });
            if (FormType == typeof(Frm_Soundbanks_Main))
            {
                Combobox_LookIn.Items.Add(new { Text = "Audio Data", Value = 0 });
                Combobox_LookIn.Items.Add(new { Text = "Sounds", Value = 1 });
                Combobox_LookIn.Items.Add(new { Text = "Stream Sounds", Value = 2 });
            }

            //Show Data
            Combobox_LookIn.DisplayMember = "Text";
            Combobox_LookIn.ValueMember = "Value";
            Combobox_LookIn.SelectedIndex = 0;
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Search_Click(object sender, EventArgs e)
        {
            if (!BgWorker_Searches.IsBusy)
            {
                /*Clear stored data from previous searchs*/
                if (Results != null)
                {
                    Results.Clear();
                }
                ListViewResults.Items.Clear();

                /*Reset Status Bar*/
                ListViewResults.Invoke((MethodInvoker)delegate
                {
                    Label_Results.Text = string.Format("{0} Items", ListViewResults.Items.Count);
                });

                /*Start Search*/
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
        /*-----File-----*/
        private void MenuItemFile_New_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private void MenuItemFile_SaveAs_Click(object sender, EventArgs e)
        {
            string PathToSave = GenericFunctions.SaveFileBrowser("Text File (*.txt)|*.txt", 0, true, "");
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

        /*edit*/
        private void MenuItemEdit_SelectAll_Click(object sender, EventArgs e)
        {
            Thread SelectAllItems = new Thread(() =>
            {
                ListViewResults.Invoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem item in ListViewResults.Items)
                    {
                        item.Selected = true;
                    }

                    ListViewResults.Focus();
                });
            });
            SelectAllItems.Start();
            SelectAllItems.IsBackground = true;
        }

        private void MenuItemEdit_SelectNone_Click(object sender, EventArgs e)
        {
            Thread SelectAllItems = new Thread(() =>
            {
                ListViewResults.Invoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem item in ListViewResults.Items)
                    {
                        item.Selected = false;
                    }

                    ListViewResults.Focus();
                });
            });
            SelectAllItems.Start();
            SelectAllItems.IsBackground = true;
        }

        private void MenuItemEdit_InvertSelection_Click(object sender, EventArgs e)
        {
            Thread InvertCurrentSelection = new Thread(() =>
            {
                ListViewResults.Invoke((MethodInvoker)delegate
                {
                    int[] selectedArray = new int[ListViewResults.SelectedIndices.Count];

                    ListViewResults.SelectedIndices.CopyTo(selectedArray, 0);

                    HashSet<int> selected = new HashSet<int>();
                    selected.UnionWith(selectedArray);

                    ListViewResults.SelectedIndices.Clear();
                    for (int i = 0; i < ListViewResults.Items.Count; i++)
                    {
                        if (!selected.Contains(i))
                        {
                            ListViewResults.SelectedIndices.Add(i);
                        }
                    }
                    ListViewResults.Focus();

                    selected.Clear();
                });
            });
            InvertCurrentSelection.Start();
            InvertCurrentSelection.IsBackground = true;
        }

        /*-------Menu Object------*/
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
                    Color NewColor = Color.FromArgb(GenericFunctions.GetColorFromColorPicker());
                    Results[(ListViewResults.SelectedItems[0].Index)].ForeColor = NewColor;

                    /*Update cell color from the list view item*/
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

            /*Get Selected Index*/
            Combobox_LookIn.Invoke((MethodInvoker)delegate
            {
                ComboSelectedIndex = (int)(Combobox_LookIn.SelectedItem as dynamic).Value;
            });

            /*Soundbanks main*/
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

            /*Print list*/
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
            string ObjectType;

            if (ListViewResults.SelectedItems.Count > 0)
            {
                if (FormToSearch != null)
                {
                    if (FormType == typeof(Frm_Soundbanks_Main))
                    {
                        TreeNode NodeToOpen = Results[(ListViewResults.SelectedItems[0].Index)];
                        ObjectType = NodeToOpen.Tag.ToString();
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
                        ListViewResults.Invoke((MethodInvoker)delegate
                        {
                            ListViewResults.Items.Add(ItemToAdd);
                        });

                        //Results Count
                        ListViewResults.Invoke((MethodInvoker)delegate
                        {
                            Label_Results.Text = string.Format("{0} Items", ListViewResults.Items.Count);
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

            SearchNodeRecursiveByText(Collection, SearchFor, ControlToSearch, MatchOnly, Matches, e);

            return Matches;
        }

        internal void SearchNodeRecursiveByText(IEnumerable nodes, string searchFor, TreeView TreeViewControl, bool MatchOnly, List<TreeNode> Matches, DoWorkEventArgs e)
        {
            foreach (TreeNode node in nodes)
            {
                if (BgWorker_Searches.CancellationPending)
                {
                    e.Cancel = true;
                    Label_Results.Text = "Cancelling";
                    break;
                }
                else
                {
                    if (MatchOnly)
                    {
                        if (!(node.Tag.Equals("Root") || node.Tag.Equals("Folder")))
                        {
                            if (node.Text.ToLower().Contains(searchFor))
                            {
                                Matches.Add(node);
                            }
                        }
                    }
                    else
                    {
                        if (!(node.Tag.Equals("Root") || node.Tag.Equals("Folder")))
                        {
                            if (node.Text.Equals(searchFor, StringComparison.OrdinalIgnoreCase))
                            {
                                Matches.Add(node);
                            }
                        }
                    }
                    if (node != null)
                    {
                        SearchNodeRecursiveByText(node.Nodes, searchFor, TreeViewControl, MatchOnly, Matches, e);
                    }
                }
            }
        }

        private void ClearSearch()
        {
            if (!BgWorker_Searches.IsBusy)
            {
                DialogResult AskToClearSearch = MessageBox.Show("This will clear your current search", "EuroSound", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (AskToClearSearch == DialogResult.OK)
                {
                    /*Clear stored data from previous searchs*/
                    if (Results != null)
                    {
                        Results.Clear();
                    }
                    ListViewResults.Items.Clear();
                    Textbox_TextToSearch.Clear();
                }
            }
        }
    }
}