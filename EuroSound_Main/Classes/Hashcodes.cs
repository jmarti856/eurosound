using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public static class Hashcodes
    {
        public static void LoadSoundHashcodes(string SoundHashcodesPath, Dictionary<string, string> SFX_Defines, Dictionary<string, string> SB_Defines, ResourceManager ResxM)
        {
            if (File.Exists(SoundHashcodesPath))
            {
                /*Read Data*/
                ReadHashcodes(SoundHashcodesPath, SFX_Defines, SB_Defines);
                GlobalPreferences.HT_SoundsMD5 = GenericFunctions.CalculateMD5(SoundHashcodesPath);
            }
            else
            {
                MessageBox.Show(ResxM.GetString("Hashcodes_SFXDefines_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LoadSoundDataFile(string SoundHashcodespath, Dictionary<string, double[]> SFX_Data, Dictionary<string, string> SFX_Defines, ResourceManager ResxM)
        {
            if (File.Exists(SoundHashcodespath))
            {
                /*Read Data*/
                ReadSFXData(SoundHashcodespath, SFX_Data, SFX_Defines);
                GlobalPreferences.HT_SoundsDataMD5 = GenericFunctions.CalculateMD5(SoundHashcodespath);
            }
            else
            {
                MessageBox.Show(ResxM.GetString("Hashcodes_SFXData_NotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region SFX Defines && SB Defines dictionary
        public static void ReadHashcodes(string FilePath, Dictionary<string, string> SFX_Defines, Dictionary<string, string> SB_Defines)
        {
            string line;
            string HexNum, HexLabel;

            //Clear dictionaries
            SFX_Defines.Clear();
            SB_Defines.Clear();

            //Regex FindHexNumber = new Regex(@"(0[xX][A-Fa-f0-9]+;?)+$");
            Regex FindHashcodeLabel = new Regex(@"\s+(\w+)");

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
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
        }
        #endregion

        #region SFX DATA DICTIONARY
        internal static void ReadSFXData(string FilePath, Dictionary<string, double[]> SFX_Data, Dictionary<string, string> SFX_Defines)
        {
            string[] SplitedLine;
            string line;
            double[] ArrayOfValues;

            SFX_Data.Clear();

            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader reader = new StreamReader(fs);

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("//", StringComparison.OrdinalIgnoreCase) || line.StartsWith("SFXOutputDetails", StringComparison.OrdinalIgnoreCase))
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

            fs.Close();
            fs.Dispose();
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
