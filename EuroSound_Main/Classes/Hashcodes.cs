using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal static class Hashcodes
    {
        internal static Dictionary<uint, string> SB_Defines = new Dictionary<uint, string>();
        internal static Dictionary<uint, float[]> SFX_Data = new Dictionary<uint, float[]>();
        internal static Dictionary<uint, string> SFX_Defines = new Dictionary<uint, string>();

        internal static void AddHashcodesToCombobox(ComboBox ControlToAddData, Dictionary<uint, string> HashcodesDict)
        {
            /*Datasource Combobox*/
            ControlToAddData.DataSource = HashcodesDict.ToList();
            ControlToAddData.ValueMember = "Key";
            ControlToAddData.DisplayMember = "Value";
        }

        internal static uint GetHashcodeByLabel(Dictionary<uint, string> DataDict, string Hashcode)
        {
            uint HashcodeHex = 0x00000000;
            foreach (KeyValuePair<uint, string> Entry in DataDict)
            {
                if (Entry.Value.Equals(Hashcode, StringComparison.OrdinalIgnoreCase))
                {
                    HashcodeHex = Entry.Key;
                    break;
                }
            }

            return HashcodeHex;
        }

        internal static string GetHashcodeLabel(Dictionary<uint, string> DataDict, uint Hashcode)
        {
            string HashcodeHex = string.Empty;
            foreach (KeyValuePair<uint, string> Entry in DataDict)
            {
                if (Entry.Key == Hashcode)
                {
                    HashcodeHex = Entry.Value;
                    break;
                }
            }

            return HashcodeHex;
        }

        internal static void LoadSoundDataFile()
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

        internal static void LoadSoundHashcodes(string SoundHashcodesPath)
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
        #region SFX Defines && SB Defines dictionary

        internal static void ReadHashcodes(string FilePath)
        {
            string line;
            uint HexNum;
            string HexLabel;

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
                        HexNum = Convert.ToUInt32(matches[1].Value.Trim(), 16);

                        if (HexNum >= 436207616)
                        {
                            if (HexLabel.StartsWith("SF", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!SFX_Defines.ContainsKey(HexNum))
                                {
                                    SFX_Defines.Add(HexNum, HexLabel);
                                }
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
            reader.Close();
            reader.Dispose();

            bs.Close();
            bs.Dispose();

            fs.Close();
            fs.Dispose();
        }

        #endregion SFX Defines && SB Defines dictionary

        #region SFX DATA DICTIONARY

        internal static void ReadSFXData()
        {
            string[] SplitedLine;
            string line, hashcode;

            SFX_Data.Clear();

            FileStream fs = new FileStream(GlobalPreferences.HT_SoundsDataPath, FileMode.Open, FileAccess.Read);
            BufferedStream bs = new BufferedStream(fs);
            StreamReader reader = new StreamReader(bs);
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("/", StringComparison.OrdinalIgnoreCase) || line.StartsWith("SFX", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                else
                {
                    SplitedLine = line.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
                    if (SplitedLine.Length >= 9)
                    {
                        float[] ArrayOfValues = new float[8];
                        ArrayOfValues[0] = StringFloatToDouble(SplitedLine[0]);
                        ArrayOfValues[1] = StringFloatToDouble(SplitedLine[1]);
                        ArrayOfValues[2] = StringFloatToDouble(SplitedLine[2]);
                        ArrayOfValues[3] = StringFloatToDouble(SplitedLine[3]);
                        ArrayOfValues[4] = StringFloatToDouble(SplitedLine[4]);
                        ArrayOfValues[5] = StringFloatToDouble(SplitedLine[5]);
                        ArrayOfValues[6] = StringFloatToDouble(SplitedLine[6]);
                        ArrayOfValues[7] = StringFloatToDouble(SplitedLine[7]);
                        hashcode = "0x1A" + (int.Parse(ArrayOfValues[0].ToString()).ToString("X8").Substring(2));
                        if (!SFX_Data.ContainsKey(Convert.ToUInt32(hashcode, 16)))
                        {
                            SFX_Data.Add(Convert.ToUInt32(hashcode, 16), ArrayOfValues);
                        }
                    }
                }
            }
            reader.Close();
            reader.Dispose();

            bs.Close();
            bs.Dispose();

            fs.Close();
            fs.Dispose();
        }

        private static float StringFloatToDouble(string Number)
        {
            float FinalNumber;
            string num;

            try
            {
                num = Number.Replace("f", string.Empty).Trim();
                FinalNumber = float.Parse(num, CultureInfo.GetCultureInfo("en-US"));
            }
            catch
            {
                FinalNumber = 0.0f;
            }

            return FinalNumber;
        }

        #endregion SFX DATA DICTIONARY
    }
}