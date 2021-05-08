using Microsoft.Win32;
using System.IO;
using System.Text;

namespace EuroSound_Application.ApplicationRegistryFunctions
{
    internal class BackupReloadSettings
    {
        private RegistryKey EurocomKey, EuroSoundKey;
        private RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);

        public void BackupSettings(string SaveFilePath)
        {
            if (CheckForEuroSoundRegistryKeys())
            {
                BackupSettings CreateSettingsFile = new BackupSettings();
                CreateSettingsFile.Save(SaveFilePath, EuroSoundKey);
            }
            DisposeKeys();
        }

        public void RestoreSettings(string FilePath)
        {
            if (CheckForEuroSoundRegistryKeys())
            {
                using (BinaryReader BReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
                {
                    if (FileIsCorrect(BReader))
                    {
                        RestoreSettings RestoreSettingsFile = new RestoreSettings();
                        RestoreSettingsFile.Restore(BReader, EuroSoundKey);
                    }
                    BReader.Close();
                }
            }
            DisposeKeys();
        }

        private bool CheckForEuroSoundRegistryKeys()
        {
            bool KeysExist = false;

            if (SoftwareKey.OpenSubKey("Eurocomm", true) != null)
            {
                EurocomKey = SoftwareKey.OpenSubKey("Eurocomm", true);
                if (EurocomKey.OpenSubKey("EuroSound", true) != null)
                {
                    EuroSoundKey = EurocomKey.OpenSubKey("EuroSound", true);
                    KeysExist = true;
                }
            }

            return KeysExist;
        }

        private bool FileIsCorrect(BinaryReader BReader)
        {
            string MAGIC;
            sbyte Version;
            bool FileIsCorrect = false;

            MAGIC = Encoding.ASCII.GetString(BReader.ReadBytes(4));

            if (MAGIC.Equals("ESRF"))
            {
                Version = BReader.ReadSByte();
                if (Version == 10)
                {
                    FileIsCorrect = true;
                }
            }

            return FileIsCorrect;
        }

        private void DisposeKeys()
        {
            EuroSoundKey.Close();
            EurocomKey.Close();
            SoftwareKey.Close();

            EuroSoundKey.Dispose();
            EurocomKey.Dispose();
            SoftwareKey.Dispose();
        }
    }
}
