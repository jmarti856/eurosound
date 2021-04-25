using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationPreferences.EuroSound_Profiles;
using EuroSound_Application.ApplicationPreferences.Ini_File;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.HashCodesFunctions;
using System;
using System.Collections.Generic;
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
            //*===============================================================================================
            //* Variables
            //*===============================================================================================
            Random RandomNumber = new Random();
            IniFile_Functions ProfilesReader = new IniFile_Functions();
            GenericFunctions.ResourcesManager = new ResourceManager(typeof(Properties.Resources));
            ProfilesFunctions ProfilesLoader = new ProfilesFunctions();
            Label_EuroSoundVersion.Text = string.Join(" ", new string[] { AssemblyDescription, "Version", GenericFunctions.GetEuroSoundVersion()[0].ToString() });

            //*===============================================================================================
            //* Load INI FIle
            //*===============================================================================================
            Label_Status.Text = "Loading ESound.ini, please wait...";

            //Get profiles list form ini file
            if (File.Exists(Application.StartupPath + "\\Esound.ini"))
            {
                GenericFunctions.AvailableProfiles = ProfilesReader.GetAvailableProfiles(Application.StartupPath + "\\Esound.ini");
                await Task.Delay(RandomNumber.Next(150, 250));
            }

            //*===============================================================================================
            //* Load Profile
            //*===============================================================================================
            Label_Status.Text = "Checking Profile, please wait...";

            string ProfileNameFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfileName");
            string ProfilePathFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfile");

            //Reload last profile if file exists
            if (File.Exists(ProfilePathFromReg))
            {
                GlobalPreferences.SelectedProfileName = ProfileNameFromReg;
                GlobalPreferences.SelectedProfile = ProfilePathFromReg;
            }
            else
            {
                //Load profile from ini file
                if (!string.IsNullOrEmpty(ProfileNameFromReg))
                {
                    foreach (KeyValuePair<string, string> Profile in GenericFunctions.AvailableProfiles)
                    {
                        if (Profile.Key.Equals(ProfileNameFromReg))
                        {
                            if (File.Exists(Profile.Value))
                            {
                                GlobalPreferences.SelectedProfileName = Profile.Key;
                                GlobalPreferences.SelectedProfile = Profile.Value;
                            }
                            break;
                        }
                    }
                }
            }

            //*===============================================================================================
            //* Load Profile Data
            //*===============================================================================================
            if (File.Exists(GlobalPreferences.SelectedProfile))
            {
                //Reload profile if paths are not equal
                if (!ProfilePathFromReg.Equals(GlobalPreferences.SelectedProfile))
                {
                    Label_Status.Text = "Loading Profile, please wait...";

                    //Load and apply profile to update regedit data
                    ProfilesLoader.ApplyProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName, true);
                    await Task.Delay(RandomNumber.Next(170, 250));
                }

                //[SoundbanksSettings]
                Label_Status.Text = "Loading soundbanks settings, please wait...";
                GlobalPreferences.SoundbankFrequency = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Frequency"));
                GlobalPreferences.SoundbankEncoding = WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Encoding");
                GlobalPreferences.SoundbankBits = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Bits"));
                GlobalPreferences.SoundbankChannels = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Channels"));
                await Task.Delay(RandomNumber.Next(50, 150));

                //[StreamFileSettings]
                Label_Status.Text = "Loading streambanks settings, please wait...";
                GlobalPreferences.StreambankFrequency = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Frequency"));
                GlobalPreferences.StreambankEncoding = WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Encoding");
                GlobalPreferences.StreambankBits = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Bits"));
                GlobalPreferences.StreambankChannels = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Channels"));
                await Task.Delay(RandomNumber.Next(32, 160));

                //[MusicFileSettings]
                Label_Status.Text = "Loading musicbanks settings, please wait...";
                GlobalPreferences.MusicbankFrequency = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Frequency"));
                GlobalPreferences.MusicbankEncoding = WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Encoding");
                GlobalPreferences.MusicbankBits = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Bits"));
                GlobalPreferences.MusicbankChannels = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Channels"));
                await Task.Delay(RandomNumber.Next(12, 100));

                //[HashTableFiles]
                Label_Status.Text = "Loading hastables files, please wait...";
                GlobalPreferences.HT_SoundsPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_Sound", "Path");
                GlobalPreferences.HT_SoundsMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_Sound", "MD5");
                await Task.Delay(RandomNumber.Next(50, 150));

                GlobalPreferences.HT_SoundsDataPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "Path");
                GlobalPreferences.HT_SoundsDataMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "MD5");
                await Task.Delay(RandomNumber.Next(32, 160));

                GlobalPreferences.HT_MusicPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "Path");
                GlobalPreferences.HT_MusicMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "MD5");
                await Task.Delay(RandomNumber.Next(12, 100));

                //[ExternalFiles]
                Label_Status.Text = "Loading External File Path, please wait...";
                GlobalPreferences.StreamFilePath = WindowsRegistryFunctions.LoadExternalFiles("StreamFile", "Path");
                GlobalPreferences.MkFileListPath = WindowsRegistryFunctions.LoadExternalFiles("MkFileList", "Path");
                GlobalPreferences.MkFileList2Path = WindowsRegistryFunctions.LoadExternalFiles("MkFileList2", "Path");
                await Task.Delay(RandomNumber.Next(25, 43));

                //[OutputFolders]
                Label_Status.Text = "Loading output folders paths, please wait...";
                GlobalPreferences.SFXOutputPath = WindowsRegistryFunctions.LoadOutputFolders("SoundsOutputDirectory", "Path");
                GlobalPreferences.StreamFileOutputPath = WindowsRegistryFunctions.LoadOutputFolders("StreamSoundsOutputDirectory", "Path");
                GlobalPreferences.MusicOutputPath = WindowsRegistryFunctions.LoadOutputFolders("MusicOutputDirectory", "Path");
                await Task.Delay(RandomNumber.Next(80, 100));
            }

            //*===============================================================================================
            //* Load User Settings
            //*===============================================================================================
            Label_Status.Text = "Loading user settings, please wait...";
            GlobalPreferences.TV_SelectedFont = WindowsRegistryFunctions.LoadTreeViewPreferences("TV_SelectedFont");
            GlobalPreferences.TV_ShowLines = Convert.ToBoolean(int.Parse(WindowsRegistryFunctions.LoadTreeViewPreferences("TV_ShowLines")));
            GlobalPreferences.TV_ShowRootLines = Convert.ToBoolean(int.Parse(WindowsRegistryFunctions.LoadTreeViewPreferences("TV_ShowRootLines")));
            GlobalPreferences.TV_Indent = int.Parse(WindowsRegistryFunctions.LoadTreeViewPreferences("TV_Indent"));
            GlobalPreferences.TV_ItemHeight = int.Parse(WindowsRegistryFunctions.LoadTreeViewPreferences("TV_ItemHeight"));
            await Task.Delay(RandomNumber.Next(1, 50));

            GlobalPreferences.WavesViewerControl_WavesColor = WindowsRegistryFunctions.LoadWavesControlColors("WavesColors");
            GlobalPreferences.WavesViewerControl_BackgroundColor = WindowsRegistryFunctions.LoadWavesControlColors("BackgroundColor");
            GlobalPreferences.LoadLastLoadedESF = WindowsRegistryFunctions.LoadAutomaticalyLoadLastESF("AutomaticalyLoadLastESF");
            GlobalPreferences.TV_IgnoreStlyesFromESF = WindowsRegistryFunctions.LoadAutomaticalyLoadLastESF("TV_IgnoreStlyesFromESF"); ;
            await Task.Delay(RandomNumber.Next(5, 10));

            GlobalPreferences.UseSystemTray = Convert.ToBoolean(WindowsRegistryFunctions.GetSystemConfig("UseSysTray"));
            await Task.Delay(RandomNumber.Next(10, 22));

            //-----------------------------------------[Sound Data]----------------------------------------
            Label_Status.Text = "Loading sounds data hashtable, please wait...";
            if (File.Exists(GlobalPreferences.HT_SoundsDataPath))
            {
                Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Hashcodes_SFXData_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await Task.Delay(RandomNumber.Next(30, 120));

            //-----------------------------------------[Sound Defines]----------------------------------------
            Label_Status.Text = "Loading sounds hashtable, please wait...";
            if (File.Exists(GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Hashcodes_SFXDefines_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await Task.Delay(RandomNumber.Next(80, 100));

            //-----------------------------------------[Music Defines]----------------------------------------
            Label_Status.Text = "Loading musics hashtable, please wait...";
            if (File.Exists(GlobalPreferences.HT_MusicPath))
            {
                Hashcodes.LoadMusicHashcodes(GlobalPreferences.HT_MusicPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Hashcodes_SFXData_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            await Task.Delay(RandomNumber.Next(30, 120));

            //-----------------------------------------[SoX Executable]----------------------------------------
            string SoXExePath;
            Label_Status.Text = "Loading SoX Executable Path, please wait...";
            SoXExePath = WindowsRegistryFunctions.LoadSoxFilePath();

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
            WindowsRegistryFunctions.LoadDefaultAudioDevice();
            await Task.Delay(RandomNumber.Next(35, 150));

            //-----------------------------------------[Open Form]----------------------------------------
            Label_Status.Text = "Starting application, please wait...";
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