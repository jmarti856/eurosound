using EuroSound_Application.SoundBanksEditor;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.GenerateSoundBankSFX
{
    internal class GenerateSFXSoundBank
    {
        /*--[Global vars]--*/
        private List<long> HashcodeOffsets = new List<long>();
        private int ItemIndex;
        private List<long> SampleDataOffsets = new List<long>();
        private long SampleInfoStartOffset, SampleDataStartOffset;
        private long SFXlength, SampleInfoLength, SampleDataLength;

        private long WholeFileSize;
        /*--[Data lists]--*/

        internal void WriteFileHeader(BinaryStream BWriter, uint FileHashcode, ProgressBar Bar)
        {
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, 4);

                /*--magic[magic value]--*/
                BWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
                ProgressBarUpdate(Bar, 1);

                /*--hashc[Hashcode for the current soundbank without the section prefix]--*/
                BWriter.WriteUInt32(FileHashcode);
                ProgressBarUpdate(Bar, 1);

                /*--offst[Constant offset to the next section,]--*/
                BWriter.WriteUInt32(0xC9);
                ProgressBarUpdate(Bar, 1);

                /*--fulls[Size of the whole file, in bytes. Unused. ]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);
            }
            catch
            {

            }
        }

        internal void WriteFileSections(BinaryStream BWriter, int NumberOfSamples, ProgressBar Bar)
        {
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, 9);

                /*--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--*/
                BWriter.WriteUInt32(0x800);
                ProgressBarUpdate(Bar, 1);
                /*--sfxlength[size of the first section, in bytes]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);

                /*--sampleinfostart[offset to the second section where the sample properties are stored]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);
                /*--sampleinfolen[size of the second section, in bytes]--*/
                BWriter.WriteUInt32((uint)NumberOfSamples);
                ProgressBarUpdate(Bar, 1);

                /*--specialsampleinfostart[unused and uses the same sample data offset as dummy for some reason]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);
                /*--specialsampleinfolen[unused and set to zero]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);

                /*--sampledatastart[Offset that points to the beginning of the PCM data, where sound is actually stored]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);
                /*--sampledatalen[Size of the block, in bytes]--*/
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);

                BWriter.Seek(0x800, SeekOrigin.Begin);
                ProgressBarUpdate(Bar, 1);
            }
            catch
            {

            }
        }

        internal void WriteSFXSection(BinaryStream BWriter, Dictionary<uint, EXSound> FinalSoundsDict, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            try
            {
                string Hashcode;
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, FinalSoundsDict.Count());

                /*--[SFX entry count in this soundbank]--*/
                BWriter.WriteUInt32((uint)FinalSoundsDict.Count);

                /*--[SFX header]--*/
                foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
                {
                    Hashcode = Sound.Value.Hashcode.ToString("X8");
                    BWriter.WriteUInt32(Convert.ToUInt32("0x00" + Hashcode.Substring(2), 16));
                    BWriter.WriteUInt32((uint)BWriter.BaseStream.Position);
                }
                /*--[Linear array of sorted SFX headers laid out in this format]--*/
                foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
                {
                    /*--[Add hashcode offset to the dictionary]--*/
                    HashcodeOffsets.Add(BWriter.BaseStream.Position - 2048);

                    /*--[Write data]--*/
                    BWriter.WriteInt16(Sound.Value.DuckerLenght);
                    BWriter.WriteInt16(Sound.Value.MinDelay);
                    BWriter.WriteInt16(Sound.Value.MaxDelay);
                    BWriter.WriteInt16(Sound.Value.InnerRadiusReal);
                    BWriter.WriteInt16(Sound.Value.OuterRadiusReal);
                    BWriter.WriteSByte(Sound.Value.ReverbSend);
                    BWriter.WriteSByte(Sound.Value.TrackingType);
                    BWriter.WriteSByte(Sound.Value.MaxVoices);
                    BWriter.WriteSByte(Sound.Value.Priority);
                    BWriter.WriteSByte(Sound.Value.Ducker);
                    BWriter.WriteSByte(Sound.Value.MasterVolume);
                    BWriter.WriteUInt16(Sound.Value.Flags);

                    BWriter.WriteInt16((short)Sound.Value.Samples.Count);

                    SetLabelText(LabelInfo, "WritingSFX Data");

                    foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                    {
                        /*--[FILE REFERENCE]--*/
                        if (Sample.Value.FileRef < 0)
                        {
                            BWriter.WriteInt16(Sample.Value.FileRef);
                        }
                        else
                        {
                            ItemIndex = GetSteamIndexInSoundbank(Sample.Value.ComboboxSelectedAudio, FinalAudioDataDict, Sample.Value, Sound.Value.Flags);
                            if (ItemIndex >= 0)
                            {
                                BWriter.WriteInt16((short)ItemIndex);
                            }
                        }

                        /*--[Sample Data]--*/
                        BWriter.WriteInt16(Sample.Value.PitchOffset);
                        BWriter.WriteInt16(Sample.Value.RandomPitchOffset);
                        BWriter.WriteSByte(Sample.Value.BaseVolume);
                        BWriter.WriteSByte(Sample.Value.RandomVolumeOffset);
                        BWriter.WriteSByte(Sample.Value.Pan);
                        BWriter.WriteSByte(Sample.Value.RandomPan);

                        /*--[Aligment Padding]--*/
                        AddPaddingBytes(2, BWriter);
                    }
                    ProgressBarUpdate(Bar, 1);
                }
                /*--[Section length, current position - start position]--*/
                SFXlength = BWriter.BaseStream.Position - 2048;
            }
            catch
            {

            }
        }

        internal void WriteSampleInfoSection(BinaryStream BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            try
            {
                long AlignOffset;

                /*Align Bytes*/
                AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
                BWriter.Seek(AlignOffset, SeekOrigin.Current);
                BWriter.Align(16);


                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, FinalAudioDataDict.Count());

                /*--[Start section offset]--*/
                SampleInfoStartOffset = (BWriter.BaseStream.Position);

                /*--[Write total number of samples]--*/
                BWriter.WriteUInt32((uint)FinalAudioDataDict.Keys.Count);

                foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
                {
                    /*--[Write data]--*/
                    BWriter.WriteUInt32(entry.Value.Flags);
                    BWriter.WriteUInt32(00000000);
                    BWriter.WriteUInt32(entry.Value.DataSize);
                    BWriter.WriteUInt32(entry.Value.Frequency);
                    BWriter.WriteUInt32(entry.Value.RealSize);
                    BWriter.WriteUInt32(entry.Value.Channels);
                    BWriter.WriteUInt32(entry.Value.Bits);
                    BWriter.WriteUInt32(entry.Value.PSIsample);
                    BWriter.WriteUInt32(entry.Value.LoopOffset);
                    BWriter.WriteUInt32(entry.Value.Duration);

                    ProgressBarUpdate(Bar, 1);
                    SetLabelText(LabelInfo, "WritingSampleInfo: " + entry.Key);
                }

                /*--[Section length, current position - start position]--*/
                SampleInfoLength = BWriter.BaseStream.Position - SampleInfoStartOffset;
            }
            catch
            {

            }
        }

        internal void WriteSampleDataSection(BinaryStream BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            try
            {
                long AlignOffset;

                /*Align Bytes*/
                AlignOffset = (BWriter.BaseStream.Position + 3048) & (3048 - 1);
                BWriter.Seek(AlignOffset, SeekOrigin.Current);
                BWriter.Align(16);

                /*Update UI*/
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, FinalAudioDataDict.Count());

                /*--[Start section offset]--*/
                SampleDataStartOffset = (BWriter.BaseStream.Position);
                foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
                {
                    /*--Add Sample data offset to the list--*/
                    SampleDataOffsets.Add(BWriter.BaseStream.Position - SampleDataStartOffset);

                    /*--Write PCM Data--*/
                    BWriter.Write(entry.Value.PCMdata);

                    ProgressBarUpdate(Bar, 1);
                    SetLabelText(LabelInfo, "WrittingSampleData: " + entry.Key);
                }

                /*--Section length, current position - start position--*/
                SampleDataLength = BWriter.BaseStream.Position - SampleDataStartOffset;

                /*Get total file size*/
                WholeFileSize = BWriter.BaseStream.Position;
            }
            catch
            {

            }
        }

        internal void WriteFinalOffsets(BinaryStream BWriter, ProgressBar Bar, Label LabelInfo)
        {
            //*===============================================================================================
            //* WRITE FINAL HEADER INFO
            //*===============================================================================================
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, (HashcodeOffsets.Count() + SampleDataOffsets.Count() + 5));

                /*--Size of the whole file--*/
                BWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)WholeFileSize);
                ProgressBarUpdate(Bar, 1);

                /*--SFX Length--*/
                BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)SFXlength);
                ProgressBarUpdate(Bar, 1);

                /*--Sample info start--*/
                BWriter.BaseStream.Seek(0x18, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)SampleInfoStartOffset);
                BWriter.WriteUInt32((uint)SampleInfoLength);
                ProgressBarUpdate(Bar, 1);

                /*--Special sample info start--*/
                BWriter.BaseStream.Seek(0x20, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)SampleDataStartOffset);
                BWriter.WriteUInt32(00000000);
                ProgressBarUpdate(Bar, 1);

                /*--Sample Data Start--*/
                BWriter.BaseStream.Seek(0x28, SeekOrigin.Begin);
                BWriter.WriteUInt32((uint)SampleDataStartOffset);
                BWriter.WriteUInt32((uint)SampleDataLength);
                ProgressBarUpdate(Bar, 1);

                //*===============================================================================================
                //* WRITE HASHCODE REAL OFFSETS
                //*===============================================================================================
                BWriter.Seek(0x804, SeekOrigin.Begin);
                foreach (long offset in HashcodeOffsets)
                {
                    BWriter.Seek(4, SeekOrigin.Current);
                    BWriter.WriteUInt32((uint)offset);

                    ProgressBarUpdate(Bar, 1);
                    SetLabelText(LabelInfo, "WrittingFinalOffsets: " + offset);
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
                    BWriter.WriteUInt32((uint)item);

                    /*--Skip other properties--*/
                    BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 32, SeekOrigin.Begin);

                    ProgressBarUpdate(Bar, 1);
                    SetLabelText(LabelInfo, "WrittingFinalOffsets: " + item);
                }
            }
            catch
            {

            }
        }

        internal Dictionary<uint, EXSound> GetFinalSoundsDictionary(Dictionary<uint, EXSound> SoundsList, ProgressBar Bar, Label LabelInfo)
        {
            Dictionary<uint, EXSound> FinalSoundsDict = new Dictionary<uint, EXSound>();

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, SoundsList.Count());

            //Discard SFXs that has checked as "no output"
            foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
            {
                if (Sound.Value.OutputThisSound)
                {
                    FinalSoundsDict.Add(Sound.Key, Sound.Value);
                }
                SetLabelText(LabelInfo, "Checking SFX Data");
                ProgressBarUpdate(Bar, 1);
            }

            return FinalSoundsDict;
        }

        internal Dictionary<string, EXAudio> GetFinalAudioDictionary(List<string> UsedAudios, Dictionary<string, EXAudio> AudiosList, ProgressBar Bar)
        {
            Dictionary<string, EXAudio> FinalAudioDataDict = new Dictionary<string, EXAudio>();

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, UsedAudios.Count());

            //Add data
            foreach (string Key in UsedAudios)
            {
                KeyValuePair<string, EXAudio> ObjectToAdd;
                if (AudiosList.ContainsKey(Key))
                {
                    ObjectToAdd = new KeyValuePair<string, EXAudio>(Key, AudiosList[Key]);
                    FinalAudioDataDict.Add(ObjectToAdd.Key, ObjectToAdd.Value);
                }
                ProgressBarUpdate(Bar, 1);
            }

            return FinalAudioDataDict;
        }

        private void AddPaddingBytes(int NumberOfBytes, BinaryStream BWriter)
        {
            for (int i = 0; i < NumberOfBytes; i++)
            {
                BWriter.WriteSByte(0);
            }
        }

        private uint GetHashcodeWithoutSection(uint Hashcode)
        {
            uint FinalHashcode = Hashcode - 0x1A000000;
            return FinalHashcode;
        }

        private int GetSteamIndexInSoundbank(string SampleName, Dictionary<string, EXAudio> AudioDataList, EXSample Sample, int Flags)
        {
            int index;

            if (EXSoundbanksFunctions.SubSFXFlagChecked(Flags))
            {
                index = (int)GetHashcodeWithoutSection(Sample.HashcodeSubSFX);
            }
            else
            {
                index = AudioDataList.Keys.ToList().IndexOf(SampleName);
            }

            return index;
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
                //Thread.Sleep(1);
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