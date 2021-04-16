using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundMusicFilesFunctions
{
    public class ESF_LoadStreamSounds
    {
        internal string ReadEuroSoundStreamFile(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList)
        {
            uint TreeViewDataOffset, StreamSoundsDictionaryOffset;
            string ProfileSelected, ProfileSelectedName;

            //File Hashcode
            FileProperties.Hashcode = BReader.ReadUInt32();
            //Sound ID
            FileProperties.SoundID = BReader.ReadUInt32();
            //Sounds List Offset
            BReader.BaseStream.Position += 4;//Only Used in the "Frm_NewStreamSound" Form
            //TreeViewData Offset
            TreeViewDataOffset = BReader.ReadUInt32();
            //Dictionary Data Offset
            StreamSoundsDictionaryOffset = BReader.ReadUInt32();
            //FileSize
            BReader.BaseStream.Position += 4;
            //File Name
            FileProperties.FileName = BReader.ReadString();
            //Profile Path
            ProfileSelected = BReader.ReadString();
            //Profile Name
            ProfileSelectedName = BReader.ReadString();

            GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Position = TreeViewDataOffset;
            ReadTreeViewData(BReader, TreeViewControl);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            BReader.BaseStream.Position = StreamSoundsDictionaryOffset;
            ReadDictionaryData(BReader, StreamSoundsList);

            //Close Reader
            BReader.Close();

            return ProfileSelectedName;
        }

        internal void ReadDictionaryData(BinaryReader BReader, Dictionary<uint, EXSoundStream> DictionaryData)
        {
            int DictionaryItems, PCM_DataLength, ADPCM_DataLength;
            uint SoundStreamKey, StartMarkersCount, MarkersCount;

            DictionaryItems = BReader.ReadInt32();
            for (int i = 0; i < DictionaryItems; i++)
            {
                SoundStreamKey = BReader.ReadUInt32();
                EXSoundStream StreamSound = new EXSoundStream
                {
                    //DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                };

                //Read Wav
                PCM_DataLength = BReader.ReadInt32();
                StreamSound.PCM_Data = BReader.ReadBytes(PCM_DataLength);
                ADPCM_DataLength = BReader.ReadInt32();
                StreamSound.IMA_ADPCM_DATA = BReader.ReadBytes(ADPCM_DataLength);
                StreamSound.Frequency = BReader.ReadUInt32();
                StreamSound.Channels = BReader.ReadByte();
                StreamSound.Bits = BReader.ReadUInt32();
                StreamSound.Duration = BReader.ReadUInt32();
                StreamSound.Encoding = BReader.ReadString();
                StreamSound.WAVFileMD5 = BReader.ReadString();
                StreamSound.WAVFileName = BReader.ReadString();
                StreamSound.RealSize = BReader.ReadUInt32();

                //Read Start Markers List
                StartMarkersCount = BReader.ReadUInt32();
                for (int j = 0; j < StartMarkersCount; j++)
                {
                    EXStreamStartMarker StartMarker = new EXStreamStartMarker
                    {
                        Name = BReader.ReadUInt32(),
                        Position = BReader.ReadUInt32(),
                        MusicMakerType = BReader.ReadUInt32(),
                        Flags = BReader.ReadUInt32(),
                        Extra = BReader.ReadUInt32(),
                        LoopStart = BReader.ReadUInt32(),
                        MarkerCount = BReader.ReadUInt32(),
                        LoopMarkerCount = BReader.ReadUInt32(),
                        MarkerPos = BReader.ReadUInt32(),
                        IsInstant = BReader.ReadUInt32(),
                        InstantBuffer = BReader.ReadUInt32(),
                        StateA = BReader.ReadUInt32(),
                        StateB = BReader.ReadUInt32()
                    };
                    StreamSound.StartMarkers.Add(StartMarker);
                }

                //Read Markers
                MarkersCount = BReader.ReadUInt32();
                for (int k = 0; k < MarkersCount; k++)
                {
                    EXStreamMarker Marker = new EXStreamMarker
                    {
                        Name = BReader.ReadInt32(),
                        Position = BReader.ReadUInt32(),
                        MusicMakerType = BReader.ReadUInt32(),
                        Flags = BReader.ReadUInt32(),
                        Extra = BReader.ReadUInt32(),
                        LoopStart = BReader.ReadUInt32(),
                        MarkerCount = BReader.ReadUInt32(),
                        LoopMarkerCount = BReader.ReadUInt32()
                    };
                    StreamSound.Markers.Add(Marker);
                }
                StreamSound.OutputThisSound = BReader.ReadBoolean();
                DictionaryData.Add(SoundStreamKey, StreamSound);
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
                BReader.BaseStream.Position += 4;
                //ImageKey Unused for now
                BReader.ReadString();
                SelectedImageIndex = BReader.ReadInt32();
                ImageIndex = BReader.ReadInt32();
                Tag = BReader.ReadString();
                NodeColor = Color.FromArgb(BReader.ReadInt32());
                BReader.ReadBoolean();

                if (GlobalPreferences.TV_IgnoreStlyesFromESF)
                {
                    NodeColor = Color.Black;
                }

                TreeNodeFunctions.TreeNodeAddNewNode(ParentNode, NodeName, DisplayName, SelectedImageIndex, ImageIndex, Tag, NodeColor, TreeViewControl);
            }
        }
    }
}
