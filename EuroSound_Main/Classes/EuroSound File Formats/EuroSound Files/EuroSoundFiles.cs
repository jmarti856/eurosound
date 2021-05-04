using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.EuroSoundMusicFilesFunctions;
using EuroSound_Application.EuroSoundSoundBanksFilesFunctions;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions
{
    internal class EuroSoundFiles
    {
        uint FileVersion = 0;

        internal string LoadSoundBanksDocument(TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties)
        {
            string ProfileName = string.Empty;
            sbyte TypeOfStoredData;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            if (TreeViewControl.InvokeRequired)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Enabled = false;
                });
            }
            else
            {
                TreeViewControl.Enabled = false;
            }

            using (BufferedStream bs = new BufferedStream(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryReader BReader = new BinaryReader(bs, Encoding.ASCII))
                {
                    //Init reader
                    if (FileIsCorrect(BReader))
                    {
                        //Type of stored data
                        TypeOfStoredData = BReader.ReadSByte();
                        if (TypeOfStoredData == 0)
                        {
                            ESF_LoadSoundBanks Version11Reader = new ESF_LoadSoundBanks();
                            ProfileName = Version11Reader.ReadEuroSoundSoundBankFile(FileProperties, BReader, SoundsList, AudiosList, TreeViewControl, (int)FileVersion);
                        }
                    }
                    BReader.Close();
                }
                bs.Close();
            }

            //Enable again the treeview
            if (TreeViewControl.InvokeRequired)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Enabled = true;
                });
            }
            else
            {
                TreeViewControl.Enabled = true;
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            return ProfileName;
        }

        internal string SaveSoundBanksDocument(TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties)
        {
            using (BinaryStream BWriter = new BinaryStream(File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read), null, Encoding.ASCII))
            {

                //*===============================================================================================
                //* HEADER
                //*===============================================================================================
                //MAGIC
                BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
                //FileVersion
                BWriter.Write(Convert.ToUInt32(int.Parse(GenericFunctions.GetEuroSoundVersion().Replace(".", ""))));
                //Type of stored data
                BWriter.Write(Convert.ToSByte(FileProperties.TypeOfData));

                ESF_SaveSoundBanks SaveSoundBank = new ESF_SaveSoundBanks();
                SaveSoundBank.SaveSoundBanks(BWriter, TreeViewControl, SoundsList, AudiosList, FileProperties);

                BWriter.Close();
            }

            return FilePath;
        }

        internal string LoadStreamSoundsDocument(TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, string FilePath, ProjectFile FileProperties, ResourceManager ResxM)
        {
            string ProfileName = string.Empty;
            sbyte TypeOfStoredData;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(ResxM.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            if (TreeViewControl.InvokeRequired)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Enabled = false;
                });
            }
            else
            {
                TreeViewControl.Enabled = false;
            }

            //Init reader
            using (BufferedStream bs = new BufferedStream(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryReader BReader = new BinaryReader(bs, Encoding.ASCII))
                {
                    if (FileIsCorrect(BReader))
                    {
                        //Type of stored data
                        TypeOfStoredData = BReader.ReadSByte();
                        if (TypeOfStoredData == 1)
                        {
                            ESF_LoadStreamSounds Version11Reader = new ESF_LoadStreamSounds();
                            ProfileName = Version11Reader.ReadEuroSoundStreamFile(FileProperties, BReader, TreeViewControl, StreamSoundsList, (int)FileVersion);
                        }
                    }
                    BReader.Close();
                }
                bs.Close();
            }

            //Enable again the treeview
            if (TreeViewControl.InvokeRequired)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Enabled = true;
                });
            }
            else
            {
                TreeViewControl.Enabled = true;
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(ResxM.GetString("StatusBar_Status_Ready"));

            return ProfileName;
        }

        internal string SaveStreamedSoundsBank(TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, string FilePath, ProjectFile FileProperties)
        {
            BinaryStream BWriter = new BinaryStream(File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read), null, Encoding.ASCII);
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            //MAGIC
            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            //FileVersion
            BWriter.Write(Convert.ToUInt32(int.Parse(GenericFunctions.GetEuroSoundVersion().Replace(".", ""))));
            //Type of stored data
            BWriter.Write(Convert.ToSByte(FileProperties.TypeOfData));

            ESF_SaveStreamedSounds SaveSoundBank = new ESF_SaveStreamedSounds();
            SaveSoundBank.SaveStreamedSounds(BWriter, TreeViewControl, StreamSoundsList, FileProperties);

            BWriter.Close();
            BWriter.Dispose();

            return FilePath;
        }

        internal string LoadMusicsDocument(TreeView TreeViewControl, Dictionary<uint, EXMusic> MusicsList, string FilePath, ProjectFile FileProperties, ResourceManager ResxM)
        {
            sbyte TypeOfStoredData;
            string ProfileName = string.Empty;

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(ResxM.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            if (TreeViewControl.InvokeRequired)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Enabled = false;
                });
            }
            else
            {
                TreeViewControl.Enabled = false;
            }

            //Init reader
            using (BufferedStream bs = new BufferedStream(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryReader BReader = new BinaryReader(bs, Encoding.ASCII))
                {
                    if (FileIsCorrect(BReader))
                    {
                        //Type of stored data
                        TypeOfStoredData = BReader.ReadSByte();
                        if (TypeOfStoredData == 2)
                        {
                            ESF_LoadMusics Version11Reader = new ESF_LoadMusics();
                            ProfileName = Version11Reader.ReadEuroSoundMusicFile(FileProperties, BReader, TreeViewControl, MusicsList, (int)FileVersion);
                        }
                    }
                    BReader.Close();
                }
                bs.Close();
            }

            //Enable again the treeview
            if (TreeViewControl.InvokeRequired)
            {
                TreeViewControl.Invoke((MethodInvoker)delegate
                {
                    TreeViewControl.Enabled = true;
                });
            }
            else
            {
                TreeViewControl.Enabled = true;
            }

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(ResxM.GetString("StatusBar_Status_Ready"));

            return ProfileName;
        }

        internal string SaveMusics(TreeView TreeViewControl, Dictionary<uint, EXMusic> StreamSoundsList, string FilePath, ProjectFile FileProperties)
        {
            BinaryStream BWriter = new BinaryStream(File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read), null, Encoding.ASCII);
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            //MAGIC
            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            //FileVersion
            BWriter.Write(Convert.ToUInt32(int.Parse(GenericFunctions.GetEuroSoundVersion().Replace(".", ""))));
            //Type of stored data
            BWriter.Write(Convert.ToSByte(FileProperties.TypeOfData));

            ESF_SaveMusics SaveMusicBank = new ESF_SaveMusics();
            SaveMusicBank.SaveMusics(BWriter, TreeViewControl, StreamSoundsList, FileProperties);

            BWriter.Close();
            BWriter.Dispose();

            return FilePath;
        }

        public bool FileIsCorrect(BinaryReader BReader)
        {
            string Magic;
            bool FileCorrect = false;

            //Read MAGIC
            Magic = Encoding.ASCII.GetString(BReader.ReadBytes(3));
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
    }
}