using System.Collections.Generic;

namespace EuroSound_Application.Musics
{
    internal static class EXMusicsFunctions
    {
        internal static EXMusic GetMusicByName(uint NameToSearch, Dictionary<uint, EXMusic> MusicsList)
        {
            EXMusic SearchedMusic = null;

            if (MusicsList.ContainsKey(NameToSearch))
            {
                SearchedMusic = MusicsList[NameToSearch];
                return SearchedMusic;
            }

            return SearchedMusic;
        }

        internal static void RemoveMusic(string Name, Dictionary<uint, EXMusic> MusicsList)
        {
            if (MusicsList.ContainsKey(uint.Parse(Name)))
            {
                MusicsList.Remove(uint.Parse(Name));
            }
        }

        internal static bool MusicWillBeOutputed(Dictionary<uint, EXMusic> SoundsList, string SoundName)
        {
            bool Output = false;

            EXMusic Test = GetMusicByName(uint.Parse(SoundName), SoundsList);
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
