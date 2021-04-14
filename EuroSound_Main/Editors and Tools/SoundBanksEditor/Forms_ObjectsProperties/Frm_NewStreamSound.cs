using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.EuroSoundFilesFunctions;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_NewStreamSound : Form
    {
        public int SelectedSound { get; set; } = 0;
        private EXSample SelectedSample;

        public Frm_NewStreamSound(EXSample Sample)
        {
            InitializeComponent();
            SelectedSample = Sample;
        }

        private void Frm_NewStreamSound_Load(object sender, EventArgs e)
        {
            int StreamIndex;

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
                StreamIndex = (Math.Abs(SelectedSample.FileRef) - 1);
                if (StreamIndex <= ListBox_StreamSounds.Items.Count && ListBox_StreamSounds.Items.Count > 0)
                {
                    ListBox_StreamSounds.SelectedIndex = StreamIndex;
                }
            }

            //Ensure that this boolean is correctly stated
            if (!SelectedSample.IsStreamed)
            {
                SelectedSample.IsStreamed = true;
            }
        }

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

        private void ReadSoundsList()
        {
            sbyte TypeOfStoredData;
            uint ListOffset, NumberOfItems;

            if (File.Exists(GlobalPreferences.StreamFilePath))
            {
                EuroSoundFiles ESFReader = new EuroSoundFiles();
                using (BinaryReader BReader = new BinaryReader(File.Open(GlobalPreferences.StreamFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    if (ESFReader.FileIsCorrect(BReader))
                    {
                        //Type of stored data
                        TypeOfStoredData = BReader.ReadSByte();
                        if (TypeOfStoredData == 1)
                        {
                            //File Hashcode
                            BReader.ReadUInt32();
                            //Sound ID
                            BReader.ReadUInt32();
                            //Sounds List Offset
                            ListOffset = BReader.ReadUInt32();

                            //Go to list offset
                            BReader.BaseStream.Seek(ListOffset, SeekOrigin.Begin);
                            NumberOfItems = BReader.ReadUInt32();

                            //Clear List
                            ListBox_StreamSounds.Items.Clear();

                            //Add Items To List
                            for (int i = 0; i < NumberOfItems; i++)
                            {
                                ListBox_StreamSounds.Items.Add(BReader.ReadString());
                            }
                        }
                    }

                    BReader.Close();
                }
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("StreamFileNotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
