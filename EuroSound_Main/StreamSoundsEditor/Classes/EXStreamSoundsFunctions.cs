using System.Collections.Generic;

namespace EuroSound_Application
{
    internal static class EXStreamSoundsFunctions
    {
        internal static void RemoveStreamedSound(string Name, Dictionary<int, EXSoundStream> SoundsList)
        {
            EXSoundStream itemToRemove = GetSoundByName(int.Parse(Name), SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(int.Parse(Name));
            }
        }

        internal static EXSoundStream GetSoundByName(int NameToSearch, Dictionary<int, EXSoundStream> SoundsList)
        {
            EXSoundStream SearchedSound = null;

            if (SoundsList.ContainsKey(NameToSearch))
            {
                SearchedSound = SoundsList[NameToSearch];
                return SearchedSound;
            }

            return SearchedSound;
        }
    }
}
