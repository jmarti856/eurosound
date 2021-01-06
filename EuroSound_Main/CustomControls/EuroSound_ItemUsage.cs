using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_ItemUsage : Form
    {
        private string FormID;
        private List<string> UsageItemsList;
        private Form FormToSearch;

        public EuroSound_ItemUsage(List<string> ListToPrint, string _FormID)
        {
            InitializeComponent();
            UsageItemsList = ListToPrint;
            FormID = _FormID;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EuroSound_ItemUsage_Shown(object sender, EventArgs e)
        {
            string[] LineSplit;
            FormToSearch = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", FormID);
            if (FormToSearch.GetType() == typeof(Frm_Soundbanks_Main))
            {
                foreach (string Item in UsageItemsList)
                {
                    LineSplit = Item.Split(',');
                    TreeNode NodeSound = ((Frm_Soundbanks_Main)FormToSearch).TreeView_File.Nodes.Find(LineSplit[1], true)[0];
                    ListViewItem ItemToAdd = new ListViewItem(new[] { LineSplit[0], NodeSound.Text })
                    {
                        ImageIndex = 0
                    };
                    ItemToAdd.Tag = LineSplit[1];
                    ListView_ItemUsage.Items.Add(ItemToAdd);
                }
                UsageItemsList = null;
            }
        }

        private void ListView_ItemUsage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_ItemUsage.SelectedItems.Count > 0)
            {
                if (FormToSearch.GetType() == typeof(Frm_Soundbanks_Main))
                {
                    string NodeKey = ListView_ItemUsage.SelectedItems[0].Tag.ToString();
                    TreeNode[] NodeSound = ((Frm_Soundbanks_Main)FormToSearch).TreeView_File.Nodes.Find(NodeKey, true);
                    if (NodeSound.Length > 0)
                    {
                        ((Frm_Soundbanks_Main)FormToSearch).OpenSoundProperties(NodeSound[0]);
                    }
                }
            }
        }
    }
}