using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferences.Ini_File
{
    internal class IniFile_Functions
    {
        internal Dictionary<string, string> GetAvailableProfiles(string iniFilePath)
        {
            string ProjectName = string.Empty, ProjectFilePath = string.Empty;
            Regex RemoveCharactersFromPathString = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
            Regex PatternForProjects = new Regex(@"(?<=\[).+?(?=\])");
            Dictionary<string, string> ProfilesDictionary = new Dictionary<string, string>();

            IEnumerable<string> FileLines = File.ReadLines(iniFilePath);
            foreach (string CurrentLine in FileLines)
            {
                if (string.IsNullOrEmpty(CurrentLine))
                {
                    continue;
                }
                else
                {
                    //[Project Name]
                    if (CurrentLine.Trim().StartsWith("["))
                    {
                        MatchCollection RegexMatches = PatternForProjects.Matches(CurrentLine);
                        if (RegexMatches.Count > 0)
                        {
                            ProjectName = RegexMatches[0].ToString().Trim();
                        }
                    }

                    //[Use Profile]
                    if (CurrentLine.Trim().StartsWith("UseESP", System.StringComparison.OrdinalIgnoreCase))
                    {
                        string[] LineData = CurrentLine.Split('=');
                        if (LineData.Length > 1)
                        {
                            ProjectFilePath = LineData[1].Trim();
                        }

                        //Add data to dictionary
                        if (!string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(ProjectFilePath))
                        {
                            if (!ProfilesDictionary.ContainsKey(ProjectName))
                            {
                                string projectFilePath = RemoveCharactersFromPathString.Replace(ProjectFilePath, "");
                                string rootedFilePath = projectFilePath;
                                if (!Path.IsPathRooted(rootedFilePath))
                                {
                                    rootedFilePath = Path.GetFullPath(Application.StartupPath + rootedFilePath);
                                }
                                ProfilesDictionary.Add(ProjectName, rootedFilePath);
                                ProjectFilePath = string.Empty;
                                ProjectName = string.Empty;
                            }
                        }
                    }
                }
            }
            return ProfilesDictionary;
        }

        internal List<string[]> GetAudioConverterPresets(string FilePath)
        {
            List<string[]> AvailablePresets = new List<string[]>();

            IEnumerable<string> FileLines = File.ReadLines(FilePath);
            foreach (string CurrentLine in FileLines)
            {
                if (string.IsNullOrEmpty(CurrentLine))
                {
                    continue;
                }
                else
                {
                    if (CurrentLine.Trim().StartsWith("ACPresets", System.StringComparison.OrdinalIgnoreCase))
                    {
                        string[] LineData = CurrentLine.Split('=');
                        if (LineData.Length > 1)
                        {
                            MatchCollection Preset = Regex.Matches(LineData[1], @"\{\s*""(.*)""\s*,\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*,\s*""(.*)""\s*\}");
                            if (Preset.Count > 0 && Preset[0].Groups.Count == 6)
                            {
                                AvailablePresets.Add(new string[] { Preset[0].Groups[1].Value.Trim(), Preset[0].Groups[2].Value, Preset[0].Groups[3].Value, Preset[0].Groups[4].Value, Preset[0].Groups[5].Value.Trim() });
                            }
                        }
                    }
                }
            }
            return AvailablePresets;
        }
    }
}
