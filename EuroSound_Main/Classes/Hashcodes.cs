using EuroSound_Application.ApplicationPreferences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application.HashCodesFunctions
{
    internal static class Hashcodes
    {
        internal static Dictionary<uint, string> SB_Defines = new Dictionary<uint, string>();
        internal static List<float[]> SFX_Data = new List<float[]>();
        internal static Dictionary<uint, string> SFX_Defines = new Dictionary<uint, string>();
        internal static Dictionary<uint, string> MFX_Defines = new Dictionary<uint, string>();
        internal static Dictionary<uint, string> MFX_JumpCodes = new Dictionary<uint, string>();

        internal static void AddDataToCombobox(ComboBox ControlToAddData, Dictionary<uint, string> HashcodesDict)
        {
            //Datasource Combobox
            ControlToAddData.DataSource = HashcodesDict.OrderBy(o => o.Value).ToList();
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
            string HashcodeHex = "HASHCODE NOT FOUND";
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

        internal static void LoadSoundDataFile(string FilePath)
        {
            string Rootedpath = FilePath;

            //Combine path if required
            if (!Path.IsPathRooted(Rootedpath))
            {
                Rootedpath = Path.GetFullPath(Application.StartupPath + FilePath);
            }

            //Read Data
            if (File.Exists(Rootedpath))
            {
                ReadSFXData(Rootedpath);
                GlobalPreferences.HT_SoundsDataMD5 = GenericFunctions.CalculateMD5(FilePath);
            }
            else
            {
                MessageBox.Show(string.Join(" ", "Loading File:", Rootedpath, "\n", "\n", "Error:", FilePath, "was not found"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal static void LoadMusicHashcodes(string FilePath)
        {
            string Rootedpath = FilePath;

            //Combine path if required
            if (!Path.IsPathRooted(Rootedpath))
            {
                Rootedpath = Path.GetFullPath(Application.StartupPath + FilePath);
            }

            //Read Data
            if (File.Exists(Rootedpath))
            {
                ReadMusicHashcodes(Rootedpath);
                GlobalPreferences.HT_MusicMD5 = GenericFunctions.CalculateMD5(Rootedpath);
            }
            else
            {
                MessageBox.Show(string.Join(" ", "Loading File:", Rootedpath, "\n", "\n", "Error:", FilePath, "was not found"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        internal static void LoadSoundHashcodes(string FilePath)
        {
            string Rootedpath = FilePath;

            //Combine path if required
            if (!Path.IsPathRooted(Rootedpath))
            {
                Rootedpath = Path.GetFullPath(Application.StartupPath + FilePath);
            }

            //Read Data
            if (File.Exists(Rootedpath))
            {
                ReadHashcodes(Rootedpath);
                GlobalPreferences.HT_SoundsMD5 = GenericFunctions.CalculateMD5(Rootedpath);
            }
            else
            {
                MessageBox.Show(string.Join(" ", "Loading File:", Rootedpath, "\n", "\n", "Error:", FilePath, "was not found"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #region SFX Defines && SB Defines dictionary
        internal static void ReadHashcodes(string FilePath)
        {
            //Clear dictionaries
            SFX_Defines.Clear();
            SB_Defines.Clear();

            Regex FindHashcodeLabel = new Regex(@"\s+(\w+)");

            using (FileStream fs = File.OpenRead(FilePath))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader reader = new StreamReader(bs))
                    {
                        string CurrentLine;
                        while ((CurrentLine = reader.ReadLine()) != null)
                        {
                            if (CurrentLine.StartsWith("/") || string.IsNullOrEmpty(CurrentLine))
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = FindHashcodeLabel.Matches(CurrentLine);
                                if (matches.Count >= 2)
                                {
                                    string HexLabel = matches[0].Value.Trim();
                                    try
                                    {
                                        uint HexNum = Convert.ToUInt32(matches[1].Value.Trim(), 16);

                                        if (HexNum >= GlobalPreferences.SfxPrefix)
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
        }
        #endregion SFX Defines && SB Defines dictionary

        #region MFX Defines
        internal static void ReadMusicHashcodes(string filePath)
        {
            //Clear dictionaries
            MFX_Defines.Clear();

            Regex FindHashcodeLabel = new Regex(@"\s+(\w+)");

            using (FileStream fs = File.OpenRead(filePath))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader reader = new StreamReader(bs))
                    {
                        string currentLine;
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (currentLine.StartsWith("/") || string.IsNullOrEmpty(currentLine))
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = FindHashcodeLabel.Matches(currentLine);
                                if (matches.Count >= 2)
                                {
                                    string HexLabel = matches[0].Value.Trim();
                                    if (HexLabel.Equals("MFX_MaximumDefined"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            uint HexNum = Convert.ToUInt32(matches[1].Value.Trim(), 16);

                                            if (HexNum >= GlobalPreferences.MusicPrefix)
                                            {
                                                if (!MFX_Defines.ContainsKey(HexNum))
                                                {
                                                    MFX_Defines.Add(HexNum, HexLabel);
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
            }
        }

        internal static void ReadMusicJumpCodes()
        {
            //Clear dictionaries
            MFX_Defines.Clear();

            Regex FindHashcodeLabel = new Regex(@"\s+(\w+)");

            using (FileStream fs = File.OpenRead(GlobalPreferences.HT_MusicPath))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader reader = new StreamReader(bs))
                    {
                        string currentLine;
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (currentLine.StartsWith("/") || string.IsNullOrEmpty(currentLine))
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = FindHashcodeLabel.Matches(currentLine);
                                if (matches.Count >= 2)
                                {
                                    string HexLabel = matches[0].Value.Trim();
                                    if (HexLabel.StartsWith("JMP", StringComparison.OrdinalIgnoreCase))
                                    {
                                        try
                                        {
                                            uint HexNum = Convert.ToUInt32(matches[1].Value.Trim(), 16);

                                            if (HexNum >= GlobalPreferences.MusicPrefix)
                                            {
                                                if (!MFX_JumpCodes.ContainsKey(HexNum))
                                                {
                                                    MFX_JumpCodes.Add(HexNum, HexLabel);
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
            }
        }
        #endregion MFX Defines

        #region SFX DATA DICTIONARY
        internal static void ReadSFXData(string filePath)
        {
            SFX_Data.Clear();
            using (FileStream fs = File.OpenRead(filePath))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader reader = new StreamReader(bs))
                    {
                        string currentLine;
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (currentLine.StartsWith("/", StringComparison.OrdinalIgnoreCase) || currentLine.StartsWith("SF", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            else
                            {
                                string[] SplitedLine = currentLine.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
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
                                    SFX_Data.Add(ArrayOfValues);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion SFX DATA DICTIONARY
    }
}