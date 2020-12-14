using System;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_EuroSound_Splash : Form
    {
        string ArgumentFileToLoad;

        public Frm_EuroSound_Splash(string FileToLoad)
        {
            InitializeComponent();
            ArgumentFileToLoad = FileToLoad;
        }

        private async void Frm_EuroSound_Splash_Shown(object sender, EventArgs e)
        {
            GenericFunctions.ResourcesManager = new ResourceManager(typeof(Properties.Resources));

            /*Declare Variables*/
            string[] HashTable_Sounds, HashTable_SoundsData, HashTable_Musics;

            Label_EuroSoundVersion.Text = AssemblyDescription + " Version " + AssemblyVersion[0];

            /*--Load Preferences--*/
            Label_Status.Text = "Loading preferences, please wait...";
            WindowsRegistryFunctions.CreateEuroSoundKeyIfNotExists();
            HashTable_Sounds = WindowsRegistryFunctions.LoadHashTablePathAndMD5("Sounds");
            GlobalPreferences.HT_SoundsPath = HashTable_Sounds[0];
            GlobalPreferences.HT_SoundsMD5 = HashTable_Sounds[1];

            await Task.Delay(150);

            HashTable_SoundsData = WindowsRegistryFunctions.LoadHashTablePathAndMD5("SoundsData");
            GlobalPreferences.HT_SoundsDataPath = HashTable_SoundsData[0];
            GlobalPreferences.HT_SoundsDataMD5 = HashTable_SoundsData[1];

            await Task.Delay(200);

            HashTable_Musics = WindowsRegistryFunctions.LoadHashTablePathAndMD5("Musics");
            GlobalPreferences.HT_MusicPath = HashTable_Musics[0];
            GlobalPreferences.HT_MusicMD5 = HashTable_Musics[1];

            /*--Load Sound Data Hashcodes--*/
            Label_Status.Text = "Loading Hashcodes Sounds Data, please wait...";
            Hashcodes.LoadSoundDataFile();

            await Task.Delay(340);

            /*--Load Sound Hashcodes--*/
            Label_Status.Text = "Loading Hashcodes Sounds, please wait...";
            Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);

            await Task.Delay(500);

            /*--Start Form--*/
            Frm_EuroSound_Main EuroSoundMain = new Frm_EuroSound_Main(ArgumentFileToLoad)
            {
                Owner = this
            };
            this.Hide();
            EuroSoundMain.ShowDialog();
            this.Close();
        }

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
