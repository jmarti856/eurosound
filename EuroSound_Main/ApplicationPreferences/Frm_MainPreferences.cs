using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using System;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_MainPreferences : Form
    {
        //*===============================================================================================
        //* TEMPORAL PROPERTIES
        //*===============================================================================================
        //Frm_HashTablesConfig
        internal string HT_SoundsPathTEMPORAL;
        internal string HT_SoundsDataPathTEMPORAL;
        internal string HT_MusicPathTEMPORAL;

        //Frm_TreeViewPrefs
        internal string SelectedFontTEMPORAL;
        internal int TreeViewIndentTEMPORAL;
        internal bool ShowLinesTEMPORAL;
        internal bool ShowRootLinesTEMPORAL;

        //Frm_GeneralPreferences
        internal string SFXOutputPathTEMPORAL;
        internal string MusicOutputPathTEMPORAL;
        internal int ColorWavesControlTEMPORAL;
        internal int BackColorWavesControlTEMPORAL;

        //Frm_StreamFile
        internal string StreamFilePathTEMPORAL;

        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private Frm_HashTablesConfig GeneralHashTable;
        private Frm_General GeneralPreferences;
        private Frm_TreeViewPrefs TreeViewPrefs;
        private Frm_StreamFile ExternalFile;

        public Frm_MainPreferences()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_MainPreferences_Load(object sender, EventArgs e)
        {
            TreeViewPreferences.ExpandAll();
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
            RemoveAllFormsInsidePanel(Panel_SecondaryForms);

            /*-----------------[Frm_HashTablesConfig]-----------------*/
            if (!string.IsNullOrEmpty(HT_SoundsPathTEMPORAL))
            {
                GlobalPreferences.HT_SoundsPath = HT_SoundsPathTEMPORAL;
                GlobalPreferences.HT_SoundsDataPath = HT_SoundsDataPathTEMPORAL;
                GlobalPreferences.HT_MusicPath = HT_MusicPathTEMPORAL;

                //SaveConfigs in Registry
                WRegistryFunctions.SaveHashTablePathAndMD5("Sounds", GlobalPreferences.HT_SoundsPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsPath));
                WRegistryFunctions.SaveHashTablePathAndMD5("SoundsData", GlobalPreferences.HT_SoundsDataPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsDataPath));
                WRegistryFunctions.SaveHashTablePathAndMD5("Musics", GlobalPreferences.HT_MusicPath, GenericFunctions.CalculateMD5(GlobalPreferences.HT_MusicPath));
            }

            /*-----------------[Frm_TreeViewPrefs]-----------------*/
            if (!string.IsNullOrEmpty(SelectedFontTEMPORAL))
            {
                GlobalPreferences.SelectedFont = SelectedFontTEMPORAL;
                GlobalPreferences.TreeViewIndent = TreeViewIndentTEMPORAL;
                GlobalPreferences.ShowLines = ShowLinesTEMPORAL;
                GlobalPreferences.ShowRootLines = ShowRootLinesTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveTreeViewPreferences();
            }

            /*-----------------[Frm_GeneralPreferences]-----------------*/
            if (!string.IsNullOrEmpty(SFXOutputPathTEMPORAL))
            {
                GlobalPreferences.SFXOutputPath = SFXOutputPathTEMPORAL;
                GlobalPreferences.MusicOutputPath = MusicOutputPathTEMPORAL;
                GlobalPreferences.ColorWavesControl = ColorWavesControlTEMPORAL;
                GlobalPreferences.BackColorWavesControl = BackColorWavesControlTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveGeneralPreferences();
            }

            /*-----------------[Frm_StreamFile]-----------------*/
            if (!string.IsNullOrEmpty(StreamFilePathTEMPORAL))
            {
                GlobalPreferences.StreamFilePath = StreamFilePathTEMPORAL;

                //SaveConfig in Registry
                WRegistryFunctions.SaveExternalFilePath(GlobalPreferences.StreamFilePath);
            }

            Close();
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
                        AutoScroll = true,
                        Tag = Tag
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
                        AutoScroll = true,
                        Tag = Tag
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
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(GeneralPreferences);
                    GeneralPreferences.Dock = DockStyle.Fill;
                    GeneralPreferences.Show();
                }
            }
            else if (string.Equals(e.Node.Name, "StreamFile"))
            {
                if (!Panel_SecondaryForms.Controls.Contains(ExternalFile))
                {
                    RemoveAllFormsInsidePanel(Panel_SecondaryForms);

                    ExternalFile = new Frm_StreamFile
                    {
                        TopLevel = false,
                        AutoScroll = true,
                        Tag = Tag
                    };
                    Panel_SecondaryForms.Controls.Add(ExternalFile);
                    ExternalFile.Dock = DockStyle.Fill;
                    ExternalFile.Show();
                }
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
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
    }
}