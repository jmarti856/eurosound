using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace EuroSound_Application.ApplicationPreferences.Ini_File
{
    class IniFile_Functions
    {
        internal Dictionary<string, string> GetAvailableProfiles(string Path)
        {
            Regex RemoveCharactersFromPathString = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");
            Regex PatternForProjects = new Regex(@"(?<=\[).+?(?=\])");
            MatchCollection RegexMatches;
            Dictionary<string, string> ProfilesDictionary = new Dictionary<string, string>();
            string[] LineData;
            string ProjectName = string.Empty, ESP = string.Empty;

            IEnumerable<string> lines = File.ReadLines(Path);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                else
                {
                    //Get project name
                    if (line.Trim().StartsWith("["))
                    {
                        RegexMatches = PatternForProjects.Matches(line);
                        if (RegexMatches.Count > 0)
                        {
                            ProjectName = RegexMatches[0].ToString();
                        }
                    }

                    //Get path
                    if (line.Trim().StartsWith("UseESP"))
                    {
                        LineData = line.Split('=');
                        if (LineData.Length > 1)
                        {
                            ESP = LineData[1];
                        }
                    }

                    //Add data if not contains
                    if (!string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(ESP))
                    {
                        if (!ProfilesDictionary.ContainsKey(ProjectName))
                        {
                            ProfilesDictionary.Add(ProjectName, RemoveCharactersFromPathString.Replace(ESP, ""));
                            ESP = string.Empty;
                            ProjectName = string.Empty;
                        }
                    }
                }
            }

            return ProfilesDictionary;
        }
    }
}
