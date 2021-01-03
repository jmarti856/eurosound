using EuroSound_Application.SoundBanksEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        internal void WriteFileHeader(BinaryWriter BWriter, uint FileHashcode, ProgressBar Bar)
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

        internal void WriteFileSections(BinaryWriter BWriter, int NumberOfSamples, ProgressBar Bar)
        {
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, 9);

                /*--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--*/
                BWriter.Write(Convert.ToUInt32(0x800));
                ProgressBarUpdate(Bar, 1);
                /*--sfxlength[size of the first section, in bytes]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);

                /*--sampleinfostart[offset to the second section where the sample properties are stored]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
                /*--sampleinfolen[size of the second section, in bytes]--*/
                BWriter.Write(Convert.ToUInt32(NumberOfSamples));
                ProgressBarUpdate(Bar, 1);

                /*--specialsampleinfostart[unused and uses the same sample data offset as dummy for some reason]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
                /*--specialsampleinfolen[unused and set to zero]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);

                /*--sampledatastart[Offset that points to the beginning of the PCM data, where sound is actually stored]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);
                /*--sampledatalen[Size of the block, in bytes]--*/
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);

                BWriter.Seek(0x800, SeekOrigin.Begin);
                ProgressBarUpdate(Bar, 1);
            }
            catch
            {

            }
        }

        internal void WriteSFXSection(BinaryWriter BWriter, Dictionary<uint, EXSound> FinalSoundsDict, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            try
            {
                string Hashcode;
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, FinalSoundsDict.Count());

                /*--[SFX entry count in this soundbank]--*/
                BWriter.Write(Convert.ToUInt32(FinalSoundsDict.Count));

                /*--[SFX header]--*/
                foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
                {
                    Hashcode = Sound.Value.Hashcode.ToString("X8");
                    BWriter.Write(Convert.ToUInt32("0x00" + Hashcode.Substring(2), 16));
                    BWriter.Write(Convert.ToUInt32(BWriter.BaseStream.Position));
                }
                /*--[Linear array of sorted SFX headers laid out in this format]--*/
                foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
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

                    SetLabelText(LabelInfo, "WritingSFX: " + Sound.Value.DisplayName);

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
                    ProgressBarUpdate(Bar, 1);
                }
                /*--[Section length, current position - start position]--*/
                SFXlength = BWriter.BaseStream.Position - 2048;
            }
            catch
            {

            }
        }

        internal void WriteSampleInfoSection(BinaryWriter BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            try
            {
                ProgressBarReset(Bar);
                ProgressBarMaximum(Bar, FinalAudioDataDict.Count());

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

        internal void WriteSampleDataSection(BinaryWriter BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            try
            {
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

        internal void WriteFinalOffsets(BinaryWriter BWriter, ProgressBar Bar, Label LabelInfo)
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
                BWriter.Write(Convert.ToUInt32(WholeFileSize));
                ProgressBarUpdate(Bar, 1);

                /*--SFX Length--*/
                BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
                BWriter.Write(Convert.ToUInt32(SFXlength));
                ProgressBarUpdate(Bar, 1);

                /*--Sample info start--*/
                BWriter.BaseStream.Seek(0x18, SeekOrigin.Begin);
                BWriter.Write(Convert.ToUInt32(SampleInfoStartOffset));
                BWriter.Write(Convert.ToUInt32(SampleInfoLength));
                ProgressBarUpdate(Bar, 1);

                /*--Special sample info start--*/
                BWriter.BaseStream.Seek(0x20, SeekOrigin.Begin);
                BWriter.Write(Convert.ToUInt32(SampleDataStartOffset));
                BWriter.Write(Convert.ToUInt32(00000000));
                ProgressBarUpdate(Bar, 1);

                /*--Sample Data Start--*/
                BWriter.BaseStream.Seek(0x28, SeekOrigin.Begin);
                BWriter.Write(Convert.ToUInt32(SampleDataStartOffset));
                BWriter.Write(Convert.ToUInt32(SampleDataLength));
                ProgressBarUpdate(Bar, 1);

                //*===============================================================================================
                //* WRITE HASHCODE REAL OFFSETS
                //*===============================================================================================
                BWriter.Seek(0x804, SeekOrigin.Begin);
                foreach (long offset in HashcodeOffsets)
                {
                    BWriter.Seek(4, SeekOrigin.Current);
                    BWriter.Write(Convert.ToUInt32(offset));

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
                    BWriter.Write(Convert.ToUInt32(item));

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
                SetLabelText(LabelInfo, "Checking SFX: " + Sound.Value.DisplayName);
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

        private void AddPaddingBytes(int NumberOfBytes, BinaryWriter BWriter)
        {
            for (int i = 0; i < NumberOfBytes; i++)
            {
                BWriter.Write(Convert.ToSByte(0));
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