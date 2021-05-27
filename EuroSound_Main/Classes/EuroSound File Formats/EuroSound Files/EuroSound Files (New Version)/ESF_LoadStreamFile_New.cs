using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion.StreamFile
{
    internal class ESF_LoadStreamFile_New
    {
        internal string ReadEuroSoundStreamFile(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, Dictionary<uint, EXAppTarget> OutputTargets)
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
            //Sounds List Offset
            BReader.ReadUInt32(); //Only Used in the "Frm_NewStreamSound" Form
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
            ReadDictionaryData(BReader, StreamSoundsList);

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

        internal void ReadDictionaryData(BinaryReader BReader, Dictionary<uint, EXSoundStream> DictionaryData)
        {
            uint DictionaryItems = BReader.ReadUInt32();

            for (int i = 0; i < DictionaryItems; i++)
            {
                uint SoundStreamKey = BReader.ReadUInt32();
                EXSoundStream StreamSound = new EXSoundStream
                {
                    //DisplayName = BReader.ReadString(),
                    BaseVolume = BReader.ReadUInt32(),
                };

                //Read Wav
                uint PCM_DataLength = BReader.ReadUInt32();
                StreamSound.PCM_Data = BReader.ReadBytes((int)PCM_DataLength);
                uint ADPCM_DataLength = BReader.ReadUInt32();
                StreamSound.IMA_ADPCM_DATA = BReader.ReadBytes((int)ADPCM_DataLength);
                StreamSound.Frequency = BReader.ReadUInt32();
                StreamSound.Bits = BReader.ReadUInt32();
                StreamSound.Duration = BReader.ReadUInt32();
                StreamSound.Channels = BReader.ReadByte();
                StreamSound.Encoding = BReader.ReadString();
                StreamSound.WAVFileMD5 = BReader.ReadString();
                StreamSound.WAVFileName = BReader.ReadString();


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
    }
}
