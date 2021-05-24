using EuroSound_Application.Classes.SFX_Files;
using EuroSound_Application.GenerateSoundBankSFX;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System.Collections.Generic;

namespace EuroSound_Application.Classes.BashMode
{
    internal class BashMode_CommonFunctions
    {
        private SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();

        internal void CreateSFXSoundBanks(BinaryStream BWriter, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, uint File_Hashcode)
        {
            GenerateSFXSoundBank SFXGenerator = new GenerateSFXSoundBank();

            //*===============================================================================================
            //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
            //*===============================================================================================
            //Discard SFXs that has checked as "no output"
            Dictionary<uint, EXSound> FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(SoundsList, null, null);

            //*===============================================================================================
            //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED
            //*===============================================================================================
            IEnumerable<string> UsedAudios = EXSoundbanksFunctions.GetAudiosToExport(FinalSoundsDict);

            //Add data
            Dictionary<string, EXAudio> FinalAudioDataDict = SFXGenerator.GetFinalAudioDictionaryPCMData(UsedAudios, AudiosList, null);

            //*===============================================================================================
            //* STEP 3: CHECK DATA THAT WILL BE OUTPUTED
            //*===============================================================================================
            bool CanOutputFile = true;
            List<uint> SoundsHashcodes = new List<uint>();

            //Check Data, first the SFX Objects
            foreach (KeyValuePair<uint, EXSound> SoundToCheck in FinalSoundsDict)
            {
                CanOutputFile = SFX_Check.ValidateSFX(SoundToCheck.Value, FinalSoundsDict, SoundsHashcodes, null, null);
                if (CanOutputFile == false)
                {
                    break;
                }
            }

            if (CanOutputFile)
            {
                //Check Data, audio objects
                foreach (KeyValuePair<string, EXAudio> AudioToCheck in FinalAudioDataDict)
                {
                    CanOutputFile = SFX_Check.ValidateAudios(AudioToCheck.Value, null, null);
                    if (CanOutputFile == false)
                    {
                        break;
                    }
                }
            }

            //*===============================================================================================
            //* STEP 4: START WRITTING
            //*===============================================================================================
            if (CanOutputFile)
            {
                //--------------------------------------[WRITE FILE HEADER]--------------------------------------
                //Write Data
                SFXGenerator.WriteFileHeader(BWriter, File_Hashcode, null);

                //--------------------------------------[Write SECTIONS]--------------------------------------
                //Write Data
                SFXGenerator.WriteFileSections(BWriter, GenericFunctions.CountNumberOfSamples(FinalSoundsDict), null);

                //--------------------------------------[SECTION SFX elements]--------------------------------------
                //Write Data
                SFXGenerator.WriteSFXSection(BWriter, FinalSoundsDict, FinalAudioDataDict, null, null);

                //--------------------------------------[SECTION Sample info elements]--------------------------------------
                //Write Data
                SFXGenerator.WriteSampleInfoSection(BWriter, FinalAudioDataDict, null, null);

                //--------------------------------------[SECTION Sample data]--------------------------------------
                //Write Data
                SFXGenerator.WriteSampleDataSectionPC(BWriter, FinalAudioDataDict, null, null);

                //*===============================================================================================
                //* STEP 5: WRITE FINAL OFFSETS
                //*===============================================================================================
                //Write Data
                SFXGenerator.WriteFinalOffsets(BWriter, null, null);
            }

            //Close File
            BWriter.Close();
        }

        internal void CreateSFXStreamFie(BinaryStream BWriter, Dictionary<uint, EXSoundStream> DictionaryData, uint File_Hashcode)
        {
            GenerateSFXStreamedSounds SFXGenerator = new GenerateSFXStreamedSounds();

            //*===============================================================================================
            //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
            //*===============================================================================================
            Dictionary<uint, EXSoundStream> FinalSoundsDict = SFXGenerator.GetFinalSoundsDictionary(DictionaryData, null, null);

            //*===============================================================================================
            //* STEP 2: CHECK DATA THAT WILL BE OUTPUTED
            //*===============================================================================================
            bool CanOutputFile = true;

            //Check Data
            foreach (KeyValuePair<uint, EXSoundStream> SoundToCheck in FinalSoundsDict)
            {
                CanOutputFile = SFX_Check.ValidateStreamingSounds(SoundToCheck.Value, null, null);
                if (CanOutputFile == false)
                {
                    break;
                }
                else
                {
                    CanOutputFile = SFX_Check.ValidateMarkers(SoundToCheck.Value.Markers, null, null);
                    if (CanOutputFile == false)
                    {
                        break;
                    }
                }
            }

            //*===============================================================================================
            //* STEP 3: START WRITTING
            //*===============================================================================================
            if (CanOutputFile)
            {
                //Write Header
                SFXGenerator.WriteFileHeader(BWriter, File_Hashcode, null);

                //Write Sections
                SFXGenerator.WriteFileSections(BWriter, null);

                //Write Table
                SFXGenerator.WriteLookUptable(BWriter, FinalSoundsDict, null);

                //Write Data
                SFXGenerator.WriteStreamFile(BWriter, FinalSoundsDict, null);

                //*===============================================================================================
                //* STEP 4: WRITE FINAL OFFSETS
                //*===============================================================================================
                SFXGenerator.WriteFinalOffsets(BWriter, null);
            }
            BWriter.Close();
        }

        internal void CreateSFXMusicFile(BinaryStream BWriter, Dictionary<uint, EXMusic> DictionaryData, uint File_Hashcode)
        {
            GenerateSFXMusicBank SFXCreator = new GenerateSFXMusicBank();

            //*===============================================================================================
            //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED
            //*===============================================================================================
            Dictionary<uint, EXMusic> FinalMusicsDict = SFXCreator.GetFinalMusicsDictionary(DictionaryData, null, null);

            //*===============================================================================================
            //* STEP 2: CHECK DATA THAT WILL BE OUTPUTED
            //*===============================================================================================
            bool CanOutputFile = true;

            //Check Data
            foreach (KeyValuePair<uint, EXMusic> MusicToCheck in FinalMusicsDict)
            {
                CanOutputFile = SFX_Check.ValidateMusics(MusicToCheck.Value, null, null);
                if (CanOutputFile == false)
                {
                    break;
                }
                else
                {
                    CanOutputFile = SFX_Check.ValidateMarkers(MusicToCheck.Value.Markers, null, null);
                    if (CanOutputFile == false)
                    {
                        break;
                    }
                }
            }

            //*===============================================================================================
            //* STEP 3: START WRITTING
            //*===============================================================================================
            if (CanOutputFile)
            {
                //Write Header
                SFXCreator.WriteFileHeader(BWriter, File_Hashcode, null);

                //Write Sections
                SFXCreator.WriteFileSections(BWriter, null);

                //Write Table
                SFXCreator.WriteFileSection1(BWriter, FinalMusicsDict, null);

                //Write Data
                SFXCreator.WriteFileSection2(BWriter, FinalMusicsDict, null);

                //*===============================================================================================
                //* STEP 4: WRITE FINAL OFFSETS
                //*===============================================================================================
                SFXCreator.WriteFinalOffsets(BWriter, null);
            }
            BWriter.Close();
        }
    }
}
