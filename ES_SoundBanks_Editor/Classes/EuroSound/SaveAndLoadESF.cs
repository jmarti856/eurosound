using FunctionsLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    static class SaveAndLoadESF
    {
        internal static string SaveDocument(TreeView TreeViewControl, Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties)
        {
            BinaryWriter BWriter = new BinaryWriter(File.Open(FilePath, FileMode.Create, FileAccess.Write), Encoding.ASCII);

            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            BWriter.Write(FileProperties.TypeOfData);
            BWriter.Write(FileProperties.FileName);
            BWriter.Write(FileProperties.Hashcode);
            BWriter.Write(FileProperties.SoundID);

            /*Tree view Data*/
            BWriter.Write((Convert.ToInt32(BWriter.BaseStream.Position) + 200));
            BWriter.Seek(200, SeekOrigin.Current);
            SaveTreeViewData(TreeViewControl, BWriter);

            /*SoundsList Data*/
            BWriter.Write((Convert.ToInt32(BWriter.BaseStream.Position) + 100));
            BWriter.Seek(100, SeekOrigin.Current);
            SaveSoundsListData(SoundsList, BWriter);

            /*Audio Data*/
            SaveAudiosData(AudiosList, BWriter);

            BWriter.Close();
            BWriter.Dispose();

            return FilePath;
        }

        private static void SaveSoundsListData(Dictionary<int, EXSound> SoundsList, BinaryWriter BWriter)
        {
            BWriter.Write(SoundsList.Count);

            foreach (KeyValuePair<int, EXSound> SoundItem in SoundsList)
            {
                /*Display Info*/
                BWriter.Write(SoundItem.Key);
                BWriter.Write(SoundItem.Value.Hashcode);
                BWriter.Write(SoundItem.Value.DisplayName);
                BWriter.Write(SoundItem.Value.OutputThisSound);

                /*---Required for EngineX---*/
                BWriter.Write(SoundItem.Value.DuckerLenght);
                BWriter.Write(SoundItem.Value.MinDelay);
                BWriter.Write(SoundItem.Value.MaxDelay);
                BWriter.Write(SoundItem.Value.InnerRadiusReal);
                BWriter.Write(SoundItem.Value.OuterRadiusReal);
                BWriter.Write(SoundItem.Value.ReverbSend);
                BWriter.Write(SoundItem.Value.TrackingType);
                BWriter.Write(SoundItem.Value.MaxVoices);
                BWriter.Write(SoundItem.Value.Priority);
                BWriter.Write(SoundItem.Value.Ducker);
                BWriter.Write(SoundItem.Value.MasterVolume);
                BWriter.Write(SoundItem.Value.Flags);

                /*Write Samples*/
                BWriter.Write(SoundItem.Value.Samples.Count);
                foreach (EXSample ItemSample in SoundItem.Value.Samples)
                {
                    /*Display Info*/
                    BWriter.Write(ItemSample.Name);
                    BWriter.Write(ItemSample.DisplayName);
                    BWriter.Write(ItemSample.IsStreamed);
                    BWriter.Write(ItemSample.FileRef);
                    BWriter.Write(ItemSample.ComboboxSelectedAudio);
                    BWriter.Write(ItemSample.HashcodeSubSFX);

                    /*---Required for EngineX---*/
                    BWriter.Write(ItemSample.PitchOffset);
                    BWriter.Write(ItemSample.RandomPitchOffset);
                    BWriter.Write(ItemSample.BaseVolume);
                    BWriter.Write(ItemSample.RandomVolumeOffset);
                    BWriter.Write(ItemSample.Pan);
                    BWriter.Write(ItemSample.RandomPan);
                }
            }
        }

        private static void SaveTreeViewData(TreeView TreeViewControl, BinaryWriter BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 3));
            SaveTreeNodes(TreeViewControl, TreeViewControl.Nodes[0], BWriter);
            SaveTreeNodes(TreeViewControl, TreeViewControl.Nodes[1], BWriter);
            SaveTreeNodes(TreeViewControl, TreeViewControl.Nodes[2], BWriter);
        }

        private static void SaveTreeNodes(TreeView TreeViewControl, TreeNode Selected, BinaryWriter BWriter)
        {
            if (!Selected.Tag.Equals("Root"))
            {
                if (Selected.Parent == null)
                {
                    BWriter.Write("Root");
                }
                else
                {
                    BWriter.Write(Selected.Parent.Name);
                }
                BWriter.Write(Selected.Name);
                BWriter.Write(Selected.Text);
                BWriter.Write(Selected.SelectedImageIndex);
                BWriter.Write(Selected.ImageIndex);
                BWriter.Write(Selected.Tag.ToString());
                BWriter.Write(Selected.ForeColor.ToArgb());

            }
            foreach (TreeNode Node in Selected.Nodes)
            {
                SaveTreeNodes(TreeViewControl, Node, BWriter);
            }
        }

        private static void SaveAudiosData(Dictionary<string, EXAudio> AudiosList, BinaryWriter BWriter)
        {
            BWriter.Write(AudiosList.Count);

            foreach (KeyValuePair<string, EXAudio> Entry in AudiosList)
            {
                BWriter.Write(Entry.Key);
                BWriter.Write(Entry.Value.Dependencies);
                BWriter.Write(Entry.Value.DisplayName);
                BWriter.Write(Entry.Value.Name);
                BWriter.Write(Entry.Value.Encoding);
                BWriter.Write(Entry.Value.Flags);
                BWriter.Write(Entry.Value.DataSize);
                BWriter.Write(Entry.Value.Frequency);
                BWriter.Write(Entry.Value.RealSize);
                BWriter.Write(Entry.Value.Channels);
                BWriter.Write(Entry.Value.Bits);
                BWriter.Write(Entry.Value.PSIsample);
                BWriter.Write(Entry.Value.LoopOffset);
                BWriter.Write(Entry.Value.Duration);
                BWriter.Write(Entry.Value.PCMdata.Length);
                BWriter.Write(Entry.Value.PCMdata);
            }
        }

        internal static void LoadDocument(TreeView TreeViewControl, Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, string FilePath, ProjectFile FileProperties, ResourceManager ResxM)
        {
            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(ResxM.GetString("StatusBar_ReadingESFFile"));

            //Disable temporaly the treeview
            TreeViewControl.Enabled = false;

            //Init reader
            BinaryReader BReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read), Encoding.ASCII);
            if (FileIsCorrect(BReader))
            {
                FileProperties.TypeOfData = BReader.ReadInt32();
                FileProperties.FileName = BReader.ReadString();
                FileProperties.Hashcode = BReader.ReadString();
                FileProperties.SoundID = BReader.ReadInt32();

                /*Tree view Data*/
                BReader.BaseStream.Position = (BReader.ReadInt32() + 4);
                ReadTreeViewData(BReader, TreeViewControl, FileProperties);

                /*Sounds list data*/
                BReader.BaseStream.Position = (BReader.ReadInt32() + 4);
                ReadSoundsListData(BReader, SoundsList);

                ReadAudiosDictionary(BReader, AudiosList);

                GenericFunctions.SetCurrentFileLabel(FileProperties.FileName);
            }
            BReader.Close();
            BReader.Dispose();

            /*Expand root nodes only*/
            TreeViewControl.Nodes[0].Collapse();
            TreeViewControl.Nodes[0].Expand();
            TreeViewControl.Nodes[1].Collapse();
            TreeViewControl.Nodes[1].Expand();

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
            GenericFunctions.SetProgramStateShowToStatusBar(ResxM.GetString("StatusBar_Status_Ready"));
        }

        private static void UpdateNodeImages(TreeNode Node, Dictionary<int, EXSound> SoundsList)
        {
            if (Node.Tag.Equals("Sound"))
            {
                EXSound sound = EXObjectsFunctions.GetSoundByName(int.Parse(Node.Name), SoundsList);
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

        private static void ReadSoundsListData(BinaryReader BReader, Dictionary<int, EXSound> SoundsList)
        {
            int NumberOfSounds, NumberOfSamples, SoundID;

            NumberOfSounds = BReader.ReadInt32();

            for (int i = 0; i < NumberOfSounds; i++)
            {
                SoundID = BReader.ReadInt32();
                EXSound NewSound = new EXSound
                {
                    Hashcode = BReader.ReadString(),
                    DisplayName = BReader.ReadString(),
                    OutputThisSound = BReader.ReadBoolean(),

                    /*---Required for EngineX---*/
                    DuckerLenght = BReader.ReadInt32(),
                    MinDelay = BReader.ReadInt32(),
                    MaxDelay = BReader.ReadInt32(),
                    InnerRadiusReal = BReader.ReadInt32(),
                    OuterRadiusReal = BReader.ReadInt32(),
                    ReverbSend = BReader.ReadInt32(),
                    TrackingType = BReader.ReadInt32(),
                    MaxVoices = BReader.ReadInt32(),
                    Priority = BReader.ReadInt32(),
                    Ducker = BReader.ReadInt32(),
                    MasterVolume = BReader.ReadInt32(),
                    Flags = BReader.ReadInt32()
                };

                NumberOfSamples = BReader.ReadInt32();
                for (int j = 0; j < NumberOfSamples; j++)
                {
                    EXSample NewSample = new EXSample
                    {
                        Name = BReader.ReadString(),
                        DisplayName = BReader.ReadString(),
                        IsStreamed = BReader.ReadBoolean(),
                        FileRef = BReader.ReadInt32(),
                        ComboboxSelectedAudio = BReader.ReadString(),
                        HashcodeSubSFX = BReader.ReadString(),

                        /*---Required For EngineX---*/
                        PitchOffset = BReader.ReadInt32(),
                        RandomPitchOffset = BReader.ReadInt32(),
                        BaseVolume = BReader.ReadInt32(),
                        RandomVolumeOffset = BReader.ReadInt32(),
                        Pan = BReader.ReadInt32(),
                        RandomPan = BReader.ReadInt32()
                    };

                    NewSound.Samples.Add(NewSample);
                }

                SoundsList.Add(SoundID, NewSound);
            }
        }

        private static void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl, ProjectFile CurrentProject)
        {
            int NumberOfNodes;

            NumberOfNodes = BReader.ReadInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                TreeNodeFunctions.TreeNodeAddNewNode(BReader.ReadString(), BReader.ReadString(), BReader.ReadString(), BReader.ReadInt32(), BReader.ReadInt32(), BReader.ReadString(), Color.FromArgb(BReader.ReadInt32()), TreeViewControl, CurrentProject);
            }
        }

        private static void ReadAudiosDictionary(BinaryReader BReader, Dictionary<string, EXAudio> AudiosList)
        {
            int TotalEntries, PCMDataLength;
            string HashMD5;

            TotalEntries = BReader.ReadInt32();

            for (int i = 0; i < TotalEntries; i++)
            {
                HashMD5 = BReader.ReadString();
                EXAudio AudioToAdd = new EXAudio
                {
                    DisplayName = BReader.ReadString(),
                    Dependencies = BReader.ReadString(),
                    Name = BReader.ReadString(),
                    Encoding = BReader.ReadString(),
                    Flags = BReader.ReadInt32(),
                    DataSize = BReader.ReadInt32(),
                    Frequency = BReader.ReadInt32(),
                    RealSize = BReader.ReadInt32(),
                    Channels = BReader.ReadInt32(),
                    Bits = BReader.ReadInt32(),
                    PSIsample = BReader.ReadInt32(),
                    LoopOffset = BReader.ReadInt32(),
                    Duration = BReader.ReadInt32()
                };
                PCMDataLength = BReader.ReadInt32();
                AudioToAdd.PCMdata = BReader.ReadBytes(PCMDataLength);

                AudiosList.Add(HashMD5, AudioToAdd);
            }
        }

        private static bool FileIsCorrect(BinaryReader BReader)
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
    }
}
