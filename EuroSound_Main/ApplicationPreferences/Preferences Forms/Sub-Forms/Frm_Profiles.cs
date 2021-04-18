using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_Profiles : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_Profiles()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> ProjectName in GenericFunctions.AvailableProfiles)
            {
                ListView_Profiles.Items.Add(new ListViewItem { Text = ProjectName.Key, Tag = ProjectName.Value });
            }

            //Get Last Selected Profile
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            if (!string.IsNullOrEmpty(((Frm_MainPreferences)OpenForm).SelectedProfileNameTEMPORAL))
            {
                foreach (ListViewItem Item in ListView_Profiles.Items)
                {
                    if (Item.Text.Equals(((Frm_MainPreferences)OpenForm).SelectedProfileNameTEMPORAL))
                    {
                        Item.Selected = true;
                        break;
                    }
                }
            }

            //Check if we have any ESF loaded
            if (GenericFunctions.NumberOfChildForms() > 0)
            {
                ListView_Profiles.Enabled = false;
                RichTextbox_ProfilesInfo.Clear();
                RichTextbox_ProfilesInfo.Text += "There is " + GenericFunctions.NumberOfChildForms() + " loaded ESF file. You can't change the global profile wihle ESF files are currently loaded.";
            }
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ListView_Profiles.SelectedItems.Count > 0)
            {
                ((Frm_MainPreferences)OpenForm).SelectedProfileTEMPORAL = ListView_Profiles.SelectedItems[0].Tag.ToString();
                ((Frm_MainPreferences)OpenForm).SelectedProfileNameTEMPORAL = ListView_Profiles.SelectedItems[0].Text;
            }
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void ListView_Profiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView_Profiles.SelectedItems.Count > 0)
            {
                RichTextbox_ProfilesInfo.Clear();
                RichTextbox_ProfilesInfo.Text += "Profile Name: " + ListView_Profiles.SelectedItems[0].Text + "\n";
                RichTextbox_ProfilesInfo.Text += "ESP Used: " + ListView_Profiles.SelectedItems[0].Tag + "\n";
            }
        }

        private void ListView_Profiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (ListView_Profiles.FocusedItem != null)
            {
                if (ListView_Profiles.SelectedItems.Count == 0)
                {
                    ListView_Profiles.FocusedItem.Selected = true;
                }
            }
        }
    }
}
