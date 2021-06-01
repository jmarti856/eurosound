using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.TreeViewLibraryFunctions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundSoundBanksFilesFunctions
{
    internal class ESF_LoadSoundBanks
    {
        internal string ReadEuroSoundSoundBankFile(ProjectFile FileProperties, BinaryReader BReader, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, TreeView TreeViewControl, int FileVersion)
        {
            //File Hashcode
            FileProperties.Hashcode = BReader.ReadUInt32();
            //Latest SoundID value
            FileProperties.ObjectID = BReader.ReadUInt32();
            //TreeView Data
            uint TreeViewDataOffset = BReader.ReadUInt32();
            //SoundsListData Offset
            uint SoundsListDataOffset = BReader.ReadUInt32();
            //AudioData Offset
            uint AudioDataOffset = BReader.ReadUInt32();
            //FullSize
            BReader.BaseStream.Position += 4;
            //File Name
            FileProperties.FileName = BReader.ReadString();
            //Profile Path
            string ProfileSelected = BReader.ReadString();
            //Profile Name
            string ProfileSelectedName = BReader.ReadString();

            GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

            //*===============================================================================================
            //* Sounds List Data
            //*===============================================================================================
            BReader.BaseStream.Position = SoundsListDataOffset;
            ReadSoundsListData(BReader, SoundsList);

            //*===============================================================================================
            //* Audio Data
            //*===============================================================================================
            BReader.BaseStream.Position = AudioDataOffset;
            ReadAudioDataDictionary(BReader, AudiosList, FileVersion);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Position = TreeViewDataOffset;
            ReadTreeViewData(BReader, TreeViewControl, FileVersion);

            //Close Reader
            BReader.Close();

            return ProfileSelectedName;
        }

        internal void ReadAudioDataDictionary(BinaryReader BReader, Dictionary<string, EXAudio> AudiosList, int Version)
        {
            int TotalEntries = BReader.ReadInt32();

            for (int i = 0; i < TotalEntries; i++)
            {
                string HashMD5 = BReader.ReadString();
                EXAudio AudioToAdd = new EXAudio
                {
                    Dependencies = BReader.ReadString(),
                    LoadedFileName = BReader.ReadString(),
                    Encoding = BReader.ReadString(),
                    Flags = BReader.ReadUInt16()
                };
                BReader.ReadUInt32();
                AudioToAdd.Frequency = BReader.ReadUInt32();
                BReader.ReadUInt32();
                AudioToAdd.Channels = BReader.ReadUInt32();
                AudioToAdd.Bits = BReader.ReadUInt32();
                AudioToAdd.PSIsample = BReader.ReadUInt32();
                AudioToAdd.LoopOffset = BReader.ReadUInt32();
                AudioToAdd.Duration = BReader.ReadUInt32();
                int PCMDataLength = BReader.ReadInt32();
                AudioToAdd.PCMdata = BReader.ReadBytes(PCMDataLength);

                //PS2
                if (Version >= 1013)
                {
                    AudioToAdd.FrequencyPS2 = BReader.ReadUInt32();
                    BReader.ReadUInt32();
                    BReader.ReadBoolean();
                }
                else
                {
                    AudioToAdd.FrequencyPS2 = AudioToAdd.Frequency;
                }

                AudiosList.Add(HashMD5, AudioToAdd);
            }
        }

        internal void ReadSoundsListData(BinaryReader BReader, Dictionary<uint, EXSound> SoundsList)
        {
            int NumberOfSounds = BReader.ReadInt32();

            for (int i = 0; i < NumberOfSounds; i++)
            {
                uint SoundID = BReader.ReadUInt32();
                EXSound NewSound = new EXSound
                {
                    Hashcode = BReader.ReadUInt32(),
                    OutputThisSound = BReader.ReadBoolean(),

                    //---Required for EngineX---
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

                int NumberOfSamples = BReader.ReadInt32();
                for (int j = 0; j < NumberOfSamples; j++)
                {
                    uint SampleID = BReader.ReadUInt32();
                    EXSample NewSample = new EXSample
                    {
                        IsStreamed = BReader.ReadBoolean(),
                        FileRef = BReader.ReadInt16(),
                        ComboboxSelectedAudio = BReader.ReadString(),
                        HashcodeSubSFX = BReader.ReadUInt32(),

                        //---Required For EngineX---
                        PitchOffset = BReader.ReadInt16(),
                        RandomPitchOffset = BReader.ReadInt16(),
                        BaseVolume = BReader.ReadSByte(),
                        RandomVolumeOffset = BReader.ReadSByte(),
                        Pan = BReader.ReadSByte(),
                        RandomPan = BReader.ReadSByte()
                    };
                    NewSound.Samples.Add(SampleID, NewSample);
                }
                SoundsList.Add(SoundID, NewSound);
            }
        }

        internal void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl, int Version)
        {
            bool NodeIsExpanded = false, NodeIsSelected = false;
            bool ParentIsExpanded = false;

            int NumberOfNodes = BReader.ReadInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                string ParentNode = BReader.ReadString();
                string NodeName = BReader.ReadString();
                string DisplayName = BReader.ReadString();
                int SelectedImageIndex = BReader.ReadInt32();
                int ImageIndex = BReader.ReadInt32();
                string Tag = BReader.ReadString();
                Color NodeColor = Color.FromArgb(BReader.ReadInt32());
                BReader.ReadBoolean();

                //Check version
                if (Version >= 1008)
                {
                    ParentIsExpanded = BReader.ReadBoolean();
                    NodeIsExpanded = BReader.ReadBoolean();
                    NodeIsSelected = BReader.ReadBoolean();
                }

                //Ignore state
                if (GlobalPreferences.TV_IgnoreStlyesFromESF)
                {
                    ParentIsExpanded = false;
                    NodeIsExpanded = false;
                    NodeIsSelected = false;
                }

                //Whenever possible use system colors
                if (NodeColor.GetBrightness() < 0.1 || GlobalPreferences.TV_IgnoreStlyesFromESF)
                {
                    NodeColor = SystemColors.WindowText;
                }

                TreeNodeFunctions.TreeNodeAddNewNode(ParentNode, NodeName, DisplayName, SelectedImageIndex, ImageIndex, Enumerations.GetTreeNodeType(Tag), ParentIsExpanded, NodeIsExpanded, NodeIsSelected, NodeColor, TreeViewControl);
            }
        }
    }
}
