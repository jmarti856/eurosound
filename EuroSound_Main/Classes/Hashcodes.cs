using EuroSound_Application.ApplicationPreferences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal static class Hashcodes
    {
        internal static SortedList<uint, string> SB_Defines = new SortedList<uint, string>();
        internal static SortedList<int, float[]> SFX_Data = new SortedList<int, float[]>();
        internal static SortedList<uint, string> SFX_Defines = new SortedList<uint, string>();

        internal static void AddDataToCombobox(ComboBox ControlToAddData, SortedList<uint, string> HashcodesDict)
        {
            //Datasource Combobox
            ControlToAddData.DataSource = HashcodesDict.ToList();
            if (ControlToAddData.InvokeRequired)
            {
                ControlToAddData.Invoke((MethodInvoker)delegate
                {
                    ControlToAddData.ValueMember = "Key";
                    ControlToAddData.DisplayMember = "Value";
                });
            }
            else
            {
                ControlToAddData.ValueMember = "Key";
                ControlToAddData.DisplayMember = "Value";
            }
        }

        internal static uint GetHashcodeByLabel(SortedList<uint, string> DataDict, string Hashcode)
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

        internal static string GetHashcodeLabel(SortedList<uint, string> DataDict, uint Hashcode)
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
                //Read Data
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
                //Read Data
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

            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader reader = new StreamReader(bs))
                    {
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
                                    try
                                    {
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
                                        else if (HexLabel.StartsWith("SB_", StringComparison.OrdinalIgnoreCase) || HexLabel.StartsWith("StreamFileHashCode", StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (!SB_Defines.ContainsKey(HexNum))
                                            {
                                                SB_Defines.Add(HexNum, HexLabel);
                                            }
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        MessageBox.Show(string.Join(" ", new string[] { "A hashcode with an invalid hex format has been found, the label is:", HexLabel }), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            SB_Defines.TrimExcess();
        }
        #endregion SFX Defines && SB Defines dictionary

        #region SFX DATA DICTIONARY
        internal static void ReadSFXData()
        {
            int ItemKey = 0;
            string[] SplitedLine;
            string line;

            SFX_Data.Clear();
            using (FileStream fs = new FileStream(GlobalPreferences.HT_SoundsDataPath, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader reader = new StreamReader(bs))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("/", StringComparison.OrdinalIgnoreCase) || line.StartsWith("SF", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            else
                            {
                                SplitedLine = line.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
                                if (SplitedLine.Length >= 9)
                                {
                                    float[] ArrayOfValues = new float[8];
                                    ArrayOfValues[0] = GenericFunctions.StringFloatToDouble(SplitedLine[0]);
                                    ArrayOfValues[1] = GenericFunctions.StringFloatToDouble(SplitedLine[1]);
                                    ArrayOfValues[2] = GenericFunctions.StringFloatToDouble(SplitedLine[2]);
                                    ArrayOfValues[3] = GenericFunctions.StringFloatToDouble(SplitedLine[3]);
                                    ArrayOfValues[4] = GenericFunctions.StringFloatToDouble(SplitedLine[4]);
                                    ArrayOfValues[5] = GenericFunctions.StringFloatToDouble(SplitedLine[5]);
                                    ArrayOfValues[6] = GenericFunctions.StringFloatToDouble(SplitedLine[6]);
                                    ArrayOfValues[7] = GenericFunctions.StringFloatToDouble(SplitedLine[7]);

                                    SFX_Data.Add(ItemKey, ArrayOfValues);
                                }
                            }

                            ItemKey++;
                        }
                    }
                }
            }
            SFX_Data.TrimExcess();
        }
        #endregion SFX DATA DICTIONARY
    }
}