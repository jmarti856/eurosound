using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    internal class GenerateSFXMusicBank
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
            uint Hashcode;

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, 4);

            //--magic[magic value]--
            BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
            ProgressBarUpdate(Bar, 1);

            //--hashc[Hashcode for the current soundbank without the section prefix]--
            Hashcode = 0x00E00000 | (FileHashcode - 0x1BE00000);
            BWriter.WriteUInt32(Hashcode);
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
        //* FILE SECTION 1
        //*===============================================================================================
        public void WriteFileSection1(BinaryStream BWriter, Dictionary<uint, EXMusic> MusicsDictionary, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar)
        {
            long StartMarkerOffset, MarkerSizeStartOffset;
            long AlignOffset;
            uint SoundStartOffset, MarkerDataOffset;
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
            ProgressBarMaximum(Bar, 1);

            BWriter.Seek((int)FileStart1, SeekOrigin.Begin);

            SoundStartOffset = (uint)BWriter.BaseStream.Position;
            MarkersStartList.Add(SoundStartOffset - FileStart2);

            foreach (EXMusic MusicToExport in MusicsDictionary.Values)
            {
                //Start marker count
                BWriter.WriteUInt32((uint)MusicToExport.StartMarkers.Count);
                //Marker count
                BWriter.WriteUInt32((uint)MusicToExport.Markers.Count);
                //Start marker offset
                StartMarkerOffset = BWriter.BaseStream.Position - SoundStartOffset;
                BWriter.WriteUInt32((uint)StartMarkerOffset);
                //Marker offset
                BWriter.WriteUInt32(00000000);
                //Base volume
                BWriter.WriteUInt32(MusicToExport.BaseVolume);

                //--[Write Debug File]--
                if (DebugMarkersData)
                {
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine("// Wav File Name");
                    DebugFile.WriteLine("\t" + MusicToExport.WAVFileMD5);
                    DebugFile.WriteLine("// Start marker count");
                    DebugFile.WriteLine("\t" + (uint)MusicToExport.StartMarkers.Count);
                    DebugFile.WriteLine("// Marker count");
                    DebugFile.WriteLine("\t" + (uint)MusicToExport.Markers.Count);
                    DebugFile.WriteLine("// Start marker offset");
                    DebugFile.WriteLine("\t" + (uint)StartMarkerOffset);
                    DebugFile.WriteLine("// Marker offset");
                    DebugFile.WriteLine("\t" + (uint)00000000);
                    DebugFile.WriteLine("// Base volume");
                    DebugFile.WriteLine("\t" + MusicToExport.BaseVolume + "\n");
                }

                MarkerSizeStartOffset = BWriter.BaseStream.Position;
                //Start Markers Data
                for (int i = 0; i < MusicToExport.StartMarkers.Count; i++)
                {
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].Name);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].Position);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].MusicMakerType);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].Flags);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].Extra);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].LoopStart);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].MarkerCount);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].LoopMarkerCount);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].MarkerPos);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].IsInstant);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].InstantBuffer);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].StateA);
                    BWriter.WriteUInt32(MusicToExport.StartMarkers[i].StateB);
                }

                //--[Write Debug File]--
                if (DebugMarkersData)
                {
                    for (int i = 0; i < MusicToExport.StartMarkers.Count; i++)
                    {
                        DebugFile.WriteLine("// ----Start Markers Data----" + "\n");
                        DebugFile.WriteLine("// Name");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].Name);
                        DebugFile.WriteLine("// Position");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].Position);
                        DebugFile.WriteLine("// Music Maker Type");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].MusicMakerType);
                        DebugFile.WriteLine("// Flags");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].Flags);
                        DebugFile.WriteLine("// Extra");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].Extra);
                        DebugFile.WriteLine("// Loop Start");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].LoopStart);
                        DebugFile.WriteLine("// Marker Count");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].MarkerCount);
                        DebugFile.WriteLine("// Loop Marker Count");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].LoopMarkerCount);
                        DebugFile.WriteLine("// Marker Pos");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].MarkerPos);
                        DebugFile.WriteLine("// Is Instant");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].IsInstant);
                        DebugFile.WriteLine("// Instant Buffer");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].InstantBuffer);
                        DebugFile.WriteLine("// StateA");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].StateA);
                        DebugFile.WriteLine("// StateB");
                        DebugFile.WriteLine("\t" + MusicToExport.StartMarkers[i].StateB);
                    }
                }
                MarkerDataOffset = (uint)(BWriter.BaseStream.Position - MarkerSizeStartOffset);

                //Markers
                for (int j = 0; j < MusicToExport.Markers.Count; j++)
                {
                    BWriter.WriteInt32(MusicToExport.Markers[j].Name);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].Position);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].MusicMakerType);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].Flags);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].Extra);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].LoopStart);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].MarkerCount);
                    BWriter.WriteUInt32(MusicToExport.Markers[j].LoopMarkerCount);
                }

                //--[Write Debug File]--
                if (DebugMarkersData)
                {
                    for (int j = 0; j < MusicToExport.Markers.Count; j++)
                    {
                        DebugFile.WriteLine("// ----Markers Data----" + "\n");
                        DebugFile.WriteLine("// Name");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].Name);
                        DebugFile.WriteLine("// Position");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].Position);
                        DebugFile.WriteLine("// Music Maker Type");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].MusicMakerType);
                        DebugFile.WriteLine("// Flags");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].Flags);
                        DebugFile.WriteLine("// Extra");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].Extra);
                        DebugFile.WriteLine("// Loop Start");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].LoopStart);
                        DebugFile.WriteLine("// Marker Count");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].MarkerCount);
                        DebugFile.WriteLine("// Loop Marker Count");
                        DebugFile.WriteLine("\t" + MusicToExport.Markers[j].LoopMarkerCount + "\n");
                    }
                }

                //Write Marker Data Offset
                BWriter.Seek((int)SoundStartOffset, SeekOrigin.Begin);
                BWriter.Seek(8, SeekOrigin.Current);
                BWriter.WriteUInt32((uint)(MarkerDataOffset + StartMarkerOffset));

                //Align Bytes
                AlignOffset = (BWriter.BaseStream.Position + FileStart2) & (FileStart2 - 1);
                BWriter.Seek(AlignOffset, SeekOrigin.Current);

                BWriter.Align(16);

                //Update GUI
                ProgressBarUpdate(Bar, 1);
            }

            FileLength1 = BWriter.BaseStream.Position - FileStart1;
        }

        //*===============================================================================================
        //* FILE SECTION 2
        //*===============================================================================================
        public void WriteFileSection2(BinaryStream BWriter, Dictionary<uint, EXMusic> MusicsDictionary)
        {
            int IndexLC, IndexRC;
            bool StereoInterleaving;
            int TotalLength;

            //Write ADPCM Data
            BWriter.Seek((int)FileStart2, SeekOrigin.Begin);

            foreach (EXMusic MusicToExport in MusicsDictionary.Values)
            {
                //Initialize variables
                IndexLC = 0;
                IndexRC = 0;
                StereoInterleaving = true;
                TotalLength = MusicToExport.IMA_ADPCM_DATA_LeftChannel.Length * 2;

                //Write ima data
                for (int i = 0; i < TotalLength; i++)
                {
                    if (StereoInterleaving)
                    {
                        BWriter.Write(MusicToExport.IMA_ADPCM_DATA_LeftChannel[IndexLC]);
                        IndexLC++;
                    }
                    else
                    {
                        BWriter.Write(MusicToExport.IMA_ADPCM_DATA_RightChannel[IndexRC]);
                        IndexRC++;
                    }
                    StereoInterleaving = !StereoInterleaving;
                }

                FileLength2 = BWriter.BaseStream.Position - FileStart2;
                FullFileLength = BWriter.BaseStream.Position;
            }
        }

        //*===============================================================================================
        //* FINAL OFFSETS
        //*===============================================================================================
        public void WriteFinalOffsets(BinaryStream BWriter, uint FileHashcode, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar)
        {
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
        internal Dictionary<uint, EXMusic> GetFinalMusicsDictionary(Dictionary<uint, EXMusic> MusicsList, ProgressBar Bar, Label LabelInfo)
        {
            Dictionary<uint, EXMusic> FinalSoundsDict = new Dictionary<uint, EXMusic>();

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, MusicsList.Count());

            //Discard SFXs that has checked as "no output"
            foreach (KeyValuePair<uint, EXMusic> MusicToCheck in MusicsList)
            {
                if (MusicToCheck.Value.OutputThisSound)
                {
                    FinalSoundsDict.Add(MusicToCheck.Key, MusicToCheck.Value);
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
