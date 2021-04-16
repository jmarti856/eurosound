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

        internal string[] ReadSection(string SectionName, int NumberOfItems, IEnumerable<string> lines)
        {
            string[] SectionData = new string[NumberOfItems];
            string[] LineData;
            bool ReadingSection = false;
            int counter = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                else
                {
                    if (line.Trim().Equals("[" + SectionName + "]", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadingSection = true;
                        continue;
                    }

                    if (ReadingSection)
                    {
                        if (counter < SectionData.Length)
                        {
                            if (SectionName.Equals("SoundFlags", StringComparison.OrdinalIgnoreCase) || SectionName.Equals("AudioFlags", StringComparison.OrdinalIgnoreCase))
                            {
                                SectionData[counter] = line.Trim();
                            }
                            else
                            {
                                LineData = line.Trim().Split('=');
                                if (LineData.Length == 2)
                                {
                                    SectionData[counter] = LineData[1].Trim();
                                }
                            }

                            counter++;
                        }
                    }

                    if (line.Trim().Equals("[End]", StringComparison.OrdinalIgnoreCase))
                    {
                        if (ReadingSection)
                        {
                            ReadingSection = false;
                            break;
                        }
                    }
                }
            }

            return SectionData;
        }
    }
}
