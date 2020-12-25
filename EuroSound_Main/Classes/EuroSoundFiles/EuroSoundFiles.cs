using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal class EuroSoundFiles
    {
        internal void LoadSoundBanksDocument(TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties, ResourceManager ResxM)
        {
            uint Version;
            sbyte TypeOfStoredData;
            /*Update Status Bar*/
            GenericFunctions.SetStatusToStatusBar(ResxM.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            TreeViewControl.Enabled = false;

            //Init reader
            BinaryReader BReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read), Encoding.ASCII);
            if (FileIsCorrect(BReader))
            {
                //*===============================================================================================
                //* HEADER
                //*===============================================================================================
                /*FileVersion*/
                Version = BReader.ReadUInt32();
                if (Version == 10) //Only Soundbanks
                {
                    LoadEuroSoundFileV10 Version10Reader = new LoadEuroSoundFileV10();
                    Version10Reader.ReadEuroSoundFile10(FileProperties, BReader, TreeViewControl, SoundsList, AudiosList);
                }
                else if (Version == 11)
                {
                    /*Type of stored data*/
                    TypeOfStoredData = BReader.ReadSByte();
                    if (TypeOfStoredData == 0)
                    {
                        ESF_LoadSoundBanks Version11Reader = new ESF_LoadSoundBanks();
                        Version11Reader.ReadEuroSoundFile11(FileProperties, BReader, TreeViewControl, SoundsList, AudiosList);
                    }
                }
                else
                {
                    MessageBox.Show("This file was written by Eurosound v" + Version + " and cannot be read by v10 or lower.", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            BReader.Close();
            BReader.Dispose();

            /*Expand root nodes only*/
            TreeViewControl.Nodes[0].Collapse();
            TreeViewControl.Nodes[0].Expand();
            TreeViewControl.Nodes[1].Collapse();
            TreeViewControl.Nodes[1].Expand();
            TreeViewControl.Nodes[2].Collapse();
            TreeViewControl.Nodes[2].Expand();

            TreeViewControl.Sort();
            TreeViewControl.TreeViewNodeSorter = new NodeSorter();

            /*Update images*/
            foreach (TreeNode Node in TreeViewControl.Nodes)
            {
                UpdateNodeImages(Node, SoundsList);
            }

            //Enable again the treeview
            TreeViewControl.Enabled = true;

            /*Update Status Bar*/
            GenericFunctions.SetStatusToStatusBar(ResxM.GetString("StatusBar_Status_Ready"));
        }

        internal string SaveSoundBanksDocument(TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties)
        {
            BinaryWriter BWriter = new BinaryWriter(File.Open(FilePath, FileMode.Create, FileAccess.Write), Encoding.ASCII);
            //*===============================================================================================
            //* HEADER
            //*===============================================================================================
            /*MAGIC*/
            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            /*FileVersion*/
            BWriter.Write(Convert.ToUInt32(11));
            /*Type of stored data*/
            BWriter.Write(Convert.ToSByte(FileProperties.TypeOfData));

            if (FileProperties.TypeOfData == 0)
            {
                ESF_SaveSoundBanks SaveSoundBank = new ESF_SaveSoundBanks();
                SaveSoundBank.SaveSoundBanks(BWriter, TreeViewControl, SoundsList, AudiosList, FileProperties);
            }

            BWriter.Close();
            BWriter.Dispose();

            return FilePath;
        }
        public bool FileIsCorrect(BinaryReader BReader)
        {
            string Magic;
            bool FileCorrect;

            //Read MAGIC
            Magic = Encoding.ASCII.GetString(BReader.ReadBytes(3));

            if (Magic.Equals("ESF"))
            {
                FileCorrect = true;
            }
            else
            {
                FileCorrect = false;
            }

            return FileCorrect;
        }

        private void UpdateNodeImages(TreeNode Node, Dictionary<uint, EXSound> SoundsList)
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
                    UpdateNodeImages(tn, SoundsList);
                }
            }
        }
    }
}