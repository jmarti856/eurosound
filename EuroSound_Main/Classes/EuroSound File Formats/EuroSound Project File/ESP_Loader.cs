using EuroSound_Application.ApplicationPreferences;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EuroSound_Application.EuroSound_Profiles
{
    internal class ESP_Loader
    {
        private Regex RemoveCharactersFromPathString = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");

        internal bool FileIsValid(IEnumerable<string> lines)
        {
            bool FileValid = false;
            bool FirstValidation = false, SecondValidation = false;
            int position = 0;

            //Check two first lines
            foreach (string line in lines)
            {
                if (line.Trim().Equals("ESP", StringComparison.OrdinalIgnoreCase))
                {
                    FirstValidation = true;
                }

                if (line.Trim().Equals("Version: 1", StringComparison.OrdinalIgnoreCase))
                {
                    SecondValidation = true;
                }

                position++;

                if (position == 2)
                {
                    break;
                }
            }

            //Check booleans
            if (FirstValidation && SecondValidation)
            {
                FileValid = true;
            }

            return FileValid;
        }

        internal void ReadSoundBankSettings(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[SoundbanksSettings]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "Frequency":
                                if (int.TryParse(LineData[1], out int SoundbankFrequencyParsed))
                                {
                                    GlobalPreferences.SoundbankFrequency = SoundbankFrequencyParsed;
                                }
                                break;
                            case "Encoding":
                                GlobalPreferences.SoundbankEncoding = LineData[1];
                                break;
                            case "Bits":
                                if (int.TryParse(LineData[1], out int SoundbankBitsParsed))
                                {
                                    GlobalPreferences.SoundbankBits = SoundbankBitsParsed;
                                }
                                break;
                            case "Channels":
                                if (int.TryParse(LineData[1], out int SoundbankChannelsParsed))
                                {
                                    GlobalPreferences.SoundbankChannels = SoundbankChannelsParsed;
                                }
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadStreamFileSettings(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[StreamFileSettings]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "Frequency":
                                if (int.TryParse(LineData[1], out int StreambankFrequencyParsed))
                                {
                                    GlobalPreferences.StreambankFrequency = StreambankFrequencyParsed;
                                }
                                break;
                            case "Encoding":
                                GlobalPreferences.StreambankEncoding = LineData[1];
                                break;
                            case "Bits":
                                if (int.TryParse(LineData[1], out int StreambankBitsParsed))
                                {
                                    GlobalPreferences.StreambankBits = StreambankBitsParsed;
                                }
                                break;
                            case "Channels":
                                if (int.TryParse(LineData[1], out int StreambankChannelsParsed))
                                {
                                    GlobalPreferences.StreambankChannels = StreambankChannelsParsed;
                                }
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadMusicFileSettings(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[MusicFileSettings]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "Frequency":
                                if (int.TryParse(LineData[1], out int MusicbankFrequencyParsed))
                                {
                                    GlobalPreferences.MusicbankFrequency = MusicbankFrequencyParsed;
                                }
                                break;
                            case "Encoding":
                                GlobalPreferences.MusicbankEncoding = LineData[1];
                                break;
                            case "Bits":
                                if (int.TryParse(LineData[1], out int MusicbankBitsParsed))
                                {
                                    GlobalPreferences.MusicbankBits = MusicbankBitsParsed;
                                }
                                break;
                            case "Channels":
                                if (int.TryParse(LineData[1], out int MusicbankChannelsParsed))
                                {
                                    GlobalPreferences.MusicbankChannels = MusicbankChannelsParsed;
                                }
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadHashTableFiles(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[HashTableFiles]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "HT_Sound":
                                GlobalPreferences.HT_SoundsPath = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                            case "HT_SoundData":
                                GlobalPreferences.HT_SoundsDataPath = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                            case "HT_MusicEvent":
                                GlobalPreferences.HT_MusicPath = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadHashCodesPrefixes(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[HashCodesPrefixes]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "StreamFileHashCode":
                                GlobalPreferences.StreamFileHashCode = Convert.ToUInt32(LineData[1], 16);
                                break;
                            case "SfxPrefix":
                                GlobalPreferences.SfxPrefix = Convert.ToUInt32(LineData[1], 16);
                                break;
                            case "MfxPrefix":
                                GlobalPreferences.MusicPrefix = Convert.ToUInt32(LineData[1], 16);
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadExternalFiles(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[ExternalFiles]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "StreamFile":
                                GlobalPreferences.StreamFilePath = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                            case "MkFileList":
                                GlobalPreferences.MkFileListPath = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                            case "MkFileList2":
                                GlobalPreferences.MkFileList2Path = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadOutputFolders(IEnumerable<string> lines)
        {
            bool ReadingSection = false;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[OutputFolders]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "OutputDirectory":
                                GlobalPreferences.OutputDirectory = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                            case "DebugFilesOutputDirectory":
                                GlobalPreferences.DebugFilesOutputDirectory = RemoveCharactersFromPathString.Replace(LineData[1], "");
                                break;
                        }
                    }
                }
            }
        }

        internal string[] ReadFlagsBlock(string BlockName, IEnumerable<string> lines)
        {
            bool ReadingSection = false;
            string[] FlagLabels = new string[16];
            int counter = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[" + BlockName + "]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }

                    if (ReadingSection)
                    {
                        FlagLabels[counter] = line.Trim();
                        counter++;
                    }
                }
            }

            return FlagLabels;
        }
    }
}
