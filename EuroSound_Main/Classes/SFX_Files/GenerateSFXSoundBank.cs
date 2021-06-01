using EuroSound_Application.AudioFunctionsLibrary;
using EuroSound_Application.Editors_and_Tools;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.SoundBanksEditor;
using NAudio.Wave;
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
        private List<long> DataSizeOffsets = new List<long>();

        internal void WriteFileHeader(BinaryStream BWriter, uint FileHashcode, ProgressBar Bar)
        {
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
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
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
        }

        internal void WriteFileSections(BinaryStream BWriter, int NumberOfSamples, ProgressBar Bar)
        {
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, 9);

            //--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--
            BWriter.WriteUInt32(SFXStartSection);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--sfxlength[size of the first section, in bytes]--
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--sampleinfostart[offset to the second section where the sample properties are stored]--
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--sampleinfolen[size of the second section, in bytes]--
            BWriter.WriteUInt32((uint)NumberOfSamples);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--specialsampleinfostart[unused and uses the same sample data offset as dummy for some reason]--
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--specialsampleinfolen[unused and set to zero]--
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--sampledatastart[Offset that points to the beginning of the PCM data, where sound is actually stored]--
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
            //--sampledatalen[Size of the block, in bytes]--
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            BWriter.Seek(SFXStartSection, SeekOrigin.Begin);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
        }

        internal void WriteSFXSection(BinaryStream BWriter, Dictionary<uint, EXSound> FinalSoundsDict, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            int index = 0;

            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, FinalSoundsDict.Count());

            //--[SFX entry count in this soundbank]--
            BWriter.WriteUInt32((uint)FinalSoundsDict.Count);

            //--[SFX header]--
            HashcodeOffsetM = new long[FinalSoundsDict.Keys.Count, 2];
            foreach (KeyValuePair<uint, EXSound> Sound in FinalSoundsDict)
            {
                BWriter.WriteUInt32(Sound.Value.Hashcode & 0x00ffffff); //Apply bytes mask, example: 0x1A00005C -> 0x0000005C
                BWriter.WriteUInt32((uint)BWriter.BaseStream.Position);

                HashcodeOffsetM[index, 0] = Convert.ToUInt32(Sound.Value.Hashcode.ToString("X8"), 16);
                index++;
            }

            index = 0;

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
                GenericFunctions.SetLabelText(LabelInfo, "WritingSFX Data");

                foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                {
                    //--[FILE REFERENCE]--
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

                    //--[Sample Data]--
                    BWriter.WriteInt16(Sample.Value.PitchOffset);
                    BWriter.WriteInt16(Sample.Value.RandomPitchOffset);
                    BWriter.WriteSByte(Sample.Value.BaseVolume);
                    BWriter.WriteSByte(Sample.Value.RandomVolumeOffset);
                    BWriter.WriteSByte(Sample.Value.Pan);
                    BWriter.WriteSByte(Sample.Value.RandomPan);

                    //--[Aligment Padding]--
                    AddPaddingBytes(2, BWriter);
                }
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
                index++;
            }

            //--[Section length, current position - start position]--
            SFXlength = BWriter.BaseStream.Position - SFXStartSection;
        }

        internal void WriteSampleInfoSection(BinaryStream BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, ProgressBar Bar, Label LabelInfo)
        {
            //--[Align Bytes]--
            BWriter.Align(16, true);

            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, FinalAudioDataDict.Count());

            //--[Start section offset]--
            SampleInfoStartOffset = (BWriter.BaseStream.Position);

            //--[Write total number of samples]--
            BWriter.WriteUInt32((uint)FinalAudioDataDict.Keys.Count);

            foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
            {
                //--[Write data]--
                BWriter.WriteUInt32(entry.Value.Flags);
                BWriter.WriteUInt32(00000000);
                BWriter.WriteUInt32(00000000);
                BWriter.WriteUInt32(entry.Value.Frequency);
                BWriter.WriteUInt32((uint)entry.Value.PCMdata.Length);
                BWriter.WriteUInt32(entry.Value.Channels);
                BWriter.WriteUInt32(entry.Value.Bits);
                BWriter.WriteUInt32(entry.Value.PSIsample);
                BWriter.WriteUInt32(entry.Value.LoopOffset);
                BWriter.WriteUInt32(entry.Value.Duration);

                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
                GenericFunctions.SetLabelText(LabelInfo, "WritingSampleInfo: " + entry.Key);
            }

            //--[Section length, current position - start position]--
            SampleInfoLength = BWriter.BaseStream.Position - SampleInfoStartOffset;
        }

        internal void WriteSampleDataSectionPC(BinaryStream BWriter, Dictionary<string, EXAudio> FinalAudioDataDict, int blockAlign, ProgressBar Bar, Label LabelInfo)
        {
            //Align Bytes
            BWriter.Align(16, true);

            //Update UI
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, FinalAudioDataDict.Count());

            //--[Start section offset]--
            SampleDataStartOffset = BWriter.BaseStream.Position;
            foreach (KeyValuePair<string, EXAudio> entry in FinalAudioDataDict)
            {
                //--Add Sample data offset to the list--
                long StartOffset = BWriter.BaseStream.Position;
                SampleDataOffsets.Add(StartOffset - SampleDataStartOffset);

                //--Write PCM Data--
                BWriter.WriteBytes(entry.Value.PCMdata);

                //--Align--
                BWriter.Align(blockAlign);

                //--Add Data Size offset to the list--
                DataSizeOffsets.Add(BWriter.BaseStream.Position - StartOffset);

                //--Update UI--
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
                GenericFunctions.SetLabelText(LabelInfo, "WrittingSampleData: " + entry.Key);
            }
            //--Trim list--
            SampleDataOffsets.TrimExcess();
            DataSizeOffsets.TrimExcess();

            //--Section length, current position - start position--
            SampleDataLength = BWriter.BaseStream.Position - SampleDataStartOffset;

            //Get total file size
            WholeFileSize = BWriter.BaseStream.Position;
        }

        internal void WriteFinalOffsets(BinaryStream BWriter, ProgressBar Bar, Label LabelInfo)
        {
            //*===============================================================================================
            //* WRITE FINAL HEADER INFO
            //*===============================================================================================
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, (HashcodeOffsetM.GetLength(0) + SampleDataOffsets.Count()) + 5);

            //--Size of the whole file--
            BWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)WholeFileSize);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--SFX Length--
            BWriter.BaseStream.Seek(0x14, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SFXlength);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--Sample info start--
            BWriter.BaseStream.Seek(0x18, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SampleInfoStartOffset);
            BWriter.WriteUInt32((uint)SampleInfoLength);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--Special sample info start--
            BWriter.BaseStream.Seek(0x20, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SampleDataStartOffset);
            BWriter.WriteUInt32(00000000);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //--Sample Data Start--
            BWriter.BaseStream.Seek(0x28, SeekOrigin.Begin);
            BWriter.WriteUInt32((uint)SampleDataStartOffset);
            BWriter.WriteUInt32((uint)SampleDataLength);
            ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);

            //*===============================================================================================
            //* WRITE HASHCODE REAL OFFSETS
            //*===============================================================================================
            BWriter.Seek(0x804, SeekOrigin.Begin);
            for (int i = 0; i < HashcodeOffsetM.GetLength(0); i++)
            {
                BWriter.Seek(4, SeekOrigin.Current);
                BWriter.WriteUInt32((uint)HashcodeOffsetM[i, 1]);

                //Update UI
                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
                GenericFunctions.SetLabelText(LabelInfo, "WrittingFinalOffsets: " + HashcodeOffsetM[i, 1]);
            }

            //*===============================================================================================
            //* WRITE FINAL OFFSETS TO SAMPLE DATA
            //*===============================================================================================
            BWriter.BaseStream.Seek(SampleInfoStartOffset + 4, SeekOrigin.Begin);
            for (int i = 0; i < SampleDataOffsets.Count; i++)
            {
                //--Skip numsamples and flags--
                BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 4, SeekOrigin.Begin);

                //--Write Relative Offset To Audio Data--
                BWriter.WriteUInt32((uint)SampleDataOffsets[i]);

                //--Write Data Size, with paddings--
                BWriter.WriteUInt32((uint)DataSizeOffsets[i]);

                //--Skip other properties--
                BWriter.BaseStream.Seek(BWriter.BaseStream.Position + 28, SeekOrigin.Begin);

                ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
                GenericFunctions.SetLabelText(LabelInfo, "WrittingFinalOffsets: " + SampleDataOffsets[i]);
            }
        }

        internal Dictionary<uint, EXSound> GetFinalSoundsDictionary(Dictionary<uint, EXSound> SoundsList, ProgressBar Bar, Label LabelInfo)
        {
            Dictionary<uint, EXSound> FinalSortedDict = new Dictionary<uint, EXSound>();

            //Update UI
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, SoundsList.Count());

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
                        GenericFunctions.SetLabelText(LabelInfo, "Checking SFX Data");
                        ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
                        break;
                    }
                }
            }
            return FinalSortedDict;
        }

        internal Dictionary<string, EXAudio> GetFinalAudioDictionaryPCMData(IEnumerable<string> UsedAudios, Dictionary<string, EXAudio> AudiosList, ProgressBar Bar, string outputTarget)
        {
            Dictionary<string, EXAudio> FinalAudioDataDict = new Dictionary<string, EXAudio>();

            //Update UI
            ToolsCommonFunctions.ProgressBarReset(Bar);
            GenericFunctions.ProgressBarSetMaximum(Bar, UsedAudios.Count());

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
                            EXAudio audioToExport = AudiosList[element];
                            if (outputTarget.Equals("PC", StringComparison.OrdinalIgnoreCase))
                            {
                                audioToExport.LoopOffset *= 2;
                                FinalAudioDataDict.Add(element, audioToExport);
                            }
                            else if (outputTarget.Equals("PS2", StringComparison.OrdinalIgnoreCase))
                            {
                                //Output audio in WAV
                                byte[] pcmData;
                                AudioFunctions audiof = new AudioFunctions();
                                MemoryStream AudioSample = new MemoryStream(audioToExport.PCMdata);
                                IWaveProvider provider = new RawSourceWaveStream(AudioSample, new WaveFormat((int)audioToExport.Frequency, (int)audioToExport.Bits, (int)audioToExport.Channels));
                                using (MediaFoundationResampler conversionStream = new MediaFoundationResampler(provider, new WaveFormat((int)audioToExport.FrequencyPS2, (int)audioToExport.Bits, (int)audioToExport.Channels)))
                                {
                                    //Get PCM Data Stereo
                                    using (MemoryStream outStream = new MemoryStream())
                                    {
                                        byte[] bytes = new byte[conversionStream.WaveFormat.AverageBytesPerSecond * 4];
                                        while (true)
                                        {
                                            int bytesRead = conversionStream.Read(bytes, 0, bytes.Length);
                                            if (bytesRead == 0)
                                            {
                                                break;
                                            }
                                            outStream.Write(bytes, 0, bytesRead);
                                        }
                                        pcmData = outStream.ToArray();
                                    }
                                }

                                VAG_Encoder_Decoder.VagFunctions vagF = new VAG_Encoder_Decoder.VagFunctions();

                                //Check loopOffset
                                uint loopOffset = 0;
                                bool UseLoopOffset = Convert.ToBoolean(audioToExport.Flags > 0);
                                if (UseLoopOffset)
                                {
                                    loopOffset = vagF.CalculateLoopOffset(audioToExport.LoopOffset, audioToExport.Frequency, audioToExport.FrequencyPS2);
                                }

                                //Parse audio to VAG
                                byte[] encodedVagData = vagF.VAGEncoder(audiof.ConvertPCMDataToShortArray(pcmData), 16, loopOffset, UseLoopOffset);

                                //Create audio
                                EXAudio ps2Audio = new EXAudio
                                {
                                    Flags = audioToExport.Flags,
                                    Frequency = audioToExport.FrequencyPS2,
                                    Channels = audioToExport.Channels,
                                    Bits = 4,
                                    PSIsample = audioToExport.PSIsample,
                                    LoopOffset = loopOffset,
                                    Duration = audioToExport.Duration,
                                    PCMdata = encodedVagData
                                };
                                FinalAudioDataDict.Add(element, ps2Audio);
                            }
                        }
                    }
                    ToolsCommonFunctions.ProgressBarAddValue(Bar, 1);
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
                //Apply bytes mask, example: 0x1A00005C -> 0x0000005C
                index = (int)Sample.HashcodeSubSFX & 0x00ffffff;
            }
            else
            {
                index = AudioDataList.Keys.ToList().IndexOf(SampleName);
            }

            return index;
        }
    }
}