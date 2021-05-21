using EuroSound_Application.CurrentProjectFunctions;
using System;
using System.Collections.Generic;

namespace EuroSound_Application.ApplicationTargets
{
    internal static class EXAppTarget_Functions
    {
        internal static string GetBinaryName(ProjectFile currentProjectInfo, string ProfileName)
        {
            string fileName = string.Empty;

            if (ProfileName.Equals("Sphinx", StringComparison.OrdinalIgnoreCase))
            {
                if (currentProjectInfo.TypeOfData == (int)GenericFunctions.ESoundFileType.SoundBanks || currentProjectInfo.TypeOfData == (int)GenericFunctions.ESoundFileType.StreamSounds)
                {
                    fileName = string.Join("", "HC", currentProjectInfo.Hashcode.ToString("X8").Substring(2), ".SFX");
                }
                else
                {
                    fileName = string.Join("", "HCE", (currentProjectInfo.Hashcode & 0x000fffff).ToString("X8").Substring(3), ".SFX");
                }
            }
            return fileName;
        }

        internal static void UpdateAppTargetName(string NewTarget, Dictionary<uint, EXAppTarget> outputTargets)
        {
            foreach (EXAppTarget storedTarget in outputTargets.Values)
            {
                storedTarget.BinaryName = NewTarget;
            }
        }

        internal static EXAppTarget ReturnTargetFromDictionary(uint targetKey, Dictionary<uint, EXAppTarget> targetsList)
        {
            //REnamed to ReturnSoundFromDictionary
            EXAppTarget searchedSound = null;

            if (targetsList.ContainsKey(targetKey))
            {
                searchedSound = targetsList[targetKey];
                return searchedSound;
            }

            return searchedSound;
        }
    }
}
