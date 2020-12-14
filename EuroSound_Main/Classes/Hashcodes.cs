﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public static class Hashcodes
    {
        public static Dictionary<string, string> SFX_Defines = new Dictionary<string, string>();
        public static Dictionary<string, string> SB_Defines = new Dictionary<string, string>();
        public static Dictionary<string, double[]> SFX_Data = new Dictionary<string, double[]>();

        public static void LoadSoundHashcodes(string SoundHashcodesPath)
        {
            if (File.Exists(SoundHashcodesPath))
            {
                /*Read Data*/
                ReadHashcodes(SoundHashcodesPath);
                GlobalPreferences.HT_SoundsMD5 = GenericFunctions.CalculateMD5(SoundHashcodesPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Hashcodes_SFXDefines_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LoadSoundDataFile()
        {
            if (File.Exists(GlobalPreferences.HT_SoundsDataPath))
            {
                /*Read Data*/
                ReadSFXData();
                GlobalPreferences.HT_SoundsDataMD5 = GenericFunctions.CalculateMD5(GlobalPreferences.HT_SoundsDataPath);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Hashcodes_SFXData_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region SFX Defines && SB Defines dictionary
        public static void ReadHashcodes(string FilePath)
        {
            string line;
            string HexNum, HexLabel;

            //Clear dictionaries
            SFX_Defines.Clear();
            SB_Defines.Clear();

            //Regex FindHexNumber = new Regex(@"(0[xX][A-Fa-f0-9]+;?)+$");
            Regex FindHashcodeLabel = new Regex(@"\s+(\w+)");

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BufferedStream bs = new BufferedStream(fs);
            StreamReader reader = new StreamReader(bs);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("/"))
                {
                    continue;
                }
                else
                {
                    MatchCollection matches = FindHashcodeLabel.Matches(line);
                    if (matches.Count >= 2)
                    {
                        HexLabel = matches[0].Value.Trim();
                        HexNum = matches[1].Value.Trim();

                        if (HexNum.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        {
                            if (HexLabel.StartsWith("SF", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!SFX_Defines.ContainsKey(HexNum))
                                {
                                    SFX_Defines.Add(HexNum, HexLabel);
                                }
                            }
                            else if (HexLabel.StartsWith("SB_", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!SB_Defines.ContainsKey(HexNum))
                                {
                                    SB_Defines.Add(HexNum, HexLabel);
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            reader.DiscardBufferedData();
            reader.Dispose();

            bs.Close();
            bs.Dispose();

            fs.Close();
            fs.Dispose();
        }
        #endregion

        #region SFX DATA DICTIONARY
        internal static void ReadSFXData()
        {
            string[] SplitedLine;
            string line, hashcode;
            double[] ArrayOfValues = new double[8];

            SFX_Data.Clear();

            FileStream fs = new FileStream(GlobalPreferences.HT_SoundsDataPath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("/", StringComparison.OrdinalIgnoreCase) || line.StartsWith("SFXO", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                else
                {
                    SplitedLine = line.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
                    if (SplitedLine.Length >= 9)
                    {
                        ArrayOfValues[0] = StringFloatToDouble(SplitedLine[0]);
                        ArrayOfValues[1] = StringFloatToDouble(SplitedLine[1]);
                        ArrayOfValues[2] = StringFloatToDouble(SplitedLine[2]);
                        ArrayOfValues[3] = StringFloatToDouble(SplitedLine[3]);
                        ArrayOfValues[4] = StringFloatToDouble(SplitedLine[4]);
                        ArrayOfValues[5] = StringFloatToDouble(SplitedLine[5]);
                        ArrayOfValues[6] = StringFloatToDouble(SplitedLine[6]);
                        ArrayOfValues[7] = StringFloatToDouble(SplitedLine[7]);
                        hashcode = GetHashcodeByLabel(SFX_Defines, SplitedLine[SplitedLine.Length - 1].Split(new[] { "//" }, StringSplitOptions.None)[1].Trim());
                        if (!SFX_Data.ContainsKey(hashcode))
                        {
                            SFX_Data.Add(hashcode, ArrayOfValues);
                        }
                    }
                }
            }
            reader.Close();
            reader.DiscardBufferedData();
            reader.Dispose();

            fs.Close();
            fs.Dispose();
        }

        private static double StringFloatToDouble(string Number)
        {
            double FinalNumber;
            string num;

            try
            {
                num = Number.Replace("f", string.Empty).Trim();
                FinalNumber = double.Parse(num, CultureInfo.GetCultureInfo("en-US"));
            }
            catch
            {
                FinalNumber = 0.0;
            }

            return FinalNumber;
        }
        #endregion

        public static string GetHashcodeByLabel(Dictionary<string, string> DataDict, string Hashcode)
        {
            string HashcodeHex = string.Empty;
            foreach (KeyValuePair<string, string> Entry in DataDict)
            {
                if (Entry.Value.ToUpper().Equals(Hashcode.ToUpper()))
                {
                    HashcodeHex = Entry.Key;
                    break;
                }
            }

            return HashcodeHex;
        }

        public static string GetHashcodeLabel(Dictionary<string, string> DataDict, string Hashcode)
        {
            string HashcodeHex = string.Empty;
            foreach (KeyValuePair<string, string> Entry in DataDict)
            {
                if (Entry.Key.ToUpper().Equals(Hashcode.ToUpper()))
                {
                    HashcodeHex = Entry.Value;
                    break;
                }
            }

            return HashcodeHex;
        }

        public static void AddHashcodesToCombobox(ComboBox ControlToAddData, Dictionary<string, string> HashcodesDict)
        {
            /*Datasource Combobox*/
            ControlToAddData.DataSource = HashcodesDict.ToList();
            ControlToAddData.ValueMember = "Key";
            ControlToAddData.DisplayMember = "Value";
        }
    }
}
