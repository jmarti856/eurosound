using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    class LoadEuroSoundFileV10
    {
        public void ReadEuroSoundFile10(ProjectFile FileProperties, BinaryReader BReader, TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList)
        {
            /*File Hashcode*/
            FileProperties.Hashcode = Convert.ToUInt32(BReader.ReadString(), 16);
            /*Latest SoundID value*/
            FileProperties.SoundID = Convert.ToUInt32(BReader.ReadInt32());
            /*SoundsListData Offset -- Not used for now*/
            BReader.ReadUInt32();
            /*AudioData Offset -- Not used for now*/
            BReader.ReadUInt32();
            /*Type of stored data*/
            FileProperties.TypeOfData = Convert.ToByte(BReader.ReadInt32());
            /*File Name*/
            FileProperties.FileName = BReader.ReadString();

            /*Tree view Data*/
            BReader.BaseStream.Position = (BReader.ReadInt32() + 4);
            ReadTreeViewData(BReader, TreeViewControl);

            /*Sounds list data*/
            BReader.BaseStream.Position = (BReader.ReadInt32() + 4);
            ReadSoundsListData(BReader, SoundsList);

            ReadAudiosDictionary(BReader, AudiosList);

            GenericFunctions.SetCurrentFileLabel(FileProperties.FileName);
        }

        private void ReadAudiosDictionary(BinaryReader BReader, Dictionary<string, EXAudio> AudiosList)
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
                    Flags = Convert.ToUInt16(BReader.ReadInt32()),
                    DataSize = Convert.ToUInt32(BReader.ReadInt32()),
                    Frequency = Convert.ToUInt32(BReader.ReadInt32()),
                    RealSize = Convert.ToUInt32(BReader.ReadInt32()),
                    Channels = Convert.ToUInt32(BReader.ReadInt32()),
                    Bits = Convert.ToUInt32(BReader.ReadInt32()),
                    PSIsample = Convert.ToUInt32(BReader.ReadInt32()),
                    LoopOffset = Convert.ToUInt32(BReader.ReadInt32()),
                    Duration = Convert.ToUInt32(BReader.ReadInt32())
                };
                PCMDataLength = BReader.ReadInt32();
                AudioToAdd.PCMdata = BReader.ReadBytes(PCMDataLength);

                AudiosList.Add(HashMD5, AudioToAdd);
            }
        }

        private void ReadSoundsListData(BinaryReader BReader, Dictionary<uint, EXSound> SoundsList)
        {
            int NumberOfSounds, NumberOfSamples, SubSFXHash;
            uint SoundID;

            NumberOfSounds = BReader.ReadInt32();

            for (int i = 0; i < NumberOfSounds; i++)
            {
                SoundID = Convert.ToUInt32(BReader.ReadInt32());
                EXSound NewSound = new EXSound
                {
                    Hashcode = Convert.ToUInt32(BReader.ReadString(), 16),
                    DisplayName = BReader.ReadString(),
                    OutputThisSound = BReader.ReadBoolean(),

                    /*---Required for EngineX---*/
                    DuckerLenght = Convert.ToInt16(BReader.ReadInt32()),
                    MinDelay = Convert.ToInt16(BReader.ReadInt32()),
                    MaxDelay = Convert.ToInt16(BReader.ReadInt32()),
                    InnerRadiusReal = Convert.ToInt16(BReader.ReadInt32()),
                    OuterRadiusReal = Convert.ToInt16(BReader.ReadInt32()),
                    ReverbSend = Convert.ToSByte(BReader.ReadInt32()),
                    TrackingType = Convert.ToSByte(BReader.ReadInt32()),
                    MaxVoices = Convert.ToSByte(BReader.ReadInt32()),
                    Priority = Convert.ToSByte(BReader.ReadInt32()),
                    Ducker = Convert.ToSByte(BReader.ReadInt32()),
                    MasterVolume = Convert.ToSByte(BReader.ReadInt32()),
                    Flags = Convert.ToUInt16(BReader.ReadInt32()),
                };

                NumberOfSamples = BReader.ReadInt32();
                for (int j = 0; j < NumberOfSamples; j++)
                {
                    EXSample NewSample = new EXSample
                    {
                        Name = BReader.ReadString(),
                        DisplayName = BReader.ReadString(),
                        IsStreamed = BReader.ReadBoolean(),
                        FileRef = Convert.ToInt16(BReader.ReadInt32()),
                        ComboboxSelectedAudio = BReader.ReadString()
                    };
                    try
                    {
                        SubSFXHash = Convert.ToInt32(BReader.ReadString(), 16);
                    }
                    catch
                    {
                        SubSFXHash = 0;
                    }
                    NewSample.HashcodeSubSFX = Convert.ToUInt32(SubSFXHash);

                    /*---Required For EngineX---*/
                    NewSample.PitchOffset = Convert.ToInt16(BReader.ReadInt32());
                    NewSample.RandomPitchOffset = Convert.ToInt16(BReader.ReadInt32());
                    NewSample.BaseVolume = Convert.ToSByte(BReader.ReadInt32());
                    NewSample.RandomVolumeOffset = Convert.ToSByte(BReader.ReadInt32());
                    NewSample.Pan = Convert.ToSByte(BReader.ReadInt32());
                    NewSample.RandomPan = Convert.ToSByte(BReader.ReadInt32());

                    NewSound.Samples.Add(NewSample);
                }

                SoundsList.Add(SoundID, NewSound);
            }
        }

        private void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl)
        {
            int NumberOfNodes;

            NumberOfNodes = BReader.ReadInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                TreeNodeFunctions.TreeNodeAddNewNode(BReader.ReadString(), BReader.ReadString(), BReader.ReadString(), BReader.ReadInt32(), BReader.ReadInt32(), BReader.ReadString(), Color.FromArgb(BReader.ReadInt32()), TreeViewControl);
            }
        }
    }
}
