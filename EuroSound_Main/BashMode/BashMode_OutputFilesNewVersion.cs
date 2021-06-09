using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Classes.BashMode;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion.Musicbanks;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion.SoundBanks;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion.StreamFile;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application.BashMode
{
    internal class BashMode_OutputFilesNewVersion
    {
        private EuroSoundFiles_CommonFunctions ESF_CommonFunctions = new EuroSoundFiles_CommonFunctions();

        private EXAppTarget GetRequestedAppTarget(string targetName, Dictionary<uint, EXAppTarget> OutputTargets)
        {
            EXAppTarget targetToUse = null;

            foreach (EXAppTarget itemTarget in OutputTargets.Values)
            {
                if (itemTarget.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase))
                {
                    targetToUse = itemTarget;
                    break;
                }
            }
            return targetToUse;
        }

        internal void OutputSoundBank(BinaryStream BReader, string selectedTarget, int Version)
        {
            ESF_LoadSoundBanks_New SectionsReader = new ESF_LoadSoundBanks_New();

            Dictionary<uint, EXSound> SoundsList = new Dictionary<uint, EXSound>();
            Dictionary<string, EXAudio> AudiosList = new Dictionary<string, EXAudio>();
            Dictionary<uint, EXAppTarget> OutputTargets = new Dictionary<uint, EXAppTarget>();

            //*===============================================================================================
            //* ESF FILE
            //*===============================================================================================
            //File Hashcode
            uint File_Hashcode = BReader.ReadUInt32();
            //Latest SoundID value
            BReader.ReadUInt32();
            //File Size
            BReader.ReadUInt32();
            //AudioData Offset
            uint audioDictionaryOffset = BReader.ReadUInt32();
            //SoundsListData Offset
            uint soundDictionaryOffset = BReader.ReadUInt32();
            //TreeViewData Offset
            BReader.ReadUInt32();
            //Target App
            uint targetDictionaryOffset = BReader.ReadUInt32();
            //File Section 5
            BReader.ReadUInt32();
            //File Section 6
            BReader.ReadUInt32();
            //Project Name
            BReader.ReadString();
            //Project Description
            BReader.ReadString();
            //Profile Name
            string profileSelectedName = BReader.ReadString();
            //Profile Path
            string profileSelected = BReader.ReadString();

            GenericFunctions.CheckProfiles(profileSelected, profileSelectedName);

            //*===============================================================================================
            //* Audio Data
            //*===============================================================================================
            BReader.BaseStream.Seek(audioDictionaryOffset, SeekOrigin.Begin);
            SectionsReader.ReadAudioDataDictionary(BReader, AudiosList, Version);

            //*===============================================================================================
            //* Sounds List Data
            //*===============================================================================================
            BReader.BaseStream.Seek(soundDictionaryOffset, SeekOrigin.Begin);
            SectionsReader.ReadSoundsListData(BReader, SoundsList, Version);

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            BReader.BaseStream.Seek(targetDictionaryOffset, SeekOrigin.Begin);
            ESF_CommonFunctions.ReadAppTargetData(BReader, OutputTargets);

            //*===============================================================================================
            //* CREATE SFX FILE
            //*===============================================================================================
            //Get target
            EXAppTarget requestedTarget = GetRequestedAppTarget(selectedTarget, OutputTargets);

            //Output file
            if (requestedTarget != null)
            {
                //Create directories
                string directoryPath = Path.Combine(requestedTarget.OutputDirectory, string.Join("", "_bin_", requestedTarget.Name));
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, "_Eng", requestedTarget.BinaryName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                //Create file
                if (Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    using (BinaryStream BWriter = new BinaryStream(File.Open(filePath, FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                    {
                        new BashMode_CommonFunctions().CreateSFXSoundBanks(BWriter, SoundsList, AudiosList, File_Hashcode, requestedTarget.Name);
                    }
                }
            }
        }

        internal void OutputStreamFile(BinaryStream BReader, string selectedTarget)
        {
            ESF_LoadStreamFile_New SectionsReader = new ESF_LoadStreamFile_New();
            Dictionary<uint, EXAppTarget> OutputTargets = new Dictionary<uint, EXAppTarget>();
            Dictionary<uint, EXSoundStream> DictionaryData = new Dictionary<uint, EXSoundStream>();

            //*===============================================================================================
            //* ESF FILE
            //*===============================================================================================
            //File Hashcode
            uint fileHashcode = BReader.ReadUInt32();
            //Latest SoundID value
            BReader.ReadUInt32();
            //File Size
            BReader.ReadUInt32();
            //Dictionary Data Offset
            uint mediaDictionaryOffset = BReader.ReadUInt32();
            //TreeViewData Offset
            BReader.ReadUInt32();
            //Target App 
            uint targetDictionaryOffset = BReader.ReadUInt32();
            //Sounds List Offset
            BReader.ReadUInt32(); //Only Used in the "Frm_NewStreamSound" Form
            //File Section 5
            BReader.ReadUInt32();
            //File Section 6
            BReader.ReadUInt32();
            //Project Name
            BReader.ReadString();
            //Project Description
            BReader.ReadString();
            //Profile Name
            string profileSelectedName = BReader.ReadString();
            //Profile Path
            string profileSelected = BReader.ReadString();

            GenericFunctions.CheckProfiles(profileSelected, profileSelectedName);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            BReader.BaseStream.Seek(mediaDictionaryOffset, SeekOrigin.Begin);
            SectionsReader.ReadDictionaryData(BReader, DictionaryData);

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            BReader.BaseStream.Seek(targetDictionaryOffset, SeekOrigin.Begin);
            ESF_CommonFunctions.ReadAppTargetData(BReader, OutputTargets);

            //*===============================================================================================
            //* CREATE SFX FILE
            //*===============================================================================================
            //Get target
            EXAppTarget requestedTarget = GetRequestedAppTarget(selectedTarget, OutputTargets);

            //Output file
            if (requestedTarget != null)
            {
                //Create directories
                string directoryPath = Path.Combine(requestedTarget.OutputDirectory, string.Join("", "_bin_", requestedTarget.Name));
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, "_Eng", requestedTarget.BinaryName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                //Create file
                if (Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    using (BinaryStream BWriter = new BinaryStream(File.Open(filePath, FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                    {
                        new BashMode_CommonFunctions().CreateSFXStreamFie(BWriter, DictionaryData, fileHashcode, requestedTarget.Name);
                    }
                }
            }
        }

        internal void OutputMusicBank(BinaryStream BReader, string selectedTarget)
        {
            ESF_LoadMusicBanks_New SectionsReader = new ESF_LoadMusicBanks_New();
            Dictionary<uint, EXMusic> DictionaryData = new Dictionary<uint, EXMusic>();
            Dictionary<uint, EXAppTarget> OutputTargets = new Dictionary<uint, EXAppTarget>();

            //*===============================================================================================
            //* ESF FILE
            //*===============================================================================================
            //File Hashcode
            uint fileHashcode = BReader.ReadUInt32();
            //Latest SoundID value
            BReader.ReadUInt32();
            //File Size
            BReader.ReadUInt32();
            //Dictionary Data Offset
            uint mediaDictionaryOffset = BReader.ReadUInt32();
            //TreeViewData Offset
            BReader.ReadUInt32();
            //Target App 
            uint targetDictionaryOffset = BReader.ReadUInt32();
            //File Section 4
            BReader.ReadUInt32();
            //File Section 5
            BReader.ReadUInt32();
            //File Section 6
            BReader.ReadUInt32();
            //Project Name
            BReader.ReadString();
            //Project Description
            BReader.ReadString();
            //Profile Name
            string profileSelectedName = BReader.ReadString();
            //Profile Path
            string profileSelected = BReader.ReadString();

            GenericFunctions.CheckProfiles(profileSelected, profileSelectedName);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            BReader.BaseStream.Seek(mediaDictionaryOffset, SeekOrigin.Begin);
            SectionsReader.ReadDictionaryData(BReader, DictionaryData);

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            BReader.BaseStream.Seek(targetDictionaryOffset, SeekOrigin.Begin);
            ESF_CommonFunctions.ReadAppTargetData(BReader, OutputTargets);

            //*===============================================================================================
            //* CREATE SFX FILE
            //*===============================================================================================
            //Get target
            EXAppTarget requestedTarget = GetRequestedAppTarget(selectedTarget, OutputTargets);

            //Output file
            if (requestedTarget != null)
            {
                //Create directories
                string directoryPath = Path.Combine(requestedTarget.OutputDirectory, string.Join("", "_bin_", requestedTarget.Name));
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, "music", requestedTarget.BinaryName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                //Create file
                if (Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    using (BinaryStream BWriter = new BinaryStream(File.Open(filePath, FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                    {
                        new BashMode_CommonFunctions().CreateSFXMusicFile(BWriter, DictionaryData, fileHashcode, requestedTarget.Name);
                    }
                }
            }
        }
    }
}
