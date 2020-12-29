using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public class ESF_LoadStreamSounds
    {
        internal void ReadEuroSoundFile11(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList)
        {
            uint TreeViewDataOffset, StreamSoundsDictionaryOffset;

            /*File Hashcode*/
            FileProperties.Hashcode = BReader.ReadUInt32();
            /*Sound ID*/
            FileProperties.StreamedSoundID = BReader.ReadUInt32();
            /*Sounds List Offset*/
            BReader.ReadUInt32();//Only Used in the SoundBanks Form
            /*TreeViewData Offset*/
            TreeViewDataOffset = BReader.ReadUInt32();
            /*Dictionary Data Offset*/
            StreamSoundsDictionaryOffset = BReader.ReadUInt32();
            /*FileSize*/
            BReader.ReadUInt32();
            /*File Name*/
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
            ReadDictionaryData(BReader, StreamSoundsList);

            //Close Reader
            BReader.Close();
        }

        internal void ReadDictionaryData(BinaryReader BReader, Dictionary<uint, EXSoundStream> DictionaryData)
        {
            int DictionaryItems, ADPCM_Lenght, MarkersCount, StateLength, MarkersDataLength;
            uint SoundStreamKey;


            DictionaryItems = BReader.ReadInt32();
            for (int i = 0; i < DictionaryItems; i++)
            {
                SoundStreamKey = BReader.ReadUInt32();
                EXSoundStream StreamSound = new EXSoundStream
                {
                    DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                    Hashcode = BReader.ReadUInt32()
                };
                ADPCM_Lenght = BReader.ReadInt32();
                StreamSound.IMA_ADPCM_DATA = BReader.ReadBytes(ADPCM_Lenght);

                /*Markers Length*/
                MarkersCount = BReader.ReadInt32();
                for (int j = 0; j < MarkersCount; j++)
                {
                    EXStreamSoundMarker SoundMarker = new EXStreamSoundMarker
                    {
                        Position = BReader.ReadUInt32(),
                        IsInstant = BReader.ReadUInt32(),
                        InstantBuffer = BReader.ReadUInt32()
                    };
                    StateLength = BReader.ReadInt32();
                    SoundMarker.State = BReader.ReadBytes(StateLength);

                    MarkersDataLength = BReader.ReadInt32();
                    for (int k = 0; k < MarkersDataLength; k++)
                    {
                        EXStreamSoundMarkerData SoundMarkerData = new EXStreamSoundMarkerData
                        {
                            Name = BReader.ReadUInt32(),
                            Position = BReader.ReadUInt32(),
                            MusicMakerType = BReader.ReadUInt32(),
                            Flags = BReader.ReadUInt32(),
                            Extra = BReader.ReadUInt32(),
                            LoopStart = BReader.ReadUInt32(),
                            MarkerCount = BReader.ReadUInt32(),
                            LoopMarkerCount = BReader.ReadUInt32()
                        };

                        SoundMarker.MarkersData.Add(SoundMarkerData);
                    }
                    StreamSound.Markers.Add(SoundMarker);
                }

                StreamSound.IMA_Data_MD5 = BReader.ReadString();
                StreamSound.IMA_Data_Name = BReader.ReadString();
                StreamSound.OutputThisSound = BReader.ReadBoolean();

                StreamSound.IDMarkerName = BReader.ReadUInt32();
                StreamSound.IDMarkerPos = BReader.ReadUInt32();

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
