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
        internal string ReadEuroSoundStreamFile(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, int FileVersion)
        {
            //File Hashcode
            FileProperties.Hashcode = BReader.ReadUInt32();
            //Sound ID
            FileProperties.SoundID = BReader.ReadUInt32();
            //Sounds List Offset
            BReader.BaseStream.Position += 4;//Only Used in the "Frm_NewStreamSound" Form
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
            ReadDictionaryData(BReader, StreamSoundsList);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Position = TreeViewDataOffset;
            ReadTreeViewData(BReader, TreeViewControl, FileVersion);

            //Close Reader
            BReader.Close();

            return ProfileSelectedName;
        }

        internal void ReadDictionaryData(BinaryReader BReader, Dictionary<uint, EXSoundStream> DictionaryData)
        {
            int DictionaryItems = BReader.ReadInt32();

            for (int i = 0; i < DictionaryItems; i++)
            {
                uint SoundStreamKey = BReader.ReadUInt32();
                EXSoundStream StreamSound = new EXSoundStream
                {
                    //DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                };

                //Read Wav
                int PCM_DataLength = BReader.ReadInt32();
                StreamSound.PCM_Data = BReader.ReadBytes(PCM_DataLength);
                int ADPCM_DataLength = BReader.ReadInt32();
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
                    StreamSound.StartMarkers.Add(StartMarker);
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
                    StreamSound.Markers.Add(Marker);
                }
                StreamSound.OutputThisSound = BReader.ReadBoolean();
                DictionaryData.Add(SoundStreamKey, StreamSound);
            }
        }

        internal void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl, int Version)
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
                if (Color.Equals(NodeColor, ColorTranslator.FromHtml("#000000")) || GlobalPreferences.TV_IgnoreStlyesFromESF)
                {
                    NodeColor = SystemColors.WindowText;
                }

                TreeNodeFunctions.TreeNodeAddNewNode(ParentNode, NodeName, DisplayName, SelectedImageIndex, ImageIndex, Tag, ParentIsExpanded, NodeIsExpanded, NodeIsSelected, NodeColor, TreeViewControl);
            }
        }
    }
}
