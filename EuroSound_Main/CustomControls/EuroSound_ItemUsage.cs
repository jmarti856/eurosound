using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_ItemUsage : Form
    {
        private string FormID, FormName;
        private List<string> UsageItemsList;
        private Form SoundbanksParentForm;

        public EuroSound_ItemUsage(List<string> ListToPrint, string _FormID, string _FormName)
        {
            InitializeComponent();
            UsageItemsList = ListToPrint;
            FormID = _FormID;
            FormName = _FormName;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EuroSound_ItemUsage_Shown(object sender, EventArgs e)
        {
            string[] LineSplit;
            if (FormName.Equals("Frm_Soundbanks_Main"))
            {
                SoundbanksParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", FormID);
                foreach (string Item in UsageItemsList)
                {
                    LineSplit = Item.Split(',');
                    TreeNode NodeSound = ((Frm_Soundbanks_Main)SoundbanksParentForm).TreeView_File.Nodes.Find(LineSplit[1], true)[0];
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
                if (FormName.Equals("Frm_Soundbanks_Main"))
                {
                    string NodeKey = ListView_ItemUsage.SelectedItems[0].Tag.ToString();
                    TreeNode NodeSound = ((Frm_Soundbanks_Main)ParentForm).TreeView_File.Nodes.Find(NodeKey, true)[0];
                    ((Frm_Soundbanks_Main)ParentForm).OpenSoundProperties(NodeSound);
                }
            }
        }
    }
}