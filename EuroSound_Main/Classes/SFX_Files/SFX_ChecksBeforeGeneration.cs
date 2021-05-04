using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;

namespace EuroSound_Application.Classes.SFX_Files
{
    class SFX_ChecksBeforeGeneration
    {
        internal bool ValidateMarkers(List<EXStreamMarker> MarkersList, string ObjectName, IList<string> Reports)
        {
            int StartMarkers = 0;
            int EndMarkers = 0;
            int GotoMarkers = 0;
            int LoopMarkers = 0;

            //Count Markers
            for (int i = 0; i < MarkersList.Count; i++)
            {
                switch (MarkersList[i].MusicMakerType)
                {
                    case (int)GenericFunctions.ESoundMarkers.Start:
                        StartMarkers++;
                        break;
                    case (int)GenericFunctions.ESoundMarkers.End:
                        EndMarkers++;
                        break;
                    case (int)GenericFunctions.ESoundMarkers.Goto:
                        GotoMarkers++;
                        break;
                    case (int)GenericFunctions.ESoundMarkers.Loop:
                        LoopMarkers++;
                        break;
                }
            }

            //Check Markers
            bool MarkersCorrect = true;

            if (StartMarkers < 1)
            {
                if (Reports != null)
                {
                    Reports.Add("1No Start markers found on \"" + ObjectName + "\", should contains at least one.");
                }
            }
            if (LoopMarkers > 1) //Cancel output
            {
                MarkersCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0More than one Loop marker on \"" + ObjectName + "\", must have only one.");
                }
            }
            if (GotoMarkers > 1) //Cancel output
            {
                MarkersCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0More than one Goto on \"" + ObjectName + "\", must have only one.");
                }
            }
            if (EndMarkers > 1) //Cancel output
            {
                MarkersCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0More than one End marker on \"" + ObjectName + "\", must have only one.");
                }
            }

            return MarkersCorrect;
        }

        internal bool ValidateMusics(EXMusic MusicToOutput, string ObjectName, IList<string> Reports)
        {
            bool MusicIsCorrect = true;

            if (MusicToOutput.PCM_Data_LeftChannel.Length == 0)
            {
                MusicIsCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0Error in \"" + ObjectName + "\", the left channel does not contains any data.");
                }
            }

            if (MusicToOutput.PCM_Data_RightChannel.Length == 0)
            {
                MusicIsCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0Error in \"" + ObjectName + "\", the right channel does not contains any data.");
                }
            }

            if (MusicToOutput.PCM_Data_LeftChannel.Length > 0 && MusicToOutput.PCM_Data_RightChannel.Length > 0)
            {
                if (MusicToOutput.Channels_LeftChannel != MusicToOutput.Channels_RightChannel)
                {
                    MusicIsCorrect = false;
                    if (Reports != null)
                    {
                        Reports.Add("0Error in \"" + ObjectName + "\", number of channels does not match.");
                    }
                }
                if (!MusicToOutput.Encoding_LeftChannel.Equals(MusicToOutput.Encoding_RightChannel))
                {
                    MusicIsCorrect = false;
                    if (Reports != null)
                    {
                        Reports.Add("0Error in \"" + ObjectName + "\", the encoding between channels does not match.");
                    }
                }
                if (MusicToOutput.Frequency_LeftChannel != MusicToOutput.Frequency_RightChannel)
                {
                    MusicIsCorrect = false;
                    if (Reports != null)
                    {
                        Reports.Add("0Error in \"" + ObjectName + "\", the frequency between channels does not match.");
                    }
                }
                if (MusicToOutput.RealSize_LeftChannel != MusicToOutput.RealSize_RightChannel)
                {
                    MusicIsCorrect = false;
                    if (Reports != null)
                    {
                        Reports.Add("0Error in \"" + ObjectName + "\", the real size between channels does not match.");
                    }
                }
                if (MusicToOutput.Bits_LeftChannel != MusicToOutput.Bits_RightChannel)
                {
                    MusicIsCorrect = false;
                    if (Reports != null)
                    {
                        Reports.Add("0Error in \"" + ObjectName + "\", the bits per second between channels does not match.");
                    }
                }
            }

            return MusicIsCorrect;
        }

        internal bool ValidateStreamingSounds(EXSoundStream StreamSoundToExport, string ObjectName, IList<string> Reports)
        {
            bool StreamSoundIsCorrect = true;

            if (StreamSoundToExport.PCM_Data.Length < 1)
            {
                StreamSoundIsCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0Error in \"" + ObjectName + "\", there is no loaded data.");
                }
            }

            return StreamSoundIsCorrect;
        }

        internal bool ValidateSFX(EXSound SoundToExport, IList<uint> Hashcodes, string ObjectName, IList<string> Reports)
        {
            bool SFXIsCorrect = true;

            //Hashcodes check
            if (SoundToExport.Hashcode == 0)
            {
                if (Reports != null)
                {
                    Reports.Add("1\"" + ObjectName + "\", does not have a HashCode.");
                }
            }
            else
            {
                if (Hashcodes.Contains(SoundToExport.Hashcode))
                {
                    if (Reports != null)
                    {
                        Reports.Add("1Duplicate HashCode, more than one object contains: " + SoundToExport.Hashcode.ToString("X8"));
                    }
                }
                else
                {
                    Hashcodes.Add(SoundToExport.Hashcode);
                }
            }

            //Check samples
            if (SoundToExport.Samples.Count < 1)
            {
                SFXIsCorrect = false;
                if (Reports != null)
                {
                    Reports.Add("0SFX " + ObjectName + " does not contains any sample.");
                }
            }

            //Check Samples has an audio asociated
            foreach (KeyValuePair<uint, EXSample> Sample in SoundToExport.Samples)
            {
                if (Sample.Value.IsStreamed)
                {
                    if (Sample.Value.FileRef >= 0)
                    {
                        SFXIsCorrect = false;
                        if (Reports != null)
                        {
                            Reports.Add("0SFX " + ObjectName + " does not have any external sound selected.");
                        }
                    }
                }
                else
                {
                    if (EXSoundbanksFunctions.SubSFXFlagChecked(SoundToExport.Flags))
                    {
                        if (Sample.Value.HashcodeSubSFX == 0)
                        {
                            SFXIsCorrect = false;
                            if (Reports != null)
                            {
                                Reports.Add("0SFX " + ObjectName + " does not have any Sub SFX HashCode selected.");
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Sample.Value.ComboboxSelectedAudio))
                        {
                            SFXIsCorrect = false;
                            if (Reports != null)
                            {
                                Reports.Add("0SFX " + ObjectName + " does not have any audio asociated.");
                            }
                        }
                    }
                }
            }

            return SFXIsCorrect;
        }

        internal bool ValidateAudios(EXAudio AudioToExport, string ObjectName, IList<string> Reports)
        {
            bool AudioIsValid = true;

            if (AudioToExport.PCMdata.Length == 0)
            {
                AudioIsValid = false;
                if (Reports != null)
                {
                    Reports.Add("0SFX " + ObjectName + " does not have data loaded.");
                }
            }

            return AudioIsValid;
        }
    }
}
