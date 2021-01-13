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
        public void WriteStreamFile(BinaryStream BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, ProgressBar Bar)
        {
            long StartMarkerOffset, MarkerSizeStartOffset, AudioOffset;
            long AlignOffset;
            uint SoundStartOffset, MarkerDataOffset, MarkerSize;

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
                BWriter.WriteUInt32((uint)00000000);
                //Base volume
                BWriter.WriteUInt32(SoundToWrtie.Value.BaseVolume);

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
                MarkerSize = (uint)(BWriter.BaseStream.Position - MarkerSizeStartOffset);

                //Write Marker Size
                BWriter.Seek((int)SoundStartOffset, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)(MarkerSize + StartMarkerOffset));
                BWriter.Seek(20, SeekOrigin.Current);
                BWriter.WriteUInt32((uint)(MarkerDataOffset + StartMarkerOffset));

                //Write ima data
                AudioOffset = SoundStartOffset + 0x1000;
                BWriter.Seek((int)AudioOffset, SeekOrigin.Begin);
                BWriter.Write(SoundToWrtie.Value.IMA_ADPCM_DATA);
                BWriter.Seek(516, SeekOrigin.Current);

                //Align Bytes
                AlignOffset = (BWriter.BaseStream.Position + 4096) & (4096 - 1);
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
        public void WriteFinalOffsets(BinaryStream BWriter, ProgressBar Bar)
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

            //Stream look-up
            BWriter.Seek((int)FileStart1, SeekOrigin.Begin);
            foreach (long Offset in MarkersStartList)
            {
                BWriter.WriteUInt32((uint)Offset);
                ProgressBarUpdate(Bar, 1);
            }
        }


        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        internal Dictionary<uint, EXSoundStream> GetFinalSoundsDictionary(Dictionary<uint, EXSoundStream> SoundsList, TreeView TreeViewControl, ProgressBar Bar, Label LabelInfo)
        {
            uint NodeKey;
            Dictionary<uint, EXSoundStream> FinalSoundsDict = new Dictionary<uint, EXSoundStream>();

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, SoundsList.Count());

            //Discard SFXs that has checked as "no output"
            foreach (TreeNode node in TreeViewControl.Nodes[0].Nodes)
            {
                NodeKey = Convert.ToUInt32(node.Name);
                EXSoundStream SoundToCheck = EXStreamSoundsFunctions.GetSoundByName(NodeKey, SoundsList);
                if (SoundToCheck != null)
                {
                    if (SoundToCheck.OutputThisSound)
                    {
                        FinalSoundsDict.Add(NodeKey, SoundToCheck);
                    }
                    SetLabelText(LabelInfo, "Checking Stream Data");
                    ProgressBarUpdate(Bar, 1);
                }
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
