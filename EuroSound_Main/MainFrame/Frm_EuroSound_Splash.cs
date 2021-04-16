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
            Label_EuroSoundVersion.Text = string.Join(" ", new string[] { AssemblyDescription, "Version", GenericFunctions.GetEuroSoundVersion()[0].ToString() });

            //*===============================================================================================
            //* Load Profile Settings
            //*===============================================================================================
            GlobalPreferences.SelectedProfile = WRegistryFunctions.LoadCurrentProfie();
            if (!string.IsNullOrEmpty(GlobalPreferences.SelectedProfile))
            {
                //[SoundbanksSettings]
                Label_Status.Text = "Loading soundbanks settings, please wait...";
                GlobalPreferences.SoundbankFrequency = int.Parse(WRegistryFunctions.LoadSoundSettings("SoundBanks", "Frequency"));
                GlobalPreferences.SoundbankEncoding = WRegistryFunctions.LoadSoundSettings("SoundBanks", "Encoding");
                GlobalPreferences.SoundbankBits = int.Parse(WRegistryFunctions.LoadSoundSettings("SoundBanks", "Bits"));
                GlobalPreferences.SoundbankChannels = int.Parse(WRegistryFunctions.LoadSoundSettings("SoundBanks", "Channels"));
                await Task.Delay(RandomNumber.Next(50, 150));

                //[StreamFileSettings]
                Label_Status.Text = "Loading streambanks settings, please wait...";
                GlobalPreferences.StreambankFrequency = int.Parse(WRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Frequency"));
                GlobalPreferences.StreambankEncoding = WRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Encoding");
                GlobalPreferences.StreambankBits = int.Parse(WRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Bits"));
                GlobalPreferences.StreambankChannels = int.Parse(WRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Channels"));
                await Task.Delay(RandomNumber.Next(32, 160));

                //[MusicFileSettings]
                Label_Status.Text = "Loading musicbanks settings, please wait...";
                GlobalPreferences.MusicbankFrequency = int.Parse(WRegistryFunctions.LoadSoundSettings("MusicBanks", "Frequency"));
                GlobalPreferences.MusicbankEncoding = WRegistryFunctions.LoadSoundSettings("MusicBanks", "Encoding");
                GlobalPreferences.MusicbankBits = int.Parse(WRegistryFunctions.LoadSoundSettings("MusicBanks", "Bits"));
                GlobalPreferences.MusicbankChannels = int.Parse(WRegistryFunctions.LoadSoundSettings("MusicBanks", "Channels"));
                await Task.Delay(RandomNumber.Next(12, 100));

                //[HashTableFiles]
                Label_Status.Text = "Loading hastables files, please wait...";
                GlobalPreferences.HT_SoundsPath = WRegistryFunctions.LoadHashtablesFiles("HT_Sound", "Path");
                GlobalPreferences.HT_SoundsMD5 = WRegistryFunctions.LoadHashtablesFiles("HT_Sound", "MD5");
                await Task.Delay(RandomNumber.Next(50, 150));

                GlobalPreferences.HT_SoundsDataPath = WRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "Path");
                GlobalPreferences.HT_SoundsDataMD5 = WRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "MD5");
                await Task.Delay(RandomNumber.Next(32, 160));

                GlobalPreferences.HT_MusicPath = WRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "Path");
                GlobalPreferences.HT_MusicMD5 = WRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "MD5");
                await Task.Delay(RandomNumber.Next(12, 100));

                //[ExternalFiles]
                Label_Status.Text = "Loading External File Path, please wait...";
                GlobalPreferences.StreamFilePath = WRegistryFunctions.LoadExternalFiles("StreamFile", "Path");
                await Task.Delay(RandomNumber.Next(25, 43));

                //[OutputFolders]
                Label_Status.Text = "Loading output folders paths, please wait...";
                GlobalPreferences.SFXOutputPath = WRegistryFunctions.LoadOutputFolders("SoundsOutputDirectory", "Path");
                GlobalPreferences.StreamFileOutputPath = WRegistryFunctions.LoadOutputFolders("StreamSoundsOutputDirectory", "Path");
                GlobalPreferences.MusicOutputPath = WRegistryFunctions.LoadOutputFolders("MusicOutputDirectory", "Path");
                await Task.Delay(RandomNumber.Next(80, 100));
            }

            //*===============================================================================================
            //* Load User Settings
            //*===============================================================================================
            Label_Status.Text = "Loading user settings, please wait...";
            GlobalPreferences.TV_SelectedFont = WRegistryFunctions.LoadTreeViewPreferences("TV_SelectedFont");
            GlobalPreferences.TV_ShowLines = Convert.ToBoolean(int.Parse(WRegistryFunctions.LoadTreeViewPreferences("TV_ShowLines")));
            GlobalPreferences.TV_ShowRootLines = Convert.ToBoolean(int.Parse(WRegistryFunctions.LoadTreeViewPreferences("TV_ShowRootLines")));
            GlobalPreferences.TV_Indent = int.Parse(WRegistryFunctions.LoadTreeViewPreferences("TV_Indent"));
            GlobalPreferences.TV_ItemHeight = int.Parse(WRegistryFunctions.LoadTreeViewPreferences("TV_ItemHeight"));
            await Task.Delay(RandomNumber.Next(1, 50));

            GlobalPreferences.WavesViewerControl_WavesColor = WRegistryFunctions.LoadWavesControlColors("WavesColors");
            GlobalPreferences.WavesViewerControl_BackgroundColor = WRegistryFunctions.LoadWavesControlColors("BackgroundColor");
            await Task.Delay(RandomNumber.Next(5, 10));

            GlobalPreferences.UseSystemTray = Convert.ToBoolean(WRegistryFunctions.GetSystemConfig("UseSysTray"));
            await Task.Delay(RandomNumber.Next(10, 22));

            //-----------------------------------------[Sound Data]----------------------------------------
            Label_Status.Text = "Loading sounds data hashtable, please wait...";
            Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);
            await Task.Delay(RandomNumber.Next(30, 120));

            //-----------------------------------------[Sound Defines]----------------------------------------
            Label_Status.Text = "Loading sounds hashtable, please wait...";
            Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            await Task.Delay(RandomNumber.Next(80, 100));

            //-----------------------------------------[Music Defines]----------------------------------------
            Label_Status.Text = "Loading musics hashtable, please wait...";
            Hashcodes.LoadMusicHashcodes(GlobalPreferences.HT_MusicPath);
            await Task.Delay(RandomNumber.Next(30, 120));

            //-----------------------------------------[SoX Executable]----------------------------------------
            string SoXExePath;
            Label_Status.Text = "Loading SoX Executable Path, please wait...";
            SoXExePath = WRegistryFunctions.LoadSoxFilePath();

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
            await Task.Delay(RandomNumber.Next(50, 120));

            //-----------------------------------------[Load AudioDevice]---------------------------------------
            Label_Status.Text = "Loading audio devices preferences, please wait...";
            WRegistryFunctions.LoadDefaultAudioDevice();
            await Task.Delay(RandomNumber.Next(35, 150));

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