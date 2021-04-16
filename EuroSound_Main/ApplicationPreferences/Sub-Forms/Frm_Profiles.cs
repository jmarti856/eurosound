using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_Profiles : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;
        private TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        public Frm_Profiles()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_TreeViewPrefs_Load(object sender, EventArgs e)
        {
            //Get Profiles List
            List<string> AvailableProfiles = GetAllAccessibleFiles(@"S:\");
            for (int i = 0; i < AvailableProfiles.Count; i++)
            {
                ListViewItem NewProfile = new ListViewItem
                {
                    Text = myTI.ToTitleCase(Path.GetFileNameWithoutExtension(AvailableProfiles[i])),
                    Tag = AvailableProfiles[i]
                };
                ListView_Profiles.Items.Add(NewProfile);
            }

            //Get Last Selected Profile
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            if (!string.IsNullOrEmpty(((Frm_MainPreferences)OpenForm).SelectedProfileTEMPORAL))
            {
                foreach (ListViewItem Item in ListView_Profiles.Items)
                {
                    if (Item.Tag.ToString().Equals(((Frm_MainPreferences)OpenForm).SelectedProfileTEMPORAL))
                    {
                        Item.Selected = true;
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
            else
            {
                ListView_Profiles.Enabled = true;
            }
        }

        private void Frm_TreeViewPrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ListView_Profiles.SelectedItems.Count > 0)
            {
                ((Frm_MainPreferences)OpenForm).SelectedProfileTEMPORAL = ListView_Profiles.SelectedItems[0].Tag.ToString();
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
                RichTextbox_ProfilesInfo.Text += "Profile Name: " + myTI.ToTitleCase(ListView_Profiles.SelectedItems[0].Text) + "\n";
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

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        public static List<string> GetAllAccessibleFiles(string rootPath, List<string> alreadyFound = null)
        {
            if (alreadyFound == null)
            {
                alreadyFound = new List<string>();
            }

            DirectoryInfo di = new DirectoryInfo(rootPath);
            IEnumerable<DirectoryInfo> dirs = di.EnumerateDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if (!((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    alreadyFound = GetAllAccessibleFiles(dir.FullName, alreadyFound);
                }
            }

            string[] files = Directory.GetFiles(rootPath, "*.ESP");
            foreach (string s in files)
            {
                alreadyFound.Add(s);
            }

            return alreadyFound;
        }
    }
}
