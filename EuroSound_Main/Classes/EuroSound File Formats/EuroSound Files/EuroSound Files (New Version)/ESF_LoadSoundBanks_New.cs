﻿using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion.SoundBanks
{
    internal class ESF_LoadSoundBanks_New
    {
        internal string ReadEuroSoundSoundBankFile(ProjectFile FileProperties, BinaryStream BReader, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, Dictionary<uint, EXAppTarget> OutputTargets, TreeView TreeViewControl, int FileVersion)
        {
            EuroSoundFiles_CommonFunctions ESF_CommonFunctions = new EuroSoundFiles_CommonFunctions();

            //File Hashcode
            FileProperties.Hashcode = BReader.ReadUInt32();
            //Latest SoundID value
            FileProperties.ObjectID = BReader.ReadUInt32();
            //File Size
            BReader.ReadUInt32();
            //AudioData Offset
            uint audioDictionaryOffset = BReader.ReadUInt32();
            //SoundsListData Offset
            uint soundDictionaryOffset = BReader.ReadUInt32();
            //TreeViewData Offset
            uint treeViewDataOffset = BReader.ReadUInt32();
            //Target App
            uint targetDictionaryOffset = BReader.ReadUInt32();
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
            //* Audio Data
            //*===============================================================================================
            long sectionPos = audioDictionaryOffset - BReader.Position;
            BReader.BaseStream.Seek(sectionPos, SeekOrigin.Current);
            ReadAudioDataDictionary(BReader, AudiosList, FileVersion);

            //*===============================================================================================
            //* Sounds List Data
            //*===============================================================================================
            sectionPos = soundDictionaryOffset - BReader.Position;
            BReader.BaseStream.Seek(sectionPos, SeekOrigin.Current);
            ReadSoundsListData(BReader, SoundsList, FileVersion);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            sectionPos = treeViewDataOffset - BReader.Position;
            BReader.BaseStream.Seek(sectionPos, SeekOrigin.Current);
            ESF_CommonFunctions.ReadTreeViewData(BReader, TreeViewControl);

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            sectionPos = targetDictionaryOffset - BReader.Position;
            BReader.BaseStream.Seek(sectionPos, SeekOrigin.Current);
            ESF_CommonFunctions.ReadAppTargetData(BReader, OutputTargets);

            //Close Reader
            BReader.Close();

            return profileSelectedName;
        }

        internal void ReadAudioDataDictionary(BinaryStream BReader, Dictionary<string, EXAudio> AudiosList, int FileVersion)
        {
            uint TotalEntries = BReader.ReadUInt32();

            for (int i = 0; i < TotalEntries; i++)
            {
                string HashMD5 = BReader.ReadString();
                EXAudio AudioToAdd = new EXAudio
                {
                    Dependencies = BReader.ReadString(),
                    LoadedFileName = BReader.ReadString(),
                    Encoding = BReader.ReadString(),
                    Flags = BReader.ReadUInt16(),
                    Frequency = BReader.ReadUInt32(),
                    Channels = BReader.ReadUInt32(),
                    Bits = BReader.ReadUInt32(),
                };
                if (FileVersion < 1014)
                {
                    AudioToAdd.PSIsample = BReader.ReadUInt32();
                }
                AudioToAdd.LoopOffset = BReader.ReadUInt32();
                AudioToAdd.Duration = BReader.ReadUInt32();
                AudioToAdd.FrequencyPS2 = BReader.ReadUInt32();
                int PCMDataLength = BReader.ReadInt32();
                AudioToAdd.PCMdata = BReader.ReadBytes(PCMDataLength);

                //Add object
                AudiosList.Add(HashMD5, AudioToAdd);
            }
        }

        internal void ReadSoundsListData(BinaryStream BReader, Dictionary<uint, EXSound> SoundsList, int FileVersion)
        {
            uint NumberOfSounds = BReader.ReadUInt32();

            for (int i = 0; i < NumberOfSounds; i++)
            {
                uint SoundID = BReader.ReadUInt32();
                EXSound NewSound = new EXSound
                {
                    Hashcode = BReader.ReadUInt32(),
                    OutputThisSound = BReader.ReadBoolean(),

                    //---Required for EngineX---
                    DuckerLength = BReader.ReadInt16(),
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
                if (FileVersion > 1013)
                {
                    NewSound.OutputTarget = BReader.Read1Byte();
                }

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


    }
}
