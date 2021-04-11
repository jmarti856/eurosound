using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;

namespace EuroSound_Application.ApplicationRegistryFunctions
{
    class RestoreSettings
    {
        public void Restore(BinaryReader BReader, RegistryKey EuroSoundKey)
        {
            Dictionary<uint, string> SubKeysToRestore = new Dictionary<uint, string>();
            byte ValueKind;
            uint NumberOfSubKeys, SubkeyOffset, SubkeyItems;
            int NumericSubKeyValue;
            string KeyName, SubKeyName, SubKeyValue;

            NumberOfSubKeys = BReader.ReadUInt32();

            for (int i = 0; i < NumberOfSubKeys; i++)
            {
                KeyName = BReader.ReadString();
                SubkeyOffset = BReader.ReadUInt32();

                SubKeysToRestore.Add(SubkeyOffset, KeyName);
            }

            foreach (KeyValuePair<uint, string> SubKeyInfo in SubKeysToRestore)
            {
                BReader.BaseStream.Seek(SubKeyInfo.Key, SeekOrigin.Begin);
                SubkeyItems = BReader.ReadUInt32();

                //CreateFolder
                CreateSubKeyIfNotExists(SubKeyInfo.Value, EuroSoundKey);
                using (RegistryKey KeyToCreate = EuroSoundKey.OpenSubKey(SubKeyInfo.Value, true))
                {
                    //CreateValues
                    for (int i = 0; i < SubkeyItems; i++)
                    {
                        SubKeyName = BReader.ReadString();
                        ValueKind = BReader.ReadByte();
                        if (ValueKind == 1)
                        {
                            NumericSubKeyValue = BReader.ReadInt32();
                            KeyToCreate.SetValue(SubKeyName, NumericSubKeyValue, RegistryValueKind.DWord);
                        }
                        else if (ValueKind == 2)
                        {
                            SubKeyValue = BReader.ReadString();
                            KeyToCreate.SetValue(SubKeyName, SubKeyValue, RegistryValueKind.String);
                        }
                    }

                    KeyToCreate.Close();
                }
            }
        }

        private void CreateSubKeyIfNotExists(string SubKeyName, RegistryKey EuroSoundKey)
        {
            if (EuroSoundKey.OpenSubKey(SubKeyName, true) == null)
            {
                EuroSoundKey.CreateSubKey(SubKeyName);
            }
        }
    }
}
