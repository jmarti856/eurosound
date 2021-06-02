﻿using EuroSound_Application.Editors_and_Tools;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    internal class GenerateSFXStreamedSounds
    {
        private List<long> MarkersStartList = new List<long>();
        private long FileLength1, FileLength2, FullFileLength;

        private const uint FileStart1 = 0x800;
        private const uint FileStart2 = 0x1000;

        //*===============================================================================================
        //* HEADER
        //*===============================================================================================
        internal void WriteFileHeader(BinaryStream BWriter, uint FileHashcode, ProgressBar Bar)
        {
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, 4);

            //--magic[magic value]--
            BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--hashc[Hashcode for the current soundbank without the section prefix]--
            BWriter.WriteUInt32(FileHashcode);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--offst[Constant offset to the next section,]--
            BWriter.WriteUInt32(0xC9);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--fulls[Size of the whole file, in bytes. Unused. ]--
            BWriter.WriteUInt32(0);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
        }

        //*===============================================================================================
        //* Write Sections
        //*===============================================================================================
        internal void WriteFileSections(BinaryStream BWriter, ProgressBar Bar)
        {
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, 6);

            //--File start 1[an offset that points to the stream look-up file details, always 0x800]--
            BWriter.WriteUInt32(FileStart1);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--File length 1[size of the first section, in bytes]--
            BWriter.WriteUInt32(0);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--File start 2[offset to the second section with the sample data]--
            BWriter.WriteUInt32(FileStart2);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--File length 2[size of the second section, in bytes]--
            BWriter.WriteUInt32(0);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--File start 3[unused and uses the same sample data offset as dummy for some reason]--
            BWriter.WriteUInt32(0);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--File length 3[unused and set to zero]--
            BWriter.WriteUInt32(0);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
        }

        //*===============================================================================================
        //* Look up Table
        //*===============================================================================================
        public void WriteLookUptable(BinaryStream BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, ProgressBar Bar)
        {
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, StreamSoundsList.Keys.Count);

            BWriter.Seek((int)FileStart1, SeekOrigin.Begin);
            for (int i = 0; i < StreamSoundsList.Count; i++)
            {
                BWriter.WriteUInt32(Convert.ToUInt32(00000000));
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            }

            FileLength1 = BWriter.BaseStream.Position - FileStart1;
        }

        //*===============================================================================================
        //* WRITE MARKERS
        //*===============================================================================================
        public void WriteStreamFile(BinaryStream BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, ProgressBar Bar)
        {
            //Update GUI
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, StreamSoundsList.Keys.Count);

            BWriter.Seek((int)FileStart2, SeekOrigin.Begin);
            foreach (KeyValuePair<uint, EXSoundStream> SoundToWrtie in StreamSoundsList)
            {
                uint SoundStartOffset = (uint)BWriter.BaseStream.Position;
                MarkersStartList.Add(SoundStartOffset - FileStart2);

                //Marker size
                BWriter.WriteUInt32(0);
                //Audio Offset
                BWriter.WriteUInt32(SoundStartOffset);
                //Audio size
                BWriter.WriteUInt32((uint)SoundToWrtie.Value.IMA_ADPCM_DATA.Length);
                //Start marker count
                BWriter.WriteUInt32((uint)SoundToWrtie.Value.StartMarkers.Count);
                //Marker count
                BWriter.WriteUInt32((uint)SoundToWrtie.Value.Markers.Count);
                //Start marker offset
                long StartMarkerOffset = BWriter.BaseStream.Position - SoundStartOffset;
                BWriter.WriteUInt32((uint)StartMarkerOffset);
                //Marker offset
                BWriter.WriteUInt32(0);
                //Base volume
                BWriter.WriteUInt32(SoundToWrtie.Value.BaseVolume);

                long MarkerSizeStartOffset = BWriter.BaseStream.Position;
                //Start Markers Data
                for (int i = 0; i < SoundToWrtie.Value.StartMarkers.Count; i++)
                {
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].Name);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].Position);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].MusicMakerType);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].Flags);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].Extra);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].LoopStart);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].MarkerCount);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].LoopMarkerCount);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].MarkerPos);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].IsInstant);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].InstantBuffer);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].StateA);
                    BWriter.WriteUInt32(SoundToWrtie.Value.StartMarkers[i].StateB);
                }
                uint MarkerDataOffset = (uint)(BWriter.BaseStream.Position - MarkerSizeStartOffset);

                //Markers
                for (int j = 0; j < SoundToWrtie.Value.Markers.Count; j++)
                {
                    BWriter.WriteInt32(SoundToWrtie.Value.Markers[j].Name);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].Position);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].MusicMakerType);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].Flags);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].Extra);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].LoopStart);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].MarkerCount);
                    BWriter.WriteUInt32(SoundToWrtie.Value.Markers[j].LoopMarkerCount);
                }

                uint MarkerSize = (uint)(BWriter.BaseStream.Position - MarkerSizeStartOffset);

                //Write Marker Size
                BWriter.Seek((int)SoundStartOffset, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)(MarkerSize + StartMarkerOffset));
                BWriter.Seek(20, SeekOrigin.Current);
                BWriter.WriteUInt32((uint)(MarkerDataOffset + StartMarkerOffset));

                //Write ima data
                long AudioOffset = SoundStartOffset + FileStart2;
                BWriter.Seek((int)AudioOffset, SeekOrigin.Begin);
                BWriter.Write(SoundToWrtie.Value.IMA_ADPCM_DATA);

                //Align Bytes
                BWriter.Align(FileStart2);

                //Update GUI
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            }
            FileLength2 = BWriter.BaseStream.Position - FileStart2;

            FullFileLength = BWriter.BaseStream.Position;
        }

        //*===============================================================================================
        //* FINAL OFFSETS
        //*===============================================================================================
        public void WriteFinalOffsets(BinaryStream BWriter, ProgressBar Bar)
        {
            //Update GUI
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, MarkersStartList.Count);

            //File Full Size
            BWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)FullFileLength);

            //File length 1
            BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)FileLength1);

            //File length 2
            BWriter.BaseStream.Seek(0x1C, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)FileLength2);

            BWriter.Seek((int)FileStart1, SeekOrigin.Begin);
            foreach (long Offset in MarkersStartList)
            {
                BWriter.WriteUInt32((uint)Offset);
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            }
        }


        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        internal Dictionary<uint, EXSoundStream> GetFinalSoundsDictionary(Dictionary<uint, EXSoundStream> SoundsList, ProgressBar Bar, Label LabelInfo)
        {
            Dictionary<uint, EXSoundStream> FinalSoundsDict = new Dictionary<uint, EXSoundStream>();

            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, SoundsList.Count());

            //Discard SFXs that has checked as "no output"
            foreach (KeyValuePair<uint, EXSoundStream> SoundToCheck in SoundsList)
            {
                if (SoundToCheck.Value.OutputThisSound)
                {
                    FinalSoundsDict.Add(SoundToCheck.Key, SoundToCheck.Value);
                }
                GenericFunctions.SetLabelText(LabelInfo, "Checking Stream Data");
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            }

            return FinalSoundsDict;
        }
    }
}
