using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application.SplashForm
{
    public partial class Frm_EuroSound_Splash : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        public Frm_EuroSound_Splash()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORMS EVENTS
        //*===============================================================================================
        private async void Frm_EuroSound_Splash_Shown(object sender, EventArgs e)
        {
            Random RandomNumber = new Random();

            //-----------------------------------------[Variables Declaration]----------------------------------------
            WindowsRegistryFunctions WRegistryFunctions = new WindowsRegistryFunctions();
            GenericFunctions.ResourcesManager = new ResourceManager(typeof(Properties.Resources));
            string[] HashTable_Sounds, HashTable_SoundsData, HashTable_Musics, TreeViewPreferences;
            Label_EuroSoundVersion.Text = string.Join(" ", new string[] { AssemblyDescription, "Version", GenericFunctions.GetEuroSoundVersion()[0].ToString() });

            //-----------------------------------------[Load Preferences]----------------------------------------
            Label_Status.Text = "Loading preferences, please wait...";
            HashTable_Sounds = WRegistryFunctions.LoadHashTablePathAndMD5("Sounds");
            GlobalPreferences.HT_SoundsPath = HashTable_Sounds[0];
            GlobalPreferences.HT_SoundsMD5 = HashTable_Sounds[1];

            await Task.Delay(RandomNumber.Next(50, 150));

            HashTable_SoundsData = WRegistryFunctions.LoadHashTablePathAndMD5("SoundsData");
            GlobalPreferences.HT_SoundsDataPath = HashTable_SoundsData[0];
            GlobalPreferences.HT_SoundsDataMD5 = HashTable_SoundsData[1];

            await Task.Delay(RandomNumber.Next(32, 160));

            HashTable_Musics = WRegistryFunctions.LoadHashTablePathAndMD5("Musics");
            GlobalPreferences.HT_MusicPath = HashTable_Musics[0];
            GlobalPreferences.HT_MusicMD5 = HashTable_Musics[1];

            await Task.Delay(RandomNumber.Next(12, 100));
            TreeViewPreferences = WRegistryFunctions.LoadTreeViewPreferences();
            GlobalPreferences.SelectedFont = TreeViewPreferences[0];
            GlobalPreferences.ShowLines = Convert.ToBoolean(int.Parse(TreeViewPreferences[1]));
            GlobalPreferences.ShowRootLines = Convert.ToBoolean(int.Parse(TreeViewPreferences[2]));
            GlobalPreferences.TreeViewIndent = int.Parse(TreeViewPreferences[3]);

            await Task.Delay(RandomNumber.Next(1, 50));
            string[] OutputPaths = WRegistryFunctions.LoadGeneralPreferences();
            GlobalPreferences.SFXOutputPath = OutputPaths[0];
            GlobalPreferences.MusicOutputPath = OutputPaths[1];
            GlobalPreferences.ColorWavesControl = int.Parse(OutputPaths[2]);
            GlobalPreferences.BackColorWavesControl = int.Parse(OutputPaths[3]);

            await Task.Delay(RandomNumber.Next(5, 80));
            GlobalPreferences.UseSystemTray = Convert.ToBoolean(WRegistryFunctions.GetSystemConfig());

            //-----------------------------------------[Sound Data]----------------------------------------
            Label_Status.Text = "Loading sounds data hashtable, please wait...";
            Hashcodes.LoadSoundDataFile();

            await Task.Delay(RandomNumber.Next(30, 120));

            //-----------------------------------------[Sound Defines]----------------------------------------
            Label_Status.Text = "Loading sounds hashtable, please wait...";
            Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);

            await Task.Delay(RandomNumber.Next(80, 100));

            //-----------------------------------------[Music Defines]----------------------------------------
            Label_Status.Text = "Loading musics hashtable, please wait...";
            Hashcodes.LoadMusicHashcodes();

            await Task.Delay(RandomNumber.Next(30, 120));

            //-----------------------------------------[External File]----------------------------------------
            Label_Status.Text = "Loading External File Path, please wait...";
            GlobalPreferences.StreamFilePath = WRegistryFunctions.SetExternalFilePath();

            await Task.Delay(RandomNumber.Next(25, 43));
            //-----------------------------------------[SoX Executable]----------------------------------------
            string SoXExePath;
            Label_Status.Text = "Loading SoX Executable Path, please wait...";
            SoXExePath = WRegistryFunctions.SetSoxFilePath();

            if (string.IsNullOrEmpty(SoXExePath))
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("SoXNoPath"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (File.Exists(SoXExePath))
                {
                    GlobalPreferences.SoXPath = SoXExePath;
                }
                else
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("SoXInvalidPath"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            await Task.Delay(RandomNumber.Next(20, 50));

            //-----------------------------------------[Create Flags]---------------------------------------
            Label_Status.Text = "Loading Controller Flags, please wait...";
            WRegistryFunctions.SaveSFXFlagsIfNecessary();
            WRegistryFunctions.SaveAudioFlagsIfNecessary();

            await Task.Delay(RandomNumber.Next(20, 35));

            //-----------------------------------------[Load AudioDevice]---------------------------------------
            Label_Status.Text = "Loading Audio Devices Preferences, please wait...";
            WRegistryFunctions.SetDefaultAudioDevice();

            await Task.Delay(RandomNumber.Next(10, 50));
            //-----------------------------------------[Open Form]----------------------------------------
            DialogResult = DialogResult.OK;
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
                    return string.Empty;
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
    }
}