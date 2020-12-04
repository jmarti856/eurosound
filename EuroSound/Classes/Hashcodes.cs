using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public static class Hashcodes
    {
        public static Dictionary<string, string> SFX_Defines = new Dictionary<string, string>();
        public static Dictionary<string, string> SB_Defines = new Dictionary<string, string>();
        public static Dictionary<string, double[]> SFX_Data = new Dictionary<string, double[]>();

        internal static void LoadSoundHashcodes()
        {
            if (File.Exists(EXFile.HT_SoundsPath))
            {
                ReadHashcodes(EXFile.HT_SoundsPath);
                EXFile.HT_SoundsMD5 = Generic.CalculateMD5(EXFile.HT_SoundsPath);
            }
            else
            {
                MessageBox.Show("ERROR: \"SFX_Defines.h\" not found. Please fix the file path before creating or modifying a soundbank.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static void LoadSoundDataFile()
        {
            if (File.Exists(EXFile.HT_SoundsDataPath))
            {
                ReadSFXData(EXFile.HT_SoundsDataPath);
                EXFile.HT_SoundsDataMD5 = Generic.CalculateMD5(EXFile.HT_SoundsDataPath);
            }
            else
            {
                MessageBox.Show("ERROR: \"SFX_Data.h\" not found. Please fix the file path before creating or modifying a soundbank.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region SFX Defines && SB Defines dictionary
        public static void ReadHashcodes(string FilePath)
        {
            string[] SplitedLine;
            string line;

            //Clear dictionaries
            SFX_Defines.Clear();
            SB_Defines.Clear();

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
                        else
                        {
                            if (line.Length > 1)
                            {
                                MessageBox.Show("Error reading line: " + line);
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

        internal static string GetHashcodeLabel(Dictionary<string, string> DataDict, string Hashcode)
        {
            string HashcodeHex = string.Empty;
            foreach (KeyValuePair<string, string> Entry in DataDict)
            {
                if (Entry.Key.ToUpper().Equals(Hashcode.ToUpper()))
                {
                    HashcodeHex = Entry.Value;
                }
            }

            return HashcodeHex;
        }

        internal static void AddHashcodesToCombobox(ComboBox ControlToAddData, Dictionary<string, string> HashcodesDict)
        {
            /*Datasource Combobox*/
            ControlToAddData.DataSource = HashcodesDict.ToList();
            ControlToAddData.ValueMember = "Key";
            ControlToAddData.DisplayMember = "Value";
        }
        #endregion

        #region SFX DATA DICTIONARY
        internal static void ReadSFXData(string FilePath)
        {
            string[] SplitedLine;
            string line;
            double[] ArrayOfValues;

            SFX_Data.Clear();

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (StreamReader reader = new StreamReader(fs))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("//") || line.StartsWith("SFXOutputDetails"))
                    {
                        continue;
                    }
                    else
                    {
                        SplitedLine = line.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
                        if (SplitedLine.Length >= 9)
                        {
                            ArrayOfValues = new double[8];
                            ArrayOfValues[0] = StringFloatToDouble(SplitedLine[0]);
                            ArrayOfValues[1] = StringFloatToDouble(SplitedLine[1]);
                            ArrayOfValues[2] = StringFloatToDouble(SplitedLine[2]);
                            ArrayOfValues[3] = StringFloatToDouble(SplitedLine[3]);
                            ArrayOfValues[4] = StringFloatToDouble(SplitedLine[4]);
                            ArrayOfValues[5] = StringFloatToDouble(SplitedLine[5]);
                            ArrayOfValues[6] = StringFloatToDouble(SplitedLine[6]);
                            ArrayOfValues[7] = StringFloatToDouble(SplitedLine[7]);
                            string Hashcode = GetHashcodeByLabel(SFX_Defines, SplitedLine[SplitedLine.Length - 1].Split(new[] { "//" }, StringSplitOptions.None)[1].Trim());
                            if (!SFX_Data.ContainsKey(Hashcode))
                            {

                                SFX_Data.Add(Hashcode, ArrayOfValues);
                            }
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
            }
        }

        private static double StringFloatToDouble(string Number)
        {
            double FinalNumber;
            string num;

            try
            {
                NumberFormatInfo provider = new NumberFormatInfo
                {
                    NumberDecimalSeparator = "."
                };

                num = Number.Replace("f", string.Empty).Trim();
                FinalNumber = double.Parse(num, provider);
            }
            catch
            {
                FinalNumber = 0.0;
            }

            return FinalNumber;
        }
        #endregion
    }
}
