using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_SB_Editor
{
    public static class EXBuildSFX
    {
        public static void ExportContentToSFX(List<EXSound> SoundsList, string SavePath)
        {
            long SampleInfoStartOffset, SampleDataStartOffset;
            long SFXlength, SampleInfoLength, SampleDataLength;

            long WholeFileSize;

            List<long> HashcodeOffsets = new List<long>();
            List<long> SampleDataOffsets = new List<long>();
            List<string> SampleInfoTable = GetSampleInfoTable(SoundsList);

            BinaryWriter BWriter = new BinaryWriter(File.Open(SavePath, FileMode.Create, FileAccess.Write), Encoding.ASCII);
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            /*--magic[magic value]--*/
            BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));

            /*--hashc[Hashcode for the current soundbank without the section prefix]--*/
            BWriter.Write(Convert.ToUInt32(Convert.ToInt32(EXFile.Hashcode, 16)));

            /*--offst[Constant offset to the next section,]--*/
            BWriter.Write(Convert.ToInt32(0xC9));

            /*--fulls[Size of the whole file, in bytes. Unused. ]--*/
            BWriter.Write(Convert.ToInt32(00000000));

            //*===============================================================================================
            //* SECTIONS
            //*===============================================================================================
            /*--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--*/
            BWriter.Write(Convert.ToInt32(0x800));
            /*--sfxlength[size of the first section, in bytes]--*/
            BWriter.Write(Convert.ToUInt32(00000000));

            /*--sampleinfostart[offset to the second section where the sample properties are stored]--*/
            BWriter.Write(Convert.ToInt32(00000000));
            /*--sampleinfolen[size of the second section, in bytes]--*/
            BWriter.Write(Convert.ToInt32(CountNumberOfSamples(SoundsList)));

            /*--specialsampleinfostart[unused and uses the same sample data offset as dummy for some reason]--*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*--specialsampleinfolen[unused and set to zero]--*/
            BWriter.Write(Convert.ToUInt32(00000000));

            /*--sampledatastart[Offset that points to the beginning of the PCM data, where sound is actually stored]--*/
            BWriter.Write(Convert.ToInt32(00000000));
            /*--sampledatalen[Size of the block, in bytes]--*/
            BWriter.Write(Convert.ToInt32(00000000));

            BWriter.Seek(0x800, SeekOrigin.Begin);
            //*===============================================================================================
            //* SECTION SFX elements
            //*===============================================================================================
            /*--SFX entry count in this soundbank--*/
            BWriter.Write(SoundsList.Count);

            /*--SFX header--*/
            foreach (EXSound Sound in SoundsList)
            {
                BWriter.Write(Convert.ToUInt32("0x00" + Sound.Hashcode.Substring(4), 16));
                BWriter.Write(Convert.ToUInt32(BWriter.BaseStream.Position));
            }
            /*--Linear array of sorted SFX headers laid out in this format--*/
            foreach (EXSound Sound in SoundsList)
            {
                /*--Add hashcode offset to the dictionary--*/
                HashcodeOffsets.Add(BWriter.BaseStream.Position);

                /*--Write data--*/
                BWriter.Write(Convert.ToInt16(Sound.DuckerLenght));
                BWriter.Write(Convert.ToInt16(Sound.MinDelay));
                BWriter.Write(Convert.ToInt16(Sound.MaxDelay));
                BWriter.Write(Convert.ToInt16(Sound.InnerRadiusReal));
                BWriter.Write(Convert.ToInt16(Sound.OuterRadiusReal));
                BWriter.Write(Convert.ToSByte(Sound.ReverbSend));
                BWriter.Write(Convert.ToSByte(Sound.TrackingType));
                BWriter.Write(Convert.ToSByte(Sound.MaxVoices));
                BWriter.Write(Convert.ToSByte(Sound.Priority));
                BWriter.Write(Convert.ToSByte(Sound.Ducker));
                BWriter.Write(Convert.ToSByte(Sound.MasterVolume));
                BWriter.Write(Convert.ToUInt16(Sound.Flags));

                BWriter.Write(Convert.ToInt16(Sound.Samples.Count));

                foreach (EXSample Sample in Sound.Samples)
                {
                    /*--[FILE REFERENCE]--*/
                    BWriter.Write(Convert.ToInt16(GetSteamIndexInSoundbank(Sample.Name, SampleInfoTable)));

                    /*Sample Data*/
                    BWriter.Write(Convert.ToInt16(Sample.PitchOffset));
                    BWriter.Write(Convert.ToInt16(Sample.RandomPitchOffset));
                    BWriter.Write(Convert.ToInt16(Sample.BaseVolume));
                    BWriter.Write(Convert.ToSByte(Sample.RandomVolumeOffset));
                    BWriter.Write(Convert.ToSByte(Sample.Pan));
                    BWriter.Write(Convert.ToSByte(Sample.RandomPan));

                    /*Aligment Padding*/
                    BWriter.Write(Convert.ToSByte(0));
                    BWriter.Write(Convert.ToSByte(0));
                }
            }
            /*--Section length, current position - start position--*/
            SFXlength = BWriter.BaseStream.Position - 2048;

            //*===============================================================================================
            //* SECTION Sample info elements
            //*===============================================================================================
            /*Start section offset*/
            SampleInfoStartOffset = (BWriter.BaseStream.Position);

            /*Write total number of samples*/
            BWriter.Write(Convert.ToInt16(CountNumberOfSamples(SoundsList)));

            foreach (EXSound Sound in SoundsList)
            {
                for (int i = 0; i < Sound.Samples.Count; i++)
                {
                    /*Write data*/
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.Flags));
                    BWriter.Write(Convert.ToUInt32(BWriter.BaseStream.Position));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.DataSize));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.Frequency));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.RealSize));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.Channels));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.Bits));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.PSIsample));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.LoopOffset));
                    BWriter.Write(Convert.ToUInt32(Sound.Samples[i].Audio.Duration));
                }
            }

            /*--Section length, current position - start position--*/
            SampleInfoLength = BWriter.BaseStream.Position - SampleInfoStartOffset;

            //*===============================================================================================
            //* SECTION Sample data
            //*===============================================================================================
            /*Start section offset*/
            SampleDataStartOffset = (BWriter.BaseStream.Position);
            foreach (EXSound Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Samples)
                {
                    /*--Add Sample data offset to the list--*/
                    SampleDataOffsets.Add(BWriter.BaseStream.Position);

                    /*--Write PCM Data--*/
                    BWriter.Write(Sample.Audio.PCMdata);

                    /*--Aligment Padding--*/
                    BWriter.Write(Convert.ToSByte(0));
                    BWriter.Write(Convert.ToSByte(0));
                }
            }
            /*--Section length, current position - start position--*/
            SampleDataLength = BWriter.BaseStream.Position - SampleDataStartOffset;

            /*Get total file size*/
            WholeFileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* WRITE FINAL HEADER INFO
            //*===============================================================================================
            /*--Size of the whole file--*/
            BWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(WholeFileSize));

            /*--SFX Length--*/
            BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(SFXlength));

            /*--Sample info start--*/
            BWriter.BaseStream.Seek(0x18, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(SampleInfoStartOffset));
            BWriter.Write(Convert.ToUInt32(SampleInfoLength));

            /*--Special sample info start--*/
            BWriter.BaseStream.Seek(0x20, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(SampleDataStartOffset));
            BWriter.Write(Convert.ToUInt32(00000000));

            /*--Sample Data Start--*/
            BWriter.BaseStream.Seek(0x28, SeekOrigin.Begin);
            BWriter.Write(Convert.ToUInt32(SampleDataStartOffset));
            BWriter.Write(Convert.ToUInt32(SampleDataLength));

            //*===============================================================================================
            //* WRITE HASHCODE REAL OFFSETS
            //*===============================================================================================
            BWriter.Seek(0x804, SeekOrigin.Begin);
            foreach (long offset in HashcodeOffsets)
            {
                BWriter.Seek(4, SeekOrigin.Current);
                BWriter.Write(Convert.ToUInt32(offset));
            }

            //*===============================================================================================
            //* WRITE FINAL OFFSETS TO SAMPLE DATA
            //*===============================================================================================
            BWriter.BaseStream.Seek(SampleInfoStartOffset, SeekOrigin.Begin);
            long Offset;
            foreach (long item in SampleDataOffsets)
            {
                /*--Skip numsamples and flags--*/
                Offset = BWriter.BaseStream.Position + 6;
                BWriter.BaseStream.Seek(Offset, SeekOrigin.Begin);

                /*--Calculate Relative Offset--*/
                BWriter.Write(Convert.ToUInt32(item - Offset));

                /*--Skip other properties--*/
                Offset = BWriter.BaseStream.Position + 34;
                BWriter.BaseStream.Seek(Offset, SeekOrigin.Begin);
            }
            BWriter.Close();
        }

        private static int CountNumberOfSamples(List<EXSound> SoundsList)
        {
            int Counter = 0;
            foreach (EXSound Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Samples)
                {
                    if (!Sample.Audio.IsEmpty())
                    {
                        Counter++;
                    }
                }
            }
            return Counter;
        }

        private static List<string> GetSampleInfoTable(List<EXSound> SoundsList)
        {
            List<string> SampleInfoTable = new List<string>();

            foreach (EXSound Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Samples)
                {
                    if (Sample.IsStreamed == false)
                    {
                        SampleInfoTable.Add(Sample.Name);
                    }
                }
            }

            return SampleInfoTable;
        }

        private static int GetSteamIndexInSoundbank(string SampleName, List<string> SampleInfoTable)
        {
            return SampleInfoTable.IndexOf(SampleName);
        }
    }
}
