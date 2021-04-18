﻿using System.Collections.Generic;

namespace EuroSound_Application.StreamSounds
{
    internal static class EXStreamSoundsFunctions
    {
        internal static void RemoveStreamedSound(string Name, Dictionary<uint, EXSoundStream> SoundsList)
        {
            if (SoundsList.ContainsKey(uint.Parse(Name)))
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

            EXSoundStream ObjectToCheck = GetSoundByName(uint.Parse(SoundName), SoundsList);
            if (ObjectToCheck != null)
            {
                if (ObjectToCheck.OutputThisSound)
                {
                    Output = true;
                }
            }

            return Output;
        }

        internal static string GetMarkerType(uint MarkerValue)
        {
            string MType;

            switch (MarkerValue)
            {
                case 10:
                    MType = "Start";
                    break;
                case 9:
                    MType = "End";
                    break;
                case 7:
                    MType = "Goto";
                    break;
                case 6:
                    MType = "Loop";
                    break;
                case 5:
                    MType = "Pause";
                    break;
                default:
                    MType = "Jump";
                    break;
            }

            return MType;
        }
    }
}
