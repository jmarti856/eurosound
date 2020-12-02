using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EuroSound
{
    public static class EXFunctions
    {
        internal static bool SoundExistsInList(string ItemToSearch, List<EXSound> SoundsList)
        {
            bool ItemExists = false;
            foreach (EXSound item in SoundsList)
            {
                if (item.Name.Equals(ItemToSearch))
                {
                    ItemExists = true;
                    break;
                }
            }

            return ItemExists;
        }

        public static bool SoundContainsSample(EXSound Sound, string SampleName)
        {
            bool ItemExists = false;
            foreach (EXSample item in Sound.Samples)
            {
                if (item.Name.Equals(SampleName))
                {
                    ItemExists = true;
                    break;
                }
            }

            return ItemExists;
        }

        internal static void RemoveSampleFromSound(EXSound SoundName, string SampleName)
        {
            EXSample SampleToRemove = TreeNodeFunctions.GetSelectedSample(SoundName, SampleName);
            if (SampleToRemove != null)
            {
                SoundName.Samples.Remove(SampleToRemove);
                Debug.WriteLine(string.Format("INFO -- Object {0} removed from the list.", SampleToRemove.Name));
            }
            else
            {
                Debug.WriteLine(string.Format("WARNING -- Object {0} not found in the list.", SampleName));
            }
        }

        internal static void RemoveSound(string Name, List<EXSound> SoundsList)
        {
            EXSound itemToRemove = TreeNodeFunctions.GetSelectedSound(Name, SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(itemToRemove);
                Debug.WriteLine(string.Format("INFO -- Object {0} removed from the list.", itemToRemove.Name));
            }
            else
            {
                Debug.WriteLine(string.Format("WARNING -- Object {0} not found in the list.", Name));
            }
        }

        internal static bool NewSound(string v_Name, string v_DisplayName, List<EXSound> SoundsList)
        {
            bool AddedCorrectly = false;

            EXSound Sound = new EXSound
            {
                Name = RemoveWhiteSpaces(v_Name),
                DisplayName = v_DisplayName
            };

            /*Check if already exists*/
            if (SoundExistsInList(Sound.Name, SoundsList))
            {
                Debug.WriteLine(string.Format("ERROR -- Sound {0} already exists.", Sound.Name));
            }
            else
            {
                SoundsList.Add(Sound);
                AddedCorrectly = true;

                Debug.WriteLine(string.Format("INFO -- Sound {0} added to the list.", Sound.Name));
            }

            return AddedCorrectly;
        }

        internal static bool AddSampleToSound(EXSound Sound, string SampleName)
        {
            bool AddedCorrectly = false;

            EXSample Sample = new EXSample
            {
                Name = RemoveWhiteSpaces(SampleName),
                DisplayName = SampleName
            };

            /*--Check that Sound is not null*/
            if (Sound != null)
            {
                /*Check if already exists*/
                if (SoundContainsSample(Sound, Sample.Name))
                {
                    Debug.WriteLine(string.Format("ERROR -- Sample {0} already exists.", SampleName));
                }
                else
                {
                    Sound.Samples.Add(Sample);
                    AddedCorrectly = true;
                    Debug.WriteLine(string.Format("INFO -- Sample {0} added to the sound {1}.", SampleName, Sound.DisplayName));
                }
            }
            else
            {
                Debug.WriteLine(string.Format("ERROR -- Seems that there's no sound selected."));
            }

            return AddedCorrectly;
        }

        internal static string RemoveWhiteSpaces(string TextToModify)
        {
            string NewString = string.Empty;

            /*Before remove whitespaces, first check that is not null*/
            if (!(string.IsNullOrEmpty(TextToModify)))
            {
                NewString = Regex.Replace(TextToModify, @"\s", "");
            }

            return NewString;
        }
    }
}
