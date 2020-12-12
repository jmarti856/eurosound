using System;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_MainPreferences : Form
    {
        private Frm_HashTablesConfig GeneralHashTable;

        public Frm_MainPreferences()
        {
            InitializeComponent();
        }

        private void Frm_MainPreferences_Load(object sender, EventArgs e)
        {
            TreeViewPreferences.ExpandAll();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            /*-----------------[Frm_HashTablesConfig]-----------------*/
            GlobalPreferences.HT_SoundsPath = GlobalPreferences.HT_SoundsPathTEMPORAL;
            GlobalPreferences.HT_SoundsDataPath = GlobalPreferences.HT_SoundsDataPathTEMPORAL;
            GlobalPreferences.HT_MusicPath = GlobalPreferences.HT_MusicPathTEMPORAL;
            //SaveConfigs in Registry
            WindowsRegistryFunctions.SaveHashTablePathAndMD5("Sounds", GlobalPreferences.HT_SoundsPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsPath));
            WindowsRegistryFunctions.SaveHashTablePathAndMD5("SoundsData", GlobalPreferences.HT_SoundsDataPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsDataPath));
            WindowsRegistryFunctions.SaveHashTablePathAndMD5("Musics", GlobalPreferences.HT_MusicPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_MusicPath));

            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TreeViewPreferences_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Open Sub-form "Frm_HashTablesConfig"
            if (string.Equals(e.Node.Name, "General", StringComparison.InvariantCulture))
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
        }

        private static void RemoveAllFormsInsidePanel(Panel p_control)
        {
            foreach (object control in p_control.Controls)
            {
                p_control.Controls.Remove((Control)control);
            }
        }
    }
}
