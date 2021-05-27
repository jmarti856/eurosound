using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion.Musicbanks
{
    internal class ESF_LoadMusicBanks_New
    {
        internal string ReadEuroSoundMusicFile(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXMusic> MusicsList, Dictionary<uint, EXAppTarget> OutputTargets)
        {
            EuroSoundFiles_CommonFunctions ESF_CommonFunctions = new EuroSoundFiles_CommonFunctions();

            //File Hashcode
            FileProperties.Hashcode = BReader.ReadUInt32();
            //Latest SoundID value
            FileProperties.ObjectID = BReader.ReadUInt32();
            //File Size
            BReader.ReadUInt32();
            //Dictionary Data Offset
            uint mediaDictionaryOffset = BReader.ReadUInt32();
            //TreeViewData Offset
            uint treeViewDataOffset = BReader.ReadUInt32();
            //Target App 
            uint targetDictionaryOffset = BReader.ReadUInt32();
            //File Section 4
            BReader.ReadUInt32();
            //File Section 5
            BReader.ReadUInt32();
            //File Section 6
            BReader.ReadUInt32();
            //Project Name
            FileProperties.FileName = BReader.ReadString();
            //Project Description
            FileProperties.ProjectDescription = BReader.ReadString();
            //Profile Name
            string profileSelectedName = BReader.ReadString();
            //Profile Path
            string profileSelected = BReader.ReadString();

            GenericFunctions.CheckProfiles(profileSelected, profileSelectedName);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            BReader.BaseStream.Seek(mediaDictionaryOffset, SeekOrigin.Begin);
            ReadDictionaryData(BReader, MusicsList);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BReader.BaseStream.Seek(treeViewDataOffset, SeekOrigin.Begin);
            ESF_CommonFunctions.ReadTreeViewData(BReader, TreeViewControl);

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            BReader.BaseStream.Seek(targetDictionaryOffset, SeekOrigin.Begin);
            ESF_CommonFunctions.ReadAppTargetData(BReader, OutputTargets);

            //Close Reader
            BReader.Close();

            return profileSelectedName;
        }

        internal void ReadDictionaryData(BinaryReader BReader, Dictionary<uint, EXMusic> DictionaryData)
        {
            uint DictionaryItems = BReader.ReadUInt32();

            for (int i = 0; i < DictionaryItems; i++)
            {
                uint SoundStreamKey = BReader.ReadUInt32();
                EXMusic Music = new EXMusic
                {
                    //DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                };

                //Read Data Left Channel
                Music.Channels_LeftChannel = BReader.ReadByte();
                Music.Frequency_LeftChannel = BReader.ReadUInt32();
                Music.Bits_LeftChannel = BReader.ReadUInt32();
                Music.Duration_LeftChannel = BReader.ReadUInt32();
                Music.Encoding_LeftChannel = BReader.ReadString();
                Music.WAVFileMD5_LeftChannel = BReader.ReadString();
                Music.WAVFileName_LeftChannel = BReader.ReadString();
                uint PCM_DataLength = BReader.ReadUInt32();
                Music.PCM_Data_LeftChannel = BReader.ReadBytes((int)PCM_DataLength);
                uint ADPCM_DataLength = BReader.ReadUInt32();
                Music.IMA_ADPCM_DATA_LeftChannel = BReader.ReadBytes((int)ADPCM_DataLength);

                //Read Data Right Channel
                Music.Channels_RightChannel = BReader.ReadByte();
                Music.Frequency_RightChannel = BReader.ReadUInt32();
                Music.Bits_RightChannel = BReader.ReadUInt32();
                Music.Duration_RightChannel = BReader.ReadUInt32();
                Music.Encoding_RightChannel = BReader.ReadString();
                Music.WAVFileMD5_RightChannel = BReader.ReadString();
                Music.WAVFileName_RightChannel = BReader.ReadString();
                PCM_DataLength = BReader.ReadUInt32();
                Music.PCM_Data_RightChannel = BReader.ReadBytes((int)PCM_DataLength);
                ADPCM_DataLength = BReader.ReadUInt32();
                Music.IMA_ADPCM_DATA_RightChannel = BReader.ReadBytes((int)ADPCM_DataLength);

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
    }
}
