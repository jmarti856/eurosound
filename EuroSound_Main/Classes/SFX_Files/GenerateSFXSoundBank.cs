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
        //--[Global vars]--
        private const uint SFXStartSection = 0x800;
        private int ItemIndex;
        private long WholeFileSize;
        private long SampleInfoStartOffset, SampleDataStartOffset;
        private long SFXlength, SampleInfoLength, SampleDataLength;
        private long[,] HashcodeOffsetM;
        private List<long> SampleDataOffsets = new List<long>();

        internal void WriteFileHeader(BinaryStream BWriter, uint FileHashcode, ProgressBar Bar)
        {
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
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

        internal void WriteFileSections(BinaryStream BWriter, int NumberOfSamples, ProgressBar Bar)
        {
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, 9);

            //--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--
            BWriter.WriteUInt32(SFXStartSection);
            ProgressBarUpdate(Bar, 1);
            //--sfxlength[size of the first section, in bytes]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);

            //--sampleinfostart[offset to the second section where the sample properties are stored]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);
            //--sampleinfolen[size of the second section, in bytes]--
            BWriter.WriteUInt32((uint)NumberOfSamples);
            ProgressBarUpdate(Bar, 1);

            //--specialsampleinfostart[unused and uses the same sample data offset as dummy for some reason]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);
            //--specialsampleinfolen[unused and set to zero]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);

            //--sampledatastart[Offset that points to the beginning of the PCM data, where sound is actually stored]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);
            //--sampledatalen[Size of the block, in bytes]--
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);

            BWriter.Seek(SFXStartSection, SeekOrigin.Begin);
            ProgressBarUpdate(Bar, 1);
        }

        internal void WriteSFXSection(BinaryStream BWriter, Dictionary<uint, EXSound> FinalSoundsDict, Dictionary<string, EXAudio> FinalAudioDataDict, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar, Label LabelInfo)
        {
            int index = 0;
            bool DebugSFXElements;

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, FinalSoundsDict.Count());

            //CheckFlag SFX Elements is checked
            DebugSFXElements = Convert.ToBoolean((DebugFlags >> 1) & 1);

            //--[SFX entry count in this soundbank]--
            BWriter.WriteUInt32((uint)FinalSoundsDict.Count);

            //--[SFX header]--
            HashcodeOffsetM = new long[FinalSoundsDict.Keys.Count, 2];
            foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
            {
                BWriter.WriteUInt32(Sound.Value.Hashcode - 0x1A000000);
                BWriter.WriteUInt32((uint)BWriter.BaseStream.Position);

                HashcodeOffsetM[index, 0] = Convert.ToUInt32(Sound.Value.Hashcode.ToString("X8"), 16);
                index++;
            }

            index = 0;

            if (DebugSFXElements)
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// SFX Elements");
                DebugFile.WriteLine(new String('/', 70) + "\n");
            }

            //--[Align Bytes]--
            BWriter.Align(4, true);

            //--[Linear array of sorted SFX headers laid out in this format]--
            foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
            {
                //--[Add hashcode offset to the dictionary]--
                HashcodeOffsetM[index, 1] = BWriter.BaseStream.Position - SFXStartSection;

                //--[Write data]--
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

                //--[Write Debug File]--
                if (DebugSFXElements)
                {
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine("// SFX HashCode");
                    DebugFile.WriteLine("\t0x" + Sound.Value.Hashcode.ToString("X8") + " -> " + (Sound.Value.Hashcode - 0x1A000000).ToString("X8"));
                    DebugFile.WriteLine("// DuckerLenght");
                    DebugFile.WriteLine("\t" + Sound.Value.DuckerLenght);
                    DebugFile.WriteLine("// MinDelay");
                    DebugFile.WriteLine("\t" + Sound.Value.MinDelay);
                    DebugFile.WriteLine("// MaxDelay");
                    DebugFile.WriteLine("\t" + Sound.Value.MaxDelay);
                    DebugFile.WriteLine("// InnerRadiusReal");
                    DebugFile.WriteLine("\t" + Sound.Value.InnerRadiusReal);
                    DebugFile.WriteLine("// OuterRadiusReal");
                    DebugFile.WriteLine("\t" + Sound.Value.OuterRadiusReal);
                    DebugFile.WriteLine("// ReverbSend");
                    DebugFile.WriteLine("\t" + Sound.Value.ReverbSend);
                    DebugFile.WriteLine("// TrackingType");
                    DebugFile.WriteLine("\t" + Sound.Value.TrackingType);
                    DebugFile.WriteLine("// MaxVoices");
                    DebugFile.WriteLine("\t" + Sound.Value.MaxVoices);
                    DebugFile.WriteLine("// Priority");
                    DebugFile.WriteLine("\t" + Sound.Value.Priority);
                    DebugFile.WriteLine("// Ducker");
                    DebugFile.WriteLine("\t" + Sound.Value.Ducker);
                    DebugFile.WriteLine("// MasterVolume");
                    DebugFile.WriteLine("\t" + Sound.Value.MasterVolume);
                    DebugFile.WriteLine("// Flags");
                    DebugFile.WriteLine("\t" + Sound.Value.Flags);
                    DebugFile.WriteLine("// Samples Count");
                    DebugFile.WriteLine("\t" + (short)Sound.Value.Samples.Count + "\n");
                }

                foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                {
                    //--[FILE REFERENCE]--
                    if (Sample.Value.FileRef < 0)
                    {
                        BWriter.WriteInt16(Sample.Value.FileRef);

                        //--[Write Debug File]--
                        if (DebugSFXElements)
                        {
                            DebugFile.WriteLine("// File Reference");
                            DebugFile.WriteLine("\t" + Sample.Value.FileRef);
                        }
                    }
                    else
                    {
                        ItemIndex = GetSteamIndexInSoundbank(Sample.Value.ComboboxSelectedAudio, FinalAudioDataDict, Sample.Value, Sound.Value.Flags);
                        if (ItemIndex >= 0)
                        {
                            BWriter.WriteInt16((short)ItemIndex);

                            //--[Write Debug File]--
                            if (DebugSFXElements)
                            {
                                DebugFile.WriteLine("// File Reference");
                                DebugFile.WriteLine("\t" + ItemIndex);
                            }
                        }
                    }

                    //--[Sample Data]--
                    BWriter.WriteInt16(Sample.Value.PitchOffset);
                    BWriter.WriteInt16(Sample.Value.RandomPitchOffset);
                    BWriter.WriteSByte(Sample.Value.BaseVolume);
                    BWriter.WriteSByte(Sample.Value.RandomVolumeOffset);
                    BWriter.WriteSByte(Sample.Value.Pan);
                    BWriter.WriteSByte(Sample.Value.RandomPan);

                    //--[Aligment Padding]--
                    AddPaddingBytes(2, BWriter);

                    //--[Write Debug File]--
                    if (DebugSFXElements)
                    {
                        DebugFile.WriteLine("// PitchOffset");
                        DebugFile.WriteLine("\t" + Sample.Value.PitchOffset);
                        DebugFile.WriteLine("// RandomPitchOffset");
                        DebugFile.WriteLine("\t" + Sample.Value.RandomPitchOffset);
                        DebugFile.WriteLine("// BaseVolume");
                        DebugFile.WriteLine("\t" + Sample.Value.BaseVolume);
                        DebugFile.WriteLine("// RandomVolumeOffset");
                        DebugFile.WriteLine("\t" + Sample.Value.RandomVolumeOffset);
                        DebugFile.WriteLine("// Pan");
                        DebugFile.WriteLine("\t" + Sample.Value.Pan);
                        DebugFile.WriteLine("// RandomPan");
                        DebugFile.WriteLine("\t" + Sample.Value.RandomPan + "\n");
                    }
                }
                ProgressBarUpdate(Bar, 1);
                index++;
            }

            //--[Section length, current position - start position]--
            SFXlength = BWriter.BaseStream.Position - SFXStartSection;
        }

        internal void WriteSampleInfoSection(BinaryStream BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar, Label LabelInfo)
        {
            bool DebugSampleInfo;

            //CheckFlag Sample Info is checked
            DebugSampleInfo = Convert.ToBoolean((DebugFlags >> 2) & 1);

            //--[Align Bytes]--
            BWriter.Align(16, true);

            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, FinalAudioDataDict.Count());

            //--[Start section offset]--
            SampleInfoStartOffset = (BWriter.BaseStream.Position);

            //--[Debug File]--
            if (DebugSampleInfo)
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// Sample Info Section");
                DebugFile.WriteLine(new String('/', 70) + "\n");
                DebugFile.WriteLine("// Number Of Samples");
                DebugFile.WriteLine("\t" + FinalAudioDataDict.Keys.Count + "\n");
            }

            //--[Write total number of samples]--
            BWriter.WriteUInt32((uint)FinalAudioDataDict.Keys.Count);

            foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
            {
                //--[Write data]--
                BWriter.WriteUInt32(entry.Value.Flags);
                BWriter.WriteUInt32(00000000);
                BWriter.WriteUInt32(entry.Value.DataSize);
                BWriter.WriteUInt32(entry.Value.Frequency);
                BWriter.WriteUInt32(entry.Value.DataSize);
                BWriter.WriteUInt32(entry.Value.Channels);
                BWriter.WriteUInt32(entry.Value.Bits);
                BWriter.WriteUInt32(entry.Value.PSIsample);
                BWriter.WriteUInt32(entry.Value.LoopOffset);
                BWriter.WriteUInt32(entry.Value.Duration);

                //--Write debug file
                if (DebugSampleInfo)
                {
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine("// File Name");
                    DebugFile.WriteLine("\t" + entry.Value.LoadedFileName);
                    DebugFile.WriteLine("// Flags");
                    DebugFile.WriteLine("\t" + entry.Value.Flags);
                    DebugFile.WriteLine("// DataSize");
                    DebugFile.WriteLine("\t" + entry.Value.DataSize);
                    DebugFile.WriteLine("// Frequency");
                    DebugFile.WriteLine("\t" + entry.Value.Frequency);
                    DebugFile.WriteLine("// Real Size");
                    DebugFile.WriteLine("\t" + entry.Value.DataSize);
                    DebugFile.WriteLine("// Channels");
                    DebugFile.WriteLine("\t" + entry.Value.Channels);
                    DebugFile.WriteLine("// Bits");
                    DebugFile.WriteLine("\t" + entry.Value.Bits);
                    DebugFile.WriteLine("// PSIsample");
                    DebugFile.WriteLine("\t" + entry.Value.PSIsample);
                    DebugFile.WriteLine("// LoopOffset");
                    DebugFile.WriteLine("\t" + entry.Value.LoopOffset);
                    DebugFile.WriteLine("// Duration");
                    DebugFile.WriteLine("\t" + entry.Value.Duration + "\n");
                }

                ProgressBarUpdate(Bar, 1);
                SetLabelText(LabelInfo, "WritingSampleInfo: " + entry.Key);
            }

            //--[Section length, current position - start position]--
            SampleInfoLength = BWriter.BaseStream.Position - SampleInfoStartOffset;
        }

        internal void WriteSampleDataSection(BinaryStream BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, int DebugFlags, StreamWriter DebugFile, ProgressBar Bar, Label LabelInfo)
        {
            bool DebugSampleDataSection;

            //CheckFlag Sample Info is checked
            DebugSampleDataSection = Convert.ToBoolean((DebugFlags >> 3) & 1);

            //Align Bytes
            BWriter.Align(16, true);

            //Update UI
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, FinalAudioDataDict.Count());

            //DebugFile
            if (DebugSampleDataSection)
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// Sample Data Section");
                DebugFile.WriteLine(new String('/', 70) + "\n");
            }

            //--[Start section offset]--
            SampleDataStartOffset = BWriter.BaseStream.Position;
            foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
            {
                //--DebugFile--
                if (DebugSampleDataSection)
                {
                    DebugFile.WriteLine(new String('/', 70));
                    DebugFile.WriteLine("// Sample Name");
                    DebugFile.WriteLine("\t" + entry.Value.LoadedFileName);
                    DebugFile.WriteLine("// Position");
                    DebugFile.WriteLine("\t" + BWriter.BaseStream.Position);
                    DebugFile.WriteLine("// Relative Position");
                    DebugFile.WriteLine("\t" + (BWriter.BaseStream.Position - SampleDataStartOffset));
                    DebugFile.WriteLine("// PCM Data");
                    DebugFile.WriteLine("\t" + entry.Value.PCMdata);
                    DebugFile.WriteLine("// Aligment Bits");
                    DebugFile.WriteLine("\t" + 4);
                    DebugFile.WriteLine("\n");
                }

                //--Add Sample data offset to the list--
                SampleDataOffsets.Add(BWriter.BaseStream.Position - SampleDataStartOffset);

                //--Write PCM Data--
                BWriter.WriteBytes(entry.Value.PCMdata);

                //--Align--
                BWriter.Align(4);

                //--Update UI--
                ProgressBarUpdate(Bar, 1);
                SetLabelText(LabelInfo, "WrittingSampleData: " + entry.Key);
            }
            //--Trim list--
            SampleDataOffsets.TrimExcess();

            //--Section length, current position - start position--
            SampleDataLength = BWriter.BaseStream.Position - SampleDataStartOffset;

            //Get total file size
            WholeFileSize = BWriter.BaseStream.Position;
        }

        internal void WriteFinalOffsets(BinaryStream BWriter, int DebugFlags, StreamWriter DebugFile, uint FileHashcode, ProgressBar Bar, Label LabelInfo)
        {
            bool DebugHashcodesTable;

            //*===============================================================================================
            //* WRITE FINAL HEADER INFO
            //*===============================================================================================
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, HashcodeOffsetM.Length + SampleDataOffsets.Count() + 5);

            //--Size of the whole file--
            BWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)WholeFileSize);
            ProgressBarUpdate(Bar, 1);

            //--SFX Length--
            BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SFXlength);
            ProgressBarUpdate(Bar, 1);

            //--Sample info start--
            BWriter.BaseStream.Seek(0x18, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SampleInfoStartOffset);
            BWriter.WriteUInt32((uint)SampleInfoLength);
            ProgressBarUpdate(Bar, 1);

            //--Special sample info start--
            BWriter.BaseStream.Seek(0x20, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SampleDataStartOffset);
            BWriter.WriteUInt32(00000000);
            ProgressBarUpdate(Bar, 1);

            //--Sample Data Start--
            BWriter.BaseStream.Seek(0x28, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SampleDataStartOffset);
            BWriter.WriteUInt32((uint)SampleDataLength);
            ProgressBarUpdate(Bar, 1);

            //*--------------------[Write Debug Info]--------------------
            //CheckFlag Header Info is checked
            if (Convert.ToBoolean((DebugFlags >> 4) & 1))
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// Soundbank Header");
                DebugFile.WriteLine(new String('/', 70) + "\n");
                DebugFile.WriteLine("// 'MUSX' Marker");
                DebugFile.WriteLine("\t4d555358");
                DebugFile.WriteLine("// File HashCode");
                DebugFile.WriteLine("\t0x" + FileHashcode.ToString("X8"));
                DebugFile.WriteLine("// File Version");
                DebugFile.WriteLine("\t000000c9");
                DebugFile.WriteLine("// File Size");
                DebugFile.WriteLine("\t" + WholeFileSize);
                DebugFile.WriteLine("// Offset To SFX Section");
                DebugFile.WriteLine("\t800h");
                DebugFile.WriteLine("// SFX Section Length");
                DebugFile.WriteLine("\t" + SFXlength);
                DebugFile.WriteLine("// Offset To Sample Info Section");
                DebugFile.WriteLine("\t" + SampleInfoStartOffset + "h");
                DebugFile.WriteLine("// Sample Info Section Length");
                DebugFile.WriteLine("\t" + SampleInfoLength);
                DebugFile.WriteLine("// Offset To Special Sample Info Section");
                DebugFile.WriteLine("\t" + SampleDataStartOffset + "h");
                DebugFile.WriteLine("// Special Sample Info Section Length");
                DebugFile.WriteLine("\t00000000");
                DebugFile.WriteLine("// Offset To Sample Data Section");
                DebugFile.WriteLine("\t" + SampleDataStartOffset + "h");
                DebugFile.WriteLine("// Sample Data Section Length");
                DebugFile.WriteLine("\t" + SampleDataLength + "\n");
            }

            //*===============================================================================================
            //* WRITE HASHCODE REAL OFFSETS
            //*===============================================================================================
            //CheckFlag Hashcodes Table is checked
            DebugHashcodesTable = Convert.ToBoolean((DebugFlags >> 0) & 1);

            if (DebugHashcodesTable)
            {
                DebugFile.WriteLine(new String('/', 70));
                DebugFile.WriteLine("// HashCodes Offsets Table");
                DebugFile.WriteLine(new String('/', 70) + "\n");
            }

            BWriter.Seek(0x804, SeekOrigin.Begin);
            for (int i = 0; i < HashcodeOffsetM.GetLength(0); i++)
            {
                BWriter.Seek(4, SeekOrigin.Current);
                BWriter.WriteUInt32((uint)HashcodeOffsetM[i, 1]);

                //Debug File
                if (DebugHashcodesTable)
                {
                    DebugFile.WriteLine("// HashCode: 0x" + HashcodeOffsetM[i, 0].ToString("X8") + " -> 0x" + (HashcodeOffsetM[i, 0] - 0x1A000000).ToString("X8"));
                    DebugFile.WriteLine("\t" + (uint)HashcodeOffsetM[i, 1] + "h");
                }

                //Update UI
                ProgressBarUpdate(Bar, 1);
                SetLabelText(LabelInfo, "WrittingFinalOffsets: " + HashcodeOffsetM[i, 1]);
            }

            //*===============================================================================================
            //* WRITE FINAL OFFSETS TO SAMPLE DATA
            //*===============================================================================================
            BWriter.BaseStream.Seek(SampleInfoStartOffset + 4, SeekOrigin.Begin);
            foreach (long item in SampleDataOffsets)
            {
                //--Skip numsamples and flags--
                BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 4, SeekOrigin.Begin);

                //--Calculate Relative Offset--
                BWriter.WriteUInt32((uint)item);

                //--Skip other properties--
                BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 32, SeekOrigin.Begin);

                ProgressBarUpdate(Bar, 1);
                SetLabelText(LabelInfo, "WrittingFinalOffsets: " + item);
            }
        }

        internal Dictionary<uint, EXSound> GetFinalSoundsDictionary(Dictionary<uint, EXSound> SoundsList, ProgressBar Bar, Label LabelInfo)
        {
            Dictionary<uint, EXSound> FinalSortedDict = new Dictionary<uint, EXSound>();

            //Update UI
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, SoundsList.Count());

            //Discard SFXs that are checked as "no output" and sort
            foreach (KeyValuePair<uint, string> HashCode in Hashcodes.SFX_Defines)
            {
                foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                {
                    if ((Sound.Value.Hashcode) == HashCode.Key)
                    {
                        if (Sound.Value.OutputThisSound)
                        {
                            if (!FinalSortedDict.ContainsKey(Sound.Key))
                            {
                                FinalSortedDict.Add(Sound.Key, Sound.Value);
                            }
                        }
                        SetLabelText(LabelInfo, "Checking SFX Data");
                        ProgressBarUpdate(Bar, 1);
                        break;
                    }
                }
            }

            return FinalSortedDict;
        }

        internal Dictionary<string, EXAudio> GetFinalAudioDictionaryPCMData(IEnumerable<string> UsedAudios, Dictionary<string, EXAudio> AudiosList, ProgressBar Bar)
        {
            Dictionary<string, EXAudio> FinalAudioDataDict = new Dictionary<string, EXAudio>();

            //Update UI
            ProgressBarReset(Bar);
            ProgressBarMaximum(Bar, UsedAudios.Count());

            //Add Data
            using (IEnumerator<string> enumerator = UsedAudios.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    string element = enumerator.Current;
                    if (AudiosList.ContainsKey(element))
                    {
                        if (!FinalAudioDataDict.ContainsKey(element))
                        {
                            FinalAudioDataDict.Add(element, AudiosList[element]);
                        }
                    }
                    ProgressBarUpdate(Bar, 1);
                }
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

        private int GetSteamIndexInSoundbank(string SampleName, Dictionary<string, EXAudio> AudioDataList, EXSample Sample, int Flags)
        {
            int index;

            if (EXSoundbanksFunctions.SubSFXFlagChecked(Flags))
            {
                index = (int)Sample.HashcodeSubSFX - 0x1A000000;
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
                    if (BarToUpdate.Value < BarToUpdate.Maximum)
                    {
                        BarToUpdate.Value += ValueToAdd;
                    }
                });
            }
        }

        private void ProgressBarMaximum(ProgressBar BarToChange, int Maximum)
        {
            if (BarToChange != null)
            {
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