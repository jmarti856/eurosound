using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion.Musicbanks;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion.SoundBanks;
using EuroSound_Application.EuroSoundFilesFunctions.NewVersion.StreamFile;
using EuroSound_Application.EuroSoundMusicFilesFunctions;
using EuroSound_Application.EuroSoundSoundBanksFilesFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions
{
    internal class EuroSoundFiles
    {
        internal uint FileVersion = 0;

        internal bool FileIsCorrect(BinaryStream BReader)
        {
            bool FileCorrect = false;

            //Read MAGIC
            string Magic = Encoding.ASCII.GetString(BReader.ReadBytes(3));
            if (Magic.Equals("ESF"))
            {
                //FileVersion
                FileVersion = BReader.ReadUInt32();
                if (FileVersion <= int.Parse(GenericFunctions.GetEuroSoundVersion().Replace(".", "")))
                {
                    FileCorrect = true;
                }
                else
                {
                    MessageBox.Show("This file was written by Eurosound v" + FileVersion + " and cannot be read by v" + GenericFunctions.GetEuroSoundVersion().Replace(".", "") + " or lower.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return FileCorrect;
        }

        internal string LoadEuroSoundFile(TreeView treeViewControl, object dictionaryData, object dictionaryMedia, Dictionary<uint, EXAppTarget> OutputTargets, string filePath, ProjectFile projectProperties)
        {
            string profileName = string.Empty;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            if (treeViewControl.InvokeRequired)
            {
                treeViewControl.Invoke((MethodInvoker)delegate
                {
                    treeViewControl.Enabled = false;
                });
            }
            else
            {
                treeViewControl.Enabled = false;
            }

            using (BufferedStream fileBuffer = new BufferedStream(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryStream BReader = new BinaryStream(fileBuffer))
                {
                    //Init reader
                    if (FileIsCorrect(BReader))
                    {
                        //Older versions, before 1.0.1.3
                        if (FileVersion < 1013)
                        {
                            switch (BReader.ReadSByte())
                            {
                                case (int)Enumerations.ESoundFileType.SoundBanks:
                                    profileName = new ESF_LoadSoundBanks().ReadEuroSoundSoundBankFile(projectProperties, BReader, (Dictionary<uint, EXSound>)dictionaryData, (Dictionary<string, EXAudio>)dictionaryMedia, treeViewControl, (int)FileVersion);
                                    break;
                                case (int)Enumerations.ESoundFileType.StreamSounds:
                                    profileName = new ESF_LoadStreamSounds().ReadEuroSoundStreamFile(projectProperties, BReader, treeViewControl, (Dictionary<uint, EXSoundStream>)dictionaryData, (int)FileVersion);
                                    break;
                                case (int)Enumerations.ESoundFileType.MusicBanks:
                                    profileName = new ESF_LoadMusics().ReadEuroSoundMusicFile(projectProperties, BReader, treeViewControl, (Dictionary<uint, EXMusic>)dictionaryData, (int)FileVersion);
                                    break;
                            }
                        }
                        //Newer versions, from 1.0.1.3
                        else
                        {
                            switch (BReader.ReadSByte())
                            {
                                case (int)Enumerations.ESoundFileType.SoundBanks:
                                    profileName = new ESF_LoadSoundBanks_New().ReadEuroSoundSoundBankFile(projectProperties, BReader, (Dictionary<uint, EXSound>)dictionaryData, (Dictionary<string, EXAudio>)dictionaryMedia, OutputTargets, treeViewControl);
                                    break;
                                case (int)Enumerations.ESoundFileType.StreamSounds:
                                    profileName = new ESF_LoadStreamFile_New().ReadEuroSoundStreamFile(projectProperties, BReader, treeViewControl, (Dictionary<uint, EXSoundStream>)dictionaryData, OutputTargets);
                                    break;
                                case (int)Enumerations.ESoundFileType.MusicBanks:
                                    profileName = new ESF_LoadMusicBanks_New().ReadEuroSoundMusicFile(projectProperties, BReader, treeViewControl, (Dictionary<uint, EXMusic>)dictionaryData, OutputTargets);
                                    break;
                            }
                        }
                    }
                    BReader.Close();
                }
                fileBuffer.Close();
            }

            //Enable again the treeview
            if (treeViewControl.InvokeRequired)
            {
                treeViewControl.Invoke((MethodInvoker)delegate
                {
                    treeViewControl.Enabled = true;
                });
            }
            else
            {
                treeViewControl.Enabled = true;
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            return profileName;
        }

        internal string SaveEuroSoundFile(TreeView TreeViewControl, object dictionaryData, object dictionaryMedia, Dictionary<uint, EXAppTarget> OutputTargets, string FilePath, ProjectFile FileProperties)
        {
            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_GenericSavingFile"));

            using (BinaryStream BWriter = new BinaryStream(File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read), null, Encoding.ASCII))
            {
                //Write header
                WriteFileHeader(BWriter, FileProperties);

                switch (FileProperties.TypeOfData)
                {
                    case (int)Enumerations.ESoundFileType.SoundBanks:
                        new ESF_SaveSoundBanks().SaveSoundBanks(BWriter, TreeViewControl, (Dictionary<uint, EXSound>)dictionaryData, (Dictionary<string, EXAudio>)dictionaryMedia, OutputTargets, FileProperties);
                        break;
                    case (int)Enumerations.ESoundFileType.StreamSounds:
                        new ESF_SaveStreamFile().SaveStreamedSounds(BWriter, TreeViewControl, (Dictionary<uint, EXSoundStream>)dictionaryData, OutputTargets, FileProperties);
                        break;
                    case (int)Enumerations.ESoundFileType.MusicBanks:
                        new ESF_SaveMusicBanks().SaveMusics(BWriter, TreeViewControl, (Dictionary<uint, EXMusic>)dictionaryData, OutputTargets, FileProperties);
                        break;
                }

                //Close
                BWriter.Close();
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            return FilePath;
        }

        private void WriteFileHeader(BinaryStream BWriter, ProjectFile FileProperties)
        {
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            //MAGIC
            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            //FileVersion
            BWriter.WriteUInt32(Convert.ToUInt32(int.Parse(GenericFunctions.GetEuroSoundVersion().Replace(".", ""))));
            //Type of stored data
            BWriter.WriteSByte(FileProperties.TypeOfData);
            //HashCode
            BWriter.WriteUInt32(FileProperties.Hashcode);
            //LastObject ID
            BWriter.WriteUInt32(FileProperties.ObjectID);
        }
    }
}