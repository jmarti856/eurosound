using Microsoft.Win32;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application.ApplicationRegistryFunctions
{
    class BackupSettings
    {
        public void Save(string SavePath, RegistryKey EuroSoundKey)
        {
            long AlignOffset;
            uint NumberOfSubKeys;
            byte ValueKind = 0;
            string KeyName, KeyType, KeyValue;
            int NumericKeyValue;
            List<long> SectionOffsets = new List<long>();
            List<long> FinalSectionOffsets = new List<long>();

            using (BinaryStream BWriter = new BinaryStream(File.Open(SavePath, FileMode.Create, FileAccess.Write, FileShare.Read), null, Encoding.ASCII))
            {
                //--magic--
                BWriter.Write(Encoding.ASCII.GetBytes("ESRF"));
                //--WriteFileVersion--
                BWriter.Write((sbyte)10);
                //--WirteNumberOfSubKeys
                NumberOfSubKeys = (uint)EuroSoundKey.SubKeyCount;
                BWriter.Write(NumberOfSubKeys);


                //Write SubKeys Names
                foreach (string value in EuroSoundKey.GetSubKeyNames())
                {
                    //Subkey Name
                    BWriter.Write(value);

                    //Save Position
                    SectionOffsets.Add(BWriter.BaseStream.Position);

                    //SubKey Offset
                    BWriter.Write(Convert.ToUInt32(00000000));
                }


                //Align Bytes
                AlignOffset = (BWriter.BaseStream.Position + 512) & (512 - 1);
                BWriter.Seek(AlignOffset, SeekOrigin.Current);
                BWriter.Align(16);

                //Subkeys Values
                foreach (string Value in EuroSoundKey.GetSubKeyNames())
                {
                    FinalSectionOffsets.Add(BWriter.BaseStream.Position);
                    using (RegistryKey KeyToSave = EuroSoundKey.OpenSubKey(Value))
                    {
                        if (KeyToSave != null)
                        {
                            BWriter.Write((uint)KeyToSave.ValueCount);
                            foreach (string SubKeyValue in KeyToSave.GetValueNames())
                            {
                                KeyName = SubKeyValue;
                                KeyType = KeyToSave.GetValueKind(SubKeyValue).ToString();
                                if (KeyType.Equals("DWord"))
                                {
                                    ValueKind = 1;
                                }
                                else if (KeyType.Equals("String"))
                                {
                                    ValueKind = 2;
                                }

                                //Write Key name
                                BWriter.Write(KeyName);

                                //Write type of data that follows
                                BWriter.Write(ValueKind);

                                //Write SubKey Value
                                if (ValueKind == 1)
                                {
                                    NumericKeyValue = int.Parse(KeyToSave.GetValue(SubKeyValue).ToString());
                                    BWriter.Write(NumericKeyValue);
                                }
                                else if (ValueKind == 2)
                                {
                                    KeyValue = KeyToSave.GetValue(SubKeyValue).ToString();
                                    BWriter.Write(KeyValue);
                                }
                            }
                        }
                        KeyToSave.Close();
                    };
                    //Align Bytes
                    AlignOffset = (BWriter.BaseStream.Position + 1024) & (1024 - 1);
                    BWriter.Seek(AlignOffset, SeekOrigin.Current);
                    BWriter.Align(16);
                }

                //Write Final Offsets
                for (int i = 0; i < SectionOffsets.Count; i++)
                {
                    BWriter.Seek((int)SectionOffsets[i], SeekOrigin.Begin);
                    BWriter.Write(Convert.ToUInt32(FinalSectionOffsets[i]));
                }

                BWriter.Close();
            }
        }
    }
}
