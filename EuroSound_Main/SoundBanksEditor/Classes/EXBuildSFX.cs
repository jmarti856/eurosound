using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EuroSound_Application
{
    internal class EXBuildSFX
    {
        /*--[Global vars]--*/
        private List<long> HashcodeOffsets = new List<long>();
        private int ItemIndex;
        private List<long> SampleDataOffsets = new List<long>();
        private long SampleInfoStartOffset, SampleDataStartOffset;
        private long SFXlength, SampleInfoLength, SampleDataLength;

        private long WholeFileSize;
        /*--[Data lists]--*/
        public void WriteFileHeader(BinaryWriter BWriter, string FileHashcode)
        {
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            /*--magic[magic value]--*/
            BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));

            /*--hashc[Hashcode for the current soundbank without the section prefix]--*/
            BWriter.Write(Convert.ToUInt32(Convert.ToInt32(FileHashcode, 16)));

            /*--offst[Constant offset to the next section,]--*/
            BWriter.Write(Convert.ToUInt32(0xC9));

            /*--fulls[Size of the whole file, in bytes. Unused. ]--*/
            BWriter.Write(Convert.ToUInt32(00000000));
        }

        public void WriteFileSections(BinaryWriter BWriter, int NumberOfSamples)
        {
            /*--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--*/
            BWriter.Write(Convert.ToUInt32(0x800));
            /*--sfxlength[size of the first section, in bytes]--*/
            BWriter.Write(Convert.ToUInt32(00000000));

            /*--sampleinfostart[offset to the second section where the sample properties are stored]--*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*--sampleinfolen[size of the second section, in bytes]--*/
            BWriter.Write(Convert.ToUInt32(NumberOfSamples));

            /*--specialsampleinfostart[unused and uses the same sample data offset as dummy for some reason]--*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*--specialsampleinfolen[unused and set to zero]--*/
            BWriter.Write(Convert.ToUInt32(00000000));

            /*--sampledatastart[Offset that points to the beginning of the PCM data, where sound is actually stored]--*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*--sampledatalen[Size of the block, in bytes]--*/
            BWriter.Write(Convert.ToUInt32(00000000));

            BWriter.Seek(0x800, SeekOrigin.Begin);
        }

        public void WriteFinalOffsets(BinaryWriter BWriter)
        {
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
            BWriter.BaseStream.Seek(SampleInfoStartOffset + 4, SeekOrigin.Begin);
            foreach (long item in SampleDataOffsets)
            {
                /*--Skip numsamples and flags--*/
                BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 4, SeekOrigin.Begin);

                /*--Calculate Relative Offset--*/
                BWriter.Write(Convert.ToUInt32(item));

                /*--Skip other properties--*/
                BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 32, SeekOrigin.Begin);
            }
        }

        public void WriteSampleDataSection(BinaryWriter BWriter, Dictionary<string, EXAudio> FinalAudioDataDict)
        {
            /*--[Start section offset]--*/
            SampleDataStartOffset = (BWriter.BaseStream.Position);
            foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
            {
                /*--Add Sample data offset to the list--*/
                SampleDataOffsets.Add(BWriter.BaseStream.Position - SampleDataStartOffset);

                /*--Write PCM Data--*/
                BWriter.Write(entry.Value.PCMdata);
            }

            /*--Section length, current position - start position--*/
            SampleDataLength = BWriter.BaseStream.Position - SampleDataStartOffset;

            /*Get total file size*/
            WholeFileSize = BWriter.BaseStream.Position;
        }

        public void WriteSampleInfoSection(BinaryWriter BWriter, Dictionary<string, EXAudio> FinalAudioDataDict)
        {
            /*--[Start section offset]--*/
            SampleInfoStartOffset = (BWriter.BaseStream.Position);

            /*--[Write total number of samples]--*/
            BWriter.Write(Convert.ToUInt32(FinalAudioDataDict.Keys.Count));

            foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
            {
                /*--[Write data]--*/
                BWriter.Write(Convert.ToUInt32(entry.Value.Flags));
                BWriter.Write(Convert.ToUInt32(00000000));
                BWriter.Write(Convert.ToUInt32(entry.Value.DataSize));
                BWriter.Write(Convert.ToUInt32(entry.Value.Frequency));
                BWriter.Write(Convert.ToUInt32(entry.Value.RealSize));
                BWriter.Write(Convert.ToUInt32(entry.Value.Channels));
                BWriter.Write(Convert.ToUInt32(entry.Value.Bits));
                BWriter.Write(Convert.ToUInt32(entry.Value.PSIsample));
                BWriter.Write(Convert.ToUInt32(entry.Value.LoopOffset));
                BWriter.Write(Convert.ToUInt32(entry.Value.Duration));
            }

            /*--[Section length, current position - start position]--*/
            SampleInfoLength = BWriter.BaseStream.Position - SampleInfoStartOffset;
        }

        public void WriteSFXSection(BinaryWriter BWriter, Dictionary<int, EXSound> FinalSoundsDict, Dictionary<string, EXAudio> FinalAudioDataDict)
        {
            /*--[SFX entry count in this soundbank]--*/
            BWriter.Write(Convert.ToUInt32(FinalSoundsDict.Count));

            /*--[SFX header]--*/
            foreach (KeyValuePair<int, EXSound> Sound in FinalSoundsDict)
            {
                BWriter.Write(Convert.ToUInt32("0x00" + Sound.Value.Hashcode.Substring(4), 16));
                BWriter.Write(Convert.ToUInt32(BWriter.BaseStream.Position));
            }
            /*--[Linear array of sorted SFX headers laid out in this format]--*/
            foreach (KeyValuePair<int, EXSound> Sound in FinalSoundsDict)
            {
                /*--[Add hashcode offset to the dictionary]--*/
                HashcodeOffsets.Add(BWriter.BaseStream.Position - 2048);

                /*--[Write data]--*/
                BWriter.Write(Convert.ToInt16(Sound.Value.DuckerLenght));
                BWriter.Write(Convert.ToInt16(Sound.Value.MinDelay));
                BWriter.Write(Convert.ToInt16(Sound.Value.MaxDelay));
                BWriter.Write(Convert.ToInt16(Sound.Value.InnerRadiusReal));
                BWriter.Write(Convert.ToInt16(Sound.Value.OuterRadiusReal));
                BWriter.Write(Convert.ToSByte(Sound.Value.ReverbSend));
                BWriter.Write(Convert.ToSByte(Sound.Value.TrackingType));
                BWriter.Write(Convert.ToSByte(Sound.Value.MaxVoices));
                BWriter.Write(Convert.ToSByte(Sound.Value.Priority));
                BWriter.Write(Convert.ToSByte(Sound.Value.Ducker));
                BWriter.Write(Convert.ToSByte(Sound.Value.MasterVolume));
                BWriter.Write(Convert.ToUInt16(Sound.Value.Flags));

                BWriter.Write(Convert.ToInt16(Sound.Value.Samples.Count));

                foreach (EXSample Sample in Sound.Value.Samples)
                {
                    /*--[FILE REFERENCE]--*/
                    if (Sample.FileRef < 0)
                    {
                        BWriter.Write(Convert.ToInt16(Sample.FileRef));
                    }
                    else
                    {
                        ItemIndex = GetSteamIndexInSoundbank(Sample.ComboboxSelectedAudio, FinalAudioDataDict, Sample, Sound.Value.Flags);
                        if (ItemIndex >= 0)
                        {
                            BWriter.Write(Convert.ToInt16(ItemIndex));
                        }
                    }

                    /*--[Sample Data]--*/
                    BWriter.Write(Convert.ToInt16(Sample.PitchOffset));
                    BWriter.Write(Convert.ToInt16(Sample.RandomPitchOffset));
                    BWriter.Write(Convert.ToSByte(Sample.BaseVolume));
                    BWriter.Write(Convert.ToSByte(Sample.RandomVolumeOffset));
                    BWriter.Write(Convert.ToSByte(Sample.Pan));
                    BWriter.Write(Convert.ToSByte(Sample.RandomPan));

                    /*--[Aligment Padding]--*/
                    AddPaddingBytes(2, BWriter);
                }
            }
            /*--[Section length, current position - start position]--*/
            SFXlength = BWriter.BaseStream.Position - 2048;
        }
        private void AddPaddingBytes(int NumberOfBytes, BinaryWriter BWriter)
        {
            for (int i = 0; i < NumberOfBytes; i++)
            {
                BWriter.Write(Convert.ToSByte(0));
            }
        }

        private string GetHashcodeWithoutSection(string Hashcode)
        {
            return "0x00" + Hashcode.Substring(4);
        }

        private int GetSteamIndexInSoundbank(string SampleName, Dictionary<string, EXAudio> AudioDataList, EXSample Sample, int Flags)
        {
            int index;

            if (EXObjectsFunctions.SubSFXFlagChecked(Flags))
            {
                index = Convert.ToInt32(GetHashcodeWithoutSection(Sample.HashcodeSubSFX), 16);
            }
            else
            {
                index = AudioDataList.Keys.ToList().IndexOf(SampleName);
            }

            return index;
        }
    }
}