using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion.SoundBanks
{
    internal class ESF_SaveSoundBanks
    {
        internal void SaveSoundBanks(BinaryStream BWriter, TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, Dictionary<uint, EXAppTarget> OutputTargets, ProjectFile FileProperties)
        {
            int SpaceBetweenBlocks = 256;
            EuroSoundFiles_CommonFunctions ESF_CommonFunctions = new EuroSoundFiles_CommonFunctions();

            //File Size
            BWriter.WriteUInt32(0);
            //AudioData Offset
            BWriter.WriteUInt32(0);
            //SoundsListData Offset
            BWriter.WriteUInt32(0);
            //TreeViewData Offset
            BWriter.WriteUInt32(0);
            //Target App
            BWriter.WriteUInt32(0);
            //File Section 5
            BWriter.WriteUInt32(0);
            //File Section 6
            BWriter.WriteUInt32(0);
            //Project Name
            BWriter.WriteString(FileProperties.FileName);
            //Project Description
            BWriter.WriteString(FileProperties.ProjectDescription);
            //Profile Name
            BWriter.WriteString(GlobalPreferences.SelectedProfileName);
            //Profile Path
            BWriter.WriteString(GlobalPreferences.SelectedProfile);

            //Space between sections
            GenericFunctions.CustomSeek(2048, BWriter, (byte)'«');

            //*===============================================================================================
            //* Audio Data
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Section Data
            long AudioDataOffset = BWriter.BaseStream.Position;
            SaveAudiosData(AudiosList, BWriter);

            //Align Bytes
            BWriter.Align(16, true);

            //Space between sections
            GenericFunctions.CustomSeek(SpaceBetweenBlocks, BWriter, (byte)'«');

            //*===============================================================================================
            //* Sounds List Data
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Section Data
            long SoundsListDataOffset = BWriter.BaseStream.Position;
            SaveSoundsListData(SoundsList, BWriter);

            //Align Bytes
            BWriter.Align(16, true);

            //Space between sections
            GenericFunctions.CustomSeek(SpaceBetweenBlocks, BWriter, (byte)'«');

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Section Data
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            ESF_CommonFunctions.SaveTreeViewData(TreeViewControl, BWriter, 4);

            //Align Bytes
            BWriter.Align(16, true);

            //Space between sections
            GenericFunctions.CustomSeek(SpaceBetweenBlocks, BWriter, (byte)'«');

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            //Write Data
            long AppTargetDataOffset = BWriter.BaseStream.Position;
            ESF_CommonFunctions.SaveAppTargetData(OutputTargets, BWriter);
            BWriter.Align(16, true);

            long FileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* FINAL OFFSETS
            //*===============================================================================================
            //Go to section offsets position
            BWriter.Seek(0x10, SeekOrigin.Begin);

            BWriter.WriteUInt32((uint)FileSize);
            BWriter.WriteUInt32((uint)AudioDataOffset);
            BWriter.WriteUInt32((uint)SoundsListDataOffset);
            BWriter.WriteUInt32((uint)TreeViewDataOffset);
            BWriter.WriteUInt32((uint)AppTargetDataOffset);
        }

        private void SaveAudiosData(Dictionary<string, EXAudio> audiosList, BinaryStream binaryWriter)
        {
            binaryWriter.WriteUInt32((uint)audiosList.Count);

            foreach (KeyValuePair<string, EXAudio> entry in audiosList)
            {
                binaryWriter.WriteString(entry.Key);
                binaryWriter.WriteString(entry.Value.Dependencies);
                binaryWriter.WriteString(entry.Value.LoadedFileName);
                binaryWriter.WriteString(entry.Value.Encoding);
                binaryWriter.WriteUInt16(entry.Value.Flags);
                binaryWriter.WriteUInt32(entry.Value.Frequency);
                binaryWriter.WriteUInt32(entry.Value.Channels);
                binaryWriter.WriteUInt32(entry.Value.Bits);
                binaryWriter.WriteUInt32(entry.Value.PSIsample);
                binaryWriter.WriteUInt32(entry.Value.LoopOffset);
                binaryWriter.WriteUInt32(entry.Value.Duration);
                binaryWriter.WriteUInt32(entry.Value.FrequencyPS2);
                binaryWriter.WriteUInt32(entry.Value.LoopOffsetPS2);
                binaryWriter.WriteUInt32((uint)entry.Value.PCMdata.Length);
                binaryWriter.WriteBytes(entry.Value.PCMdata);
                binaryWriter.WriteBoolean(entry.Value.LoopOffsetPS2Locked);
            }
        }

        private void SaveSoundsListData(Dictionary<uint, EXSound> soundsList, BinaryStream binaryWriter)
        {
            binaryWriter.WriteUInt32((uint)soundsList.Count);

            foreach (KeyValuePair<uint, EXSound> soundItem in soundsList)
            {
                //Display Info
                binaryWriter.WriteUInt32(soundItem.Key);
                binaryWriter.WriteUInt32(soundItem.Value.Hashcode);
                binaryWriter.WriteBoolean(soundItem.Value.OutputThisSound);

                //---Required for EngineX---
                binaryWriter.WriteInt16(soundItem.Value.DuckerLenght);
                binaryWriter.WriteInt16(soundItem.Value.MinDelay);
                binaryWriter.WriteInt16(soundItem.Value.MaxDelay);
                binaryWriter.WriteInt16(soundItem.Value.InnerRadiusReal);
                binaryWriter.WriteInt16(soundItem.Value.OuterRadiusReal);
                binaryWriter.WriteSByte(soundItem.Value.ReverbSend);
                binaryWriter.WriteSByte(soundItem.Value.TrackingType);
                binaryWriter.WriteSByte(soundItem.Value.MaxVoices);
                binaryWriter.WriteSByte(soundItem.Value.Priority);
                binaryWriter.WriteSByte(soundItem.Value.Ducker);
                binaryWriter.WriteSByte(soundItem.Value.MasterVolume);
                binaryWriter.WriteUInt16(soundItem.Value.Flags);

                //Write Samples
                binaryWriter.WriteUInt32((uint)soundItem.Value.Samples.Count);
                foreach (KeyValuePair<uint, EXSample> itemSample in soundItem.Value.Samples)
                {
                    //Key
                    binaryWriter.WriteUInt32(itemSample.Key);

                    //Display Info
                    binaryWriter.WriteBoolean(itemSample.Value.IsStreamed);
                    binaryWriter.WriteInt16(itemSample.Value.FileRef);
                    binaryWriter.WriteString(itemSample.Value.ComboboxSelectedAudio);
                    binaryWriter.WriteUInt32(itemSample.Value.HashcodeSubSFX);

                    //---Required for EngineX---
                    binaryWriter.WriteInt16(itemSample.Value.PitchOffset);
                    binaryWriter.WriteInt16(itemSample.Value.RandomPitchOffset);
                    binaryWriter.WriteSByte(itemSample.Value.BaseVolume);
                    binaryWriter.WriteSByte(itemSample.Value.RandomVolumeOffset);
                    binaryWriter.WriteSByte(itemSample.Value.Pan);
                    binaryWriter.WriteSByte(itemSample.Value.RandomPan);
                }
            }
        }
    }
}
