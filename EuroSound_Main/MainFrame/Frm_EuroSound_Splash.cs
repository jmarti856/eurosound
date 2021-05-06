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
            int Minimum, Maximum;
            Random RandomNumber = new Random();
            IniFile_Functions ProfilesReader = new IniFile_Functions();
            GenericFunctions.ResourcesManager = new ResourceManager(typeof(Properties.Resources));
            ProfilesFunctions ProfilesLoader = new ProfilesFunctions();

            //*===============================================================================================
            //* Get Version and wait times
            //*===============================================================================================
            Label_EuroSoundVersion.Text = string.Join(" ", new string[] { AssemblyDescription, "Version", GenericFunctions.GetEuroSoundVersion()[0].ToString() });

            Minimum = RandomNumber.Next(2, 10);
            Maximum = RandomNumber.Next(20, 60);

            //*===============================================================================================
            //* Load INI FIle
            //*===============================================================================================
            Label_Status.Text = "Loading ESound.ini, please wait...";

            //Get profiles list form ini file
            if (File.Exists(Application.StartupPath + "\\Esound.ini"))
            {
                GenericFunctions.AvailableProfiles = ProfilesReader.GetAvailableProfiles(Application.StartupPath + "\\Esound.ini");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));
            }

            //*===============================================================================================
            //* Load Profile
            //*===============================================================================================
            Label_Status.Text = "Checking Profile, please wait...";

            string ProfileNameFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfileName");
            string ProfilePathFromReg = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfile");
            string ProfileMD5 = WindowsRegistryFunctions.LoadCurrentProfie("CurrentProfileMD5");

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
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

            //*===============================================================================================
            //* Load Profile Data
            //*===============================================================================================
            if (File.Exists(GlobalPreferences.SelectedProfile))
            {
                //Reload profile if paths are not equal or the file has changed
                if (!ProfilePathFromReg.Equals(GlobalPreferences.SelectedProfile) || !GenericFunctions.CalculateMD5(GlobalPreferences.SelectedProfile).Equals(ProfileMD5))
                {
                    Label_Status.Text = "Loading Profile, please wait...";

                    //Load and apply profile to update regedit data
                    ProfilesLoader.ApplyProfile(GlobalPreferences.SelectedProfile, GlobalPreferences.SelectedProfileName, true);
                    await Task.Delay(RandomNumber.Next(Minimum, Maximum));
                }

                //[SoundbanksSettings]
                Label_Status.Text = "Loading soundbanks settings, please wait...";
                GlobalPreferences.SoundbankFrequency = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Frequency"));
                GlobalPreferences.SoundbankEncoding = WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Encoding");
                GlobalPreferences.SoundbankBits = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Bits"));
                GlobalPreferences.SoundbankChannels = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("SoundBanks", "Channels"));
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[StreamFileSettings]
                Label_Status.Text = "Loading streambanks settings, please wait...";
                GlobalPreferences.StreambankFrequency = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Frequency"));
                GlobalPreferences.StreambankEncoding = WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Encoding");
                GlobalPreferences.StreambankBits = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Bits"));
                GlobalPreferences.StreambankChannels = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("StreamSoundbanks", "Channels"));
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[MusicFileSettings]
                Label_Status.Text = "Loading musicbanks settings, please wait...";
                GlobalPreferences.MusicbankFrequency = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Frequency"));
                GlobalPreferences.MusicbankEncoding = WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Encoding");
                GlobalPreferences.MusicbankBits = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Bits"));
                GlobalPreferences.MusicbankChannels = int.Parse(WindowsRegistryFunctions.LoadSoundSettings("MusicBanks", "Channels"));
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[HashTableFiles]
                Label_Status.Text = "Loading hastables files, please wait...";
                GlobalPreferences.HT_SoundsPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_Sound", "Path");
                GlobalPreferences.HT_SoundsMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_Sound", "MD5");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                GlobalPreferences.HT_SoundsDataPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "Path");
                GlobalPreferences.HT_SoundsDataMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_SoundData", "MD5");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                GlobalPreferences.HT_MusicPath = WindowsRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "Path");
                GlobalPreferences.HT_MusicMD5 = WindowsRegistryFunctions.LoadHashtablesFiles("HT_MusicEvent", "MD5");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[HashCodesPrefixes]
                Label_Status.Text = "Loading HashCodes prefixes, please wait...";
                GlobalPreferences.StreamFileHashCode = WindowsRegistryFunctions.LoadHashCodesPrefixes("StreamFileHashCode");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                GlobalPreferences.SfxPrefix = WindowsRegistryFunctions.LoadHashCodesPrefixes("SfxPrefix");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                GlobalPreferences.SongPrefix = WindowsRegistryFunctions.LoadHashCodesPrefixes("SongPrefix");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[ExternalFiles]
                Label_Status.Text = "Loading External File Path, please wait...";
                GlobalPreferences.StreamFilePath = WindowsRegistryFunctions.LoadExternalFiles("StreamFile", "Path");
                GlobalPreferences.MkFileListPath = WindowsRegistryFunctions.LoadExternalFiles("MkFileList", "Path");
                GlobalPreferences.MkFileList2Path = WindowsRegistryFunctions.LoadExternalFiles("MkFileList2", "Path");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[OutputFolders]
                Label_Status.Text = "Loading output folders paths, please wait...";
                GlobalPreferences.SFXOutputPath = WindowsRegistryFunctions.LoadOutputFolders("SoundsOutputDirectory", "Path");
                GlobalPreferences.StreamFileOutputPath = WindowsRegistryFunctions.LoadOutputFolders("StreamSoundsOutputDirectory", "Path");
                GlobalPreferences.MusicOutputPath = WindowsRegistryFunctions.LoadOutputFolders("MusicOutputDirectory", "Path");
                GlobalPreferences.DebugFilesFolder = WindowsRegistryFunctions.LoadOutputFolders("DebugFilesFolder", "Path");
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));

                //[OutputSettings]
                Label_Status.Text = "Loading output settings, please wait...";
                GlobalPreferences.OutputSoundPath = WindowsRegistryFunctions.LoadSaveOutputSettings("PlayOutputSoundFilePath", string.Empty);
                GlobalPreferences.PlaySoundWhenOutput = Convert.ToBoolean(int.Parse(WindowsRegistryFunctions.LoadSaveOutputSettings("PlaySoundWhenOutput", "0")));
                await Task.Delay(RandomNumber.Next(Minimum, Maximum));
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
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

            GlobalPreferences.WavesViewerControl_WavesColor = WindowsRegistryFunctions.LoadWavesControlColors("WavesColors");
            GlobalPreferences.WavesViewerControl_BackgroundColor = WindowsRegistryFunctions.LoadWavesControlColors("BackgroundColor");
            GlobalPreferences.LoadLastLoadedESF = WindowsRegistryFunctions.LoadAutomaticalyLoadLastESF("AutomaticalyLoadLastESF");
            GlobalPreferences.TV_IgnoreStlyesFromESF = WindowsRegistryFunctions.LoadAutomaticalyLoadLastESF("TV_IgnoreStlyesFromESF");
            GlobalPreferences.UseThreadingWhenLoad = WindowsRegistryFunctions.LoadAutomaticalyLoadLastESF("UseThreadingWhenLoad");
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

            GlobalPreferences.UseSystemTray = Convert.ToBoolean(WindowsRegistryFunctions.GetSystemConfig("UseSysTray"));
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

            GlobalPreferences.ShowUpdatesAlerts = WindowsRegistryFunctions.LoadUpdatesAlerts();

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
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

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
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

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
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

            //-----------------------------------------[Load AudioDevice]---------------------------------------
            Label_Status.Text = "Loading audio devices preferences, please wait...";
            WindowsRegistryFunctions.LoadDefaultAudioDevice();
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

            //-----------------------------------------[Backup Settings]----------------------------------------
            Label_Status.Text = "Loading backup settings, please wait...";
            GlobalPreferences.MakeBackups = Convert.ToBoolean(int.Parse(WindowsRegistryFunctions.LoadBackupSettings("MakeBackups")));
            GlobalPreferences.MakeBackupsDirectory = WindowsRegistryFunctions.LoadBackupSettings("BackupsFolder");
            GlobalPreferences.MakeBackupsMaxNumber = int.Parse(WindowsRegistryFunctions.LoadBackupSettings("MaxBackups"));
            GlobalPreferences.MakeBackupsInterval = int.Parse(WindowsRegistryFunctions.LoadBackupSettings("BackupsInterval"));
            GlobalPreferences.MakeBackupsIndex = int.Parse(WindowsRegistryFunctions.LoadBackupSettings("NextBackup"));
            await Task.Delay(RandomNumber.Next(Minimum, Maximum));

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