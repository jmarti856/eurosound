using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_MainPreferences : Form
    {
        private Frm_HashTablesConfig GeneralHashTable;
        private Frm_General GeneralPreferences;
        private Frm_TreeViewPrefs TreeViewPrefs;
        public Frm_MainPreferences()
        {
            InitializeComponent();
        }

        private static void RemoveAllFormsInsidePanel(Panel p_control)
        {
            Control.ControlCollection FormsToClose = p_control.Controls;
            if (FormsToClose.Count > 0)
            {
                for (int i = 0; i < FormsToClose.Count; i++)
                {
                    ((Form)FormsToClose[i]).Close();
                }
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            RemoveAllFormsInsidePanel(Panel_SecondaryForms);

            /*-----------------[Frm_HashTablesConfig]-----------------*/
            if (!string.IsNullOrEmpty(GlobalPreferences.HT_SoundsPathTEMPORAL))
            {
                GlobalPreferences.HT_SoundsPath = GlobalPreferences.HT_SoundsPathTEMPORAL;
                GlobalPreferences.HT_SoundsDataPath = GlobalPreferences.HT_SoundsDataPathTEMPORAL;
                GlobalPreferences.HT_MusicPath = GlobalPreferences.HT_MusicPathTEMPORAL;

                //SaveConfigs in Registry
                WindowsRegistryFunctions.SaveHashTablePathAndMD5("Sounds", GlobalPreferences.HT_SoundsPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsPath));
                WindowsRegistryFunctions.SaveHashTablePathAndMD5("SoundsData", GlobalPreferences.HT_SoundsDataPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsDataPath));
                WindowsRegistryFunctions.SaveHashTablePathAndMD5("Musics", GlobalPreferences.HT_MusicPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_MusicPath));
            }

            /*-----------------[Frm_TreeViewPrefs]-----------------*/
            if (!string.IsNullOrEmpty(GlobalPreferences.SelectedFontTEMPORAL))
            {
                GlobalPreferences.SelectedFont = GlobalPreferences.SelectedFontTEMPORAL;
                GlobalPreferences.TreeViewIndent = GlobalPreferences.TreeViewIndentTEMPORAL;
                GlobalPreferences.ShowLines = GlobalPreferences.ShowLinesTEMPORAL;
                GlobalPreferences.ShowRootLines = GlobalPreferences.ShowRootLinesTEMPORAL;

                //SaveConfig in Registry
                WindowsRegistryFunctions.SaveTreeViewPreferences();
            }

            /*-----------------[Frm_GeneralPreferences]-----------------*/
            if (!string.IsNullOrEmpty(GlobalPreferences.SFXOutputPathTEMPORAL))
            {
                GlobalPreferences.SFXOutputPath = GlobalPreferences.SFXOutputPathTEMPORAL;

                //SaveConfig in Registry
                WindowsRegistryFunctions.SaveGeneralPreferences();
            }

            this.Close();
        }

        private void Frm_MainPreferences_Load(object sender, EventArgs e)
        {
            TreeViewPreferences.ExpandAll();
        }
        private void TreeViewPreferences_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Open Sub-form "Frm_HashTablesConfig"
            if (string.Equals(e.Node.Name, "HashTables"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(GeneralHashTable))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    GeneralHashTable = new Frm_HashTablesConfig
                    {
                        TopLevel = false,
                        AutoScroll = true
                    };
                    Panel_SecondaryForms.Controls.Add(GeneralHashTable);
                    GeneralHashTable.Dock = DockStyle.Fill;
                    GeneralHashTable.Show();
                }
            }
            //Open Sub-Form "Frm_TreeViewPrefs"
            else if (string.Equals(e.Node.Name, "ESFTree"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(TreeViewPrefs))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    TreeViewPrefs = new Frm_TreeViewPrefs
                    {
                        TopLevel = false,
                        AutoScroll = true
                    };
                    Panel_SecondaryForms.Controls.Add(TreeViewPrefs);
                    TreeViewPrefs.Dock = DockStyle.Fill;
                    TreeViewPrefs.Show();
                }
            }
            //Open Sub-Form "Frm_GeneralPreferences"
            else if (string.Equals(e.Node.Name, "General"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(GeneralPreferences))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    GeneralPreferences = new Frm_General
                    {
                        TopLevel = false,
                        AutoScroll = true
                    };
                    Panel_SecondaryForms.Controls.Add(GeneralPreferences);
                    GeneralPreferences.Dock = DockStyle.Fill;
                    GeneralPreferences.Show();
                }
            }
        }
    }
}