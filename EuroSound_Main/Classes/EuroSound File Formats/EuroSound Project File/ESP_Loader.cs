using EuroSound_Application.ApplicationPreferences;
using System;
using System.Collections.Generic;

namespace EuroSound_Application.EuroSound_Profiles
{
    class ESP_Loader
    {
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
                if (string.IsNullOrEmpty(line))
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
                        bool ParseRes;
                        int NumericValue;

                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "Frequency":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.SoundbankFrequency = NumericValue;
                                }
                                break;
                            case "Encoding":
                                GlobalPreferences.SoundbankEncoding = LineData[1];
                                break;
                            case "Bits":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.SoundbankBits = NumericValue;
                                }
                                break;
                            case "Channels":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.SoundbankChannels = NumericValue;
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
                if (string.IsNullOrEmpty(line))
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
                        bool ParseRes;
                        int NumericValue;

                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "Frequency":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.StreambankFrequency = NumericValue;
                                }
                                break;
                            case "Encoding":
                                GlobalPreferences.StreambankEncoding = LineData[1];
                                break;
                            case "Bits":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.StreambankBits = NumericValue;
                                }
                                break;
                            case "Channels":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.StreambankChannels = NumericValue;
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
                if (string.IsNullOrEmpty(line))
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
                        bool ParseRes;
                        int NumericValue;

                        string[] LineData = line.Trim().Split('=');
                        switch (LineData[0])
                        {
                            case "Frequency":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.MusicbankFrequency = NumericValue;
                                }
                                break;
                            case "Encoding":
                                GlobalPreferences.MusicbankEncoding = LineData[1];
                                break;
                            case "Bits":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.MusicbankBits = NumericValue;
                                }
                                break;
                            case "Channels":
                                ParseRes = int.TryParse(LineData[1], out NumericValue);
                                if (ParseRes)
                                {
                                    GlobalPreferences.MusicbankChannels = NumericValue;
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
                if (string.IsNullOrEmpty(line))
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
                                GlobalPreferences.HT_SoundsPath = LineData[1];
                                break;
                            case "HT_SoundData":
                                GlobalPreferences.HT_SoundsDataPath = LineData[1];
                                break;
                            case "HT_MusicEvent":
                                GlobalPreferences.HT_MusicPath = LineData[1];
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
                if (string.IsNullOrEmpty(line))
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
                                GlobalPreferences.StreamFilePath = LineData[1];
                                break;
                            case "MkFileList":
                                GlobalPreferences.MkFileListPath = LineData[1];
                                break;
                            case "MkFileList2":
                                GlobalPreferences.MkFileList2Path = LineData[1];
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
                if (string.IsNullOrEmpty(line))
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
                            case "MusicOutputDirectory":
                                GlobalPreferences.MusicOutputPath = LineData[1];
                                break;
                            case "SoundsOutputDirectory":
                                GlobalPreferences.SFXOutputPath = LineData[1];
                                break;
                            case "StreamSoundsOutputDirectory":
                                GlobalPreferences.StreamFileOutputPath = LineData[1];
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
                if (string.IsNullOrEmpty(line))
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
