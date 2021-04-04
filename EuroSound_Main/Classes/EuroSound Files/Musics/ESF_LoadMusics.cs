using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundMusicFilesFunctions
{
    public class ESF_LoadMusics
    {
        internal void ReadEuroSoundFile11(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXMusic> MusicsList)
        {
            uint TreeViewDataOffset, StreamSoundsDictionaryOffset;

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

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Position = TreeViewDataOffset;
            ReadTreeViewData(BReader, TreeViewControl);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            BReader.BaseStream.Position = StreamSoundsDictionaryOffset;
            ReadDictionaryData(BReader, MusicsList);

            //Close Reader
            BReader.Close();
        }

        internal void ReadDictionaryData(BinaryReader BReader, Dictionary<uint, EXMusic> DictionaryData)
        {
            int DictionaryItems, PCM_DataLength, ADPCM_DataLength;
            uint SoundStreamKey, StartMarkersCount, MarkersCount;

            DictionaryItems = BReader.ReadInt32();
            for (int i = 0; i < DictionaryItems; i++)
            {
                SoundStreamKey = BReader.ReadUInt32();
                EXMusic Music = new EXMusic
                {
                    //DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                };

                //Read Data
                Music.Frequency = BReader.ReadUInt32();
                Music.Channels = BReader.ReadByte();
                Music.Bits = BReader.ReadUInt32();
                Music.Duration = BReader.ReadUInt32();
                Music.RealSize = BReader.ReadUInt32();
                Music.Encoding = BReader.ReadString();
                Music.WAVFileMD5 = BReader.ReadString();
                Music.WAVFileName = BReader.ReadString();

                //Read Wav Left Channel
                PCM_DataLength = BReader.ReadInt32();
                Music.PCM_Data_LeftChannel = BReader.ReadBytes(PCM_DataLength);
                ADPCM_DataLength = BReader.ReadInt32();
                Music.IMA_ADPCM_DATA_LeftChannel = BReader.ReadBytes(ADPCM_DataLength);

                //Read Wav Right Channel
                PCM_DataLength = BReader.ReadInt32();
                Music.PCM_Data_RightChannel = BReader.ReadBytes(PCM_DataLength);
                ADPCM_DataLength = BReader.ReadInt32();
                Music.IMA_ADPCM_DATA_RightChannel = BReader.ReadBytes(ADPCM_DataLength);

                //Read Stereo Track
                PCM_DataLength = BReader.ReadInt32();
                Music.PCM_Data = BReader.ReadBytes(PCM_DataLength);

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
                    Music.StartMarkers.Add(StartMarker);
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
                    Music.Markers.Add(Marker);
                }
                Music.OutputThisSound = BReader.ReadBoolean();
                DictionaryData.Add(SoundStreamKey, Music);
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

                TreeNodeFunctions.TreeNodeAddNewNode(ParentNode, NodeName, DisplayName, SelectedImageIndex, ImageIndex, Tag, NodeColor, TreeViewControl);
            }
        }
    }
}
