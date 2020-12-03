using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EuroSound
{
    public static class Hashcodes
    {
        public static Dictionary<string, string> SFX_Defines = new Dictionary<string, string>();
        public static Dictionary<string, string> SB_Defines = new Dictionary<string, string>();

        public static void ReadHashcodes(string FilePath)
        {
            string[] SplitedLine;
            string line;

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (StreamReader reader = new StreamReader(fs))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("/"))
                    {
                        Debug.WriteLine("WARNING -- line " + line + " skipped.");
                        continue;
                    }
                    else
                    {
                        SplitedLine = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (SplitedLine.Length > 1)
                        {
                            if (SplitedLine[1].ToString().StartsWith("SFX_"))
                            {
                                AddItemToDictionary(SFX_Defines, SplitedLine[2], SplitedLine[1]);
                            }
                            else if (SplitedLine[1].ToString().StartsWith("SB_"))
                            {
                                AddItemToDictionary(SB_Defines, SplitedLine[2], SplitedLine[1]);
                            }
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
            }
        }

        private static void AddItemToDictionary(Dictionary<string, string> DataDict, string Key, string Value)
        {
            if (!DataDict.ContainsKey(Key))
            {
                DataDict.Add(Key, Value);
                Debug.WriteLine(string.Format("INFO -- {0} {1} added to the SFX_Defines dictionary.", Key, Value));
            }
        }

        internal static string GetHashcodeByLabel(Dictionary<string, string> DataDict, string Hashcode)
        {
            string HashcodeHex = string.Empty;
            foreach (KeyValuePair<string, string> Entry in DataDict)
            {
                if (Entry.Value.ToUpper().Equals(Hashcode.ToUpper()))
                {
                    HashcodeHex = Entry.Key;
                }
            }

            return HashcodeHex;
        }
    }
}
