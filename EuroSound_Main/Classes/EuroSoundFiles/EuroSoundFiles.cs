using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using EuroSound_Application.TreeViewLibraryFunctions;
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
        internal void LoadSoundBanksDocument(TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties)
        {
            sbyte TypeOfStoredData;

            /*Update Status Bar*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            TreeViewControl.Enabled = false;

            using (BufferedStream bs = new BufferedStream(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryReader BReader = new BinaryReader(bs, Encoding.ASCII))
                {
                    //Init reader
                    if (FileIsCorrect(BReader))
                    {
                        /*Type of stored data*/
                        TypeOfStoredData = BReader.ReadSByte();
                        if (TypeOfStoredData == 0)
                        {
                            ESF_LoadSoundBanks Version11Reader = new ESF_LoadSoundBanks();
                            Version11Reader.ReadEuroSoundFile11(FileProperties, BReader, SoundsList, AudiosList, TreeViewControl);
                        }
                    }
                    BReader.Close();
                }
                bs.Close();
            }

            //Sort nodes
            TreeViewControl.TreeViewNodeSorter = new NodeSorter();

            /*Update images*/
            foreach (TreeNode Node in TreeViewControl.Nodes)
            {
                UpdateNodeImagesSoundBank(Node, SoundsList);
            }

            //Enable again the treeview
            TreeViewControl.Enabled = true;

            /*Update Status Bar*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));


        }

        internal string SaveSoundBanksDocument(TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties)
        {
            BinaryStream BWriter = new BinaryStream(File.Open(FilePath, FileMode.Create, FileAccess.Write));
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            /*MAGIC*/
            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            /*FileVersion*/
            BWriter.Write(Convert.ToUInt32(11));
            /*Type of stored data*/
            BWriter.Write(Convert.ToSByte(FileProperties.TypeOfData));

            ESF_SaveSoundBanks SaveSoundBank = new ESF_SaveSoundBanks();
            SaveSoundBank.SaveSoundBanks(BWriter, TreeViewControl, SoundsList, AudiosList, FileProperties);

            BWriter.Close();
            BWriter.Dispose();

            return FilePath;
        }

        internal void LoadStreamSoundsDocument(TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, string FilePath, ProjectFile FileProperties, ResourceManager ResxM)
        {
            sbyte TypeOfStoredData;
            /*Update Status Bar*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(ResxM.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            TreeViewControl.Enabled = false;

            //Init reader
            using (BufferedStream bs = new BufferedStream(File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryReader BReader = new BinaryReader(bs, Encoding.ASCII))
                {
                    if (FileIsCorrect(BReader))
                    {
                        /*Type of stored data*/
                        TypeOfStoredData = BReader.ReadSByte();
                        if (TypeOfStoredData == 1)
                        {
                            ESF_LoadStreamSounds Version11Reader = new ESF_LoadStreamSounds();
                            Version11Reader.ReadEuroSoundFile11(FileProperties, BReader, TreeViewControl, StreamSoundsList);
                        }
                    }
                    BReader.Close();
                }
                bs.Close();
            }

            /*Expand root nodes only*/
            TreeViewControl.Nodes[0].Collapse();
            TreeViewControl.Nodes[0].Expand();

            /*Update images*/
            foreach (TreeNode Node in TreeViewControl.Nodes)
            {
                UpdateNodeImagesStreamSoundBank(Node, StreamSoundsList);
            }

            //Enable again the treeview
            TreeViewControl.Enabled = true;

            /*Update Status Bar*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(ResxM.GetString("StatusBar_Status_Ready"));
        }

        internal string SaveStreamedSoundsBank(TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, string FilePath, ProjectFile FileProperties)
        {
            BinaryStream BWriter = new BinaryStream(File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read));
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            /*MAGIC*/
            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            /*FileVersion*/
            BWriter.Write(Convert.ToUInt32(11));
            /*Type of stored data*/
            BWriter.Write(Convert.ToSByte(FileProperties.TypeOfData));

            ESF_SaveStreamedSounds SaveSoundBank = new ESF_SaveStreamedSounds();
            SaveSoundBank.SaveStreamedSounds(BWriter, TreeViewControl, StreamSoundsList, FileProperties);

            BWriter.Close();
            BWriter.Dispose();

            return FilePath;
        }

        public bool FileIsCorrect(BinaryReader BReader)
        {
            string Magic;
            uint Version;
            bool FileCorrect = false;

            //Read MAGIC
            Magic = Encoding.ASCII.GetString(BReader.ReadBytes(3));
            if (Magic.Equals("ESF"))
            {
                /*FileVersion*/
                Version = BReader.ReadUInt32();
                if (Version == 11)
                {
                    FileCorrect = true;
                }
                else
                {
                    MessageBox.Show("This file was written by Eurosound v" + Version + " and cannot be read by v1.1 or lower.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return FileCorrect;
        }

        private void UpdateNodeImagesSoundBank(TreeNode Node, Dictionary<uint, EXSound> SoundsList)
        {
            if (Node.Tag.Equals("Sound"))
            {
                EXSound sound = EXSoundbanksFunctions.GetSoundByName(uint.Parse(Node.Name), SoundsList);
                if (sound != null)
                {
                    if (!sound.OutputThisSound)
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(Node, 5, 5);
                    }
                }
            }
            else
            {
                foreach (TreeNode tn in Node.Nodes)
                {
                    UpdateNodeImagesSoundBank(tn, SoundsList);
                }
            }
        }

        private void UpdateNodeImagesStreamSoundBank(TreeNode Node, Dictionary<uint, EXSoundStream> SoundsList)
        {
            if (Node.Tag.Equals("Sound"))
            {
                EXSoundStream sound = EXStreamSoundsFunctions.GetSoundByName(uint.Parse(Node.Name), SoundsList);
                if (sound != null)
                {
                    if (!sound.OutputThisSound)
                    {
                        TreeNodeFunctions.TreeNodeSetNodeImage(Node, 5, 5);
                    }
                }
            }
            else
            {
                foreach (TreeNode tn in Node.Nodes)
                {
                    UpdateNodeImagesStreamSoundBank(tn, SoundsList);
                }
            }
        }
    }
}