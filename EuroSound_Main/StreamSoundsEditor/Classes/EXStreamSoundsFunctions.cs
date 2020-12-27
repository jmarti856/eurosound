using System.Collections.Generic;

namespace EuroSound_Application
{
    internal static class EXStreamSoundsFunctions
    {
        internal static void RemoveStreamedSound(string Name, Dictionary<uint, EXSoundStream> SoundsList)
        {
            EXSoundStream itemToRemove = GetSoundByName(uint.Parse(Name), SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(uint.Parse(Name));
            }
        }

        internal static EXSoundStream GetSoundByName(uint NameToSearch, Dictionary<uint, EXSoundStream> SoundsList)
        {
            EXSoundStream SearchedSound = null;

            if (SoundsList.ContainsKey(NameToSearch))
            {
                SearchedSound = SoundsList[NameToSearch];
                return SearchedSound;
            }

            return SearchedSound;
        }

        internal static bool SoundWillBeOutputed(Dictionary<uint, EXSoundStream> SoundsList, string SoundName)
        {
            bool Output = false;

            EXSoundStream Test = GetSoundByName(uint.Parse(SoundName), SoundsList);
            if (Test != null)
            {
                if (Test.OutputThisSound)
                {
                    Output = true;
                }
            }

            return Output;
        }
    }
}
