using EuroSound_Application.Classes.BashMode;
using EuroSound_Application.EuroSoundFilesFunctions;
using EuroSound_Application.EuroSoundMusicFilesFunctions;
using EuroSound_Application.EuroSoundSoundBanksFilesFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application.BashMode
{
    internal class BashMode_OutputFilesOldVersion
    {
        internal void OutputSoundBank(BinaryStream BReader, string filePath, string target)
        {
            EuroSoundFiles ESoundFiles = new EuroSoundFiles();
            ESF_LoadSoundBanks SectionsReader = new ESF_LoadSoundBanks();

            Dictionary<uint, EXSound> SoundsList = new Dictionary<uint, EXSound>();
            Dictionary<string, EXAudio> AudiosList = new Dictionary<string, EXAudio>();

            //*===============================================================================================
            //* ESF FILE
            //*===============================================================================================
            //File Hashcode
            uint File_Hashcode = BReader.ReadUInt32();
            //Latest SoundID value
            BReader.ReadUInt32();
            //TreeView Data
            BReader.ReadUInt32();
            //SoundsListData Offset -- Not used for now
            uint SoundsListDataOffset = BReader.ReadUInt32();
            //AudioData Offset -- Not used for now
            uint AudioDataOffset = BReader.ReadUInt32();
            //FullSize
            BReader.ReadUInt32();
            //File Name
            BReader.ReadString();
            //Profile Path
            string ProfileSelected = BReader.ReadString();
            //Profile Name
            string ProfileSelectedName = BReader.ReadString();

            GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

            //--------------------------[SOUNDS LIST DATA]--------------------------
            BReader.BaseStream.Position = (SoundsListDataOffset);
            SectionsReader.ReadSoundsListData(BReader, SoundsList);

            //--------------------------[AUDIO DATA]--------------------------
            BReader.BaseStream.Position = (AudioDataOffset);
            SectionsReader.ReadAudioDataDictionary(BReader, AudiosList, (int)ESoundFiles.FileVersion);

            //*===============================================================================================
            //* CREATE SFX FILE
            //*===============================================================================================
            string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

            if (Directory.Exists(filePath))
            {
                using (BinaryStream BWriter = new BinaryStream(File.Open(Path.Combine(filePath, FileName + ".SFX"), FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                {
                    new BashMode_CommonFunctions().CreateSFXSoundBanks(BWriter, SoundsList, AudiosList, File_Hashcode, target);
                }
            }
        }

        internal void OutputStreamFile(BinaryStream BReader, string filePath)
        {
            ESF_LoadStreamSounds SectionsReader = new ESF_LoadStreamSounds();
            Dictionary<uint, EXSoundStream> DictionaryData = new Dictionary<uint, EXSoundStream>();

            //*===============================================================================================
            //* ESF FILE
            //*===============================================================================================
            //File Hashcode
            uint File_Hashcode = BReader.ReadUInt32();
            //Sound ID
            BReader.ReadUInt32();
            //Sounds List Offset
            BReader.ReadUInt32();//Only Used in the "Frm_NewStreamSound" Form
            BReader.ReadUInt32();//TreeViewData Offset
            //Dictionary Data Offset
            uint StreamSoundsDictionaryOffset = BReader.ReadUInt32();
            //FileSize
            BReader.ReadUInt32();
            //File Name
            BReader.ReadString();
            //Profile Path
            string ProfileSelected = BReader.ReadString();
            //Profile Name
            string ProfileSelectedName = BReader.ReadString();

            GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

            //--------------------------[SOUNDS DATA]--------------------------
            BReader.BaseStream.Position = (StreamSoundsDictionaryOffset);
            SectionsReader.ReadDictionaryData(BReader, DictionaryData);

            //*===============================================================================================
            //* CREATE SFX FILE
            //*===============================================================================================
            string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

            if (Directory.Exists(filePath))
            {
                using (BinaryStream BWriter = new BinaryStream(File.Open(Path.Combine(filePath, FileName + ".SFX"), FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                {
                    new BashMode_CommonFunctions().CreateSFXStreamFie(BWriter, DictionaryData, File_Hashcode);
                }
            }
        }

        internal void OutputMusicBank(BinaryStream BReader, string filePath, string target)
        {
            uint MusicsDictionaryOffset, File_Hashcode;
            ESF_LoadMusics SectionsReader = new ESF_LoadMusics();
            Dictionary<uint, EXMusic> DictionaryData = new Dictionary<uint, EXMusic>();

            //*===============================================================================================
            //* ESF FILE
            //*===============================================================================================
            //File Hashcode
            File_Hashcode = BReader.ReadUInt32();
            //Sound ID
            BReader.ReadUInt32();
            //Sounds List Offset
            BReader.ReadUInt32();
            //TreeViewData Offset
            BReader.ReadUInt32();
            //Dictionary Data Offset
            MusicsDictionaryOffset = BReader.ReadUInt32();
            //FileSize
            BReader.ReadUInt32();
            //File Name
            BReader.ReadString();
            //Profile Path
            string ProfileSelected = BReader.ReadString();
            //Profile Name
            string ProfileSelectedName = BReader.ReadString();

            GenericFunctions.CheckProfiles(ProfileSelected, ProfileSelectedName);

            //--------------------------[SOUNDS DATA]--------------------------
            BReader.BaseStream.Position = (MusicsDictionaryOffset);
            SectionsReader.ReadDictionaryData(BReader, DictionaryData);

            //*===============================================================================================
            //* CREATE SFX FILE
            //*===============================================================================================
            string FileName = "HC" + File_Hashcode.ToString("X8").Substring(2);

            if (Directory.Exists(filePath))
            {
                using (BinaryStream BWriter = new BinaryStream(File.Open(Path.Combine(filePath, FileName + ".SFX"), FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                {
                    new BashMode_CommonFunctions().CreateSFXMusicFile(BWriter, DictionaryData, File_Hashcode, target);
                }
            }
        }
    }
}
