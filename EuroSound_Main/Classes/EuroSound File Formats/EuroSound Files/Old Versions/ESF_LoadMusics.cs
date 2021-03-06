﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundMusicFilesFunctions
{
    public class ESF_LoadMusics
    {
        internal string ReadEuroSoundMusicFile(ProjectFile FileProperties, BinaryStream BReader, TreeView TreeViewControl, Dictionary<uint, EXMusic> MusicsList, int FileVersion)
        {
            //File Hashcode
            FileProperties.Hashcode = BReader.ReadUInt32();
            //Sound ID
            FileProperties.ObjectID = BReader.ReadUInt32();
            //Sounds List Offset
            BReader.BaseStream.Position += 4;
            //TreeViewData Offset
            uint TreeViewDataOffset = BReader.ReadUInt32();
            //Dictionary Data Offset
            uint StreamSoundsDictionaryOffset = BReader.ReadUInt32();
            //FileSize
            BReader.BaseStream.Position += 4;
            //File Name
            FileProperties.FileName = BReader.ReadString();
            //Profile Path
            string ProfileSelected = BReader.ReadString();
            //Profile Name
            string ProfileSelectedName = BReader.ReadString();

            GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            BReader.BaseStream.Position = StreamSoundsDictionaryOffset;
            ReadDictionaryData(BReader, MusicsList);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Position = TreeViewDataOffset;
            ReadTreeViewData(BReader, TreeViewControl, FileVersion);

            //Close Reader
            BReader.Close();

            return ProfileSelectedName;
        }

        internal void ReadDictionaryData(BinaryStream BReader, Dictionary<uint, EXMusic> DictionaryData)
        {
            int DictionaryItems = BReader.ReadInt32();

            for (int i = 0; i < DictionaryItems; i++)
            {
                uint SoundStreamKey = BReader.ReadUInt32();
                EXMusic Music = new EXMusic
                {
                    //DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                };

                //Read Data Left Channel
                Music.Frequency_LeftChannel = BReader.ReadUInt32();
                Music.Channels_LeftChannel = BReader.Read1Byte();
                Music.Bits_LeftChannel = BReader.ReadUInt32();
                Music.Duration_LeftChannel = BReader.ReadUInt32();
                BReader.ReadUInt32();
                Music.Encoding_LeftChannel = BReader.ReadString();
                Music.WAVFileMD5_LeftChannel = BReader.ReadString();
                Music.WAVFileName_LeftChannel = BReader.ReadString();
                int PCM_DataLength = BReader.ReadInt32();
                Music.PCM_Data_LeftChannel = BReader.ReadBytes(PCM_DataLength);
                int ADPCM_DataLength = BReader.ReadInt32();
                Music.IMA_ADPCM_DATA_LeftChannel = BReader.ReadBytes(ADPCM_DataLength);

                //Read Data Right Channel
                Music.Frequency_RightChannel = BReader.ReadUInt32();
                Music.Channels_RightChannel = BReader.Read1Byte();
                Music.Bits_RightChannel = BReader.ReadUInt32();
                Music.Duration_RightChannel = BReader.ReadUInt32();
                BReader.ReadUInt32();
                Music.Encoding_RightChannel = BReader.ReadString();
                Music.WAVFileMD5_RightChannel = BReader.ReadString();
                Music.WAVFileName_RightChannel = BReader.ReadString();
                PCM_DataLength = BReader.ReadInt32();
                Music.PCM_Data_RightChannel = BReader.ReadBytes(PCM_DataLength);
                ADPCM_DataLength = BReader.ReadInt32();
                Music.IMA_ADPCM_DATA_RightChannel = BReader.ReadBytes(ADPCM_DataLength);

                //Read Start Markers List
                uint StartMarkersCount = BReader.ReadUInt32();
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
                uint MarkersCount = BReader.ReadUInt32();
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

        internal void ReadTreeViewData(BinaryStream BReader, TreeView TreeViewControl, int Version)
        {
            bool ParentIsExpanded = false, NodeIsExpanded = false, NodeIsSelected = false;
            int NumberOfNodes = BReader.ReadInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                string ParentNode = BReader.ReadString();
                string NodeName = BReader.ReadString();
                string DisplayName = BReader.ReadString();
                //Index Unused for now
                BReader.BaseStream.Position += 4;
                //ImageKey Unused for now
                BReader.ReadString();
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
