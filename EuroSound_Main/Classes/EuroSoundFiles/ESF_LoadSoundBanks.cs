using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal class ESF_LoadSoundBanks
    {
        internal void ReadEuroSoundFile11(ProjectFile FileProperties, BinaryReader BReader, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl)
        {
            uint TreeViewDataOffset, SoundsListDataOffset, AudioDataOffset;
            /*File Hashcode*/
            FileProperties.Hashcode = BReader.ReadUInt32();
            /*Latest SoundID value*/
            FileProperties.SoundID = BReader.ReadUInt32();
            /*TreeView Data*/
            TreeViewDataOffset = BReader.ReadUInt32();
            /*SoundsListData Offset -- Not used for now*/
            SoundsListDataOffset = BReader.ReadUInt32();
            /*AudioData Offset -- Not used for now*/
            AudioDataOffset = BReader.ReadUInt32();
            /*FullSize*/
            BReader.ReadUInt32();
            /*File Name*/
            FileProperties.FileName = BReader.ReadString();

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Position = TreeViewDataOffset;
            ReadTreeViewData(BReader, TreeViewControl);

            //*===============================================================================================
            //* Sounds List Data
            //*===============================================================================================
            BReader.BaseStream.Position = SoundsListDataOffset;
            ReadSoundsListData(BReader, SoundsList);

            //*===============================================================================================
            //* Audio Data
            //*===============================================================================================
            BReader.BaseStream.Position = AudioDataOffset;
            ReadAudioDataDictionary(BReader, AudiosList);

            //Close Reader
            BReader.Close();
        }

        internal void ReadAudioDataDictionary(BinaryReader BReader, Dictionary<string, EXAudio> AudiosList)
        {
            int TotalEntries, PCMDataLength;
            string HashMD5;

            TotalEntries = BReader.ReadInt32();

            for (int i = 0; i < TotalEntries; i++)
            {
                HashMD5 = BReader.ReadString();
                EXAudio AudioToAdd = new EXAudio
                {
                    DisplayName = BReader.ReadString(),
                    Dependencies = BReader.ReadString(),
                    Name = BReader.ReadString(),
                    Encoding = BReader.ReadString(),
                    Flags = BReader.ReadUInt16(),
                    DataSize = BReader.ReadUInt32(),
                    Frequency = BReader.ReadUInt32(),
                    RealSize = BReader.ReadUInt32(),
                    Channels = BReader.ReadUInt32(),
                    Bits = BReader.ReadUInt32(),
                    PSIsample = BReader.ReadUInt32(),
                    LoopOffset = BReader.ReadUInt32(),
                    Duration = BReader.ReadUInt32()
                };
                PCMDataLength = BReader.ReadInt32();
                AudioToAdd.PCMdata = BReader.ReadBytes(PCMDataLength);

                AudiosList.Add(HashMD5, AudioToAdd);
            }
        }

        internal void ReadSoundsListData(BinaryReader BReader, Dictionary<uint, EXSound> SoundsList)
        {
            int NumberOfSounds, NumberOfSamples;
            uint SoundID;

            NumberOfSounds = BReader.ReadInt32();

            for (int i = 0; i < NumberOfSounds; i++)
            {
                SoundID = BReader.ReadUInt32();
                EXSound NewSound = new EXSound
                {
                    Hashcode = BReader.ReadUInt32(),
                    DisplayName = BReader.ReadString(),
                    OutputThisSound = BReader.ReadBoolean(),

                    /*---Required for EngineX---*/
                    DuckerLenght = BReader.ReadInt16(),
                    MinDelay = BReader.ReadInt16(),
                    MaxDelay = BReader.ReadInt16(),
                    InnerRadiusReal = BReader.ReadInt16(),
                    OuterRadiusReal = BReader.ReadInt16(),
                    ReverbSend = BReader.ReadSByte(),
                    TrackingType = BReader.ReadSByte(),
                    MaxVoices = BReader.ReadSByte(),
                    Priority = BReader.ReadSByte(),
                    Ducker = BReader.ReadSByte(),
                    MasterVolume = BReader.ReadSByte(),
                    Flags = BReader.ReadUInt16()
                };

                NumberOfSamples = BReader.ReadInt32();
                for (int j = 0; j < NumberOfSamples; j++)
                {
                    EXSample NewSample = new EXSample
                    {
                        Name = BReader.ReadString(),
                        DisplayName = BReader.ReadString(),
                        IsStreamed = BReader.ReadBoolean(),
                        FileRef = BReader.ReadInt16(),
                        ComboboxSelectedAudio = BReader.ReadString(),
                        HashcodeSubSFX = BReader.ReadUInt32(),

                        /*---Required For EngineX---*/
                        PitchOffset = BReader.ReadInt16(),
                        RandomPitchOffset = BReader.ReadInt16(),
                        BaseVolume = BReader.ReadSByte(),
                        RandomVolumeOffset = BReader.ReadSByte(),
                        Pan = BReader.ReadSByte(),
                        RandomPan = BReader.ReadSByte()
                    };
                    NewSound.Samples.Add(NewSample);
                }

                SoundsList.Add(SoundID, NewSound);
            }
        }

        internal void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl)
        {
            int NumberOfNodes, SelectedImageIndex, ImageIndex;
            string ParentNode, NodeName, DisplayName, Tag;
            Color NodeColor;

            NumberOfNodes = BReader.ReadInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                ParentNode = BReader.ReadString();
                NodeName = BReader.ReadString();
                DisplayName = BReader.ReadString();
                //Index Unused for now
                BReader.ReadInt32();
                //ImageKey Unused for now
                BReader.ReadString();
                SelectedImageIndex = BReader.ReadInt32();
                ImageIndex = BReader.ReadInt32();
                Tag = BReader.ReadString();
                NodeColor = Color.FromArgb(BReader.ReadInt32());
                BReader.ReadBoolean();

                TreeNodeFunctions.TreeNodeAddNewNode(ParentNode, NodeName, DisplayName, SelectedImageIndex, ImageIndex, Tag, NodeColor, TreeViewControl);
            }
        }
    }
}
