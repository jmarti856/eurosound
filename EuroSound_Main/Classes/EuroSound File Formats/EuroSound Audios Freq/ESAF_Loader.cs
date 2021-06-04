using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EuroSound_Application.EuroSound_Audio_File
{
    internal class ESAF_Loader
    {
        private List<string> ImportResults = new List<string>();

        internal IList<string> LoadSFX_File(string FilePath, ProjectFile FileProperties, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList)
        {
            string[] FileLines = File.ReadAllLines(FilePath);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESIFFile"));

            //Check file is not empty
            if (FileLines.Length > 0)
            {
                //Check file is correct
                if (FileLines[0].Equals("*EUROSOUND_PS2_AUDIO_FREQUENCIES V1.0"))
                {
                    for (int i = 1; i < FileLines.Length; i++)
                    {
                        string[] LineInfo = FileLines[i].Trim().Split(':');
                        if (string.IsNullOrEmpty(LineInfo[0]) || LineInfo[0].StartsWith("*COMMENT"))
                        {
                            continue;
                        }
                        else
                        {
                            if (LineInfo.Length == 2)
                            {
                                if (FileLines[i].Contains("{"))
                                {
                                    uint HashCode = GlobalPreferences.SfxPrefix | uint.Parse(LineInfo[0]);
                                    while (!FileLines[i].Contains("}"))
                                    {
                                        i++;
                                        LineInfo = FileLines[i].Trim().Split(':');
                                        if (LineInfo.Length == 2)
                                        {
                                            uint Sample = uint.Parse(LineInfo[0]);
                                            uint Rate = uint.Parse(LineInfo[1]);

                                            foreach (KeyValuePair<uint, EXSound> item in SoundsList)
                                            {
                                                if (item.Value.Hashcode == HashCode)
                                                {
                                                    if (item.Value.Samples.Count > Sample)
                                                    {
                                                        EXSample sampleToCheck = item.Value.Samples.ElementAt((int)Sample).Value;
                                                        if (AudiosList.ContainsKey(sampleToCheck.ComboboxSelectedAudio))
                                                        {
                                                            EXAudio audioToModify = AudiosList[sampleToCheck.ComboboxSelectedAudio];
                                                            audioToModify.FrequencyPS2 = Rate;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ImportResults.Add(string.Join("", "0", "Error the file: ", FilePath, " is not valid"));
                }
            }

            //Update project status variable
            FileProperties.FileHasBeenModified = true;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            return ImportResults;
        }
    }
}
