using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal class GenerateSFXStreamedSounds
    {
        private List<long> MarkersStartList = new List<long>();
        long FileLength1, FileLength2;

        //*===============================================================================================
        //* HEADER
        //*===============================================================================================
        internal void WriteFileHeader(BinaryWriter BWriter, uint FileHashcode, ProgressBar Bar)
        {
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, 4);

                /*--magic[magic value]--*/
                BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
                ProgressBarUpdate(Bar, 1);

                /*--hashc[Hashcode for the current soundbank without the section prefix]--*/
                BWriter.Write(Convert.ToUInt32(FileHashcode));
                ProgressBarUpdate(Bar, 1);

                /*--offst[Constant offset to the next section,]--*/
                BWriter.Write(Convert.ToUInt32(0xC9));
                ProgressBarUpdate(Bar, 1);

                /*--fulls[Size of the whole file, in bytes. Unused. ]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
            }
            catch
            {

            }
        }

        //*===============================================================================================
        //* Write Sections
        //*===============================================================================================
        internal void WriteFileSections(BinaryWriter BWriter, int NumberOfSamples, ProgressBar Bar)
        {
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, 6);

                /*--File start 1[an offset that points to the stream look-up file details, always 0x800]--*/
                BWriter.Write(Convert.ToUInt32(0x800));
                ProgressBarUpdate(Bar, 1);
                /*--File length 1[size of the first section, in bytes]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);

                /*--File start 2[offset to the second section with the sample data]--*/
                BWriter.Write(Convert.ToUInt32(0x1000));
                ProgressBarUpdate(Bar, 1);
                /*--File length 2[size of the second section, in bytes]--*/
                BWriter.Write(Convert.ToUInt32(NumberOfSamples));
                ProgressBarUpdate(Bar, 1);

                /*--File start 3[unused and uses the same sample data offset as dummy for some reason]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
                /*--File length 3[unused and set to zero]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
            }
            catch
            {

            }
        }

        //*===============================================================================================
        //* Look up Table
        //*===============================================================================================
        private void WriteLookUptable(BinaryWriter BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, ProgressBar Bar)
        {
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, StreamSoundsList.Keys.Count);

            BWriter.Seek(0x800, SeekOrigin.Begin);
            for (int i = 0; i < StreamSoundsList.Count; i++)
            {
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
            }

            FileLength1 = BWriter.BaseStream.Position;
        }

        //*===============================================================================================
        //* WRITE MARKERS
        //*===============================================================================================
        private void WriteStreamFile(BinaryWriter BWriter, Dictionary<uint, EXSoundStream> StreamSoundsList, ProgressBar Bar)
        {
            long MarkerStart;

            /*Update GUI*/
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, StreamSoundsList.Keys.Count);

            BWriter.Seek(0x1000, SeekOrigin.Begin);
            foreach (KeyValuePair<uint, EXSoundStream> SoundToWrtie in StreamSoundsList)
            {
                MarkerStart = BWriter.BaseStream.Position;
                MarkersStartList.Add(MarkerStart);

                /*Marker size*/
                BWriter.Write(Convert.ToUInt32(77777777));
                /*Audio Offset*/
                BWriter.Write(Convert.ToUInt32(88888888));
                /*Audio size*/
                BWriter.Write(Convert.ToUInt32(SoundToWrtie.Value.IMA_ADPCM_DATA.Length));
                /*Start marker count*/
                BWriter.Write(Convert.ToUInt32(SoundToWrtie.Value.IDMarkerPos));
                /*Marker count*/
                BWriter.Write(Convert.ToUInt32(SoundToWrtie.Value.IDMarkerName));
                /*Start marker offset.*/
                BWriter.Write(Convert.ToUInt32(00000000));
                /*Marker offset.*/
                BWriter.Write(Convert.ToUInt32(00000000));
                /*Base volume*/
                BWriter.Write(Convert.ToUInt32(SoundToWrtie.Value.BaseVolume));

                /*Stream marker start data */
                foreach (EXStreamSoundMarker SoundMarker in SoundToWrtie.Value.Markers)
                {
                    /*Marker*/
                    foreach (EXStreamSoundMarkerData SoundMarkerData in SoundMarker.MarkersData)
                    {
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.Name));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.Position));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.MusicMakerType));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.Flags));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.Extra));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.LoopStart));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.MarkerCount));
                        BWriter.Write(Convert.ToUInt32(SoundMarkerData.LoopMarkerCount));
                    }
                    /*Marker position*/
                    BWriter.Write(Convert.ToUInt32(SoundMarker.Position));
                    /*Is instant*/
                    BWriter.Write(Convert.ToUInt32(SoundMarker.IsInstant));
                    /*Instant buffer.*/
                    BWriter.Write(Convert.ToUInt32(SoundMarker.InstantBuffer));
                    /*State*/
                    BWriter.Write(Convert.ToUInt32(SoundMarker.State));
                }
            }

            FileLength2 = BWriter.BaseStream.Position;
        }

        //*===============================================================================================
        //* FINAL OFFSETS
        //*===============================================================================================
        private void WriteFinalOffsets(BinaryWriter BWriter)
        {
            /*File length 1*/
            BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(FileLength1));

            /*File length 2*/
            BWriter.BaseStream.Seek(0x1C, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(FileLength2));

            /*Stream look-up*/
            foreach (long Offset in MarkersStartList)
            {
                BWriter.Write(Convert.ToUInt32(Offset));
            }
        }

        private void ProgressBarReset(ProgressBar BarToReset)
        {
            if (BarToReset != null)
            {
                /*Update Progress Bar*/
                BarToReset.Invoke((MethodInvoker)delegate
                {
                    BarToReset.Value = 0;
                });
            }
        }

        private void ProgressBarUpdate(ProgressBar BarToUpdate, int ValueToAdd)
        {
            /*Update Progress Bar*/
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
                /*Update Progress Bar*/
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
