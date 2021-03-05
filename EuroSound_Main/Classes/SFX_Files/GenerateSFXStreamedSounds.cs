using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    internal class GenerateSFXStreamedSounds
    {
        private List<long> MarkersStartList = new List<long>();
        long FileLength1, FileLength2, FullFileLength;

        private const uint FileStart1 = 0x800;
        private const uint FileStart2 = 0x1000;

        //*===============================================================================================
        //* HEADER
        //*===============================================================================================
        internal void WriteFileHeader(BinaryStream BWriter, uint FileHashcode, ProgressBar Bar)
        {
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, 4);

            //--magic[magic value]--
            BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
            ProgressBarUpdate(Bar, 1);

            //--hashc[Hashcode for the current soundbank without the section prefix]--
            BWriter.WriteUInt32(FileHashcode);
            ProgressBarUpdate(Bar, 1);

            //--offst[Constant offset to the next section,]--
            BWriter.WriteUInt32(0xC9);
            ProgressBarUpdate(Bar, 1);

            //--fulls[Size of the whole file, in bytes. Unused. ]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);
        }

        //*===============================================================================================
        //* Write Sections
        //*===============================================================================================
        internal void WriteFileSections(BinaryStream BWriter, ProgressBar Bar)
        {
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, 6);

            //--File start 1[an offset that points to the stream look-up file details, always 0x800]--
            BWriter.WriteUInt32(FileStart1);
            ProgressBarUpdate(Bar, 1);
            //--File length 1[size of the first section, in bytes]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);

            //--File start 2[offset to the second section with the sample data]--
            BWriter.WriteUInt32(FileStart2);
            ProgressBarUpdate(Bar, 1);
            //--File length 2[size of the second section, in bytes]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);

            //--File start 3[unused and uses the same sample data offset as dummy for some reason]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);
            //--File length 3[unused and set to zero]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);
        }

        //*===============================================================================================
        //* Look up Table
        //*===============================================================================================
        public void WriteLookUptable(BinaryStream BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, ProgressBar Bar)
        {
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, StreamSoundsList.Keys.Count);

            BWriter.Seek((int)FileStart1, SeekOrigin.Begin);
            for (int i = 0; i < StreamSoundsList.Count; i++)
            {
                BWriter.WriteUInt32(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
            }

            FileLength1 = BWriter.BaseStream.Position - FileStart1;
        }

        //*===============================================================================================
        //* WRITE MARKERS
        //*===============================================================================================
        public void WriteStreamFile(BinaryStream BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar)
        {
            long StartMarkerOffset, MarkerSizeStartOffset, AudioOffset;
            long AlignOffset;
            uint SoundStartOffset, MarkerDataOffset, MarkerSize;
            bool DebugMarkersData;

            //CheckFlag Stream Data is checked
            DebugMarkersData = Convert.ToBoolean((DebugFlags >> 1) & 1);

            if (DebugMarkersData)
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// Stream Sounds Data");
                DebugFile.WriteLine(new String('/', 70) + "\n");
            }

            //Update GUI
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, StreamSoundsList.Keys.Count);

            BWriter.Seek((int)FileStart2, SeekOrigin.Begin);
            foreach (KeyValuePair<uint, EXSoundStream> SoundToWrtie in StreamSoundsList)
            {
                SoundStartOffset = (uint)BWriter.BaseStream.Position;
                MarkersStartList.Add(SoundStartOffset - FileStart2);

                //Marker size
                BWriter.WriteUInt32(00000000);
                //Audio Offset
                BWriter.WriteUInt32(SoundStartOffset);
                //Audio size
                BWriter.WriteUInt32((uint)SoundToWrtie.Value.IMA_ADPCM_DATA.Length);
                //Start marker count
                BWriter.WriteUInt32((uint)SoundToWrtie.Value.StartMarkers.Count);
                //Marker count
                BWriter.WriteUInt32((uint)SoundToWrtie.Value.Markers.Count);
                //Start marker offset
                StartMarkerOffset = BWriter.BaseStream.Position - SoundStartOffset;
                BWriter.WriteUInt32((uint)StartMarkerOffset);
                //Marker offset
                BWriter.WriteUInt32(00000000);
                //Base volume
                BWriter.WriteUInt32(SoundToWrtie.Value.BaseVolume);

                //--[Write Debug File]--
                if (DebugMarkersData)
                {
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine("// Wav File Name");
                    DebugFile.WriteLine("\t" + SoundToWrtie.Value.WAVFileName);
                    DebugFile.WriteLine("// Audio size");
                    DebugFile.WriteLine("\t" + (uint)SoundToWrtie.Value.IMA_ADPCM_DATA.Length);
                    DebugFile.WriteLine("// Start marker count");
                    DebugFile.WriteLine("\t" + (uint)SoundToWrtie.Value.StartMarkers.Count);
                    DebugFile.WriteLine("// Marker count");
                    DebugFile.WriteLine("\t" + (uint)SoundToWrtie.Value.Markers.Count);
                    DebugFile.WriteLine("// Start marker offset");
                    DebugFile.WriteLine("\t" + (uint)StartMarkerOffset);
                    DebugFile.WriteLine("// Marker offset");
                    DebugFile.WriteLine("\t" + (uint)00000000);
                    DebugFile.WriteLine("// Base volume");
                    DebugFile.WriteLine("\t" + SoundToWrtie.Value.BaseVolume + "\n");
                }

                MarkerSizeStartOffset = BWriter.BaseStream.Position;
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

                //--[Write Debug File]--
                if (DebugMarkersData)
                {
                    for (int i = 0; i < SoundToWrtie.Value.StartMarkers.Count; i++)
                    {
                        DebugFile.WriteLine("// ----Start Markers Data----" + "\n");
                        DebugFile.WriteLine("// Name");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].Name);
                        DebugFile.WriteLine("// Position");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].Position);
                        DebugFile.WriteLine("// Music Maker Type");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].MusicMakerType);
                        DebugFile.WriteLine("// Flags");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].Flags);
                        DebugFile.WriteLine("// Extra");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].Extra);
                        DebugFile.WriteLine("// Loop Start");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].LoopStart);
                        DebugFile.WriteLine("// Marker Count");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].MarkerCount);
                        DebugFile.WriteLine("// Loop Marker Count");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].LoopMarkerCount);
                        DebugFile.WriteLine("// Marker Pos");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].MarkerPos);
                        DebugFile.WriteLine("// Is Instant");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].IsInstant);
                        DebugFile.WriteLine("// Instant Buffer");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].InstantBuffer);
                        DebugFile.WriteLine("// StateA");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].StateA);
                        DebugFile.WriteLine("// StateB");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.StartMarkers[i].StateB);
                    }
                }
                MarkerDataOffset = (uint)(BWriter.BaseStream.Position - MarkerSizeStartOffset);

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

                //--[Write Debug File]--
                if (DebugMarkersData)
                {
                    for (int j = 0; j < SoundToWrtie.Value.Markers.Count; j++)
                    {
                        DebugFile.WriteLine("// ----Markers Data----" + "\n");
                        DebugFile.WriteLine("// Name");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].Name);
                        DebugFile.WriteLine("// Position");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].Position);
                        DebugFile.WriteLine("// Music Maker Type");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].MusicMakerType);
                        DebugFile.WriteLine("// Flags");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].Flags);
                        DebugFile.WriteLine("// Extra");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].Extra);
                        DebugFile.WriteLine("// Loop Start");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].LoopStart);
                        DebugFile.WriteLine("// Marker Count");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].MarkerCount);
                        DebugFile.WriteLine("// Loop Marker Count");
                        DebugFile.WriteLine("\t" + SoundToWrtie.Value.Markers[j].LoopMarkerCount + "\n");
                    }
                }
                MarkerSize = (uint)(BWriter.BaseStream.Position - MarkerSizeStartOffset);

                //Write Marker Size
                BWriter.Seek((int)SoundStartOffset, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)(MarkerSize + StartMarkerOffset));
                BWriter.Seek(20, SeekOrigin.Current);
                BWriter.WriteUInt32((uint)(MarkerDataOffset + StartMarkerOffset));

                //Write ima data
                AudioOffset = SoundStartOffset + FileStart2;
                BWriter.Seek((int)AudioOffset, SeekOrigin.Begin);
                BWriter.Write(SoundToWrtie.Value.IMA_ADPCM_DATA);
                BWriter.Seek(516, SeekOrigin.Current);

                //Align Bytes
                AlignOffset = (BWriter.BaseStream.Position + FileStart2) & (FileStart2 - 1);
                BWriter.Seek(AlignOffset, SeekOrigin.Current);

                BWriter.Align(16);

                //Update GUI
                ProgressBarUpdate(Bar, 1);
            }
            FileLength2 = BWriter.BaseStream.Position - FileStart2;

            FullFileLength = BWriter.BaseStream.Position;
        }

        //*===============================================================================================
        //* FINAL OFFSETS
        //*===============================================================================================
        public void WriteFinalOffsets(BinaryStream BWriter, uint FileHashcode, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar)
        {
            bool DebugLookupTable;

            //CheckFlag Stream Data is checked
            DebugLookupTable = Convert.ToBoolean((DebugFlags >> 0) & 1);

            //Update GUI
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, MarkersStartList.Count);

            //File Full Size
            BWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)FullFileLength);

            //File length 1
            BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)FileLength1);

            //File length 2
            BWriter.BaseStream.Seek(0x1C, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)FileLength2);

            //Stream look-up
            if (DebugLookupTable)
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// Lookup Table");
                DebugFile.WriteLine(new String('/', 70) + "\n");
            }

            BWriter.Seek((int)FileStart1, SeekOrigin.Begin);
            foreach (long Offset in MarkersStartList)
            {
                BWriter.WriteUInt32((uint)Offset);
                ProgressBarUpdate(Bar, 1);
                if (DebugLookupTable)
                {
                    DebugFile.WriteLine("\t" + (uint)Offset);
                }
            }

            //CheckFlag Header Info is checked
            if (Convert.ToBoolean((DebugFlags >> 2) & 1))
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// Stream File Header");
                DebugFile.WriteLine(new String('/', 70) + "\n");
                DebugFile.WriteLine("// 'MUSX' Marker");
                DebugFile.WriteLine("\t4d555358");
                DebugFile.WriteLine("// File HashCode");
                DebugFile.WriteLine("\t0x" + FileHashcode.ToString("X8"));
                DebugFile.WriteLine("// File Version");
                DebugFile.WriteLine("\t000000c9");
                DebugFile.WriteLine("// File Size");
                DebugFile.WriteLine("\t" + FullFileLength);
                DebugFile.WriteLine("// Offset To File Start 1");
                DebugFile.WriteLine("\t800h");
                DebugFile.WriteLine("// File Start 1 Length");
                DebugFile.WriteLine("\t" + FileLength1);
                DebugFile.WriteLine("// Offset To File Start 2");
                DebugFile.WriteLine("\t1000h");
                DebugFile.WriteLine("// File Start 2 Length");
                DebugFile.WriteLine("\t" + FileLength2);
                DebugFile.WriteLine("// Offset To File Start 3");
                DebugFile.WriteLine("\t0");
                DebugFile.WriteLine("// File Start 3 Length");
                DebugFile.WriteLine("\t0" + "\n");
            }
        }


        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        internal Dictionary<uint, EXSoundStream> GetFinalSoundsDictionary(Dictionary<uint, EXSoundStream> SoundsList, ProgressBar Bar, Label LabelInfo)
        {
            Dictionary<uint, EXSoundStream> FinalSoundsDict = new Dictionary<uint, EXSoundStream>();

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, SoundsList.Count());

            //Discard SFXs that has checked as "no output"
            foreach (KeyValuePair<uint, EXSoundStream> SoundToCheck in SoundsList)
            {
                if (SoundToCheck.Value.OutputThisSound)
                {
                    FinalSoundsDict.Add(SoundToCheck.Key, SoundToCheck.Value);
                }
                SetLabelText(LabelInfo, "Checking Stream Data");
                ProgressBarUpdate(Bar, 1);
            }

            return FinalSoundsDict;
        }

        private void ProgressBarReset(ProgressBar BarToReset)
        {
            if (BarToReset != null)
            {
                //Update Progress Bar
                BarToReset.Invoke((MethodInvoker)delegate
                {
                    BarToReset.Value = 0;
                });
            }
        }

        private void ProgressBarUpdate(ProgressBar BarToUpdate, int ValueToAdd)
        {
            //Update Progress Bar
            if (BarToUpdate != null)
            {
                BarToUpdate.Invoke((MethodInvoker)delegate
                {
                    BarToUpdate.Value += ValueToAdd;
                });
                Thread.Sleep(1);
            }
        }

        private void ProgressBarMaximum(ProgressBar BarToChange, int Maximum)
        {
            if (BarToChange != null)
            {
                //Update Progress Bar
                BarToChange.Invoke((MethodInvoker)delegate
                {
                    BarToChange.Maximum = Maximum;
                });
            }
        }

        private void SetLabelText(Label LabelToChange, string TextToShow)
        {
            if (LabelToChange != null)
            {
                LabelToChange.Invoke((MethodInvoker)delegate
                {
                    LabelToChange.Text = TextToShow;
                });
            }
        }
    }
}
