using System;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Splash : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private string ArgumentFileToLoad;
        private WindowsRegistryFunctions WRegistryFunctions;

        public Frm_EuroSound_Splash(string FileToLoad)
        {
            InitializeComponent();
            ArgumentFileToLoad = FileToLoad;
        }

        //*===============================================================================================
        //* FORMS EVENTS
        //*===============================================================================================
        private async void Frm_EuroSound_Splash_Shown(object sender, EventArgs e)
        {
            //* Variables Declaration
            WRegistryFunctions = new WindowsRegistryFunctions();
            Random rnd = new Random();
            GenericFunctions.ResourcesManager = new ResourceManager(typeof(Properties.Resources));
            string[] HashTable_Sounds, HashTable_SoundsData, HashTable_Musics, TreeViewPreferences;
            Label_EuroSoundVersion.Text = AssemblyDescription + " Version " + AssemblyVersion[0];

            //* --Load Preferences--
            Label_Status.Text = "Loading preferences, please wait...";
            HashTable_Sounds = WRegistryFunctions.LoadHashTablePathAndMD5("Sounds");
            GlobalPreferences.HT_SoundsPath = HashTable_Sounds[0];
            GlobalPreferences.HT_SoundsMD5 = HashTable_Sounds[1];

            await Task.Delay(rnd.Next(100, 160));

            HashTable_SoundsData = WRegistryFunctions.LoadHashTablePathAndMD5("SoundsData");
            GlobalPreferences.HT_SoundsDataPath = HashTable_SoundsData[0];
            GlobalPreferences.HT_SoundsDataMD5 = HashTable_SoundsData[1];

            await Task.Delay(rnd.Next(142, 200));

            HashTable_Musics = WRegistryFunctions.LoadHashTablePathAndMD5("Musics");
            GlobalPreferences.HT_MusicPath = HashTable_Musics[0];
            GlobalPreferences.HT_MusicMD5 = HashTable_Musics[1];

            await Task.Delay(rnd.Next(82, 100));
            TreeViewPreferences = WRegistryFunctions.LoadTreeViewPreferences();
            GlobalPreferences.SelectedFont = TreeViewPreferences[0];
            GlobalPreferences.ShowLines = Convert.ToBoolean(TreeViewPreferences[1]);
            GlobalPreferences.ShowRootLines = Convert.ToBoolean(TreeViewPreferences[2]);
            GlobalPreferences.TreeViewIndent = int.Parse(TreeViewPreferences[3]);

            await Task.Delay(rnd.Next(1, 5));
            GlobalPreferences.SFXOutputPath = WRegistryFunctions.LoadGeneralPreferences();

            //* --Load Sound Data Hashcodes--
            Label_Status.Text = "Loading sounds data hashtable, please wait...";
            Hashcodes.LoadSoundDataFile();

            await Task.Delay(rnd.Next(200, 340));

            //* --Load Sound Hashcodes--
            Label_Status.Text = "Loading sounds hashtable, please wait...";
            Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);

            await Task.Delay(rnd.Next(400, 500));

            //* --Start Form--
            Frm_EuroSound_Main EuroSoundMain = new Frm_EuroSound_Main(ArgumentFileToLoad)
            {
                Owner = this
            };
            this.Hide();
            EuroSoundMain.ShowDialog();
            this.Close();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}