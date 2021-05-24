using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.Clases;
using EuroSound_Application.EuroSoundFilesFunctions;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_NewStreamSound : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        public int SelectedSound { get; set; } = 0;
        private EXSample SelectedSample;

        public Frm_NewStreamSound(EXSample Sample)
        {
            InitializeComponent();
            SelectedSample = Sample;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_NewStreamSound_Load(object sender, EventArgs e)
        {
            Textbox_ExternalFile.Text = GlobalPreferences.StreamFilePath;
            ReadSoundsList();

            numeric_pitchoffset.Value = SelectedSample.PitchOffset;
            numeric_randomPitchOffset.Value = SelectedSample.RandomPitchOffset;
            Numeric_BaseVolume.Value = decimal.Divide(SelectedSample.BaseVolume, 100);
            numeric_randomvolumeoffset.Value = decimal.Divide(SelectedSample.RandomVolumeOffset, 100);
            numeric_pan.Value = decimal.Divide(SelectedSample.Pan, 100);
            numeric_randompan.Value = decimal.Divide(SelectedSample.RandomPan, 100);

            if (SelectedSample.FileRef != 0)
            {
                int streamIndex = (Math.Abs(SelectedSample.FileRef) - 1);
                if (streamIndex <= ListBox_StreamSounds.Items.Count && ListBox_StreamSounds.Items.Count > 0)
                {
                    ListBox_StreamSounds.SelectedIndex = streamIndex;
                }
            }

            //Ensure that this boolean is correctly stated
            if (!SelectedSample.IsStreamed)
            {
                SelectedSample.IsStreamed = true;
            }
        }

        //*===============================================================================================
        //* Control Events
        //*===============================================================================================
        private void Button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SelectedSample.PitchOffset = (short)numeric_pitchoffset.Value;
            SelectedSample.RandomPitchOffset = (short)numeric_randomPitchOffset.Value;
            SelectedSample.BaseVolume = (sbyte)(Numeric_BaseVolume.Value * 100);
            SelectedSample.RandomVolumeOffset = (sbyte)(numeric_randomvolumeoffset.Value * 100);
            SelectedSample.Pan = (sbyte)(numeric_pan.Value * 100);
            SelectedSample.RandomPan = (sbyte)(numeric_randompan.Value * 100);

            SelectedSound = (ListBox_StreamSounds.SelectedIndex + 1) * -1;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_RefreshList_Click(object sender, EventArgs e)
        {
            ReadSoundsList();
        }

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        private void ReadSoundsList()
        {
            if (File.Exists(GlobalPreferences.StreamFilePath))
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(GlobalPreferences.StreamFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    EuroSoundFiles esfFunctions = new EuroSoundFiles();
                    if (esfFunctions.FileIsCorrect(binaryReader))
                    {
                        //Type of stored data
                        sbyte typeOfStoredData = binaryReader.ReadSByte();
                        if (typeOfStoredData == (int)Enumerations.ESoundFileType.StreamSounds)
                        {
                            uint ListOffset;
                            //Older versions, before 1.0.1.3
                            if (esfFunctions.FileVersion < 1013)
                            {
                                binaryReader.BaseStream.Seek(16, SeekOrigin.Begin);
                                ListOffset = binaryReader.ReadUInt32();
                            }
                            else
                            {
                                binaryReader.BaseStream.Seek(32, SeekOrigin.Begin);
                                ListOffset = binaryReader.ReadUInt32();
                            }

                            //Go to list offset
                            binaryReader.BaseStream.Seek(ListOffset, SeekOrigin.Begin);
                            uint numberOfItems = binaryReader.ReadUInt32();

                            //Clear List
                            ListBox_StreamSounds.Items.Clear();

                            //Add Items To List
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                ListBox_StreamSounds.Items.Add(binaryReader.ReadString());
                            }
                        }
                    }
                    binaryReader.Close();
                }
            }
            else
            {
                DialogResult specifyOtherPathQuestion = MessageBox.Show("The stream sounds file has not found, the file route is: \"" + GlobalPreferences.StreamFilePath + "\", do you want to specify another path ?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (specifyOtherPathQuestion == DialogResult.Yes)
                {
                    GlobalPreferences.StreamFilePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Files (*.esf)|*.esf", 0, true);
                    WindowsRegistryFunctions.SaveExternalFiles("StreamFile", "Path", GlobalPreferences.StreamFilePath);
                }
            }
        }
    }
}
