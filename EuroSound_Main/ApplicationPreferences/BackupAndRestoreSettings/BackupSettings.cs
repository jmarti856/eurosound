using Microsoft.Win32;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application.ApplicationRegistryFunctions
{
    internal class BackupSettings
    {
        public void Save(string SavePath, RegistryKey EuroSoundKey)
        {
            List<long> SectionOffsets = new List<long>();
            List<long> FinalSectionOffsets = new List<long>();

            using (BinaryStream BWriter = new BinaryStream(File.Open(SavePath, FileMode.Create, FileAccess.Write, FileShare.Read), null, Encoding.ASCII))
            {
                //--magic--
                BWriter.WriteBytes(Encoding.ASCII.GetBytes("ESRF"));
                //--WriteFileVersion--
                BWriter.WriteSByte(10);
                //--WirteNumberOfSubKeys
                uint NumberOfSubKeys = (uint)EuroSoundKey.SubKeyCount;
                BWriter.WriteUInt32(NumberOfSubKeys);
                //Align Bytes
                BWriter.Align(16);
                GenericFunctions.CustomSeek(32, BWriter, (byte)'«');

                //Write SubKeys Names
                foreach (string value in EuroSoundKey.GetSubKeyNames())
                {
                    //Subkey Name
                    BWriter.WriteString(value);

                    //Save Position
                    SectionOffsets.Add(BWriter.BaseStream.Position);

                    //SubKey Offset
                    BWriter.WriteUInt32(0);
                }

                //Align Bytes
                BWriter.Align(16);
                GenericFunctions.CustomSeek(128, BWriter, (byte)'«');

                //Subkeys Values
                foreach (string Value in EuroSoundKey.GetSubKeyNames())
                {
                    FinalSectionOffsets.Add(BWriter.BaseStream.Position);
                    using (RegistryKey KeyToSave = EuroSoundKey.OpenSubKey(Value))
                    {
                        if (KeyToSave != null)
                        {
                            BWriter.WriteUInt32((uint)KeyToSave.ValueCount);
                            foreach (string SubKeyValue in KeyToSave.GetValueNames())
                            {
                                byte ValueKind = 0;
                                string KeyType = KeyToSave.GetValueKind(SubKeyValue).ToString();
                                if (KeyType.Equals("DWord"))
                                {
                                    ValueKind = 1;
                                }
                                else if (KeyType.Equals("String"))
                                {
                                    ValueKind = 2;
                                }

                                //Write Key name
                                BWriter.WriteString(SubKeyValue);

                                //Write type of data that follows
                                BWriter.WriteByte(ValueKind);

                                //Write SubKey Value
                                if (ValueKind == 1)
                                {
                                    int NumericKeyValue = int.Parse(KeyToSave.GetValue(SubKeyValue).ToString());
                                    BWriter.WriteInt32(NumericKeyValue);
                                }
                                else if (ValueKind == 2)
                                {
                                    string KeyValue = KeyToSave.GetValue(SubKeyValue).ToString();
                                    BWriter.WriteString(KeyValue);
                                }
                            }
                        }
                        KeyToSave.Close();
                    };
                    //Align Bytes
                    BWriter.Align(16);
                }

                //Write Final Offsets
                for (int i = 0; i < SectionOffsets.Count; i++)
                {
                    BWriter.Seek((int)SectionOffsets[i], SeekOrigin.Begin);
                    BWriter.WriteUInt32((uint)FinalSectionOffsets[i]);
                }

                //Align Bytes
                BWriter.Seek(BWriter.BaseStream.Length);
                BWriter.Align(16);

                //Close
                BWriter.Close();
            }
        }
    }
}
